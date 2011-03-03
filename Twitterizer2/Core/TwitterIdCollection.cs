namespace Twitterizer
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using Twitterizer.Core;

    /// <summary>
    /// Holds a collection of ID values
    /// </summary>
    public class TwitterIdCollection : Collection<decimal>, ITwitterObject
    {
        /// <summary>
        /// Annotations are additional pieces of data, supplied by Twitter clients, in a non-structured dictionary.
        /// </summary>
        /// <value>The annotations.</value>
        public Dictionary<string, string> Annotations { get; set; }
    }
}
