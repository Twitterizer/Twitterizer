namespace Twitterizer
{
    using System;
    using Twitterizer.Core;

    /// <summary>
    /// Represents an empty object.
    /// </summary>
    [Serializable]
    public class TwitterEmptyObject : ITwitterObject
    {
        /// <summary>
        /// Gets or sets a value indicating whether this instance is empty.
        /// </summary>
        /// <value><c>true</c> if this instance is empty; otherwise, <c>false</c>.</value>
        public bool IsEmpty { get; set; }

        /// <summary>
        /// Gets or sets information about the user's rate usage.
        /// </summary>
        /// <value>The rate limiting object.</value>
        public RateLimiting RateLimiting { get; set; }

        /// <summary>
        /// Gets or sets the oauth tokens.
        /// </summary>
        /// <value>The oauth tokens.</value>
        public OAuthTokens Tokens { get; set; }

        /// <summary>
        /// Gets details about the request attempted.
        /// </summary>
        /// <value>The last request status.</value>
        public RequestStatus RequestStatus { get; set; }
    }
}
