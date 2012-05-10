using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twitterizer.Models;

namespace Twitterizer
{
    /// <summary>
    /// Tweets are the atomic building blocks of Twitter, 140-character status updates with additional associated metadata. People tweet for a variety of reasons about a multitude of topics.
    /// NOT YET IMPLEMENTED: GET/oembed
    /// </summary>
    public static class Tweets
    {
        /// <summary>
        /// Show user objects of up to 100 members who retweeted the status.
        /// </summary>
        /// <param name="tokens">The tokens.</param>
        /// <param name="statusId">The status id.</param>
        /// <param name="options">The options. Leave null for defaults.</param>
        /// <returns>A collection of user objects.</returns>
        public static async Task<TwitterResponse<TwitterUserCollection>> RetweetedByAsync(decimal statusId, OAuthTokens tokens, RetweetedByOptions options = null)
        {
            return await Core.CommandPerformer.PerformAction(new Commands.RetweetedByCommand(tokens, statusId, options));
        }        

        /// <summary>
        /// Show user ids of up to 100 members who retweeted the status.
        /// </summary>
        /// <param name="tokens">The tokens.</param>
        /// <param name="statusId">The status id.</param>
        /// <param name="options">The options. Leave null for defaults.</param>
        /// <returns>A collection of user ids.</returns>
        public static async Task<TwitterResponse<UserIdCollection>> RetweetedByIdsAsync(decimal statusId, OAuthTokens tokens, RetweetedByIdsOptions options = null)
        {
            return await Core.CommandPerformer.PerformAction(new Commands.RetweetedByIdsCommand(tokens, statusId, options));
        }        

        /// <summary>
        /// Returns up to 100 of the first retweets of a given tweet.
        /// </summary>
        /// <param name="tokens">The tokens.</param>
        /// <param name="statusId">The status id.</param>
        /// <param name="options">The options. Leave null for defaults.</param>
        /// <returns>
        /// A <see cref="TwitterStatusCollection"/> instance.
        /// </returns>
        public static async Task<TwitterResponse<TwitterStatusCollection>> RetweetsAsync(decimal statusId, OAuthTokens tokens, RetweetsOptions options = null)
        {
            return await Core.CommandPerformer.PerformAction(new Commands.RetweetsCommand(tokens, statusId, options));
        }

        /// <summary>
        /// Returns a single status, with user information, specified by the id parameter.
        /// </summary>
        /// <param name="tokens">The tokens.</param>
        /// <param name="statusId">The status id. Leave null for unauthenticated request.</param>
        /// <param name="options">The options. Leave null for defaults.</param>
        /// <returns>A <see cref="Status"/> instance.</returns>
        public static async Task<TwitterResponse<Status>> ShowAsync(decimal statusId, OAuthTokens tokens = null, OptionalProperties options = null)
        {
            return await Core.CommandPerformer.PerformAction(new Commands.ShowStatusCommand(tokens, statusId, options));
        }

        /// <summary>
        /// Deletes the specified status.
        /// </summary>
        /// <param name="tokens">The oauth tokens.</param>
        /// <param name="id">The status id.</param>
        /// <param name="options">The options. Leave null for defaults.</param>
        /// <returns>
        /// A <see cref="Status"/> object of the deleted status.
        /// </returns>
        public static async Task<TwitterResponse<Status>> DeleteAsync(decimal id, OAuthTokens tokens, OptionalProperties options = null)
        {
            return await Core.CommandPerformer.PerformAction(new Twitterizer.Commands.DeleteStatusCommand(tokens, id, options));
        }

        /// <summary>
        /// Retweets a tweet. Requires the id parameter of the tweet you are retweeting. (say that 5 times fast)
        /// </summary>
        /// <param name="tokens">The tokens.</param>
        /// <param name="statusId">The status id.</param>
        /// <param name="options">The options. Leave null for defaults.</param>
        /// <returns>A <see cref="Status"/> representing the newly created tweet.</returns>
        public static async Task<TwitterResponse<Status>> RetweetAsync(decimal statusId, OAuthTokens tokens, OptionalProperties options = null)
        {
            return await Core.CommandPerformer.PerformAction(new Commands.RetweetCommand(tokens, statusId, options));
        }

        /// <summary>
        /// Updates the authenticating user's status. A status update with text identical to the authenticating user's text identical to the authenticating user's current status will be ignored to prevent duplicates.
        /// </summary>
        /// <param name="tokens">The tokens.</param>
        /// <param name="text">The status text.</param>
        /// <param name="options">The options. Leave null for defaults.</param>
        /// <returns>
        /// A <see cref="Status"/> object of the newly created status.
        /// </returns>
        public static async Task<TwitterResponse<Status>> UpdateAsync(string text, OAuthTokens tokens, StatusUpdateOptions options = null)
        {
            return await Core.CommandPerformer.PerformAction(new Commands.UpdateStatusCommand(tokens, text, options));
        }

        /// <summary>
        /// Updates the authenticating user's status. A status update with text identical to the authenticating user's text identical to the authenticating user's current status will be ignored to prevent duplicates.
        /// </summary>
        /// <param name="tokens">The tokens.</param>
        /// <param name="text">The status text.</param>
        /// <param name="fileData">The file to upload, as a byte array.</param>
        /// <param name="options">The options. Leave null for defaults.</param>
        /// <returns>
        /// A <see cref="Status"/> object of the newly created status.
        /// </returns>
        public static async Task<TwitterResponse<Status>> UpdateWithMediaAsync(string text, byte[] fileData, OAuthTokens tokens, StatusUpdateOptions options = null)
        {
            return await Core.CommandPerformer.PerformAction(new Commands.UpdateWithMediaCommand(tokens, text, fileData, options));
        }

        /// <summary>
        /// Shows Related Results of a tweet. Requires the id parameter of the tweet you are getting results for.
        /// </summary>
        /// <param name="tokens">The tokens.</param>
        /// <param name="statusId">The status id.</param>
        /// <param name="options">The options. Leave null for defaults.</param>
        /// <returns>A <see cref="Status"/> representing the newly created tweet.</returns>
        public static async Task<TwitterResponse<TwitterRelatedTweetsCollection>> RelatedResultsShowAsync(decimal statusId, OAuthTokens tokens, OptionalProperties options = null)
        {
            return await Core.CommandPerformer.PerformAction(new Commands.RelatedResultsCommand(tokens, statusId, options));
        }

    }
}
