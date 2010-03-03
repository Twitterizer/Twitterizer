﻿//-----------------------------------------------------------------------
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
        #region Public Static Methods
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
        #endregion
        #endregion

        #region Non-Static Methods
        #region Public Non-Static Methods
        /// <summary>
        /// Updates the authenticated user's status to the supplied text.
        /// </summary>
        /// <param name="text">The status text.</param>
        /// <returns>A <see cref="TwitterStatus"/> object of the newly created status.</returns>
        public TwitterStatus Update(string text)
        {
            Commands.UpdateStatusCommand command = new Commands.UpdateStatusCommand(this.Tokens);
            command.Text = text;

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

        /// <summary>
        /// Deletes this instance.
        /// </summary>
        /// <returns>A <see cref="TwitterStatus"/> object of the deleted status.</returns>
        public TwitterStatus Delete()
        {
            Commands.DeleteStatusCommand command = new Commands.DeleteStatusCommand(this.Tokens);
            command.Id = this.Id;

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
        #endregion
        #endregion
    }
}
