namespace Twitterizer
{
    using System.Runtime.Serialization;
    using Twitterizer.Core;

    [DataContract]
    internal class TwitterSearchResultWrapper : BaseObject
    {
        /// <summary>
        /// Gets or sets the results.
        /// </summary>
        /// <value>The results.</value>
        [DataMember(Name = "results")]
        public TwitterSearchResultCollection Results { get; set; }

        /// <summary>
        /// Gets or sets the max id.
        /// </summary>
        /// <value>The max id.</value>
        [DataMember(Name = "max_id")]
        public long MaxId { get; set; }

        /// <summary>
        /// Gets or sets the since id.
        /// </summary>
        /// <value>The since id.</value>
        [DataMember(Name = "since_id")]
        public long SinceId { get; set; }

        /// <summary>
        /// Gets or sets the refresh query string.
        /// </summary>
        /// <value>The refresh query string.</value>
        [DataMember(Name = "refresh_url")]
        public string RefreshQueryString { get; set; }

        /// <summary>
        /// Gets or sets the next page query string.
        /// </summary>
        /// <value>The next page query string.</value>
        [DataMember(Name = "next_page")]
        public string NextPageQueryString { get; set; }

        /// <summary>
        /// Gets or sets the results per page.
        /// </summary>
        /// <value>The results per page.</value>
        [DataMember(Name = "results_per_page")]
        public int ResultsPerPage { get; set; }

        /// <summary>
        /// Gets or sets the page number.
        /// </summary>
        /// <value>The page number.</value>
        [DataMember(Name = "page")]
        public int PageNumber { get; set; }

        /// <summary>
        /// Gets or sets the completed in.
        /// </summary>
        /// <value>The completed in.</value>
        [DataMember(Name = "completed_in")]
        public double CompletedIn { get; set; }

        /// <summary>
        /// Gets or sets the query.
        /// </summary>
        /// <value>The query.</value>
        [DataMember(Name = "query")]
        public string Query { get; set; }
    }
}
