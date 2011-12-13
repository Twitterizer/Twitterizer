//-----------------------------------------------------------------------
// <copyright file="TwitterTrendsAsync.cs" company="Patrick 'Ricky' Smith">
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
// <summary>The Asynchronous Twitter Trend class</summary>
//-----------------------------------------------------------------------
namespace Twitterizer
{
    using System;
#if SILVERLIGHT
    using System.Threading;
#endif
    public static class TwitterTrendsAsync
    {

        /// <summary>
        /// Gets the trends with the specified WOEID.
        /// </summary>
        /// <param name="WoeID">The WOEID.</param>
        /// <param name="options">The options.</param>
        /// <param name="timeout">The timeout.</param>
        /// <param name="function">The function.</param>
        /// <returns></returns>
        public static IAsyncResult Trends(int WoeID, LocalTrendsOptions options, TimeSpan timeout, Action<TwitterAsyncResponse<TwitterTrendCollection>> function)
        {
#if !SILVERLIGHT
            Func<int, LocalTrendsOptions, TwitterResponse<TwitterTrendCollection>> methodToCall = TwitterTrend.Trends;

            return methodToCall.BeginInvoke(
                WoeID, 
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
                        function(new TwitterAsyncResponse<TwitterTrendCollection>() { Result = RequestResult.Unknown, ExceptionThrown = ex });
                    }
                },
                null);
#else            
            ThreadPool.QueueUserWorkItem((x) =>
                {
                    function(TwitterTrend.Trends(WoeID, options).ToAsyncResponse<TwitterTrendCollection>());  
                });
            return null;
#endif
        }

        /// <summary>
        /// Gets the trends with the specified WOEID.
        /// </summary>
        /// <param name="WoeID">The WOEID.</param>
        /// <param name="timeout">The timeout.</param>
        /// <param name="function">The function.</param>
        /// <returns></returns>
        public static IAsyncResult Trends(int WoeID, TimeSpan timeout, Action<TwitterAsyncResponse<TwitterTrendCollection>> function)
        {
#if !SILVERLIGHT
            Func<int, TwitterResponse<TwitterTrendCollection>> methodToCall = TwitterTrend.Trends;

            return methodToCall.BeginInvoke(
                WoeID,                
                result =>
                {
                    result.AsyncWaitHandle.WaitOne(timeout);
                    try
                    {
                        function(methodToCall.EndInvoke(result).ToAsyncResponse());
                    }
                    catch (Exception ex)
                    {
                        function(new TwitterAsyncResponse<TwitterTrendCollection>() { Result = RequestResult.Unknown, ExceptionThrown = ex });
                    }
                },
                null);
#else            
            ThreadPool.QueueUserWorkItem((x) =>
                {
                    function(TwitterTrend.Trends(WoeID).ToAsyncResponse<TwitterTrendCollection>());  
                });
            return null;
#endif
        }

        /// <summary>
        /// Gets the trends with the specified WOEID.
        /// </summary>
        /// <param name="tokens">The request tokens.</param>
        /// <param name="WoeID">The WOEID.</param>
        /// <param name="options">The options.</param>
        /// <param name="timeout">The timeout.</param>
        /// <param name="function">The function.</param>
        /// <returns></returns>
        public static IAsyncResult Trends(OAuthTokens tokens, int WoeID, LocalTrendsOptions options, TimeSpan timeout, Action<TwitterAsyncResponse<TwitterTrendCollection>> function)
        {
#if !SILVERLIGHT
            Func<OAuthTokens, int, LocalTrendsOptions, TwitterResponse<TwitterTrendCollection>> methodToCall = TwitterTrend.Trends;

            return methodToCall.BeginInvoke(
                tokens,
                WoeID,
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
                        function(new TwitterAsyncResponse<TwitterTrendCollection>() { Result = RequestResult.Unknown, ExceptionThrown = ex });
                    }
                },
                null);
#else            
            ThreadPool.QueueUserWorkItem((x) =>
                {
                    function(TwitterTrend.Trends(tokens, WoeID, options).ToAsyncResponse<TwitterTrendCollection>());  
                });
            return null;
