//-----------------------------------------------------------------------
// <copyright file="UserIdCollection.cs" company="Patrick 'Ricky' Smith">
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
// <author>Edgardo Vega</author>
// <summary>The twitter list collection class.</summary>
//-----------------------------------------------------------------------
namespace Twitterizer
{
    using System;
    using Twitterizer.Core;
    using Newtonsoft.Json.Linq;
    using Newtonsoft.Json;
    using System.Collections.ObjectModel;

    /// <summary>
    /// The twitter list collection class.
    /// </summary>
    [Serializable]
    public class UserIdCollection : Collection<decimal>, ITwitterObject
    {
        /// <summary>
        /// Gets or sets the next cursor.
        /// </summary>
        /// <value>The next cursor.</value>
        public long NextCursor { get; set; }

        /// <summary>
        /// Gets or sets the previous cursor.
        /// </summary>
        /// <value>The previous cursor.</value>
        public long PreviousCursor { get; set; }

        /// <summary>
        /// Gets or sets the request parameters.
        /// </summary>
        /// <value>The request parameters.</value>
        public new OAuthTokens Tokens { get; set; }

        /// <summary>
        /// Gets or sets information about the user's rate usage.
        /// </summary>
        /// <value>The rate limiting object.</value>
        public new RateLimiting RateLimiting { get; set; }

        /// <summary>
        /// Gets or sets the command.
        /// </summary>
        /// <value>The command.</value>
        internal Core.TwitterCommand<UserIdCollection> Command { get; set; }

        /// <summary>
        /// Gets the next page.
        /// </summary>
        /// <returns>A <see cref="TwitterListCollection"/> instance.</returns>
        /// <value>The next page.</value>
        public TwitterResponse<UserIdCollection> NextPage()
        {
            CursorPagedCommand<UserIdCollection> newCommand =
                (CursorPagedCommand<UserIdCollection>)this.Command.Clone();

            if (newCommand.Cursor == 0)
                return null;

            newCommand.Cursor = this.NextCursor;

            return CommandPerformer<UserIdCollection>.PerformAction(newCommand);
        }

        /// <summary>
        /// Gets the previous page.
        /// </summary>
        /// <returns>A <see cref="TwitterListCollection"/> instance.</returns>
        /// <value>The previous page.</value>
        public TwitterResponse<UserIdCollection> PreviousPage()
        {
            CursorPagedCommand<UserIdCollection> newCommand =
                (CursorPagedCommand<UserIdCollection>)this.Command.Clone();

            if (newCommand.Cursor == 0)
                return null;

            newCommand.Cursor = this.PreviousCursor;

            return Core.CommandPerformer<UserIdCollection>.PerformAction(newCommand);
        }
              

        /// <summary>
        /// Deserializes the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        internal static UserIdCollection DeserializeWrapper(JObject value)
        {
            if (value == null || value.SelectToken("ids") == null)
                return null;

            decimal[] parsedIds = JsonConvert.DeserializeObject<decimal[]>(value.SelectToken("ids").ToString());

            UserIdCollection result = new UserIdCollection
                                                      {
                                                          NextCursor = value.SelectToken("next_cursor").Value<long>(),
                                                          PreviousCursor =
                                                              value.SelectToken("previous_cursor").Value<long>()
                                                      };

            foreach (decimal t in parsedIds)
            {
                result.Add(t);
            }

            return result;
        }
    }
}
