using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twitterizer.Models;

namespace Twitterizer
{
    /// <summary>
    /// Users are at the center of everything Twitter: they follow, they favorite, and tweet & retweet.
    /// NOT YET IMPLEMENTED: GET/profile_image/:screenname, GET/contributees, GET/contributors
    /// </summary>
    public static class Users
    {
        /// <summary>
        /// Return up to 100 users worth of extended information, specified by either ID, screen name, or combination of the two.
        /// </summary>
        /// <param name="tokens">The tokens.</param>
        /// <param name="options">The options.</param>
        /// <returns></returns>
        public static async Task<TwitterResponse<TwitterUserCollection>> LookupAsync(OAuthTokens tokens, LookupUsersOptions options)
        {
            return await Core.CommandPerformer.PerformAction(new Commands.LookupUsersCommand(tokens, options));
        }

        /// <include file='TwitterUser.xml' path='TwitterUser/Search[@name="Common"]/*'/>
        /// <include file='TwitterUser.xml' path='TwitterUser/Search[@name="WithTokensAndOptions"]/*'/>
        public static async Task<TwitterResponse<TwitterUserCollection>> SearchAsync(string query, OAuthTokens tokens = null, UserSearchOptions options = null)
        {
            return await Core.CommandPerformer.PerformAction(new Commands.UserSearchCommand(tokens, query, options));
        } 
        
        /// <include file='TwitterUser.xml' path='TwitterUser/Show[@name="Common"]/*'/>
        /// <include file='TwitterUser.xml' path='TwitterUser/Show[@name="ByIDWithTokensAndOptions"]/*'/>
        public static async Task<TwitterResponse<TwitterUser>> ShowAsync(long id, OAuthTokens tokens = null, OptionalProperties options = null)
        {
            return await Core.CommandPerformer.PerformAction(new Commands.ShowUserCommand(tokens, id, string.Empty, options));
        }        

        /// <include file='TwitterUser.xml' path='TwitterUser/Show[@name="Common"]/*'/>
        /// <include file='TwitterUser.xml' path='TwitterUser/Show[@name="ByUsernameWithTokensAndOptions"]/*'/>
        public static async Task<TwitterResponse<TwitterUser>> ShowAsync(string username, OAuthTokens tokens = null, OptionalProperties options = null)
        {
            return await Core.CommandPerformer.PerformAction(new Commands.ShowUserCommand(tokens, 0, username, options));
        }                
    }
}
