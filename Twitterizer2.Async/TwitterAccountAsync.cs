using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Twitterizer;

namespace Twitterizer2
{
    public static class TwitterAccountAsync
    {
        /// <summary>
        /// Gets the rate limiting status status for the authenticated user asynchronously.
        /// </summary>
        /// <param name="tokens">The OAuth tokens.</param>
        /// <param name="options">The options.</param>
        /// <param name="function">The callback or anonymous funtion.</param>
        /// <returns>
        /// A <see cref="TwitterRateLimitStatus"/> instance.
        /// </returns>
        public static void GetStatus(OAuthTokens tokens, OptionalProperties options, Action<TwitterResponse<TwitterRateLimitStatus>> function)
        {
            Func<OAuthTokens, OptionalProperties, TwitterResponse<TwitterRateLimitStatus>> methodToCall = TwitterRateLimitStatus.GetStatus;

            methodToCall.BeginInvoke(
                tokens,
                options,
                result => function(methodToCall.EndInvoke(result)),
                null);
        }
    }
}
