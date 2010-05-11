//-----------------------------------------------------------------------
// <copyright file="TwitterUser.cs" company="Patrick 'Ricky' Smith">
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
// <summary>The TwitterUser class.</summary>
//-----------------------------------------------------------------------
namespace Twitterizer
{
    using System;
    using System.Diagnostics;
    using System.Drawing;
    using System.Globalization;
    using Newtonsoft.Json;
    
    /// <summary>
    /// The class that represents a twitter user account
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    [DebuggerDisplay("@{ScreenName}")]
    [Serializable]
    public class TwitterUser : Core.TwitterObject
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="TwitterUser"/> class.
        /// </summary>
        /// <param name="tokens">OAuth access tokens.</param>
        public TwitterUser(OAuthTokens tokens) 
            : base()
        {
            this.Tokens = tokens;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TwitterUser"/> class.
        /// </summary>
        internal TwitterUser()
            : base()
        {
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the User ID.
        /// </summary>
        /// <value>The User ID.</value>
        [JsonProperty(PropertyName = "id")]
        public decimal Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the user.
        /// </summary>
        /// <value>The name of the user.</value>
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the location.
        /// </summary>
        /// <value>The location.</value>
        [JsonProperty(PropertyName = "location")]
        public string Location { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>The description.</value>
        [JsonProperty(PropertyName = "description")]
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        /// <value>The status.</value>
        [JsonProperty(PropertyName = "status")]
        public TwitterStatus Status { get; set; }

        /// <summary>
        /// Gets the created date.
        /// </summary>
        /// <value>The created date.</value>
        [JsonProperty(PropertyName = "created_at")]
        [JsonConverter(typeof(TwitterizerDateConverter))]
        public DateTime? CreatedDate { get; set; }

        /// <summary>
        /// Gets or sets the time zone.
        /// </summary>
        /// <value>The time zone.</value>
        [JsonProperty(PropertyName = "time_zone")]
        public string TimeZone { get; set; }

        /// <summary>
        /// Gets or sets the number of followers.
        /// </summary>
        /// <value>The number of followers.</value>
        [JsonProperty(PropertyName = "followers_count")]
        public long? NumberOfFollowers { get; set; }

        /// <summary>
        /// Gets or sets the number of statuses.
        /// </summary>
        /// <value>The number of statuses.</value>
        [JsonProperty(PropertyName = "statuses_count")]
        public long NumberOfStatuses { get; set; }

        /// <summary>
        /// Gets or sets the number of friends.
        /// </summary>
        /// <value>The number of friends.</value>
        [JsonProperty(PropertyName = "friends_count")]
        public long NumberOfFriends { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the user has enabled contributors access to his or her account.
        /// </summary>
        /// <value>The is contributors enabled value.</value>
        [JsonProperty(PropertyName = "contributors_enabled")]
        public bool IsContributorsEnabled { get; set; }

        /// <summary>
        /// Gets or sets the language.
        /// </summary>
        /// <value>The language.</value>
        [JsonProperty(PropertyName = "lang")]
        public string Language { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the user receives notifications.
        /// </summary>
        /// <value>
        /// <c>true</c> if the user receives notifications; otherwise, <c>false</c>.
        /// </value>
        [JsonProperty(PropertyName = "notifications")]
        public bool? DoesReceiveNotifications { get; set; }

        /// <summary>
        /// Gets or sets the screenname.
        /// </summary>
        /// <value>The screenname.</value>
        [JsonProperty(PropertyName = "screen_name")]
        public string ScreenName { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the authenticated user is following this user.
        /// </summary>
        /// <value>
        /// <c>true</c> if the authenticated user is following this user; otherwise, <c>false</c>.
        /// </value>
        [JsonProperty(PropertyName = "following")]
        public bool? IsFollowing { get; set; }

        /// <summary>
        /// Gets or sets the number of favorites.
        /// </summary>
        /// <value>The number of favorites.</value>
        [JsonProperty(PropertyName = "favourites_count")]
        public long NumberOfFavorites { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this user is protected.
        /// </summary>
        /// <value>
        /// <c>true</c> if this user is protected; otherwise, <c>false</c>.
        /// </value>
        [JsonProperty(PropertyName = "protected")]
        public bool IsProtected { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this user is geo enabled.
        /// </summary>
        /// <value>
        /// <c>true</c> if this user is geo enabled; otherwise, <c>false</c>.
        /// </value>
        [JsonProperty(PropertyName = "geo_enabled")]
        public bool? IsGeoEnabled { get; set; }

        /// <summary>
        /// Gets or sets the time zone offset.
        /// </summary>
        /// <value>The time zone offset.</value>
        /// <remarks>Also called the Coordinated Universal Time (UTC) offset.</remarks>
        [JsonProperty(PropertyName = "utc_offset")]
        public double? TimeZoneOffset { get; set; }

        /// <summary>
        /// Gets or sets the user's website.
        /// </summary>
        /// <value>The website address.</value>
        [JsonProperty(PropertyName = "url")]
        public string Website { get; set; }

        #region Profile Layout Properties
        /// <summary>
        /// Gets or sets the color of the profile background.
        /// </summary>
        /// <value>The color of the profile background.</value>
        [JsonProperty(PropertyName = "profile_background_color")]
        public string ProfileBackgroundColorString { get; set; }

        /// <summary>
        /// Gets the color of the profile background.
        /// </summary>
        /// <value>The color of the profile background.</value>
        public Color ProfileBackgroundColor
        {
            get
            {
                return ConversionUtility.FromTwitterString(this.ProfileBackgroundColorString);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this user's profile background image is tiled.
        /// </summary>
        /// <value>
        /// <c>true</c> if this user's profile background image is tiled; otherwise, <c>false</c>.
        /// </value>
        [JsonProperty(PropertyName = "profile_background_tile")]
        public bool? IsProfileBackgroundTiled { get; set; }

        /// <summary>
        /// Gets or sets the color of the profile link.
        /// </summary>
        /// <value>The color of the profile link.</value>
        [JsonProperty(PropertyName = "profile_link_color")]
        public string ProfileLinkColorString { get; set; }

        /// <summary>
        /// Gets the color of the profile link.
        /// </summary>
        /// <value>The color of the profile link.</value>
        public Color ProfileLinkColor
        {
            get
            {
                return ConversionUtility.FromTwitterString(this.ProfileLinkColorString);
            }
        }

        /// <summary>
        /// Gets or sets the profile background image location.
        /// </summary>
        /// <value>The profile background image location.</value>
        [JsonProperty(PropertyName = "profile_background_image_url")]
        public string ProfileBackgroundImageLocation { get; set; }

        /// <summary>
        /// Gets or sets the color of the profile text.
        /// </summary>
        /// <value>The color of the profile text.</value>
        [JsonProperty(PropertyName = "profile_text_color")]
        public string ProfileTextColorString { get; set; }

        /// <summary>
        /// Gets the color of the profile text.
        /// </summary>
        /// <value>The color of the profile text.</value>
        public Color ProfileTextColor
        {
            get
            {
                return ConversionUtility.FromTwitterString(this.ProfileTextColorString);
            }
        }
        
        /// <summary>
        /// Gets or sets the profile image location.
        /// </summary>
        /// <value>The profile image location.</value>
        [JsonProperty(PropertyName = "profile_image_url")]
        public string ProfileImageLocation { get; set; }

        /// <summary>
        /// Gets or sets the color of the profile sidebar border.
        /// </summary>
        /// <value>The color of the profile sidebar border.</value>
        [JsonProperty(PropertyName = "profile_sidebar_border_color")]
        public string ProfileSidebarBorderColorString { get; set; }

        /// <summary>
        /// Gets the color of the profile sidebar border.
        /// </summary>
        /// <value>The color of the profile sidebar border.</value>
        public Color ProfileSidebarBorderColor
        {
            get
            {
                return ConversionUtility.FromTwitterString(this.ProfileSidebarBorderColorString);
            }
        }
        #endregion

        #endregion

        /// <summary>
        /// Gets the user.
        /// </summary>
        /// <param name="id">The user id.</param>
        /// <returns>A new instance of the <see cref="Twitterizer.TwitterUser"/> class.</returns>
        public static TwitterUser Show(decimal id)
        {
            Commands.ShowUserCommand command = new Commands.ShowUserCommand(null);
            command.UserId = id;

            command.Validate();
            if (!command.IsValid)
            {
                throw new CommandValidationException<TwitterUser>()
                {
                    Command = command,
                    MethodName = "GetUser"
                };
            }

            return Core.CommandPerformer<TwitterUser>.PerformAction(command);
        }

        /// <summary>
        /// Gets the user.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <returns>A new instance of the <see cref="Twitterizer.TwitterUser"/> class.</returns>
        public static TwitterUser Show(string username)
        {
            Commands.ShowUserCommand command = new Commands.ShowUserCommand(null);
            command.Username = username;

            command.Validate();
            if (!command.IsValid)
            {
                throw new CommandValidationException<TwitterUser>()
                {
                    Command = command,
                    MethodName = "GetUser"
                };
            }

            return Core.CommandPerformer<TwitterUser>.PerformAction(command);
        }

        /// <summary>
        /// Gets the user.
        /// </summary>
        /// <param name="requestTokens">The request tokens.</param>
        /// <param name="id">The user id.</param>
        /// <returns>A new instance of the <see cref="Twitterizer.TwitterUser"/> class.</returns>
        public static TwitterUser Show(OAuthTokens requestTokens, decimal id)
        {
            try
            {
                Commands.ShowUserCommand command = new Commands.ShowUserCommand(requestTokens);
                command.UserId = id;

                return Core.CommandPerformer<TwitterUser>.PerformAction(command);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Gets the user.
        /// </summary>
        /// <param name="tokens">The tokens.</param>
        /// <returns>
        /// A new instance of the <see cref="Twitterizer.TwitterUser"/> class.
        /// </returns>
        public static TwitterUser Show(OAuthTokens tokens)
        {
            Commands.ShowUserCommand command = new Twitterizer.Commands.ShowUserCommand(tokens);

            return Core.CommandPerformer<TwitterUser>.PerformAction(command);
        }

        /// <summary>
        /// Gets the user.
        /// </summary>
        /// <param name="tokens">The tokens.</param>
        /// <param name="username">The username.</param>
        /// <returns>A new instance of the <see cref="Twitterizer.TwitterUser"/> class.</returns>
        public static TwitterUser Show(OAuthTokens tokens, string username)
        {
            Commands.ShowUserCommand command = new Twitterizer.Commands.ShowUserCommand(tokens);
            command.Username = username;

            return Core.CommandPerformer<TwitterUser>.PerformAction(command);
        }
       
        /// <summary>
        /// Searches the specified tokens.
        /// </summary>
        /// <param name="tokens">The tokens.</param>
        /// <param name="query">The query.</param>
        /// <param name="numberPerPage">The number per page.</param>
        /// <returns>A <see cref="TwitterUserCollection"/> instance.</returns>
        /// <remarks>For more information, see: http://help.twitter.com/forums/31935/entries/60660</remarks>
        public static TwitterUserCollection Search(OAuthTokens tokens, string query, int numberPerPage)
        {
            Commands.UserSearchCommand command = new Commands.UserSearchCommand(tokens, query)
            {
                NumberPerPage = numberPerPage,
                Query = query
            };

            command.Validate();
            if (!command.IsValid)
            {
                throw new CommandValidationException<TwitterUserCollection>()
                {
                    Command = command,
                    MethodName = "Search"
                };
            }

            TwitterUserCollection result = Core.CommandPerformer<TwitterUserCollection>.PerformAction(command);
            result.PagedCommand = command;

            return result;
        }

        /// <summary>
        /// Searches the specified tokens.
        /// </summary>
        /// <param name="tokens">The tokens.</param>
        /// <param name="query">The query.</param>
        /// <returns>
        /// A <see cref="TwitterUserCollection"/> instance.
        /// </returns>
        /// <remarks>For more information, see: http://help.twitter.com/forums/31935/entries/60660</remarks>
        public static TwitterUserCollection Search(OAuthTokens tokens, string query)
        {
            return Search(tokens, query, -1);
        } 
    }
}
