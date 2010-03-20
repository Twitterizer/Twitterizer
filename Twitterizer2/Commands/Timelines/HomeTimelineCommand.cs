//-----------------------------------------------------------------------
// <copyright file="HomeTimelineCommand.cs" company="Patrick 'Ricky' Smith">
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
// <summary>The Home Timeline Command class</summary>
//-----------------------------------------------------------------------
namespace Twitterizer.Commands
{
    using System;
    using System.Globalization;

    /// <summary>
    /// The Home Timeline Command
    /// </summary>
    [Serializable]
    internal sealed class HomeTimelineCommand :
        Core.PagedCommand<TwitterStatusCollection>
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="HomeTimelineCommand"/> class.
        /// </summary>
        /// <param name="tokens">The request tokens.</param>
        public HomeTimelineCommand(OAuthTokens tokens)
            : base("GET", new Uri("http://api.twitter.com/1/statuses/home_timeline.json"), tokens)
        {
            if (tokens == null)
            {
                throw new ArgumentNullException("tokens");
            }
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the count of statuses to obtain.
        /// </summary>
        /// <value>The count of statuses to obtain.</value>
        public int Count { get; set; }

        /// <summary>
        /// Gets or sets the id of the status to obtain all statuses posted thereafter.
        /// </summary>
        /// <value>The status id.</value>
        public long SinceId { get; set; }

        /// <summary>
        /// Gets or sets the max status id to obtain.
        /// </summary>
        /// <value>The max status id.</value>
        public long MaxId { get; set; }
        #endregion

        /// <summary>
        /// Initializes the command.
        /// </summary>
        public override void Init()
        {
            if (this.Count > 0)
                this.RequestParameters.Add("count", this.Count.ToString(CultureInfo.InvariantCulture));
            if (this.SinceId > 0)
                this.RequestParameters.Add("since_id", this.SinceId.ToString(CultureInfo.InvariantCulture));
            if (this.MaxId > 0)
                this.RequestParameters.Add("max_id", this.MaxId.ToString(CultureInfo.InvariantCulture));

            if (this.Page <= 1)
            {
                this.Page = 1;
            }

            this.RequestParameters.Add(
                "page", 
                this.Page.ToString(CultureInfo.InvariantCulture));
        }

        /// <summary>
        /// Validates this instance.
        /// </summary>
        public override void Validate()
        {
            this.IsValid = this.Tokens != null;
        }

        /// <summary>
        /// Clones this instance.
        /// </summary>
        /// <returns>A cloned command object.</returns>
        internal override Core.BaseCommand<TwitterStatusCollection> Clone()
        {
            return new HomeTimelineCommand(this.Tokens)
            {
                Page = this.Page,
                Count = this.Count,
                MaxId = this.MaxId,
                SinceId = this.SinceId
            };
        }
    }
}
