using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twitterizer.Models;

namespace Twitterizer
{
    /// <summary>
    /// Timelines are collections of Tweets, ordered with the most recent first.
    /// NOT YET IMPLEMENTED: GET/retweet_to_user, GET/retweet_by_user
    /// </summary>
    public static class Timelines
    {
        /// <overloads>
        /// Returns the 20 most recent statuses, including retweets, posted by the authenticating user and that user's friends. This is the equivalent of /timeline/home on the Web.
        /// </overloads>
        /// <param name="tokens">The tokens.</param>
        /// <param name="options">The options. Leave null for defaults.</param>
        /// <returns>A collection of <see cref="Status"/> items.</returns>
        public static async Task<TwitterResponse<TwitterStatusCollection>> HomeTimelineAsync(OAuthTokens tokens, TimelineOptions options = null)
        {
            return await Core.CommandPerformer.PerformAction(new Commands.HomeTimelineCommand(tokens, options));
        }

        /// <summary>
        /// Returns the 20 most recent mentions (status containing @username) for the authenticating user.
        /// </summary>
        /// <param name="tokens">The tokens.</param>
        /// <param name="options">The options. Leave null for defaults.</param>
        /// <returns>A <see cref="TwitterStatusCollection"/> instance.</returns>
        public static async Task<TwitterResponse<TwitterStatusCollection>> MentionsAsync(OAuthTokens tokens, TimelineOptions options = null)
        {            
            return await Core.CommandPerformer.PerformAction(new Commands.MentionsCommand(tokens, options));
        }

        /// <summary>
        /// Returns the 20 most recent retweets posted by the authenticating user.
        /// </summary>
        /// <param name="tokens">The tokens.</param>
        /// <param name="options">The options. Leave null for defaults.</param>
        /// <returns>A <see cref="TwitterStatusCollection"/> instance.</returns>
        public static async Task<TwitterResponse<TwitterStatusCollection>> RetweetedByMeAsync(OAuthTokens tokens, TimelineOptions options = null)
        {
            return await Core.CommandPerformer.PerformAction(new Commands.RetweetedByMeCommand(tokens, options));
        }

        /// <summary>
        /// Returns the 20 most recent retweets posted by the authenticating user's friends.
        /// </summary>
        /// <param name="tokens">The tokens.</param>
        /// <param name="options">The options. Leave null for defaults.</param>
        /// <returns>A <see cref="TwitterStatusCollection"/> instance.</returns>
        public static async Task<TwitterResponse<TwitterStatusCollection>> RetweetedToMeAsync(OAuthTokens tokens, TimelineOptions options = null)
        {
            return await Core.CommandPerformer.PerformAction(new Commands.RetweetedToMeCommand(tokens, options));
        }

        /// <summary>
        /// Returns the 20 most recent tweets of the authenticated user that have been retweeted by others.
        /// </summary>
        /// <param name="tokens">The tokens.</param>
        /// <param name="options">The options. Leave null for defaults.</param>
        /// <returns>A <see cref="TwitterStatusCollection"/> instance.</returns>
        public static async Task<TwitterResponse<TwitterStatusCollection>> RetweetsOfMeAsync(OAuthTokens tokens, RetweetsOfMeOptions options)
        {
            return await Core.CommandPerformer.PerformAction(new Commands.RetweetsOfMeCommand(tokens, options));
        }

        /// <summary>
        /// Returns the 20 most recent statuses posted by the authenticating user. It is also possible to request another user's timeline by using the screen_name or user_id parameter.
        /// </summary>
        /// <param name="tokens">The oauth tokens. Leave null for an unauthenticated request.</param>
        /// <param name="options">The options. Leave null for defaults.</param>
        /// <returns>
        /// A <see cref="TwitterStatusCollection"/> instance.
        /// </returns>
        public static async Task<TwitterResponse<TwitterStatusCollection>> UserTimelineAsync(OAuthTokens tokens = null, UserTimelineOptions options = null)
        {
            return await Core.CommandPerformer.PerformAction(new Commands.UserTimelineCommand(tokens, options));
        }                  
    }
}
