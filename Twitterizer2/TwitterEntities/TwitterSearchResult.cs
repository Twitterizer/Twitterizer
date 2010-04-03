namespace Twitterizer
{
    using System;
    using System.Globalization;
    using System.Runtime.Serialization;

    [DataContract, Serializable]
    public class TwitterSearchResult : Core.BaseObject
    {
        /// <summary>
        /// Gets or sets the profile image URL.
        /// </summary>
        /// <value>The profile image URL.</value>
        [DataMember(Name = "profile_image_url")]
        public string ProfileImageLocation { get; set; }

        /// <summary>
        /// Gets or sets the created date string.
        /// </summary>
        /// <value>The created date string.</value>
        [DataMember(Name = "created_at")]
        public string CreatedDateString { get; set; }

        /// <summary>
        /// Gets the created date.
        /// </summary>
        /// <value>The created date.</value>
        [IgnoreDataMember]
        public DateTime CreatedDate
        {
            get
            {
                DateTime parsedDate;

                if (DateTime.TryParse(this.CreatedDateString, out parsedDate))
                {
                    return parsedDate;
                }
                else
                {
                    return new DateTime();
                }
            }
        }

        /// <summary>
        /// Gets or sets the name of from user screen.
        /// </summary>
        /// <value>The name of from user screen.</value>
        [DataMember(Name = "from_user")]
        public string FromUserScreenName { get; set; }

        /// <summary>
        /// Gets or sets from user id.
        /// </summary>
        /// <value>From user id.</value>
        [DataMember(Name = "from_user_id")]
        public long? FromUserId { get; set; }

        /// <summary>
        /// Gets or sets the name of to user screen.
        /// </summary>
        /// <value>The name of to user screen.</value>
        [DataMember(Name = "to_user", IsRequired = false)]
        public string ToUserScreenName { get; set; }

        /// <summary>
        /// Gets or sets to user id.
        /// </summary>
        /// <value>To user id.</value>
        [DataMember(Name = "to_user_id")]
        public long? ToUserId { get; set; }

        /// <summary>
        /// Gets or sets the text.
        /// </summary>
        /// <value>The text.</value>
        [DataMember(Name = "text")]
        public string Text { get; set; }

        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        /// <value>The id.</value>
        [DataMember(Name = "id")]
        public long Id { get; set; }

        /// <summary>
        /// Gets or sets the source.
        /// </summary>
        /// <value>The source.</value>
        [DataMember(Name = "source")]
        public string Source { get; set; }
    }
}
