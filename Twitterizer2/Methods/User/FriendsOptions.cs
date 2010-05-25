namespace Twitterizer
{
    /// <summary>
    /// The friends options class. Provides a payload for optional parameters of the <see cref="Twitterizer.Commands.FriendsCommand"/> class.
    /// </summary>
    [System.Serializable]
    public class FriendsOptions : OptionalProperties
    {
        /// <summary>
        /// Gets or sets the user id.
        /// </summary>
        /// <value>The user id.</value>
        public decimal UserId { get; set; }

        /// <summary>
        /// Gets or sets the user's screen name.
        /// </summary>
        /// <value>The screen name of the user.</value>
        public string ScreenName { get; set; }

        /// <summary>
        /// Gets or sets the cursor.
        /// </summary>
        /// <value>The cursor.</value>
        public long Cursor { get; set; }
    }
}
