//-----------------------------------------------------------------------
// <copyright file="OAuth.cs" company="Patrick 'Ricky' Smith">
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
// <email>ricky@digitally-born.com</email>
// <date>2010-02-26</date>
// <summary>Provides simple methods to simplify OAuth interaction.</summary>
//-----------------------------------------------------------------------

namespace Twitterizer
{
    using System;
    using System.Globalization;
    using System.Net;
    using System.Collections.Generic;
    using System.Text;
    using System.Security.Cryptography;
    using System.Linq;
    using System.IO;

    public class OAuthUtility
    {
        private const string SignatureType = "HMAC-SHA1";

        public static TokenResponse GetRequestToken(string consumerKey, string consumerSecret)
        {
            try
            {
                HttpWebRequest request = CreateOAuthRequest(
                    "http://www.twitter.com/oauth/request_token",
                    null,
                    "POST",
                    consumerKey,
                    consumerSecret,
                    null);
                    
                request.Method = "POST";

                WebResponse webResponse = request.GetResponse();
            }
            catch (WebException wex)
            {
                
                throw;
            }
           
            TokenResponse response = new TokenResponse();
            return response;
        }

        public static TokenResponse GetAccessToken(string consumerKey, string requestToken)
        {
            TokenResponse response = new TokenResponse();
            return response;
        }

        /// <summary>
        /// Values returned by Twitter when getting a request token or an access token.
        /// </summary>
        public class TokenResponse
        {
            public string Token { get; set; }
            public string TokenSecret { get; set; }
        }

        #region Taken from oAuthBase
        /// <summary>
        /// Generate the timestamp for the signature        
        /// </summary>
        /// <returns></returns>
        internal static string GenerateTimeStamp()
        {
            // Default implementation of UNIX time of the current UTC time
            TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return Convert.ToInt64(ts.TotalSeconds).ToString();
        }

        /// <summary>
        /// Generate a nonce
        /// </summary>
        /// <returns></returns>
        internal static string GenerateNonce()
        {
            // Just a simple implementation of a random number between 123400 and 9999999
            return new Random().Next(123400, 9999999).ToString();
        }

        /// <summary>
        /// Generates and adds a signature to parameters.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <param name="parameters">The parameters.</param>
        /// <param name="httpMethod">The HTTP method.</param>
        /// <param name="consumerSecret">The consumer secret.</param>
        /// <param name="tokenSecret">The token secret.</param>
        internal static void AddSignatureToParameters(
            Uri url, 
            Dictionary<string, string> parameters,
            string httpMethod,
            string consumerSecret,
            string tokenSecret)
        {
            string normalizedUrl = NormalizeUrl(url);

            // Get the oauth parameters the parameters
            Dictionary<string, string> oauthParameters =
                (from p in parameters
                 where p.Key.StartsWith("oauth_")
                 select p).ToDictionary(p => p.Key, p => p.Value);

            string signatureBase = string.Format(
                "{0}&{1}&{2}",
                httpMethod.ToUpper(),
                UrlEncode(normalizedUrl),
                UrlEncode(oauthParameters));


            string ignoreUrl;
            string ignoreParams;
            string compareBase = new Removed.OAuthBase().GenerateSignatureBase(
                url,
                oauthParameters["oauth_consumer_key"],
                string.Empty,
                tokenSecret,
                httpMethod,
                oauthParameters["oauth_timestamp"],
                oauthParameters["oauth_nonce"],
                oauthParameters["oauth_signature_method"],
                out ignoreUrl,
                out ignoreParams);

            HMACSHA1 hmacsha1 = new HMACSHA1();

            hmacsha1.Key = Encoding.ASCII.GetBytes(
                string.Format(
                    "{0}&{1}", 
                    UrlEncode(consumerSecret), 
                    string.IsNullOrEmpty(tokenSecret) ? "" : UrlEncode(tokenSecret)));

            byte[] dataBuffer = Encoding.ASCII.GetBytes(signatureBase);
            byte[] hashBytes = hmacsha1.ComputeHash(dataBuffer);

            // Add the signature to the oauth parameters
            parameters.Add("oauth_signature", Convert.ToBase64String(hashBytes));
        }

        private static void SortParameters(Dictionary<string, string> parameters)
        {
            parameters
                .OrderBy(p => p.Key)
                .ThenBy(p => p.Value)
                .ToDictionary(p => p.Key, p => p.Value);
        }

        /// <summary>
        /// Normalizes the URL.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <returns></returns>
        internal static string NormalizeUrl(Uri url)
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
        /// This is a different Url Encode implementation since the default .NET one outputs the percent encoding in lower case.
        /// While this is not a problem with the percent encoding spec, it is used in upper case throughout OAuth
        /// </summary>
        /// <param name="value">The value to Url encode</param>
        /// <returns>Returns a Url encoded string</returns>
        internal static string UrlEncode(string value)
        {
            char[] unreservedChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-_.~".ToCharArray();

            StringBuilder result = new StringBuilder();

            foreach (char symbol in value)
            {
                if (unreservedChars.Contains(symbol))
                {
                    result.Append(symbol);
                    continue;
                }

                result.Append('%' + String.Format("{0:X2}", (int)symbol));
            }

            return result.ToString();
        }

        /// <summary>
        /// URLs the encode.
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns></returns>
        internal static string UrlEncode(Dictionary<string, string> parameters)
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
        #endregion

        internal static HttpWebRequest CreateOAuthRequest(
            string baseUrl, 
            Dictionary<string, string> parameters, 
            string httpMethod, 
            string consumerKey,
            string consumerSecret,
            string tokenSecret)
        {
            Dictionary<string, string> newParameters = new Dictionary<string, string>();

            if (parameters != null)
            {
                foreach (KeyValuePair<string, string> item in parameters)
                {
                    newParameters.Add(item.Key, item.Value);
                }
            }

            newParameters.Add("oauth_version", "1.0");
            newParameters.Add("oauth_nonce", GenerateNonce());
            newParameters.Add("oauth_timestamp", GenerateTimeStamp());
            newParameters.Add("oauth_signature_method", "HMAC-SHA1");
            newParameters.Add("oauth_consumer_key", consumerKey);

            AddSignatureToParameters(
                new Uri(baseUrl),
                newParameters,
                httpMethod,
                consumerSecret,
                tokenSecret);

            StringBuilder uriBuilder = new StringBuilder(baseUrl);
            uriBuilder.Append('?');

            // Append the oauth parameters first, sorted by key, then value
            var oauthParametersOrdered = from p in newParameters
                                         where p.Key.Contains("oauth_")
                                         orderby p.Key, p.Value
                                         select p;

            foreach (KeyValuePair<string, string> item in oauthParametersOrdered)
            {
                uriBuilder.AppendFormat(
                    "{0}={1}&",
                    item.Key,
                    UrlEncode(item.Value));
            }

            // Now append the other parameters the same way
            var otherParametersOrdered = from p in newParameters
                                         where !p.Key.Contains("oauth_")
                                         orderby p.Key, p.Value
                                         select p;

            foreach (KeyValuePair<string, string> item in otherParametersOrdered)
            {
                uriBuilder.AppendFormat(
                    "{0}={1}&",
                    item.Key,
                    UrlEncode(item.Value));
            }

            uriBuilder.Remove(uriBuilder.Length - 1, 1);

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uriBuilder.ToString());
            request.Method = httpMethod;

            return request;
        }
    }
}
