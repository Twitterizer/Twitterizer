using System;
using System.Threading;
using Twitterizer.Core;

namespace Twitterizer
{
    internal static class AsyncUtility
    {
        #region Parameterized Helper Callbacks

        public static void ZeroParamsCallback<TResponse>(Object result, TimeSpan timeout, Func<OAuthTokens, TwitterResponse<TResponse>> methodToCall, Action<TwitterAsyncResponse<TResponse>> function)
            where TResponse : ITwitterObject
        {
#if !SILVERLIGHT
            (result as IAsyncResult).AsyncWaitHandle.WaitOne(timeout);
            try
            {
                function(methodToCall.EndInvoke(result as IAsyncResult).ToAsyncResponse());
            }
            catch (Exception ex)
            {
                function(new TwitterAsyncResponse<TResponse> { Result = RequestResult.Unknown, ExceptionThrown = ex });
            }
#else
            function(((TwitterResponse<TResponse>)result).ToAsyncResponse());
#endif
        }

        public static void OneParamCallback<TParam, TResponse>(Object result, TimeSpan timeout, Func<OAuthTokens, TParam, TwitterResponse<TResponse>> methodToCall, Action<TwitterAsyncResponse<TResponse>> function)
            where TResponse : ITwitterObject
        {
#if !SILVERLIGHT
            (result as IAsyncResult).AsyncWaitHandle.WaitOne(timeout);
            try
            {
                function(methodToCall.EndInvoke(result as IAsyncResult).ToAsyncResponse());
            }
            catch (Exception ex)
            {
                function(new TwitterAsyncResponse<TResponse> { Result = RequestResult.Unknown, ExceptionThrown = ex });
            }
#else
            function(((TwitterResponse<TResponse>)result).ToAsyncResponse());
#endif
        }

        public static void OneParamNoTokenCallback<TParam, TResponse>(Object result, TimeSpan timeout, Func<TParam, TwitterResponse<TResponse>> methodToCall, Action<TwitterAsyncResponse<TResponse>> function)
            where TResponse : ITwitterObject
        {
#if !SILVERLIGHT
            (result as IAsyncResult).AsyncWaitHandle.WaitOne(timeout);
            try
            {
                function(methodToCall.EndInvoke(result as IAsyncResult).ToAsyncResponse());
            }
            catch (Exception ex)
            {
                function(new TwitterAsyncResponse<TResponse> { Result = RequestResult.Unknown, ExceptionThrown = ex });
            }
#else
            function(((TwitterResponse<TResponse>)result).ToAsyncResponse());
#endif

        }

        public static void TwoParamsCallback<TParam1, TParam2, TResponse>(Object result, TimeSpan timeout, Func<OAuthTokens, TParam1, TParam2, TwitterResponse<TResponse>> methodToCall, Action<TwitterAsyncResponse<TResponse>> function)
            where TResponse : ITwitterObject
        {
#if !SILVERLIGHT
            (result as IAsyncResult).AsyncWaitHandle.WaitOne(timeout);
            try
            {
                function(methodToCall.EndInvoke(result as IAsyncResult).ToAsyncResponse());
            }
            catch (Exception ex)
            {
                function(new TwitterAsyncResponse<TResponse> { Result = RequestResult.Unknown, ExceptionThrown = ex });
            }
#else
            function(((TwitterResponse<TResponse>)result).ToAsyncResponse());
#endif

        }

        public static void TwoParamsNoTokenCallback<TParam1, TParam2, TResponse>(Object result, TimeSpan timeout, Func<TParam1, TParam2, TwitterResponse<TResponse>> methodToCall, Action<TwitterAsyncResponse<TResponse>> function)
            where TResponse : ITwitterObject
        {
#if !SILVERLIGHT
            (result as IAsyncResult).AsyncWaitHandle.WaitOne(timeout);
            try
            {
                function(methodToCall.EndInvoke(result as IAsyncResult).ToAsyncResponse());
            }
            catch (Exception ex)
            {
                function(new TwitterAsyncResponse<TResponse> { Result = RequestResult.Unknown, ExceptionThrown = ex });
            }
#else
            function(((TwitterResponse<TResponse>)result).ToAsyncResponse());
#endif

        }

