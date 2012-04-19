using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twitterizer.Models;

namespace Twitterizer
{
    /// <summary>
    /// Categorical organization of users that others may be interested to follow.
    /// NOT YET IMPLEMENTED: GET/suggestions:slug/members
    /// </summary>
    public static class SuggestedUsers
    {
        /// <summary>
        /// Access to Twitter's suggested user list. This returns the list of suggested user categories. The category can be used in the users/suggestions/category endpoint to get the users in that category.
        /// </summary>
        /// <param name="tokens">The tokens.</param>
        /// <param name="options">The options.</param>
        /// <returns>A collection of categories without user data.</returns>
        public static async Task<TwitterResponse<TwitterUserCategoryCollection>> SuggestedUserCategoriesAsync(OAuthTokens tokens, OptionalProperties options = null)
        {
            return await Core.CommandPerformer.PerformAction(new Commands.SuggestedUserCategoriesCommand(tokens, options));
        }

        /// <summary>
        /// Access the users in a given category of the Twitter suggested user list.
        /// </summary>
        /// <param name="tokens">The tokens.</param>
        /// <param name="categorySlug">The category slug.</param>
        /// <param name="options">The options.</param>
        /// <returns></returns>
        /// <remarks>It is recommended that end clients cache this data for no more than one hour.</remarks>
        public static async Task<TwitterResponse<TwitterUserCategory>> GetSuggestedUsersAsync(OAuthTokens tokens, string categorySlug, OptionalProperties options = null)
        {
            return await Core.CommandPerformer.PerformAction(new Commands.SuggestedUsersCommand(tokens, categorySlug, options));
        }        
    }
}
