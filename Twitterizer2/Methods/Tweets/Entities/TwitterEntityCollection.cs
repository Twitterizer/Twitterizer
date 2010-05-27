namespace Twitterizer.Entities
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Collections.ObjectModel;
using Newtonsoft.Json;

    public class TwitterEntityCollection : Collection<TwitterEntity>
    {
        public class Converter : JsonConverter
        {
            public override bool CanConvert(Type objectType)
            {
                return objectType == typeof(TwitterEntityCollection);
            }

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

                            if (reader.TokenType == JsonToken.PropertyName)
                            {
                                if ((string)reader.Value == "url")
                                {
                                    reader.Read();
                                    ((TwitterUrlEntity)entity).Url = (string)reader.Value;
                                }
                            }

                            break;
                        case "user_mentions":
                            if (reader.TokenType == JsonToken.StartObject)
                            entity = new TwitterMentionEntity();

                            if (reader.TokenType == JsonToken.PropertyName)
                            {
                                if ((string)reader.Value == "screen_name")
                                {
                                    reader.Read();
                                    ((TwitterMentionEntity)entity).ScreenName = (string)reader.Value;
                                }

                                if ((string)reader.Value == "name")
                                {
                                    reader.Read();
                                    ((TwitterMentionEntity)entity).Name = (string)reader.Value;
                                }

                                if ((string)reader.Value == "id")
                                {
                                    reader.Read();
                                    ((TwitterMentionEntity)entity).UserId = Convert.ToDecimal(reader.Value);
                                }
                            }

                            break;
                        case "hashtags":
                            if (reader.TokenType == JsonToken.StartObject)
                            entity = new TwitterHashTagEntity();

                            if (reader.TokenType == JsonToken.PropertyName)
                            {
                                if ((string)reader.Value == "text")
                                {
                                    reader.Read();
                                    ((TwitterHashTagEntity)entity).Text = (string)reader.Value;
                                }
                            }

                            break;
                        default:
                            break;
                    }
                    
                    // Read the indicies (for all entities)
                    if (reader.TokenType == JsonToken.PropertyName && (string)reader.Value == "indices")
                    {
                        reader.Read();
                        reader.Read();
                        entity.StartIndex = (long)reader.Value;
                        reader.Read();
                        entity.EndIndex = (long)reader.Value;
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
        }
    }
}