        public static void ThreeParamsCallback<TParam1, TParam2, TParam3, TResponse>(Object result, TimeSpan timeout, Func<OAuthTokens, TParam1, TParam2, TParam3, TwitterResponse<TResponse>> methodToCall, Action<TwitterAsyncResponse<TResponse>> function)
            where TResponse : ITwitterObject
        {
#if !SILVERLIGHT
            (result as IAsyncResult).AsyncWaitHandle.WaitOne(timeout);
            try
            {
                function(methodToCall.EndInvoke(result as IAsyncResult).ToAsyncResponse());
            }
            catch (Exception ex)
            {
                function(new TwitterAsyncResponse<TResponse> { Result = RequestResult.Unknown, ExceptionThrown = ex });
            }
#else
            function(((TwitterResponse<TResponse>)result).ToAsyncResponse());
#endif

        }

        public static void ThreeParamsNoTokenCallback<TParam1, TParam2, TParam3, TResponse>(Object result, TimeSpan timeout, Func<TParam1, TParam2, TParam3, TwitterResponse<TResponse>> methodToCall, Action<TwitterAsyncResponse<TResponse>> function)
            where TResponse : ITwitterObject
        {
#if !SILVERLIGHT
            (result as IAsyncResult).AsyncWaitHandle.WaitOne(timeout);
            try
            {
                function(methodToCall.EndInvoke(result as IAsyncResult).ToAsyncResponse());
            }
            catch (Exception ex)
            {
                function(new TwitterAsyncResponse<TResponse> { Result = RequestResult.Unknown, ExceptionThrown = ex });
            }
#else
            function(((TwitterResponse<TResponse>)result).ToAsyncResponse());
#endif

        }

        public static void FourParamsCallback<TParam1, TParam2, TParam3, TParam4, TResponse>(Object result, TimeSpan timeout, Func<OAuthTokens, TParam1, TParam2, TParam3, TParam4, TwitterResponse<TResponse>> methodToCall, Action<TwitterAsyncResponse<TResponse>> function)
            where TResponse : ITwitterObject
        {
#if !SILVERLIGHT
            (result as IAsyncResult).AsyncWaitHandle.WaitOne(timeout);
            try
            {
                function(methodToCall.EndInvoke(result as IAsyncResult).ToAsyncResponse());
            }
            catch (Exception ex)
            {
                function(new TwitterAsyncResponse<TResponse> { Result = RequestResult.Unknown, ExceptionThrown = ex });
            }
#else
            function(((TwitterResponse<TResponse>)result).ToAsyncResponse());
#endif

        }

        public static void FourParamsNoTokenCallback<TParam1, TParam2, TParam3, TParam4, TResponse>(Object result, TimeSpan timeout, Func<TParam1, TParam2, TParam3, TParam4, TwitterResponse<TResponse>> methodToCall, Action<TwitterAsyncResponse<TResponse>> function)
            where TResponse : ITwitterObject
        {
#if !SILVERLIGHT
            (result as IAsyncResult).AsyncWaitHandle.WaitOne(timeout);
            try
            {
                function(methodToCall.EndInvoke(result as IAsyncResult).ToAsyncResponse());
            }
            catch (Exception ex)
            {
                function(new TwitterAsyncResponse<TResponse> { Result = RequestResult.Unknown, ExceptionThrown = ex });
            }
#else
            function(((TwitterResponse<TResponse>)result).ToAsyncResponse());
#endif

        }

        #endregion

        #region Zero parameters

        public static IAsyncResult ExecuteAsyncMethod<TResponse>(OAuthTokens tokens, TimeSpan timeout, Func<OAuthTokens, TwitterResponse<TResponse>> methodToCall, Action<TwitterAsyncResponse<TResponse>> function)
            where TResponse : ITwitterObject
        {
#if !SILVERLIGHT
            return methodToCall.BeginInvoke(tokens, result => ZeroParamsCallback(result, timeout, methodToCall, function), null);
#else
            ThreadPool.QueueUserWorkItem(result => ZeroParamsCallback(methodToCall(tokens), timeout, methodToCall, function));
            return null;
#endif
        }

        public static IAsyncResult ExecuteAsyncMethod<TResponse, TProperties>(OAuthTokens tokens, TProperties properties, TimeSpan timeout, Func<OAuthTokens, TProperties, TwitterResponse<TResponse>> methodToCall, Action<TwitterAsyncResponse<TResponse>> function)
            where TResponse : ITwitterObject
            where TProperties : OptionalProperties
        {
#if !SILVERLIGHT
            return methodToCall.BeginInvoke(tokens, properties, result => OneParamCallback(result, timeout, methodToCall, function), null);
#else
            ThreadPool.QueueUserWorkItem(result => OneParamCallback(methodToCall(tokens, properties), timeout, methodToCall, function));
            return null;
#endif
        }

        #endregion

