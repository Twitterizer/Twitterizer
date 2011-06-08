//-----------------------------------------------------------------------
// <copyright file="TwitterEntityCollection.cs" company="Patrick 'Ricky' Smith">
//  This file is part of the Twitterizer library (http://www.twitterizer.net)
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
// <summary>The twitter entity collection class</summary>
//-----------------------------------------------------------------------

namespace Twitterizer.Entities
{
    using System;
    using System.Collections.ObjectModel;
    using Newtonsoft.Json;
using System.Linq.Expressions;
    using System.Reflection;

    /// <summary>
    /// Represents multiple <see cref="Twitterizer.Entities.TwitterEntity"/> objects.
    /// </summary>
#if !SILVERLIGHT
    [Serializable]
#endif
    public class TwitterEntityCollection : Collection<TwitterEntity>
    {
        /// <summary>
        /// The Json converter for <see cref="TwitterEntityCollection"/> data.
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
                return objectType == typeof(TwitterEntityCollection);
            }

            /// <summary>
            /// Reads the JSON representation of the object.
            /// </summary>
            /// <param name="reader">The <see cref="T:Newtonsoft.Json.JsonReader"/> to read from.</param>
            /// <param name="objectType">Type of the object.</param>
            /// <param name="existingValue">The existing value of object being read.</param>
            /// <param name="serializer">The calling serializer.</param>
            /// <returns>The object value.</returns>
            public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
            {
                TwitterEntityCollection result = existingValue as TwitterEntityCollection;
                if (result == null)
                    result = new TwitterEntityCollection();

                int startDepth = reader.Depth;
                string entityType = string.Empty;
                TwitterEntity entity = null;

                while (reader.Read() && reader.Depth >= startDepth)
                {
                    if (reader.TokenType == JsonToken.PropertyName && reader.Depth == startDepth + 1)
                    {
                        entityType = (string)reader.Value;
                        continue;
                    }

                    switch (entityType)
                    {
                        case "urls":
                            if (reader.TokenType == JsonToken.StartObject)
                                entity = new TwitterUrlEntity();

                            ReadFieldValue(reader, "url", entity, () => ((TwitterUrlEntity)entity).Url);
                            ReadFieldValue(reader, "display_url", entity, () => ((TwitterUrlEntity)entity).DisplayUrl);
                            ReadFieldValue(reader, "expanded_url", entity, () => ((TwitterUrlEntity)entity).ExpandedUrl);

                            break;

                        case "user_mentions":
                            if (reader.TokenType == JsonToken.StartObject)
                                entity = new TwitterMentionEntity();

                            ReadFieldValue(reader, "screen_name", entity, () => ((TwitterMentionEntity)entity).ScreenName);
                            ReadFieldValue(reader, "name", entity, () => ((TwitterMentionEntity)entity).Name);
                            ReadFieldValue(reader, "id", entity, () => ((TwitterMentionEntity)entity).UserId);

                            break;

                        case "hashtags":
                           if (reader.TokenType == JsonToken.StartObject)
                                entity = new TwitterHashTagEntity();

                            ReadFieldValue(reader, "text", entity, () => ((TwitterHashTagEntity)entity).Text);

                            break;

                        case "media":
                            // Move to object start and parse the entity
                            reader.Read();
                            entity = parseMediaEntity(reader);

                            break;

                        default:
                            break;
                    }

                    // Read the indicies (for all entities)
                    if (reader.TokenType == JsonToken.PropertyName && (string)reader.Value == "indices")
                    {
                        reader.Read();
                        reader.Read();
                        entity.StartIndex = Convert.ToInt32((long)reader.Value);
                        reader.Read();
                        entity.EndIndex = Convert.ToInt32((long)reader.Value);
                    }

                    if (reader.TokenType == JsonToken.EndObject && entity != null)
                    {
                        result.Add(entity);
                        entity = null;
                    }
                }

                return result;
            }

            public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
            {
                throw new NotImplementedException();
            }

            public TwitterMediaEntity parseMediaEntity(JsonReader reader)
            {
                if (reader.TokenType != JsonToken.StartObject)
                    return null;

                TwitterMediaEntity entity = new TwitterMediaEntity();

                int startDepth = reader.Depth;

                // Start looping through all of the child nodes
                while (reader.Read() && reader.Depth >= startDepth)
                {
                    // If the current node isn't a property, skip it
                    if (reader.TokenType != JsonToken.PropertyName)
                    {
                        continue;
                    }

                    string fieldName = reader.Value as string;
                    if (string.IsNullOrEmpty(fieldName))
                    {
                        continue;
                    }

                    switch (fieldName)
                    {
                        case "type":
                            entity.MediaType = string.IsNullOrEmpty((string)reader.Value) ?
                                TwitterMediaEntity.MediaTypes.Unknown :
                                TwitterMediaEntity.MediaTypes.Photo;
                            break;

                        case "sizes":
                            if (reader.TokenType != JsonToken.PropertyName)
                            {
                                break;
                            }

                            TwitterMediaEntity.MediaSize newSize = new TwitterMediaEntity.MediaSize();

                            switch ((string)reader.Value)
                            {
                                case "large":
                                    newSize.Size = TwitterMediaEntity.MediaSize.MediaSizes.Large;
                                    break;
                                case "medium":
                                    newSize.Size = TwitterMediaEntity.MediaSize.MediaSizes.Medium;
                                    break;
                                case "small":
                                    newSize.Size = TwitterMediaEntity.MediaSize.MediaSizes.Small;
                                    break;
                                case "thumb":
                                    newSize.Size = TwitterMediaEntity.MediaSize.MediaSizes.Thumb;
                                    break;
                                default:
                                    break;
                            }

                            int sizeDepth = reader.Depth;

                            // Loop through all of the properties of the size and read their values
                            while (reader.Read() && sizeDepth > reader.Depth)
                            {

                            }

                            break;

                        default:
                            break;
                    }

                    ReadFieldValue(reader, "id", entity, () => entity.Id);
                    ReadFieldValue(reader, "id_str", entity, () => entity.IdString);
                    ReadFieldValue(reader, "media_url", entity, () => entity.MediaUrl);
                    ReadFieldValue(reader, "media_url_https", entity, () => entity.MediaUrlSecure);
                    ReadFieldValue(reader, "url", entity, () => entity.Url);
                    ReadFieldValue(reader, "display_url", entity, () => entity.DisplayUrl);
                    ReadFieldValue(reader, "expanded_url", entity, () => entity.ExpandedUrl);
                }

                return entity;
            }

            private bool ReadFieldValue<T>(JsonReader reader, string fieldName, ref T result)
            {
                if (reader.TokenType != JsonToken.PropertyName)
                    return false;

                if ((string)reader.Value != fieldName)
                    return false;

                reader.Read();

                if (reader.ValueType == typeof(T))
                {
                    result = (T)reader.Value;
                }
                else
                {
                    result = (T)Convert.ChangeType(reader.Value, typeof(T));
                }

                return true;
            }

            private bool ReadFieldValue<TSource, TProperty>(JsonReader reader, string fieldName, TSource source, Expression<Func<TProperty>> property)
            {
                if (reader == null || source == null)
                {
                    return false;
                }

                var expr = (MemberExpression)property.Body;
                var prop = (PropertyInfo)expr.Member;

                TProperty value = (TProperty)prop.GetValue(source, null);
                if (ReadFieldValue<TProperty>(reader, fieldName, ref value))
                {
                    prop.SetValue(source, value, null);
                    return true;
                }

                return false;
            }
        }
    }
}
