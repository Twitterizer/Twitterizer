namespace Twitterizer
{
    using System;
    using System.IO;
    using System.Net;
    using Twitterizer.Core;

    public class AuthenticationFailedException : TwitterizerException
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="AuthenticationFailedException"/> class.
        /// </summary>
        public AuthenticationFailedException() 
            : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthenticationFailedException"/> class.
        /// </summary>
        /// <param name="wex">The <see cref="System.Net.WebException"/>.</param>
        public AuthenticationFailedException(WebException wex)
            : this("Authentication failed.", wex)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthenticationFailedException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="wex">The wex.</param>
        public AuthenticationFailedException(string message, WebException wex)
            : base(message, wex)
        {
            this.ResponseBody = new StreamReader(wex.Response.GetResponseStream()).ReadToEnd();

            this.RateLimiting = new RateLimiting();

            if (!string.IsNullOrEmpty(wex.Response.Headers.Get("X-RateLimit-Limit")))
            {
                this.RateLimiting.Total = int.Parse(wex.Response.Headers.Get("X-RateLimit-Limit"));
            }

            if (!string.IsNullOrEmpty(wex.Response.Headers.Get("X-RateLimit-Remaining")))
            {
                this.RateLimiting.Remaining = int.Parse(wex.Response.Headers.Get("X-RateLimit-Remaining"));
            }

            if (!string.IsNullOrEmpty(wex.Response.Headers["X-RateLimit-Reset"]))
            {
                this.RateLimiting.ResetDate = (new DateTime(1970, 1, 1, 0, 0, 0, 0))
                    .AddSeconds(double.Parse(wex.Response.Headers.Get("X-RateLimit-Reset"))); ;
            }
        }
        #endregion

    }
}
