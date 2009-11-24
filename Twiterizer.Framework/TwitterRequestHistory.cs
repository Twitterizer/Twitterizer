namespace Twitterizer.Framework
{
    using System.Collections.Generic;

    public sealed class TwitterRequestHistory
    {
        static readonly TwitterRequestHistory instance = new TwitterRequestHistory();

        /// <summary>
        /// Initializes the <see cref="TwitterRequestHistory"/> class.
        /// </summary>
        static TwitterRequestHistory()
        {
            instance.Requests = new Queue<TwitterRequestData>(5);
        }

        /// <summary>
        /// Gets the history.
        /// </summary>
        /// <value>The history.</value>
        public static Queue<TwitterRequestData> History
        {
            get
            {
                return instance.Requests;
            }
        }

        /// <summary>
        /// Gets or sets the requests.
        /// </summary>
        /// <value>The requests.</value>
        public Queue<TwitterRequestData> Requests { get; set; }
    }
}