        #region One Int32 parameter

        public static IAsyncResult ExecuteAsyncMethod<TResponse>(OAuthTokens tokens, int i, TimeSpan timeout, Func<OAuthTokens, int, TwitterResponse<TResponse>> methodToCall, Action<TwitterAsyncResponse<TResponse>> function)
            where TResponse : ITwitterObject
        {
#if !SILVERLIGHT
            return methodToCall.BeginInvoke(tokens, i, result => OneParamCallback(result, timeout, methodToCall, function), null);
#else
            ThreadPool.QueueUserWorkItem(result => OneParamCallback(methodToCall(tokens, i), timeout, methodToCall, function));
            return null;
#endif
        }

        public static IAsyncResult ExecuteAsyncMethod<TResponse, TProperties>(OAuthTokens tokens, int i, TProperties properties, TimeSpan timeout, Func<OAuthTokens, int, TProperties, TwitterResponse<TResponse>> methodToCall, Action<TwitterAsyncResponse<TResponse>> function)
            where TResponse : ITwitterObject
            where TProperties : OptionalProperties
        {
#if !SILVERLIGHT
            return methodToCall.BeginInvoke(tokens, i, properties, result => TwoParamsCallback(result, timeout, methodToCall, function), null);
#else
            ThreadPool.QueueUserWorkItem(result => TwoParamsCallback(methodToCall(tokens, i, properties), timeout, methodToCall, function));
            return null;
#endif
        }

        #endregion

        #region One String parameter

        public static IAsyncResult ExecuteAsyncMethod<TResponse>(OAuthTokens tokens, string s, TimeSpan timeout, Func<OAuthTokens, string, TwitterResponse<TResponse>> methodToCall, Action<TwitterAsyncResponse<TResponse>> function)
            where TResponse : ITwitterObject
        {
#if !SILVERLIGHT
            return methodToCall.BeginInvoke(tokens, s, result => OneParamCallback(result, timeout, methodToCall, function), null);
#else
            ThreadPool.QueueUserWorkItem(result => OneParamCallback(methodToCall(tokens, s), timeout, methodToCall, function));
            return null;
#endif
        }

        public static IAsyncResult ExecuteAsyncMethod<TResponse, TProperties>(OAuthTokens tokens, string s, TProperties properties, TimeSpan timeout, Func<OAuthTokens, string, TProperties, TwitterResponse<TResponse>> methodToCall, Action<TwitterAsyncResponse<TResponse>> function)
            where TResponse : ITwitterObject
            where TProperties : OptionalProperties
        {
#if !SILVERLIGHT
            return methodToCall.BeginInvoke(tokens, s, properties, result => TwoParamsCallback(result, timeout, methodToCall, function), null);
#else
            ThreadPool.QueueUserWorkItem(result => TwoParamsCallback(methodToCall(tokens, s, properties), timeout, methodToCall, function));
            return null;
#endif
        }

        #endregion

        #region Two String parameters

        public static IAsyncResult ExecuteAsyncMethod<TResponse>(OAuthTokens tokens, string s1, string s2, TimeSpan timeout, Func<OAuthTokens, string, string, TwitterResponse<TResponse>> methodToCall, Action<TwitterAsyncResponse<TResponse>> function)
            where TResponse : ITwitterObject
        {
#if !SILVERLIGHT
            return methodToCall.BeginInvoke(tokens, s1, s2, result => TwoParamsCallback(result, timeout, methodToCall, function), null);
#else
            ThreadPool.QueueUserWorkItem(result => TwoParamsCallback(methodToCall(tokens, s1, s2), timeout, methodToCall, function));
            return null;
#endif
        }

        public static IAsyncResult ExecuteAsyncMethod<TResponse, TProperties>(OAuthTokens tokens, string s1, string s2, TProperties properties, TimeSpan timeout, Func<OAuthTokens, string, string, TProperties, TwitterResponse<TResponse>> methodToCall, Action<TwitterAsyncResponse<TResponse>> function)
            where TResponse : ITwitterObject
            where TProperties : OptionalProperties
        {
#if !SILVERLIGHT
            return methodToCall.BeginInvoke(tokens, s1, s2, properties, result => ThreeParamsCallback(result, timeout, methodToCall, function), null);
#else
            ThreadPool.QueueUserWorkItem(result => ThreeParamsCallback(methodToCall(tokens, s1, s2, properties), timeout, methodToCall, function));
            return null;
#endif
        }

