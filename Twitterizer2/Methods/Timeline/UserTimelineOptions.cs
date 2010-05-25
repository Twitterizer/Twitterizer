namespace Twitterizer
{
    /// <summary>
    /// The UserTimelineOptions class. Provides a payload for optional parameters of the <see cref="Twitterizer.Commands.UserTimelineCommand"/> class.
    /// </summary>
    public class UserTimelineOptions : TimelineOptions
    {
        /// <summary>
        /// Gets or sets the ID of the user for whom to request a list of followers.
        /// </summary>
        /// <value>The user id.</value>
        public decimal UserId { get; set; }

        /// <summary>
        /// Gets or sets the screen name of the user for whom to request a list of followers. 
        /// </summary>
        /// <value>The name of the screen.</value>
        public string ScreenName { get; set; }
    }
}
