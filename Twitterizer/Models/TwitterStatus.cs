//-----------------------------------------------------------------------
// <copyright file="TwitterStatus.cs" company="Patrick 'Ricky' Smith">
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
// <summary>The TwitterStatus class</summary>
//-----------------------------------------------------------------------
namespace Twitterizer.Models
{
    using System;
    using System.Linq;
    using System.Diagnostics;
    using Newtonsoft.Json;
    using Twitterizer.Core;
    using System.Runtime.Serialization;
    using Newtonsoft.Json.Converters;

    /// <include file='TwitterStatus.xml' path='TwitterStatus/TwitterStatus/*'/>
    [JsonObject(MemberSerialization.OptIn)]
    [DebuggerDisplay("{User.ScreenName}/{Text}")]
    [DataContract]
    public class TwitterStatus : TwitterStatusBase
    {
        #region Properties
        /// <summary>
        /// Gets or sets a value indicating whether this status message is truncated.
        /// </summary>
        /// <value>
        /// <c>true</c> if this status message is truncated; otherwise, <c>false</c>.
        /// </value>
        [DataMember, JsonProperty(PropertyName = "truncated")]
        public bool? IsTruncated { get; set; }        

        /// <summary>
        /// Gets or sets the screenName the status is in reply to.
        /// </summary>
        /// <value>The screenName.</value>
        [DataMember, JsonProperty(PropertyName = "in_reply_to_screen_name")]
        public string InReplyToScreenName { get; set; }

        /// <summary>
        /// Gets or sets the user id the status is in reply to.
        /// </summary>
        /// <value>The user id.</value>
        [DataMember, JsonProperty(PropertyName = "in_reply_to_user_id")]
        public long? InReplyToUserId { get; set; }

        /// <summary>
        /// Gets or sets the status id the status is in reply to.
        /// </summary>
        /// <value>The status id.</value>
        [DataMember, JsonProperty(PropertyName = "in_reply_to_status_id")]
        public decimal? InReplyToStatusId { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the authenticated user has favorited this status.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is favorited; otherwise, <c>false</c>.
        /// </value>
        [DataMember, JsonProperty(PropertyName = "favorited")]
        public bool? IsFavorited { get; set; }        

        /// <summary>
        /// Gets or sets the user.
        /// </summary>
        /// <value>The user that posted this status.</value>
        [DataMember, JsonProperty(PropertyName = "user")]
        public TwitterUser User { get; set; }

        /// <summary>
        /// Gets or sets the retweeted status.
        /// </summary>
        /// <value>The retweeted status.</value>
        [DataMember, JsonProperty(PropertyName = "retweeted_status")]
        public TwitterStatus RetweetedStatus { get; set; }

        /// <summary>
        /// Gets or sets the place.
        /// </summary>
        /// <value>The place.</value>
        [DataMember, JsonProperty(PropertyName = "place")]
        public TwitterPlace Place { get; set; }

        /// <summary>
        /// Gets or sets the retweet count string.
        /// </summary>
        /// <value>The retweet count.</value>
        [DataMember, JsonProperty(PropertyName = "retweet_count")]
        public string RetweetCountString { get; set; }

        /// <summary>
        /// Gets the retweet count.
        /// </summary>
        /// <value>The retweet count.</value>
        [DataMember]
        public int? RetweetCount
        {
            get
            {
                if (string.IsNullOrEmpty(this.RetweetCountString)) return null;

                int parsedResult;

                if (
                    this.RetweetCountString.EndsWith("+") &&
                    !int.TryParse(this.RetweetCountString.Substring(0, this.RetweetCountString.Length - 1), out parsedResult)
                    )
                {
                    return null;
                }

                if (!int.TryParse(this.RetweetCountString, out parsedResult))
                {
                    return null;
                }

                return parsedResult;
            }
        }

        /// <summary>
        /// Gets a value indicating that the number of retweets exceeds the reported value in RetweetCount. For example, "more than 100"
        /// </summary>
        /// <value>The retweet count plus indicator.</value>
        [DataMember]
        public bool? RetweetCountPlus
        {
            get
            {
                if (string.IsNullOrEmpty(this.RetweetCountString)) return null;

                return this.RetweetCountString.EndsWith("+");
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="TwitterStatus"/> is retweeted.
        /// </summary>
        /// <value><c>true</c> if retweeted; otherwise, <c>false</c>.</value>
        [DataMember, JsonProperty(PropertyName = "retweeted")]
        public bool Retweeted { get; set; }
        #endregion

        
    }
}
