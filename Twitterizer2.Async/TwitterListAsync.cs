using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Twitterizer;

namespace Twitterizer
{
    public static class TwitterListAsync
    {
        /// <summary>
        /// Add a member to a list. The authenticated user must own the list to be able to add members to it. Lists are limited to having 500 members.
        /// </summary>
        /// <param name="tokens">The tokens.</param>
        /// <param name="ownerUsername">The owner username.</param>
        /// <param name="listId">The list id.</param>
        /// <param name="userIdToAdd">The user id to add.</param>
        /// <param name="options">The options.</param>
        /// <param name="timeout">The timeout.</param>
        /// <param name="function">The function.</param>
        /// <returns></returns>
        public static IAsyncResult AddMember(OAuthTokens tokens, string ownerUsername, string listId, decimal userIdToAdd, OptionalProperties options, TimeSpan timeout, Action<TwitterResponse<TwitterList>> function)
        {
            Func<OAuthTokens, string, string, decimal, OptionalProperties, TwitterResponse<TwitterList>> methodToCall = TwitterList.AddMember;

            return methodToCall.BeginInvoke(
                tokens,
                ownerUsername,
                listId,
                userIdToAdd,
                options,
                result =>
                {
                    result.AsyncWaitHandle.WaitOne(timeout);
                    function(methodToCall.EndInvoke(result));
                },
                null);
        }

        /// <summary>
        /// Check if a user is a member of the specified list.
        /// </summary>
        /// <param name="tokens">The tokens.</param>
        /// <param name="ownerUsername">The owner username.</param>
        /// <param name="listId">The list id.</param>
        /// <param name="userId">The user id.</param>
        /// <param name="options">The options.</param>
        /// <param name="timeout">The timeout.</param>
        /// <param name="function">The function.</param>
        /// <returns></returns>
        public static IAsyncResult CheckMembership(OAuthTokens tokens, string ownerUsername, string listId, decimal userId, OptionalProperties options, TimeSpan timeout, Action<TwitterResponse<TwitterUser>> function)
        {
            Func<OAuthTokens, string, string, decimal, OptionalProperties, TwitterResponse<TwitterUser>> methodToCall = TwitterList.CheckMembership;

            return methodToCall.BeginInvoke(
                tokens,
                ownerUsername,
                listId,
                userId,
                options,
                result =>
                {
                    result.AsyncWaitHandle.WaitOne(timeout);
                    function(methodToCall.EndInvoke(result));
                },
                null);
        }

        /// <summary>
        /// Deletes the specified list. Must be owned by the authenticated user.
        /// </summary>
        /// <param name="tokens">The tokens.</param>
        /// <param name="username">The username.</param>
        /// <param name="listIdOrSlug">The list id or slug.</param>
        /// <param name="options">The options.</param>
        /// <param name="timeout">The timeout.</param>
        /// <param name="function">The function.</param>
        /// <returns></returns>
        public static IAsyncResult Delete(OAuthTokens tokens, string username, string listIdOrSlug, OptionalProperties options, TimeSpan timeout, Action<TwitterResponse<TwitterList>> function)
        {
            Func<OAuthTokens, string, string, OptionalProperties, TwitterResponse<TwitterList>> methodToCall = TwitterList.Delete;

            return methodToCall.BeginInvoke(
                tokens,
                username,
                listIdOrSlug,
                options,
                result =>
                {
                    result.AsyncWaitHandle.WaitOne(timeout);
                    function(methodToCall.EndInvoke(result));
                },
                null);
        }

        /// <summary>
        /// Show the specified list. Private lists will only be shown if the authenticated user owns the specified list.
        /// </summary>
        /// <param name="tokens">The tokens.</param>
        /// <param name="username">The username.</param>
        /// <param name="listIdOrSlug">The list id or slug.</param>
        /// <param name="options">The options.</param>
        /// <param name="timeout">The timeout.</param>
        /// <param name="function">The function.</param>
        /// <returns></returns>
        public static IAsyncResult GetList(OAuthTokens tokens, string username, string listIdOrSlug, OptionalProperties options, TimeSpan timeout, Action<TwitterResponse<TwitterList>> function)
        {
            Func<OAuthTokens, string, string, OptionalProperties, TwitterResponse<TwitterList>> methodToCall = TwitterList.GetList;

            return methodToCall.BeginInvoke(
                tokens,
                username,
                listIdOrSlug,
                options,
                result =>
                {
                    result.AsyncWaitHandle.WaitOne(timeout);
                    function(methodToCall.EndInvoke(result));
                },
                null);
        }

