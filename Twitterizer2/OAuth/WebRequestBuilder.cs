namespace Twitterizer
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
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

    public sealed class WebRequestBuilder
    {
        public Uri RequestUri { get; set; }
        
        public Dictionary<string, string> Parameters { get; private set; }

        public Dictionary<string, string> OAuthParameters { get; private set; }

        public HTTPVerb Verb { get; set; }

        public OAuthTokens Tokens { get; set; }

        public WebProxy Proxy { get; set; }

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
            SetupOAuth();
            RebuildRequestUriWithParameters();

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(this.RequestUri);
            request.Method = this.Verb.ToString();
            request.UserAgent = string.Format(CultureInfo.InvariantCulture, "Twitterizer/{0}", Information.AssemblyVersion());

            if (this.UseOAuth)
                request.Headers.Add("Authorization", GenerateAuthorizationHeader());

            if (this.Proxy != null)
                request.Proxy = Proxy;

            if (this.Verb == HTTPVerb.POST)
            {
                request.ContentType = "application/x-www-form-urlencoded";
            }

            return (HttpWebResponse)request.GetResponse();
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
        private void SetupOAuth()
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
        /// This is a different Url Encode implementation since the default .NET one outputs the percent encoding in lower case.
        /// While this is not a problem with the percent encoding spec, it is used in upper case throughout OAuth
        /// </summary>
        /// <param name="value">The value to Url encode</param>
        /// <returns>Returns a Url encoded string</returns>
        [SuppressMessage("Microsoft.Design", "CA1055:UriReturnValuesShouldNotBeStrings", Justification = "Return type is not a URL.")]
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
        /// URLs the encode.
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

        private string GenerateAuthorizationHeader()
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