        public static IAsyncResult ExecuteAsyncMethod<TResponse, TProperties>(OAuthTokens tokens, string s1, string s2, decimal d, TProperties properties, TimeSpan timeout, Func<OAuthTokens, string, string, decimal, TProperties, TwitterResponse<TResponse>> methodToCall, Action<TwitterAsyncResponse<TResponse>> function)
            where TResponse : ITwitterObject
            where TProperties : OptionalProperties
        {
#if !SILVERLIGHT
            return methodToCall.BeginInvoke(tokens, s1, s2, d, properties, result => FourParamsCallback(result, timeout, methodToCall, function), null);
#else
            ThreadPool.QueueUserWorkItem(result => FourParamsCallback(methodToCall(tokens, s1, s2, d, properties), timeout, methodToCall, function));
            return null;
#endif
        }

        public static IAsyncResult ExecuteAsyncMethod<TResponse, TProperties>(OAuthTokens tokens, string s1, bool b, string s2, TProperties properties, TimeSpan timeout, Func<OAuthTokens, string, bool, string, TProperties, TwitterResponse<TResponse>> methodToCall, Action<TwitterAsyncResponse<TResponse>> function)
            where TResponse : ITwitterObject
            where TProperties : OptionalProperties
        {
#if !SILVERLIGHT
            return methodToCall.BeginInvoke(tokens, s1, b, s2, properties, result => FourParamsCallback(result, timeout, methodToCall, function), null);
#else
            ThreadPool.QueueUserWorkItem(result => FourParamsCallback(methodToCall(tokens, s1, b, s2, properties), timeout, methodToCall, function));
            return null;
#endif
        }

        #endregion

        #region One Decimal parameter

        public static IAsyncResult ExecuteAsyncMethod<TResponse>(OAuthTokens tokens, decimal d, TimeSpan timeout, Func<OAuthTokens, decimal, TwitterResponse<TResponse>> methodToCall, Action<TwitterAsyncResponse<TResponse>> function)
            where TResponse : ITwitterObject
        {
#if !SILVERLIGHT
            return methodToCall.BeginInvoke(tokens, d, result => OneParamCallback(result, timeout, methodToCall, function), null);
#else
            ThreadPool.QueueUserWorkItem(result => OneParamCallback(methodToCall(tokens, d), timeout, methodToCall, function));
            return null;
#endif
        }

        public static IAsyncResult ExecuteAsyncMethod<TResponse, TProperties>(OAuthTokens tokens, decimal d, TProperties properties, TimeSpan timeout, Func<OAuthTokens, decimal, TProperties, TwitterResponse<TResponse>> methodToCall, Action<TwitterAsyncResponse<TResponse>> function)
            where TResponse : ITwitterObject
            where TProperties : OptionalProperties
        {
#if !SILVERLIGHT
            return methodToCall.BeginInvoke(tokens, d, properties, result => TwoParamsCallback(result, timeout, methodToCall, function), null);
#else
            ThreadPool.QueueUserWorkItem(result => TwoParamsCallback(methodToCall(tokens, d, properties), timeout, methodToCall, function));
            return null;
#endif
        }

        #endregion

        #region One Byte[] parameter

        public static IAsyncResult ExecuteAsyncMethod<TResponse>(OAuthTokens tokens, byte[] b, TimeSpan timeout, Func<OAuthTokens, byte[], TwitterResponse<TResponse>> methodToCall, Action<TwitterAsyncResponse<TResponse>> function)
            where TResponse : ITwitterObject
        {
#if !SILVERLIGHT
            return methodToCall.BeginInvoke(tokens, b, result => OneParamCallback(result, timeout, methodToCall, function), null);
#else
            ThreadPool.QueueUserWorkItem(result => OneParamCallback(methodToCall(tokens, b), timeout, methodToCall, function));
            return null;
#endif
        }

        public static IAsyncResult ExecuteAsyncMethod<TResponse, TProperties>(OAuthTokens tokens, byte[] b, TProperties properties, TimeSpan timeout, Func<OAuthTokens, byte[], TProperties, TwitterResponse<TResponse>> methodToCall, Action<TwitterAsyncResponse<TResponse>> function)
            where TResponse : ITwitterObject
            where TProperties : OptionalProperties
        {
#if !SILVERLIGHT
            return methodToCall.BeginInvoke(tokens, b, properties, result => TwoParamsCallback(result, timeout, methodToCall, function), null);
#else
            ThreadPool.QueueUserWorkItem(result => TwoParamsCallback(methodToCall(tokens, b, properties), timeout, methodToCall, function));
            return null;
#endif
        }

        #endregion
    }
}