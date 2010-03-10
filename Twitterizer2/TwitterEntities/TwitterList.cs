//-----------------------------------------------------------------------
// <copyright file="TwitterList.cs" company="Patrick 'Ricky' Smith">
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
// <summary>The twitter list entity class</summary>
//-----------------------------------------------------------------------

namespace Twitterizer
{
    using System.Diagnostics;
    using System.Runtime.Serialization;
    using Twitterizer.Core;

    /// <summary>
    /// The twitter list entity class
    /// </summary>
    [DataContract]
    [DebuggerDisplay("TwitterList = {ScreenName}")]
    public class TwitterList : BaseObject
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="TwitterList"/> class.
        /// </summary>
        /// <param name="tokens">OAuth access tokens.</param>
        public TwitterList(OAuthTokens tokens) 
            : base()
        {
            this.Tokens = tokens;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TwitterList"/> class.
        /// </summary>
        internal TwitterList()
            : base()
        {
        }
        #endregion

        #region API properties
        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        /// <value>The list id.</value>
        [DataMember(Name = "id")]
        public long Id { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The list name.</value>
        [DataMember(Name = "name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the full name.
        /// </summary>
        /// <value>The full name.</value>
        [DataMember(Name = "full_name")]
        public string FullName { get; set; }

        /// <summary>
        /// Gets or sets the slug.
        /// </summary>
        /// <value>The list slug.</value>
        [DataMember(Name = "slug")]
        public string Slug { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>The description.</value>
        [DataMember(Name = "description")]
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the number of subscribers.
        /// </summary>
        /// <value>The number of subscribers.</value>
        [DataMember(Name = "subscriber_count")]
        public int NumberOfSubscribers { get; set; }

        /// <summary>
        /// Gets or sets the number of members.
        /// </summary>
        /// <value>The number of members.</value>
        [DataMember(Name = "member_count")]
        public int NumberOfMembers { get; set; }

        /// <summary>
        /// Gets or sets the absolute path.
        /// </summary>
        /// <value>The absolute path.</value>
        [DataMember(Name = "uri")]
        public string AbsolutePath { get; set; }

        /// <summary>
        /// Gets or sets the mode.
        /// </summary>
        /// <value>The list mode.</value>
        [DataMember(Name = "mode")]
        public string Mode { get; set; }

        /// <summary>
        /// Gets or sets the user that owns the list.
        /// </summary>
        /// <value>The owning user.</value>
        [DataMember(Name = "user")]
        public TwitterUser User { get; set; }
        #endregion

        #region Calculated Properties
        /// <summary>
        /// Gets a value indicating whether this instance is public.
        /// </summary>
        /// <value><c>true</c> if this instance is public; otherwise, <c>false</c>.</value>
        public bool IsPublic
        {
            get
            {
                return this.Mode == "public";
            }
        }
        #endregion

        #region Static Members
        /// <summary>
        /// Creates a new list.
        /// </summary>
        /// <param name="tokens">The oauth tokens.</param>
        /// <param name="username">The username.</param>
        /// <param name="name">The list name.</param>
        /// <param name="isPublic">if set to <c>true</c> creates a public list.</param>
        /// <param name="description">The description.</param>
        /// <returns>A <see cref="TwitterList"/> instance.</returns>
        public static TwitterList New(OAuthTokens tokens, string username, string name, bool isPublic, string description)
        {
            Commands.CreateListCommand command = new Twitterizer.Commands.CreateListCommand(tokens, name, username)
            {
                IsPublic = isPublic,
                Description = description
            };

            return Core.CommandPerformer<TwitterList>.PerformAction(command);
        }

        /// <summary>
        /// Modifies a list.
        /// </summary>
        /// <param name="tokens">The oauth tokens.</param>
        /// <param name="username">The username.</param>
        /// <param name="listId">The list id.</param>
        /// <param name="name">The list name.</param>
        /// <param name="isPublic">if set to <c>true</c> creates a public list.</param>
        /// <param name="description">The description.</param>
        /// <returns>A <see cref="TwitterList"/> instance.</returns>
        public static TwitterList Update(OAuthTokens tokens, string username, long listId, string name, bool isPublic, string description)
        {
            Commands.UpdateListCommand command = new Twitterizer.Commands.UpdateListCommand(tokens, username, listId)
            {
                Name = name,
                IsPublic = isPublic,
                Description = description
            };

            return Core.CommandPerformer<TwitterList>.PerformAction(command);
        }
        #endregion

        #region Non-Static Members
        /// <summary>
        /// Creates a new list.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <param name="name">The list name.</param>
        /// <param name="isPublic">if set to <c>true</c> creates a public list.</param>
        /// <param name="description">The description.</param>
        /// <returns>A <see cref="TwitterList"/> instance.</returns>
        public TwitterList New(string username, string name, bool isPublic, string description)
        {
            Commands.CreateListCommand command = new Twitterizer.Commands.CreateListCommand(this.Tokens, name, username)
            {
                IsPublic = isPublic,
                Description = description
            };

            return Core.CommandPerformer<TwitterList>.PerformAction(command);
        }
        #endregion
    }
}
