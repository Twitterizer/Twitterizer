//-----------------------------------------------------------------------
// <copyright file="ShowFriendshipCommand.cs" company="Patrick 'Ricky' Smith">
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
// <summary>The show friendship command class.</summary>
//-----------------------------------------------------------------------

namespace Twitterizer.Commands
{
    using System;
    using System.Globalization;
    using Twitterizer.Core;

    /// <summary>
    /// The show friendship command class.
    /// </summary>
    internal sealed class ShowFriendshipCommand : BaseCommand<TwitterRelationship>
    {
        /// <summary>
        /// The base address to the API method.
        /// </summary>
        private const string Path = "http://api.twitter.com/1/friendships/show.json";

         #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="ShowFriendshipCommand"/> class.
        /// </summary>
        /// <param name="requestTokens">The request tokens.</param>
        public ShowFriendshipCommand(OAuthTokens requestTokens)
            : base("GET", new Uri(Path), requestTokens)
        {
        }
        #endregion

        #region API Parameters
        /// <summary>
        /// Gets or sets the id of the source user.
        /// </summary>
        /// <value>The source id.</value>
        public long SourceId { get; set; }

        /// <summary>
        /// Gets or sets the screenname of the source user.
        /// </summary>
        /// <value>The screenname of the source user.</value>
        public string SourceScreenName { get; set; }

        /// <summary>
        /// Gets or sets the id of the target user.
        /// </summary>
        /// <value>The target id.</value>
        public long TargetId { get; set; }

        /// <summary>
        /// Gets or sets the screenname of the target user.
        /// </summary>
        /// <value>The screenname of the target user.</value>
        public string TargetScreenName { get; set; }
        #endregion

        /// <summary>
        /// Initializes the command.
        /// </summary>
        public override void Init()
        {
            if (this.SourceId > 0)
            {
                this.RequestParameters.Add("source_id", this.SourceId.ToString(CultureInfo.InvariantCulture));
            }

            // Only add the screen name if the id is not given.
            if (!string.IsNullOrEmpty(this.SourceScreenName) && this.SourceId <= 0)
            {
                this.RequestParameters.Add("source_screen_name", this.SourceScreenName);
            }

            if (this.TargetId > 0)
            {
                this.RequestParameters.Add("target_id", this.TargetId.ToString(CultureInfo.InvariantCulture));
            }

            // Only add the screen name if the id is not given.
            if (!string.IsNullOrEmpty(this.TargetScreenName) && this.TargetId <= 0)
            {
                this.RequestParameters.Add("target_screen_name", this.TargetScreenName);
            }
        }

        /// <summary>
        /// Validates this instance.
        /// </summary>
        public override void Validate()
        {
            // If the request is unauthorized
            if (this.Tokens == null)
            {
                // Source information is required
                if (this.SourceId <= 0 && string.IsNullOrEmpty(this.SourceScreenName))
                {
                    this.IsValid = false;
                    return;
                }
            }

            // Target information is always required (one of the variables)
            this.IsValid = this.TargetId > 0 || !string.IsNullOrEmpty(this.TargetScreenName);
        }
    }
}
