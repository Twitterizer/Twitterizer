﻿//-----------------------------------------------------------------------
// <copyright file="UserShowCommand.cs" company="Patrick 'Ricky' Smith">
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
// <summary>The 'Show User' command class.</summary>
//-----------------------------------------------------------------------

namespace Twitterizer.Commands
{
    using System;
    using System.Globalization;
    using Twitterizer;

    /// <summary>
    /// The User Show Command
    /// </summary>
    /// <remarks>http://apiwiki.twitter.com/Twitter-REST-API-Method:-users%C2%A0show</remarks>
    public class UserShowCommand : Core.BaseCommand<TwitterUser>
    {
        /// <summary>
        /// The base address to the API method.
        /// </summary>
        private const string Path = "http://api.twitter.com/1/users/show.json";

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="UserShowCommand"/> class.
        /// </summary>
        /// <param name="requestTokens">The request tokens.</param>
        public UserShowCommand(OAuthTokens requestTokens)
            : base("GET", new Uri(Path), requestTokens)
        {
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the user ID.
        /// </summary>
        /// <value>The user ID.</value>
        public long UserId { get; set; }

        /// <summary>
        /// Gets or sets the name of the user.
        /// </summary>
        /// <value>The name of the user.</value>
        public string Username { get; set; }

        /// <summary>
        /// Gets or sets the name of the user ID or.
        /// </summary>
        /// <value>The name of the user ID or.</value>
        public string UserIdOrName { get; set; }
        #endregion

        /// <summary>
        /// Inits this instance.
        /// </summary>
        public override void Init()
        {
            if (!string.IsNullOrEmpty(this.UserIdOrName))
                this.RequestParameters.Add("id", this.UserIdOrName);
            
            if (this.UserId > 0)
                this.RequestParameters.Add("user_id", this.UserId.ToString(CultureInfo.CurrentCulture));
            
            if (!string.IsNullOrEmpty(this.Username))
                this.RequestParameters.Add("screen_name", this.Username);
        }

        /// <summary>
        /// Validates this instance.
        /// </summary>
        public override void Validate()
        {
            this.IsValid = this.Tokens != null || 
                this.UserId > 0 || 
                !string.IsNullOrEmpty(this.UserIdOrName) || 
                !string.IsNullOrEmpty(this.Username);
        }
    }
}
