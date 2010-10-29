//-----------------------------------------------------------------------
// <copyright file="OAuthUtilityAsync.cs" company="Patrick 'Ricky' Smith">
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
// <summary>Provides simple methods to simplify OAuth interaction.</summary>
//-----------------------------------------------------------------------
namespace Twitterizer
{
    using System;
    using System.Net;
    using Twitterizer;

    public static class OAuthUtilityAsync
    {
        /// <summary>
        /// Gets a new OAuth request token from the twitter api.
        /// </summary>
        /// <param name="consumerKey">The consumer key.</param>
        /// <param name="consumerSecret">The consumer secret.</param>
        /// <param name="callbackAddress">Address of the callback.</param>
        /// <param name="proxy">The proxy.</param>
        /// <param name="timeout">The timeout.</param>
        /// <param name="function">The function.</param>
        /// <returns>
        /// A new <see cref="Twitterizer.OAuthTokenResponse"/> instance.
        /// </returns>
        public static IAsyncResult GetRequestToken(string consumerKey, string consumerSecret, string callbackAddress, WebProxy proxy, TimeSpan timeout, Action<OAuthTokenResponse> function)
        {
            Func<string, string, string, WebProxy, OAuthTokenResponse> methodToCall = OAuthUtility.GetRequestToken;

            return methodToCall.BeginInvoke(
                consumerKey,
                consumerSecret,
                callbackAddress,
                proxy,
                result =>
                {
                    result.AsyncWaitHandle.WaitOne(timeout);
                    try
                    {
                        function(methodToCall.EndInvoke(result));
                    }
                    catch (Exception ex)
                    {
                        function(null);
                    }
                },
                null);
        }

        /// <summary>
        /// Gets the access token.
        /// </summary>
        /// <param name="consumerKey">The consumer key.</param>
        /// <param name="consumerSecret">The consumer secret.</param>
        /// <param name="requestToken">The request token.</param>
        /// <param name="verifier">The verifier.</param>
        /// <param name="proxy">The proxy.</param>
        /// <param name="timeout">The timeout.</param>
        /// <param name="function">The function.</param>
        /// <returns>An <see cref="OAuthTokenResponse"/> class containing access token information.</returns>
        public static IAsyncResult GetAccessToken(
            string consumerKey, 
            string consumerSecret, 
            string requestToken, 
            string verifier, 
            WebProxy proxy, 
            TimeSpan timeout, 
            Action<OAuthTokenResponse> function)
        {
            Func<string, string, string, string, WebProxy, OAuthTokenResponse> methodToCall = OAuthUtility.GetAccessToken;

            return methodToCall.BeginInvoke(
                consumerKey,
                consumerSecret,
                requestToken,
                verifier,
                proxy,
                result =>
                {
                    result.AsyncWaitHandle.WaitOne(timeout);
                    try
                    {
                        function(methodToCall.EndInvoke(result));
                    }
                    catch (Exception ex)
                    {
                        function(null);
                    }
                },
                null);
        }
    }
}
