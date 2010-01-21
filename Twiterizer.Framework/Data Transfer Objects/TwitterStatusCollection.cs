/* This file is part of the Twitterizer library <http://code.google.com/p/twitterizer/>
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
    using System.Collections;

    /// <summary>
    /// A collection of <see cref="Twitterizer.Framework.TwitterStatus"/>
    /// </summary>
    [Serializable]
    public class TwitterStatusCollection : CollectionBase
    {
        /// <summary>
        /// Gets or sets the rate limit.
        /// </summary>
        /// <value>The rate limit.</value>
        public int? RateLimit { get; set; }

        /// <summary>
        /// Gets or sets the rate limit remaining.
        /// </summary>
        /// <value>The rate limit remaining.</value>
        public int? RateLimitRemaining { get; set; }

        /// <summary>
        /// Gets or sets the rate limit reset.
        /// </summary>
        /// <value>The rate limit reset.</value>
        public DateTime? RateLimitReset { get; set; }

        /// <summary>
        /// Gets or sets the next cursor.
        /// </summary>
        /// <value>The next cursor.</value>
        public long? NextCursor { get; set; }

        /// <summary>
        /// Gets or sets the previous cursor.
        /// </summary>
        /// <value>The previous cursor.</value>
        public long? PreviousCursor { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="Twitterizer.Framework.TwitterStatus"/> at the specified index.
        /// </summary>
        /// <value></value>
        public TwitterStatus this[int index]
        {
            get
            {
                return (TwitterStatus)List[index];
            }

            set
            {
                List[index] = value;
            }
        }

        /// <summary>
        /// Adds the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public int Add(TwitterStatus value)
        {
            return List.Add(value);
        }

        /// <summary>
        /// Indexes the of.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public int IndexOf(TwitterStatus value)
        {
            return List.IndexOf(value);
        }

        /// <summary>
        /// Inserts the specified index.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <param name="value">The value.</param>
        public void Insert(int index, TwitterStatus value)
        {
            List.Insert(index, value);
        }

        /// <summary>
        /// Removes the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        public void Remove(TwitterStatus value)
        {
            List.Remove(value);
        }

        /// <summary>
        /// Determines whether the specified value is contained within the collection..
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>
        ///     <c>true</c> if the collection contains the specified value; otherwise, <c>false</c>.
        /// </returns>
        public bool Contains(TwitterStatus value)
        {
            // If value is not of type Int16, this will return false.
            return List.Contains(value);
        }
    }
}