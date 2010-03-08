//-----------------------------------------------------------------------
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
    using System.Globalization;
    using System.IO;
    using System.Net;
    using System.Runtime.Serialization;
    using System.Runtime.Serialization.Json;
    using System.Security.Permissions;
    using System.Text;
    using Twitterizer.Core;

    /// <summary>
    /// The Twitterizer Exception
    /// </summary>
    /// <seealso cref="System.Net.WebException"/>
    [Serializable]
    public class TwitterizerException : WebException, ISerializable
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="TwitterizerException"/> class.
        /// </summary>
        public TwitterizerException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TwitterizerException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        public TwitterizerException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TwitterizerException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="innerException">The inner exception.</param>
        public TwitterizerException(string message, Exception innerException) :
            base(message, innerException)
        {
            if (innerException.GetType() == typeof(WebException))
            {
                WebException webException = (WebException)innerException;

                HttpWebResponse response = (HttpWebResponse)webException.Response;

                this.ResponseBody = new StreamReader(response.GetResponseStream()).ReadToEnd();

#if DEBUG
                System.Diagnostics.Debug.WriteLine("----------- RESPONSE -----------");
                System.Diagnostics.Debug.Write(this.ResponseBody);
                System.Diagnostics.Debug.WriteLine("----------- END -----------");
#endif
                this.ParseRateLimitHeaders(response);
                try
                {
                    DataContractJsonSerializer ds = new DataContractJsonSerializer(typeof(TwitterErrorDetails));
                    webException.Response.GetResponseStream().Seek(0, SeekOrigin.Begin);
                    this.ErrorDetails = ds.ReadObject(webException.Response.GetResponseStream()) as TwitterErrorDetails;
                }
                catch (System.Runtime.Serialization.SerializationException)
                {
                    // Try to deserialize as XML (specifically OAuth requests)
                    System.Xml.Serialization.XmlSerializer ds =
                        new System.Xml.Serialization.XmlSerializer(typeof(TwitterErrorDetails));

                    webException.Response.GetResponseStream().Seek(0, SeekOrigin.Begin);
                    this.ErrorDetails = ds.Deserialize(webException.Response.GetResponseStream()) as TwitterErrorDetails;
                }
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TwitterizerException"/> class.
        /// </summary>
        /// <param name="info">The object that holds the serialized object data.</param>
        /// <param name="context">The contextual information about the source or destination.</param>
        protected TwitterizerException(
            SerializationInfo info,
            StreamingContext context)
            : base(info, context)
        {
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

        #region ISerializable Members
        /// <summary>
        /// When overridden in a derived class, sets the <see cref="T:System.Runtime.Serialization.SerializationInfo"/> with information about the exception.
        /// </summary>
        /// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo"/> that holds the serialized object data about the exception being thrown.</param>
        /// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext"/> that contains contextual information about the source or destination.</param>
        /// <exception cref="T:System.ArgumentNullException">
        /// The <paramref name="info"/> parameter is a null reference (Nothing in Visual Basic).
        /// </exception>
        /// <PermissionSet>
        /// <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Read="*AllFiles*" PathDiscovery="*AllFiles*"/>
        /// <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="SerializationFormatter"/>
        /// </PermissionSet>
        [SecurityPermissionAttribute(SecurityAction.LinkDemand, SerializationFormatter = true)]
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            if (info == null)
                throw new ArgumentNullException("info");

            base.GetObjectData(info, context);
        }
        #endregion

        /// <summary>
        /// Parses the rate limit headers.
        /// </summary>
        /// <param name="response">The response.</param>
        protected void ParseRateLimitHeaders(WebResponse response)
        {
            this.RateLimiting = new RateLimiting();

            if (!string.IsNullOrEmpty(response.Headers.Get("X-RateLimit-Limit")))
            {
                this.RateLimiting.Total = int.Parse(response.Headers.Get("X-RateLimit-Limit"), CultureInfo.InvariantCulture);
            }

            if (!string.IsNullOrEmpty(response.Headers.Get("X-RateLimit-Remaining")))
            {
                this.RateLimiting.Remaining = int.Parse(response.Headers.Get("X-RateLimit-Remaining"), CultureInfo.InvariantCulture);
            }

            if (!string.IsNullOrEmpty(response.Headers["X-RateLimit-Reset"]))
            {
                this.RateLimiting.ResetDate = (new DateTime(1970, 1, 1, 0, 0, 0, 0))
                    .AddSeconds(double.Parse(response.Headers.Get("X-RateLimit-Reset"), CultureInfo.InvariantCulture));
            }
        }
    }
}
