//-----------------------------------------------------------------------
// <copyright file="TwitterTimelineAsync.cs" company="Patrick 'Ricky' Smith">
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
// <summary>The TwitterTimelineAsync class</summary>
//-----------------------------------------------------------------------
using System;
using Twitterizer;

namespace Twitterizer
{
    public static class TwitterTimelineAsync
    {
        /// <summary>
        /// Homes the timeline.
        /// </summary>
        /// <param name="tokens">The tokens.</param>
        /// <param name="options">The options.</param>
        /// <param name="timeout">The timeout.</param>
        /// <param name="function">The function.</param>
        /// <returns></returns>
        public static IAsyncResult HomeTimeline(OAuthTokens tokens, TimelineOptions options, TimeSpan timeout, Action<TwitterAsyncResponse<TwitterStatusCollection>> function)
        {
            Func<OAuthTokens, TimelineOptions, TwitterResponse<TwitterStatusCollection>> methodToCall = TwitterTimeline.HomeTimeline;

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
                        function(new TwitterAsyncResponse<TwitterStatusCollection>() { Result = RequestResult.Unknown, ExceptionThrown = ex });
                    }
                },
                null);
        }

        /// <summary>
        /// Obtains the authorized user's friends timeline.
        /// </summary>
        /// <param name="tokens">The tokens.</param>
        /// <param name="options">The options.</param>
        /// <param name="timeout">The timeout.</param>
        /// <param name="function">The function.</param>
        /// <returns></returns>
        public static IAsyncResult FriendTimeline(OAuthTokens tokens, TimelineOptions options, TimeSpan timeout, Action<TwitterAsyncResponse<TwitterStatusCollection>> function)
        {
            Func<OAuthTokens, TimelineOptions, TwitterResponse<TwitterStatusCollection>> methodToCall = TwitterTimeline.FriendTimeline;

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
                        function(new TwitterAsyncResponse<TwitterStatusCollection>() { Result = RequestResult.Unknown, ExceptionThrown = ex });
                    }
                },
                null);
        }

        /// <summary>
        /// Returns the 20 most recent mentions (status containing @username) for the authenticating user.
        /// </summary>
        /// <param name="tokens">The tokens.</param>
        /// <param name="options">The options.</param>
        /// <param name="timeout">The timeout.</param>
        /// <param name="function">The function.</param>
        /// <returns></returns>
        public static IAsyncResult Mentions(OAuthTokens tokens, TimelineOptions options, TimeSpan timeout, Action<TwitterAsyncResponse<TwitterStatusCollection>> function)
        {
            Func<OAuthTokens, TimelineOptions, TwitterResponse<TwitterStatusCollection>> methodToCall = TwitterTimeline.Mentions;

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
                        function(new TwitterAsyncResponse<TwitterStatusCollection>() { Result = RequestResult.Unknown, ExceptionThrown = ex });
                    }
                },
                null);
        }

        /// <summary>
        /// Gets the public timeline.
        /// </summary>
        /// <param name="tokens">The tokens.</param>
        /// <param name="options">The options.</param>
        /// <param name="timeout">The timeout.</param>
        /// <param name="function">The function.</param>
        /// <returns></returns>
        public static IAsyncResult PublicTimeline(OAuthTokens tokens, OptionalProperties options, TimeSpan timeout, Action<TwitterAsyncResponse<TwitterStatusCollection>> function)
        {
            Func<OAuthTokens, OptionalProperties, TwitterResponse<TwitterStatusCollection>> methodToCall = TwitterTimeline.PublicTimeline;

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
                        function(new TwitterAsyncResponse<TwitterStatusCollection>() { Result = RequestResult.Unknown, ExceptionThrown = ex });
                    }
                },
                null);
        }

        /// <summary>
        /// Gets the public timeline.
        /// </summary>
        /// <param name="options">The options.</param>
        /// <param name="timeout">The timeout.</param>
        /// <param name="function">The function.</param>
        /// <returns></returns>
        public static IAsyncResult PublicTimeline(OptionalProperties options, TimeSpan timeout, Action<TwitterAsyncResponse<TwitterStatusCollection>> function)
        {
            Func<OAuthTokens, OptionalProperties, TwitterResponse<TwitterStatusCollection>> methodToCall = TwitterTimeline.PublicTimeline;

            return methodToCall.BeginInvoke(
                null,
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
                        function(new TwitterAsyncResponse<TwitterStatusCollection>() { Result = RequestResult.Unknown, ExceptionThrown = ex });
                    }
                },
                null);
        }

        /// <summary>
        /// Returns the 20 most recent retweets posted by the authenticating user.
        /// </summary>
        /// <param name="tokens">The tokens.</param>
        /// <param name="options">The options.</param>
        /// <param name="timeout">The timeout.</param>
        /// <param name="function">The function.</param>
        /// <returns></returns>
        public static IAsyncResult RetweetedByMe(OAuthTokens tokens, TimelineOptions options, TimeSpan timeout, Action<TwitterAsyncResponse<TwitterStatusCollection>> function)
        {
            Func<OAuthTokens, TimelineOptions, TwitterResponse<TwitterStatusCollection>> methodToCall = TwitterTimeline.RetweetedByMe;

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
                        function(new TwitterAsyncResponse<TwitterStatusCollection>() { Result = RequestResult.Unknown, ExceptionThrown = ex });
                    }
                },
                null);
        }

        /// <summary>
        /// Returns the 20 most recent retweets posted by the authenticating user's friends.
        /// </summary>
        /// <param name="tokens">The tokens.</param>
        /// <param name="options">The options.</param>
        /// <param name="timeout">The timeout.</param>
        /// <param name="function">The function.</param>
        /// <returns></returns>
        public static IAsyncResult RetweetedToMe(OAuthTokens tokens, TimelineOptions options, TimeSpan timeout, Action<TwitterAsyncResponse<TwitterStatusCollection>> function)
        {
            Func<OAuthTokens, TimelineOptions, TwitterResponse<TwitterStatusCollection>> methodToCall = TwitterTimeline.RetweetedToMe;

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
                        function(new TwitterAsyncResponse<TwitterStatusCollection>() { Result = RequestResult.Unknown, ExceptionThrown = ex });
                    }
                },
                null);
        }

        /// <summary>
        /// Returns the 20 most recent tweets of the authenticated user that have been retweeted by others.
        /// </summary>
        /// <param name="tokens">The tokens.</param>
        /// <param name="options">The options.</param>
        /// <param name="timeout">The timeout.</param>
        /// <param name="function">The function.</param>
        /// <returns></returns>
        public static IAsyncResult RetweetsOfMe(OAuthTokens tokens, RetweetsOfMeOptions options, TimeSpan timeout, Action<TwitterAsyncResponse<TwitterStatusCollection>> function)
        {
            Func<OAuthTokens, RetweetsOfMeOptions, TwitterResponse<TwitterStatusCollection>> methodToCall = TwitterTimeline.RetweetsOfMe;

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
                        function(new TwitterAsyncResponse<TwitterStatusCollection>() { Result = RequestResult.Unknown, ExceptionThrown = ex });
                    }
                },
                null);
        }

        /// <summary>
        /// Returns the 20 most recent statuses posted by the authenticating user. It is also possible to request another user's timeline by using the screen_name or user_id parameter.
        /// </summary>
        /// <param name="tokens">The tokens.</param>
        /// <param name="options">The options.</param>
        /// <param name="timeout">The timeout.</param>
        /// <param name="function">The function.</param>
        /// <returns></returns>
        public static IAsyncResult UserTimeline(OAuthTokens tokens, UserTimelineOptions options, TimeSpan timeout, Action<TwitterAsyncResponse<TwitterStatusCollection>> function)
        {
            Func<OAuthTokens, UserTimelineOptions, TwitterResponse<TwitterStatusCollection>> methodToCall = TwitterTimeline.UserTimeline;

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
                        function(new TwitterAsyncResponse<TwitterStatusCollection>() { Result = RequestResult.Unknown, ExceptionThrown = ex });
                    }
                },
                null);
        }
    }
}
