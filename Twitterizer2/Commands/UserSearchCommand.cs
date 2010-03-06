//-----------------------------------------------------------------------
// <copyright file="UserSearchCommand.cs" company="Patrick 'Ricky' Smith">
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
// <summary>The user search command class.</summary>
//-----------------------------------------------------------------------

namespace Twitterizer.Commands
{
    using System;
    using System.Globalization;
    using Twitterizer.Core;

    /// <summary>
    /// The User Search Command class.
    /// </summary>
    internal sealed class UserSearchCommand : PagedCommand<TwitterUserCollection>
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="UserSearchCommand"/> class.
        /// </summary>
        /// <param name="tokens">The request tokens.</param>
        /// <param name="query">The query.</param>
        public UserSearchCommand(OAuthTokens tokens, string query)
            : base("GET", new Uri("http://api.twitter.com/1/users/search.json"), tokens)
        {
            if (tokens == null)
            {
                throw new ArgumentNullException("tokens");
            }

            if (string.IsNullOrEmpty(query))
            {
                throw new ArgumentNullException("query");
            }

            this.Query = query;
        }
        #endregion

        #region API Properties
        /// <summary>
        /// Gets or sets the query.
        /// </summary>
        /// <value>The query.</value>
        public string Query { get; set; }

        /// <summary>
        /// Gets or sets the number per page. Cannot be greater than 20.
        /// </summary>
        /// <value>The number per page.</value>
        public int NumberPerPage { get; set; }
        #endregion

        /// <summary>
        /// Validates this instance.
        /// </summary>
        public override void Validate()
        {
            this.IsValid = false;

            if (this.Page > 0 && this.NumberPerPage > 0)
            {
                if (this.NumberPerPage * this.Page > 1000)
                {
                    return;
                }
            }

            this.IsValid = true;
        }

        /// <summary>
        /// Initializes the command.
        /// </summary>
        public override void Init()
        {
            this.RequestParameters.Add("q", this.Query);

            if (this.NumberPerPage > 0)
                this.RequestParameters.Add("per_page", this.NumberPerPage.ToString(CultureInfo.InvariantCulture));

            if (this.Page > 0)
                this.RequestParameters.Add("page", this.Page.ToString(CultureInfo.InvariantCulture));
        }

        /// <summary>
        /// Clones this instance.
        /// </summary>
        /// <returns>
        /// A new instance of the <see cref="Twitterizer.Core.PagedCommand{T}"/> interface.
        /// </returns>
        internal override PagedCommand<TwitterUserCollection> Clone()
        {
            return new UserSearchCommand(this.Tokens, this.Query)
            {
                NumberPerPage = this.NumberPerPage,
                Page = this.Page
            };
        }
    }
}