        /// <summary>
        /// List the lists of the specified user. Private lists will be included if the authenticated users is the same as the user who's lists are being returned.
        /// </summary>
        /// <param name="tokens">The tokens.</param>
        /// <param name="username">The username.</param>
        /// <param name="options">The options.</param>
        /// <param name="timeout">The timeout.</param>
        /// <param name="function">The function.</param>
        /// <returns></returns>
        public static IAsyncResult GetLists(OAuthTokens tokens, string username, OptionalProperties options, TimeSpan timeout, Action<TwitterResponse<TwitterListCollection>> function)
        {
            Func<OAuthTokens, string, OptionalProperties, TwitterResponse<TwitterListCollection>> methodToCall = TwitterList.GetLists;

            return methodToCall.BeginInvoke(
                tokens,
                username,
                options,
                result =>
                {
                    result.AsyncWaitHandle.WaitOne(timeout);
                    function(methodToCall.EndInvoke(result));
                },
                null);
        }

        /// <summary>
        /// Returns the members of the specified list.
        /// </summary>
        /// <param name="tokens">The tokens.</param>
        /// <param name="ownerUsername">The owner username.</param>
        /// <param name="listId">The list id.</param>
        /// <param name="options">The options.</param>
        /// <param name="timeout">The timeout.</param>
        /// <param name="function">The function.</param>
        /// <returns></returns>
        public static IAsyncResult GetMembers(
            OAuthTokens tokens, 
            string ownerUsername, 
            string listId,
            GetListMembersOptions options, 
            TimeSpan timeout, 
            Action<TwitterResponse<TwitterUserCollection>> function)
        {
            Func<OAuthTokens, string, string, GetListMembersOptions, TwitterResponse<TwitterUserCollection>> methodToCall = TwitterList.GetMembers;

            return methodToCall.BeginInvoke(
                tokens,
                ownerUsername,
                listId,
                options,
                result =>
                {
                    result.AsyncWaitHandle.WaitOne(timeout);
                    function(methodToCall.EndInvoke(result));
                },
                null);
        }

        /// <summary>
        /// List the lists the specified user has been added to.
        /// </summary>
        /// <param name="tokens">The tokens.</param>
        /// <param name="username">The username.</param>
        /// <param name="options">The options.</param>
        /// <param name="timeout">The timeout.</param>
        /// <param name="function">The function.</param>
        /// <returns></returns>
        public static IAsyncResult GetMemberships(OAuthTokens tokens, string username, OptionalProperties options, TimeSpan timeout, Action<TwitterResponse<TwitterListCollection>> function)
        {
            Func<OAuthTokens, string, OptionalProperties, TwitterResponse<TwitterListCollection>> methodToCall = TwitterList.GetMemberships;

            return methodToCall.BeginInvoke(
                tokens,
                username,
                options,
                result =>
                {
                    result.AsyncWaitHandle.WaitOne(timeout);
                    function(methodToCall.EndInvoke(result));
                },
                null);
        }

        /// <summary>
        /// Show tweet timeline for members of the specified list.
        /// </summary>
        /// <param name="tokens">The tokens.</param>
        /// <param name="username">The username.</param>
        /// <param name="listIdOrSlug">The list id or slug.</param>
        /// <param name="options">The options.</param>
        /// <param name="timeout">The timeout.</param>
        /// <param name="function">The function.</param>
        /// <returns></returns>
        public static IAsyncResult GetStatuses(
            OAuthTokens tokens, 
            string username, 
            string listIdOrSlug, 
            ListStatusesOptions options, 
            TimeSpan timeout, 
            Action<TwitterResponse<TwitterStatusCollection>> function)
        {
            Func<OAuthTokens, string, string, ListStatusesOptions, TwitterResponse<TwitterStatusCollection>> methodToCall = TwitterList.GetStatuses;

            return methodToCall.BeginInvoke(
                tokens,
                username,
                listIdOrSlug,
                options,
                result =>
                {
                    result.AsyncWaitHandle.WaitOne(timeout);
                    function(methodToCall.EndInvoke(result));
                },
                null);
        }

