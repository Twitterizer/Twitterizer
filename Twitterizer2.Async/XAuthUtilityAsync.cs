//-----------------------------------------------------------------------
// <copyright file="XAuthUtilityAsync.cs" company="Patrick 'Ricky' Smith">
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
// <summary>Provides simple methods to simplify XAuth interaction.</summary>
//-----------------------------------------------------------------------
namespace Twitterizer
{
    using System;
#if SILVERLIGHT
    using System.Threading;
#endif

    /// <summary>
    /// An asynchronous wrapper around the <see cref="XAuthUtility"/> class.
    /// </summary>
    public static class XAuthUtilityAsync
    {
        /// <summary>
        /// Allows OAuth applications to directly exchange Twitter usernames and passwords for OAuth access tokens and secrets.
        /// </summary>
        /// <param name="consumerKey">The consumer key.</param>
        /// <param name="consumerSecret">The consumer secret.</param>
        /// <param name="username">The username.</param>
        /// <param name="password">The password.</param>
        /// <param name="timeout">The timeout.</param>
        /// <param name="function">The function.</param>
        /// <returns>A <see cref="OAuthTokenResponse"/> instance.</returns>        
        public static IAsyncResult GetAccessTokens(string consumerKey, string consumerSecret, string username, string password, TimeSpan timeout, Action<OAuthTokenResponse> function)
        {
#if !SILVERLIGHT            
            Func<string, string, string, string, OAuthTokenResponse> methodToCall = XAuthUtility.GetAccessTokens;

            return methodToCall.BeginInvoke(
                consumerKey,
                consumerSecret,
                username,
                password,
                result =>
                {
                    result.AsyncWaitHandle.WaitOne(timeout);
                    try
                    {
                        function(methodToCall.EndInvoke(result));
                    }
                    catch (Exception)
                    {
                        function(null);
                    }
                },
                null);
#else
            ThreadPool.QueueUserWorkItem(x => function(XAuthUtility.GetAccessTokens(consumerKey, consumerSecret, username, password)));
            return null;
#endif
        }
    }
}
