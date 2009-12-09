/*
 * This file is part of the Twitterizer library <http://code.google.com/p/twitterizer/>
 *
 * Copyright (c) 2008, Patrick "Ricky" Smith <ricky@digitally-born.com>
 * All rights reserved.
 * 
 * Redistribution and use in source and binary forms, with or without modification, are 
 * permitted provided that the following conditions are met:
 *
 * - Redistributions of source code must retain the above copyright notice, this list 
 *   of conditions and the following disclaimer.
 * - Redistributions in binary form must reproduce the above copyright notice, this list 
 *   of conditions and the following disclaimer in the documentation and/or other 
 *   materials provided with the distribution.
 * - Neither the name of the Twitterizer nor the names of its contributors may be 
 *   used to endorse or promote products derived from this software without specific 
 *   prior written permission.
 *
 * THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND 
 * ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED 
 * WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. 
 * IN NO EVENT SHALL THE COPYRIGHT OWNER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, 
 * INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT 
 * NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR 
 * PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, 
 * WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) 
 * ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE 
 * POSSIBILITY OF SUCH DAMAGE.
 */
using System;
using System.Text.RegularExpressions;

namespace Twitterizer.Framework
{
    public class TwitterizerException : Exception
    {
        /// <summary>
        /// Contains the Request Data that is used in the Twitter API request.
        /// </summary>
        /// <value>The request data.</value>
        public TwitterRequestData RequestData { get; set; }

        /// <summary>
        /// Contains the raw xml returned by the Twitter API.
        /// </summary>
        /// <value>The raw XML.</value>
        public string RawXML
        {
            get
            {
                return (RequestData == null ? string.Empty : RequestData.Response);
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TwitterizerException"/> class.
        /// </summary>
        /// <param name="Message">The message.</param>
        /// <param name="RequestData">The request data.</param>
        public TwitterizerException(string Message, TwitterRequestData RequestData)
            : base(Message)
        {
            this.RequestData = RequestData;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TwitterizerException"/> class.
        /// </summary>
        /// <param name="Message">The message.</param>
        /// <param name="RequestData">The request data.</param>
        /// <param name="InnerException">The inner exception.</param>
        public TwitterizerException(string Message, TwitterRequestData RequestData, Exception InnerException)
            : base(Message, InnerException)
        {
            this.RequestData = RequestData;
        }

        /// <summary>
        /// Parses the error message.
        /// </summary>
        public static string ParseErrorMessage(string XML)
        {
            if (string.IsNullOrEmpty(XML))
                return string.Empty;

            if (!Regex.IsMatch(XML, "<error>.+</error>", RegexOptions.IgnoreCase))
                return string.Empty;

            return Regex.Match(XML, "(?:<error>).+(?:</error>)", RegexOptions.IgnoreCase).Value;
        }
    }
}
