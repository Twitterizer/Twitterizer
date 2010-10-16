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
    using System.Net;
    using System.Linq;
    using System.Text;
#if !SILVERLIGHT
    using System.Web;
#endif
#if !LITE && !SILVERLIGHT
    using System.Web.Caching;
#endif
    using Twitterizer;

    /// <summary>
    /// The base command class.
    /// </summary>
    /// <typeparam name="T">The business object the command should return.</typeparam>
#if !SILVERLIGHT
    [Serializable]
#endif
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
            this.OptionalProperties = optionalProperties ?? new OptionalProperties();

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
        /// Gets or sets the image to upload.
        /// </summary>
        /// <value>The image to upload.</value>
        public TwitterImage ImageToUpload { get; set; }

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
        public TwitterResponse<T> ExecuteCommand()
        {
            TwitterResponse<T> twitterResponse = new TwitterResponse<T>();

            if (this.OptionalProperties.UseSSL)
            {
                this.Uri = new Uri(this.Uri.AbsoluteUri.Replace("http://", "https://"));
            }

            Debug.WriteLine(string.Format(CultureInfo.CurrentCulture, "Begin {0}", this.Uri.AbsoluteUri), "Twitterizer2");

            // Loop through all of the custom attributes assigned to the command class
            foreach (Attribute attribute in this.GetType().GetCustomAttributes(false))
            {
                if (attribute.GetType() == typeof(AuthorizedCommandAttribute))
                {
                    if (this.Tokens == null)
                    {
                        throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, "Tokens are required for the \"{0}\" command.", this.GetType()));
                    }

                    if (string.IsNullOrEmpty(this.Tokens.ConsumerKey) ||
                        string.IsNullOrEmpty(this.Tokens.ConsumerSecret) ||
                        string.IsNullOrEmpty(this.Tokens.AccessToken) ||
                        string.IsNullOrEmpty(this.Tokens.AccessTokenSecret))
                    {
                        throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, "Token values cannot be null when executing the \"{0}\" command.", this.GetType()));
                    }
                }
                else if (attribute.GetType() == typeof(RateLimitedAttribute))
                {
                    // Get the rate limiting status
                    if (TwitterRateLimitStatus.GetStatus(this.Tokens).ResponseObject.RemainingHits == 0)
                    {
                        throw new TwitterizerException("You are being rate limited.");
                    }
                }

            }

#if !LITE && !SILVERLIGHT
            // Variables and objects needed for caching
            StringBuilder cacheKeyBuilder = new StringBuilder(this.Uri.AbsoluteUri);
            if (this.Tokens != null)
            {
                cacheKeyBuilder.AppendFormat("|{0}|{1}", this.Tokens.ConsumerKey, this.Tokens.ConsumerKey);
            }

            Cache cache = HttpRuntime.Cache;
#endif

            // Prepare the query parameters
            Dictionary<string, string> queryParameters = new Dictionary<string, string>();
            foreach (KeyValuePair<string, string> item in this.RequestParameters)
            {
                queryParameters.Add(item.Key, item.Value);
#if !LITE && !SILVERLIGHT
                cacheKeyBuilder.AppendFormat("|{0}={1}", item.Key, item.Value);
#endif
            }

#if !LITE && !SILVERLIGHT
            // Lookup the cached item and return it
            if (this.Verb == HTTPVerb.GET && this.OptionalProperties.CacheOutput && cache[cacheKeyBuilder.ToString()] != null)
            {
                if (cache[cacheKeyBuilder.ToString()] is T)
                {
                    Debug.WriteLine("Found in cache", "Twitterizer2");
                    Debug.WriteLine(string.Format(CultureInfo.CurrentCulture, "End {0}", this.Uri.AbsoluteUri),
                                    "Twitterizer2");

                    return new TwitterResponse<T>()
                               {
                                   ResponseObject = (T)cache[cacheKeyBuilder.ToString()],
                                   ResponseCached = true
                               };
                }
            }
            
