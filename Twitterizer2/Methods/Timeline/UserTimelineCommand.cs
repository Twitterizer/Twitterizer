//-----------------------------------------------------------------------
// <copyright file="UserTimelineCommand.cs" company="Patrick 'Ricky' Smith">
//  This file is part of the Twitterizer library (http://www.twitterizer.net/)
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
// <summary>The user timeline command.</summary>
//-----------------------------------------------------------------------

namespace Twitterizer.Commands
{
    using System;
    using System.Globalization;
    using Twitterizer.Core;

    /// <summary>
    /// The user timeline command.
    /// </summary>
    internal sealed class UserTimelineCommand :
        PagedCommand<TwitterStatusCollection>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserTimelineCommand"/> class.
        /// </summary>
        /// <param name="tokens">The request tokens.</param>
        /// <param name="options">The options.</param>
        public UserTimelineCommand(OAuthTokens tokens, TimelineOptions options)
            : base(HTTPVerb.GET, "statuses/user_timeline.json", tokens, options)
        {
            if (tokens == null && options == null)
            {
                throw new ArgumentException("You must supply either OAuth tokens or identify a user in the TimelineOptions class.");
            }
        }

        /// <summary>
        /// Initializes the command.
        /// </summary>
        public override void Init()
        {
            // Enable opt-in beta for entities
            this.RequestParameters.Add("include_entities", "true");

            UserTimelineOptions options = this.OptionalProperties as UserTimelineOptions;

            if (options == null)
            {
                return;
            }

            if (options.UserId > 0)
                this.RequestParameters.Add("user_id", options.UserId.ToString(CultureInfo.InvariantCulture));

            if (!string.IsNullOrEmpty(options.ScreenName))
                this.RequestParameters.Add("screen_name", options.ScreenName);

            if (options.Count > 0)
                this.RequestParameters.Add("count", options.Count.ToString(CultureInfo.InvariantCulture));

            if (options.SinceStatusId > 0)
                this.RequestParameters.Add("since_id", options.SinceStatusId.ToString(CultureInfo.InvariantCulture));

            if (options.MaxStatusId > 0)
                this.RequestParameters.Add("max_id", options.MaxStatusId.ToString(CultureInfo.InvariantCulture));

            if (options.Page > 0)
                this.RequestParameters.Add("page", options.Page.ToString(CultureInfo.InvariantCulture));

            if (options.IncludeRetweets)
                this.RequestParameters.Add("include_rts", "true");
        }

        /// <summary>
        /// Clones this instance.
        /// </summary>
        /// <returns>A cloned command object.</returns>
        internal override Core.TwitterCommand<TwitterStatusCollection> Clone()
        {
            return new UserTimelineCommand(
                this.Tokens, 
                this.OptionalProperties as UserTimelineOptions);
        }
    }
}