#endif
        }

        /// <summary>
        /// Gets the trends with the specified WOEID.
        /// </summary>
        /// <param name="tokens">The request tokens.</param>
        /// <param name="WoeID">The WOEID.</param>
        /// <param name="timeout">The timeout.</param>
        /// <param name="function">The function.</param>
        /// <returns></returns>
        public static IAsyncResult Trends(OAuthTokens tokens, int WoeID, TimeSpan timeout, Action<TwitterAsyncResponse<TwitterTrendCollection>> function)
        {
#if !SILVERLIGHT
            Func<OAuthTokens, int, TwitterResponse<TwitterTrendCollection>> methodToCall = TwitterTrend.Trends;

            return methodToCall.BeginInvoke(
                tokens,
                WoeID,
                result =>
                {
                    result.AsyncWaitHandle.WaitOne(timeout);
                    try
                    {
                        function(methodToCall.EndInvoke(result).ToAsyncResponse());
                    }
                    catch (Exception ex)
                    {
                        function(new TwitterAsyncResponse<TwitterTrendCollection>() { Result = RequestResult.Unknown, ExceptionThrown = ex });
                    }
                },
                null);
#else            
            ThreadPool.QueueUserWorkItem((x) =>
                {
                    function(TwitterTrend.Trends(tokens, WoeID).ToAsyncResponse<TwitterTrendCollection>());  
                });
            return null;
#endif
        }



        /// <summary>
        /// Gets the locations where trends are available.
        /// </summary>   
        /// <param name="tokens">The request tokens.</param>
        /// <param name="options">The options.</param>
        /// <param name="timeout">The timeout.</param>
        /// <param name="function">The function.</param>
        /// <returns></returns>
        public static IAsyncResult Available(OAuthTokens tokens, AvailableTrendsOptions options, TimeSpan timeout, Action<TwitterAsyncResponse<TwitterTrendLocationCollection>> function)
        {
#if !SILVERLIGHT
            Func<OAuthTokens, AvailableTrendsOptions, TwitterResponse<TwitterTrendLocationCollection>> methodToCall = TwitterTrend.Available;

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
                        function(new TwitterAsyncResponse<TwitterTrendLocationCollection>() { Result = RequestResult.Unknown, ExceptionThrown = ex });
                    }
                },
                null);
#else            
            ThreadPool.QueueUserWorkItem((x) =>
                {
                    function(TwitterTrend.Available(tokens, options).ToAsyncResponse<TwitterTrendLocationCollection>());  
                });
            return null;
#endif
        }

        /// <summary>
        /// Gets the locations where trends are available.
        /// </summary>   
        /// <param name="options">The options.</param>
        /// <param name="timeout">The timeout.</param>
        /// <param name="function">The function.</param>
        /// <returns></returns>
        public static IAsyncResult Available(AvailableTrendsOptions options, TimeSpan timeout, Action<TwitterAsyncResponse<TwitterTrendLocationCollection>> function)
        {
#if !SILVERLIGHT
            Func<AvailableTrendsOptions, TwitterResponse<TwitterTrendLocationCollection>> methodToCall = TwitterTrend.Available;

            return methodToCall.BeginInvoke(
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
                        function(new TwitterAsyncResponse<TwitterTrendLocationCollection>() { Result = RequestResult.Unknown, ExceptionThrown = ex });
                    }
                },
                null);
#else            
            ThreadPool.QueueUserWorkItem((x) =>
                {
                    function(TwitterTrend.Available(options).ToAsyncResponse<TwitterTrendLocationCollection>());  
                });
            return null;
#endif
        }

        /// <summary>
        /// Gets the locations where trends are available.
        /// </summary>   
        /// <param name="timeout">The timeout.</param>
        /// <param name="function">The function.</param>
        /// <returns></returns>
        public static IAsyncResult Available(TimeSpan timeout, Action<TwitterAsyncResponse<TwitterTrendLocationCollection>> function)
        {
#if !SILVERLIGHT
            Func<TwitterResponse<TwitterTrendLocationCollection>> methodToCall = TwitterTrend.Available;

            return methodToCall.BeginInvoke(
                result =>
                {
                    result.AsyncWaitHandle.WaitOne(timeout);
                    try
                    {
                        function(methodToCall.EndInvoke(result).ToAsyncResponse());
                    }
                    catch (Exception ex)
                    {
                        function(new TwitterAsyncResponse<TwitterTrendLocationCollection>() { Result = RequestResult.Unknown, ExceptionThrown = ex });
                    }
                },
                null);
#else            
            ThreadPool.QueueUserWorkItem((x) =>
                {
                    function(TwitterTrend.Available().ToAsyncResponse<TwitterTrendLocationCollection>());  
                });
            return null;
#endif
        }


        /// <summary>
        /// Gets the daily global trends
        /// </summary>
        /// <param name="tokens">The request tokens.</param>
        /// <param name="options">The options.</param>
        /// <param name="timeout">The timeout.</param>
        /// <param name="function">The function.</param>
        /// <returns></returns>
        public static IAsyncResult Daily(OAuthTokens tokens, TrendsOptions options, TimeSpan timeout, Action<TwitterAsyncResponse<TwitterTrendDictionary>> function)
        {
#if !SILVERLIGHT
            Func<OAuthTokens, TrendsOptions, TwitterResponse<TwitterTrendDictionary>> methodToCall = TwitterTrend.Daily;

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
                        function(new TwitterAsyncResponse<TwitterTrendDictionary>() { Result = RequestResult.Unknown, ExceptionThrown = ex });
                    }
                },
                null);
#else            
            ThreadPool.QueueUserWorkItem((x) =>
                {
                    function(TwitterTrend.Daily(tokens, options).ToAsyncResponse<TwitterTrendDictionary>());  
                });
            return null;
