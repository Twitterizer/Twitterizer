//-----------------------------------------------------------------------
// <copyright file="ListStatusesCommand.cs" company="Patrick 'Ricky' Smith">
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
// <summary>The get list statuses command class</summary>
//-----------------------------------------------------------------------

namespace Twitterizer.Commands
{
    using System;
    using System.Globalization;
    using Twitterizer;
    using Twitterizer.Core;

    /// <summary>
    /// The get list statuses command class
    /// </summary>
    internal sealed class ListStatusesCommand : PagedCommand<TwitterStatusCollection>
    {
        /// <summary>
        /// The base address to the API method.
        /// </summary>
        private const string Path = "http://api.twitter.com/1/{0}/lists/{1}/statuses.json";

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="ListStatusesCommand"/> class.
        /// </summary>
        /// <param name="requestTokens">The request tokens.</param>
        /// <param name="username">The username.</param>
        /// <param name="listId">The list id.</param>
        public ListStatusesCommand(OAuthTokens requestTokens, string username, long listId)
            : base("GET", new Uri(Path), requestTokens)
        {
            if (string.IsNullOrEmpty(username))
            {
                throw new ArgumentNullException("username");
            }

            if (listId <= 0)
            {
                throw new ArgumentNullException("listId");
            }

            this.Username = username;
            this.ListId = listId;
            this.Uri = new Uri(string.Format(CultureInfo.CurrentCulture, Path, username, listId));
        }
        #endregion

        #region API Properties
        /// <summary>
        /// Gets the username.
        /// </summary>
        /// <value>The username.</value>
        public string Username { get; private set; }

        /// <summary>
        /// Gets the list id.
        /// </summary>
        /// <value>The list id.</value>
        public long ListId { get; private set; }

        /// <summary>
        /// Gets or sets the since id.
        /// </summary>
        /// <value>The since id.</value>
        public long SinceId { get; set; }

        /// <summary>
        /// Gets or sets the max id.
        /// </summary>
        /// <value>The max id.</value>
        public long MaxId { get; set; }

        /// <summary>
        /// Gets or sets the number of items per page to request.
        /// </summary>
        /// <value>The number of items per page.</value>
        public int ItemsPerPage { get; set; }
        #endregion

        /// <summary>
        /// Initializes the command.
        /// </summary>
        public override void Init()
        {
            if (this.SinceId > 0)
            {
                this.RequestParameters.Add("since_id", this.SinceId.ToString(CultureInfo.InvariantCulture));
            }

            if (this.MaxId > 0)
            {
                this.RequestParameters.Add("max_id", this.MaxId.ToString(CultureInfo.InvariantCulture));
            }

            if (this.ItemsPerPage > 0)
            {
                this.RequestParameters.Add("per_page", this.ItemsPerPage.ToString(CultureInfo.InvariantCulture));
            }

            if (this.Page > 0)
            {
                this.RequestParameters.Add("page", this.Page.ToString(CultureInfo.InvariantCulture));
            }
        }

        /// <summary>
        /// Validates this instance.
        /// </summary>
        public override void Validate()
        {
            this.IsValid = true;
        }

        /// <summary>
        /// Clones this instance.
        /// </summary>
        /// <returns>
        /// A new instance of the <see cref="Twitterizer.Core.PagedCommand{T}"/> interface.
        /// </returns>
        internal override TwitterCommand<TwitterStatusCollection> Clone()
        {
            return new ListStatusesCommand(this.Tokens, this.Username, this.ListId)
            {
                MaxId = this.MaxId,
                SinceId = this.SinceId,
                ItemsPerPage = this.ItemsPerPage,
                Page = this.Page,
            };
        }
    }
}
