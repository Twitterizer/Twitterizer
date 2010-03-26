//-----------------------------------------------------------------------
// <copyright file="RequestStatus.cs" company="Patrick 'Ricky' Smith">
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
// <summary>The twitter status class. Provides information about the last request made.</summary>
//-----------------------------------------------------------------------

namespace Twitterizer
{
    using System;
    using System.Net;
    using System.Runtime.Serialization.Json;
    using System.Text;
    using System.Xml.Serialization;

    /// <summary>
    /// Describes the result status of a request
    /// </summary>
    public enum RequestResult
    {
        /// <summary>
        /// The request was completed successfully
        /// </summary>
        Success,

        /// <summary>
        /// The URI requested is invalid or the resource requested, such as a user, does not exists.
        /// </summary>
        FileNotFound,

        /// <summary>
        /// The request was invalid.  An accompanying error message will explain why.
        /// </summary>
        BadRequest,

        /// <summary>
        /// Authentication credentials were missing or incorrect.
        /// </summary>
        Unauthorized,

        /// <summary>
        /// Returned by the Search API when an invalid format is specified in the request.
        /// </summary>
        NotAcceptable,

        /// <summary>
        /// The authorized user, or client IP address, is being rate limited.
        /// </summary>
        RateLimited,

        /// <summary>
        /// Twitter is currently down.
        /// </summary>
        TwitterIsDown,

        /// <summary>
        /// Twitter is online, but is overloaded. Try again later.
        /// </summary>
        TwitterIsOverloaded,

        /// <summary>
        /// Something unexpected happened. See the error message for additional information.
        /// </summary>
        Unknown
    }

    /// <summary>
    /// The twitter status class. Provides thread-safe information about the last request made.
    /// </summary>
    [Serializable]
    public sealed class RequestStatus
    {
        /// <summary>
        /// A lock for the last status instance
        /// </summary>
        private static readonly object padlock = new object();

        /// <summary>
        /// The last request status
        /// </summary>
        private static RequestStatus lastRequestStatus;

        /// <summary>
        /// Initializes a new instance of the <see cref="RequestStatus"/> class.
        /// </summary>
        internal RequestStatus()
        {
            this.Status = RequestResult.Success;
        }

        /// <summary>
        /// Gets the last request status.
        /// </summary>
        /// <value>The last request status.</value>
        public static RequestStatus LastRequestStatus
        {
            get
            {
                lock (padlock)
                {
                    if (lastRequestStatus == null)
                    {
                        lastRequestStatus = new RequestStatus();
                    }

                    return lastRequestStatus;
                }
            }
        }

        /// <summary>
        /// Gets the full path.
        /// </summary>
        /// <value>The full path.</value>
        public string FullPath { get; internal set; }

        /// <summary>
        /// Gets the error details.
        /// </summary>
        /// <value>The error details.</value>
        public TwitterErrorDetails ErrorDetails { get; internal set; }

        /// <summary>
        /// Gets the response body.
        /// </summary>
        /// <value>The response body.</value>
        public string ResponseBody { get; internal set; }

        /// <summary>
        /// Gets the status.
        /// </summary>
        /// <value>The status.</value>
        public RequestResult Status { get; internal set; }

        /// <summary>
        /// Updates the request status.
        /// </summary>
        /// <param name="webResponse">The web response.</param>
        /// <returns><c>true</c> if the status was updated successfully, otherwise <c>false</c></returns>
        public static bool UpdateRequestStatus(HttpWebResponse webResponse)
        {
            if (webResponse == null)
            {
                return false;
            }

            System.IO.Stream responseStream = webResponse.GetResponseStream();

            if (responseStream != null && responseStream.CanRead)
            {
                LastRequestStatus.ResponseBody = Encoding.UTF8.GetString(WebResponseUtility.ReadStream(responseStream));
            }

            LastRequestStatus.FullPath = webResponse.ResponseUri.AbsolutePath;

            switch (webResponse.StatusCode)
            {
                case HttpStatusCode.OK:
                    LastRequestStatus.Status = RequestResult.Success;
                    return true;

                case HttpStatusCode.BadRequest:
                    LastRequestStatus.Status = RequestResult.BadRequest;
                    break;

                case HttpStatusCode.Unauthorized:
                    LastRequestStatus.Status = RequestResult.Unauthorized;
                    break;

                case HttpStatusCode.Forbidden:
                    LastRequestStatus.Status = RequestResult.RateLimited;
                    break;

                default:
                    LastRequestStatus.Status = RequestResult.Unknown;
                    return false;
            }

            try
            {
                if (webResponse.ContentType.StartsWith("text/xml", StringComparison.OrdinalIgnoreCase))
                {
                    XmlSerializer xmlSerializer = new XmlSerializer(typeof(TwitterErrorDetails));
                    LastRequestStatus.ErrorDetails = xmlSerializer.Deserialize(webResponse.GetResponseStream()) as TwitterErrorDetails;
                }

                if (webResponse.ContentType.StartsWith("application/json", StringComparison.OrdinalIgnoreCase))
                {
                    DataContractJsonSerializer ds = new DataContractJsonSerializer(typeof(TwitterErrorDetails));
                    LastRequestStatus.ErrorDetails = ds.ReadObject(webResponse.GetResponseStream()) as TwitterErrorDetails;
                }
            }
            catch (InvalidOperationException)
            {
                // Do nothing. This is no-fail code.
            }

            return true;
        }
    }
}
