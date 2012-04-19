using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twitterizer.Models;

namespace Twitterizer
{
    /// <summary>
    /// With so many tweets from so many users, themes are bound to arise from the zeitgeist. The Trends methods allow you to explore what's trending on Twitter.
    /// </summary>
    public static class Trends
    {
        /// <summary>
        /// Gets the trends with the specified WOEID.
        /// </summary>
        /// <param name="tokens">The request tokens. Leave null for unauthenticated request.</param>
        /// <param name="WoeID">The WOEID.</param>
        /// <param name="options">The options. Leave null for defaults.</param>
        /// <returns>
        /// A collection of <see cref="Twitterizer.TwitterTrend"/> objects.
        /// </returns>
        public static async Task<TwitterResponse<TwitterTrendCollection>> TrendsAsync(int WoeID, OAuthTokens tokens = null, LocalTrendsOptions options = null)
        {
            return await Core.CommandPerformer.PerformAction(new Twitterizer.Commands.TrendsCommand(tokens, WoeID, options));
        }        

        /// <summary>
        /// Gets the locations where trends are available.
        /// </summary>   
        /// <param name="tokens">The request tokens. Leave null for unauthenticated request.</param>
        /// <param name="options">The options. Leave null for defaults.</param>
        /// <returns>
        /// A collection of <see cref="Twitterizer.TwitterTrendLocation"/> objects.
        /// </returns>
        public static async Task<TwitterResponse<TwitterTrendLocationCollection>> AvailableAsync(OAuthTokens tokens = null, AvailableTrendsOptions options = null)
        {
            return await Core.CommandPerformer.PerformAction(new Twitterizer.Commands.AvailableTrendsCommand(tokens, options));
        }        


        /// <summary>
        /// Gets the daily global trends
        /// </summary>
        /// <param name="tokens">The request tokens. Leave null for unauthenticated request.</param>
        /// <param name="options">The options. Leave null for defaults.</param>
        public static async Task<TwitterResponse<TwitterTrendDictionary>> DailyAsync(OAuthTokens tokens = null, TrendsOptions options = null)
        {
            return await Core.CommandPerformer.PerformAction(new Twitterizer.Commands.DailyTrendsCommand(tokens, options));
        }

        /// <summary>
        /// Gets the weekly global trends
        /// </summary>
        /// <param name="tokens">The request tokens. Leave null for unauthenticated request.</param>
        /// <param name="options">The options. Leave null for defaults.</param>
        public static async Task<TwitterResponse<TwitterTrendDictionary>> WeeklyAsync(OAuthTokens tokens = null, TrendsOptions options = null)
        {
            return await Core.CommandPerformer.PerformAction(new Twitterizer.Commands.WeeklyTrendsCommand(tokens, options));
        }        

    }
}
