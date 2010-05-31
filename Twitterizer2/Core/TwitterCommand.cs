//-----------------------------------------------------------------------
// <copyright file="TwitterCommand.cs" company="Patrick Ricky Smith">
//  This file is part of the Twitterizer library (http://www.twitterizer.net/)
// 
//  Copyright (c) 2010, Patrick "Ricky" Smith (ricky@digitally-born.com)
//  All rights reserved.
//  
//  Redistribution and use in source and binary forms, with or without modification, are 
//  permitted provided that the following conditions are met:
// 
//  - Redistributions of source code must retain the above copyright notice, this list 
//    of conditions and the following disclaimer.
//  - Redistributions in binary form must reproduce the above copyright notice, this list 
//    of conditions and the following disclaimer in the documentation and/or other 
//    materials provided with the distribution.
//  - Neither the name of the Twitterizer nor the names of its contributors may be 
//    used to endorse or promote products derived from this software without specific 
//    prior written permission.
// 
//  THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND 
//  ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED 
//  WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. 
//  IN NO EVENT SHALL THE COPYRIGHT OWNER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, 
//  INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT 
//  NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR 
//  PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, 
//  WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) 
//  ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE 
//  POSSIBILITY OF SUCH DAMAGE.
// </copyright>
// <author>Ricky Smith</author>
// <summary>The base class for all command classes.</summary>
//-----------------------------------------------------------------------

