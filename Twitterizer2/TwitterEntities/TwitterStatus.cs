//-----------------------------------------------------------------------
// <copyright file="TwitterStatus.cs" company="Patrick 'Ricky' Smith">
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
// <summary>The TwitterStatus class</summary>
//-----------------------------------------------------------------------
namespace Twitterizer
{
    using System;
    using System.Globalization;
    using System.Runtime.Serialization;
    using Twitterizer.Core;

    /// <summary>
    /// The TwitterStatus class.
    /// </summary>
    [DataContract]
    [Serializable]
    public class TwitterStatus : BaseObject
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="TwitterStatus"/> class.
        /// </summary>
        public TwitterStatus() : base() 
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TwitterStatus"/> class.
        /// </summary>
        /// <param name="tokens">OAuth access tokens.</param>
        public TwitterStatus(OAuthTokens tokens) 
            : base()
        {
            this.Tokens = tokens;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the status id.
        /// </summary>
        /// <value>The status id.</value>
        [DataMember(Name = "id")]
        public long Id { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this status message is truncated.
        /// </summary>
        /// <value>
        /// <c>true</c> if this status message is truncated; otherwise, <c>false</c>.
        /// </value>
        [DataMember(Name = "truncated", IsRequired = false)]
        public bool? IsTruncated { get; set; }

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
        /// Gets or sets the source.
        /// </summary>
        /// <value>The source.</value>
        [DataMember(Name = "source")]
        public string Source { get; set; }

        /// <summary>
        /// Gets or sets the screenName the status is in reply to.
        /// </summary>
        /// <value>The screenName.</value>
        [DataMember(Name = "in_reply_to_screen_name")]
        public string InReplyToScreenName { get; set; }

        /// <summary>
        /// Gets or sets the user id the status is in reply to.
        /// </summary>
        /// <value>The user id.</value>
        [DataMember(Name = "in_reply_to_user_id")]
        public long? InReplyToUserId { get; set; }

        /// <summary>
        /// Gets or sets the status id the status is in reply to.
        /// </summary>
        /// <value>The status id.</value>
        [DataMember(Name = "in_reply_to_status_id")]
        public long? InReplyToStatusId { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the authenticated user has favorited this status.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is favorited; otherwise, <c>false</c>.
        /// </value>
        [DataMember(Name = "favorited", IsRequired = false)]
        public bool? IsFavorited { get; set; }

        /// <summary>
        /// Gets or sets the text of the status.
        /// </summary>
        /// <value>The status text.</value>
        [DataMember(Name = "text")]
        public string Text { get; set; }

        /// <summary>
        /// Gets or sets the user.
        /// </summary>
        /// <value>The user that posted this status.</value>
        [DataMember(Name = "user")]
        public TwitterUser User { get; set; }
        #endregion

        #region Static Methods
        /// <summary>
        /// Updates the authenticated user's status to the supplied text.
        /// </summary>
        /// <param name="tokens">The tokens.</param>
        /// <param name="text">The status text.</param>
        /// <returns>A <see cref="TwitterStatus"/> object of the newly created status.</returns>
        public static TwitterStatus Update(OAuthTokens tokens, string text)
        {
            return new TwitterStatus(tokens).Update(text);
        }

        /// <summary>
        /// Deletes the specified tokens.
        /// </summary>
        /// <param name="tokens">The oauth tokens.</param>
        /// <param name="id">The status id.</param>
        /// <returns>A <see cref="TwitterStatus"/> object of the deleted status.</returns>
        public static TwitterStatus Delete(OAuthTokens tokens, long id)
        {
            TwitterStatus status = new TwitterStatus()
            {
                Id = id,
                Tokens = tokens
            };

            return status.Delete();
        }

        #region GetStatus
        /// <summary>
        /// Returns a single status, with user information, specified by the id parameter.
        /// </summary>
        /// <param name="tokens">The tokens.</param>
        /// <param name="statusId">The status id.</param>
        /// <returns>A <see cref="TwitterStatus"/> instance.</returns>
        public static TwitterStatus GetStatus(OAuthTokens tokens, long statusId)
        {
            Commands.ShowStatusCommand command = new Commands.ShowStatusCommand(tokens, statusId);

            command.Validate();
            if (!command.IsValid)
            {
                throw new CommandValidationException<TwitterStatus>()
                {
                    Command = command,
                    MethodName = "GetStatus"
                };
            }

            return Core.CommandPerformer<TwitterStatus>.PerformAction(command);
        }

        /// <summary>
        /// Returns a single status, with user information, specified by the id parameter.
        /// </summary>
        /// <param name="statusId">The status id.</param>
        /// <returns>A <see cref="TwitterStatus"/> instance.</returns>
        public static TwitterStatus GetStatus(long statusId)
        {
            return GetStatus(null, statusId);
        } 
        #endregion

        #region GetPublicTimeline
        /// <summary>
        /// Gets the public timeline.
        /// </summary>
        /// <returns>A <see cref="TwitterStatusCollection"/>.</returns>
        public static TwitterStatusCollection GetPublicTimeline()
        {
            Commands.PublicTimelineCommand command = new Commands.PublicTimelineCommand(null);

            command.Validate();
            if (!command.IsValid)
            {
                throw new CommandValidationException<TwitterStatusCollection>()
                {
                    Command = command,
                    MethodName = "GetPublicTimeline"
                };
            }

            return Core.CommandPerformer<TwitterStatusCollection>.PerformAction(command);
        }

        /// <summary>
        /// Gets the public timeline.
        /// </summary>
        /// <param name="tokens">The oauth tokens.</param>
        /// <returns>
        /// A <see cref="TwitterStatusCollection"/>.
        /// </returns>
        public static TwitterStatusCollection GetPublicTimeline(OAuthTokens tokens)
        {
            Commands.PublicTimelineCommand command = new Commands.PublicTimelineCommand(tokens);

            command.Validate();
            if (!command.IsValid)
            {
                throw new CommandValidationException<TwitterStatusCollection>()
                {
                    Command = command,
                    MethodName = "GetPublicTimeline"
                };
            }

            return Core.CommandPerformer<TwitterStatusCollection>.PerformAction(command);
        } 
        #endregion

        #region GetHomeTimeline
        /// <summary>
        /// Gets the home timeline.
        /// </summary>
        /// <returns>A <see cref="TwitterStatusCollection"/>.</returns>
        public static TwitterStatusCollection GetHomeTimeline()
        {
            Commands.HomeTimelineCommand command = new Commands.HomeTimelineCommand(null);

            command.Validate();
            if (!command.IsValid)
            {
                throw new CommandValidationException<TwitterStatusCollection>()
                {
                    Command = command,
                    MethodName = "GetHomeTimeline"
                };
            }

            return Core.CommandPerformer<TwitterStatusCollection>.PerformAction(command);
        }

        /// <summary>
        /// Gets the home timeline.
        /// </summary>
        /// <param name="tokens">The oauth tokens.</param>
        /// <returns>
        /// A <see cref="TwitterStatusCollection"/>.
        /// </returns>
        public static TwitterStatusCollection GetHomeTimeline(OAuthTokens tokens)
        {
            Commands.HomeTimelineCommand command = new Commands.HomeTimelineCommand(tokens);

            command.Validate();
            if (!command.IsValid)
            {
                throw new CommandValidationException<TwitterStatusCollection>()
                {
                    Command = command,
                    MethodName = "GetHomeTimeline"
                };
            }

            TwitterStatusCollection result = Core.CommandPerformer<TwitterStatusCollection>.PerformAction(command);
            result.Command = command;

            return result;
        }
        #endregion

        /// <summary>
        /// Retweets a tweet. Requires the id parameter of the tweet you are retweeting. (say that 5 times fast)
        /// </summary>
        /// <param name="tokens">The tokens.</param>
        /// <param name="statusId">The status id.</param>
        /// <returns>A <see cref="TwitterStatus"/> instance.</returns>
        public static TwitterStatus Retweet(OAuthTokens tokens, long statusId)
        {
            Commands.RetweetCommand command = new Commands.RetweetCommand(tokens);
            command.StatusId = statusId;

            command.Validate();
            if (!command.IsValid)
            {
                throw new CommandValidationException<TwitterStatus>()
                {
                    Command = command,
                    MethodName = "Retweet"
                };
            }

            return Core.CommandPerformer<TwitterStatus>.PerformAction(command);
        }

        #region Retweets
        /// <summary>
        /// Returns up to 100 of the first retweets of a given tweet.
        /// </summary>
        /// <param name="tokens">The tokens.</param>
        /// <param name="statusId">The status id.</param>
        /// <param name="count">The count.</param>
        /// <returns>A <see cref="TwitterStatusCollection"/> instance.</returns>
        public static TwitterStatusCollection Retweets(OAuthTokens tokens, long statusId, int count)
        {
            Commands.RetweetsCommand command = new Commands.RetweetsCommand(tokens, statusId);
            command.Count = count;

            command.Validate();
            if (!command.IsValid)
            {
                throw new CommandValidationException<TwitterStatusCollection>()
                {
                    Command = command,
                    MethodName = "Retweet"
                };
            }

            return Core.CommandPerformer<TwitterStatusCollection>.PerformAction(command);
        }

        /// <summary>
        /// Returns up to 100 of the first retweets of a given tweet.
        /// </summary>
        /// <param name="tokens">The tokens.</param>
        /// <param name="statusId">The status id.</param>
        /// <returns>A <see cref="TwitterStatusCollection"/> instance.</returns>
        public static TwitterStatusCollection Retweets(OAuthTokens tokens, long statusId)
        {
            return Retweets(tokens, statusId, -1);
        } 
        #endregion
        #endregion

        #region Non-Static Methods
        #region Update
        /// <summary>
        /// Updates the authenticated user's status to the supplied text.
        /// </summary>
        /// <param name="text">The status text.</param>
        /// <returns>A <see cref="TwitterStatus"/> object of the newly created status.</returns>
        public TwitterStatus Update(string text)
        {
            return this.Update(
                text,
                -1,
                string.Empty,
                string.Empty);
        }

        /// <summary>
        /// Updates the authenticated user's status to the supplied text.
        /// </summary>
        /// <param name="text">The status text.</param>
        /// <param name="replyToStatusId">The reply to status id.</param>
        /// <returns>A <see cref="TwitterStatus"/> object.</returns>
        public TwitterStatus Update(string text, long replyToStatusId)
        {
            return this.Update(
                text,
                replyToStatusId,
                string.Empty,
                string.Empty);
        }

        /// <summary>
        /// Updates the authenticated user's status to the supplied text.
        /// </summary>
        /// <param name="text">The status text.</param>
        /// <param name="replyToStatusId">The reply to status id.</param>
        /// <param name="latitude">The latitude.</param>
        /// <param name="longitude">The longitude.</param>
        /// <returns>A <see cref="TwitterStatus"/> object.</returns>
        public TwitterStatus Update(string text, long replyToStatusId, string latitude, string longitude)
        {
            Commands.UpdateStatusCommand command = new Commands.UpdateStatusCommand(this.Tokens, text)
            {
                InReplyToStatusId = replyToStatusId,
                Latitude = latitude,
                Longitude = longitude,
            };

            command.Validate();
            if (!command.IsValid)
            {
                throw new CommandValidationException<TwitterStatus>()
                {
                    Command = command,
                    MethodName = "Update"
                };
            }

            return Core.CommandPerformer<TwitterStatus>.PerformAction(command);
        } 
        #endregion

        /// <summary>
        /// Deletes this instance.
        /// </summary>
        /// <returns>A <see cref="TwitterStatus"/> object of the deleted status.</returns>
        public TwitterStatus Delete()
        {
            Commands.DeleteStatusCommand command = new Commands.DeleteStatusCommand(this.Tokens, this.Id);

            command.Validate();
            if (!command.IsValid)
            {
                throw new CommandValidationException<TwitterStatus>()
                {
                    Command = command,
                    MethodName = "Delete"
                };
            }

            return Core.CommandPerformer<TwitterStatus>.PerformAction(command);
        }

        /// <summary>
        /// Retweets a tweet. Requires the id parameter of the tweet you are retweeting. (say that 5 times fast)
        /// </summary>
        /// <returns>A <see cref="TwitterStatus"/> instance.</returns>
        public TwitterStatus Retweet()
        {
            Commands.RetweetCommand command = new Commands.RetweetCommand(this.Tokens);
            command.StatusId = this.Id;

            command.Validate();
            if (!command.IsValid)
            {
                throw new CommandValidationException<TwitterStatus>()
                {
                    Command = command,
                    MethodName = "Retweet"
                };
            }

            return Core.CommandPerformer<TwitterStatus>.PerformAction(command);
        }

        #region Retweets
        /// <summary>
        /// Returns up to 100 of the first retweets of a given tweet.
        /// </summary>
        /// <param name="count">The count.</param>
        /// <returns>
        /// A <see cref="TwitterStatusCollection"/> instance.
        /// </returns>
        public TwitterStatusCollection Retweets(int count)
        {
            Commands.RetweetsCommand command = new Commands.RetweetsCommand(this.Tokens, this.Id);
            command.Count = count;

            command.Validate();
            if (!command.IsValid)
            {
                throw new CommandValidationException<TwitterStatusCollection>()
                {
                    Command = command,
                    MethodName = "Retweet"
                };
            }

            return Core.CommandPerformer<TwitterStatusCollection>.PerformAction(command);
        }

        /// <summary>
        /// Returns up to 100 of the first retweets of a given tweet.
        /// </summary>
        /// <returns>
        /// A <see cref="TwitterStatusCollection"/> instance.
        /// </returns>
        public TwitterStatusCollection Retweets()
        {
            return Retweets(this.Tokens, this.Id, -1);
        }
        #endregion
        #endregion
    }
}
