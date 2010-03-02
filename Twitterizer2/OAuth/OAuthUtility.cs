﻿//-----------------------------------------------------------------------
// <copyright file="OAuthUtility.cs" company="Patrick 'Ricky' Smith">
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
// <summary>Provides simple methods to simplify OAuth interaction.</summary>
//-----------------------------------------------------------------------

namespace Twitterizer
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Security.Cryptography;
    using System.Text;
    using System.Text.RegularExpressions;

    /// <summary>
    /// A utility for handling the oauth protocol.
    /// </summary>
    public class OAuthUtility
    {
        /// <summary>
        /// The name of the signature type twiter uses.
        /// </summary>
        private const string SignatureType = "HMAC-SHA1";

        #region Public Methods
        /// <summary>
        /// Gets a new OAuth request token from the twitter api.
        /// </summary>
        /// <param name="consumerKey">The consumer key.</param>
        /// <param name="consumerSecret">The consumer secret.</param>
        /// <returns>A new <see cref="Twitterizer.OAuthUtility.TokenResponse"/> instance.</returns>
        public static TokenResponse GetRequestToken(string consumerKey, string consumerSecret)
        {
            return GetRequestToken(consumerKey, consumerSecret, string.Empty);
        }

        /// <summary>
        /// Gets a new OAuth request token from the twitter api.
        /// </summary>
        /// <param name="consumerKey">The consumer key.</param>
        /// <param name="consumerSecret">The consumer secret.</param>
        /// <param name="callBackUrl">The call back URL.</param>
        /// <returns>A new <see cref="Twitterizer.OAuthUtility.TokenResponse"/> instance.</returns>
        public static TokenResponse GetRequestToken(string consumerKey, string consumerSecret, string callBackUrl)
        {
            TokenResponse response = new TokenResponse();

            try
            {
                HttpWebRequest request = CreateOAuthRequest(
                    "http://twitter.com/oauth/request_token",
                    null,
                    "GET",
                    consumerKey,
                    consumerSecret,
                    null,
                    null,
                    callBackUrl);

                request.Method = "GET";

                WebResponse webResponse = request.GetResponse();

                string responseBody = new StreamReader(webResponse.GetResponseStream()).ReadToEnd();

                Match matchedValues = Regex.Match(responseBody, @"oauth_token=(?<token>[^&]+)|oauth_token_secret=(?<secret>[^&]+)");

                response.Token = matchedValues.Groups["token"].Value;
                response.TokenSecret = matchedValues.Groups["secret"].Value;
            }
            catch (WebException wex)
            {
                string httpResponse = new StreamReader(wex.Response.GetResponseStream()).ReadToEnd();

                if (string.IsNullOrEmpty(httpResponse))
                {
                    throw;
                }

                throw new ApplicationException(httpResponse, wex);
            }

            return response;
        }

        /// <summary>
        /// Gets an access token.
        /// </summary>
        /// <param name="consumerKey">The consumer key.</param>
        /// <param name="consumerSecret">The consumer secret.</param>
        /// <param name="requestToken">The request token.</param>
        /// <returns>
        /// A <see cref="Twitterizer.OAuthUtility.TokenResponse"/> containing the requested tokens.
        /// </returns>
        public static TokenResponse GetAccessToken(string consumerKey, string consumerSecret, string requestToken)
        {
            TokenResponse response = new TokenResponse();

            try
            {
                HttpWebRequest request = CreateOAuthRequest(
                    "http://twitter.com/oauth/access_token",
                    null,
                    "GET",
                    consumerKey,
                    consumerSecret,
                    requestToken,
                    string.Empty,
                    string.Empty);

                request.Method = "GET";

                WebResponse webResponse = request.GetResponse();

                string responseBody = new StreamReader(webResponse.GetResponseStream()).ReadToEnd();

                response.Token = Regex.Match(responseBody, @"oauth_token=([^&]+)").Groups[1].Value;
                response.TokenSecret = Regex.Match(responseBody, @"oauth_token_secret=([^&]+)").Groups[1].Value;
                response.UserID = long.Parse(Regex.Match(responseBody, @"user_id=([^&]+)").Groups[1].Value);
                response.Screenname = Regex.Match(responseBody, @"screen_name=([^&]+)").Groups[1].Value;
            }
            catch (WebException wex)
            {
                string httpResponse = new StreamReader(wex.Response.GetResponseStream()).ReadToEnd();

                if (string.IsNullOrEmpty(httpResponse))
                {
                    throw;
                }

                throw new ApplicationException(httpResponse, wex);
            }

            return response;
        }

        /// <summary>
        /// This is a different Url Encode implementation since the default .NET one outputs the percent encoding in lower case.
        /// While this is not a problem with the percent encoding spec, it is used in upper case throughout OAuth
        /// </summary>
        /// <param name="value">The value to Url encode</param>
        /// <returns>Returns a Url encoded string</returns>
        public static string UrlEncode(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return string.Empty;
            }

            char[] unreservedChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-_.~".ToCharArray();

            StringBuilder result = new StringBuilder();

            foreach (char symbol in value)
            {
                if (unreservedChars.Contains(symbol))
                {
                    result.Append(symbol);
                    continue;
                }

                if (symbol == ' ')
                {
                    result.Append("%2520");
                    continue;
                }

                result.AppendFormat("%{0:X2}", (int)symbol);
            }

            return result.ToString();
        }
        #endregion

        /// <summary>
        /// Creates the OAuth request.
        /// </summary>
        /// <param name="baseUrl">The base URL.</param>
        /// <param name="parameters">The parameters.</param>
        /// <param name="httpMethod">The HTTP method.</param>
        /// <param name="consumerKey">The consumer key.</param>
        /// <param name="consumerSecret">The consumer secret.</param>
        /// <param name="token">The access or request token.</param>
        /// <param name="tokenSecret">The token secret.</param>
        /// <param name="callBackUri">The call back URI.</param>
        /// <returns>A new instance of the <see cref="System.Net.HttpWebRequest"/> class.</returns>
        internal static HttpWebRequest CreateOAuthRequest(
            string baseUrl,
            Dictionary<string, string> parameters,
            string httpMethod,
            string consumerKey,
            string consumerSecret,
            string token,
            string tokenSecret,
            string callBackUri)
        {
            Dictionary<string, string> newParameters = new Dictionary<string, string>();

            if (parameters != null)
            {
                // Copy the given parameters into a new collection, as to not modify the source collection
                foreach (KeyValuePair<string, string> item in parameters)
                {
                    newParameters.Add(item.Key, item.Value);
                }
            }

            // Add the OAuth parameters
            newParameters.Add("oauth_version", "1.0");
            newParameters.Add("oauth_nonce", GenerateNonce());
            newParameters.Add("oauth_timestamp", GenerateTimeStamp());
            newParameters.Add("oauth_signature_method", "HMAC-SHA1");
            newParameters.Add("oauth_consumer_key", consumerKey);
            newParameters.Add("oauth_consumer_secret", consumerSecret);
            newParameters.Add("oauth_token", token);

            if (!string.IsNullOrEmpty(tokenSecret))
            {
                newParameters.Add("oauth_token_secret", tokenSecret);
            }

            if (!string.IsNullOrEmpty(callBackUri))
            {
                newParameters.Add("oauth_callback", callBackUri);
            }

            AddSignatureToParameters(
                new Uri(baseUrl),
                newParameters,
                httpMethod,
                consumerSecret,
                tokenSecret);

            StringBuilder authHeaderBuilder = new StringBuilder("Authorization: OAuth");

            foreach (var item in newParameters
                .Where(p => p.Key.Contains("oauth_") && !p.Key.EndsWith("_secret"))
                .OrderBy(p=> p.Key)
                .ThenBy(p=>UrlEncode(p.Value)))
            {
                authHeaderBuilder.AppendFormat(
                    ",{0}=\"{1}\"",
                    UrlEncode(item.Key),
                    UrlEncode(item.Value));
            }

#if DEBUG
            System.Diagnostics.Debug.WriteLine(string.Format("OAUTH HEADER: {0}", authHeaderBuilder.ToString()));
#endif

            HttpWebRequest request = null;

            if (httpMethod == "GET")
            {
                StringBuilder uriBuilder = new StringBuilder();

                var queryStringParameters = from p in newParameters
                                            where !p.Key.Contains("oauth_")
                                            orderby p.Key, p.Value
                                            select p;

                foreach (KeyValuePair<string, string> item in queryStringParameters)
                {
                    if (uriBuilder.Length > 0)
                    {
                        uriBuilder.Append("&");
                    }

                    uriBuilder.AppendFormat(
                        "{0}={1}&",
                        item.Key,
                        UrlEncode(item.Value));
                }

                uriBuilder.Insert(0, string.Format("{0}?", baseUrl));

                request = (HttpWebRequest)WebRequest.Create(uriBuilder.ToString());
                request.Method = httpMethod;
                request.Headers.Add(authHeaderBuilder.ToString());

#if DEBUG
                System.Diagnostics.Debug.WriteLine(string.Format("OAUTH GET: {0}", uriBuilder.ToString()));
#endif
            }
            else if (httpMethod == "POST")
            {
                StringBuilder stringBuilder = new StringBuilder();

                foreach (KeyValuePair<string, string> item in newParameters.Where(p => !p.Key.Contains("oauth_")))
                {
                    if (stringBuilder.Length > 0)
                    {
                        stringBuilder.Append("&");
                    }

                    stringBuilder.AppendFormat(
                        "{0}={1}",
                        item.Key,
                        UrlEncode(item.Value));
                }

                string postData = stringBuilder.ToString();

                request = (HttpWebRequest)WebRequest.Create(baseUrl);

                request.Method = httpMethod;
                request.ContentType = "application/x-www-form-urlencoded";
                request.Headers.Add(authHeaderBuilder.ToString());

                using (StreamWriter postDataWriter = new StreamWriter(request.GetRequestStream()))
                {
                    postDataWriter.Write(Encoding.UTF8.GetBytes(postData));
                    postDataWriter.Close();
                }
#if DEBUG
                System.Diagnostics.Debug.WriteLine(string.Format("OAUTH POST: {0}\nPOST DATA: {1}", baseUrl, postData));
#endif
            }
            else
            {
                throw new ArgumentException("The HTTP method supplied is not supported.", "httpMethod");
            }

            return request;
        }

        /// <summary>
        /// Flattens the and encode parameters.
        /// </summary>
        /// <param name="queryStringParameters">The query string parameters.</param>
        /// <returns></returns>
        private static string FlattenAndEncodeParameters(IEnumerable<KeyValuePair<string, string>> queryStringParameters)
        {
            StringBuilder stringBuilder = new StringBuilder();

            foreach (KeyValuePair<string, string> item in queryStringParameters)
            {
                if (stringBuilder.Length > 0)
                {
                    stringBuilder.Append("&");
                }

                stringBuilder.AppendFormat(
                    "{0}={1}",
                    item.Key,
                    UrlEncode(item.Value));
            }

            return stringBuilder.ToString();
        }

        /// <summary>
        /// Generate the timestamp for the signature        
        /// </summary>
        /// <returns>A timestamp value in a string.</returns>
        private static string GenerateTimeStamp()
        {
            // Default implementation of UNIX time of the current UTC time
            TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return Convert.ToInt64(ts.TotalSeconds).ToString();
        }

        /// <summary>
        /// Generate a nonce
        /// </summary>
        /// <returns>A random number between 123400 and 9999999 in a string.</returns>
        private static string GenerateNonce()
        {
            // Just a simple implementation of a random number between 123400 and 9999999
            return new Random().Next(123400, int.MaxValue).ToString();
        }

        /// <summary>
        /// Generates and adds a signature to parameters.
        /// </summary>
        /// <param name="url">The base URL.</param>
        /// <param name="parameters">The parameters.</param>
        /// <param name="httpMethod">The HTTP method.</param>
        /// <param name="consumerSecret">The consumer secret.</param>
        /// <param name="tokenSecret">The token secret.</param>
        private static void AddSignatureToParameters(
            Uri url, 
            Dictionary<string, string> parameters,
            string httpMethod,
            string consumerSecret,
            string tokenSecret)
        {
            string normalizedUrl = NormalizeUrl(url);

            // Get the oauth parameters from the parameters
            Dictionary<string, string> baseStringParameters =
                (from p in parameters
                 where !(p.Key.EndsWith("_secret") && p.Key.StartsWith("oauth_"))
                 select p).ToDictionary(p => p.Key, p => p.Value);

            string signatureBase = string.Format(
                "{0}&{1}&{2}",
                httpMethod.ToUpper(),
                UrlEncode(normalizedUrl),
                UrlEncode(baseStringParameters));

            HMACSHA1 hmacsha1 = new HMACSHA1();

            string key = string.Format(
                    "{0}&{1}",
                    UrlEncode(consumerSecret),
                    UrlEncode(tokenSecret));

            hmacsha1.Key = Encoding.ASCII.GetBytes(key);

            string result = Convert.ToBase64String(
                hmacsha1.ComputeHash(
                    Encoding.ASCII.GetBytes(signatureBase)));

            // Add the signature to the oauth parameters
            parameters.Add("oauth_signature", result);

#if DEBUG
            System.Diagnostics.Debug.WriteLine("----------- OAUTH SIGNATURE GENERATION -----------");
            System.Diagnostics.Debug.WriteLine(string.Format("url.PathAndQuery = \"{0}\"", url.PathAndQuery));
            System.Diagnostics.Debug.WriteLine(string.Format("httpMethod = \"{0}\"", httpMethod));
            System.Diagnostics.Debug.WriteLine(string.Format("consumerSecret = \"{0}\"", consumerSecret));
            System.Diagnostics.Debug.WriteLine(string.Format("tokenSecret = \"{0}\"", tokenSecret));
            System.Diagnostics.Debug.WriteLine(string.Format("normalizedUrl = \"{0}\"", normalizedUrl));
            System.Diagnostics.Debug.WriteLine(string.Format("signatureBase = \"{0}\"", signatureBase));
            System.Diagnostics.Debug.WriteLine(string.Format("key = \"{0}\"", key));
            System.Diagnostics.Debug.WriteLine(string.Format("signature = \"{0}\"", result));
            System.Diagnostics.Debug.WriteLine("--------- END OAUTH SIGNATURE GENERATION ----------");
#endif
        }

        /// <summary>
        /// Normalizes the URL.
        /// </summary>
        /// <param name="url">The URL to normalize.</param>
        /// <returns>The normalized url string.</returns>
        private static string NormalizeUrl(Uri url)
        {
            string normalizedUrl = string.Format("{0}://{1}", url.Scheme, url.Host);
            if (!((url.Scheme == "http" && url.Port == 80) || (url.Scheme == "https" && url.Port == 443)))
            {
                normalizedUrl += ":" + url.Port;
            }

            normalizedUrl += url.AbsolutePath;
            return normalizedUrl;
        }

        /// <summary>
        /// URLs the encode.
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>A string of all the <paramref name="parameters"/> keys and value pairs with the values encoded.</returns>
        private static string UrlEncode(Dictionary<string, string> parameters)
        {
            StringBuilder parameterString = new StringBuilder();

            var paramsSorted = from p in parameters
                               orderby p.Key, p.Value
                               select p;

            foreach (var item in paramsSorted)
            {
                parameterString.Append(
                    string.Format(
                        "{0}={1}&", 
                        item.Key, 
                        item.Value));
            }

            parameterString.Remove(parameterString.Length - 1, 1);

            return UrlEncode(parameterString.ToString());
        }

        /// <summary>
        /// Values returned by Twitter when getting a request token or an access token.
        /// </summary>
        public class TokenResponse
        {
            /// <summary>
            /// Gets or sets the token.
            /// </summary>
            /// <value>The token.</value>
            public string Token { get; set; }

            /// <summary>
            /// Gets or sets the token secret.
            /// </summary>
            /// <value>The token secret.</value>
            public string TokenSecret { get; set; }

            /// <summary>
            /// Gets or sets the user ID.
            /// </summary>
            /// <value>The user ID.</value>
            public long UserID { get; set; }

            /// <summary>
            /// Gets or sets the screenname.
            /// </summary>
            /// <value>The screenname.</value>
            public string Screenname { get; set; }
        }
    }
}
