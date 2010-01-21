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
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// The list of all query parameters that are acceptible by the twitter api.
    /// </summary>
    public enum TwitterParameterNames
    {
        /// <summary>
        /// The numerical ID of the item to retrieve. 
        /// </summary>
        ID,
        
        /// <summary>
        /// A datetime to be used as the beginning of a date range.
        /// </summary>
        Since,
        
        /// <summary>
        /// A numerical ID of the least recent item to be returned.
        /// </summary>
        SinceID,
        
        /// <summary>
        /// A numerical ID of the most recent item to be returned.
        /// </summary>
        MaxID,
        
        /// <summary>
        /// A numerical value indicating how many items to return.
        /// </summary>
        Count,
        
        /// <summary>
        /// A numerical value indicating a page number to be returned.
        /// </summary>
        Page,
        
        /// <summary>
        /// A numerical, or string, value indicating the screenname or ID number of a user.
        /// </summary>
        UserID,
        
        /// <summary>
        /// A string value indicating the screen name of the user to match.
        /// </summary>
        ScreenName,
        
        /// <summary>
        /// A numerical value indicating the cursor index to be retrieved.
        /// </summary>
        Cursor
    }

    /// <summary>
    /// The TwitterParameters dictionary class.
    /// </summary>
    [Serializable]
    public class TwitterParameters : Dictionary<TwitterParameterNames, object>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TwitterParameters"/> class.
        /// </summary>
        public TwitterParameters()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TwitterParameters"/> class.
        /// </summary>
        /// <param name="Name">The name.</param>
        /// <param name="Value">The value.</param>
        public TwitterParameters(TwitterParameterNames parameterName, object value)
        {
            this.Add(parameterName, value);
        }

        /// <summary>
        /// Builds the action URI.
        /// </summary>
        /// <param name="Uri">The base URI.</param>
        /// <returns>The uri for the specified action, including parameters.</returns>
        public string BuildActionUri(string uri)
        {
            if (Count == 0)
            {
                return uri;
            }

            string parameterString = string.Empty;

            foreach (TwitterParameterNames key in Keys)
            {
                switch (key)
                {
                    case TwitterParameterNames.Since:
                        parameterString = string.Format("{0}&since={1}", parameterString, this[key]);
                        break;
                    case TwitterParameterNames.SinceID:
                        parameterString = string.Format("{0}&since_id={1}", parameterString, this[key]);
                        break;
                    case TwitterParameterNames.Count:
                        parameterString = string.Format("{0}&count={1}", parameterString, this[key]);
                        break;
                    case TwitterParameterNames.Page:
                        parameterString = string.Format("{0}&page={1}", parameterString, this[key]);
                        break;
                    case TwitterParameterNames.ID:
                        parameterString = string.Format("{0}&id={1}", parameterString, this[key]);
                        break;
                    case TwitterParameterNames.MaxID:
                        parameterString = string.Format("{0}&max_id={1}", parameterString, this[key]);
                        break;
                    case TwitterParameterNames.UserID:
                        parameterString = string.Format("{0}&user_id={1}", parameterString, this[key]);
                        break;
                    case TwitterParameterNames.ScreenName:
                        parameterString = string.Format("{0}&screen_name={1}", parameterString, this[key]);
                        break;
                    case TwitterParameterNames.Cursor:
                        parameterString = string.Format("{0}&cursor={1}", parameterString, this[key]);
                        break;
                }
            }

            if (string.IsNullOrEmpty(parameterString))
            {
                return uri;
            }

            // First char of parameterString is a leading & that should be removed
            return string.Format("{2}{0}?{1}", uri, parameterString.Remove(0, 1), Twitter.Domain);
        }

        /// <summary>
        /// Adds a key/value pair to the collection
        /// </summary>
        /// <param name="Key">The key.</param>
        /// <param name="Value">The value.</param>
        public new void Add(TwitterParameterNames key, object value)
        {
            switch (key)
            {
                case TwitterParameterNames.Since:
                    if (!(value is DateTime))
                    {
                        throw new ApplicationException("Value given for since was not a Date.");
                    }

                    DateTime dateValue = (DateTime)value;

                    // RFC1123 date string
                    base.Add(key, dateValue.ToString("r"));

                    break;
                default:
                    base.Add(key, value.ToString());
                    break;
            }
        }
    }
}
