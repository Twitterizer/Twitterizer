//-----------------------------------------------------------------------
// <copyright file="TwitterDirectMessage.cs" company="Patrick 'Ricky' Smith">
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
// <summary>The direct message entity class</summary>
//-----------------------------------------------------------------------

namespace Twitterizer
{
    using System;
    using System.Globalization;
    using System.Runtime.Serialization;
    using Twitterizer.Core;

    /// <summary>
    /// The Direct Message Entity Class
    /// </summary>
    [DataContract]
    public class TwitterDirectMessage : BaseObject
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="TwitterDirectMessage"/> class.
        /// </summary>
        public TwitterDirectMessage() : base() 
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TwitterDirectMessage"/> class.
        /// </summary>
        /// <param name="tokens">OAuth access tokens.</param>
        public TwitterDirectMessage(OAuthTokens tokens) 
            : base()
        {
            this.Tokens = tokens;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the direct message id.
        /// </summary>
        /// <value>The direct message id.</value>
        [DataMember(Name = "id")]
        public long Id { get; set; }

        /// <summary>
        /// Gets or sets the sender id.
        /// </summary>
        /// <value>The sender id.</value>
        [DataMember(Name = "sender_id")]
        public long SenderId { get; set; }

        /// <summary>
        /// Gets or sets the direct message text.
        /// </summary>
        /// <value>The direct message text.</value>
        [DataMember(Name = "text")]
        public string Text { get; set; }

        /// <summary>
        /// Gets or sets the recipient id.
        /// </summary>
        /// <value>The recipient id.</value>
        [DataMember(Name = "recipient_id")]
        public long RecipientId { get; set; }

        /// <summary>
        /// Gets or sets the created date string.
        /// </summary>
        /// <value>The created date string.</value>
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
        /// Gets or sets the name of the sender screen.
        /// </summary>
        /// <value>The name of the sender screen.</value>
        [DataMember(Name = "sender_screen_name")]
        public string SenderScreenName { get; set; }

        /// <summary>
        /// Gets or sets the name of the recipient screen.
        /// </summary>
        /// <value>The name of the recipient screen.</value>
        [DataMember(Name = "recipient_screen_name")]
        public string RecipientScreenName { get; set; }

        /// <summary>
        /// Gets or sets the sender.
        /// </summary>
        /// <value>The sender.</value>
        [DataMember(Name = "sender")]
        public TwitterUser Sender { get; set; }

        /// <summary>
        /// Gets or sets the recipient.
        /// </summary>
        /// <value>The recipient.</value>
        [DataMember(Name = "recipient")]
        public TwitterUser Recipient { get; set; }
        #endregion

        #region Static Methods
        /// <summary>
        /// Returns a list of the 20 most recent direct messages sent to the authenticating user.
        /// </summary>
        /// <param name="tokens">The tokens.</param>
        /// <returns>A <see cref="TwitterDirectMessageCollection"/> instance.</returns>
        public static TwitterDirectMessageCollection GetDirectMessages(OAuthTokens tokens)
        {
            return GetDirectMessages(tokens, -1, -1, -1);
        }

        /// <summary>
        /// Returns a list of the 20 most recent direct messages sent to the authenticating user.
        /// </summary>
        /// <param name="tokens">The tokens.</param>
        /// <param name="sinceStatusId">The since status id.</param>
        /// <param name="maxStatusId">The max status id.</param>
        /// <param name="count">The count.</param>
        /// <returns>A <see cref="TwitterDirectMessageCollection"/> instance.</returns>
        public static TwitterDirectMessageCollection GetDirectMessages(OAuthTokens tokens, long sinceStatusId, long maxStatusId, int count)
        {
            Commands.DirectMessagesCommand command = new Commands.DirectMessagesCommand(tokens)
            {
                SinceStatusId = sinceStatusId,
                MaxStatusId = maxStatusId,
                Count = count
            };

            command.Validate();
            if (!command.IsValid)
            {
                throw new CommandValidationException<TwitterDirectMessageCollection>()
                {
                    Command = command,
                    MethodName = "GetDirectMessages"
                };
            }

            TwitterDirectMessageCollection result = Core.CommandPerformer<TwitterDirectMessageCollection>.PerformAction(command);

            return result;
        }

        /// <summary>
        /// Returns a list of the 20 most recent direct messages sent by the authenticating user.
        /// </summary>
        /// <param name="tokens">The tokens.</param>
        /// <returns>
        /// A <see cref="TwitterDirectMessageCollection"/> instance.
        /// </returns>
        public static TwitterDirectMessageCollection GetDirectMessagesSent(OAuthTokens tokens)
        {
            return GetDirectMessagesSent(tokens, -1, -1, -1, -1);
        }

        /// <summary>
        /// Returns a list of the 20 most recent direct messages sent by the authenticating user.
        /// </summary>
        /// <param name="tokens">The tokens.</param>
        /// <param name="sinceStatusId">The since status id.</param>
        /// <param name="maxStatusId">The max status id.</param>
        /// <param name="count">The count.</param>
        /// <param name="page">The page number.</param>
        /// <returns>
        /// A <see cref="TwitterDirectMessageCollection"/> instance.
        /// </returns>
        public static TwitterDirectMessageCollection GetDirectMessagesSent(OAuthTokens tokens, long sinceStatusId, long maxStatusId, int count, int page)
        {
            Commands.DirectMessagesSentCommand command = new Commands.DirectMessagesSentCommand(tokens)
            {
                SinceStatusId = sinceStatusId,
                MaxStatusId = maxStatusId,
                Count = count,
                Page = page
            };

            command.Validate();
            if (!command.IsValid)
            {
                throw new CommandValidationException<TwitterDirectMessageCollection>()
                {
                    Command = command,
                    MethodName = "GetDirectMessagesSent"
                };
            }

            TwitterDirectMessageCollection result = Core.CommandPerformer<TwitterDirectMessageCollection>.PerformAction(command);

            return result;
        }

        /// <summary>
        /// Sends a new direct message to the specified user from the authenticating user.
        /// </summary>
        /// <param name="tokens">The OAuth tokens.</param>
        /// <param name="userId">The user id of the recipient user.</param>
        /// <param name="text">The text of your direct message.</param>
        /// <returns>A <see cref="TwitterDirectMessage"/> instance.</returns>
        public static TwitterDirectMessage Send(OAuthTokens tokens, long userId, string text)
        {
            return Send(tokens, userId, string.Empty, text);
        }

        /// <summary>
        /// Sends a new direct message to the specified user from the authenticating user.
        /// </summary>
        /// <param name="tokens">The OAuth tokens.</param>
        /// <param name="userName">The user nmae of the recipient user.</param>
        /// <param name="text">The text of your direct message.</param>
        /// <returns>A <see cref="TwitterDirectMessage"/> instance.</returns>
        public static TwitterDirectMessage Send(OAuthTokens tokens, string userName, string text)
        {
            return Send(tokens, -1, userName, text);
        }
        #endregion

        #region Non-static Methods
        /// <summary>
        /// Deletes this direct message.
        /// </summary>
        /// <returns>A <see cref="TwitterDirectMessage"/> instance.</returns>
        public TwitterDirectMessage Delete()
        {
            Commands.DeleteDirectMessageCommand command = new Commands.DeleteDirectMessageCommand(this.Tokens, this.Id);

            command.Validate();
            if (!command.IsValid)
            {
                throw new CommandValidationException<TwitterDirectMessage>()
                {
                    Command = command,
                    MethodName = "Delete"
                };
            }

            TwitterDirectMessage result = Core.CommandPerformer<TwitterDirectMessage>.PerformAction(command);

            return result;
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Sends a new direct message to the specified user from the authenticating user.
        /// </summary>
        /// <param name="tokens">The OAuth tokens.</param>
        /// <param name="userId">The user id.</param>
        /// <param name="userName">Name of the user.</param>
        /// <param name="text">The text of your direct message.</param>
        /// <returns>A <see cref="TwitterDirectMessage"/> instance.</returns>
        private static TwitterDirectMessage Send(OAuthTokens tokens, long userId, string userName, string text)
        {
            Commands.SendDirectMessageCommand command = new Commands.SendDirectMessageCommand(tokens, text)
            {
                RecipientUserId = userId,
                RecipientUserName = userName
            };

            command.Validate();
            if (!command.IsValid)
            {
                throw new CommandValidationException<TwitterDirectMessage>()
                {
                    Command = command,
                    MethodName = "Send"
                };
            }

            TwitterDirectMessage result = Core.CommandPerformer<TwitterDirectMessage>.PerformAction(command);

            return result;
        }
        #endregion
    }
}
