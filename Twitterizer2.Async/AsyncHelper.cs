using System;
using Twitterizer.Core;

namespace Twitterizer
{
    internal static class AsyncHelper
    {
        public static IAsyncResult ExecuteAsyncMethod<TResponse>(OAuthTokens tokens, int woeId, TimeSpan timeout, Func<OAuthTokens, int, TwitterResponse<TResponse>> methodToCall, Action<TwitterAsyncResponse<TResponse>> function)
    where TResponse : ITwitterObject
        {
#if !SILVERLIGHT
            return methodToCall.BeginInvoke(
                tokens,
                woeId,
                result =>
                {
                    result.AsyncWaitHandle.WaitOne(timeout);
                    try
                    {
                        function(methodToCall.EndInvoke(result).ToAsyncResponse());
                    }
                    catch (Exception ex)
                    {
                        function(new TwitterAsyncResponse<TResponse> { Result = RequestResult.Unknown, ExceptionThrown = ex });
                    }
                },
                null);
#else
            System.Threading.ThreadPool.QueueUserWorkItem(x => methodToCall(tokens, woeId).ToAsyncResponse<TResponse>());
            return null;
#endif
        }

        public static IAsyncResult ExecuteAsyncMethod<TResponse, TProperties>(OAuthTokens tokens, int woeId, TProperties properties, TimeSpan timeout, Func<OAuthTokens, int, TProperties, TwitterResponse<TResponse>> methodToCall, Action<TwitterAsyncResponse<TResponse>> function)
            where TResponse : ITwitterObject
            where TProperties : OptionalProperties
        {
#if !SILVERLIGHT
            return methodToCall.BeginInvoke(
                tokens,
                woeId,
                properties,
                result =>
                {
                    result.AsyncWaitHandle.WaitOne(timeout);
                    try
                    {
                        function(methodToCall.EndInvoke(result).ToAsyncResponse());
                    }
                    catch (Exception ex)
                    {
                        function(new TwitterAsyncResponse<TResponse> { Result = RequestResult.Unknown, ExceptionThrown = ex });
                    }
                },
                null);
#else
            System.Threading.ThreadPool.QueueUserWorkItem(x => methodToCall(tokens, woeId, properties).ToAsyncResponse<TResponse>());
            return null;
#endif
        }

        public static IAsyncResult ExecuteAsyncMethod<TResponse>(OAuthTokens tokens, TimeSpan timeout, Func<OAuthTokens, TwitterResponse<TResponse>> methodToCall, Action<TwitterAsyncResponse<TResponse>> function)
            where TResponse : ITwitterObject
        {
#if !SILVERLIGHT
            return methodToCall.BeginInvoke(
                tokens,
                result =>
                {
                    result.AsyncWaitHandle.WaitOne(timeout);
                    try
                    {
                        function(methodToCall.EndInvoke(result).ToAsyncResponse());
                    }
                    catch (Exception ex)
                    {
                        function(new TwitterAsyncResponse<TResponse> { Result = RequestResult.Unknown, ExceptionThrown = ex });
                    }
                },
                null);
#else
            System.Threading.ThreadPool.QueueUserWorkItem(x => methodToCall(tokens).ToAsyncResponse<TResponse>());
            return null;
#endif
        }

        public static IAsyncResult ExecuteAsyncMethod<TResponse, TProperties>(OAuthTokens tokens, TProperties properties, TimeSpan timeout, Func<OAuthTokens, TProperties, TwitterResponse<TResponse>> methodToCall, Action<TwitterAsyncResponse<TResponse>> function)
            where TResponse : ITwitterObject
            where TProperties : OptionalProperties
        {
#if !SILVERLIGHT
            return methodToCall.BeginInvoke(
                tokens,
                properties,
                result =>
                {
                    result.AsyncWaitHandle.WaitOne(timeout);
                    try
                    {
                        function(methodToCall.EndInvoke(result).ToAsyncResponse());
                    }
                    catch (Exception ex)
                    {
                        function(new TwitterAsyncResponse<TResponse> { Result = RequestResult.Unknown, ExceptionThrown = ex });
                    }
                },
                null);
#else
            System.Threading.ThreadPool.QueueUserWorkItem(x => methodToCall(tokens, properties).ToAsyncResponse<TResponse>());
            return null;
#endif
        }

    }
}