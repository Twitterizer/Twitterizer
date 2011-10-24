namespace Twitterizer
{
    using System;
#if SILVERLIGHT
    using System.Threading;
#endif
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
                    catch (TwitterizerException ex)
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
#if !SILVERLIGHT
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
#else            
            ThreadPool.QueueUserWorkItem((x) =>
                {
                    function(TwitterAccount.VerifyCredentials(tokens).ToAsyncResponse<TwitterUser>());  
                });
            return null;
#endif
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
#if !SILVERLIGHT
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
#else            
            ThreadPool.QueueUserWorkItem((x) =>
                {
                    function(TwitterAccount.VerifyCredentials(tokens, options).ToAsyncResponse<TwitterUser>());  
                });
            return null;
#endif
        }

        /// <summary>
        /// Updates the authenticating user's profile image. Note that this method expects raw multipart data, not a URL to an image.
        /// </summary>
        /// <param name="tokens">The tokens.</param>
        /// <param name="imageLocation">The image location.</param>
        /// <param name="options">The options.</param>
        /// <param name="timeout">The timeout.</param>
        /// <param name="function">The callback or anonymous funtion.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static IAsyncResult UpdateProfileImage(OAuthTokens tokens, string imageLocation, TimeSpan timeout, Action<TwitterAsyncResponse<TwitterUser>> function, OptionalProperties options = null)
        {
            return UpdateProfileImage(tokens, System.IO.File.ReadAllBytes(imageLocation), timeout, function, options);
        }

        /// <summary>
        /// Updates the authenticating user's profile image. Note that this method expects raw multipart data, not a URL to an image.
        /// </summary>
        /// <param name="tokens">The tokens.</param>
        /// <param name="imageData">The image data.</param>
        /// <param name="timeout">The timeout.</param>
        /// <param name="function">The callback or anonymous funtion.</param>
        /// <param name="options">The options.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static IAsyncResult UpdateProfileImage(OAuthTokens tokens, byte[] imageData, TimeSpan timeout, Action<TwitterAsyncResponse<TwitterUser>> function, OptionalProperties options = null)
        {
            Func<OAuthTokens, byte[], OptionalProperties, TwitterResponse<TwitterUser>> methodToCall = TwitterAccount.UpdateProfileImage;

            return methodToCall.BeginInvoke(
                tokens,
                imageData,
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


        /// <summary>
        /// Updates the authenticating user's profile background image. This method can also be used to enable or disable the profile background image.
        /// </summary>
        /// <param name="tokens">The tokens.</param>
        /// <param name="fileLocation">The file location.</param>
        /// <param name="timeout">The timeout.</param>
        /// <param name="function">The function.</param>
        /// <param name="options">The options.</param>
        /// <returns></returns>
        public static IAsyncResult UpdateProfileBackgroundImage(OAuthTokens tokens, string fileLocation, TimeSpan timeout, Action<TwitterAsyncResponse<TwitterUser>> function, UpdateProfileBackgroundImageOptions options = null)
        {
            return UpdateProfileBackgroundImage(tokens, System.IO.File.ReadAllBytes(fileLocation), timeout, function, options);
        }

        /// <summary>
        /// Updates the authenticating user's profile background image. This method can also be used to enable or disable the profile background image.
        /// </summary>
        /// <param name="tokens">The tokens.</param>
        /// <param name="imageData">The image data.</param>
        /// <param name="timeout">The timeout.</param>
        /// <param name="function">The function.</param>
        /// <param name="options">The options.</param>
        /// <returns></returns>
        public static IAsyncResult UpdateProfileBackgroundImage(OAuthTokens tokens, byte[] imageData, TimeSpan timeout, Action<TwitterAsyncResponse<TwitterUser>> function, UpdateProfileBackgroundImageOptions options = null)
        {
            Func<OAuthTokens, byte[], UpdateProfileBackgroundImageOptions, TwitterResponse<TwitterUser>> methodToCall = TwitterAccount.UpdateProfileBackgroundImage;

            return methodToCall.BeginInvoke(
                tokens,
                imageData,
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
