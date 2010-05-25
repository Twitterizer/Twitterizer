//-----------------------------------------------------------------------
// <copyright file="TwitterTrendTimeframe.cs" company="Patrick 'Ricky' Smith">
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
// <summary>The twitter trend timeframe class</summary>
//-----------------------------------------------------------------------

namespace Twitterizer
{
    using System;
    using System.Collections.ObjectModel;
    using Newtonsoft.Json.Linq;
    using Twitterizer.Core;

    /// <summary>
    /// The Twitter trend timeframe class.
    /// </summary>
    public class TwitterTrendTimeframe : TwitterObject
    {
        /// <summary>
        /// Gets or sets the effective date.
        /// </summary>
        /// <value>The effective date.</value>
        public DateTime EffectiveDate { get; set; }

        /// <summary>
        /// Gets or sets the trends.
        /// </summary>
        /// <value>The trends.</value>
        public Collection<TwitterTrend> Trends { get; set; }

        /// <summary>
        /// Deserializes the json.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>A <see cref="TwitterTrendTimeframe"/> object.</returns>
        internal static TwitterTrendTimeframe DeserializeJson(Newtonsoft.Json.Linq.JObject value)
        {
            TwitterTrendTimeframe result = new TwitterTrendTimeframe();
            result.EffectiveDate = new DateTime(1970, 1, 1, 0, 0, 0).AddSeconds((int)value.SelectToken("as_of"));
            result.Trends = new Collection<TwitterTrend>();

            JArray trendArray = (JArray)value.SelectToken(string.Format("trends.{0:yyyy-MM-dd HH:mm:ss}", result.EffectiveDate));

            foreach (JObject item in trendArray)
            {
                TwitterTrend newTrend = new TwitterTrend();

                JToken token = null;

                if (item.TryGetValue("query", out token))
                {
                    newTrend.SearchQuery = token.Value<string>();
                }

                if (item.TryGetValue("name", out token))
                {
                    newTrend.Name = token.Value<string>();
                }

                if (item.TryGetValue("url", out token))
                {
                    newTrend.Address = token.Value<string>();
                }

                result.Trends.Add(newTrend);
            }

            return result;
        }
    }
}
