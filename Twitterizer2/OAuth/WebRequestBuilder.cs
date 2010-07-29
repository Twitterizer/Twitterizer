//-----------------------------------------------------------------------
// <copyright file="WebRequestBuilder.cs" company="Patrick 'Ricky' Smith">
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
// <summary>Provides the means of preparing and executing Anonymous and OAuth signed web requests.</summary>
//-----------------------------------------------------------------------
namespace Twitterizer
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Net;
    using System.Security.Cryptography;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Web;

    /// <summary>
    /// Enumeration of the supported HTTP verbs supported by the <see cref="Twitterizer.Core.CommandPerformer{T}"/>
    /// </summary>
    public enum HTTPVerb
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
    /// The Web Request Builder class.
    /// </summary>
    public sealed class WebRequestBuilder
    {
        /// <summary>
        /// Gets or sets the request URI.
        /// </summary>
        /// <value>The request URI.</value>
        public Uri RequestUri { get; set; }

        /// <summary>
        /// Gets or sets the parameters.
        /// </summary>
        /// <value>The parameters.</value>
        public Dictionary<string, string> Parameters { get; private set; }

        /// <summary>
        /// Gets or sets the OAuth parameters.
        /// </summary>
        /// <value>The O auth parameters.</value>
        public Dictionary<string, string> OAuthParameters { get; private set; }

        /// <summary>
        /// Gets or sets the verb.
        /// </summary>
        /// <value>The verb.</value>
        public HTTPVerb Verb { get; set; }

        /// <summary>
        /// Gets or sets the oauth tokens.
        /// </summary>
        /// <value>The tokens.</value>
        public OAuthTokens Tokens { get; set; }

        /// <summary>
        /// Gets or sets the proxy.
        /// </summary>
        /// <value>The proxy.</value>
        public WebProxy Proxy { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the request will be signed with an OAuth authorization header.
        /// </summary>
        /// <value><c>true</c> if [use O auth]; otherwise, <c>false</c>.</value>
        public bool UseOAuth { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="WebRequestBuilder"/> class.
        /// </summary>
        /// <param name="requestUri">The request URI.</param>
        /// <param name="verb">The verb.</param>
        public WebRequestBuilder(Uri requestUri, HTTPVerb verb)
        {
            if (requestUri == null)
                throw new ArgumentNullException("requestUri");

            this.RequestUri = requestUri;
            this.Verb = verb;
            this.UseOAuth = false;

            this.Parameters = new Dictionary<string, string>();
            this.OAuthParameters = new Dictionary<string, string>();

            if (!string.IsNullOrEmpty(this.RequestUri.Query))
            {
                foreach (Match item in Regex.Matches(this.RequestUri.Query, @"(?<key>[^&?=]+)=(?<value>[^&?=]+)"))
                {
                    this.Parameters.Add(item.Groups["key"].Value, item.Groups["value"].Value);
                }

                this.RequestUri = new Uri(this.RequestUri.AbsoluteUri.Replace(this.RequestUri.Query, ""));
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="WebRequestBuilder"/> class.
        /// </summary>
        /// <param name="requestUri">The request URI.</param>
        /// <param name="verb">The verb.</param>
        /// <param name="tokens">The tokens.</param>
        public WebRequestBuilder(Uri requestUri, HTTPVerb verb, OAuthTokens tokens)
            : this(requestUri, verb)
        {
            this.Tokens = tokens;

            if (tokens != null)
            {
                if (string.IsNullOrEmpty(this.Tokens.ConsumerKey) || string.IsNullOrEmpty(this.Tokens.ConsumerSecret))
                {
                    throw new ArgumentException("Consumer key and secret are required for OAuth requests.");
                }

                if (string.IsNullOrEmpty(this.Tokens.AccessToken) ^ string.IsNullOrEmpty(this.Tokens.AccessTokenSecret))
                {
                    throw new ArgumentException("The access token is invalid. You must specify the key AND secret values.");
                }

                this.UseOAuth = true;
            }
        }

        /// <summary>
        /// Executes the request.
        /// </summary>
        /// <returns></returns>
        public HttpWebResponse ExecuteRequest()
        {
            HttpWebRequest request = PrepareRequest();

            return (HttpWebResponse)request.GetResponse();
        }

        /// <summary>
        /// Prepares the request. It is not nessisary to call this method unless additional configuration is required.
        /// </summary>
        /// <returns>A <see cref="HttpWebRequest"/> object fully configured and ready for execution.</returns>
        public HttpWebRequest PrepareRequest()
        {
            SetupOAuth();
            RebuildRequestUriWithParameters();

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(this.RequestUri);
            request.Method = this.Verb.ToString();
            request.UserAgent = string.Format(CultureInfo.InvariantCulture, "Twitterizer/{0}", System.Reflection.Assembly.GetAssembly(typeof(WebRequestBuilder)).GetName().Version.ToString());

            if (this.UseOAuth)
                request.Headers.Add("Authorization", GenerateAuthorizationHeader());

            if (this.Proxy != null)
                request.Proxy = Proxy;

            if (this.Verb == HTTPVerb.POST)
            {
                request.ContentType = "application/x-www-form-urlencoded";
            }

            return request;
        }

        /// <summary>
        /// Rebuilds the request URI with parameters.
        /// </summary>
        private void RebuildRequestUriWithParameters()
        {
            StringBuilder requestParametersBuilder = new StringBuilder(this.RequestUri.AbsoluteUri);
            requestParametersBuilder.Append(this.RequestUri.Query.Length == 0 ? "?" : "&");

            foreach (KeyValuePair<string, string> item in this.Parameters)
            {
                requestParametersBuilder.AppendFormat("{0}={1}&", item.Key, UrlEncode(item.Value));
            }

            if (requestParametersBuilder.Length > 0)
            {
                this.RequestUri = new Uri(requestParametersBuilder.ToString());
            }
        }
        
        #region OAuth Helper Methods
        /// <summary>
        /// Sets up the OAuth request details.
        /// </summary>
        public void SetupOAuth()
        {
            // We only sign oauth requests
            if (!this.UseOAuth)
            {
                return;
            }
            
            // Add the OAuth parameters
            this.OAuthParameters.Add("oauth_version", "1.0");
            this.OAuthParameters.Add("oauth_nonce", GenerateNonce());
            this.OAuthParameters.Add("oauth_timestamp", GenerateTimeStamp());
            this.OAuthParameters.Add("oauth_signature_method", "HMAC-SHA1");
            this.OAuthParameters.Add("oauth_consumer_key", this.Tokens.ConsumerKey);
            this.OAuthParameters.Add("oauth_consumer_secret", this.Tokens.ConsumerSecret);

            if (!string.IsNullOrEmpty(this.Tokens.AccessToken))
            {
                this.OAuthParameters.Add("oauth_token", this.Tokens.AccessToken);
            }

            if (!string.IsNullOrEmpty(this.Tokens.AccessTokenSecret))
            {
                this.OAuthParameters.Add("oauth_token_secret", this.Tokens.AccessTokenSecret);
            }

            var nonSecretParameters = from p in this.OAuthParameters
                                      where !(p.Key.EndsWith("_secret", StringComparison.OrdinalIgnoreCase) &&
                                            !p.Key.EndsWith("_verifier", StringComparison.OrdinalIgnoreCase))
                                      select p;

            // Create the base string. This is the string that will be hashed for the signature.
            string signatureBaseString = string.Format(
                CultureInfo.InvariantCulture,
                "{0}&{1}&{2}",
                this.Verb.ToString().ToUpper(CultureInfo.InvariantCulture),
                UrlEncode(NormalizeUrl(this.RequestUri)),
                UrlEncode(nonSecretParameters));

            // Create our hash key (you might say this is a password)
            string key = string.Format(
                    CultureInfo.InvariantCulture,
                    "{0}&{1}",
                    UrlEncode(this.Tokens.ConsumerSecret),
                    UrlEncode(this.Tokens.AccessTokenSecret));


            // Generate the hash
            HMACSHA1 hmacsha1 = new HMACSHA1(Encoding.ASCII.GetBytes(key));
            byte[] signatureBytes = hmacsha1.ComputeHash(Encoding.ASCII.GetBytes(signatureBaseString));

            // Add the signature to the oauth parameters
            this.OAuthParameters.Add("oauth_signature", Convert.ToBase64String(signatureBytes));
        }

        /// <summary>
        /// Generate the timestamp for the signature        
        /// </summary>
        /// <returns>A timestamp value in a string.</returns>
        public static string GenerateTimeStamp()
        {
            // Default implementation of UNIX time of the current UTC time
            TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return Convert.ToInt64(ts.TotalSeconds, CultureInfo.CurrentCulture).ToString(CultureInfo.CurrentCulture);
        }

        /// <summary>
        /// Generate a nonce
        /// </summary>
        /// <returns>A random number between 123400 and 9999999 in a string.</returns>
        public static string GenerateNonce()
        {
            // Just a simple implementation of a random number between 123400 and 9999999
            return new Random()
                .Next(123400, int.MaxValue)
                .ToString("X", CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// Normalizes the URL.
        /// </summary>
        /// <param name="url">The URL to normalize.</param>
        /// <returns>The normalized url string.</returns>
        public static string NormalizeUrl(Uri url)
        {
            string normalizedUrl = string.Format(CultureInfo.InvariantCulture, "{0}://{1}", url.Scheme, url.Host);
            if (!((url.Scheme == "http" && url.Port == 80) || (url.Scheme == "https" && url.Port == 443)))
            {
                normalizedUrl += ":" + url.Port;
            }

            normalizedUrl += url.AbsolutePath;
            return normalizedUrl;
        }

        /// <summary>
        /// Encodes a value for inclusion in a URL querystring.
        /// </summary>
        /// <param name="value">The value to Url encode</param>
        /// <returns>Returns a Url encoded string</returns>
        public static string UrlEncode(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return string.Empty;
            }

            value = HttpUtility.UrlEncode(value).Replace("+", "%20");

            // UrlEncode escapes with lowercase characters (e.g. %2f) but oAuth needs %2F
            value = Regex.Replace(value, "(%[0-9a-f][0-9a-f])", c => c.Value.ToUpper());

            // these characters are not escaped by UrlEncode() but needed to be escaped
            value = value
                .Replace("(", "%28")
                .Replace(")", "%29")
                .Replace("$", "%24")
                .Replace("!", "%21")
                .Replace("*", "%2A")
                .Replace("'", "%27");

            // these characters are escaped by UrlEncode() but will fail if unescaped!
            value = value.Replace("%7E", "~");

            return value;
        }

        /// <summary>
        /// Encodes a series of key/value pairs for inclusion in a URL querystring.
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>A string of all the <paramref name="parameters"/> keys and value pairs with the values encoded.</returns>
        private static string UrlEncode(IEnumerable<KeyValuePair<string, string>> parameters)
        {
            StringBuilder parameterString = new StringBuilder();

            var paramsSorted = from p in parameters
                               orderby p.Key, p.Value
                               select p;

            foreach (var item in paramsSorted)
            {
                if (parameterString.Length > 0)
                {
                    parameterString.Append("&");
                }

                parameterString.Append(
                    string.Format(
                        CultureInfo.InvariantCulture,
                        "{0}={1}",
                        UrlEncode(item.Key),
                        UrlEncode(item.Value)));
            }

            return UrlEncode(parameterString.ToString());
        }

        /// <summary>
        /// Generates the authorization header.
        /// </summary>
        /// <returns>The string value of the HTTP header to be included for OAuth requests.</returns>
        public string GenerateAuthorizationHeader()
        {
            StringBuilder authHeaderBuilder = new StringBuilder("OAuth realm=\"Twitter API\"");

            var sortedParameters = from p in this.OAuthParameters
                                   where p.Key.StartsWith("oauth_") &&
                                        !p.Key.EndsWith("_secret", StringComparison.OrdinalIgnoreCase) &&
                                         p.Key != "oauth_signature" &&
                                         p.Key != "oauth_verifier" &&
                                        !string.IsNullOrEmpty(p.Value)
                                   orderby p.Key, UrlEncode(p.Value)
                                   select p;

            foreach (var item in sortedParameters)
            {
                authHeaderBuilder.AppendFormat(
                    ",{0}=\"{1}\"",
                    UrlEncode(item.Key),
                    UrlEncode(item.Value));
            }

            authHeaderBuilder.AppendFormat(",oauth_signature=\"{0}\"", UrlEncode(this.OAuthParameters["oauth_signature"]));

            return authHeaderBuilder.ToString();
        }
        #endregion
    }
}
