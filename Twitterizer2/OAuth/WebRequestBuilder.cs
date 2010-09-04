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
        /// The HTTP Authorization realm.
        /// </summary>
        public string Realm = "Twitter API";

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
        /// OAuth Parameters key names to include in the Authorization header.
        /// </summary>
        private static readonly string[] OAuthParametersToIncludeInHeader = new[]
                                                          {
                                                              "oauth_version",
                                                              "oauth_nonce",
                                                              "oauth_timestamp",
                                                              "oauth_signature_method",
                                                              "oauth_consumer_key",
                                                              "oauth_token"
                                                              // Leave signature omitted from the list, it is added manually
                                                              // "oauth_signature",
                                                          };

        /// <summary>
        /// Parameters that may appear in the list, but should never be included in the header or the request.
        /// </summary>
        private static readonly string[] SecretParameters = new[]
                                                                {
                                                                    "oauth_consumer_secret",
                                                                    "oauth_token_secret",
                                                                    "oauth_signature"
                                                                };

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

            if (string.IsNullOrEmpty(this.RequestUri.Query)) return;

            foreach (Match item in Regex.Matches(this.RequestUri.Query, @"(?<key>[^&?=]+)=(?<value>[^&?=]+)"))
            {
                this.Parameters.Add(item.Groups["key"].Value, item.Groups["value"].Value);
            }

            this.RequestUri = new Uri(this.RequestUri.AbsoluteUri.Replace(this.RequestUri.Query, ""));
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
            AddQueryStringParametersToUri();

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(this.RequestUri);
            if (this.Proxy != null)
                request.Proxy = Proxy;
            request.Method = this.Verb.ToString();
            request.UserAgent = string.Format(CultureInfo.InvariantCulture, "Twitterizer/{0}", System.Reflection.Assembly.GetAssembly(typeof(WebRequestBuilder)).GetName().Version);
            request.ServicePoint.Expect100Continue = false;
            
            if (this.UseOAuth)
            {
                request.Headers.Add("Authorization", GenerateAuthorizationHeader());
            }

            //AddFormFieldValuesToRequest(request);
            
            return request;
        }

        /// <summary>
        /// Adds the parameters to request uri.
        /// </summary>
        private void AddQueryStringParametersToUri()
        {
            StringBuilder requestParametersBuilder = new StringBuilder(this.RequestUri.AbsoluteUri);
            requestParametersBuilder.Append(this.RequestUri.Query.Length == 0 ? "?" : "&");

            var fieldsToInclude = from p in this.Parameters
                                  where !OAuthParametersToIncludeInHeader.Contains(p.Key) &&
                                        !SecretParameters.Contains(p.Key)
                                  select p;

            foreach (KeyValuePair<string, string> item in fieldsToInclude)
            {
                requestParametersBuilder.AppendFormat("{0}={1}&", item.Key, UrlEncode(item.Value));
            }

            if (requestParametersBuilder.Length == 0)
                return;

            requestParametersBuilder.Remove(requestParametersBuilder.Length - 1, 1);

            this.RequestUri = new Uri(requestParametersBuilder.ToString());
        }

        /// <summary>
        /// Adds the form field values to request.
        /// </summary>
        /// <param name="request">The request.</param>
        private void AddFormFieldValuesToRequest(WebRequest request)
        {
            throw new NotImplementedException("Multipart form data is not yet supported by the WebRequestBuilder.");

/*
            if (!(new[] { HTTPVerb.DELETE, HTTPVerb.POST }).Contains(this.Verb))
                return;

            request.ContentType = "application/x-www-form-urlencoded";

            StringBuilder requestParametersBuilder = new StringBuilder();

            var fieldsToInclude = from p in this.Parameters
                                  where !OAuthParametersToIncludeInHeader.Contains(p.Key) &&
                                        !SecretParameters.Contains(p.Key)
                                  select p;

            foreach (KeyValuePair<string, string> item in fieldsToInclude)
            {
                requestParametersBuilder.AppendFormat("{0}={1}&", item.Key, item.Value);
            }

            if (requestParametersBuilder.Length == 0)
                return;

            requestParametersBuilder.Remove(requestParametersBuilder.Length - 1, 1);

            byte[] formData = Encoding.UTF8.GetBytes(requestParametersBuilder.ToString());
            request.ContentLength = formData.Length;
            System.IO.Stream requestStream = request.GetRequestStream();
            requestStream.Write(formData, 0, formData.Length);
            requestStream.Close();
*/
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
            this.Parameters.Add("oauth_version", "1.0");
            this.Parameters.Add("oauth_nonce", GenerateNonce());
            this.Parameters.Add("oauth_timestamp", GenerateTimeStamp());
            this.Parameters.Add("oauth_signature_method", "HMAC-SHA1");
            this.Parameters.Add("oauth_consumer_key", this.Tokens.ConsumerKey);
            this.Parameters.Add("oauth_consumer_secret", this.Tokens.ConsumerSecret);

            if (!string.IsNullOrEmpty(this.Tokens.AccessToken))
            {
                this.Parameters.Add("oauth_token", this.Tokens.AccessToken);
            }

            if (!string.IsNullOrEmpty(this.Tokens.AccessTokenSecret))
            {
                this.Parameters.Add("oauth_token_secret", this.Tokens.AccessTokenSecret);
            }

            string signature = GenerateSignature();

            // Add the signature to the oauth parameters
            this.Parameters.Add("oauth_signature", signature);
        }

        /// <summary>
        /// Generates the signature.
        /// </summary>
        /// <returns></returns>
        public string GenerateSignature()
        {
            var nonSecretParameters = (from p in this.Parameters
                                       where !SecretParameters.Contains(p.Key)
                                       select p);

            var baseStringParameters =
                (from p in this.Parameters
                 where !(p.Key.EndsWith("_secret", StringComparison.OrdinalIgnoreCase) &&
                         p.Key.StartsWith("oauth_", StringComparison.OrdinalIgnoreCase) &&
                         !p.Key.EndsWith("_verifier", StringComparison.OrdinalIgnoreCase))
                 select p);

            Uri urlForSigning = this.RequestUri;

            // Create the base string. This is the string that will be hashed for the signature.
            string signatureBaseString = string.Format(
                CultureInfo.InvariantCulture,
                "{0}&{1}&{2}",
                this.Verb.ToString().ToUpper(CultureInfo.InvariantCulture),
                UrlEncode(NormalizeUrl(urlForSigning)),
                UrlEncode(baseStringParameters));

            // Create our hash key (you might say this is a password)
            string key = string.Format(
                CultureInfo.InvariantCulture,
                "{0}&{1}",
                UrlEncode(this.Tokens.ConsumerSecret),
                UrlEncode(this.Tokens.AccessTokenSecret));


            // Generate the hash
            HMACSHA1 hmacsha1 = new HMACSHA1(Encoding.ASCII.GetBytes(key));
            byte[] signatureBytes = hmacsha1.ComputeHash(Encoding.ASCII.GetBytes(signatureBaseString));
            return Convert.ToBase64String(signatureBytes);
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

            value = Uri.EscapeDataString(value);

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
            StringBuilder authHeaderBuilder = new StringBuilder();
            authHeaderBuilder.AppendFormat("OAuth realm=\"{0}\"", Realm);

            var sortedParameters = from p in this.Parameters
                                   where OAuthParametersToIncludeInHeader.Contains(p.Key)
                                   orderby p.Key, UrlEncode(p.Value)
                                   select p;

            foreach (var item in sortedParameters)
            {
                authHeaderBuilder.AppendFormat(
                    ",{0}=\"{1}\"",
                    UrlEncode(item.Key),
                    UrlEncode(item.Value));
            }

            authHeaderBuilder.AppendFormat(",oauth_signature=\"{0}\"", UrlEncode(this.Parameters["oauth_signature"]));

            return authHeaderBuilder.ToString();
        }
        #endregion


    }
}