namespace Twitterizer.Core
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Globalization;
    using System.IO;
    using System.Net;
    using System.Text;
    using System.Web;
    using System.Web.Caching;
    using Twitterizer;

    /// <summary>
    /// Enumeration of the supported HTTP verbs supported by the <see cref="Twitterizer.Core.CommandPerformer{T}"/>
    /// </summary>
    internal enum HTTPVerb
    {
        /// <summary>
        /// The HTTP GET method is used to retrieve data.
        /// </summary>
        GET,

        /// <summary>
        /// The HTTP POST method is used to transmit data.
        /// </summary>
        POST,

        /// <summary>
        /// The HTTP DELETE method is used to indicate that a resource should be deleted.
        /// </summary>
        DELETE
    }

    /// <summary>
    /// The base command class.
    /// </summary>
    /// <typeparam name="T">The business object the command should return.</typeparam>
    [Serializable]
    internal abstract class TwitterCommand<T> : ICommand<T>
        where T : ITwitterObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TwitterCommand&lt;T&gt;"/> class.
        /// </summary>
        /// <param name="method">The method.</param>
        /// <param name="endPoint">The end point.</param>
        /// <param name="tokens">The tokens.</param>
        /// <param name="optionalProperties">The optional properties.</param>
        protected TwitterCommand(HTTPVerb method, string endPoint, OAuthTokens tokens, OptionalProperties optionalProperties)
        {
            this.RequestParameters = new Dictionary<string, string>();
            this.Verb = method;
            this.Tokens = tokens;
            this.OptionalProperties = optionalProperties == null ? new OptionalProperties() : optionalProperties;

            this.SetCommandUri(endPoint);
        }

        /// <summary>
        /// Gets or sets the optional properties.
        /// </summary>
        /// <value>The optional properties.</value>
        public OptionalProperties OptionalProperties { get; set; }

        /// <summary>
        /// Gets or sets the API method URI.
        /// </summary>
        /// <value>The URI for the API method.</value>
        public Uri Uri { get; set; }

        /// <summary>
        /// Gets or sets the method.
        /// </summary>
        /// <value>The method.</value>
        public HTTPVerb Verb { get; set; }

        /// <summary>
        /// Gets or sets the request parameters.
        /// </summary>
        /// <value>The request parameters.</value>
        public Dictionary<string, string> RequestParameters { get; set; }

         /// <summary>
        /// Gets or sets the serialization delegate.
        /// </summary>
        /// <value>The serialization delegate.</value>
        public SerializationHelper<T>.DeserializationHandler DeserializationHandler { get; set; }

        /// <summary>
        /// Gets the request tokens.
        /// </summary>
        /// <value>The request tokens.</value>
        internal OAuthTokens Tokens { get; private set; }

        /// <summary>
        /// Initializes the command.
        /// </summary>
        public abstract void Init();

        /// <summary>
        /// Executes the command.
        /// </summary>
        /// <returns>The results of the command.</returns>
        public T ExecuteCommand()
        {
            Debug.WriteLine(string.Format(CultureInfo.CurrentCulture, "Begin {0}", this.Uri.AbsoluteUri), "Twitterizer2");

            // Check if the command is flagged to check for rate limiting.
            if (this.GetType().GetCustomAttributes(typeof(RateLimitedAttribute), false).Length > 0)
            {
                // Get the rate limiting status
                if (TwitterRateLimitStatus.GetStatus(this.Tokens).RemainingHits == 0)
                {
                    throw new TwitterizerException("You are being rate limited.");
                }
            }

            WebPermission permission = new WebPermission();
            permission.AddPermission(NetworkAccess.Connect, @"https?://api.twitter.com/.*");
            permission.AddPermission(NetworkAccess.Connect, @"https?://search.twitter.com/.*");
            permission.Demand();

            // Variables and objects needed for caching
            StringBuilder cacheKeyBuilder = new StringBuilder(this.Uri.AbsoluteUri);
            if (this.Tokens != null)
            {
                cacheKeyBuilder.AppendFormat("|{0}|{1}", this.Tokens.ConsumerKey, this.Tokens.ConsumerKey);
            }

            Cache cache = HttpRuntime.Cache;

            // Prepare the query parameters
            Dictionary<string, string> queryParameters = new Dictionary<string, string>();
            foreach (KeyValuePair<string, string> item in this.RequestParameters)
            {
                queryParameters.Add(item.Key, item.Value);
                cacheKeyBuilder.AppendFormat("|{0}={1}", item.Key, item.Value);
            }

            // Lookup the cached item and return it
            if (this.OptionalProperties.CacheOutput && cache[cacheKeyBuilder.ToString()] != null)
            {
                if (cache[cacheKeyBuilder.ToString()] is T)
                {
                    Debug.WriteLine("Found in cache", "Twitterizer2");
                    Debug.WriteLine(string.Format(CultureInfo.CurrentCulture, "End {0}", this.Uri.AbsoluteUri), "Twitterizer2");
                    return (T)cache[cacheKeyBuilder.ToString()];
                }
            }

            // Declare the variable to be returned
            T resultObject = default(T);
            RequestStatus requestStatus = null;

            // This must be set for all twitter request.
            System.Net.ServicePointManager.Expect100Continue = false;

            byte[] responseData = null;
            
            try
            {
                HttpWebResponse webResponse;

                // If we have OAuth tokens, then build and execute an OAuth request.
                if (this.Tokens != null)
                {
                    webResponse = OAuthUtility.BuildOAuthRequestAndGetResponse(
                        this.Uri.AbsoluteUri,
                        queryParameters,
                        this.Verb,
                        this.Tokens.ConsumerKey,
                        this.Tokens.ConsumerSecret,
                        this.Tokens.AccessToken,
                        this.Tokens.AccessTokenSecret);
                }
                else
                {
                    // Otherwise, build and execute a regular request
                    webResponse = this.BuildRequestAndGetResponse(queryParameters);
                }

                responseData = ConversionUtility.ReadStream(webResponse.GetResponseStream());

                // Build the request status (holds details about the last request) and update the singleton
                requestStatus = RequestStatus.BuildRequestStatus(
                    responseData, 
                    webResponse.ResponseUri.AbsoluteUri, 
                    webResponse.StatusCode, 
                    webResponse.ContentType);
                RequestStatus.UpdateRequestStatus(requestStatus);

                // Parse the rate limiting HTTP Headers
                ParseRateLimitHeaders(resultObject, webResponse);
            }
            catch (WebException wex)
            {
                Trace.TraceError(wex.Message);

                // The exception response should always be an HttpWebResponse, but we check for good measure.
                HttpWebResponse exceptionResponse = wex.Response as HttpWebResponse;
                
                responseData = ConversionUtility.ReadStream(exceptionResponse.GetResponseStream());

                RequestStatus.UpdateRequestStatus(
                        responseData, 
                        exceptionResponse.ResponseUri.AbsoluteUri, 
                        exceptionResponse.StatusCode, 
                        exceptionResponse.ContentType);

                return default(T);
            }
            finally
            {
                // Set this back to the default so it doesn't affect other .net code.
                System.Net.ServicePointManager.Expect100Continue = true;
            }

            resultObject = SerializationHelper<T>.Deserialize(responseData, this.DeserializationHandler);

            this.AddResultToCache(cacheKeyBuilder, cache, resultObject);

            // Pass the current oauth tokens into the new object, so method calls from there will keep the authentication.
            if (resultObject != null)
            {
                resultObject.Tokens = this.Tokens;
                resultObject.RequestStatus = requestStatus;
            }

            Debug.WriteLine(string.Format(CultureInfo.CurrentCulture, "Finished {0}", this.Uri.AbsoluteUri), "Twitterizer2");

            return resultObject;
        }

        /// <summary>
        /// Clones this instance.
        /// </summary>
        /// <returns>A new instance of the <see cref="Twitterizer.Core.PagedCommand{T}"/> interface.</returns>
        internal virtual TwitterCommand<T> Clone()
        {
            return default(TwitterCommand<T>);
        }

        /// <summary>
        /// Sets the command URI.
        /// </summary>
        /// <param name="endPoint">The end point.</param>
        protected void SetCommandUri(string endPoint)
        {
            this.Uri = new Uri(string.Concat(this.OptionalProperties.APIBaseAddress, endPoint));
        }

        /// <summary>
        /// Parses the rate limit headers.
        /// </summary>
        /// <param name="resultObject">The result object.</param>
        /// <param name="webResponse">The web response.</param>
        private static void ParseRateLimitHeaders(T resultObject, WebResponse webResponse)
        {
            if (resultObject == null)
            {
                return;
            }

            resultObject.RateLimiting = new RateLimiting();

            if (!string.IsNullOrEmpty(webResponse.Headers.Get("X-RateLimit-Limit")))
            {
                resultObject.RateLimiting.Total = int.Parse(webResponse.Headers.Get("X-RateLimit-Limit"), CultureInfo.InvariantCulture);
            }

            if (!string.IsNullOrEmpty(webResponse.Headers.Get("X-RateLimit-Remaining")))
            {
                resultObject.RateLimiting.Remaining = int.Parse(webResponse.Headers.Get("X-RateLimit-Remaining"), CultureInfo.InvariantCulture);
            }

            if (!string.IsNullOrEmpty(webResponse.Headers["X-RateLimit-Reset"]))
            {
                resultObject.RateLimiting.ResetDate = (new DateTime(1970, 1, 1, 0, 0, 0, 0))
                    .AddSeconds(double.Parse(webResponse.Headers.Get("X-RateLimit-Reset"), CultureInfo.InvariantCulture));
            }
        }

        /// <summary>
        /// Adds the result to cache.
        /// </summary>
        /// <param name="cacheKeyBuilder">The cache key builder.</param>
        /// <param name="cache">The cache.</param>
        /// <param name="resultObject">The result object.</param>
        private void AddResultToCache(StringBuilder cacheKeyBuilder, Cache cache, T resultObject)
        {
            // If caching is enabled, add the result to the cache.
            if (this.OptionalProperties.CacheOutput)
            {
                cache.Add(
                    cacheKeyBuilder.ToString(),
                    resultObject,
                    null,
                    Cache.NoAbsoluteExpiration,
                    this.OptionalProperties.CacheTimespan,
                    CacheItemPriority.Normal,
                    null);

                Debug.WriteLine(string.Format(CultureInfo.CurrentCulture, "Added results to cache", this.Uri.AbsoluteUri), "Twitterizer2");
            }
        }
        
        /// <summary>
        /// Builds the request.
        /// </summary>
        /// <param name="queryParameters">The query parameters.</param>
        /// <returns>
        /// A <see cref="System.Net.HttpWebRequest"/> class.
        /// </returns>
        private HttpWebResponse BuildRequestAndGetResponse(Dictionary<string, string> queryParameters)
        {
            // Check if the SSL configuration flag is set and modify the address accordingly
            if (this.OptionalProperties.UseSSL)
            {
                this.Uri = new Uri(this.Uri.AbsoluteUri.Replace("http://", "https://"));
            }

            // Prepare and execute un-authorized query
            HttpWebRequest request;

            StringBuilder queryStringBuilder = new StringBuilder();
            foreach (KeyValuePair<string, string> item in queryParameters)
            {
                if (queryStringBuilder.Length > 0)
                    queryStringBuilder.Append("&");

                queryStringBuilder.AppendFormat("{0}={1}", item.Key, item.Value);
            }

            switch (this.Verb)
            {
                case HTTPVerb.GET:
                    string fullPathAndQuery = string.Format(CultureInfo.InvariantCulture, "{0}?{1}", this.Uri, queryStringBuilder);
#if DEBUG
                    System.Diagnostics.Debug.WriteLine(
                        string.Format(
                            CultureInfo.CurrentCulture,
                            "ANON GET: {0}",
                            fullPathAndQuery));
#endif
                    request = (HttpWebRequest)WebRequest.Create(fullPathAndQuery);
                    request.Method = "GET";
                    request.UserAgent = string.Format(
                        CultureInfo.InvariantCulture,
                        "Twitterizer/{0}",
                        Information.AssemblyVersion());
                    request.Proxy = this.OptionalProperties.Proxy;
                    break;
                case HTTPVerb.POST:
                    request = (HttpWebRequest)WebRequest.Create(this.Uri);
                    request.Method = "POST";
                    request.ContentType = "application/x-www-form-urlencoded";
                    request.UserAgent = string.Format(
                        CultureInfo.InvariantCulture,
                        "Twitterizer/{0}",
                        Information.AssemblyVersion());
                    request.Proxy = this.OptionalProperties.Proxy;

                    using (StreamWriter postDataWriter = new StreamWriter(request.GetRequestStream()))
                    {
                        postDataWriter.Write(queryStringBuilder.ToString());
                        postDataWriter.Close();
                    }

#if DEBUG
                    System.Diagnostics.Debug.WriteLine(
                        string.Format(
                            CultureInfo.CurrentCulture,
                            "ANON POST: {1}\n{0}",
                            this.Uri,
                            queryStringBuilder.ToString()));
#endif
                    break;
                case HTTPVerb.DELETE:
#if DEBUG
                    System.Diagnostics.Debug.WriteLine(
                        string.Format(
                            CultureInfo.CurrentCulture,
                            "ANON DELETE: {0}",
                            this.Uri.AbsoluteUri));
#endif
                    request = (HttpWebRequest)WebRequest.Create(this.Uri);
                    request.Method = "DELETE";
                    request.UserAgent = string.Format(
                        CultureInfo.InvariantCulture,
                        "Twitterizer/{0}",
                        Information.AssemblyVersion());
                    request.Proxy = this.OptionalProperties.Proxy;
                    break;
                default:
                    throw new NotImplementedException();
            }

            return (HttpWebResponse)request.GetResponse();
        }
    }
}
