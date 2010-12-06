//-----------------------------------------------------------------------
// <copyright file="TwitterBlockAsync.cs" company="Patrick 'Ricky' Smith">
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
// <summary>The TwitterBlockAsync class.</summary>
//-----------------------------------------------------------------------

using System;
namespace Twitterizer
{
    public static class TwitterBlockAsync
    {
        /// <summary>
        /// Blocks the user specified as the authenticating user. Destroys a friendship to the blocked user if it exists.
        /// </summary>
        /// <param name="tokens">The tokens.</param>
        /// <param name="userId">The user id.</param>
        /// <param name="options">The options.</param>
        /// <param name="timeout">The timeout.</param>
        /// <param name="function">The function.</param>
        /// <returns></returns>
        public static IAsyncResult Create(OAuthTokens tokens, decimal userId, OptionalProperties options, TimeSpan timeout, Action<TwitterAsyncResponse<TwitterUser>> function)
        {
            Func<OAuthTokens, decimal, OptionalProperties, TwitterResponse<TwitterUser>> methodToCall = TwitterBlock.Create;

            return methodToCall.BeginInvoke(
                tokens,
                userId,
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
        /// Blocks the user specified as the authenticating user. Destroys a friendship to the blocked user if it exists.
        /// </summary>
        /// <param name="tokens">The tokens.</param>
        /// <param name="userId">The user id.</param>
        /// <param name="timeout">The timeout.</param>
        /// <param name="function">The function.</param>
        /// <returns></returns>
        public static IAsyncResult Create(OAuthTokens tokens, decimal userId, TimeSpan timeout, Action<TwitterAsyncResponse<TwitterUser>> function)
        {
            Func<OAuthTokens, decimal, TwitterResponse<TwitterUser>> methodToCall = TwitterBlock.Create;

            return methodToCall.BeginInvoke(
                tokens,
                userId,
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
        /// Blocks the user specified as the authenticating user. Destroys a friendship to the blocked user if it exists.
        /// </summary>
        /// <param name="tokens">The tokens.</param>
        /// <param name="screenName">The user's screen name.</param>
        /// <param name="options">The options.</param>
        /// <param name="timeout">The timeout.</param>
        /// <param name="function">The function.</param>
        /// <returns></returns>
        public static IAsyncResult Create(OAuthTokens tokens, string screenName, OptionalProperties options, TimeSpan timeout, Action<TwitterAsyncResponse<TwitterUser>> function)
        {
            Func<OAuthTokens, string, OptionalProperties, TwitterResponse<TwitterUser>> methodToCall = TwitterBlock.Create;

            return methodToCall.BeginInvoke(
                tokens,
                screenName,
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
        /// Blocks the user specifiedr. Destroys a friendship to the blocked user if it exists.
        /// </summary>
        /// <param name="tokens">The tokens.</param>
        /// <param name="screenName">The user's screen name.</param>
        /// <param name="timeout">The timeout.</param>
        /// <param name="function">The function.</param>
        /// <returns></returns>
        public static IAsyncResult Create(OAuthTokens tokens, string screenName, TimeSpan timeout, Action<TwitterAsyncResponse<TwitterUser>> function)
        {
            Func<OAuthTokens, string, TwitterResponse<TwitterUser>> methodToCall = TwitterBlock.Create;

            return methodToCall.BeginInvoke(
                tokens,
                screenName,
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
        /// Unblocks the user specified as the authenticating user.
        /// </summary>
        /// <param name="tokens">The tokens.</param>
        /// <param name="userId">The user id.</param>
        /// <param name="options">The options.</param>
        /// <param name="timeout">The timeout.</param>
        /// <param name="function">The function.</param>
        /// <returns></returns>
        public static IAsyncResult Destroy(OAuthTokens tokens, decimal userId, OptionalProperties options, TimeSpan timeout, Action<TwitterAsyncResponse<TwitterUser>> function)
        {
            Func<OAuthTokens, decimal, OptionalProperties, TwitterResponse<TwitterUser>> methodToCall = TwitterBlock.Destroy;

            return methodToCall.BeginInvoke(
                tokens,
                userId,
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
        /// Unblocks the user specified as the authenticating user.
        /// </summary>
        /// <param name="tokens">The tokens.</param>
        /// <param name="userId">The user id.</param>
        /// <param name="timeout">The timeout.</param>
        /// <param name="function">The function.</param>
        /// <returns></returns>
        public static IAsyncResult Destroy(OAuthTokens tokens, decimal userId, TimeSpan timeout, Action<TwitterAsyncResponse<TwitterUser>> function)
        {
            Func<OAuthTokens, decimal, TwitterResponse<TwitterUser>> methodToCall = TwitterBlock.Destroy;

            return methodToCall.BeginInvoke(
                tokens,
                userId,
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
        /// Unblocks the user specified as the authenticating user.
        /// </summary>
        /// <param name="tokens">The tokens.</param>
        /// <param name="screenName">The user's screen name.</param>
        /// <param name="options">The options.</param>
        /// <param name="timeout">The timeout.</param>
        /// <param name="function">The function.</param>
        /// <returns></returns>
        public static IAsyncResult Destroy(OAuthTokens tokens, string screenName, OptionalProperties options, TimeSpan timeout, Action<TwitterAsyncResponse<TwitterUser>> function)
        {
            Func<OAuthTokens, string, OptionalProperties, TwitterResponse<TwitterUser>> methodToCall = TwitterBlock.Destroy;

            return methodToCall.BeginInvoke(
                tokens,
                screenName,
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
        /// Unblocks the user specifiedr.
        /// </summary>
        /// <param name="tokens">The tokens.</param>
        /// <param name="screenName">The user's screen name.</param>
        /// <param name="timeout">The timeout.</param>
        /// <param name="function">The function.</param>
        /// <returns></returns>
        public static IAsyncResult Destroy(OAuthTokens tokens, string screenName, TimeSpan timeout, Action<TwitterAsyncResponse<TwitterUser>> function)
        {
            Func<OAuthTokens, string, TwitterResponse<TwitterUser>> methodToCall = TwitterBlock.Destroy;

            return methodToCall.BeginInvoke(
                tokens,
                screenName,
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
        /// Checks for a block against the the user specified as the authenticating user.
        /// </summary>
        /// <param name="tokens">The tokens.</param>
        /// <param name="userId">The user id.</param>
        /// <param name="options">The options.</param>
        /// <param name="timeout">The timeout.</param>
        /// <param name="function">The function.</param>
        /// <returns></returns>
        public static IAsyncResult Exists(OAuthTokens tokens, decimal userId, OptionalProperties options, TimeSpan timeout, Action<TwitterAsyncResponse<TwitterUser>> function)
        {
            Func<OAuthTokens, decimal, OptionalProperties, TwitterResponse<TwitterUser>> methodToCall = TwitterBlock.Exists;

            return methodToCall.BeginInvoke(
                tokens,
                userId,
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
        /// Checks for a block against the user specified as the authenticating user.
        /// </summary>
        /// <param name="tokens">The tokens.</param>
        /// <param name="userId">The user id.</param>
        /// <param name="timeout">The timeout.</param>
        /// <param name="function">The function.</param>
        /// <returns></returns>
        public static IAsyncResult Exists(OAuthTokens tokens, decimal userId, TimeSpan timeout, Action<TwitterAsyncResponse<TwitterUser>> function)
        {
            Func<OAuthTokens, decimal, TwitterResponse<TwitterUser>> methodToCall = TwitterBlock.Exists;

            return methodToCall.BeginInvoke(
                tokens,
                userId,
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
        /// Checks for a block against the the user specified as the authenticating user.
        /// </summary>
        /// <param name="tokens">The tokens.</param>
        /// <param name="screenName">The user's screen name.</param>
        /// <param name="options">The options.</param>
        /// <param name="timeout">The timeout.</param>
        /// <param name="function">The function.</param>
        /// <returns></returns>
        public static IAsyncResult Exists(OAuthTokens tokens, string screenName, OptionalProperties options, TimeSpan timeout, Action<TwitterAsyncResponse<TwitterUser>> function)
        {
            Func<OAuthTokens, string, OptionalProperties, TwitterResponse<TwitterUser>> methodToCall = TwitterBlock.Exists;

            return methodToCall.BeginInvoke(
                tokens,
                screenName,
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
        /// Checks for a block against the the user specifiedr.
        /// </summary>
        /// <param name="tokens">The tokens.</param>
        /// <param name="screenName">The user's screen name.</param>
        /// <param name="timeout">The timeout.</param>
        /// <param name="function">The function.</param>
        /// <returns></returns>
        public static IAsyncResult Exists(OAuthTokens tokens, string screenName, TimeSpan timeout, Action<TwitterAsyncResponse<TwitterUser>> function)
        {
            Func<OAuthTokens, string, TwitterResponse<TwitterUser>> methodToCall = TwitterBlock.Exists;

            return methodToCall.BeginInvoke(
                tokens,
                screenName,
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
    }
}
