﻿//-----------------------------------------------------------------------
// <copyright file="TwitterizerException.cs" company="Patrick 'Ricky' Smith">
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
//-----------------------------------------------------------------------

namespace Twitterizer
{
    using System;
    using System.IO;
    using System.Net;
    using System.Runtime.Serialization;
    using System.Runtime.Serialization.Json;
    using System.Text;
    using Twitterizer.Core;

    /// <summary>
    /// The Twitterizer Exception
    /// </summary>
    /// <seealso cref="System.Net.WebException"/>
    public class TwitterizerException : WebException
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="TwitterizerException"/> class.
        /// </summary>
        public TwitterizerException() 
            : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TwitterizerException"/> class.
        /// </summary>
        /// <param name="wex">The <see cref="System.Net.WebException"/>.</param>
        public TwitterizerException(WebException wex)
            : this(wex.Message, wex)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TwitterizerException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="wex">The <see cref="System.Net.WebException"/>.</param>
        public TwitterizerException(string message, WebException wex)
            : base(message)
        {
            HttpWebResponse response = (HttpWebResponse)wex.Response;

            this.ResponseBody = new StreamReader(response.GetResponseStream()).ReadToEnd();

            this.ParseRateLimitHeaders(response);

            DataContractJsonSerializer ds = new DataContractJsonSerializer(typeof(TwitterErrorDetails));
            wex.Response.GetResponseStream().Seek(0, SeekOrigin.Begin);
            this.ErrorDetails = ds.ReadObject(wex.Response.GetResponseStream()) as TwitterErrorDetails;
        }
        #endregion

        /// <summary>
        /// Gets or sets the response body.
        /// </summary>
        /// <value>The response body.</value>
        public string ResponseBody { get; protected set; }

        /// <summary>
        /// Gets or sets the rate limits.
        /// </summary>
        /// <value>The rate limits.</value>
        public RateLimiting RateLimiting { get; protected set; }

        /// <summary>
        /// Gets or sets the error details.
        /// </summary>
        /// <value>The error details.</value>
        public TwitterErrorDetails ErrorDetails { get; protected set; }

        /// <summary>
        /// Gets the response that the remote host returned.
        /// </summary>
        /// <value></value>
        /// <returns>If a response is available from the Internet resource, a <see cref="T:System.Net.WebResponse"/> instance that contains the error response from an Internet resource; otherwise, null.</returns>
        public new WebResponse Response
        {
            get
            {
                if (this.InnerException == null)
                    return null;

                return ((WebException)this.InnerException).Response;
            }
        }

        /// <summary>
        /// Gets the bug report.
        /// </summary>
        /// <value>The bug report.</value>
        public string BugReport
        {
            get
            {
                StringBuilder reportBuilder = new StringBuilder();
                reportBuilder.AppendFormat(
@"
--------------- ERROR MESSAGE ---------------
{0}

--------------- STACK TRACE -----------------
{1}

--------------- RESPONSE BODY ---------------
{2}
",
                    this.Message,
                    this.StackTrace,
                    this.ResponseBody);

                reportBuilder.Append("--------------- HTTP HEADERS ----------------");
                for (int i = 0; i < this.Response.Headers.Count; i++)
                {
                    reportBuilder.AppendFormat(
                        "{0} = \"{1}\"",
                        this.Response.Headers.AllKeys[i],
                        this.Response.Headers[i]);
                }

                return reportBuilder.ToString();
            }
        }

        /// <summary>
        /// Parses the rate limit headers.
        /// </summary>
        /// <param name="response">The response.</param>
        protected void ParseRateLimitHeaders(HttpWebResponse response)
        {
            this.RateLimiting = new RateLimiting();

            if (!string.IsNullOrEmpty(response.Headers.Get("X-RateLimit-Limit")))
            {
                this.RateLimiting.Total = int.Parse(response.Headers.Get("X-RateLimit-Limit"));
            }

            if (!string.IsNullOrEmpty(response.Headers.Get("X-RateLimit-Remaining")))
            {
                this.RateLimiting.Remaining = int.Parse(response.Headers.Get("X-RateLimit-Remaining"));
            }

            if (!string.IsNullOrEmpty(response.Headers["X-RateLimit-Reset"]))
            {
                this.RateLimiting.ResetDate = (new DateTime(1970, 1, 1, 0, 0, 0, 0))
                    .AddSeconds(double.Parse(response.Headers.Get("X-RateLimit-Reset")));
            }
        }

        /// <summary>
        /// Twitter Error Details class
        /// </summary>
        /// <remarks>Often, twitter returns error details in the body of response. This class represents the data structure of the error for deserialization.</remarks>
        [DataContract]
        public class TwitterErrorDetails
        {
            /// <summary>
            /// Gets or sets the request path.
            /// </summary>
            /// <value>The request path.</value>
            [DataMember(Name = "request")]
            public string RequestPath { get; set; }

            /// <summary>
            /// Gets or sets the error message.
            /// </summary>
            /// <value>The error message.</value>
            [DataMember(Name = "error")]
            public string ErrorMessage { get; set; }
        }
    }
}
