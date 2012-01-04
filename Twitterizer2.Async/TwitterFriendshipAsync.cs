//-----------------------------------------------------------------------
// <copyright file="TwitterFriendshipAsync.cs" company="Patrick 'Ricky' Smith">
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
// <author>David Golden</author>
// <summary>The TwitterFriendshipAsync class</summary>
//-----------------------------------------------------------------------

namespace Twitterizer
{
    using System;

    /// <summary>
    /// An asynchronous wrapper around the <see cref="TwitterFriendship"/> class.
    /// </summary>
    public static class TwitterFriendshipAsync
    {
        /// <summary>
        /// Allows the authenticating users to follow the user specified in the userName parameter.
        /// </summary>
        /// <param name="tokens">The tokens.</param>
        /// <param name="userName">The user name.</param>
        /// <param name="options">The options.</param>   
        /// <param name="timeout">The timeout.</param>
        /// <param name="function">The callback function.</param>        
        public static IAsyncResult Create(OAuthTokens tokens, string userName, CreateFriendshipOptions options, TimeSpan timeout, Action<TwitterAsyncResponse<TwitterUser>> function)
        {
            return AsyncUtility.ExecuteAsyncMethod(tokens, userName, options, timeout, TwitterFriendship.Create, function);
        }

        /// <summary>
        /// Allows the authenticating users to unfollow the user specified in the ID parameter.
        /// </summary>
        /// <param name="tokens">The tokens.</param>
        /// <param name="userName">The username.</param>
        /// <param name="options">The options.</param>  
        /// <param name="timeout">The timeout.</param>
        /// <param name="function">The callback function.</param>
        public static IAsyncResult Delete(OAuthTokens tokens, string userName, OptionalProperties options, TimeSpan timeout, Action<TwitterAsyncResponse<TwitterUser>> function)
        {
            return AsyncUtility.ExecuteAsyncMethod(tokens, userName, options, timeout, TwitterFriendship.Delete, function);
        }

        /// <summary>
        /// Returns detailed information about the relationship between two users.
        /// </summary>
        /// <param name="tokens">The tokens.</param>
        /// <param name="targetUserName">The target user name.</param>
        /// <param name="options">The options.</param>
        /// <param name="timeout">The timeout.</param>
        /// <param name="function">The callback function.</param>
        public static IAsyncResult Show(OAuthTokens tokens, string targetUserName, OptionalProperties options, TimeSpan timeout, Action<TwitterAsyncResponse<TwitterRelationship>> function)
        {
            return AsyncUtility.ExecuteAsyncMethod(tokens, targetUserName, options, timeout, TwitterFriendship.Show, function);
        }

        /// <summary>
        /// Returns the numeric IDs for every user the specified user is friends with.
        /// </summary>
        /// <param name="tokens">The tokens.</param>
        /// <param name="options">The options.</param>
        /// <param name="timeout">The timeout.</param>
        /// <param name="function">The function.</param>
        /// <returns></returns>
        public static IAsyncResult FriendsIds(OAuthTokens tokens, UsersIdsOptions options, TimeSpan timeout, Action<TwitterAsyncResponse<UserIdCollection>> function)
        {
            return AsyncUtility.ExecuteAsyncMethod(tokens, options, timeout, TwitterFriendship.FriendsIds, function);
        }

        /// <summary>
        /// Returns the numeric IDs for every user the specified user is friends with.
        /// </summary>
        /// <param name="tokens">The tokens.</param>
        /// <param name="timeout">The timeout.</param>
        /// <param name="function">The function.</param>
        /// <returns></returns>
        public static IAsyncResult FriendsIds(OAuthTokens tokens, TimeSpan timeout, Action<TwitterAsyncResponse<UserIdCollection>> function)
        {
            return AsyncUtility.ExecuteAsyncMethod(tokens, timeout, TwitterFriendship.FriendsIds, function);
        }

        /// <summary>
        /// Returns the numeric IDs for every user the specified user is following.
        /// </summary>
        /// <param name="tokens">The tokens.</param>
        /// <param name="options">The options.</param>
        /// <param name="timeout">The timeout.</param>
        /// <param name="function">The function.</param>
        /// <returns></returns>
        public static IAsyncResult FollowersIds(OAuthTokens tokens, UsersIdsOptions options, TimeSpan timeout, Action<TwitterAsyncResponse<UserIdCollection>> function)
        {
            return AsyncUtility.ExecuteAsyncMethod(tokens, options, timeout, TwitterFriendship.FollowersIds, function);
        }

        /// <summary>
        /// Returns the numeric IDs for every user the specified user is following.
        /// </summary>
        /// <param name="tokens">The tokens.</param>
        /// <param name="timeout">The timeout.</param>
        /// <param name="function">The function.</param>
        /// <returns></returns>
        public static IAsyncResult FollowersIds(OAuthTokens tokens, TimeSpan timeout, Action<TwitterAsyncResponse<UserIdCollection>> function)
        {
            return AsyncUtility.ExecuteAsyncMethod(tokens, timeout, TwitterFriendship.FollowersIds, function);
        }

