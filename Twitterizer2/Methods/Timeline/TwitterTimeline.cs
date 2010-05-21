//-----------------------------------------------------------------------
// <copyright file="TwitterTimeline.cs" company="Patrick 'Ricky' Smith">
//  This file is part of the Twitterizer library (http://code.google.com/p/twitterizer/)
// 
//  Copyright (c) 2010, Patrick "Ricky" Smith (ricky@digitally-born.com)
//  All rights reserved.
//  
//  Redistribution and use in source and binary forms, with or without modification, are 
//  permitted provided that the following conditions are met:
// 
//  - Redistributions of source code must retain the above copyright notice, this list 
//    of conditions and the following disclaimer.
//  - Redistributions in binary form must reproduce the above copyright notice, this list 
//    of conditions and the following disclaimer in the documentation and/or other 
//    materials provided with the distribution.
//  - Neither the name of the Twitterizer nor the names of its contributors may be 
//    used to endorse or promote products derived from this software without specific 
//    prior written permission.
// 
//  THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND 
//  ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED 
//  WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. 
//  IN NO EVENT SHALL THE COPYRIGHT OWNER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, 
//  INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT 
//  NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR 
//  PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, 
//  WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) 
//  ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE 
//  POSSIBILITY OF SUCH DAMAGE.
// </copyright>
// <author>Ricky Smith</author>
// <summary>The TwitterTimeline class</summary>
//-----------------------------------------------------------------------

namespace Twitterizer
{
    using System;
    using Twitterizer.Core;

    /// <summary>
    /// Provides interaction with timelines
    /// </summary>
    public static class TwitterTimeline
    {
        /// <summary>
        /// Gets the home timeline.
        /// </summary>
        /// <param name="tokens">The tokens.</param>
        /// <returns>A <see cref="TwitterStatusCollection"/> instance.</returns>
        public static TwitterStatusCollection HomeTimeline(OAuthTokens tokens)
        {
            return HomeTimeline(tokens, null);
        }

        /// <summary>
        /// Gets the home timeline.
        /// </summary>
        /// <param name="tokens">The tokens.</param>
        /// <param name="options">The options.</param>
        /// <returns>
        /// A <see cref="TwitterStatusCollection"/> object.
        /// </returns>
        public static TwitterStatusCollection HomeTimeline(OAuthTokens tokens, TimelineOptions options)
        {
            Commands.HomeTimelineCommand command = new Commands.HomeTimelineCommand(tokens, options);

            TwitterStatusCollection result = Core.CommandPerformer<TwitterStatusCollection>.PerformAction(command);

            if (result != null)
            {
                result.Command = command;
            }

            return result;
        }

        /// <summary>
        /// Gets the user time line.
        /// </summary>
        /// <param name="tokens">The tokens.</param>
        /// <returns>
        /// A <see cref="TwitterStatusCollection"/> instance.
        /// </returns>
        public static TwitterStatusCollection UserTimeline(OAuthTokens tokens)
        {
            return UserTimeline(tokens, null);
        }

        /// <summary>
        /// Gets the user time line.
        /// </summary>
        /// <param name="tokens">The tokens.</param>
        /// <param name="options">The options.</param>
        /// <returns>
        /// A <see cref="TwitterStatusCollection"/> instance.
        /// </returns>
        public static TwitterStatusCollection UserTimeline(
            OAuthTokens tokens,
            TimelineOptions options)
        {
            return UserTimeline(tokens, 0, string.Empty, string.Empty, options);
        }

        /// <summary>
        /// Gets the user time line.
        /// </summary>
        /// <param name="tokens">The oauth tokens.</param>
        /// <param name="userId">The user id.</param>
        /// <param name="screenName">Name of the screen.</param>
        /// <param name="userIdOrScreenName">Name of the user id or screen.</param>
        /// <returns>
        /// A <see cref="TwitterStatusCollection"/> instance.
        /// </returns>
        public static TwitterStatusCollection UserTimeline(
            OAuthTokens tokens,
            decimal userId,
            string screenName,
            string userIdOrScreenName)
        {
            return UserTimeline(tokens, userId, screenName, userIdOrScreenName, null);
        }

        /// <summary>
        /// Gets the user time line.
        /// </summary>
        /// <param name="tokens">The oauth tokens.</param>
        /// <param name="userId">The user id.</param>
        /// <param name="screenName">Name of the screen.</param>
        /// <param name="userIdOrScreenName">Name of the user id or screen.</param>
        /// <param name="options">The options.</param>
        /// <returns>
        /// A <see cref="TwitterStatusCollection"/> instance.
        /// </returns>
        public static TwitterStatusCollection UserTimeline(
            OAuthTokens tokens, 
            decimal userId,
            string screenName,
            string userIdOrScreenName,
            TimelineOptions options)
        {
            Commands.UserTimelineCommand command = new Commands.UserTimelineCommand(tokens, userIdOrScreenName, userId, screenName, options);
            
            TwitterStatusCollection result = Core.CommandPerformer<TwitterStatusCollection>.PerformAction(command);
            result.Command = command;

            return result;
        }

        /// <summary>
        /// Gets the public timeline.
        /// </summary>
        /// <returns>A <see cref="TwitterStatusCollection"/>.</returns>
        public static TwitterStatusCollection PublicTimeline()
        {
            return PublicTimeline(null);
        }