#endif
        }

        /// <summary>
        /// Gets the daily global trends
        /// </summary>
        /// <param name="options">The options.</param>
        /// <param name="timeout">The timeout.</param>
        /// <param name="function">The function.</param>
        /// <returns></returns>
        public static IAsyncResult Daily(TrendsOptions options, TimeSpan timeout, Action<TwitterAsyncResponse<TwitterTrendDictionary>> function)
        {
#if !SILVERLIGHT
            Func<TrendsOptions, TwitterResponse<TwitterTrendDictionary>> methodToCall = TwitterTrend.Daily;

            return methodToCall.BeginInvoke(
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
                        function(new TwitterAsyncResponse<TwitterTrendDictionary>() { Result = RequestResult.Unknown, ExceptionThrown = ex });
                    }
                },
                null);
#else            
            ThreadPool.QueueUserWorkItem((x) =>
                {
                    function(TwitterTrend.Daily(options).ToAsyncResponse<TwitterTrendDictionary>());  
                });
            return null;
#endif
        }

        /// <summary>
        /// Gets the daily global trends
        /// </summary>
        /// <param name="timeout">The timeout.</param>
        /// <param name="function">The function.</param>
        /// <returns></returns>
        public static IAsyncResult Daily(TimeSpan timeout, Action<TwitterAsyncResponse<TwitterTrendDictionary>> function)
        {
#if !SILVERLIGHT
            Func<TwitterResponse<TwitterTrendDictionary>> methodToCall = TwitterTrend.Daily;

            return methodToCall.BeginInvoke(
                result =>
                {
                    result.AsyncWaitHandle.WaitOne(timeout);
                    try
                    {
                        function(methodToCall.EndInvoke(result).ToAsyncResponse());
                    }
                    catch (Exception ex)
                    {
                        function(new TwitterAsyncResponse<TwitterTrendDictionary>() { Result = RequestResult.Unknown, ExceptionThrown = ex });
                    }
                },
                null);
#else            
            ThreadPool.QueueUserWorkItem((x) =>
                {
                    function(TwitterTrend.Daily().ToAsyncResponse<TwitterTrendDictionary>());  
                });
            return null;
#endif
        }


        /// <summary>
        /// Gets the weekly global trends
        /// </summary>
        /// <param name="tokens">The request tokens.</param>
        /// <param name="options">The options.</param>
        /// <param name="timeout">The timeout.</param>
        /// <param name="function">The function.</param>
        /// <returns></returns>
        public static IAsyncResult Weekly(OAuthTokens tokens, TrendsOptions options, TimeSpan timeout, Action<TwitterAsyncResponse<TwitterTrendDictionary>> function)
        {
#if !SILVERLIGHT
            Func<OAuthTokens, TrendsOptions, TwitterResponse<TwitterTrendDictionary>> methodToCall = TwitterTrend.Weekly;

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
                        function(new TwitterAsyncResponse<TwitterTrendDictionary>() { Result = RequestResult.Unknown, ExceptionThrown = ex });
                    }
                },
                null);
#else            
            ThreadPool.QueueUserWorkItem((x) =>
                {
                    function(TwitterTrend.Weekly(tokens, options).ToAsyncResponse<TwitterTrendDictionary>());  
                });
            return null;
#endif
        }

        /// <summary>
        /// Gets the weekly global trends
        /// </summary>
        /// <param name="options">The options.</param>
        /// <param name="timeout">The timeout.</param>
        /// <param name="function">The function.</param>
        /// <returns></returns>
        public static IAsyncResult Weekly(TrendsOptions options, TimeSpan timeout, Action<TwitterAsyncResponse<TwitterTrendDictionary>> function)
        {
#if !SILVERLIGHT
            Func<TrendsOptions, TwitterResponse<TwitterTrendDictionary>> methodToCall = TwitterTrend.Weekly;

            return methodToCall.BeginInvoke(
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
                        function(new TwitterAsyncResponse<TwitterTrendDictionary>() { Result = RequestResult.Unknown, ExceptionThrown = ex });
                    }
                },
                null);
#else            
            ThreadPool.QueueUserWorkItem((x) =>
                {
                    function(TwitterTrend.Weekly(options).ToAsyncResponse<TwitterTrendDictionary>());  
                });
            return null;
#endif
        }

        /// <summary>
        /// Gets the weekly global trends
        /// </summary>
        /// <param name="timeout">The timeout.</param>
        /// <param name="function">The function.</param>
        /// <returns></returns>
        public static IAsyncResult Weekly(TimeSpan timeout, Action<TwitterAsyncResponse<TwitterTrendDictionary>> function)
        {
#if !SILVERLIGHT
            Func<TwitterResponse<TwitterTrendDictionary>> methodToCall = TwitterTrend.Weekly;

            return methodToCall.BeginInvoke(
                result =>
                {
                    result.AsyncWaitHandle.WaitOne(timeout);
                    try
                    {
                        function(methodToCall.EndInvoke(result).ToAsyncResponse());
                    }
                    catch (Exception ex)
                    {
                        function(new TwitterAsyncResponse<TwitterTrendDictionary>() { Result = RequestResult.Unknown, ExceptionThrown = ex });
                    }
                },
                null);
#else            
            ThreadPool.QueueUserWorkItem((x) =>
                {
                    function(TwitterTrend.Weekly().ToAsyncResponse<TwitterTrendDictionary>());  
                });
            return null;
#endif
        }
    }
}
