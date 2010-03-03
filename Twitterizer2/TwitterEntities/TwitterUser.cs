﻿//-----------------------------------------------------------------------
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
    using System.Globalization;
    using System.Runtime.Serialization;
    
    /// <summary>
    /// The class that represents a twitter user account
    /// </summary>
    [DataContract]
    public class TwitterUser : Core.BaseObject
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="TwitterUser"/> class.
        /// </summary>
        public TwitterUser() : base() 
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TwitterUser"/> class.
        /// </summary>
        /// <param name="tokens">OAuth access tokens.</param>
        public TwitterUser(OAuthTokens tokens) 
            : base()
        {
            this.Tokens = tokens;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the User ID.
        /// </summary>
        /// <value>The User ID.</value>
        [DataMember(Name = "id")]
        public long Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the user.
        /// </summary>
        /// <value>The name of the user.</value>
        [DataMember(Name = "name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the location.
        /// </summary>
        /// <value>The location.</value>
        [DataMember(Name = "location")]
        public string Location { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>The description.</value>
        [DataMember(Name = "description")]
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        /// <value>The status.</value>
        [DataMember(Name = "status")]
        public TwitterStatus Status { get; set; }

        /// <summary>
        /// Gets or sets the created date.
        /// </summary>
        /// <value>The created date.</value>
        [DataMember(Name = "created_at")]
        public string CreatedDateString { get; set; }

        /// <summary>
        /// Gets the created date.
        /// </summary>
        /// <value>The created date.</value>
        [IgnoreDataMember]
        public DateTime CreatedDate
        {
            get
            {
                DateTime parsedDate;

                if (DateTime.TryParseExact(
                       this.CreatedDateString,
                       DateFormat,
                       CultureInfo.InvariantCulture,
                       DateTimeStyles.None,
                       out parsedDate))
                {
                    return parsedDate;
                }
                else
                {
                    return new DateTime();
                }
            }
        }

        /// <summary>
        /// Gets or sets the time zone.
        /// </summary>
        /// <value>The time zone.</value>
        [DataMember(Name = "time_zone")]
        public string TimeZone { get; set; }

        /// <summary>
        /// Gets or sets the color of the profile sidebar border.
        /// </summary>
        /// <value>The color of the profile sidebar border.</value>
        [DataMember(Name = "profile_sidebar_border_color")]
        public string ProfileSidebarBorderColor { get; set; }

        /// <summary>
        /// Gets or sets the number of followers.
        /// </summary>
        /// <value>The number of followers.</value>
        [DataMember(Name = "followers_count")]
        public int NumberOfFollowers { get; set; }

        /// <summary>
        /// Gets or sets the number of statuses.
        /// </summary>
        /// <value>The number of statuses.</value>
        [DataMember(Name = "statuses_count")]
        public int NumberOfStatuses { get; set; }

        /// <summary>
        /// Gets or sets the profile image location.
        /// </summary>
        /// <value>The profile image location.</value>
        [DataMember(Name = "profile_image_url")]
        public string ProfileImageLocation { get; set; }

        /// <summary>
        /// Gets or sets the number of friends.
        /// </summary>
        /// <value>The number of friends.</value>
        [DataMember(Name = "friends_count")]
        public int NumberOfFriends { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the user has enabled contributors access to his or her account.
        /// </summary>
        /// <value>The is contributors enabled value.</value>
        [DataMember(Name = "contributors_enabled")]
        public bool IsContributorsEnabled { get; set; }

        /// <summary>
        /// Gets or sets the language.
        /// </summary>
        /// <value>The language.</value>
        [DataMember(Name = "lang")]
        public string Language { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the user receives notifications.
        /// </summary>
        /// <value>
        /// <c>true</c> if the user receives notifications; otherwise, <c>false</c>.
        /// </value>
        [DataMember(Name = "notifications", IsRequired = false)]
        public bool? DoesReceiveNotifications { get; set; }

        /// <summary>
        /// Gets or sets the color of the profile text.
        /// </summary>
        /// <value>The color of the profile text.</value>
        [DataMember(Name = "profile_text_color")]
        public string ProfileTextColor { get; set; }

        /// <summary>
        /// Gets or sets the screenname.
        /// </summary>
        /// <value>The screenname.</value>
        [DataMember(Name = "screen_name")]
        public string ScreenName { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the authenticated user is following this user.
        /// </summary>
        /// <value>
        /// <c>true</c> if the authenticated user is following this user; otherwise, <c>false</c>.
        /// </value>
        [DataMember(Name = "following", IsRequired = false)]
        public bool? IsFollowing { get; set; }

        /// <summary>
        /// Gets or sets the profile background image location.
        /// </summary>
        /// <value>The profile background image location.</value>
        [DataMember(Name = "profile_background_image_url")]
        public string ProfileBackgroundImageLocation { get; set; }

        /// <summary>
        /// Gets or sets the number of favorites.
        /// </summary>
        /// <value>The number of favorites.</value>
        [DataMember(Name = "favourites_count")]
        public int NumberOfFavorites { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this user is protected.
        /// </summary>
        /// <value>
        /// <c>true</c> if this user is protected; otherwise, <c>false</c>.
        /// </value>
        [DataMember(Name = "protected")]
        public bool IsProtected { get; set; }

        /// <summary>
        /// Gets or sets the color of the profile link.
        /// </summary>
        /// <value>The color of the profile link.</value>
        [DataMember(Name = "profile_link_color")]
        public string ProfileLinkColor { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this user is geo enabled.
        /// </summary>
        /// <value>
        /// <c>true</c> if this user is geo enabled; otherwise, <c>false</c>.
        /// </value>
        [DataMember(Name = "geo_enabled", IsRequired = false)]
        public bool? IsGeoEnabled { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this user's profile background image is tiled.
        /// </summary>
        /// <value>
        /// <c>true</c> if this user's profile background image is tiled; otherwise, <c>false</c>.
        /// </value>
        [DataMember(Name = "profile_background_tile", IsRequired = false)]
        public bool? IsProfileBackgroundTiled { get; set; }

        /// <summary>
        /// Gets or sets the time zone offset.
        /// </summary>
        /// <value>The time zone offset.</value>
        /// <remarks>Also called the Coordinated Universal Time (UTC) offset.</remarks>
        [DataMember(Name = "utc_offset")]
        public double? TimeZoneOffset { get; set; }

        /// <summary>
        /// Gets or sets the color of the profile background.
        /// </summary>
        /// <value>The color of the profile background.</value>
        [DataMember(Name = "profile_background_color")]
        public string ProfileBackgroundColor { get; set; }

        /// <summary>
        /// Gets or sets the user's website.
        /// </summary>
        /// <value>The website address.</value>
        [DataMember(Name = "url")]
        public string Website { get; set; }
        #endregion

        #region Static Methods
        #region GetUser
        /// <summary>
        /// Gets the user.
        /// </summary>
        /// <param name="id">The user id.</param>
        /// <returns>A new instance of the <see cref="Twitterizer.TwitterUser"/> class.</returns>
        public static TwitterUser GetUser(long id)
        {
            Commands.UserShowCommand command = new Commands.UserShowCommand(null);
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
        public static TwitterUser GetUser(string username)
        {
            Commands.UserShowCommand command = new Commands.UserShowCommand(null);
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
        public static TwitterUser GetUser(OAuthTokens requestTokens, long id)
        {
            Commands.UserShowCommand command = new Commands.UserShowCommand(requestTokens);
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
        /// <param name="tokens">The tokens.</param>
        /// <param name="username">The username.</param>
        /// <returns>A new instance of the <see cref="Twitterizer.TwitterUser"/> class.</returns>
        public static TwitterUser GetUser(OAuthTokens tokens, string username)
        {
            Commands.UserShowCommand command = new Twitterizer.Commands.UserShowCommand(tokens);
            command.Username = username;

            return Core.CommandPerformer<TwitterUser>.PerformAction(command);
        }
        #endregion

        #region GetTimeLine
        /// <summary>
        /// Gets the user time line.
        /// </summary>
        /// <param name="tokens">The tokens.</param>
        /// <param name="userIdOrScreenName">Name of the user id or screen.</param>
        /// <returns>
        /// A <see cref="TwitterStatusCollection"/> instance.
        /// </returns>
        public static TwitterStatusCollection GetTimeline(OAuthTokens tokens, string userIdOrScreenName)
        {
            Commands.UserTimelineCommand command = new Commands.UserTimelineCommand(tokens);
            command.IdOrScreenName = userIdOrScreenName;

            TwitterStatusCollection result = Core.CommandPerformer<TwitterStatusCollection>.PerformAction(command);
            result.Command = command;

            return result;
        }

        /// <summary>
        /// Gets the user time line.
        /// </summary>
        /// <param name="tokens">The tokens.</param>
        /// <returns>
        /// A <see cref="TwitterStatusCollection"/> instance.
        /// </returns>
        public static TwitterStatusCollection GetTimeline(OAuthTokens tokens)
        {
            return GetTimeline(tokens, string.Empty, -1, -1, -1, -1);
        }

        /// <summary>
        /// Gets the user time line.
        /// </summary>
        /// <param name="tokens">The oauth tokens.</param>
        /// <param name="userIdOrScreenName">Name of the user id or screen.</param>
        /// <param name="sinceId">The min status id.</param>
        /// <param name="maxId">The max status id.</param>
        /// <param name="count">The number of statuses to return.</param>
        /// <param name="page">The page number.</param>
        /// <returns>
        /// A <see cref="TwitterStatusCollection"/> instance.
        /// </returns>
        public static TwitterStatusCollection GetTimeline(OAuthTokens tokens, string userIdOrScreenName, long sinceId, long maxId, int count, int page)
        {
            Commands.UserTimelineCommand command = new Commands.UserTimelineCommand(tokens);
            command.IdOrScreenName = userIdOrScreenName;
            command.Count = count;
            command.MaxId = maxId;
            command.Page = page;
            command.SinceId = sinceId;

            TwitterStatusCollection result = Core.CommandPerformer<TwitterStatusCollection>.PerformAction(command);
            result.Command = command;

            return result;
        }
        #endregion

        /// <summary>
        /// Gets the followers.
        /// </summary>
        /// <param name="tokens">The tokens.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>
        /// A <see cref="TwitterStatusCollection"/> instance.
        /// </returns>
        public static TwitterUserCollection GetFollowers(OAuthTokens tokens, long userId)
        {
            TwitterUser user = new TwitterUser()
            {
                Tokens = tokens,
                Id = userId
            };

            return user.GetFollowers();
        }
        #endregion

        #region Non-Static Members
        #region GetFollowers
        /// <summary>
        /// Gets the user's followers.
        /// </summary>
        /// <returns>A new instance of the <see cref="Twitterizer.TwitterUserCollection"/> class.</returns>
        public TwitterUserCollection GetFollowers()
        {
            Commands.UserFollowersCommand command = new Commands.UserFollowersCommand(this.Tokens);
            command.UserId = this.Id;

            TwitterUserCollection result = Core.CommandPerformer<TwitterUserCollection>.PerformAction(command);
            result.Command = command;

            return result;
        }
        #endregion

        #region GetTimeLine
        /// <summary>
        /// Gets the user time line.
        /// </summary>
        /// <returns>A <see cref="TwitterStatusCollection"/> instance.</returns>
        public TwitterStatusCollection GetTimeline()
        {
            return GetTimeline(this.Tokens);
        }
        #endregion
        #endregion
    }
}
