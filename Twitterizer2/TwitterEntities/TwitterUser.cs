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
    using System.Runtime.Serialization;
    
    /// <summary>
    /// The class that represents a twitter user account
    /// </summary>
    [DataContract]
    [DebuggerDisplay("TwitterUser = {ScreenName}")]
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
        [DataMember(Name = "id")]
        [CLSCompliant(false)]
        public ulong Id { get; set; }

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
        /// Gets or sets the number of followers.
        /// </summary>
        /// <value>The number of followers.</value>
        [DataMember(Name = "followers_count")]
        [CLSCompliant(false)]
        public uint NumberOfFollowers { get; set; }

        /// <summary>
        /// Gets or sets the number of statuses.
        /// </summary>
        /// <value>The number of statuses.</value>
        [DataMember(Name = "statuses_count")]
        [CLSCompliant(false)]
        public uint NumberOfStatuses { get; set; }

        /// <summary>
        /// Gets or sets the number of friends.
        /// </summary>
        /// <value>The number of friends.</value>
        [DataMember(Name = "friends_count")]
        [CLSCompliant(false)]
        public uint NumberOfFriends { get; set; }

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
        /// Gets or sets the number of favorites.
        /// </summary>
        /// <value>The number of favorites.</value>
        [DataMember(Name = "favourites_count")]
        [CLSCompliant(false)]
        public uint NumberOfFavorites { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this user is protected.
        /// </summary>
        /// <value>
        /// <c>true</c> if this user is protected; otherwise, <c>false</c>.
        /// </value>
        [DataMember(Name = "protected")]
        public bool IsProtected { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this user is geo enabled.
        /// </summary>
        /// <value>
        /// <c>true</c> if this user is geo enabled; otherwise, <c>false</c>.
        /// </value>
        [DataMember(Name = "geo_enabled", IsRequired = false)]
        public bool? IsGeoEnabled { get; set; }

        /// <summary>
        /// Gets or sets the time zone offset.
        /// </summary>
        /// <value>The time zone offset.</value>
        /// <remarks>Also called the Coordinated Universal Time (UTC) offset.</remarks>
        [DataMember(Name = "utc_offset")]
        public double? TimeZoneOffset { get; set; }

        /// <summary>
        /// Gets or sets the user's website.
        /// </summary>
        /// <value>The website address.</value>
        [DataMember(Name = "url")]
        public string Website { get; set; }

        #region Profile Layout Properties
        /// <summary>
        /// Gets or sets the color of the profile background.
        /// </summary>
        /// <value>The color of the profile background.</value>
        [DataMember(Name = "profile_background_color")]
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
        [DataMember(Name = "profile_background_tile", IsRequired = false)]
        public bool? IsProfileBackgroundTiled { get; set; }

        /// <summary>
        /// Gets or sets the color of the profile link.
        /// </summary>
        /// <value>The color of the profile link.</value>
        [DataMember(Name = "profile_link_color")]
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
        [DataMember(Name = "profile_background_image_url")]
        public string ProfileBackgroundImageLocation { get; set; }

        /// <summary>
        /// Gets or sets the color of the profile text.
        /// </summary>
        /// <value>The color of the profile text.</value>
        [DataMember(Name = "profile_text_color")]
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
        [DataMember(Name = "profile_image_url")]
        public string ProfileImageLocation { get; set; }

        /// <summary>
        /// Gets or sets the color of the profile sidebar border.
        /// </summary>
        /// <value>The color of the profile sidebar border.</value>
        [DataMember(Name = "profile_sidebar_border_color")]
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

        #region Lookup readonly properties
        /// <summary>
        /// Gets the followers.
        /// </summary>
        /// <value>The followers.</value>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public TwitterUserCollection Followers
        {
            get
            {
                return GetFollowers(this.Tokens, this.Id);
            }
        }

        /// <summary>
        /// Gets the user's friends, each with current status inline. They are ordered by the order in which the user followed them, most recently followed first, 100 at a time.
        /// </summary>
        /// <returns>
        /// A <see cref="TwitterUserCollection"/> instance.
        /// </returns>
        /// <remarks>Please note that the result set isn't guaranteed to be 100 every time as suspended users will be filtered out.</remarks>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public TwitterUserCollection Friends
        {
            get
            {
                return GetFriends(this.Tokens, this.Id);
            }
        }

        /// <summary>
        /// Gets the user's timeline.
        /// </summary>
        /// <value>The timeline.</value>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public TwitterStatusCollection Timeline
        {
            get
            {
                return GetTimeline(
                    this.Tokens,
                    this.Id,
                    string.Empty,
                    0,
                    0,
                    -1,
                    -1);
            }
        }

        /// <summary>
        /// Gets the user's friends timeline.
        /// </summary>
        /// <value>The friends timeline.</value>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public TwitterStatusCollection FriendsTimeline
        {
            get
            {
                return GetFriendsTimeline(this.Tokens, 0, 0);
            }
        }

        /// <summary>
        /// Gets statuses that mention the user.
        /// </summary>
        /// <value>The mentions.</value>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public TwitterStatusCollection Mentions
        {
            get
            {
                return GetMentions(this.Tokens, 0, 0);
            }
        }

        /// <summary>
        /// Gets the 20 most recent retweets posted by the authenticating user.
        /// </summary>
        /// <value>The retweets.</value>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public TwitterStatusCollection Retweets
        {
            get
            {
                return this.RetweetedByUser(0, 0);
            }
        }

        /// <summary>
        /// Gets the 20 most recent retweets posted by the authenticating user's friends.
        /// </summary>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public TwitterStatusCollection Retweeted
        {
            get
            {
                return this.RetweetedToUser(0, 0);
            }
        }

        /// <summary>
        /// Gets the direct messages received.
        /// </summary>
        /// <value>The direct messages received.</value>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public TwitterDirectMessageCollection DirectMessagesReceived
        {
            get
            {
                return TwitterDirectMessage.GetDirectMessages(this.Tokens);
            }
        }

        /// <summary>
        /// Gets the direct messages sent.
        /// </summary>
        /// <value>The direct messages sent.</value>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public TwitterDirectMessageCollection DirectMessagesSent
        {
            get
            {
                return TwitterDirectMessage.GetDirectMessagesSent(this.Tokens);
            }
        }
        #endregion

        #region Static Methods
        #region GetUser
        /// <summary>
        /// Gets the user.
        /// </summary>
        /// <param name="id">The user id.</param>
        /// <returns>A new instance of the <see cref="Twitterizer.TwitterUser"/> class.</returns>
        [CLSCompliant(false)]
        public static TwitterUser GetUser(ulong id)
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
        public static TwitterUser GetUser(string username)
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
        [CLSCompliant(false)]
        public static TwitterUser GetUser(OAuthTokens requestTokens, ulong id)
        {
            Commands.ShowUserCommand command = new Commands.ShowUserCommand(requestTokens);
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
            Commands.ShowUserCommand command = new Twitterizer.Commands.ShowUserCommand(tokens);
            command.Username = username;

            return Core.CommandPerformer<TwitterUser>.PerformAction(command);
        }
        #endregion

        #region GetTimeLine
        /// <summary>
        /// Gets the user time line.
        /// </summary>
        /// <param name="tokens">The tokens.</param>
        /// <returns>
        /// A <see cref="TwitterStatusCollection"/> instance.
        /// </returns>
        public static TwitterStatusCollection GetTimeline(OAuthTokens tokens)
        {
            return GetTimeline(
                tokens,
                0,
                string.Empty,
                0,
                0,
                -1,
                -1);
        }

        /// <summary>
        /// Gets the user time line.
        /// </summary>
        /// <param name="tokens">The oauth tokens.</param>
        /// <param name="userId">The user id.</param>
        /// <param name="screenName">Name of the screen.</param>
        /// <param name="sinceId">The min status id.</param>
        /// <param name="maxId">The max status id.</param>
        /// <param name="count">The number of statuses to return.</param>
        /// <param name="page">The page number.</param>
        /// <returns>
        /// A <see cref="TwitterStatusCollection"/> instance.
        /// </returns>
        [CLSCompliant(false)]
        public static TwitterStatusCollection GetTimeline(OAuthTokens tokens, ulong userId, string screenName, ulong sinceId, ulong maxId, int count, int page)
        {
            Commands.UserTimelineCommand command = new Commands.UserTimelineCommand(tokens)
            {
                UserId = userId,
                ScreenName = screenName,
                Count = count,
                MaxId = maxId,
                Page = page,
                SinceId = sinceId
            };

            TwitterStatusCollection result = Core.CommandPerformer<TwitterStatusCollection>.PerformAction(command);
            result.Command = command;

            return result;
        }
        #endregion

        /// <summary>
        /// Gets the followers.
        /// </summary>
        /// <param name="tokens">The tokens.</param>
        /// <returns>
        /// A <see cref="TwitterStatusCollection"/> instance.
        /// </returns>
        public static TwitterUserCollection GetFollowers(OAuthTokens tokens)
        {
            return GetFollowers(tokens, 0);
        }

        /// <summary>
        /// Gets the followers.
        /// </summary>
        /// <param name="tokens">The tokens.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>
        /// A <see cref="TwitterStatusCollection"/> instance.
        /// </returns>
        [CLSCompliant(false)]
        public static TwitterUserCollection GetFollowers(OAuthTokens tokens, ulong userId)
        {
            Commands.FollowersCommand command = new Commands.FollowersCommand(tokens)
            {
                UserId = userId
            };

            TwitterUserWrapper resultWrapper = Core.CommandPerformer<TwitterUserWrapper>.PerformAction(command);
            TwitterUserCollection result = resultWrapper.Users;

            result.CursorPagedCommand = command;

            return result;
        }

        #region Get Home Timeline
        /// <summary>
        /// Gets the home timeline.
        /// </summary>
        /// <param name="tokens">The tokens.</param>
        /// <returns>A <see cref="TwitterStatusCollection"/> instance.</returns>
        public static TwitterStatusCollection GetHomeTimeline(OAuthTokens tokens)
        {
            return GetHomeTimeline(tokens, 0, 0, -1, -1);
        }

        /// <summary>
        /// Gets the home timeline.
        /// </summary>
        /// <param name="tokens">The tokens.</param>
        /// <param name="sinceId">The since id.</param>
        /// <param name="maxId">The max id.</param>
        /// <param name="count">The number of items per page.</param>
        /// <param name="page">The page number.</param>
        /// <returns>A <see cref="TwitterStatusCollection"/> object.</returns>
        [CLSCompliant(false)]
        public static TwitterStatusCollection GetHomeTimeline(OAuthTokens tokens, ulong sinceId, ulong maxId, int count, int page)
        {
            Commands.HomeTimelineCommand command = new Commands.HomeTimelineCommand(tokens)
            {
                Count = count,
                SinceId = sinceId,
                MaxId = maxId,
                Page = page
            };

            TwitterStatusCollection result = Core.CommandPerformer<TwitterStatusCollection>.PerformAction(command);

            if (result != null)
            {
                result.Command = command;
            }

            return result;
        }
        #endregion

        #region Search
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
        #endregion

        #region GetFriends
        /// <summary>
        /// Returns a user's friends, each with current status inline. They are ordered by the order in which the user followed them, most recently followed first, 100 at a time.
        /// </summary>
        /// <param name="tokens">The tokens.</param>
        /// <param name="userId">The user id.</param>
        /// <param name="screenName">Name of the screen.</param>
        /// <returns>
        /// A <see cref="TwitterUserCollection"/> instance.
        /// </returns>
        /// <remarks>Please note that the result set isn't guaranteed to be 100 every time as suspended users will be filtered out.</remarks>
        [CLSCompliant(false)]
        public static TwitterUserCollection GetFriends(OAuthTokens tokens, ulong userId, string screenName)
        {
            Commands.FriendsCommand command = new Commands.FriendsCommand(tokens)
            {
                UserId = userId,
                ScreenName = screenName
            };

            command.Validate();
            if (!command.IsValid)
            {
                throw new CommandValidationException<TwitterUserWrapper>()
                {
                    Command = command,
                    MethodName = "GetFriends"
                };
            }

            TwitterUserCollection result = Core.CommandPerformer<TwitterUserWrapper>.PerformAction(command).Users;
            result.CursorPagedCommand = command;

            return result;
        }

        /// <summary>
        /// Returns a user's friends, each with current status inline. They are ordered by the order in which the user followed them, most recently followed first, 100 at a time.
        /// </summary>
        /// <param name="tokens">The tokens.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>
        /// A <see cref="TwitterUserCollection"/> instance.
        /// </returns>
        /// <remarks>Please note that the result set isn't guaranteed to be 100 every time as suspended users will be filtered out.</remarks>
        [CLSCompliant(false)]
        public static TwitterUserCollection GetFriends(OAuthTokens tokens, ulong userId)
        {
            return GetFriends(tokens, userId, string.Empty);
        }

        /// <summary>
        /// Returns a user's friends, each with current status inline. They are ordered by the order in which the user followed them, most recently followed first, 100 at a time.
        /// </summary>
        /// <param name="tokens">The tokens.</param>
        /// <param name="screenName">Name of the screen.</param>
        /// <returns>
        /// A <see cref="TwitterUserCollection"/> instance.
        /// </returns>
        /// <remarks>Please note that the result set isn't guaranteed to be 100 every time as suspended users will be filtered out.</remarks>
        public static TwitterUserCollection GetFriends(OAuthTokens tokens, string screenName)
        {
            return GetFriends(tokens, 0, screenName);
        }

        /// <summary>
        /// Returns a user's friends, each with current status inline. They are ordered by the order in which the user followed them, most recently followed first, 100 at a time.
        /// </summary>
        /// <param name="tokens">The tokens.</param>
        /// <returns>
        /// A <see cref="TwitterUserCollection"/> instance.
        /// </returns>
        /// <remarks>Please note that the result set isn't guaranteed to be 100 every time as suspended users will be filtered out.</remarks>
        public static TwitterUserCollection GetFriends(OAuthTokens tokens)
        {
            return GetFriends(tokens, 0, string.Empty);
        }
        #endregion

        #region DeleteFriendship
        /// <summary>
        /// Allows the authenticating users to unfollow the user specified in the ID parameter.
        /// </summary>
        /// <param name="tokens">The tokens.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>
        /// Returns the unfollowed user in the requested format when successful.
        /// </returns>
        [CLSCompliant(false)]
        public static TwitterUser DeleteFriendship(OAuthTokens tokens, ulong userId)
        {
            Commands.DeleteFriendshipCommand command = new Commands.DeleteFriendshipCommand(tokens)
            {
                UserId = userId
            };

            TwitterUser result = Core.CommandPerformer<TwitterUser>.PerformAction(command);

            return result;
        }

        /// <summary>
        /// Allows the authenticating users to unfollow the user specified in the ID parameter.
        /// </summary>
        /// <param name="tokens">The tokens.</param>
        /// <param name="username">The username.</param>
        /// <returns>
        /// Returns the unfollowed user in the requested format when successful.
        /// </returns>
        public static TwitterUser DeleteFriendship(OAuthTokens tokens, string username)
        {
            Commands.DeleteFriendshipCommand command = new Commands.DeleteFriendshipCommand(tokens)
            {
                Username = username
            };

            TwitterUser result = Core.CommandPerformer<TwitterUser>.PerformAction(command);

            return result;
        } 
        #endregion

        #region GetFriendship
        /// <summary>
        /// Gets the friendship.
        /// </summary>
        /// <param name="tokens">The tokens.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>A <see cref="TwitterRelationship"/> instance.</returns>
        [CLSCompliant(false)]
        public static TwitterRelationship GetFriendship(OAuthTokens tokens, ulong userId)
        {
            Commands.ShowFriendshipCommand command = new Twitterizer.Commands.ShowFriendshipCommand(tokens)
            {
                TargetId = userId
            };

            return Core.CommandPerformer<TwitterRelationship>.PerformAction(command);
        }

        /// <summary>
        /// Gets the friendship between two users.
        /// </summary>
        /// <param name="userId1">The first user id.</param>
        /// <param name="userId2">The second user id.</param>
        /// <returns>
        /// A <see cref="TwitterRelationship"/> instance.
        /// </returns>
        [CLSCompliant(false)]
        public static TwitterRelationship GetFriendship(ulong userId1, ulong userId2)
        {
            Commands.ShowFriendshipCommand command = new Twitterizer.Commands.ShowFriendshipCommand(null)
            {
                SourceId = userId1,
                TargetId = userId2
            };

            return Core.CommandPerformer<TwitterRelationship>.PerformAction(command);
        }

        /// <summary>
        /// Gets the friendship between two users.
        /// </summary>
        /// <param name="username1">The first username.</param>
        /// <param name="username2">The second username.</param>
        /// <returns>
        /// A <see cref="TwitterRelationship"/> instance.
        /// </returns>
        public static TwitterRelationship GetFriendship(string username1, string username2)
        {
            Commands.ShowFriendshipCommand command = new Twitterizer.Commands.ShowFriendshipCommand(null)
            {
                SourceScreenName = username1,
                TargetScreenName = username2
            };

            return Core.CommandPerformer<TwitterRelationship>.PerformAction(command);
        } 
        #endregion
        #endregion

        #region Non-Static Members
        /// <summary>
        /// Gets the friends timeline.
        /// </summary>
        /// <param name="tokens">The OAuth tokens.</param>
        /// <param name="sinceStatusId">The since status id.</param>
        /// <param name="maxStatusId">The max status id.</param>
        /// <returns>
        /// A <see cref="TwitterStatusCollection"/> object.
        /// </returns>
        [CLSCompliant(false)]
        public static TwitterStatusCollection GetFriendsTimeline(OAuthTokens tokens, ulong sinceStatusId, ulong maxStatusId)
        {
            Commands.FriendsTimelineCommand command = new Commands.FriendsTimelineCommand(tokens)
            {
                SinceStatusId = sinceStatusId,
                MaxStatusId = maxStatusId
            };

            TwitterStatusCollection result = Core.CommandPerformer<TwitterStatusCollection>.PerformAction(command);
            result.Command = command;

            return result;
        }

        /// <summary>
        /// Gets the 20 most recent statuses that mention the authorized user.
        /// </summary>
        /// <param name="tokens">The OAuth tokens.</param>
        /// <param name="sinceStatusId">The since status id.</param>
        /// <param name="maxStatusId">The max status id.</param>
        /// <returns>
        /// A <see cref="TwitterStatusCollection"/> object.
        /// </returns>
        [CLSCompliant(false)]
        public static TwitterStatusCollection GetMentions(OAuthTokens tokens, ulong sinceStatusId, ulong maxStatusId)
        {
            Commands.MentionsCommand command = new Commands.MentionsCommand(tokens)
            {
                SinceStatusId = sinceStatusId,
                MaxStatusId = maxStatusId
            };

            TwitterStatusCollection result = Core.CommandPerformer<TwitterStatusCollection>.PerformAction(command);
            result.Command = command;

            return result;
        }

        /// <summary>
        /// Returns the 20 most recent retweets posted by the authenticating user.
        /// </summary>
        /// <param name="sinceStatusId">The since status id.</param>
        /// <param name="maxStatusId">The max status id.</param>
        /// <returns>A <see cref="TwitterStatusCollection"/> object.</returns>
        [CLSCompliant(false)]
        public TwitterStatusCollection RetweetedByUser(ulong sinceStatusId, ulong maxStatusId)
        {
            Commands.RetweetedByMeCommand command = new Commands.RetweetedByMeCommand(this.Tokens)
            {
                SinceStatusId = sinceStatusId,
                MaxStatusId = maxStatusId
            };

            TwitterStatusCollection result = Core.CommandPerformer<TwitterStatusCollection>.PerformAction(command);
            result.Command = command;

            return result;
        }

        /// <summary>
        /// Returns the 20 most recent retweets posted by the authenticating user's friends.
        /// </summary>
        /// <param name="sinceStatusId">The since status id.</param>
        /// <param name="maxStatusId">The max status id.</param>
        /// <returns>A <see cref="TwitterStatusCollection"/> object.</returns>
        [CLSCompliant(false)]
        public TwitterStatusCollection RetweetedToUser(ulong sinceStatusId, ulong maxStatusId)
        {
            Commands.RetweetedToMeCommand command = new Commands.RetweetedToMeCommand(this.Tokens)
            {
                SinceStatusId = sinceStatusId,
                MaxStatusId = maxStatusId
            };

            TwitterStatusCollection result = Core.CommandPerformer<TwitterStatusCollection>.PerformAction(command);
            result.Command = command;

            return result;
        }

        #region RetweetsOfMe
        /// <summary>
        /// Returns the 20 most recent tweets of the authenticated user that have been retweeted by others.
        /// </summary>
        /// <returns>
        /// A <see cref="TwitterStatusCollection"/> object.
        /// </returns>
        public TwitterStatusCollection RetweetsOfUser()
        {
            return this.RetweetsOfUser(0, 0);
        }

        /// <summary>
        /// Returns the 20 most recent tweets of the authenticated user that have been retweeted by others.
        /// </summary>
        /// <param name="sinceStatusId">The since status id.</param>
        /// <param name="maxStatusId">The max status id.</param>
        /// <returns>A <see cref="TwitterStatusCollection"/> object.</returns>
        [CLSCompliant(false)]
        public TwitterStatusCollection RetweetsOfUser(ulong sinceStatusId, ulong maxStatusId)
        {
            Commands.RetweetsOfMeCommand command = new Commands.RetweetsOfMeCommand(this.Tokens)
            {
                SinceStatusId = sinceStatusId,
                MaxStatusId = maxStatusId
            };

            TwitterStatusCollection result = Core.CommandPerformer<TwitterStatusCollection>.PerformAction(command);
            result.Command = command;

            return result;
        }
        #endregion

        /// <summary>
        /// Creates a friendship between this user and the authenticated user.
        /// </summary>
        /// <param name="follow">if set to <c>true</c> enables delivery of statuses from this user to the authenticated user's device.</param>
        /// <returns>A <see cref="TwitterUser"/> instance.</returns>
        public TwitterUser CreateFriendship(bool follow)
        {
            Commands.CreateFriendshipCommand command = new Commands.CreateFriendshipCommand(this.Tokens)
            {
                UserId = this.Id,
                UserName = this.ScreenName,
                Follow = follow
            };

            TwitterUser result = Core.CommandPerformer<TwitterUser>.PerformAction(command);

            return result;
        }

        /// <summary>
        /// Allows the authenticating users to unfollow the user specified in the ID parameter.
        /// </summary>
        /// <returns>Returns the unfollowed user in the requested format when successful.</returns>
        public TwitterUser DeleteFriendship()
        {
            Commands.DeleteFriendshipCommand command = new Commands.DeleteFriendshipCommand(this.Tokens)
            {
                UserId = this.Id
            };

            TwitterUser result = Core.CommandPerformer<TwitterUser>.PerformAction(command);

            return result;
        }
        #endregion
    }
}
