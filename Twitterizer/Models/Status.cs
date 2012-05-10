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
using System.Collections.ObjectModel;

    /// <include file='TwitterStatus.xml' path='TwitterStatus/TwitterStatus/*'/>
    [JsonObject(MemberSerialization.OptIn)]
    [DebuggerDisplay("{User.ScreenName}/{Text}")]
    [DataContract]
    public class Status : StatusBase
    {
        /// <summary>
        ///  An collection of brief user objects (usually only one) indicating users who contributed to the authorship of the tweet, on behalf of the official tweet author.
        /// </summary>
        [DataMember, JsonProperty(PropertyName = "contributors", NullValueHandling = NullValueHandling.Ignore)]
        public Collection<Contributors> Contributors { get; set; }

        /// <summary>
        /// Only surfaces on methods supporting the include_my_retweet parameter, when set to true. Details the Tweet ID of the user's own retweet (if existent) of this Tweet.
        /// </summary>
        [DataMember, JsonProperty(PropertyName = "current_user_retweet", NullValueHandling = NullValueHandling.Ignore)]
        public CurrentUserRetweet CurrentUserRetweet { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the authenticated user has favorited this status.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is favorited; otherwise, <c>false</c>.
        /// </value>
        [DataMember, JsonProperty(PropertyName = "favorited", NullValueHandling=NullValueHandling.Ignore)]
        public bool IsFavorited { get; set; }

        /// <summary>
        /// Gets or sets the screenName the status is in reply to.
        /// </summary>
        /// <value>The screenName.</value>
        [DataMember, JsonProperty(PropertyName = "in_reply_to_screen_name", NullValueHandling = NullValueHandling.Ignore)]
        public string InReplyToScreenName { get; set; }

        /// <summary>
        /// Gets or sets the status id the status is in reply to.
        /// </summary>
        /// <value>The status id.</value>
        [DataMember, JsonProperty(PropertyName = "in_reply_to_status_id", NullValueHandling=NullValueHandling.Ignore)]
        public decimal InReplyToStatusId { get; set; }

        /// <summary>
        /// Gets or sets the status id as a string for the status is in reply to.
        /// </summary>
        /// <value>The string id.</value>
        [DataMember, JsonProperty(PropertyName = "in_reply_to_status_id_str", NullValueHandling = NullValueHandling.Ignore)]
        public string InReplyToStatusStringId { get; set; }

        /// <summary>
        /// Gets or sets the user id the status is in reply to.
        /// </summary>
        /// <value>The user id.</value>
        [DataMember, JsonProperty(PropertyName = "in_reply_to_user_id", NullValueHandling=NullValueHandling.Ignore)]
        public long InReplyToUserId { get; set; }

        /// <summary>
        /// Gets or sets the place.
        /// </summary>
        /// <value>The place.</value>
        [DataMember, JsonProperty(PropertyName = "place", NullValueHandling = NullValueHandling.Ignore)]
        public Place Place { get; set; }

        /// <summary>
        /// This field only surfaces when a tweet contains a link. The meaning of the field doesn't pertain to the tweet content itself, but instead it is an indicator that the URL contained in the tweet may contain content or media identified as sensitive content.
        /// </summary>
        /// <value>true/false</value>
        [DataMember, JsonProperty(PropertyName = "possibly_sensitive", NullValueHandling=NullValueHandling.Ignore)]
        public bool PossiblySensitive { get; set; }

        /// <summary>
        /// Gets or sets the retweet count string.
        /// </summary>
        /// <value>The retweet count.</value>
        [DataMember, JsonProperty(PropertyName = "retweet_count", NullValueHandling=NullValueHandling.Ignore)]
        public int RetweetCount { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="Status"/> is retweeted.
        /// </summary>
        /// <value><c>true</c> if retweeted; otherwise, <c>false</c>.</value>
        [DataMember, JsonProperty(PropertyName = "retweeted", NullValueHandling=NullValueHandling.Ignore)]
        public bool Retweeted { get; set; }

        /// <summary>
        /// Gets or sets the retweeted status.
        /// </summary>
        /// <value>The retweeted status.</value>
        [DataMember, JsonProperty(PropertyName = "retweeted_status", NullValueHandling = NullValueHandling.Ignore)]
        public Status RetweetedStatus { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this status message is truncated.
        /// </summary>
        /// <value>
        /// <c>true</c> if this status message is truncated; otherwise, <c>false</c>.
        /// </value>
        [DataMember, JsonProperty(PropertyName = "truncated", NullValueHandling = NullValueHandling.Ignore)]
        public bool IsTruncated { get; set; }

        /// <summary>
        /// Gets or sets the user.
        /// </summary>
        /// <value>The user that posted this status.</value>
        [DataMember, JsonProperty(PropertyName = "user")]
        public User User { get; set; }

        /// <summary>
        /// When present, indicates a textual representation of the two-letter country codes this content is withheld from.
        /// </summary>
        /// <value>The countries.</value>
        [DataMember, JsonProperty(PropertyName = "withheld_in_countries", NullValueHandling = NullValueHandling.Ignore)]
        public string WithheldInCountries { get; set; }

        /// <summary>
        /// When present, indicates whether the content being withheld is the "status" or a "user."
        /// </summary>
        /// <value>Status/User.</value>
        [DataMember, JsonProperty(PropertyName = "withheld_scope", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(StringEnumConverter))]
        public WithheldScope WithheldScope { get; set; }
        
    }
}
