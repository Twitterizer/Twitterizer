//-----------------------------------------------------------------------
// <copyright file="TwitterUser.cs" company="Patrick 'Ricky' Smith">
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
// <summary>The TwitterUser class.</summary>
//-----------------------------------------------------------------------
namespace Twitterizer.Models
{
    using System;
    using System.Diagnostics;
    using Newtonsoft.Json;
    using Core;
    using System.Runtime.Serialization;
    using Windows.UI;
    using Twitterizer.Models;
    using Newtonsoft.Json.Converters;

    /// <include file='TwitterUser.xml' path='TwitterUser/TwitterUser/*'/>
    [JsonObject(MemberSerialization.OptIn)]
    [DebuggerDisplay("@{ScreenName}")]
    [DataContract]
    public class User : TwitterObject
    {
        /// <summary>
        /// Indicates that the user has an account with "contributor mode" enabled, allowing for Tweets issued by the user to be co-authored by another account. Rarely true.
        /// </summary>
        /// <value>True/False.</value>
        [DataMember, JsonProperty(PropertyName = "contributors_enabled", NullValueHandling = NullValueHandling.Ignore)]
        public bool ContributorsEnabled { get; set; }

        /// <summary>
        /// Gets or sets the created date.
        /// </summary>
        /// <value>The created date.</value>
        [JsonProperty(PropertyName = "created_at")]
        [JsonConverter(typeof(TwitterizerDateConverter))]
        [DataMember]
        public DateTime? CreatedDate { get; set; }

        /// <summary>
        /// When true, indicates that the user has not altered the theme or background of their user profile.
        /// </summary>
        /// <value>True/False.</value>
        [DataMember, JsonProperty(PropertyName = "default_profile", NullValueHandling = NullValueHandling.Ignore)]
        public bool DefaultProfile { get; set; }

