using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Twitterizer;

namespace Twitterizer2.Streaming
{
    public class TwitterStreamDeletedStatus
    {

        /// <summary>
        /// Gets or sets the user id of the event. This is always the user who initiated the event.
        /// </summary>
        /// <value>The User Id.</value>
        public decimal UserId { get; set; }

        /// <summary>
        /// Gets or sets the status id of the event. This is the status that was affected.
        /// </summary>
        /// <value>The Status ID.</value>
        public decimal StatusId { get; set; }
    }
}
