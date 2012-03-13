namespace Twitterizer
{
    using System;

    /// <summary>
    /// An asynchronous wrapper around the <see cref="TwitterList"/> class.
    /// </summary>
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
        public static IAsyncResult AddMember(OAuthTokens tokens, string ownerUsername, string listId, decimal userIdToAdd, OptionalProperties options, TimeSpan timeout, Action<TwitterAsyncResponse<TwitterList>> function)
        {
            return AsyncUtility.ExecuteAsyncMethod(tokens, ownerUsername, listId, userIdToAdd, options, timeout, TwitterList.AddMember, function);
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
        public static IAsyncResult CheckMembership(OAuthTokens tokens, string ownerUsername, string listId, decimal userId, OptionalProperties options, TimeSpan timeout, Action<TwitterAsyncResponse<TwitterUser>> function)
        {
            return AsyncUtility.ExecuteAsyncMethod(tokens, ownerUsername, listId, userId, options, timeout, TwitterList.CheckMembership, function);
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
        public static IAsyncResult Delete(OAuthTokens tokens, string username, string listIdOrSlug, OptionalProperties options, TimeSpan timeout, Action<TwitterAsyncResponse<TwitterList>> function)
        {
            return AsyncUtility.ExecuteAsyncMethod(tokens, username, listIdOrSlug, options, timeout, TwitterList.Delete, function);
        }

        /// <summary>
        /// Show the specified list. Private lists will only be shown if the authenticated user owns the specified list.
        /// </summary>
        /// <param name="tokens">The tokens.</param>
        /// <param name="listIdOrSlug">The list id or slug.</param>
        /// <param name="options">The options.</param>
        /// <param name="timeout">The timeout.</param>
        /// <param name="function">The function.</param>
        /// <returns></returns>
        [Obsolete("TwitterListAsync.GetList() has been replaced with TwitterListAsync.Show()")]
        public static IAsyncResult GetList(OAuthTokens tokens, string listIdOrSlug, OptionalProperties options, TimeSpan timeout, Action<TwitterAsyncResponse<TwitterList>> function)
        {
            return Show(tokens, listIdOrSlug, options, timeout, function);
        }

        /// <summary>
        /// Show the specified list. Private lists will only be shown if the authenticated user owns the specified list.
        /// </summary>
        /// <param name="tokens">The tokens.</param>
        /// <param name="listIdOrSlug">The list id or slug.</param>
        /// <param name="options">The options.</param>
        /// <param name="timeout">The timeout.</param>
        /// <param name="function">The function.</param>
        /// <returns></returns>
        public static IAsyncResult Show(OAuthTokens tokens, string listIdOrSlug, OptionalProperties options, TimeSpan timeout, Action<TwitterAsyncResponse<TwitterList>> function)
        {
            return AsyncUtility.ExecuteAsyncMethod(tokens, listIdOrSlug, options, timeout, TwitterList.Show, function);
        }

        /// <summary>
        /// List the lists of the specified user. Private lists will be included if the authenticated users is the same as the user who's lists are being returned.
        /// </summary>
        /// <param name="tokens">The tokens.</param>
        /// <param name="options">The options.</param>
        /// <param name="timeout">The timeout.</param>
        /// <param name="function">The function.</param>
        /// <returns></returns>
        public static IAsyncResult GetLists(OAuthTokens tokens, GetListsOptions options, TimeSpan timeout, Action<TwitterAsyncResponse<TwitterListCollection>> function)
        {
            return AsyncUtility.ExecuteAsyncMethod(tokens, options, timeout, TwitterList.GetLists, function);
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
            Action<TwitterAsyncResponse<TwitterUserCollection>> function)
        {
            return AsyncUtility.ExecuteAsyncMethod(tokens, ownerUsername, listId, options, timeout, TwitterList.GetMembers, function);
        }

        /// <summary>
        /// List the lists the specified user has been added to.
        /// </summary>
        /// <param name="tokens">The tokens.</param>
        /// <param name="screenname">The screenname.</param>
        /// <param name="options">The options.</param>
        /// <param name="timeout">The timeout.</param>
        /// <param name="function">The function.</param>
        /// <returns></returns>
        public static IAsyncResult GetMemberships(OAuthTokens tokens, string screenname, ListMembershipsOptions options, TimeSpan timeout, Action<TwitterAsyncResponse<TwitterListCollection>> function)
        {
            return AsyncUtility.ExecuteAsyncMethod(tokens, screenname, options, timeout, TwitterList.GetMemberships, function);
        }

        /// <summary>
        /// List the lists the specified user has been added to.
        /// </summary>
        /// <param name="tokens">The tokens.</param>
        /// <param name="userid">The userid.</param>
        /// <param name="options">The options.</param>
        /// <param name="timeout">The timeout.</param>
        /// <param name="function">The function.</param>
        /// <returns></returns>
        public static IAsyncResult GetMemberships(OAuthTokens tokens, decimal userid, ListMembershipsOptions options, TimeSpan timeout, Action<TwitterAsyncResponse<TwitterListCollection>> function)
        {
            return AsyncUtility.ExecuteAsyncMethod(tokens, userid, options, timeout, TwitterList.GetMemberships, function);
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
            Action<TwitterAsyncResponse<TwitterStatusCollection>> function)
        {
            return AsyncUtility.ExecuteAsyncMethod(tokens, username, listIdOrSlug, options, timeout, TwitterList.GetStatuses, function);
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
            GetListSubscriptionsOptions options, 
            TimeSpan timeout, 
            Action<TwitterAsyncResponse<TwitterListCollection>> function)
        {
            return AsyncUtility.ExecuteAsyncMethod(tokens, userName, options, timeout, TwitterList.GetSubscriptions, function);
        }

        /// <summary>
        /// Creates a new list for the authenticated user. Accounts are limited to 20 lists.
        /// </summary>
        /// <param name="tokens">The tokens.</param>
        /// <param name="name">The name.</param>
        /// <param name="isPublic">if set to <c>true</c> [is public].</param>
        /// <param name="description">The description.</param>
        /// <param name="options">The options.</param>
        /// <param name="timeout">The timeout.</param>
        /// <param name="function">The function.</param>
        /// <returns></returns>
        public static IAsyncResult New(
            OAuthTokens tokens, 
            string name, 
            bool isPublic, 
            string description, 
            OptionalProperties options, 
            TimeSpan timeout,
            Action<TwitterAsyncResponse<TwitterList>> function)
        {
            return AsyncUtility.ExecuteAsyncMethod(tokens, name, isPublic, description, options, timeout, TwitterList.New, function); 
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
            Action<TwitterAsyncResponse<TwitterList>> function)
        {
            return AsyncUtility.ExecuteAsyncMethod(tokens, ownerUsername, listId, userIdToAdd, options, timeout, TwitterList.AddMember, function);
        }

        /// <summary>
        /// Updates the specified list.
        /// </summary>
        /// <param name="tokens">The tokens.</param>
        /// <param name="listId">The list id.</param>
        /// <param name="options">The options.</param>
        /// <param name="timeout">The timeout.</param>
        /// <param name="function">The function.</param>
        /// <returns></returns>
        public static IAsyncResult Update(
            OAuthTokens tokens, 
            string listId, 
            UpdateListOptions options, 
            TimeSpan timeout,
            Action<TwitterAsyncResponse<TwitterList>> function)
        {
            return AsyncUtility.ExecuteAsyncMethod(tokens, listId, options, timeout, TwitterList.Update, function);
        }

        /// <summary>
        /// Unsubscribes the authenticated user from the specified list.
        /// </summary>
        /// <param name="tokens">The tokens.</param>
        /// <param name="listId">The list id.</param>
        /// <param name="options">The options.</param>
        /// <param name="timeout">The timeout.</param>
        /// <param name="function">The function.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static IAsyncResult UnSubscribe(
            OAuthTokens tokens,
            decimal listId,
            OptionalProperties options,
            TimeSpan timeout,
            Action<TwitterAsyncResponse<TwitterList>> function)
        {
            return AsyncUtility.ExecuteAsyncMethod(tokens, listId, options, timeout, TwitterList.UnSubscribe, function);
        }
    }
}
