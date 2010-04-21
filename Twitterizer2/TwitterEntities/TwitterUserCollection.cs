//-----------------------------------------------------------------------
// <copyright file="TwitterUserCollection.cs" company="Patrick Ricky Smith">
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
// <summary>The collection class containing zero or more TwitterUser objects.</summary>
//-----------------------------------------------------------------------
namespace Twitterizer
{
    using Twitterizer.Core;

    /// <summary>
    /// The TwitterUserCollection class.
    /// </summary>
    [System.Serializable]
    public class TwitterUserCollection : TwitterCollection<TwitterUser>
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
        /// Gets or sets information about the user's rate usage.
        /// </summary>
        /// <value>The rate limiting object.</value>
        public new RateLimiting RateLimiting { get; set; }

        /// <summary>
        /// Gets or sets the paged command.
        /// </summary>
        /// <value>The paged command.</value>
        internal PagedCommand<TwitterUserCollection> PagedCommand { get; set; }

        /// <summary>
        /// Gets or sets the cursor paged command.
        /// </summary>
        /// <value>The cursor paged command.</value>
        internal CursorPagedCommand<TwitterUserWrapper> CursorPagedCommand { get; set; }

        /// <summary>
        /// Gets the next page.
        /// </summary>
        /// <returns>A <see cref="TwitterUserCollection"/> instance.</returns>
        /// <value>The next page.</value>
        public TwitterUserCollection NextPage()
        {
            if (this.PagedCommand != null)
            {
                PagedCommand<TwitterUserCollection> newCommand =
                    (PagedCommand<TwitterUserCollection>)this.PagedCommand.Clone();
                newCommand.Page += 1;

                TwitterUserCollection result = Core.CommandPerformer<TwitterUserCollection>.PerformAction(newCommand);
                result.PagedCommand = newCommand;
                return result;
            }
            else if (this.CursorPagedCommand != null)
            {
                CursorPagedCommand<TwitterUserWrapper> newCommand =
                    (CursorPagedCommand<TwitterUserWrapper>)this.CursorPagedCommand.Clone();
                newCommand.Cursor = this.NextCursor;

                TwitterUserCollection result = Core.CommandPerformer<TwitterUserWrapper>.PerformAction(newCommand).Users;
                result.CursorPagedCommand = newCommand;
                return result;
            }
            else
            {
                throw new System.NotSupportedException("Paging is not supported for this API call.");
            }
        }

        /// <summary>
        /// Gets the previous page.
        /// </summary>
        /// <returns>A <see cref="TwitterUserCollection"/> instance.</returns>
        /// <value>The previous page.</value>
        public TwitterUserCollection PreviousPage()
        {
            if (this.PagedCommand != null)
            {
                PagedCommand<TwitterUserCollection> newCommand =
                    (PagedCommand<TwitterUserCollection>)this.PagedCommand.Clone();
                newCommand.Page -= 1;

                TwitterUserCollection result = Core.CommandPerformer<TwitterUserCollection>.PerformAction(newCommand);
                result.PagedCommand = newCommand;
                return result;
            }
            else if (this.CursorPagedCommand != null)
            {
                CursorPagedCommand<TwitterUserWrapper> newCommand =
                    (CursorPagedCommand<TwitterUserWrapper>)this.CursorPagedCommand.Clone();
                newCommand.Cursor = this.PreviousCursor;

                TwitterUserCollection result = Core.CommandPerformer<TwitterUserWrapper>.PerformAction(newCommand).Users;
                result.CursorPagedCommand = newCommand;
                return result;
            }
            else
            {
                throw new System.NotSupportedException("Paging is not supported for this API call.");
            }
        }
    }
}
