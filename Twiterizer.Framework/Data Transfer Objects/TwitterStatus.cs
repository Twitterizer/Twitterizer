/*
 * This file is part of the Twitterizer library <http://code.google.com/p/twitterizer/>
 *
 * Copyright (c) 2010, Patrick "Ricky" Smith <ricky@digitally-born.com>
 * All rights reserved.
 * 
 * Redistribution and use in source and binary forms, with or without modification, are 
 * permitted provided that the following conditions are met:
 *
 * - Redistributions of source code must retain the above copyright notice, this list 
 *   of conditions and the following disclaimer.
 * - Redistributions in binary form must reproduce the above copyright notice, this list 
 *   of conditions and the following disclaimer in the documentation and/or other 
 *   materials provided with the distribution.
 * - Neither the name of the Twitterizer nor the names of its contributors may be 
 *   used to endorse or promote products derived from this software without specific 
 *   prior written permission.
 *
 * THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND 
 * ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED 
 * WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. 
 * IN NO EVENT SHALL THE COPYRIGHT OWNER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, 
 * INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT 
 * NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR 
 * PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, 
 * WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) 
 * ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE 
 * POSSIBILITY OF SUCH DAMAGE.
 */
namespace Twitterizer.Framework
{
    using System;

    /// <summary>
    /// Represents a single status (aka tweet)
    /// </summary>
    [Serializable]
    public class TwitterStatus : TwitterObject
    {
        /// <summary>
        /// Gets or sets the created.
        /// </summary>
        /// <value>The created.</value>
        public DateTime Created { get; set; }

        /// <summary>
        /// Gets or sets the ID.
        /// </summary>
        /// <value>The ID.</value>
        public long ID { get; set; }

        /// <summary>
        /// Gets or sets the text.
        /// </summary>
        /// <value>The text.</value>
        public string Text { get; set; }

        /// <summary>
        /// Gets or sets the source.
        /// </summary>
        /// <value>The source.</value>
        public string Source { get; set; }

        /// <summary>
        /// Gets or sets the recipient ID.
        /// </summary>
        /// <value>The recipient ID.</value>
        public int RecipientID { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is truncated.
        /// </summary>
        /// <value>
        ///     <c>true</c> if this instance is truncated; otherwise, <c>false</c>.
        /// </value>
        public bool IsTruncated { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is favorited.
        /// </summary>
        /// <value>
        ///     <c>true</c> if this instance is favorited; otherwise, <c>false</c>.
        /// </value>
        public bool IsFavorited { get; set; }

        /// <summary>
        /// Gets or sets the in reply to status ID.
        /// </summary>
        /// <value>The in reply to status ID.</value>
        public long InReplyToStatusID { get; set; }

        /// <summary>
        /// Gets or sets the in reply to user ID.
        /// </summary>
        /// <value>The in reply to user ID.</value>
        public int InReplyToUserID { get; set; }

        /// <summary>
        /// Gets or sets the twitter user.
        /// </summary>
        /// <value>The twitter user.</value>
        public TwitterUser TwitterUser { get; set; }

        /// <summary>
        /// Gets or sets the recipient.
        /// </summary>
        /// <value>The recipient.</value>
        public TwitterUser Recipient { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is direct message.
        /// </summary>
        /// <value>
        ///     <c>true</c> if this instance is direct message; otherwise, <c>false</c>.
        /// </value>
        public bool IsDirectMessage { get; set; }
    }
}