        /// <summary>
        /// Returns a collection of IDs for every user who has a pending request to follow the authenticating user.
        /// </summary>
        /// <param name="tokens">The tokens.</param>
        /// <param name="timeout">The timeout.</param>
        /// <param name="options">The options.</param>
        /// <param name="function">The function.</param>
        /// <returns></returns>
        public static IAsyncResult IncomingRequests(OAuthTokens tokens, IncomingFriendshipsOptions options, TimeSpan timeout, Action<TwitterAsyncResponse<TwitterCursorPagedIdCollection>> function)
        {
            return AsyncUtility.ExecuteAsyncMethod(tokens, options, timeout, TwitterFriendship.IncomingRequests, function);
        }

        /// <summary>
        /// Returns a collection of IDs for every protected user for whom the authenticating user has a pending follow request.
        /// </summary>
        /// <param name="tokens">The tokens.</param>
        /// <param name="timeout">The timeout.</param>
        /// <param name="options">The options.</param>
        /// <param name="function">The function.</param>
        /// <returns></returns>
        public static IAsyncResult OutgoingRequests(OAuthTokens tokens, OutgoingFriendshipsOptions options, TimeSpan timeout, Action<TwitterAsyncResponse<TwitterCursorPagedIdCollection>> function)
        {
            return AsyncUtility.ExecuteAsyncMethod(tokens, options, timeout, TwitterFriendship.OutgoingRequests, function);
        }

        /// <summary>
        /// Returns the numeric IDs for every user the specified user is does not want to see retweets from.
        /// </summary>
        /// <param name="tokens">The tokens.</param>
        /// <param name="options">The options.</param>
        /// <param name="timeout">The timeout.</param>
        /// <param name="function">The function.</param>
        /// <returns></returns>
        public static IAsyncResult NoRetweetIDs(OAuthTokens tokens, OptionalProperties options, TimeSpan timeout, Action<TwitterAsyncResponse<UserIdCollection>> function)
        {
            return AsyncUtility.ExecuteAsyncMethod(tokens, options, timeout, TwitterFriendship.NoRetweetIDs, function);
        }

        /// <summary>
        /// Returns the numeric IDs for every user the specified user is does not want to see retweets from.
        /// </summary>
        /// <param name="tokens">The tokens.</param>       
        /// <param name="timeout">The timeout.</param>
        /// <param name="function">The function.</param>
        /// <returns></returns>
        public static IAsyncResult NoRetweetIDs(OAuthTokens tokens, TimeSpan timeout, Action<TwitterAsyncResponse<UserIdCollection>> function)
        {
            return AsyncUtility.ExecuteAsyncMethod(tokens, timeout, TwitterFriendship.NoRetweetIDs, function);
        }

        /// <summary>
        /// Updates the friendship.
        /// </summary>
        /// <param name="tokens">The tokens.</param>
        /// <param name="userid">The userid.</param>
        /// <param name="options">The options.</param>
        /// <param name="timeout">The timeout.</param>
        /// <param name="function">The function.</param>
        /// <returns></returns>
        public static IAsyncResult Update(OAuthTokens tokens, decimal userid, UpdateFriendshipOptions options, TimeSpan timeout, Action<TwitterAsyncResponse<TwitterRelationship>> function)
        {
            return AsyncUtility.ExecuteAsyncMethod(tokens, userid, options, timeout, TwitterFriendship.Update, function);
        }

        /// <summary>
        /// Returns the numeric IDs for every user the specified user is does not want to see retweets from.
        /// </summary>
        /// <param name="tokens">The tokens.</param>
        /// <param name="userid">The userid.</param>
        /// <param name="timeout">The timeout.</param>
        /// <param name="function">The function.</param>
        /// <returns></returns>
        public static IAsyncResult Update(OAuthTokens tokens, decimal userid, TimeSpan timeout, Action<TwitterAsyncResponse<TwitterRelationship>> function)
        {
            return AsyncUtility.ExecuteAsyncMethod(tokens, userid, timeout, TwitterFriendship.Update, function);
        }

        /// <summary>
        /// Updates the friendship.
        /// </summary>
        /// <param name="tokens">The tokens.</param>
        /// <param name="screenname">The screenname.</param>
        /// <param name="options">The options.</param>
        /// <param name="timeout">The timeout.</param>
        /// <param name="function">The function.</param>
        /// <returns></returns>
        public static IAsyncResult Update(OAuthTokens tokens, string screenname, UpdateFriendshipOptions options, TimeSpan timeout, Action<TwitterAsyncResponse<TwitterRelationship>> function)
        {
            return AsyncUtility.ExecuteAsyncMethod(tokens, screenname, options, timeout, TwitterFriendship.Update, function);
        }

        /// <summary>
        /// Returns the numeric IDs for every user the specified user is does not want to see retweets from.
        /// </summary>
        /// <param name="tokens">The tokens.</param>
        /// <param name="screenname">The screenname.</param>
        /// <param name="timeout">The timeout.</param>
        /// <param name="function">The function.</param>
        /// <returns></returns>
        public static IAsyncResult Update(OAuthTokens tokens, string screenname, TimeSpan timeout, Action<TwitterAsyncResponse<TwitterRelationship>> function)
        {
            return AsyncUtility.ExecuteAsyncMethod(tokens, screenname, timeout, TwitterFriendship.Update, function);
        }
    }
}
