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
using System;
using Twitterizer;

namespace Twitterizer
{
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
            Func<OAuthTokens, string, CreateFriendshipOptions, TwitterResponse<TwitterUser>> methodToCall = TwitterFriendship.Create;

            return methodToCall.BeginInvoke(
                tokens,
                userName,
                options,
                result => 
                {
                    result.AsyncWaitHandle.WaitOne(timeout);
                    try
                    {
                        function(methodToCall.EndInvoke(result).ToAsyncResponse());
                    }
                    catch (Exception ex)
                    {
                        function(new TwitterAsyncResponse<TwitterUser>() { Result = RequestResult.Unknown, ExceptionThrown = ex });
                    }
                },
                null);
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
            Func<OAuthTokens, string, OptionalProperties, TwitterResponse<TwitterUser>> methodToCall = TwitterFriendship.Delete;

            return methodToCall.BeginInvoke(
                tokens,
                userName,
                options,
                result => 
                {
                    result.AsyncWaitHandle.WaitOne(timeout);
                    try
                    {
                        function(methodToCall.EndInvoke(result).ToAsyncResponse());
                    }
                    catch (Exception ex)
                    {
                        function(new TwitterAsyncResponse<TwitterUser>() { Result = RequestResult.Unknown, ExceptionThrown = ex });
                    }
                },
                null);
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
            Func<OAuthTokens, string, OptionalProperties, TwitterResponse<TwitterRelationship>> methodToCall = TwitterFriendship.Show;

            return methodToCall.BeginInvoke(
                tokens,
                targetUserName,
                options,
                result => 
                {
                    result.AsyncWaitHandle.WaitOne(timeout);
                    try
                    {
                        function(methodToCall.EndInvoke(result).ToAsyncResponse());
                    }
                    catch (Exception ex)
                    {
                        function(new TwitterAsyncResponse<TwitterRelationship>() { Result = RequestResult.Unknown, ExceptionThrown = ex });
                    }
                },
                null);
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
            Func<OAuthTokens, UsersIdsOptions, TwitterResponse<UserIdCollection>> methodToCall = TwitterFriendship.FriendsIds;

            return methodToCall.BeginInvoke(
                tokens,
                options,
                result =>
                {
                    result.AsyncWaitHandle.WaitOne(timeout);
                    try
                    {
                        function(methodToCall.EndInvoke(result).ToAsyncResponse());
                    }
                    catch (Exception ex)
                    {
                        function(new TwitterAsyncResponse<UserIdCollection>() { Result = RequestResult.Unknown, ExceptionThrown = ex });
                    }
                },
                null);
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
            Func<OAuthTokens, UsersIdsOptions, TwitterResponse<UserIdCollection>> methodToCall = TwitterFriendship.FriendsIds;

            return methodToCall.BeginInvoke(
                tokens,
                null,
                result =>
                {
                    result.AsyncWaitHandle.WaitOne(timeout);
                    try
                    {
                        function(methodToCall.EndInvoke(result).ToAsyncResponse());
                    }
                    catch (Exception ex)
                    {
                        function(new TwitterAsyncResponse<UserIdCollection>() { Result = RequestResult.Unknown, ExceptionThrown = ex });
                    }
                },
                null);
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
            Func<OAuthTokens, UsersIdsOptions, TwitterResponse<UserIdCollection>> methodToCall = TwitterFriendship.FollowersIds;

            return methodToCall.BeginInvoke(
                tokens,
                options,
                result =>
                {
                    result.AsyncWaitHandle.WaitOne(timeout);
                    try
                    {
                        function(methodToCall.EndInvoke(result).ToAsyncResponse());
                    }
                    catch (Exception ex)
                    {
                        function(new TwitterAsyncResponse<UserIdCollection>() { Result = RequestResult.Unknown, ExceptionThrown = ex });
                    }
                },
                null);
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
            Func<OAuthTokens, UsersIdsOptions, TwitterResponse<UserIdCollection>> methodToCall = TwitterFriendship.FollowersIds;

            return methodToCall.BeginInvoke(
                tokens,
                null,
                result =>
                {
                    result.AsyncWaitHandle.WaitOne(timeout);
                    try
                    {
                        function(methodToCall.EndInvoke(result).ToAsyncResponse());
                    }
                    catch (Exception ex)
                    {
                        function(new TwitterAsyncResponse<UserIdCollection>() { Result = RequestResult.Unknown, ExceptionThrown = ex });
                    }
                },
                null);
        }
    }
}
