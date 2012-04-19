using System;
using System.Net.Http;
using Twitterizer.Core;
using Twitterizer.Models;

namespace Twitterizer.Commands
{
    internal class CreateListSubscriberCommand : TwitterCommand<TwitterList>
    {
        private readonly decimal listId;

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateListMembershipCommand"/> class.
        /// </summary>
        /// <param name="tokens">The tokens.</param>
        /// <param name="listId">The list id.</param>
        /// <param name="options">The options.</param>
        public CreateListSubscriberCommand(OAuthTokens tokens, decimal listId, OptionalProperties options)
            : base(HttpMethod.Post, "/lists/subscribers/create.json", tokens, options)
        {
            if (tokens == null || !tokens.HasBothTokens)
            {
                throw new ArgumentNullException("tokens");
            }

            if (listId <= 0)
            {
                throw new ArgumentNullException("listId");
            }

            this.listId = listId;
        }

        /// <summary>
        /// Inits this instance.
        /// </summary>
        public override void Init()
        {
            this.RequestParameters.Add("list_id", this.listId.ToString());
        }
    }
}
