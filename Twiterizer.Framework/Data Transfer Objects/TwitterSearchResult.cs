using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Twitterizer.Framework.Data_Transfer_Objects
{
    [JsonObject(MemberSerialization.OptOut)]
    public class TwitterSearchResult
    {
        [JsonProperty("text")]
        public string Text { get; set; }

        [JsonProperty("to_user_id")]
        public int ToUserId { get; set; }

        [JsonProperty("to_user")]
        public string ToUserScreenName { get; set; }
        [JsonProperty("from_user")]
        public string FromUserScreenName { get; set; }

        [JsonProperty("from_user_id")]
        public int FromUserId { get; set; }

        [JsonProperty("iso_language_code")]
        public string IsoLanguageCode { get; set; }

        [JsonProperty("source")]
        public string Source { get; set; }

        [JsonProperty("profile_image_url")]
        public string ProfileImageUri { get; set; }

        [JsonProperty("created_at")]
        public string CreatedAt { get; set; }

        [JsonProperty("id")]
        public int Id { get; set; }


    }
}
