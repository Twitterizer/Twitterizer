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
    using System.Xml.Serialization;

    /// <summary>
    /// The twitter status class. Provides thread-safe information about the last request made.
    /// </summary>
    [Serializable]
    public sealed class RequestStatus
    {
        /// <summary>
        /// The last request status
        /// </summary>
        private static RequestStatus lastRequestStatus = null;

        /// <summary>
        /// Initializes a new instance of the <see cref="RequestStatus"/> class.
        /// </summary>
        internal RequestStatus()
        {
            this.Status = RequestStatuses.Success;
        }

        /// <summary>
        /// Gets or sets the last request status.
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
        /// Gets or sets the full path.
        /// </summary>
        /// <value>The full path.</value>
        public string FullPath { get; internal set; }

        /// <summary>
        /// Gets or sets the error details.
        /// </summary>
        /// <value>The error details.</value>
        public TwitterErrorDetails ErrorDetails { get; internal set; }

        /// <summary>
        /// Gets or sets the response body.
        /// </summary>
        /// <value>The response body.</value>
        public string ResponseBody { get; internal set; }

        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        /// <value>The status.</value>
        public RequestStatuses Status { get; internal set; }

        /// <summary>
        /// A lock for the last status instance
        /// </summary>
        static readonly object padlock = new object();

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

            RequestStatus newStatus = new RequestStatus();

            switch (webResponse.StatusCode)
            {
                case HttpStatusCode.OK:
                    newStatus.Status = RequestStatuses.Success;
                    lastRequestStatus = newStatus;
                    return true;
                    
                case HttpStatusCode.BadRequest:
                    newStatus.Status = RequestStatuses.BadRequest;
                    break;

                case HttpStatusCode.Unauthorized:
                    newStatus.Status = RequestStatuses.Unauthorized;
                    break;
                
                case HttpStatusCode.Forbidden:
                    newStatus.Status = RequestStatuses.RateLimited;
                    break;
                
                default:
                    return false;
            }

            if (webResponse.ContentType.ToLower() == "text/xml")
            {
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(TwitterErrorDetails));
                lastRequestStatus.ErrorDetails = xmlSerializer.Deserialize(webResponse.GetResponseStream()) as TwitterErrorDetails;
            }

            if (webResponse.ContentType.ToLower() == "application/json")
            {
                DataContractJsonSerializer ds = new DataContractJsonSerializer(typeof(TwitterErrorDetails));
                lastRequestStatus.ErrorDetails = ds.ReadObject(webResponse.GetResponseStream()) as TwitterErrorDetails;
            }

            lastRequestStatus = newStatus;

            return true;
        }
    }

    /// <summary>
    /// Describes the result status of a request
    /// </summary>
    public enum RequestStatuses
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
}
