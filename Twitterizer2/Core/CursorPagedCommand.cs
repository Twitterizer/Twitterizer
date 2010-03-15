//-----------------------------------------------------------------------
// <copyright file="CursorPagedCommand.cs" company="Patrick 'Ricky' Smith">
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
// <summary>The interface that indicates that the command results can be paged through using cursors.</summary>
//-----------------------------------------------------------------------
namespace Twitterizer.Core
{
    using System;

    /// <summary>
    /// The CursorCursorPagedCommand class.
    /// </summary>
    /// <typeparam name="T">The type of BaseObject that the command returns.</typeparam>
    internal abstract class CursorPagedCommand<T> : BaseCommand<T>
        where T : ITwitterObject
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="CursorPagedCommand&lt;T&gt;"/> class.
        /// </summary>
        /// <param name="method">The method.</param>
        /// <param name="uri">The URI for the API method.</param>
        /// <param name="tokens">The request tokens.</param>
        protected CursorPagedCommand(string method, Uri uri, OAuthTokens tokens)
            : base(method, uri, tokens)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CursorPagedCommand&lt;T&gt;"/> class.
        /// </summary>
        /// <param name="method">The method.</param>
        /// <param name="tokens">The tokens.</param>
        protected CursorPagedCommand(string method, OAuthTokens tokens)
            : base(method, tokens)
        {
        }
        #endregion

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
    }
}
