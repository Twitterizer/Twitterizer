namespace Twitterizer.Commands
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Globalization;

    public class UpdateStatusCommand : Core.BaseCommand<TwitterStatus>
    {
        /// <summary>
        /// The base address to the API method.
        /// </summary>
        private const string Path = "http://api.twitter.com/1/statuses/update.json";

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="UserShowCommand"/> class.
        /// </summary>
        /// <param name="requestTokens">The request tokens.</param>
        public UpdateStatusCommand(OAuthTokens requestTokens)
            : base("POST", new Uri(Path), requestTokens)
        {
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the text.
        /// </summary>
        /// <value>The text.</value>
        public string Text { get; set; }

        public long InReplyToStatusId { get; set; }

        /// <summary>
        /// Gets or sets the latitude.
        /// </summary>
        /// <value>The latitude.</value>
        public string Latitude { get; set; }

        /// <summary>
        /// Gets or sets the longitude.
        /// </summary>
        /// <value>The longitude.</value>
        public string Longitude { get; set; }
        #endregion

        /// <summary>
        /// Initializes the command.
        /// </summary>
        public override void Init()
        {
            this.RequestParameters.Add("status", OAuthUtility.UrlEncode(this.Text));

            if (this.InReplyToStatusId > 0)
                this.RequestParameters.Add("in_reply_to_status_id", this.InReplyToStatusId.ToString(CultureInfo.CurrentCulture));

            if (!string.IsNullOrEmpty(this.Latitude))
                this.RequestParameters.Add("lat", this.Latitude);

            if (!string.IsNullOrEmpty(this.Longitude))
                this.RequestParameters.Add("long", this.Longitude);
        }

        /// <summary>
        /// Validates this instance.
        /// </summary>
        public override void Validate()
        {
            //TODO: Ricky - Add Latitude and Longitude value validation
            this.IsValid = !string.IsNullOrEmpty(this.Text);
        }
    }
}
