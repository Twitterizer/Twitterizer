//-----------------------------------------------------------------------
// <copyright file="TwitterFriendship.cs" company="Patrick 'Ricky' Smith">
//  This file is part of the Twitterizer library (http://www.twitterizer.net/)
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
    using System.Threading.Tasks;
    using Core;
    using Twitterizer.Models;

    /// <summary>
    /// Users follow their interests on Twitter through both one-way and mutual following relationships.
    /// NOT YET IMPLEMENTED: GET/exists, GET/lookup
    /// </summary>
    public static class Friendship
    {
        /// <summary>
        /// Returns the numeric IDs for every user the specified user is following.
        /// </summary>
        /// <param name="tokens">The tokens.</param>
        /// <param name="options">The options. Leave null for defaults.</param>
        /// <returns>
        /// A <see cref="TwitterListCollection"/> instance.
        /// </returns>
        public static async Task<TwitterResponse<UserIdCollection>> FollowersIdsAsync(OAuthTokens tokens, UsersIdsOptions options = null)
        {
            return await Core.CommandPerformer.PerformAction(new Commands.FollowersIdsCommand(tokens, options));
        }

        /// <summary>
        /// Returns the numeric IDs for every user the specified user is friends with.
        /// </summary>
        /// <param name="tokens">The tokens.</param>
        /// <param name="options">The options. Leave null for defaults.</param>
        /// <returns>
        /// A <see cref="TwitterListCollection"/> instance.
        /// </returns>
        public static async Task<TwitterResponse<UserIdCollection>> FriendsIdsAsync(OAuthTokens tokens, UsersIdsOptions options = null)
        {
            return await Core.CommandPerformer.PerformAction(new Commands.FriendsIdsCommand(tokens, options));
        }

        /// <summary>
        /// Returns a collection of IDs for every user who has a pending request to follow the authenticating user.
        /// </summary>
        /// <param name="tokens">The tokens.</param>
        /// <param name="options">The options.</param>
        /// <returns></returns>
        public static async Task<TwitterResponse<TwitterCursorPagedIdCollection>> IncomingRequestsAsync(OAuthTokens tokens, IncomingFriendshipsOptions options = null)
        {
            return await Core.CommandPerformer.PerformAction(new Commands.IncomingFriendshipsCommand(tokens, options));
        }

        /// <summary>
        /// Returns a collection of IDs for every protected user for whom the authenticating user has a pending follow request.
        /// </summary>
        /// <param name="tokens">The tokens.</param>
        /// <param name="options">The options.</param>
        /// <returns></returns>
        public static async Task<TwitterResponse<TwitterCursorPagedIdCollection>> OutgoingRequestsAsync(OAuthTokens tokens, OutgoingFriendshipsOptions options = null)
        {
            return await Core.CommandPerformer.PerformAction(new Commands.OutgoingFriendshipsCommand(tokens, options));
        }

        /// <summary>
        /// Returns detailed information about the relationship between two users.
        /// </summary>
        /// <param name="tokens">The tokens.</param>
        /// <param name="sourceUseId">The source user id.</param>
        /// <param name="targetUserId">The target user id.</param>
        /// <param name="options">The options.</param>
        /// <returns>A <see cref="TwitterRelationship"/> instance.</returns>
        public static async Task<TwitterResponse<TwitterRelationship>> ShowAsync(OAuthTokens tokens = null, decimal sourceUseId = 0, decimal targetUserId = 0, OptionalProperties options = null)
        {
            return await Core.CommandPerformer.PerformAction(new Twitterizer.Commands.ShowFriendshipCommand(tokens, sourceUseId, string.Empty, targetUserId, string.Empty, options));
        }

        /// <summary>
        /// Returns detailed information about the relationship between two users.
        /// </summary>
        /// <param name="tokens">The tokens.</param>
        /// <param name="sourceUserName">The source user name.</param>
        /// <param name="targetUserName">The target user name.</param>
        /// <param name="options">The options.</param>
        /// <returns>A <see cref="TwitterRelationship"/> instance.</returns>
        public static async Task<TwitterResponse<TwitterRelationship>> ShowAsync(OAuthTokens tokens = null, string sourceUserName = "", string targetUserName = "", OptionalProperties options = null)
        {
            return await Core.CommandPerformer.PerformAction(new Twitterizer.Commands.ShowFriendshipCommand(tokens, 0, sourceUserName, 0, targetUserName, options));
        }

        /// <summary>
        /// Allows the authenticating users to follow the user specified in the userID parameter.
        /// </summary>
        /// <param name="tokens">The tokens.</param>
        /// <param name="userId">The user id.</param>
        /// <param name="options">The options.</param>
        /// <returns>
        /// Returns the followed user in the requested format when successful.
        /// </returns>
        public static async Task<TwitterResponse<User>> CreateAsync(decimal userId, OAuthTokens tokens, CreateFriendshipOptions options = null)
        {
            return await CommandPerformer.PerformAction(new Commands.CreateFriendshipCommand(tokens, userId, options));
        }

        /// <summary>
        /// Allows the authenticating users to follow the user specified in the userName parameter.
        /// </summary>
        /// <param name="tokens">The tokens.</param>
        /// <param name="userName">The user name.</param>
        /// <param name="options">The options.</param>
        /// <returns>
        /// Returns the followed user in the requested format when successful.
        /// </returns>
        public static async Task<TwitterResponse<User>> CreateAsync(string userName, OAuthTokens tokens, CreateFriendshipOptions options)
        {
            return await CommandPerformer.PerformAction(new Commands.CreateFriendshipCommand(tokens, userName, options));
        }
        
        /// <summary>
        /// Allows the authenticating users to unfollow the user specified.
        /// </summary>
        /// <param name="tokens">The tokens.</param>
        /// <param name="TargetID">The user id of the friendship to destroy.</param>
        /// <param name="Screenname">The screenname of the user of the friendship to destroy.</param>
        /// <param name="options">The options.</param>
        /// <returns>
        /// Returns the unfollowed user in the requested format when successful. Returns a string describing the failure condition when unsuccessful.
        /// </returns>
        public static async Task<TwitterResponse<User>> DeleteAsync(OAuthTokens tokens, decimal TargetID = 0, string Screenname = "", OptionalProperties options = null)
        {
            return await CommandPerformer.PerformAction(new Twitterizer.Commands.DeleteFriendshipCommand(tokens, TargetID, Screenname, options));
        }

        /// <summary>
        /// Updates a friendship for a user.
        /// </summary>
        /// <param name="tokens">The tokens.</param>
        /// <param name="userid">The userid.</param>
        /// <param name="options">The options. Leave null for defaults.</param>
        /// <returns></returns>
        public static async Task<TwitterResponse<TwitterRelationship>> UpdateAsync(decimal userid, OAuthTokens tokens, UpdateFriendshipOptions options = null)
        {
            return await Core.CommandPerformer.PerformAction(new Commands.UpdateFriendshipCommand(tokens, userid, options));
        }

        /// <summary>
        /// Updates a friendship for a user.
        /// </summary>
        /// <param name="tokens">The tokens.</param>
        /// <param name="screenname">The screenname.</param>
        /// <param name="options">The options. Leave null for defaults.</param>
        /// <returns></returns>
        public static async Task<TwitterResponse<TwitterRelationship>> UpdateAsync(string screenname, OAuthTokens tokens, UpdateFriendshipOptions options = null)
        {
            return await Core.CommandPerformer.PerformAction(new Commands.UpdateFriendshipCommand(tokens, screenname, options));
        }

        /// <summary>
        /// Returns a collection of IDs that the user does not want to see retweets from.
        /// </summary>
        /// <param name="tokens">The tokens.</param>
        /// <param name="options">The options. Leave null for defaults.</param>
        /// <returns></returns>
        public static async Task<TwitterResponse<UserIdCollection>> NoRetweetIDsAsync(OAuthTokens tokens, OptionalProperties options = null)
        {
            return await Core.CommandPerformer.PerformAction(new Commands.NoRetweetIDsCommand(tokens, options));
        }
    }
}