        /// <summary>
        /// When true, indicates that the user has not uploaded their own avatar and a default egg avatar is used instead.
        /// </summary>
        /// <value>True/False.</value>
        [DataMember, JsonProperty(PropertyName = "default_profile_image", NullValueHandling = NullValueHandling.Ignore)]
        public bool DefaultProfileImage { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>The description.</value>
        [DataMember, JsonProperty(PropertyName = "description")]
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the number of favorites.
        /// </summary>
        /// <value>The number of favorites.</value>
        [DataMember, JsonProperty(PropertyName = "favourites_count", NullValueHandling = NullValueHandling.Ignore)]
        public int NumberOfFavorites { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [follow request sent].
        /// </summary>
        /// <value><c>true</c> if [follow request sent]; otherwise, <c>false</c>.</value>
        [DataMember, JsonProperty(PropertyName = "follow_request_sent", NullValueHandling = NullValueHandling.Ignore)]
        public bool FollowRequestSent { get; set; }

        /// <summary>
        /// Gets or sets the a value indicating whether the authenticated user is followed by this user.
        /// </summary>
        /// <value>The is followed by.</value>
        [DataMember, JsonProperty(PropertyName = "followed_by", NullValueHandling = NullValueHandling.Ignore)]
        public bool IsFollowedBy { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the authenticated user is following this user.
        /// </summary>
        /// <value>
        /// <c>true</c> if the authenticated user is following this user; otherwise, <c>false</c>.
        /// </value>
        [DataMember, JsonProperty(PropertyName = "following", NullValueHandling = NullValueHandling.Ignore)]
        public bool IsFollowing { get; set; }

        /// <summary>
        /// Gets or sets the number of followers.
        /// </summary>
        /// <value>The number of followers.</value>
        [DataMember, JsonProperty(PropertyName = "followers_count", NullValueHandling = NullValueHandling.Ignore)]
        public int NumberOfFollowers { get; set; }

        /// <summary>
        /// Gets or sets the number of friends.
        /// </summary>
        /// <value>The number of friends.</value>
        [DataMember, JsonProperty(PropertyName = "friends_count", NullValueHandling = NullValueHandling.Ignore)]
        public int NumberOfFriends { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this user is geo enabled.
        /// </summary>
        /// <value>
        /// <c>true</c> if this user is geo enabled; otherwise, <c>false</c>.
        /// </value>
        [DataMember, JsonProperty(PropertyName = "geo_enabled", NullValueHandling = NullValueHandling.Ignore)]
        public bool IsGeoEnabled { get; set; }

        /// <summary>
        /// Gets or sets the User ID.
        /// </summary>
        /// <value>The User ID.</value>
        [DataMember, JsonProperty(PropertyName = "id")]
        public decimal Id { get; set; }

        /// <summary>
        /// Gets or sets the string id.
        /// </summary>
        /// <value>The string id.</value>
        [DataMember, JsonProperty(PropertyName = "str_id")]
        public string StringId { get; set; }

        /// <summary>
        /// When true, indicates that the user is a participant in Twitter's translator community.
        /// </summary>
        /// <value>
        /// <c>true</c> if this user is geo enabled; otherwise, <c>false</c>.
        /// </value>
        [DataMember, JsonProperty(PropertyName = "is_translator", NullValueHandling = NullValueHandling.Ignore)]
        public bool IsTranslator { get; set; }

        /// <summary>
        /// Gets or sets the language.
        /// </summary>
        /// <value>The language.</value>
        [DataMember, JsonProperty(PropertyName = "lang")]
        public string Language { get; set; }

        /// <summary>
        /// Gets or sets the listed count.
        /// </summary>
        /// <value>The listed count.</value>
        [DataMember, JsonProperty(PropertyName = "listed_count", NullValueHandling = NullValueHandling.Ignore)]
        public int ListedCount { get; set; }

        /// <summary>
        /// Gets or sets the location.
        /// </summary>
        /// <value>The location.</value>
        [DataMember, JsonProperty(PropertyName = "location")]
        public string Location { get; set; }

        /// <summary>
        /// Gets or sets the name of the user.
        /// </summary>
        /// <value>The name of the user.</value>
        [DataMember, JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the color of the profile background.
        /// </summary>
        /// <value>The color of the profile background.</value>
        [DataMember, JsonProperty(PropertyName = "profile_background_color")]
        public string ProfileBackgroundColor { get; set; }

        /// <summary>
        /// Gets or sets the profile background image location.
        /// </summary>
        /// <value>The profile background image location.</value>
        [DataMember, JsonProperty(PropertyName = "profile_background_image_url")]
        public string ProfileBackgroundImageLocation { get; set; }

        /// <summary>
        /// Gets or sets the profile background image location.
        /// </summary>
        /// <value>The profile background image location.</value>
        [DataMember, JsonProperty(PropertyName = "profile_background_image_url_https")]
        public string ProfileBackgroundImageSecureLocation { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this user's profile background image is tiled.
        /// </summary>
        /// <value>
        /// <c>true</c> if this user's profile background image is tiled; otherwise, <c>false</c>.
        /// </value>
        [DataMember, JsonProperty(PropertyName = "profile_background_tile", NullValueHandling = NullValueHandling.Ignore)]
        public bool IsProfileBackgroundTiled { get; set; }

        /// <summary>
        /// Gets or sets the profile image location.
        /// </summary>
        /// <value>The profile image location.</value>
        [DataMember, JsonProperty(PropertyName = "profile_image_url")]
        public string ProfileImageLocation { get; set; }

        /// <summary>
        /// Gets or sets the secure profile image location (https).
        /// </summary>
        /// <value>The profile image location.</value>
        [DataMember, JsonProperty(PropertyName = "profile_image_url_https")]
        public string ProfileImageSecureLocation { get; set; }

        /// <summary>
        /// Gets or sets the color of the profile link.
        /// </summary>
        /// <value>The color of the profile link.</value>
        [DataMember, JsonProperty(PropertyName = "profile_link_color")]
        public string ProfileLinkColor { get; set; }

        /// <summary>
        /// Gets or sets the color of the profile sidebar border.
        /// </summary>
        /// <value>The color of the profile sidebar border.</value>
        [DataMember, JsonProperty(PropertyName = "profile_sidebar_border_color")]
        public string ProfileSidebarBorderColor { get; set; }

        /// <summary>
        /// The hexadecimal color the user has chosen to display sidebar backgrounds with in their Twitter UI.
        /// </summary>
        /// <value>The color of the profile sidebar border.</value>
        [DataMember, JsonProperty(PropertyName = "profile_sidebar_fill_color")]
        public string ProfileSidebarFillColor { get; set; }

        /// <summary>
        /// Gets or sets the color of the profile text.
        /// </summary>
        /// <value>The color of the profile text.</value>
        [DataMember, JsonProperty(PropertyName = "profile_text_color")]
        public string ProfileTextColor { get; set; }

        /// <summary>
        /// When true, indicates the user wants their uploaded background image to be used.
        /// </summary>
        /// <value>
        /// <c>true</c>/<c>false</c>.
        /// </value>
        [DataMember, JsonProperty(PropertyName = "profile_use_background_image", NullValueHandling = NullValueHandling.Ignore)]
        public bool ProfileUseBackgroundImage { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this user is protected.
        /// </summary>
        /// <value>
        /// <c>true</c> if this user is protected; otherwise, <c>false</c>.
        /// </value>
        [DataMember, JsonProperty(PropertyName = "protected", NullValueHandling = NullValueHandling.Ignore)]
        public bool IsProtected { get; set; }

        /// <summary>
        /// Gets or sets the screenname.
        /// </summary>
        /// <value>The screenname.</value>
        [DataMember, JsonProperty(PropertyName = "screen_name")]
        public string ScreenName { get; set; }

        /// <summary>
        /// Indicates that the user would like to see media inline. Somewhat disused.
        /// </summary>
        /// <value>
        /// <c>true</c>/<c>false</c>.
        /// </value>
        [DataMember, JsonProperty(PropertyName = "show_all_inline_media", NullValueHandling = NullValueHandling.Ignore)]
        public bool ShowAllInlineMedia { get; set; }

        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        /// <value>The status.</value>
        [DataMember, JsonProperty(PropertyName = "status")]
        public Status Status { get; set; }

        /// <summary>
        /// Gets or sets the number of statuses.
        /// </summary>
        /// <value>The number of statuses.</value>
        [DataMember, JsonProperty(PropertyName = "statuses_count", NullValueHandling = NullValueHandling.Ignore)]
        public int NumberOfStatuses { get; set; }

        /// <summary>
        /// Gets or sets the time zone.
        /// </summary>
        /// <value>The time zone.</value>
        [DataMember, JsonProperty(PropertyName = "time_zone")]
        public string TimeZone { get; set; }

        /// <summary>
        /// Gets or sets the user's website.
        /// </summary>
        /// <value>The website address.</value>
        [DataMember, JsonProperty(PropertyName = "url")]
        public string Website { get; set; }

        /// <summary>
        /// Gets or sets the time zone offset.
        /// </summary>
        /// <value>The time zone offset.</value>
        /// <remarks>Also called the Coordinated Universal Time (UTC) offset.</remarks>
        [DataMember, JsonProperty(PropertyName = "utc_offset")]
        public double? TimeZoneOffset { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the user is verified.
        /// </summary>
        /// <value><c>true</c> if the user is verified; otherwise, <c>false</c>.</value>
        [DataMember, JsonProperty(PropertyName = "verified", NullValueHandling = NullValueHandling.Ignore)]
        public bool Verified { get; set; }

        /// <summary>
        /// When present, indicates a textual representation of the two-letter country codes this content is withheld from.
        /// </summary>
        /// <value>The countries.</value>
        [DataMember, JsonProperty(PropertyName = "withheld_in_countries")]
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
