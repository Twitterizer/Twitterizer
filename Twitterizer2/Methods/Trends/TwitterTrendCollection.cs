//-----------------------------------------------------------------------
// <copyright file="TwitterTrendCollection.cs" company="Patrick 'Ricky' Smith">
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
// <summary>The twitter trend collection class.</summary>
//-----------------------------------------------------------------------

namespace Twitterizer
{
    using System;
    using Newtonsoft.Json;

    /// <summary>
    /// The TwitterTrendCollection class. Represents multiple <see cref="Twitterizer.TwitterTrend"/> elements.
    /// </summary>
    [JsonConverter(typeof(TwitterTrendCollection.Converter))]
    [Serializable]
    public class TwitterTrendCollection : Core.TwitterCollection<TwitterTrend>
    {
        /// <summary>
        /// The Json converter class for the TwitterTrendCollection object
        /// </summary>
        internal class Converter : JsonConverter
        {
            /// <summary>
            /// Determines whether this instance can convert the specified object type.
            /// </summary>
            /// <param name="objectType">Type of the object.</param>
            /// <returns>
            /// <c>true</c> if this instance can convert the specified object type; otherwise, <c>false</c>.
            /// </returns>
            public override bool CanConvert(Type objectType)
            {
                return objectType == typeof(TwitterTrendCollection);
            }

            /// <summary>
            /// Reads the json.
            /// </summary>
            /// <param name="reader">The reader.</param>
            /// <param name="objectType">Type of the object.</param>
            /// <param name="existingValue">The existing value.</param>
            /// <param name="serializer">The serializer.</param>
            /// <returns>A collection of <see cref="TwitterTrend"/> items.</returns>
            public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
            {
                TwitterTrendCollection result = existingValue as TwitterTrendCollection;
                
                if (result == null)
                    result = new TwitterTrendCollection();

                int initialDepth = reader.Depth;

                while (reader.Read() && reader.Depth >= initialDepth)
                {
                    if (reader.TokenType == JsonToken.StartObject && reader.Depth > 2)
                        result.Add(new TwitterTrend());

                    if (reader.TokenType == JsonToken.PropertyName)
                    {
                        switch ((string)reader.Value)
                        {
                            case "query":
                                reader.Read();
                                result[result.Count - 1].SearchQuery = (string)reader.Value;
                                continue;
                            case "name":
                                reader.Read();
                                result[result.Count - 1].Name = (string)reader.Value;
                                continue;
                            case "url":
                                reader.Read();
                                result[result.Count - 1].Address = (string)reader.Value;
                                continue;
                        }
                    }
                }

                return result;
            }

            /// <summary>
            /// Writes the json.
            /// </summary>
            /// <param name="writer">The writer.</param>
            /// <param name="value">The value.</param>
            /// <param name="serializer">The serializer.</param>
            public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
            {
                throw new System.NotImplementedException();
            }
        }
    }
}
