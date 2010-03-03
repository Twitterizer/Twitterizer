﻿//-----------------------------------------------------------------------
// <copyright file="UserFollowersCommand.cs" company="Patrick 'Ricky' Smith">
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
// <summary>The command to obtain followers of a user.</summary>
//-----------------------------------------------------------------------
namespace Twitterizer.Commands
{
    using System;
    using System.Globalization;
    using Twitterizer;

    /// <summary>
    /// The command to obtain followers of a user.
    /// </summary>
    internal class UserFollowersCommand : 
        Core.BaseCommand<TwitterUserCollection>, 
        Core.IPagedCommand<TwitterUserCollection>
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="UserFollowersCommand"/> class.
        /// </summary>
        /// <param name="tokens">The request tokens.</param>
        public UserFollowersCommand(OAuthTokens tokens)
            : base("GET", new Uri("http://api.twitter.com/1/statuses/followers.json"), tokens)
        {
        }
        #endregion

        #region API Parameters

        /// <summary>
        /// Gets or sets the ID or screen name of the user for whom to request a list of followers. 
        /// </summary>
        /// <value>The name of the id or screen.</value>
        public string IdOrScreenName { get; set; }

        /// <summary>
        /// Gets or sets the ID of the user for whom to request a list of followers. 
        /// </summary>
        /// <value>The user id.</value>
        public long UserId { get; set; }
        
        /// <summary>
        /// Gets or sets the screen name of the user for whom to request a list of followers. 
        /// </summary>
        /// <value>The name of the screen.</value>
        public string ScreenName { get; set; }
        
        /// <summary>
        /// Gets or sets the cursor.
        /// </summary>
        /// <value>The cursor.</value>
        /// <remarks>
        /// Optional. 
        /// Breaks the results into pages. 
        /// A single page contains 100 users.
        /// </remarks>
        public long Cursor { get; set; }

        /// <summary>
        /// Gets or sets the page number to obtain.
        /// </summary>
        /// <value>The page number.</value>
        public int Page { get; set; }

        #endregion

        /// <summary>
        /// Initializes the command.
        /// </summary>
        public override void Init()
        {
            if (!string.IsNullOrEmpty(this.IdOrScreenName))
                this.RequestParameters.Add("id", this.IdOrScreenName);
            if (this.UserId > 0)
                this.RequestParameters.Add("user_id", this.UserId.ToString(CultureInfo.CurrentCulture));
            if (!string.IsNullOrEmpty(this.ScreenName))
                this.RequestParameters.Add("screen_name", this.ScreenName);
            if (this.Cursor > 0)
                this.RequestParameters.Add("cursor", this.Cursor.ToString(CultureInfo.CurrentCulture));
        }

        /// <summary>
        /// Validates this instance.
        /// </summary>
        public override void Validate()
        {
            this.IsValid = this.UserId > 0 ||
                !string.IsNullOrEmpty(this.IdOrScreenName) ||
                !string.IsNullOrEmpty(this.ScreenName);
        }

        /// <summary>
        /// Clones this instance.
        /// </summary>
        /// <returns>A cloned command object.</returns>
        public Twitterizer.Core.IPagedCommand<TwitterUserCollection> Clone()
        {
            UserFollowersCommand newCommand = new UserFollowersCommand(this.Tokens);

            newCommand.IdOrScreenName = this.IdOrScreenName;
            newCommand.ScreenName = this.ScreenName;
            newCommand.UserId = this.UserId;

            return newCommand;
        }
    }
}
