using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twitterizer.Models;

namespace Twitterizer
{
    /// <summary>
    /// These methods are used to report user accounts as spam accounts.
    /// </summary>
    public static class Spam
    {
        /// <summary>
        /// Blocks the user and reports them for spam/abuse.
        /// </summary>
        /// <param name="tokens">The tokens.</param>
        /// <param name="userId">The user id.</param>
        /// <param name="options">The options. Leave null for defaults.</param>
        /// <returns>The user details.</returns>
        public static async Task<TwitterResponse<User>> ReportSpamAsync(OAuthTokens tokens, decimal userId, OptionalProperties options = null)
        {
            return await Core.CommandPerformer.PerformAction(new Commands.ReportSpamCommand(tokens, userId, string.Empty, options));
        }        

        /// <summary>
        /// Blocks the user and reports them for spam/abuse.
        /// </summary>
        /// <param name="tokens">The tokens.</param>
        /// <param name="screenName">The user's screen name.</param>
        /// <param name="options">The options. Leave null for defaults.</param>
        /// <returns>The user details.</returns>
        public static async Task<TwitterResponse<User>> ReportSpamAsync(OAuthTokens tokens, string screenName, OptionalProperties options = null)
        {
            return await Core.CommandPerformer.PerformAction(new Commands.ReportSpamCommand(tokens, 0, screenName, options));
        }        
    }
}
