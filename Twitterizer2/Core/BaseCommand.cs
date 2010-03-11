//-----------------------------------------------------------------------
// <copyright file="BaseCommand.cs" company="Patrick Ricky Smith">
//  This file is part of the Twitterizer library (http://code.google.com/p/twitterizer/)
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
    using System.Globalization;
    using System.IO;
    using System.Net;
    using System.Runtime.Serialization.Json;
    using System.Text;
    using Twitterizer;

    /// <summary>
    /// The base command class.
    /// </summary>
    /// <typeparam name="T">The business object the command should return.</typeparam>
    internal abstract class BaseCommand<T> : ICommand<T>
        where T : ITwitterObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BaseCommand&lt;T&gt;"/> class.
        /// </summary>
        /// <param name="method">The method.</param>
        /// <param name="uri">The URI for the API method.</param>
        /// <param name="tokens">The request tokens.</param>
        protected BaseCommand(string method, Uri uri, OAuthTokens tokens)
        {
            if (string.IsNullOrEmpty(method))
            {
                throw new ArgumentNullException("method");
            }

            if (uri == null)
            {
                throw new ArgumentNullException("uri");
            }

            this.RequestParameters = new Dictionary<string, string>();
            this.Uri = uri;
            this.HttpMethod = method;
            this.Tokens = tokens;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseCommand&lt;T&gt;"/> class.
        /// </summary>
        /// <param name="method">The method.</param>
        /// <param name="tokens">The tokens.</param>
        protected BaseCommand(string method, OAuthTokens tokens)
        {
            this.RequestParameters = new Dictionary<string, string>();
            this.HttpMethod = method;
            this.Tokens = tokens;
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is valid.
        /// </summary>
        /// <value><c>true</c> if this instance is valid; otherwise, <c>false</c>.</value>
        public bool IsValid { get; set; }

        /// <summary>
        /// Gets or sets the API method URI.
        /// </summary>
        /// <value>The URI for the API method.</value>
        public Uri Uri { get; set; }

        /// <summary>
        /// Gets or sets the method.
        /// </summary>
        /// <value>The method.</value>
        public string HttpMethod { get; set; }

        /// <summary>
        /// Gets the request parameters.
        /// </summary>
        /// <value>The request parameters.</value>
        public Dictionary<string, string> RequestParameters { get; private set; }

        /// <summary>
        /// Gets the request tokens.
        /// </summary>
        /// <value>The request tokens.</value>
        internal OAuthTokens Tokens { get; private set; }

        #region ICommand<T> Members

        /// <summary>
        /// Initializes the command.
        /// </summary>
        public abstract void Init();

        /// <summary>
        /// Validates this instance.
        /// </summary>
        public abstract void Validate();

        #endregion

        /// <summary>
        /// Executes the command.
        /// </summary>
        /// <returns>The results of the command.</returns>
        /// <see cref="Twitterizer.Core.BaseObject"/>
        public T ExecuteCommand()
        {
            // Check if the command is flagged to check for rate limiting.
            if (this.GetType().GetCustomAttributes(typeof(RateLimitedAttribute), false).Length > 0)
            {
                // Get the rate limiting status
                if (TwitterRateLimitStatus.GetStatus(this.Tokens).RemainingHits == 0)
                {
                    throw new TwitterizerException("You are being rate limited.");
                }
            }

            if (!this.IsValid)
            {
                throw new CommandValidationException<T>()
                {
                    Command = this
                };
            }

            WebPermission permission = new WebPermission(
                NetworkAccess.Connect,
                new System.Text.RegularExpressions.Regex(@"https?://(www\.|api\.)?twitter\.com/.*"));
            permission.Demand();

            // Prepare the query parameters
            Dictionary<string, string> queryParameters = new Dictionary<string, string>();
            foreach (KeyValuePair<string, string> item in this.RequestParameters)
            {
                queryParameters.Add(item.Key, item.Value);
            }

            // Declare the variable to be returned
            T resultObject = default(T);

            try
            {
                // This must be set for all twitter request.
                System.Net.ServicePointManager.Expect100Continue = false;

                WebResponse webResponse;

                // If we have OAuth tokens, then build and execute an OAuth request.
                if (this.Tokens != null)
                {
                    webResponse = OAuthUtility.BuildOAuthRequestAndGetResponse(
                        this.Uri.AbsoluteUri,
                        queryParameters,
                        this.HttpMethod,
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

                // Set this back to the default so it doesn't affect other .net code.
                System.Net.ServicePointManager.Expect100Continue = true;

                // Get the response
                using (Stream responseStream = webResponse.GetResponseStream())
                {
#if DEBUG
                    byte[] data = ReadStream(responseStream);

                    System.Diagnostics.Debug.WriteLine("----------- RESPONSE -----------");
                    System.Diagnostics.Debug.Write(Encoding.UTF8.GetString(data));
                    System.Diagnostics.Debug.WriteLine("----------- END -----------");

                    // Deserialize the results.
                    DataContractJsonSerializer ds = new DataContractJsonSerializer(typeof(T));
                    resultObject = (T)ds.ReadObject(new MemoryStream(data));
                    responseStream.Close();
#else
                    // Deserialize the results.
                    DataContractJsonSerializer ds = new DataContractJsonSerializer(typeof(T));
                    resultObject = (T)ds.ReadObject(responseStream);
                    responseStream.Close();
#endif
                }

                // Parse the rate limiting HTTP Headers
                ParseRateLimitHeaders(resultObject, webResponse);
            }
            catch (WebException wex)
            {
                // The exception response should always be an HttpWebResponse, but we check for good measure.
                HttpWebResponse response = wex.Response as HttpWebResponse;
                if (response == null)
                {
                    throw;
                }

                // Determine what the problem was based on the HTTP Status Code
                switch (response.StatusCode)
                {
                    case HttpStatusCode.BadRequest:
                        throw new TwitterizerException("The request was invalid. It is possible you are being rate limited.", wex);
                    case HttpStatusCode.Unauthorized:
                        throw new AuthenticationFailedException(wex);
                }

                // We don't know what the issue is, throw a generic exception
                throw new TwitterizerException(wex.Message, wex);
            }

            // Pass the current oauth tokens into the new object, so method calls from there will keep the authentication.
            resultObject.Tokens = this.Tokens;

            return resultObject;
        }

        /// <summary>
        /// Parses the rate limit headers.
        /// </summary>
        /// <param name="resultObject">The result object.</param>
        /// <param name="webResponse">The web response.</param>
        private static void ParseRateLimitHeaders(T resultObject, WebResponse webResponse)
        {
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

#if DEBUG
        /// <summary>
        /// Reads the stream into a byte array.
        /// </summary>
        /// <param name="responseStream">The response stream.</param>
        /// <returns>A byte array.</returns>
        private static byte[] ReadStream(Stream responseStream)
        {
            byte[] data = new byte[32768];

            byte[] buffer = new byte[32768];
            using (MemoryStream ms = new MemoryStream())
            {
                bool exit = false;
                while (!exit)
                {
                    int read = responseStream.Read(buffer, 0, buffer.Length);
                    if (read <= 0)
                    {
                        data = ms.ToArray();
                        exit = true;
                    }
                    else
                    {
                        ms.Write(buffer, 0, read);
                    }
                }
            }

            return data;
        }
#endif

        /// <summary>
        /// Builds the request.
        /// </summary>
        /// <param name="queryParameters">The query parameters.</param>
        /// <returns>
        /// A <see cref="System.Net.HttpWebRequest"/> class.
        /// </returns>
        private HttpWebResponse BuildRequestAndGetResponse(Dictionary<string, string> queryParameters)
        {
            // Prepare and execute un-authorized query
            HttpWebRequest request;

            StringBuilder queryStringBuilder = new StringBuilder();
            foreach (KeyValuePair<string, string> item in queryParameters)
            {
                if (queryStringBuilder.Length > 0)
                    queryStringBuilder.Append("&");

                queryStringBuilder.AppendFormat("{0}={1}", item.Key, item.Value);
            }

            switch (this.HttpMethod.ToUpper(CultureInfo.InvariantCulture))
            {
                case "GET":
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
                    break;
                case "POST":
                    request = (HttpWebRequest)WebRequest.Create(this.Uri);
                    request.Method = "POST";
                    request.ContentType = "application/x-www-form-urlencoded";
                    request.UserAgent = string.Format(
                        CultureInfo.InvariantCulture,
                        "Twitterizer/{0}",
                        Information.AssemblyVersion());

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
                case "DELETE":
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
                    break;
                default:
                    throw new NotImplementedException();
            }

            return (HttpWebResponse)request.GetResponse();
        }
    }
}
