namespace Twitterizer
{
    using System;
    using System.Collections.ObjectModel;
    using System.Linq;
    using Newtonsoft.Json;

    /// <summary>
    /// Represents a single point on planet earth.
    /// </summary>
    public class Coordinate
    {
        /// <summary>
        /// Gets or sets the latitude.
        /// </summary>
        /// <value>The latitude.</value>
        public double Latitude { get; set; }

        /// <summary>
        /// Gets or sets the longitude.
        /// </summary>
        /// <value>The longitude.</value>
        public double Longitude { get; set; }

        /// <summary>
        /// Reads a json array of coordinates and converts it into a collection of coordinate objects.
        /// </summary>
        internal class Converter : JsonConverter
        {
            /// <summary>
            /// Determines whether this instance can convert the specified object type.
            /// </summary>
            /// <param name="objectType">Type of the object.</param>
            /// <returns>
            /// 	<c>true</c> if this instance can convert the specified object type; otherwise, <c>false</c>.
            /// </returns>
            public override bool CanConvert(Type objectType)
            {
                return objectType == typeof(Collection<Coordinate>);
            }

            /// <summary>
            /// Reads the json.
            /// </summary>
            /// <param name="reader">The reader.</param>
            /// <param name="objectType">Type of the object.</param>
            /// <param name="existingValue">The existing value.</param>
            /// <param name="serializer">The serializer.</param>
            /// <returns></returns>
            public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
            {
                Collection<Coordinate> result = existingValue as Collection<Coordinate>;

                if (result == null)
                    result = new Collection<Coordinate>();

                int startDepth = reader.Depth;

                if (reader.TokenType != JsonToken.StartArray)
                {
                    return null;
                }

                int depth = reader.Depth + 1;
                double count = 1;

                while (reader.Read() && reader.Depth >= startDepth)
                {
                    if (new[] { JsonToken.StartArray, JsonToken.EndArray }.Contains(reader.TokenType))
                        continue;

                    int itemIndex = Convert.ToInt32(Math.Ceiling(count / 2) - 1);

                    if (count % 2 > 0)
                    {
                        result.Add(new Coordinate());
                        result[itemIndex].Latitude = (double)reader.Value;
                    }
                    else
                    {
                        result[itemIndex].Longitude = (double)reader.Value;
                    }

                    count++;
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
                throw new NotImplementedException();
            }
        }
    }
}
