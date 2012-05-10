using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twitterizer.Models;

namespace Twitterizer
{
    /// <summary>
    /// Users favorite tweets to give recognition to awesome tweets, to curate the best of Twitter, to save for reading later, and a variety of other reasons. 
    /// Likewise, developers make use of "favs" in many different ways.
    /// </summary>
    public static class Favourites
    {
        /// <summary>
        /// Favorites the status specified in the ID parameter as the authenticating user.
        /// </summary>
        /// <param name="tokens">The tokens.</param>
        /// <param name="statusId">The status id.</param>
        /// <param name="options">The options. Leave null for defaults.</param>
        /// <returns>The favorite status when successful.</returns>
        public static async Task<TwitterResponse<Status>> CreateAsync(OAuthTokens tokens, decimal statusId, OptionalProperties options = null)
        {
            return await Core.CommandPerformer.PerformAction(new Commands.CreateFavoriteCommand(tokens, statusId, options));
        }

        /// <summary>
        /// Un-favorites the status specified in the ID parameter as the authenticating user.
        /// </summary>
        /// <param name="tokens">The tokens.</param>
        /// <param name="statusId">The status id.</param>
        /// <param name="options">The options. Leave null for defaults.</param>
        /// <returns>The un-favorited status in the requested format when successful.</returns>
        public static async Task<TwitterResponse<Status>> DeleteAsync(OAuthTokens tokens, decimal statusId, OptionalProperties options = null)
        {
            return await Core.CommandPerformer.PerformAction(new Commands.DeleteFavoriteCommand(tokens, statusId, options));
        }

        /// <summary>
        /// Returns the 20 most recent favorite statuses for the authenticating user or user specified by the ID parameter in the requested format.
        /// </summary>
        /// <param name="tokens">The tokens.</param>
        /// <param name="options">The options. Leave null for defaults.</param>
        /// <returns>The 20 most recent favorite statuses</returns>
        public static async Task<TwitterResponse<TwitterStatusCollection>> ListAsync(OAuthTokens tokens, ListFavoritesOptions options = null)
        {
            return await Core.CommandPerformer.PerformAction(new Commands.ListFavoritesCommand(tokens, options));
        }        
    }
}
