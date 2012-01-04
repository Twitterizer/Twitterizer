namespace Twitterizer
{
    using System;

    /// <summary>
    /// An asynchronous wrapper around the <see cref="TwitterDirectMessage"/> class.
    /// </summary>
    public static class TwitterDirectMessageAsync
    {
        /// <summary>
        /// Deletes the specified direct message.
        /// </summary>
        /// <param name="tokens">The tokens.</param>
        /// <param name="id">The direct message id.</param>
        /// <param name="options">The options.</param>
        /// <param name="timeout">The timeout.</param>
        /// <param name="function">The function.</param>
        /// <returns></returns>
        public static IAsyncResult Delete(OAuthTokens tokens, decimal id, OptionalProperties options, TimeSpan timeout, Action<TwitterAsyncResponse<TwitterDirectMessage>> function)
        {
            return AsyncUtility.ExecuteAsyncMethod(tokens, id, options, timeout, TwitterDirectMessage.Delete, function);
        }

        /// <summary>
        /// Returns a list of the 20 most recent direct messages sent by the authenticating user.
        /// </summary>
        /// <param name="tokens">The tokens.</param>
        /// <param name="options">The options.</param>
        /// <param name="timeout">The time period to wait for a response.</param>
        /// <param name="function">The method to call when the response arrives.</param>
        /// <returns>
        /// A <see cref="TwitterDirectMessageCollection"/> instance.
        /// </returns>
        public static IAsyncResult DirectMessagesSent(OAuthTokens tokens, DirectMessagesSentOptions options, TimeSpan timeout, Action<TwitterAsyncResponse<TwitterDirectMessageCollection>> function)
        {
            return AsyncUtility.ExecuteAsyncMethod(tokens, options, timeout, TwitterDirectMessage.DirectMessagesSent, function);
        }

        /// <summary>
        /// Returns a list of the 20 most recent direct messages sent to the authenticating user.
        /// </summary>
        /// <param name="tokens">The tokens.</param>
        /// <param name="options">The options.</param>
        /// <param name="timeout">The timeout.</param>
        /// <param name="function">The function.</param>
        /// <returns>
        /// A <see cref="TwitterDirectMessageCollection"/> instance.
        /// </returns>
        public static IAsyncResult DirectMessages(OAuthTokens tokens, DirectMessagesOptions options, TimeSpan timeout, Action<TwitterAsyncResponse<TwitterDirectMessageCollection>> function)
        {
            return AsyncUtility.ExecuteAsyncMethod(tokens, options, timeout, TwitterDirectMessage.DirectMessages, function);
        }

        /// <summary>
        /// Sends a new direct message to the specified user from the authenticating user.
        /// </summary>
        /// <param name="tokens">The tokens.</param>
        /// <param name="userId">The user id.</param>
        /// <param name="text">The text.</param>
        /// <param name="options">The options.</param>
        /// <param name="timeout">The timeout.</param>
        /// <param name="function">The function.</param>
        /// <returns></returns>
        public static IAsyncResult Send(OAuthTokens tokens, decimal userId, string text, OptionalProperties options, TimeSpan timeout, Action<TwitterAsyncResponse<TwitterDirectMessage>> function)
        {
            Func<OAuthTokens, decimal, string, OptionalProperties, TwitterResponse<TwitterDirectMessage>> methodToCall = TwitterDirectMessage.Send;

            return methodToCall.BeginInvoke(
                tokens,
                userId,
                text,
                options,
                result => AsyncUtility.ThreeParamsCallback(result, timeout, methodToCall, function),
                null);
        }

        /// <summary>
        /// Sends a new direct message to the specified user from the authenticating user.
        /// </summary>
        /// <param name="tokens">The tokens.</param>
        /// <param name="screenName">The user's screen name.</param>
        /// <param name="text">The text.</param>
        /// <param name="options">The options.</param>
        /// <param name="timeout">The timeout.</param>
        /// <param name="function">The function.</param>
        /// <returns></returns>
        public static IAsyncResult Send(OAuthTokens tokens, string screenName, string text, OptionalProperties options, TimeSpan timeout, Action<TwitterAsyncResponse<TwitterDirectMessage>> function)
        {
            return AsyncUtility.ExecuteAsyncMethod(tokens, screenName, text, options, timeout, TwitterDirectMessage.Send, function);
        }
    }
}