        /// <summary>
        /// Gets the public timeline.
        /// </summary>
        /// <param name="tokens">The oauth tokens.</param>
        /// <returns>
        /// A <see cref="TwitterStatusCollection"/>.
        /// </returns>
        public static TwitterStatusCollection PublicTimeline(OAuthTokens tokens)
        {
            Commands.PublicTimelineCommand command = new Commands.PublicTimelineCommand(tokens);

            return Core.CommandPerformer<TwitterStatusCollection>.PerformAction(command);
        }

        /// <summary>
        /// Obtains the authorized user's friends timeline.
        /// </summary>
        /// <param name="tokens">The tokens.</param>
        /// <returns>A <see cref="TwitterStatusCollection"/>.</returns>
        public static TwitterStatusCollection FriendTimeline(OAuthTokens tokens)
        {
            return FriendTimeline(tokens, null);
        }

        /// <summary>
        /// Obtains the authorized user's friends timeline.
        /// </summary>
        /// <param name="tokens">The tokens.</param>
        /// <param name="options">The options.</param>
        /// <returns>A <see cref="TwitterStatusCollection"/>.</returns>
        public static TwitterStatusCollection FriendTimeline(OAuthTokens tokens, TimelineOptions options)
        {
            return CommandPerformer<TwitterStatusCollection>.PerformAction(new Commands.FriendsTimelineCommand(tokens, options));
        }

        /// <summary>
        /// Returns the 20 most recent tweets of the authenticated user that have been retweeted by others.
        /// </summary>
        /// <param name="tokens">The tokens.</param>
        /// <param name="options">The options.</param>
        /// <returns>A <see cref="TwitterStatusCollection"/> instance.</returns>
        public static TwitterStatusCollection RetweetsOfMe(OAuthTokens tokens, RetweetsOfMeOptions options)
        {
            return CommandPerformer<TwitterStatusCollection>.PerformAction(
                new Commands.RetweetsOfMeCommand(tokens, options));
        }

        /// <summary>
        /// Returns the 20 most recent tweets of the authenticated user that have been retweeted by others.
        /// </summary>
        /// <param name="tokens">The tokens.</param>
        /// <returns>
        /// A <see cref="TwitterStatusCollection"/> instance.
        /// </returns>
        public static TwitterStatusCollection RetweetsOfMe(OAuthTokens tokens)
        {
            return RetweetsOfMe(tokens, null);
        }

        /// <summary>
        /// Returns the 20 most recent retweets posted by the authenticating user.
        /// </summary>
        /// <param name="tokens">The tokens.</param>
        /// <param name="options">The options.</param>
        /// <returns>A <see cref="TwitterStatusCollection"/> instance.</returns>
        public static TwitterStatusCollection RetweetedByMe(OAuthTokens tokens, TimelineOptions options)
        {
            return CommandPerformer<TwitterStatusCollection>.PerformAction(
                new Commands.RetweetedByMeCommand(tokens, options));
        }

        /// <summary>
        /// Returns the 20 most recent retweets posted by the authenticating user.
        /// </summary>
        /// <param name="tokens">The tokens.</param>
        /// <returns>
        /// A <see cref="TwitterStatusCollection"/> instance.
        /// </returns>
        public static TwitterStatusCollection RetweetedByMe(OAuthTokens tokens)
        {
            return RetweetedByMe(tokens, null);
        }

        /// <summary>
        /// Returns the 20 most recent retweets posted by the authenticating user's friends.
        /// </summary>
        /// <param name="tokens">The tokens.</param>
        /// <param name="options">The options.</param>
        /// <returns>A <see cref="TwitterStatusCollection"/> instance.</returns>
        public static TwitterStatusCollection RetweetedToMe(OAuthTokens tokens, TimelineOptions options)
        {
            return CommandPerformer<TwitterStatusCollection>.PerformAction(
                new Commands.RetweetedToMeCommand(tokens, options));
        }

        /// <summary>
        /// Returns the 20 most recent retweets posted by the authenticating user's friends.
        /// </summary>
        /// <param name="tokens">The tokens.</param>
        /// <returns>
        /// A <see cref="TwitterStatusCollection"/> instance.
        /// </returns>
        public static TwitterStatusCollection RetweetedToMe(OAuthTokens tokens)
        {
            return RetweetedToMe(tokens, null);
        }

        /// <summary>
        /// Returns the 20 most recent mentions (status containing @username) for the authenticating user.
        /// </summary>
        /// <param name="tokens">The tokens.</param>
        /// <param name="options">The options.</param>
        /// <returns>A <see cref="TwitterStatusCollection"/> instance.</returns>
        public static TwitterStatusCollection Mentions(OAuthTokens tokens, TimelineOptions options)
        {
            return CommandPerformer<TwitterStatusCollection>.PerformAction(
                new Commands.MentionsCommand(tokens, options));
        }

        /// <summary>
        /// Returns the 20 most recent mentions (status containing @username) for the authenticating user.
        /// </summary>
        /// <param name="tokens">The tokens.</param>
        /// <returns>
        /// A <see cref="TwitterStatusCollection"/> instance.
        /// </returns>
        public static TwitterStatusCollection Mentions(OAuthTokens tokens)
        {
            return Mentions(tokens, null);
        }
    }
}
