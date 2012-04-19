using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twitterizer.Models;

namespace Twitterizer
{
    /// <summary>
    /// Direct Messages are short, non-public messages sent between two users. Access to Direct Messages is governed by the The Application Permission Model.
    /// </summary>
    public static class DirectMessages
    {
        /// <summary>
        /// Returns a list of the 20 most recent direct messages sent to the authenticating user.
        /// </summary>
        /// <param name="tokens">The tokens.</param>
        /// <param name="options">The options. Leave null for defaults.</param>
        /// <returns>
        /// A <see cref="TwitterDirectMessageCollection"/> instance.
        /// </returns>
        public static async Task<TwitterResponse<TwitterDirectMessageCollection>> ReceivedAsync(OAuthTokens tokens, DirectMessagesOptions options = null)
        {
            return await Core.CommandPerformer.PerformAction(new Commands.DirectMessagesCommand(tokens, options));
        }

        /// <summary>
        /// Returns a list of the 20 most recent direct messages sent by the authenticating user.
        /// </summary>
        /// <param name="tokens">The tokens.</param>
        /// <param name="options">The options. Leave null for defaults.</param>
        /// <returns>
        /// A <see cref="TwitterDirectMessageCollection"/> instance.
        /// </returns>
        public static async Task<TwitterResponse<TwitterDirectMessageCollection>> SentAsync(OAuthTokens tokens, DirectMessagesSentOptions options = null)
        {
            return await Core.CommandPerformer.PerformAction(new Commands.DirectMessagesSentCommand(tokens, options));
        }

        /// <summary>
        /// Deletes this direct message.
        /// </summary>
        /// <param name="tokens">The tokens.</param>
        /// <param name="id">The direct message id.</param>
        /// <param name="options">The options. Leave null for defaults.</param>
        /// <returns>
        /// A <see cref="TwitterDirectMessage"/> instance.
        /// </returns>
        public static async Task<TwitterResponse<TwitterDirectMessage>> DeleteAsync(decimal id, OAuthTokens tokens, OptionalProperties options = null)
        {
            return await Core.CommandPerformer.PerformAction(new Commands.DeleteDirectMessageCommand(tokens, id, options));
        }

        /// <summary>
        /// Sends a new direct message to the specified user from the authenticating user.
        /// </summary>
        /// <param name="tokens">The OAuth tokens.</param>
        /// <param name="userId">The user id.</param>
        /// <param name="text">The text of your direct message.</param>
        /// <param name="options">The options. Leave null for defaults.</param>
        /// <returns>
        /// A <see cref="TwitterDirectMessage"/> instance.
        /// </returns>
        public static async Task<TwitterResponse<TwitterDirectMessage>> SendAsync(decimal userId, string text, OAuthTokens tokens, OptionalProperties options = null)
        {
            return await Core.CommandPerformer.PerformAction(new Commands.SendDirectMessageCommand(tokens, text, userId, options));
        }

        /// <summary>
        /// Sends a new direct message to the specified user from the authenticating user.
        /// </summary>
        /// <param name="tokens">The OAuth tokens.</param>
        /// <param name="screenName">The user's screen name.</param>
        /// <param name="text">The message text.</param>
        /// <param name="options">The options. Leave null for defaults.</param>
        /// <returns>A <see cref="TwitterDirectMessage"/> object of the created direct message.</returns>
        public static async Task<TwitterResponse<TwitterDirectMessage>> SendAsync(string screenName, string text, OAuthTokens tokens, OptionalProperties options = null)
        {
            return await Core.CommandPerformer.PerformAction(new Commands.SendDirectMessageCommand(tokens, text, screenName, options));
        }

        /// <summary>
        /// Returns a single direct message, specified by an id parameter. Like the /1/direct_messages.format request, this method will include the user objects of the sender and recipient.
        /// </summary>
        /// <param name="tokens">The tokens.</param>
        /// <param name="id">The id.</param>
        /// <param name="options">The options. Leave null for defaults.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static async Task<TwitterResponse<TwitterDirectMessage>> ShowAsync(decimal id, OAuthTokens tokens, OptionalProperties options = null)
        {
            return await Core.CommandPerformer.PerformAction(new Commands.ShowDirectMessageCommand(tokens, id, options));
        }        
    }
}
