using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Twitterizer.Models
{
    public partial class TwitterStatusBase : TwitterObject
    {
        public TwitterStatusBase() { }

        /// <summary>
        /// Gets or sets the status id.
        /// </summary>
        /// <value>The status id.</value>
        [DataMember, JsonProperty(PropertyName = "id")]
        public decimal Id { get; set; }

        /// <summary>
        /// Gets or sets the string id.
        /// </summary>
        /// <value>The string id.</value>
        [DataMember, JsonProperty(PropertyName = "id_str")]
        public string StringId { get; set; }

        /// <summary>
        /// Gets or sets the created date.
        /// </summary>
        /// <value>The created date.</value>
        [DataMember]
        [JsonProperty(PropertyName = "created_at")]
        [JsonConverter(typeof(IsoDateTimeConverter))]
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// Gets or sets the source.
        /// </summary>
        /// <value>The source.</value>
        [DataMember, JsonProperty(PropertyName = "source")]
        public string Source { get; set; }

        /// <summary>
        /// Gets or sets the text of the status.
        /// </summary>
        /// <value>The status text.</value>
        [DataMember, JsonProperty(PropertyName = "text")]
        public string Text { get; set; }

        /// <summary>
        /// Gets or sets the entities.
        /// </summary>
        /// <value>The entities.</value>
        [DataMember]
        [JsonProperty(PropertyName = "entities")]
        [JsonConverter(typeof(TwitterEntityCollection.Converter))]
        public TwitterEntityCollection Entities { get; set; }

        /// <summary>
        /// Gets or sets the geo location data.
        /// </summary>
        /// <value>The geo location data.</value>
        [DataMember, JsonProperty(PropertyName = "geo")]
        public TwitterGeo Geo { get; set; }    

        /// <summary>
        /// Returns the status text with HTML links to users, urls, and hashtags.
        /// </summary>
        /// <returns></returns>
        public string LinkifiedText()
        {
            return LinkifiedText(Entities, Text);
        }

        internal static string LinkifiedText(TwitterEntityCollection entities, string text)
        {
            if (entities == null || entities.Count == 0)
            {
                return text;
            }

            string linkedText = text;

            var entitiesSorted = entities.OrderBy(e => e.StartIndex).Reverse();

            foreach (TwitterEntity entity in entitiesSorted)
            {
                if (entity is TwitterHashTagEntity)
                {
                    TwitterHashTagEntity tagEntity = (TwitterHashTagEntity)entity;

                    linkedText = string.Format(
                        "{0}<a href=\"http://twitter.com/search?q=%23{1}\">{1}</a>{2}",
                        linkedText.Substring(0, entity.StartIndex),
                        tagEntity.Text,
                        linkedText.Substring(entity.EndIndex));
                }

                if (entity is TwitterUrlEntity)
                {
                    TwitterUrlEntity urlEntity = (TwitterUrlEntity)entity;

                    linkedText = string.Format(
                        "{0}<a href=\"{1}\">{1}</a>{2}",
                        linkedText.Substring(0, entity.StartIndex),
                        urlEntity.Url,
                        linkedText.Substring(entity.EndIndex));
                }

                if (entity is TwitterMentionEntity)
                {
                    TwitterMentionEntity mentionEntity = (TwitterMentionEntity)entity;

                    linkedText = string.Format(
                        "{0}<a href=\"http://twitter.com/{1}\">@{1}</a>{2}",
                        linkedText.Substring(0, entity.StartIndex),
                        mentionEntity.ScreenName,
                        linkedText.Substring(entity.EndIndex));
                }
            }

            return linkedText;
        }        
    }
}
