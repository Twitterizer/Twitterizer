namespace Twitterizer
{
    public class RetweetedByOptions : OptionalProperties
    {
        /// <summary>
        /// Specifies the number of records to retrieve. Must be less than or equal to 100.
        /// </summary>
        /// <value>The count.</value>
        public int Count { get; set; }

        /// <summary>
        /// Specifies the page of results to retrieve.
        /// </summary>
        /// <value>The page.</value>
        public int Page { get; set; }

        /// <summary>
        /// When set to true each tweet returned in a timeline will include a user object including only the status authors numerical ID. Omit this parameter to receive the complete user object.
        /// </summary>
        /// <value><c>true</c> if [trim user]; otherwise, <c>false</c>.</value>
        public bool TrimUser { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether entities should be included in the results.
        /// </summary>
        /// <value><c>true</c> if entities should be included; otherwise, <c>false</c>.</value>
        public bool IncludeEntities { get; set; }
    }
}
