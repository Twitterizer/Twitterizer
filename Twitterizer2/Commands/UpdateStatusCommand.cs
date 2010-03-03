//-----------------------------------------------------------------------
// <copyright file="UpdateStatusCommand.cs" company="Patrick 'Ricky' Smith">
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
// <summary>The command to update the user's status. (a.k.a. post a new tweet)</summary>
//-----------------------------------------------------------------------

namespace Twitterizer.Commands
{
    using System;
    using System.Globalization;

    /// <summary>
    /// The command to update the user's status. (a.k.a. post a new tweet)
    /// </summary>
    internal sealed class UpdateStatusCommand : Core.BaseCommand<TwitterStatus>
    {
        /// <summary>
        /// The base address to the API method.
        /// </summary>
        private const string Path = "http://api.twitter.com/1/statuses/update.json";

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateStatusCommand"/> class.
        /// </summary>
        /// <param name="requestTokens">The request tokens.</param>
        public UpdateStatusCommand(OAuthTokens requestTokens)
            : base("POST", new Uri(Path), requestTokens)
        {
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the status text.
        /// </summary>
        /// <value>The status text.</value>
        public string Text { get; set; }

        /// <summary>
        /// Gets or sets the status id the new status is in reply to.
        /// </summary>
        /// <value>The status id.</value>
        public long InReplyToStatusId { get; set; }

        /// <summary>
        /// Gets or sets the latitude.
        /// </summary>
        /// <value>The latitude.</value>
        public string Latitude { get; set; }

        /// <summary>
        /// Gets or sets the longitude.
        /// </summary>
        /// <value>The longitude.</value>
        public string Longitude { get; set; }
        #endregion

        /// <summary>
        /// Initializes the command.
        /// </summary>
        public override void Init()
        {
            this.RequestParameters.Add("status", this.Text);

            if (this.InReplyToStatusId > 0)
                this.RequestParameters.Add("in_reply_to_status_id", this.InReplyToStatusId.ToString(CultureInfo.CurrentCulture));

            if (!string.IsNullOrEmpty(this.Latitude))
                this.RequestParameters.Add("lat", this.Latitude);

            if (!string.IsNullOrEmpty(this.Longitude))
                this.RequestParameters.Add("long", this.Longitude);
        }

        /// <summary>
        /// Validates this instance.
        /// </summary>
        public override void Validate()
        {
            // TODO: Ricky - Add Latitude and Longitude value validation
            this.IsValid = !string.IsNullOrEmpty(this.Text);
        }
    }
}
