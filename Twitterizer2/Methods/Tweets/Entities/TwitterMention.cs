using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Twitterizer.Entities
{
    /// <summary>
    /// Represents a 
    /// </summary>
    public class TwitterMentionEntity : TwitterEntity
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TwitterMention"/> class.
        /// </summary>
        internal TwitterMentionEntity()
        {
        }

        /// <summary>
        /// Gets or sets the name of the screen.
        /// </summary>
        /// <value>The name of the screen.</value>
        public string ScreenName { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the user id.
        /// </summary>
        /// <value>The user id.</value>
        public decimal UserId { get; set; }
    }
}
