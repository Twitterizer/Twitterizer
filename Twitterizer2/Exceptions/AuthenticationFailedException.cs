﻿//-----------------------------------------------------------------------
// <copyright file="AuthenticationFailedException.cs" company="Patrick 'Ricky' Smith">
//  This file is part of the Twitterizer library (http://code.google.com/p/twitterizer/)
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
// <summary>The exception that indicates an API called failed due to authentication failure.</summary>
//-----------------------------------------------------------------------

namespace Twitterizer
{
    using System;
    using System.IO;
    using System.Net;
    using System.Runtime.Serialization;

    /// <summary>
    /// The exception that indicates an API called failed due to authentication failure.
    /// </summary>
    [Serializable]
    public class AuthenticationFailedException : TwitterizerException
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="AuthenticationFailedException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="innerException">The inner exception.</param>
        public AuthenticationFailedException(string message, WebException innerException)
            : base(message, innerException)
        {
            HttpWebResponse response = (HttpWebResponse)innerException.Response;
            this.ResponseBody = new StreamReader(response.GetResponseStream()).ReadToEnd();

            this.ParseRateLimitHeaders(response);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthenticationFailedException"/> class.
        /// </summary>
        /// <param name="innerException">The inner exception.</param>
        public AuthenticationFailedException(WebException innerException)
            : base("Authentication failed", innerException)
        {
            HttpWebResponse response = (HttpWebResponse)innerException.Response;
            this.ResponseBody = new StreamReader(response.GetResponseStream()).ReadToEnd();

            this.ParseRateLimitHeaders(response);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthenticationFailedException"/> class.
        /// </summary>
        public AuthenticationFailedException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthenticationFailedException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        public AuthenticationFailedException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthenticationFailedException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="innerException">The inner exception.</param>
        public AuthenticationFailedException(string message, Exception innerException) :
            base(message, innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthenticationFailedException"/> class.
        /// </summary>
        /// <param name="info">The serialization info.</param>
        /// <param name="context">The context.</param>
        protected AuthenticationFailedException(
            SerializationInfo info,
            StreamingContext context)
            : base(info, context)
        {
        }
        #endregion
    }
}
