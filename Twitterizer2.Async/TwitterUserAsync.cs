//-----------------------------------------------------------------------
// <copyright file="TwitterUserAsync.cs" company="Patrick 'Ricky' Smith">
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
// <summary>The TwitterUserAsync class</summary>
//-----------------------------------------------------------------------
using System;
using Twitterizer;

namespace Twitterizer2
{
    public static class TwitterUserAsync
    {
        /// <summary>
        /// Return up to 100 users worth of extended information, specified by either ID, screen name, or combination of the two.
        /// </summary>
        /// <param name="tokens">The tokens.</param>
        /// <param name="options">The options.</param>
        /// <param name="timeout">The timeout.</param>
        /// <param name="function">The function.</param>
        /// <returns></returns>
        public static IAsyncResult Lookup(OAuthTokens tokens, LookupUsersOptions options, TimeSpan timeout, Action<TwitterResponse<TwitterUserCollection>> function)
        {
            Func<OAuthTokens, LookupUsersOptions, TwitterResponse<TwitterUserCollection>> methodToCall = TwitterUser.Lookup;

            return methodToCall.BeginInvoke(
                tokens,
                options,
                result =>
                {
                    result.AsyncWaitHandle.WaitOne(timeout);
                    function(methodToCall.EndInvoke(result));
                },
                null);
        }

        /// <summary>
        /// Runs a search for users similar to Find People button on Twitter.com. The results returned by people search on Twitter.com are the same as those returned by this API request.
        /// </summary>
        /// <param name="tokens">The tokens.</param>
        /// <param name="query">The query.</param>
        /// <param name="options">The options.</param>
        /// <param name="timeout">The timeout.</param>
        /// <param name="function">The function.</param>
        /// <returns></returns>
        public static IAsyncResult Search(OAuthTokens tokens, string query, UserSearchOptions options, TimeSpan timeout, Action<TwitterResponse<TwitterUserCollection>> function)
        {
            Func<OAuthTokens, string, UserSearchOptions, TwitterResponse<TwitterUserCollection>> methodToCall = TwitterUser.Search;

            return methodToCall.BeginInvoke(
                tokens,
                query,
                options,
                result =>
                {
                    result.AsyncWaitHandle.WaitOne(timeout);
                    function(methodToCall.EndInvoke(result));
                },
                null);
        }

        /// <summary>
        /// Shows the specified username.
        /// </summary>
        /// <param name="tokens">The tokens.</param>
        /// <param name="username">The username.</param>
        /// <param name="options">The options.</param>
        /// <param name="timeout">The timeout.</param>
        /// <param name="function">The function.</param>
        /// <returns></returns>
        public static IAsyncResult Show(OAuthTokens tokens, string username, OptionalProperties options, TimeSpan timeout, Action<TwitterResponse<TwitterUser>> function)
        {
            Func<OAuthTokens, string, OptionalProperties, TwitterResponse<TwitterUser>> methodToCall = TwitterUser.Show;

            return methodToCall.BeginInvoke(
                tokens,
                username,
                options,
                result =>
                {
                    result.AsyncWaitHandle.WaitOne(timeout);
                    function(methodToCall.EndInvoke(result));
                },
                null);
        }

        /// <summary>
        /// Shows the specified username.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <param name="options">The options.</param>
        /// <param name="timeout">The timeout.</param>
        /// <param name="function">The function.</param>
        /// <returns></returns>
        public static IAsyncResult Show(string username, OptionalProperties options, TimeSpan timeout, Action<TwitterResponse<TwitterUser>> function)
        {
            Func<string, OptionalProperties, TwitterResponse<TwitterUser>> methodToCall = TwitterUser.Show;

            return methodToCall.BeginInvoke(                
                username,
                options,
                result =>
                {
                    result.AsyncWaitHandle.WaitOne(timeout);
                    function(methodToCall.EndInvoke(result));
                },
                null);
        }
    }
}
