namespace Twitterizer
{
    using System;

    /// <summary>
    /// An asynchronous wrapper around the <see cref="TwitterAccount"/> class.
    /// </summary>
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
            return AsyncUtility.ExecuteAsyncMethod(tokens, options, timeout, TwitterRateLimitStatus.GetStatus, function);
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
            return AsyncUtility.ExecuteAsyncMethod(tokens, timeout, TwitterAccount.VerifyCredentials, function);
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
            return AsyncUtility.ExecuteAsyncMethod(tokens, options, timeout, TwitterAccount.VerifyCredentials, function);
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
            return AsyncUtility.ExecuteAsyncMethod(tokens, imageData, options, timeout, TwitterAccount.UpdateProfileImage, function);
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
            return AsyncUtility.ExecuteAsyncMethod(tokens, imageData, options, timeout, TwitterAccount.UpdateProfileBackgroundImage, function);
        }
    }
}