        /// <summary>
        /// List the lists the specified user follows.
        /// </summary>
        /// <param name="tokens">The tokens.</param>
        /// <param name="userName">Name of the user.</param>
        /// <param name="options">The options.</param>
        /// <param name="timeout">The timeout.</param>
        /// <param name="function">The function.</param>
        /// <returns></returns>
        public static IAsyncResult GetSubscriptions(
            OAuthTokens tokens, 
            string userName, 
            OptionalProperties options, 
            TimeSpan timeout, 
            Action<TwitterResponse<TwitterListCollection>> function)
        {
            Func<OAuthTokens, string, OptionalProperties, TwitterResponse<TwitterListCollection>> methodToCall = TwitterList.GetSubscriptions;

            return methodToCall.BeginInvoke(
                tokens,
                userName,
                options,
                result =>
                {
                    result.AsyncWaitHandle.WaitOne(timeout);
                    function(methodToCall.EndInvoke(result));
                },
                null);
        }

        /// <summary>
        /// Creates a new list for the authenticated user. Accounts are limited to 20 lists.
        /// </summary>
        /// <param name="tokens">The tokens.</param>
        /// <param name="username">The username.</param>
        /// <param name="name">The name.</param>
        /// <param name="isPublic">if set to <c>true</c> [is public].</param>
        /// <param name="description">The description.</param>
        /// <param name="options">The options.</param>
        /// <param name="timeout">The timeout.</param>
        /// <param name="function">The function.</param>
        /// <returns></returns>
        public static IAsyncResult New(
            OAuthTokens tokens, 
            string username, 
            string name, 
            bool isPublic, 
            string description, 
            OptionalProperties options, 
            TimeSpan timeout,
            Action<TwitterResponse<TwitterList>> function)
        {
            Func<OAuthTokens, string, string, bool, string, OptionalProperties, TwitterResponse<TwitterList>> methodToCall = TwitterList.New;

            return methodToCall.BeginInvoke(
                tokens,
                username,
                name,
                isPublic,
                description,
                options,
                result =>
                {
                    result.AsyncWaitHandle.WaitOne(timeout);
                    function(methodToCall.EndInvoke(result));
                },
                null);
        }

        /// <summary>
        /// Removes the specified member from the list. The authenticated user must be the list's owner to remove members from the list.
        /// </summary>
        /// <param name="tokens">The tokens.</param>
        /// <param name="ownerUsername">The owner username.</param>
        /// <param name="listId">The list id.</param>
        /// <param name="userIdToAdd">The user id to add.</param>
        /// <param name="options">The options.</param>
        /// <param name="timeout">The timeout.</param>
        /// <param name="function">The function.</param>
        /// <returns></returns>
        public static IAsyncResult RemoveMember(
            OAuthTokens tokens, 
            string ownerUsername, 
            string listId, 
            decimal userIdToAdd, 
            OptionalProperties options, 
            TimeSpan timeout,
            Action<TwitterResponse<TwitterList>> function)
        {
            Func<OAuthTokens, string, string, decimal, OptionalProperties, TwitterResponse<TwitterList>> methodToCall = TwitterList.RemoveMember;

            return methodToCall.BeginInvoke(
                tokens,
                ownerUsername,
                listId,
                userIdToAdd,
                options,
                result =>
                {
                    result.AsyncWaitHandle.WaitOne(timeout);
                    function(methodToCall.EndInvoke(result));
                },
                null);
        }

        /// <summary>
        /// Updates the specified list.
        /// </summary>
        /// <param name="tokens">The tokens.</param>
        /// <param name="username">The username.</param>
        /// <param name="listId">The list id.</param>
        /// <param name="options">The options.</param>
        /// <param name="timeout">The timeout.</param>
        /// <param name="function">The function.</param>
        /// <returns></returns>
        public static IAsyncResult Update(
            OAuthTokens tokens, 
            string username, 
            string listId, 
            UpdateListOptions options, 
            TimeSpan timeout,
            Action<TwitterResponse<TwitterList>> function)
        {
            Func<OAuthTokens, string, string, UpdateListOptions, TwitterResponse<TwitterList>> methodToCall = TwitterList.Update;

            return methodToCall.BeginInvoke(
                tokens,
                username,
                listId,
                options,
                result =>
                {
                    result.AsyncWaitHandle.WaitOne(timeout);
                    function(methodToCall.EndInvoke(result));
                },
                null);
        }
    }
}
