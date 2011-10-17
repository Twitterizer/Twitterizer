using Twitterizer2.Streaming;
namespace Twitterizer.Streaming
{
    using System;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;
#if !SILVERLIGHT
    [System.Serializable]
#endif
    public class TwitterStreamEvent
    {
        /// <summary>
        /// Gets or sets the type of the event.
        /// </summary>
        /// <value>The type of the event.</value>
        [JsonProperty(PropertyName = "event")]
        [JsonConverter(typeof(StringEnumConverter))]
        public TwitterSteamEvent EventType { get; set; }

        /// <summary>
        /// Gets or sets the source of the event. This is always the user who initiated the event.
        /// </summary>
        /// <value>The source.</value>
        public TwitterUser Source { get; set; }

        /// <summary>
        /// Gets or sets the target of the event. This is the user who was affected, or who owns the affected object.
        /// </summary>
        /// <value>The source.</value>
        public TwitterUser Target { get; set; }

        /// <summary>
        /// Gets or sets the target object.
        /// </summary>
        /// <value>The target object.</value>
        public Twitterizer.Core.TwitterObject TargetObject { get; set; }

        /// <summary>
        /// Gets or sets the creation date.
        /// </summary>
        /// <value>The creation date.</value>
        [JsonProperty(PropertyName = "created_at")]
        [JsonConverter(typeof(TwitterizerDateConverter))]
        public DateTime CreatedAt { get; set; }
    }
}
