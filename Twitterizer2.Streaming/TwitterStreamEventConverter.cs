using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Twitterizer2.Streaming
{
        using System;
        using System.Globalization;

        /// <summary>
        /// Converts date strings returned by the Twitter API into <see cref="System.DateTime"/>
        /// </summary>
        public class TwitterizerDateConverter : Newtonsoft.Json.Converters.DateTimeConverterBase
        {
            public TwitterizerDateConverter() { }
            /// <summary>
            /// The date pattern for most dates returned by the API
            /// </summary>
            protected const string DateFormat = "ddd MMM dd HH:mm:ss zz00 yyyy";

            /// <summary>
            /// Reads the json.
            /// </summary>
            /// <param name="reader">The reader.</param>
            /// <param name="objectType">Type of the object.</param>
            /// <param name="existingValue">The existing value.</param>
            /// <param name="serializer">The serializer.</param>
            /// <returns>The parsed value as a DateTime, or null.</returns>
            public override object ReadJson(Newtonsoft.Json.JsonReader reader, Type objectType, object existingValue, Newtonsoft.Json.JsonSerializer serializer)
            {
                if (reader.Value == null || reader.Value.GetType() != typeof(string))
                    return new DateTime();

                DateTime parsedDate;

                return DateTime.TryParseExact(
                    (string)reader.Value,
                    DateFormat,
                    CultureInfo.InvariantCulture,
                    DateTimeStyles.None,
                    out parsedDate) ? parsedDate : new DateTime();
            }

            /// <summary>
            /// Writes the json.
            /// </summary>
            /// <param name="writer">The writer.</param>
            /// <param name="value">The value.</param>
            /// <param name="serializer">The serializer.</param>
            public override void WriteJson(Newtonsoft.Json.JsonWriter writer, object value, Newtonsoft.Json.JsonSerializer serializer)
            {
                throw new NotImplementedException();
            }

            //#if SILVERLIGHT
            //        /// <summary>
            //        /// Determines whether this instance can convert the specified object type.
            //        /// </summary>
            //        /// <param name="objectType">Type of the object.</param>
            //        /// <returns>
            //        /// <c>true</c> if this instance can convert the specified object type; otherwise, <c>false</c>.
            //        /// </returns>
            //        public override bool CanConvert(Type objectType)
            //        {
            //            return objectType == typeof(DateTime);
            //        }
            //#endif
        }

}
