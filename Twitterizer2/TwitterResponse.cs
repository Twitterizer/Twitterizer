using System;

namespace Twitterizer
{
    /// <summary>
    /// The twitter response class provides details of the response from an api call to the twitter api.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [Serializable]
    public class TwitterResponse<T>
        where T : Core.ITwitterObject
    {
        /// <summary>
        /// Gets or sets the response object.
        /// </summary>
        /// <value>The response object.</value>
        public T ResponseObject { get; internal set; }

        /// <summary>
        /// Gets or sets the result.
        /// </summary>
        /// <value>The result.</value>
        public RequestResult Result { get; set; }

        /// <summary>
        /// Gets or sets the request URL.
        /// </summary>
        /// <value>The request URL.</value>
        public string RequestUrl { get; set; }

        /// <summary>
        /// Gets the response body.
        /// </summary>
        /// <value>The response body.</value>
        public string Content { get; internal set; }

        /// <summary>
        /// Gets or sets the error message.
        /// </summary>
        /// <value>The error message.</value>
        public string ErrorMessage { get; set; }

        /// <summary>
        /// Gets or sets the tokens.
        /// </summary>
        /// <value>The tokens.</value>
        internal OAuthTokens Tokens { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [response cached].
        /// </summary>
        /// <value><c>true</c> if [response cached]; otherwise, <c>false</c>.</value>
        public Boolean ResponseCached { get; set; }

        /// <summary>
        /// Gets or sets the rate limiting.
        /// </summary>
        /// <value>The rate limiting.</value>
        public RateLimiting RateLimiting { get; set; }
    }
}
