using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twitterizer.Models;

namespace Twitterizer
{
    /// <summary>
    /// Lists are collections of tweets, culled from a curated list of Twitter users. List timeline methods include tweets by all members of a list.
    /// NOT YET IMPLEMENTED: GET/all, GET/subscribers, POST/subscribers/show, POST/members/create_all, GET/members/show, POST/members/destroy_all
    /// </summary>
    public static class Lists
    {
        /// <summary>
        /// Show tweet timeline for members of the specified list.
        /// </summary>
        /// <param name="tokens">The tokens.</param>
        /// <param name="username">The username.</param>
        /// <param name="listIdOrSlug">The list id or slug.</param>
        /// <param name="options">The options.</param>
        /// <returns>
        /// A <see cref="TwitterStatusCollection"/> instance.
        /// </returns>
        public static async Task<TwitterResponse<TwitterStatusCollection>> StatusesAsync(string listIdOrSlug, string username, OAuthTokens tokens, ListStatusesOptions options)
        {
            return await Core.CommandPerformer.PerformAction(new Twitterizer.Commands.ListStatusesCommand(tokens, username, listIdOrSlug, options));
        }

        /// <summary>
        /// Removes the specified member from the list. The authenticated user must be the list's owner to remove members from the list.
        /// </summary>
        /// <param name="tokens">The tokens.</param>
        /// <param name="ownerUsername">The username of the list owner.</param>
        /// <param name="listId">The list id.</param>
        /// <param name="userIdToRemove">The user id to add.</param>
        /// <param name="options">The options. Leave null for defaults.</param>
        /// <returns>
        /// A <see cref="TwitterList"/> representing the list the user was added to, or <c>null</c>.
        /// </returns>
        public static async Task<TwitterResponse<TwitterList>> RemoveMemberAsync(string listId, string ownerUsername, decimal userIdToAdd, OAuthTokens tokens, OptionalProperties options = null)
        {
            return await Core.CommandPerformer.PerformAction(new Twitterizer.Commands.RemoveListMemberCommand(tokens, ownerUsername, listId, userIdToAdd, options));
        }        

        /// <summary>
        /// List the lists the specified user has been added to.
        /// </summary>
        /// <param name="tokens">The tokens.</param>
        /// <param name="username">The screenname.</param>
        /// <param name="options">The options. Leave null for defaults.</param>
        /// <returns>
        /// A <see cref="TwitterListCollection"/> instance.
        /// </returns>
        public static async Task<TwitterResponse<TwitterListCollection>> MembershipsAsync(string screenname, OAuthTokens tokens, ListMembershipsOptions options = null)
        {
            return await Core.CommandPerformer.PerformAction(new Twitterizer.Commands.ListMembershipsCommand(tokens, screenname, options));
        }

        /// <summary>
        /// List the lists the specified user has been added to.
        /// </summary>
        /// <param name="tokens">The tokens.</param>
        /// <param name="username">The userid.</param>
        /// <param name="options">The options. Leave null for defaults.</param>
        /// <returns>
        /// A <see cref="TwitterListCollection"/> instance.
        /// </returns>
        public static async Task<TwitterResponse<TwitterListCollection>> MembershipsAsync(decimal userid, OAuthTokens tokens, ListMembershipsOptions options = null)
        {
            return await Core.CommandPerformer.PerformAction(new Twitterizer.Commands.ListMembershipsCommand(tokens, userid, options));
        }

        /// <summary>
        /// Subscribes the specified tokens.
        /// </summary>
        /// <param name="tokens">The tokens.</param>
        /// <param name="listId">The list id.</param>
        /// <param name="optionalProperties">The optional properties. Leave null for defaults.</param>
        /// <returns></returns>
        public static async Task<TwitterResponse<TwitterList>> SubscribeAsync(decimal listId, OAuthTokens tokens, OptionalProperties optionalProperties = null)
        {
            return await Core.CommandPerformer.PerformAction(new Commands.CreateListSubscriberCommand(tokens, listId, optionalProperties));
        }

        /// <summary>
        /// Unsubscribes the authenticated user from the specified list.
        /// </summary>
        /// <param name="tokens">The tokens.</param>
        /// <param name="listId">The list id.</param>
        /// <param name="optionalProperties">The optional properties.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static async Task<TwitterResponse<TwitterList>> UnSubscribeAsync(decimal listId, OAuthTokens tokens, OptionalProperties optionalProperties = null)
        {
            return await Core.CommandPerformer.PerformAction(new Commands.DestroyListSubscriber(tokens, listId, optionalProperties));
        }

        /// <summary>
        /// Returns the members of the specified list.
        /// </summary>
        /// <param name="tokens">The tokens.</param>
        /// <param name="username">The username.</param>
        /// <param name="listIdOrSlug">The list id or slug.</param>
        /// <param name="options">The options. Leave null for defaults.</param>
        /// <returns>
        /// A collection of users as <see cref="TwitterUserCollection"/>.
        /// </returns>
        public static async Task<TwitterResponse<TwitterUserCollection>> MembersAsync(OAuthTokens tokens, string username, string listIdOrSlug, GetListMembersOptions options = null)
        {
            return await Core.CommandPerformer.PerformAction(new Twitterizer.Commands.GetListMembersCommand(tokens, username, listIdOrSlug, options));
        }

