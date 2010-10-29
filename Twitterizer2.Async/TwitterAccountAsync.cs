using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Twitterizer;

namespace Twitterizer
{
    public static class TwitterAccountAsync
    {
        /// <summary>
        /// Gets the rate limiting status status for the authenticated user asynchronously.
        /// </summary>
        /// <param name="tokens">The OAuth tokens.</param>
        /// <param name="options">The options.</param>
        /// <param name="timeout">The timeout.</param>
        /// <param name="function">The callback or anonymous funtion.</param>
        /// <returns>
        /// A <see cref="TwitterRateLimitStatus"/> instance.
        /// </returns>
        public static IAsyncResult GetStatus(OAuthTokens tokens, OptionalProperties options, TimeSpan timeout, Action<TwitterAsyncResponse<TwitterRateLimitStatus>> function)
        {
            Func<OAuthTokens, OptionalProperties, TwitterResponse<TwitterRateLimitStatus>> methodToCall = TwitterRateLimitStatus.GetStatus;

            return methodToCall.BeginInvoke(
                tokens,
                options,
                result => 
                {
                    result.AsyncWaitHandle.WaitOne(timeout);
                    try
                    {
                        function(methodToCall.EndInvoke(result).ToAsyncResponse());
                    }
                    catch (Exception ex)
                    {
                        function(new TwitterAsyncResponse<TwitterRateLimitStatus>() { Result = RequestResult.Unknown, ExceptionThrown = ex });
                    }
                },
                null);
        }


        /// <summary>
        /// Attempts to verify the supplied credentials.
        /// </summary>
        /// <param name="tokens">The tokens.</param>  
        /// <param name="timeout">The timeout.</param>
        /// <param name="function">The callback or anonymous funtion.</param>
        /// <returns>
        /// The user, as a <see cref="TwitterUser"/>
        /// </returns>       
        public static IAsyncResult VerifyCredentials(OAuthTokens tokens, TimeSpan timeout, Action<TwitterAsyncResponse<TwitterUser>> function)
        {
            Func<OAuthTokens, TwitterResponse<TwitterUser>> methodToCall = TwitterAccount.VerifyCredentials;

            return methodToCall.BeginInvoke(
                tokens,
                result => 
                {
                    result.AsyncWaitHandle.WaitOne(timeout);
                    try
                    {
                        function(methodToCall.EndInvoke(result).ToAsyncResponse());
                    }
                    catch (Exception ex)
                    {
                        function(new TwitterAsyncResponse<TwitterUser>() { Result = RequestResult.Unknown, ExceptionThrown = ex });
                    }
                },
                null);
        }

        /// <summary>
        /// Attempts to verify the supplied credentials.
        /// </summary>
        /// <param name="tokens">The tokens.</param>  
        /// <param name="options">The options.</param>
        /// <param name="timeout">The timeout.</param>
        /// <param name="function">The callback or anonymous funtion.</param>
        /// <returns>
        /// The user, as a <see cref="TwitterUser"/>
        /// </returns>       
        public static IAsyncResult VerifyCredentials(OAuthTokens tokens, VerifyCredentialsOptions options, TimeSpan timeout, Action<TwitterAsyncResponse<TwitterUser>> function)
        {
            Func<OAuthTokens, VerifyCredentialsOptions, TwitterResponse<TwitterUser>> methodToCall = TwitterAccount.VerifyCredentials;

            return methodToCall.BeginInvoke(
                tokens,
                options,
                result =>
                {
                    result.AsyncWaitHandle.WaitOne(timeout);
                    try
                    {
                        function(methodToCall.EndInvoke(result).ToAsyncResponse());
                    }
                    catch (Exception ex)
                    {
                        function(new TwitterAsyncResponse<TwitterUser>() { Result = RequestResult.Unknown, ExceptionThrown = ex });
                    }
                },
                null);
        }

    }
}
