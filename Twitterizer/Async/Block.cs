using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twitterizer.Models;

namespace Twitterizer
{
    /// <summary>
    /// Allows users to block and unblock other users.
    /// </summary>
    public static class Block
    {
        /// <summary>
        /// Blocks the user specified as the authenticating user. Destroys a friendship to the blocked user if it exists.
        /// </summary>
        /// <param name="tokens">The tokens.</param>
        /// <param name="userId">The user id.</param>
        /// <param name="options">The options. Leave null for defaults.</param>
        /// <returns>
        /// The blocked user in the requested format when successful.
        /// </returns>
        public static async Task<TwitterResponse<User>> CreateAsync(OAuthTokens tokens, decimal userId, OptionalProperties options = null)
        {
            return await Core.CommandPerformer.PerformAction(new Commands.CreateBlockCommand(tokens, string.Empty, userId, options));
        }

        /// <summary>
        /// Blocks the user specified as the authenticating user. Destroys a friendship to the blocked user if it exists.
        /// </summary>
        /// <param name="tokens">The tokens.</param>
        /// <param name="screenName">The user's screen name.</param>
        /// <param name="options">The options. Leave null for defaults.</param>
        /// <returns>
        /// The blocked user in the requested format when successful.
        /// </returns>
        public static async Task<TwitterResponse<User>> CreateAsync(OAuthTokens tokens, string screenName, OptionalProperties options = null)
        {
            return await Core.CommandPerformer.PerformAction(new Commands.CreateBlockCommand(tokens, screenName, -1, options));
        }

        /// <summary>
        /// Unblocks the user specified as the authenticating user.
        /// </summary>
        /// <param name="tokens">The tokens.</param>
        /// <param name="userId">The user id.</param>
        /// <param name="options">The options. Leave null for defaults.</param>
        /// <returns>
        /// The unblocked user in the requested format when successful.
        /// </returns>
        public static async Task<TwitterResponse<User>> DestroyAsync(OAuthTokens tokens, decimal userId, OptionalProperties options = null)
        {
            return await Core.CommandPerformer.PerformAction(new Commands.DestroyBlockCommand(tokens, string.Empty, userId, options));
        }

        /// <summary>
        /// Unblocks the user specified as the authenticating user.
        /// </summary>
        /// <param name="tokens">The tokens.</param>
        /// <param name="screenName">The user's screen name.</param>
        /// <param name="options">The options. Leave null for defaults.</param>
        /// <returns>
        /// The unblocked user in the requested format when successful.
        /// </returns>
        public static async Task<TwitterResponse<User>> DestroyAsync(OAuthTokens tokens, string screenName, OptionalProperties options = null)
        {
            return await Core.CommandPerformer.PerformAction(new Commands.DestroyBlockCommand(tokens, screenName, -1, options));
        }

        /// <summary>
        /// Checks for a block against the the user specified as the authenticating user.
        /// </summary>
        /// <param name="tokens">The tokens.</param>
        /// <param name="userId">The user id.</param>
        /// <param name="options">The options. Leave null for defaults.</param>
        /// <returns>
        /// The blocked user in the requested format when successful.
        /// </returns>
        public static async Task<TwitterResponse<User>> ExistsAsync(OAuthTokens tokens, decimal userId, OptionalProperties options = null)
        {
            return await Core.CommandPerformer.PerformAction(new Commands.ExistsBlockCommand(tokens, string.Empty, userId, options));
        }

        /// <summary>
        /// Checks for a block against the the user specified as the authenticating user.
        /// </summary>
        /// <param name="tokens">The tokens.</param>
        /// <param name="screenName">The user's screen name.</param>
        /// <param name="options">The options. Leave null for defaults.</param>
        /// <returns>
        /// The blocked user in the requested format when successful.
        /// </returns>
        public static async Task<TwitterResponse<User>> ExistsAsync(OAuthTokens tokens, string screenName, OptionalProperties options = null)
        {
            return await Core.CommandPerformer.PerformAction(new Commands.ExistsBlockCommand(tokens, screenName, -1, options));
        }

        /// <summary>
        /// Returns a collection of user objects that the authenticating user is blocking.
        /// </summary>
        /// <param name="tokens">The tokens.</param>
        /// <param name="options">The options. Leave null for defaults.</param>
        /// <returns></returns>
        public static async Task<TwitterResponse<TwitterUserCollection>> BlockingAsync(OAuthTokens tokens, BlockingOptions options = null)
        {
            return await Core.CommandPerformer.PerformAction(new Commands.BlockingCommand(tokens, options));
        }        
    }
}
