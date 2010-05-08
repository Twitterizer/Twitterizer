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
        public static TwitterStatusCollection HomeTimeline(OAuthTokens tokens, HomeTimelineOptions options)
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
            return UserTimeline(
                tokens,
                0,
                string.Empty,
                0,
                0,
                -1,
                -1);
        }

        /// <summary>
        /// Gets the user time line.
        /// </summary>
        /// <param name="tokens">The oauth tokens.</param>
        /// <param name="userId">The user id.</param>
        /// <param name="screenName">Name of the screen.</param>
        /// <param name="sinceId">The min status id.</param>
        /// <param name="maxId">The max status id.</param>
        /// <param name="count">The number of statuses to return.</param>
        /// <param name="page">The page number.</param>
        /// <returns>
        /// A <see cref="TwitterStatusCollection"/> instance.
        /// </returns>
        [CLSCompliant(false)]
        public static TwitterStatusCollection UserTimeline(OAuthTokens tokens, ulong userId, string screenName, ulong sinceId, ulong maxId, int count, int page)
        {
            Commands.UserTimelineCommand command = new Commands.UserTimelineCommand(tokens)
            {
                UserId = userId,
                ScreenName = screenName,
                Count = count,
                MaxId = maxId,
                Page = page,
                SinceId = sinceId
            };

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

            command.Validate();
            if (!command.IsValid)
            {
                throw new CommandValidationException<TwitterStatusCollection>()
                {
                    Command = command,
                    MethodName = "GetPublicTimeline"
                };
            }

            return Core.CommandPerformer<TwitterStatusCollection>.PerformAction(command);
        } 
    }
}
