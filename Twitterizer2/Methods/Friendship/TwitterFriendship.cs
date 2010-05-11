//-----------------------------------------------------------------------
// <copyright file="TwitterFriendship.cs" company="Patrick 'Ricky' Smith">
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
// <summary>The TwitterFriendship class.</summary>
//-----------------------------------------------------------------------

namespace Twitterizer
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// Provides interaction with the Twitter API to obtain and manage relationships between users.
    /// </summary>
    public static class TwitterFriendship
    {
        /// <summary>
        /// Gets the followers.
        /// </summary>
        /// <param name="tokens">The tokens.</param>
        /// <returns>
        /// A <see cref="TwitterStatusCollection"/> instance.
        /// </returns>
        public static TwitterUserCollection Followers(OAuthTokens tokens)
        {
            return Followers(tokens, 0);
        }

        /// <summary>
        /// Gets the followers.
        /// </summary>
        /// <param name="tokens">The tokens.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>
        /// A <see cref="TwitterStatusCollection"/> instance.
        /// </returns>
        public static TwitterUserCollection Followers(OAuthTokens tokens, decimal userId)
        {
            Commands.FollowersCommand command = new Commands.FollowersCommand(tokens)
            {
                UserId = userId
            };

            TwitterUserWrapper resultWrapper = Core.CommandPerformer<TwitterUserWrapper>.PerformAction(command);
            TwitterUserCollection result = resultWrapper.Users;

            result.CursorPagedCommand = command;

            return result;
        }

        /// <summary>
        /// Returns a user's friends, each with current status inline. They are ordered by the order in which the user followed them, most recently followed first, 100 at a time.
        /// </summary>
        /// <param name="tokens">The tokens.</param>
        /// <param name="userId">The user id.</param>
        /// <param name="screenName">Name of the screen.</param>
        /// <returns>
        /// A <see cref="TwitterUserCollection"/> instance.
        /// </returns>
        /// <remarks>Please note that the result set isn't guaranteed to be 100 every time as suspended users will be filtered out.</remarks>
        public static TwitterUserCollection Friends(OAuthTokens tokens, decimal userId, string screenName)
        {
            Commands.FriendsCommand command = new Commands.FriendsCommand(tokens)
            {
                UserId = userId,
                ScreenName = screenName
            };

            TwitterUserCollection result = Core.CommandPerformer<TwitterUserWrapper>.PerformAction(command).Users;
            result.CursorPagedCommand = command;

            return result;
        }

        /// <summary>
        /// Returns a user's friends, each with current status inline. They are ordered by the order in which the user followed them, most recently followed first, 100 at a time.
        /// </summary>
        /// <param name="tokens">The tokens.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>
        /// A <see cref="TwitterUserCollection"/> instance.
        /// </returns>
        /// <remarks>Please note that the result set isn't guaranteed to be 100 every time as suspended users will be filtered out.</remarks>
        public static TwitterUserCollection Friends(OAuthTokens tokens, decimal userId)
        {
            return Friends(tokens, userId, string.Empty);
        }

        /// <summary>
        /// Returns a user's friends, each with current status inline. They are ordered by the order in which the user followed them, most recently followed first, 100 at a time.
        /// </summary>
        /// <param name="tokens">The tokens.</param>
        /// <param name="screenName">Name of the screen.</param>
        /// <returns>
        /// A <see cref="TwitterUserCollection"/> instance.
        /// </returns>
        /// <remarks>Please note that the result set isn't guaranteed to be 100 every time as suspended users will be filtered out.</remarks>
        public static TwitterUserCollection Friends(OAuthTokens tokens, string screenName)
        {
            return Friends(tokens, 0, screenName);
        }

        /// <summary>
        /// Returns a user's friends, each with current status inline. They are ordered by the order in which the user followed them, most recently followed first, 100 at a time.
        /// </summary>
        /// <param name="tokens">The tokens.</param>
        /// <returns>
        /// A <see cref="TwitterUserCollection"/> instance.
        /// </returns>
        /// <remarks>Please note that the result set isn't guaranteed to be 100 every time as suspended users will be filtered out.</remarks>
        public static TwitterUserCollection Friends(OAuthTokens tokens)
        {
            return Friends(tokens, 0, string.Empty);
        }

        /// <summary>
        /// Allows the authenticating users to unfollow the user specified in the ID parameter.
        /// </summary>
        /// <param name="tokens">The tokens.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>
        /// Returns the unfollowed user in the requested format when successful.
        /// </returns>
        public static TwitterUser Delete(OAuthTokens tokens, decimal userId)
        {
            Commands.DeleteFriendshipCommand command = new Commands.DeleteFriendshipCommand(tokens)
            {
                UserId = userId
            };

            TwitterUser result = Core.CommandPerformer<TwitterUser>.PerformAction(command);

            return result;
        }

        /// <summary>
        /// Allows the authenticating users to unfollow the user specified in the ID parameter.
        /// </summary>
        /// <param name="tokens">The tokens.</param>
        /// <param name="username">The username.</param>
        /// <returns>
        /// Returns the unfollowed user in the requested format when successful.
        /// </returns>
        public static TwitterUser Delete(OAuthTokens tokens, string username)
        {
            Commands.DeleteFriendshipCommand command = new Commands.DeleteFriendshipCommand(tokens)
            {
                Username = username
            };

            TwitterUser result = Core.CommandPerformer<TwitterUser>.PerformAction(command);

            return result;
        }

        /// <summary>
        /// Gets the friendship.
        /// </summary>
        /// <param name="tokens">The tokens.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>A <see cref="TwitterRelationship"/> instance.</returns>
        public static TwitterRelationship Show(OAuthTokens tokens, decimal userId)
        {
            Commands.ShowFriendshipCommand command = new Twitterizer.Commands.ShowFriendshipCommand(tokens)
            {
                TargetId = userId
            };

            return Core.CommandPerformer<TwitterRelationship>.PerformAction(command);
        }

        /// <summary>
        /// Gets the friendship between two users.
        /// </summary>
        /// <param name="userId1">The first user id.</param>
        /// <param name="userId2">The second user id.</param>
        /// <returns>
        /// A <see cref="TwitterRelationship"/> instance.
        /// </returns>
        public static TwitterRelationship Show(decimal userId1, decimal userId2)
        {
            Commands.ShowFriendshipCommand command = new Twitterizer.Commands.ShowFriendshipCommand(null)
            {
                SourceId = userId1,
                TargetId = userId2
            };

            return Core.CommandPerformer<TwitterRelationship>.PerformAction(command);
        }

        /// <summary>
        /// Gets the friendship between two users.
        /// </summary>
        /// <param name="username1">The first username.</param>
        /// <param name="username2">The second username.</param>
        /// <returns>
        /// A <see cref="TwitterRelationship"/> instance.
        /// </returns>
        public static TwitterRelationship Show(string username1, string username2)
        {
            Commands.ShowFriendshipCommand command = new Twitterizer.Commands.ShowFriendshipCommand(null)
            {
                SourceScreenName = username1,
                TargetScreenName = username2
            };

            return Core.CommandPerformer<TwitterRelationship>.PerformAction(command);
        } 
    }
}
