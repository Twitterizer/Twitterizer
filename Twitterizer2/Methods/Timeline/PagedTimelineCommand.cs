using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Twitterizer.Core;
using System.Globalization;

namespace Twitterizer.Commands
{
    internal class PagedTimelineCommand<T> : PagedCommand<TwitterStatusCollection>
        where T : ITwitterObject
    {
        private NumberFormatInfo numberFormat = CultureInfo.InvariantCulture.NumberFormat;

        /// <summary>
        /// Initializes a new instance of the <see cref="PagedTimelineCommand&lt;T&gt;"/> class.
        /// </summary>
        /// <param name="httpMethod">The HTTP method.</param>
        /// <param name="endPoint">The end point.</param>
        /// <param name="tokens">The tokens.</param>
        /// <param name="optionalProperties">The optional properties.</param>
        public PagedTimelineCommand(HTTPVerb httpMethod, string endPoint, OAuthTokens tokens, OptionalProperties optionalProperties)
            : base(httpMethod, endPoint, tokens, optionalProperties)
        {
        }

        /// <summary>
        /// Initializes the command.
        /// </summary>
        public override void Init()
        {
            // Enable opt-in beta for entities
            this.RequestParameters.Add("include_entities", "true");

            if (this.Page <= 0)
                this.Page = 1;

            TimelineOptions options = this.OptionalProperties as TimelineOptions;

            if (options != null)
            {
                if (options.SinceStatusId > 0)
                    this.RequestParameters.Add("since_id", options.SinceStatusId.ToString(CultureInfo.InvariantCulture));

                if (options.MaxStatusId > 0)
                    this.RequestParameters.Add("max_id", options.MaxStatusId.ToString(CultureInfo.InvariantCulture));

                if (options.Count > 0)
                    this.RequestParameters.Add("count", options.Count.ToString(CultureInfo.InvariantCulture));

                if (this.Page <= 1 && options.Page > 1)
                    this.Page = options.Page;

                if (options.IncludeRetweets)
                    this.RequestParameters.Add("include_rts", "true");
            }

            this.RequestParameters.Add("page", this.Page.ToString(CultureInfo.InvariantCulture));
        }
    }
}
