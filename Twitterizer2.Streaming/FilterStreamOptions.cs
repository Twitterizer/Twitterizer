using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Twitterizer.Streaming
{
    public sealed class FilterStreamOptions
    {
        public FilterStreamOptions()
        {
            this.Track = new List<string>();
            this.Locations = new List<Location>();
            this.Follow = new List<string>();
        }

        /// <summary>
        /// Gets or sets the number of previous statuses to consider for delivery before transitioning to live stream delivery.
        /// </summary>
        /// <value>The count.</value>
        /// <remarks>On unfiltered streams, all considered statuses are delivered, so the number requested is the number returned. On filtered streams, the number requested is the number of statuses that are applied to the filter predicate, and not the number of statuses returned.</remarks>
        public int Count { get; set; }

        /// <summary>
        /// Gets or sets the user IDs that is to be referenced in the stream.
        /// </summary>
        /// <value>The follow.</value>
        public List<string> Follow { get; set; }

        /// <summary>
        /// Gets or sets the keywords to track.
        /// </summary>
        /// <value>The keywords to track.</value>
        public List<string> Track { get; set; }

        /// <summary>
        /// Gets or sets the locations.
        /// </summary>
        /// <value>The locations.</value>
        public List<Location> Locations { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [use SSL].
        /// </summary>
        /// <value><c>true</c> if [use SSL]; otherwise, <c>false</c>.</value>
        public bool UseSSL { get; set; }
    }
}
