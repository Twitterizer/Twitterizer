namespace Twitterizer.TwitterEntities
{
    /// <summary>
    /// The Twitter Result Types
    /// </summary>
    public enum TwitterResultType
    {
        /// <summary>
        ///  In a future release this will become the default value. Include both popular and real time results in the response.
        /// </summary>
        Mixed,

        /// <summary>
        /// The current default value. Return only the most recent results in the response.
        /// </summary>
        Recent,

        /// <summary>
        /// Return only the most popular results in the response.
        /// </summary>
        Popular
    }
}