#endif
            // Declare the variable to be returned
            twitterResponse.ResponseObject = default(T);
            twitterResponse.RequestUrl = this.Uri.AbsoluteUri;
            RateLimiting rateLimiting;

            byte[] responseData;

            try
            {
                WebRequestBuilder requestBuilder = new WebRequestBuilder(this.Uri, this.Verb, this.Tokens);

#if !SILVERLIGHT
                if (this.OptionalProperties != null)
                    requestBuilder.Proxy = this.OptionalProperties.Proxy;
#endif

                foreach (var item in queryParameters)
                {
                    requestBuilder.Parameters.Add(item.Key, item.Value);
                }

                HttpWebResponse response = requestBuilder.ExecuteRequest();

                responseData = ConversionUtility.ReadStream(response.GetResponseStream());
                twitterResponse.Content = Encoding.UTF8.GetString(responseData, 0, responseData.Length);

                twitterResponse.RequestUrl = requestBuilder.RequestUri.AbsoluteUri;

                // Parse the rate limiting HTTP Headers
                rateLimiting = ParseRateLimitHeaders(response.Headers);

                // Lookup the status code and set the status accordingly
                SetStatusCode(twitterResponse, response.StatusCode, rateLimiting);

                twitterResponse.RateLimiting = rateLimiting;
            }
            catch (WebException wex)
            {
                if (new[]
                        {
#if !SILVERLIGHT
                            WebExceptionStatus.Timeout, 
                            WebExceptionStatus.ConnectionClosed,
#endif
                            WebExceptionStatus.ConnectFailure
                        }.Contains(wex.Status))
                {
                    twitterResponse.Result = RequestResult.ConnectionFailure;
                }

                // The exception response should always be an HttpWebResponse, but we check for good measure.
                HttpWebResponse exceptionResponse = wex.Response as HttpWebResponse;

                if (exceptionResponse == null)
                {
                    throw;
                }

                responseData = ConversionUtility.ReadStream(exceptionResponse.GetResponseStream());
                twitterResponse.Content = Encoding.UTF8.GetString(responseData, 0, responseData.Length);

                rateLimiting = ParseRateLimitHeaders(exceptionResponse.Headers);


                // Lookup the status code and set the status accordingly
                SetStatusCode(twitterResponse, exceptionResponse.StatusCode, rateLimiting);

                twitterResponse.RateLimiting = rateLimiting;

                if (wex.Status == WebExceptionStatus.UnknownError)
                    throw;



                return twitterResponse;
            }

            twitterResponse.ResponseObject = SerializationHelper<T>.Deserialize(responseData, this.DeserializationHandler);


#if !LITE && !SILVERLIGHT
            this.AddResultToCache(cacheKeyBuilder, cache, twitterResponse.ResponseObject);
#endif

            // Pass the current oauth tokens into the new object, so method calls from there will keep the authentication.
            twitterResponse.Tokens = this.Tokens;

            Debug.WriteLine(string.Format(CultureInfo.CurrentCulture, "Finished {0}", this.Uri.AbsoluteUri), "Twitterizer2");

            return twitterResponse;
        }

        /// <summary>
        /// Sets the status code.
        /// </summary>
        /// <param name="twitterResponse">The twitter response.</param>
        /// <param name="statusCode">The status code.</param>
        /// <param name="rateLimiting">The rate limiting.</param>
        private static void SetStatusCode(TwitterResponse<T> twitterResponse, HttpStatusCode statusCode, RateLimiting rateLimiting)
        {
            switch (statusCode)
            {
                case HttpStatusCode.OK:
                    twitterResponse.Result = RequestResult.Success;
                    break;

                case HttpStatusCode.BadRequest:
                    twitterResponse.Result = rateLimiting.Remaining == 0 ? RequestResult.RateLimited : RequestResult.BadRequest;
                    break;

                case HttpStatusCode.Unauthorized:
                    twitterResponse.Result = RequestResult.Unauthorized;
                    break;

                case HttpStatusCode.NotFound:
                    twitterResponse.Result = RequestResult.FileNotFound;
                    break;

                default:
                    twitterResponse.Result = RequestResult.Unknown;
                    break;
            }
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
            if (endPoint.StartsWith("/"))
                throw new ArgumentException("The API endpoint cannot begin with a forward slash. This will result in 404 errors and headaches.", "endPoint");

            this.Uri = new Uri(string.Concat(this.OptionalProperties.APIBaseAddress, endPoint));
        }

        /// <summary>
        /// Parses the rate limit headers.
        /// </summary>
        /// <param name="responseHeaders">The headers of the web response.</param>
        /// <returns>An object that contains the rate-limiting info contained in the response headers</returns>
        private static RateLimiting ParseRateLimitHeaders(WebHeaderCollection responseHeaders)
        {
            RateLimiting rateLimiting = new RateLimiting();

            if (!responseHeaders.AllKeys.Contains("X-RateLimit-Limit"))
            {
                rateLimiting.Total = int.Parse(responseHeaders["X-RateLimit-Limit"], CultureInfo.InvariantCulture);
            }

            if (!responseHeaders.AllKeys.Contains("X-RateLimit-Remaining"))
            {
                rateLimiting.Remaining = int.Parse(responseHeaders["X-RateLimit-Remaining"], CultureInfo.InvariantCulture);
            }

            if (!string.IsNullOrEmpty(responseHeaders["X-RateLimit-Reset"]))
            {
                rateLimiting.ResetDate = DateTime.SpecifyKind(new DateTime(1970, 1, 1, 0, 0, 0, 0)
                    .AddSeconds(double.Parse(responseHeaders["X-RateLimit-Reset"], CultureInfo.InvariantCulture)), DateTimeKind.Local);
            }
            return rateLimiting;
        }

#if !LITE && !SILVERLIGHT
        /// <summary>
        /// Adds the result to cache.
        /// </summary>
        /// <param name="cacheKeyBuilder">The cache key builder.</param>
        /// <param name="cache">The cache.</param>
        /// <param name="resultObject">The result object.</param>
        private void AddResultToCache(StringBuilder cacheKeyBuilder, Cache cache, T resultObject)
        {
            // If caching is enabled, add the result to the cache.
            if (this.Verb == HTTPVerb.GET && this.OptionalProperties.CacheOutput)
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
#endif
        }
}
