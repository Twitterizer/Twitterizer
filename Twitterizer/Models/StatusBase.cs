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
    [JsonObject(MemberSerialization.OptIn)]
    public partial class StatusBase : TwitterObject
    {
        /// <summary>
        /// Gets or sets the geo location data.
        /// </summary>
        /// <value>The geo location data.</value>
        [DataMember, JsonProperty(PropertyName = "coordinates")]
        public Coordinates Coordinates { get; set; }

        /// <summary>
        /// Gets or sets the created date.
        /// </summary>
        /// <value>The created date.</value>
        [DataMember]
        [JsonProperty(PropertyName = "created_at")]
        [JsonConverter(typeof(TwitterizerDateConverter))]
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// Gets or sets the entities.
        /// </summary>
        /// <value>The entities.</value>
        [DataMember]
        [JsonProperty(PropertyName = "entities")]
        [JsonConverter(typeof(EntityCollection.Converter))]
        public EntityCollection Entities { get; set; }
        
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
        /// Returns the status text with HTML links to users, urls, and hashtags.
        /// </summary>
        /// <returns></returns>
        public string LinkifiedText()
        {
            return LinkifiedText(Entities, Text);
        }

        internal static string LinkifiedText(EntityCollection entities, string text)
        {
            if (entities == null || entities.Count == 0)
            {
                return text;
            }

            string linkedText = text;

            var entitiesSorted = entities.OrderBy(e => e.StartIndex).Reverse();

            foreach (Entity entity in entitiesSorted)
            {
                if (entity is HashTagEntity)
                {
                    HashTagEntity tagEntity = (HashTagEntity)entity;

                    linkedText = string.Format(
                        "{0}<a href=\"http://twitter.com/search?q=%23{1}\">{1}</a>{2}",
                        linkedText.Substring(0, entity.StartIndex),
                        tagEntity.Text,
                        linkedText.Substring(entity.EndIndex));
                }

                if (entity is UrlEntity)
                {
                    UrlEntity urlEntity = (UrlEntity)entity;

                    linkedText = string.Format(
                        "{0}<a href=\"{1}\">{1}</a>{2}",
                        linkedText.Substring(0, entity.StartIndex),
                        urlEntity.Url,
                        linkedText.Substring(entity.EndIndex));
                }

                if (entity is MentionEntity)
                {
                    MentionEntity mentionEntity = (MentionEntity)entity;

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
