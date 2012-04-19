using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twitterizer.Models;

namespace Twitterizer
{
    /// <summary>
    /// Allows users to save references to search criteria for reuse later.
    /// NOT YET IMPLEMENTED: GET/show:id
    /// </summary>
    public static class SavedSearches
    {
        /// <summary>
        /// Creates the saved search specified in the query parameter as the authenticating user.
        /// </summary>
        /// <param name="tokens">The tokens.</param>
        /// <param name="query">The query.</param>
        /// <param name="options">The options. Leave null for defaults.</param>
        /// <returns>The saved search when successful.</returns>
        public static async Task<TwitterResponse<TwitterSavedSearch>> CreateAsync(string query, OAuthTokens tokens, OptionalProperties options = null)
        {
            return await Core.CommandPerformer.PerformAction(new Commands.CreateSavedSearchCommand(tokens, query, options));
        }

        /// <summary>
        /// Deletes the saved search specified in the ID parameter as the authenticating user.
        /// </summary>
        /// <param name="tokens">The tokens.</param>
        /// <param name="savedsearchId">The saved search id.</param>
        /// <param name="options">The options. Leave null for defaults.</param>
        /// <returns>The deleted saved search in the requested format when successful.</returns>
        public static async Task<TwitterResponse<TwitterSavedSearch>> DeleteAsync(decimal savedsearchId, OAuthTokens tokens, OptionalProperties options = null)
        {
            return await Core.CommandPerformer.PerformAction(new Commands.DeleteSavedSearchCommand(tokens, savedsearchId, options));
        }

        /// <summary>
        /// Returns the the authenticating user's saved search queries in the requested format.
        /// </summary>
        /// <param name="tokens">The tokens.</param>
        /// <param name="options">The options. Leave null for defaults.</param>
        /// <returns>The saved searches</returns>
        public static async Task<TwitterResponse<TwitterSavedSearchCollection>> SavedSearchesAsync(OAuthTokens tokens, OptionalProperties options = null)
        {
            return await Core.CommandPerformer.PerformAction(new Commands.SavedSearchesCommand(tokens, options));
        }        
    }
}
