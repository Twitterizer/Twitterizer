namespace Twitterizer
{
    using System;

    public static class TwitterRateLimitStatusAsync
    {
        /// <summary>
        /// Gets the rate limiting status status for the authenticated user.
        /// </summary>
        /// <param name="tokens">The OAuth tokens.</param>
        /// <param name="options">The options.</param>
        /// <param name="timeout">The timeout.</param>
        /// <param name="function">The function.</param>
        /// <returns>
        /// A <see cref="TwitterRateLimitStatus"/> instance.
        /// </returns>
        public static IAsyncResult GetStatus(OAuthTokens tokens, OptionalProperties options, TimeSpan timeout, Action<TwitterResponse<TwitterRateLimitStatus>> function)
        {
            Func<OAuthTokens, OptionalProperties, TwitterResponse<TwitterRateLimitStatus>> methodToCall = TwitterRateLimitStatus.GetStatus;

            return methodToCall.BeginInvoke(
                tokens,
                options,
                result =>
                {
                    result.AsyncWaitHandle.WaitOne(timeout);
                    function(methodToCall.EndInvoke(result));
                },
                null);
        }
    }
}
