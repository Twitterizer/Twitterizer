using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Twitterizer
{
    public class TimelineOptions : Core.OptionalProperties
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TimelineOptions"/> class.
        /// </summary>
        public TimelineOptions()
            : base()
        {
            this.Page = 1;
        }

        /// <summary>
        /// Gets or sets the minimum (earliest) status id to request.
        /// </summary>
        /// <value>The since id.</value>
        public decimal SinceStatusId { get; set; }

        /// <summary>
        /// Gets or sets the max (latest) status id to request.
        /// </summary>
        /// <value>The max id.</value>
        public decimal MaxStatusId { get; set; }

        /// <summary>
        /// Gets or sets the number of messages to request.
        /// </summary>
        /// <value>The number of messages to request.</value>
        public int Count { get; set; }

        /// <summary>
        /// Gets or sets the page number to request.
        /// </summary>
        /// <value>The page number.</value>
        public int Page { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether user objects should contain only Id values.
        /// </summary>
        /// <value><c>true</c> if user objects should contain only Id values; otherwise, <c>false</c>.</value>
        public bool SkipUser { get; set; }
    }
}
