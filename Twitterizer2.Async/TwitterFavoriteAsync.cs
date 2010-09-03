//-----------------------------------------------------------------------
// <copyright file="TwitterFavoriteAsync.cs" company="Patrick 'Ricky' Smith">
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
// <summary>The asynchronous twitter favorite class.</summary>
//-----------------------------------------------------------------------
namespace Twitterizer2
{
    using System;
    using Twitterizer;

    public static class TwitterFavoriteAsync
    {
        /// <summary>
        /// Favorites the status specified in the ID parameter as the authenticating user.
        /// </summary>
        /// <param name="tokens">The tokens.</param>
        /// <param name="statusId">The status id.</param>
        /// <param name="options">The options.</param>
        /// <param name="timeout">The timeout.</param>
        /// <param name="function">The function.</param>
        /// <returns></returns>
        public static IAsyncResult Create(OAuthTokens tokens, decimal statusId, OptionalProperties options, TimeSpan timeout, Action<TwitterResponse<TwitterStatus>> function)
        {
            Func<OAuthTokens, decimal, OptionalProperties, TwitterResponse<TwitterStatus>> methodToCall = TwitterFavorite.Create;

            return methodToCall.BeginInvoke(
                tokens,
                statusId,
                options,
                result =>
                {
                    result.AsyncWaitHandle.WaitOne(timeout);
                    function(methodToCall.EndInvoke(result));
                },
                null);
        }

        /// <summary>
        /// Un-favorites the status specified in the ID parameter as the authenticating user.
        /// </summary>
        /// <param name="tokens">The tokens.</param>
        /// <param name="statusId">The status id.</param>
        /// <param name="options">The options.</param>
        /// <param name="timeout">The timeout.</param>
        /// <param name="function">The function.</param>
        /// <returns></returns>
        public static IAsyncResult Delete(OAuthTokens tokens, decimal statusId, OptionalProperties options, TimeSpan timeout, Action<TwitterResponse<TwitterStatus>> function)
        {
            Func<OAuthTokens, decimal, OptionalProperties, TwitterResponse<TwitterStatus>> methodToCall = TwitterFavorite.Delete;

            return methodToCall.BeginInvoke(
                tokens,
                statusId,
                options,
                result =>
                {
                    result.AsyncWaitHandle.WaitOne(timeout);
                    function(methodToCall.EndInvoke(result));
                },
                null);
        }

        /// <summary>
        /// Returns the 20 most recent favorite statuses for the authenticating user or user specified by the ID parameter in the requested format.
        /// </summary>
        /// <param name="tokens">The tokens.</param>
        /// <param name="options">The options.</param>
        /// <param name="timeout">The timeout.</param>
        /// <param name="function">The function.</param>
        /// <returns></returns>
        public static IAsyncResult List(OAuthTokens tokens, ListFavoritesOptions options, TimeSpan timeout, Action<TwitterResponse<TwitterStatusCollection>> function)
        {
            Func<OAuthTokens, ListFavoritesOptions, TwitterResponse<TwitterStatusCollection>> methodToCall = TwitterFavorite.List;

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
    }
}