        /// <summary>
        /// Add a member to a list. The authenticated user must own the list to be able to add members to it. Lists are limited to having 500 members.
        /// </summary>
        /// <param name="tokens">The tokens.</param>
        /// <param name="ownerUsername">The username of the list owner.</param>
        /// <param name="listId">The list id.</param>
        /// <param name="userIdToAdd">The user id to add.</param>
        /// <param name="options">The options. Leave null for defaults.</param>
        /// <returns>
        /// A <see cref="TwitterList"/> representing the list the user was added to, or <c>null</c>.
        /// </returns>
        public static async Task<TwitterResponse<TwitterList>> AddMemberAsync(OAuthTokens tokens, string ownerUsername, string listId, decimal userIdToAdd, OptionalProperties options = null)
        {
            return await Core.CommandPerformer.PerformAction(new Twitterizer.Commands.AddListMemberCommand(tokens, ownerUsername, listId, userIdToAdd, options));
        }               

        /// <summary>
        /// Deletes the specified list. Must be owned by the authenticated user.
        /// </summary>
        /// <param name="tokens">The tokens.</param>
        /// <param name="username">The username.</param>
        /// <param name="listIdOrSlug">The list id or slug.</param>
        /// <param name="options">The options. Leave null for defaults.</param>
        /// <returns>A <see cref="TwitterList"/> instance.</returns>
        public static async Task<TwitterResponse<TwitterList>> DeleteAsync(OAuthTokens tokens, string username, string listIdOrSlug, OptionalProperties options = null)
        {
            return await Core.CommandPerformer.PerformAction(new Twitterizer.Commands.DeleteListCommand(tokens, username, listIdOrSlug, options));
        }

        /// <summary>
        /// Updates the specified list.
        /// </summary>
        /// <param name="tokens">The oauth tokens.</param>
        /// <param name="listId">The list id.</param>
        /// <param name="options">The options.</param>
        /// <returns>A <see cref="TwitterList"/> instance.</returns>
        /// <remarks></remarks>
        public static async Task<TwitterResponse<TwitterList>> UpdateAsync(OAuthTokens tokens, string listId, UpdateListOptions options)
        {
            return await Core.CommandPerformer.PerformAction(new Twitterizer.Commands.UpdateListCommand(tokens, listId, options));
        }

        /// <summary>
        /// Creates a new list for the authenticated user. Accounts are limited to 20 lists.
        /// </summary>
        /// <param name="tokens">The oauth tokens.</param>
        /// <param name="name">The list name.</param>
        /// <param name="isPublic">if set to <c>true</c> creates a public list.</param>
        /// <param name="description">The description.</param>
        /// <param name="options">The options. Leave null for defaults.</param>
        /// <returns>A <see cref="TwitterList"/> instance.</returns>
        public static async Task<TwitterResponse<TwitterList>> NewAsync(OAuthTokens tokens, string name, bool isPublic, string description, OptionalProperties options = null)
        {
            return await Core.CommandPerformer.PerformAction(new Twitterizer.Commands.CreateListCommand(tokens, name, options)
            {
                IsPublic = isPublic,
                Description = description
            });
        }

        /// <summary>
        /// List the lists of the specified user. Private lists will be included if the authenticated users is the same as the user who's lists are being returned.
        /// </summary>
        /// <param name="tokens">The tokens.</param>
        /// <param name="options">The options. Leave null for defaults.</param>
        /// <returns>
        /// A <see cref="TwitterListCollection"/> instance.
        /// </returns>
        public static async Task<TwitterResponse<TwitterListCollection>> GetListsAsync(OAuthTokens tokens, GetListsOptions options = null)
        {
            return await Core.CommandPerformer.PerformAction(new Twitterizer.Commands.GetListsCommand(tokens, options));
        }

        /// <summary>
        /// Returns the specified list. Private lists will only be shown if the authenticated user owns the specified list.
        /// </summary>
        /// <param name="tokens">The tokens.</param>
        /// <param name="slug">The slug.</param>
        /// <param name="options">The options. Leave null for defaults.</param>
        /// <returns>A <see cref="TwitterList"/> instance.</returns>
        public static async Task<TwitterResponse<TwitterList>> ShowAsync(string slug, OAuthTokens tokens, OptionalProperties options = null)
        {
            return await Core.CommandPerformer.PerformAction(new Twitterizer.Commands.GetListCommand(tokens, slug, -1, options));
        }

        /// <summary>
        /// Returns the specified list. Private lists will only be shown if the authenticated user owns the specified list.
        /// </summary>
        /// <param name="tokens">The tokens.</param>
        /// <param name="listId">The list id.</param>
        /// <param name="options">The options. Leave null for defaults.</param>
        /// <returns>A <see cref="TwitterList"/> instance.</returns>
        public static async Task<TwitterResponse<TwitterList>> ShowAsync(decimal listId, OAuthTokens tokens, OptionalProperties options = null)
        {
            return await Core.CommandPerformer.PerformAction(new Twitterizer.Commands.GetListCommand(tokens, string.Empty, listId, options));
        }

        /// <summary>
        /// List the lists the specified user follows.
        /// </summary>
        /// <param name="tokens">The tokens.</param>
        /// <param name="userName">Name of the user.</param>
        /// <param name="options">The options. Leave null for defaults.</param>
        /// <returns>
        /// A <see cref="TwitterListCollection"/> instance.
        /// </returns>
        public static async Task<TwitterResponse<TwitterListCollection>> SubscriptionsAsync(string userName, OAuthTokens tokens, GetListSubscriptionsOptions options = null)
        {
            return await Core.CommandPerformer.PerformAction(new Twitterizer.Commands.GetListSubscriptionsCommand(tokens, userName, options));
        }            
    }
}
