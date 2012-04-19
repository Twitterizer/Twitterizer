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
    using System.Globalization;
    using System.Net;
    using System.Linq;
    using System.Text;
    using Twitterizer;
    using System.Reflection;
    using System.Net.Http;
    using System.Threading.Tasks;
    using System.Net.Http.Headers;
    using Twitterizer.Models;

    /// <summary>
    /// The base command class.
    /// </summary>
    /// <typeparam name="T">The business object the command should return.</typeparam>
    internal abstract class TwitterCommand<T> : ICommand<T>
        where T : ITwitterObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TwitterCommand"/> class.
        /// </summary>
        /// <param name="method">The method.</param>
        /// <param name="endPoint">The end point.</param>
        /// <param name="tokens">The tokens.</param>
        /// <param name="optionalProperties">The optional properties.</param>
        protected TwitterCommand(HttpMethod method, string endPoint, OAuthTokens tokens, OptionalProperties optionalProperties)
        {
            this.RequestParameters = new Dictionary<string, object>();
            this.Verb = method;
            this.Tokens = tokens;
            this.OptionalProperties = optionalProperties ?? new OptionalProperties();

            this.SetCommandUri(endPoint);
        }

        /// <summary>
        /// Gets or sets the optional properties.
        /// </summary>
        /// <value>The optional properties.</value>
        protected OptionalProperties OptionalProperties { get; set; }

        /// <summary>
        /// Gets or sets the API method URI.
        /// </summary>
        /// <value>The URI for the API method.</value>
        private Uri Uri { get; set; }

        /// <summary>
        /// Gets or sets the method.
        /// </summary>
        /// <value>The method.</value>
        private HttpMethod Verb { get; set; }

        /// <summary>
        /// Gets or sets the request parameters.
        /// </summary>
        /// <value>The request parameters.</value>
        public Dictionary<string, object> RequestParameters { get; set; }

        /// <summary>
        /// Gets or sets the serialization delegate.
        /// </summary>
        /// <value>The serialization delegate.</value>
        protected SerializationHelper<T>.DeserializationHandler DeserializationHandler { get; set; }

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
        /// Gets or sets a value indicating whether this <see cref="TwitterCommand&lt;T&gt;"/> is multipart.
        /// </summary>
        /// <value><c>true</c> if multipart; otherwise, <c>false</c>.</value>
        protected bool Multipart { get; set; }

        /// <summary>
        /// Executes the command.
        /// </summary>
        /// <returns>The results of the command.</returns>
        public async Task<TwitterResponse<T>> ExecuteCommand()
        {
            TwitterResponse<T> twitterResponse = new TwitterResponse<T>();

            if (this.OptionalProperties.UseSSL)
            {
                this.Uri = new Uri(this.Uri.AbsoluteUri.Replace("http://", "https://"));
            }

            // Loop through all of the custom attributes assigned to the command class
            foreach (CustomAttributeData attribute in System.Reflection.IntrospectionExtensions.GetTypeInfo(this.GetType()).CustomAttributes)
            {
                if (attribute.AttributeType == typeof(AuthorizedCommandAttribute))
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
                else if (attribute.AttributeType == typeof(RateLimitedAttribute))
                {
                    //Get the rate limiting status
                    if ((await Account.RateLimitStatusAsync(this.Tokens)).ResponseObject.RemainingHits == 0)
                    {
                        throw new TwitterizerException("You are being rate limited.");
                    }
                }

            }

            // Prepare the query parameters
            Dictionary<string, object> queryParameters = new Dictionary<string, object>();
            foreach (KeyValuePair<string, object> item in this.RequestParameters)
            {
                queryParameters.Add(item.Key, item.Value);
            }

            // Declare the variable to be returned
            twitterResponse.ResponseObject = default(T);
            twitterResponse.RequestUrl = this.Uri.AbsoluteUri;
            RateLimiting rateLimiting = null;
            AccessLevel accessLevel;
            byte[] responseData = null;
            HttpResponseMessage response = null;

            try
            {
				WebRequestBuilder requestBuilder = new WebRequestBuilder(this.Uri, this.Verb, this.Tokens) { Multipart = this.Multipart };

                foreach (var item in queryParameters)
                {
                    requestBuilder.Parameters.Add(item.Key, item.Value);
                }

                response = await requestBuilder.ExecuteRequestAsync();

                if (response == null)
                {
                    twitterResponse.Result = RequestResult.Unknown;
                    return twitterResponse;
                }

                responseData = ConversionUtility.ReadStream(await response.Content.ReadAsStreamAsync());
                twitterResponse.Content = Encoding.UTF8.GetString(responseData, 0, responseData.Length);

                twitterResponse.RequestUrl = requestBuilder.RequestUri.AbsoluteUri;

                // Parse the rate limiting HTTP Headers
                rateLimiting = ParseRateLimitHeaders(response.Headers);

                // Parse Access Level
                accessLevel = ParseAccessLevel(response.Headers);

                // Lookup the status code and set the status accordingly
                SetStatusCode(twitterResponse, response.StatusCode, rateLimiting);

                twitterResponse.RateLimiting = rateLimiting;
                twitterResponse.AccessLevel = accessLevel;

                response.EnsureSuccessStatusCode();
            }
            catch (HttpRequestException wex)
            {
                // Lookup the status code and set the status accordingly
                SetStatusCode(twitterResponse, response.StatusCode, rateLimiting);

                twitterResponse.ErrorMessage = wex.Message;

                // Try to read the error message, if there is one.
                try
                {
                    TwitterErrorDetails errorDetails = SerializationHelper<TwitterErrorDetails>.Deserialize(responseData);
                    twitterResponse.ErrorMessage = errorDetails.ErrorMessage;
                }
                catch (Exception)
                {
                    // Occasionally, Twitter responds with XML error data even though we asked for json.
                    // This is that scenario. We will deal with it by doing nothing. It's up to the developer to deal with it.
                }

                return twitterResponse;
            }

            try
            {
                twitterResponse.ResponseObject = SerializationHelper<T>.Deserialize(responseData, this.DeserializationHandler);
            }
            catch (Newtonsoft.Json.JsonReaderException)
            {
                twitterResponse.ErrorMessage = "Unable to parse JSON";
                twitterResponse.Result = RequestResult.Unknown;
                return twitterResponse;
            }
            catch (Newtonsoft.Json.JsonSerializationException)
            {
                twitterResponse.ErrorMessage = "Unable to parse JSON";
                twitterResponse.Result = RequestResult.Unknown;
                return twitterResponse;
            }

            // Pass the current oauth tokens into the new object, so method calls from there will keep the authentication.
            twitterResponse.Tokens = this.Tokens;

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
                    twitterResponse.Result = (rateLimiting != null && rateLimiting.Remaining == 0) ? RequestResult.RateLimited : RequestResult.BadRequest;
                    break;

                case HttpStatusCode.Unauthorized:
                    twitterResponse.Result = RequestResult.Unauthorized;
                    break;

                case HttpStatusCode.NotFound:
                    twitterResponse.Result = RequestResult.FileNotFound;
                    break;

                case HttpStatusCode.ProxyAuthenticationRequired:
                    twitterResponse.Result = RequestResult.ProxyAuthenticationRequired;
                    break;

                case HttpStatusCode.RequestTimeout:
                    twitterResponse.Result = RequestResult.TwitterIsOverloaded;
                    break;

                case HttpStatusCode.Forbidden:
                    twitterResponse.Result = RequestResult.Unauthorized;
                    break;

                default:
                    twitterResponse.Result = RequestResult.Unknown;
                    break;
            }
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
        private static RateLimiting ParseRateLimitHeaders(HttpResponseHeaders responseHeaders)
        {
            RateLimiting rateLimiting = new RateLimiting();

            if (responseHeaders.Contains("X-RateLimit-Limit"))
            {
                rateLimiting.Total = int.Parse(responseHeaders.GetValues("X-RateLimit-Limit").First(), CultureInfo.InvariantCulture);
            }

            if (responseHeaders.Contains("X-RateLimit-Remaining"))
            {
                rateLimiting.Remaining = int.Parse(responseHeaders.GetValues("X-RateLimit-Remaining").First(), CultureInfo.InvariantCulture);
            }

            if (responseHeaders.Contains("X-RateLimit-Reset"))
            {
                rateLimiting.ResetDate = DateTime.SpecifyKind(new DateTime(1970, 1, 1, 0, 0, 0, 0)
                    .AddSeconds(double.Parse(responseHeaders.GetValues("X-RateLimit-Reset").First(), CultureInfo.InvariantCulture)), DateTimeKind.Utc);
            }
            return rateLimiting;
        }

        /// <summary>
        /// Parses the access level headers.
        /// </summary>
        /// <param name="responseHeaders">The headers of the web response.</param>
        /// <returns>An enum of the current access level of the OAuth Token being used.</returns>
        private AccessLevel ParseAccessLevel(HttpResponseHeaders responseHeaders)
        {
            if (responseHeaders.Contains("X-Access-Level"))
            {
                switch (responseHeaders.GetValues("X-Access-Level").First().ToLower())
                {
                    case "read":
                        return AccessLevel.Read;
                    case "read-write":
                        return AccessLevel.ReadWrite;
                    case "read-write-privatemessages":
                    case "read-write-directmessages":
                        return AccessLevel.ReadWriteDirectMessage;
                }
                return AccessLevel.Unknown;
            }
            return AccessLevel.Unavailable;
        }
    }
}
