/*
 * This file is part of the Twitterizer library <http://code.google.com/p/twitterizer/>
 *
 * Copyright (c) 2010, Patrick "Ricky" Smith <ricky@digitally-born.com>
 * All rights reserved.
 * 
 * Redistribution and use in source and binary forms, with or without modification, are 
 * permitted provided that the following conditions are met:
 *
 * - Redistributions of source code must retain the above copyright notice, this list 
 *   of conditions and the following disclaimer.
 * - Redistributions in binary form must reproduce the above copyright notice, this list 
 *   of conditions and the following disclaimer in the documentation and/or other 
 *   materials provided with the distribution.
 * - Neither the name of the Twitterizer nor the names of its contributors may be 
 *   used to endorse or promote products derived from this software without specific 
 *   prior written permission.
 *
 * THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND 
 * ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED 
 * WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. 
 * IN NO EVENT SHALL THE COPYRIGHT OWNER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, 
 * INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT 
 * NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR 
 * PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, 
 * WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) 
 * ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE 
 * POSSIBILITY OF SUCH DAMAGE.
 */
namespace Twitterizer.Framework
{
    using System;
    using System.Web;

    public class TwitterStatusMethods : TwitterMethodBase
    {
        private readonly string userName;
        private readonly string password;
        private readonly string source;
        private readonly string proxyUri = string.Empty;

        /// <summary>
        /// Initializes a new instance of the <see cref="TwitterStatusMethods"/> class.
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <param name="password">The password.</param>
        /// <param name="source">The source.</param>
        /// <param name="proxyUri">The proxy URI.</param>
        public TwitterStatusMethods(string userName, string password, string source, string proxyUri)
        {
            this.userName = userName;
            this.password = password;
            this.source = source;
            this.proxyUri = proxyUri;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TwitterStatusMethods"/> class.
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <param name="password">The password.</param>
        /// <param name="source">The source.</param>
        public TwitterStatusMethods(string userName, string password, string source)
        {
            this.userName = userName;
            this.password = password;
            this.source = source;
        }

        /// <summary>
        /// Returns the 20 most recent statuses posted from the authenticating user.
        /// </summary>
        /// <returns></returns>
        public TwitterStatusCollection UserTimeline()
        {
            return this.UserTimeline(null);
        }

        /// <summary>
        /// Returns the 20 most recent statuses posted from the authenticating user.
        /// </summary>
        /// <param name="parameters">Accepts Count, Since, SinceID, and Page parameters.</param>
        /// <returns></returns>
        public TwitterStatusCollection UserTimeline(TwitterParameters parameters)
        {
            TwitterRequest request = new TwitterRequest(this.proxyUri);
            TwitterRequestData data = new TwitterRequestData();

            // If not login information is supplied, the ID parameter is required.
            if (string.IsNullOrEmpty(this.userName) && string.IsNullOrEmpty(this.password) && 
                (parameters == null || parameters.Count == 0 || !parameters.ContainsKey(TwitterParameterNames.ID)))
            {
                throw new InvalidTwitterParameterException(TwitterParameterNames.ID, InvalidTwitterParameterReason.MissingRequiredParameter);
            }

            if (!string.IsNullOrEmpty(this.userName) && !string.IsNullOrEmpty(this.password))
            {
                data.UserName = this.userName;
                data.Password = this.password;
            }
            
            data.ActionUri = this.BuildConditionalUrl(parameters, "statuses/user_timeline.xml");

            data = request.PerformWebRequest(data, "GET");

            return data.Statuses;
        }

        /// <summary>
        /// Returns the 20 most recent statuses from non-protected users who have set a custom user icon.  Does not require authentication.
        /// </summary>
        /// <returns></returns>
        public TwitterStatusCollection PublicTimeline()
        {
            TwitterRequest request = new TwitterRequest(this.proxyUri);
            TwitterRequestData data = new TwitterRequestData();

            data.ActionUri = this.BuildConditionalUrl(null, "statuses/public_timeline.xml");

            data = request.PerformWebRequest(data, "GET");

            return data.Statuses;
        }

        public TwitterStatusCollection HomeTimeline()
        {
            return this.HomeTimeline(null);
        }

        public TwitterStatusCollection HomeTimeline(TwitterParameters parameters)
        {
            TwitterRequest request = new TwitterRequest(this.proxyUri);
            TwitterRequestData data = new TwitterRequestData();
            data.UserName = this.userName;
            data.Password = this.password;

            if (parameters != null)
            {
                foreach (TwitterParameterNames param in parameters.Keys)
                {
                    switch (param)
                    {
                        case TwitterParameterNames.SinceID:
                        case TwitterParameterNames.MaxID:
                        case TwitterParameterNames.Count:
                        case TwitterParameterNames.Page:
                            break;
                        default:
                            throw new InvalidTwitterParameterException(param, InvalidTwitterParameterReason.ParameterNotSupported);
                    }
                }
            }

            data.ActionUri = this.BuildConditionalUrl(parameters, "statuses/home_timeline.xml");

            data = request.PerformWebRequest(data, "GET");

            return data.Statuses;
        }

        /// <summary>
        /// Returns the 20 most recent statuses posted by the authenticating user and that user's friends. This is the equivalent of /home on the Web.
        /// </summary>
        /// <returns></returns>
        public TwitterStatusCollection FriendsTimeline()
        {
            return this.FriendsTimeline(null);
        }

        /// <summary>
        /// Returns the 20 most recent statuses posted by the authenticating user and that user's friends. This is the equivalent of /home on the Web.
        /// </summary>
        /// <param name="Parameters">Accepts Since, SinceID, Count, and Page parameters.</param>
        /// <returns></returns>
        /// <remarks>See: http://apiwiki.twitter.com/Twitter-REST-API-Method:-statuses-friends_timeline </remarks>
        public TwitterStatusCollection FriendsTimeline(TwitterParameters parameters)
        {
            TwitterRequest request = new TwitterRequest(this.proxyUri);
            TwitterRequestData data = new TwitterRequestData();
            data.UserName = this.userName;
            data.Password = this.password;

            // Validate the parameters that are given.
            if (parameters != null)
            {
                foreach (TwitterParameterNames param in parameters.Keys)
                {
                    switch (param)
                    {
                        case TwitterParameterNames.SinceID:
                        case TwitterParameterNames.MaxID:
                        case TwitterParameterNames.Count:
                        case TwitterParameterNames.Page:
                            break;
                        default:
                            throw new InvalidTwitterParameterException(param, InvalidTwitterParameterReason.ParameterNotSupported);
                    }
                }
            }

            data.ActionUri = this.BuildConditionalUrl(parameters, "statuses/friends_timeline.xml");

            data = request.PerformWebRequest(data, "GET");

            return data.Statuses;
        }

        /// <summary>
        /// Updates the authenticating user's status.
        /// </summary>
        /// <param name="Status">Required.  The text of your status update.</param>
        /// <returns></returns>
        public TwitterStatus Update(string status)
        {
            return this.Update(status, null);
        }

        /// <summary>
        /// Updates the authenticating user's status.
        /// </summary>
        /// <param name="Status">Required.  The text of your status update.</param>
        /// <param name="InReplyToStatusID">Optional.  The ID of an existing status that the status to be posted is in reply to.</param>
        /// <returns></returns>
        public TwitterStatus Update(string status, long? inReplyToStatusID)
        {
            TwitterRequest request = new TwitterRequest(this.proxyUri);
            TwitterRequestData data = new TwitterRequestData();
            data.UserName = this.userName;
            data.Password = this.password;
            data.Source = this.source;

            if (inReplyToStatusID.HasValue)
            {
                data.ActionUri = new Uri(
                    string.Format("{3}statuses/update.xml?status={0}&in_reply_to_status_id={1}&source={2}", HttpUtility.UrlEncode(status), inReplyToStatusID.Value, this.source, Twitter.Domain));
            }
            else
            {
                data.ActionUri = new Uri(
                    string.Format("{2}statuses/update.xml?status={0}&source={1}", HttpUtility.UrlEncode(status), this.source, Twitter.Domain));
            }

            data = request.PerformWebRequest(data);

            return data.Statuses[0];
        }

        /// <summary>
        /// Destroys the status specified by the required ID parameter.  The authenticating user must be the author of the specified status.
        /// </summary>
        /// <param name="ID">Required.  The ID of the status to destroy.</param>
        /// <returns></returns>
        /// <remarks>See: http://apiwiki.twitter.com/Twitter-REST-API-Method:-statuses%C2%A0destroy </remarks>
        public TwitterStatus Destroy(long id)
        {
            TwitterRequest request = new TwitterRequest(this.proxyUri);
            TwitterRequestData data = new TwitterRequestData();
            data.UserName = this.userName;
            data.Password = this.password;

            data.ActionUri = new Uri(
                string.Format("{1}statuses/destroy/{0}.xml", id, Twitter.Domain));

            data = request.PerformWebRequest(data, "POST");
            return data.Statuses[0];
        }

        /// <summary>
        /// Destroys the status specified by the required ID parameter.  The authenticating user must be the author of the specified status.
        /// </summary>
        /// <param name="Status">The status.</param>
        /// <returns></returns>
        /// <remarks>See: http://apiwiki.twitter.com/Twitter-REST-API-Method:-statuses%C2%A0destroy </remarks>
        public TwitterStatus Destroy(TwitterStatus status)
        {
            return this.Destroy(status.ID);
        }

        /// <summary>
        /// Returns a single status, specified by the id parameter
        /// </summary>
        /// <param name="ID">id.  Required.  The numerical ID of the status you're trying to retrieve.</param>
        /// <returns></returns>
        public TwitterStatus Show(long id)
        {
            TwitterRequest request = new TwitterRequest(this.proxyUri);
            TwitterRequestData data = new TwitterRequestData();
            data.UserName = this.userName;
            data.Password = this.password;

            data.ActionUri = new Uri(
                string.Format("{1}statuses/show/{0}.xml", id, Twitter.Domain));

            data = request.PerformWebRequest(data, "GET");

            return data.Statuses[0];
        }

        /// <summary>
        /// Returns the 20 most recent @replies (status updates prefixed with @username) for the authenticating user.
        /// </summary>
        /// <returns></returns>
        public TwitterStatusCollection Replies()
        {
            return this.Replies(null);
        }

        /// <summary>
        /// Returns the 20 most recent @replies (status updates prefixed with @username) for the authenticating user.
        /// </summary>
        /// <param name="Parameters">Optional. Accepts Page, Since, and SinceID parameters.</param>
        /// <returns></returns>
        public TwitterStatusCollection Replies(TwitterParameters parameters)
        {
            TwitterRequest request = new TwitterRequest(this.proxyUri);
            TwitterRequestData data = new TwitterRequestData();
            data.UserName = this.userName;
            data.Password = this.password;

            // Validate the parameters that are given.
            if (parameters != null)
            {
                foreach (TwitterParameterNames param in parameters.Keys)
                {
                    switch (param)
                    {
                        case TwitterParameterNames.SinceID:
                        case TwitterParameterNames.MaxID:
                        case TwitterParameterNames.Count:
                        case TwitterParameterNames.Page:
                            break;
                        default:
                            throw new InvalidTwitterParameterException(param, InvalidTwitterParameterReason.ParameterNotSupported);
                    }
                }
            }

            data.ActionUri = this.BuildConditionalUrl(parameters, "statuses/replies.xml");

            data = request.PerformWebRequest(data, "GET");

            return data.Statuses;
        }

        /// <summary>
        /// Returns a timeline of statuses where the authenticated user is mentioned.
        /// </summary>
        /// <returns></returns>
        /// <remarks>See: http://apiwiki.twitter.com/Twitter-REST-API-Method:-statuses-mentions </remarks>
        public TwitterStatusCollection Mentions()
        {
            return this.Mentions(null);
        }

        /// <summary>
        /// Returns a timeline of statuses where the authenticated user is mentioned.
        /// </summary>
        /// <param name="Parameters">The parameters.</param>
        /// <returns></returns>
        /// <remarks>See: http://apiwiki.twitter.com/Twitter-REST-API-Method:-statuses-mentions </remarks>
        public TwitterStatusCollection Mentions(TwitterParameters parameters)
        {
            TwitterRequest request = new TwitterRequest(this.proxyUri);
            TwitterRequestData data = new TwitterRequestData();
            data.UserName = this.userName;
            data.Password = this.password;

            // Validate the parameters that are given.
            if (parameters != null)
            {
                foreach (TwitterParameterNames param in parameters.Keys)
                {
                    switch (param)
                    {
                        case TwitterParameterNames.SinceID:
                        case TwitterParameterNames.MaxID:
                        case TwitterParameterNames.Count:
                        case TwitterParameterNames.Page:
                            break;
                        default:
                            throw new InvalidTwitterParameterException(param, InvalidTwitterParameterReason.ParameterNotSupported);
                    }
                }
            }

            data.ActionUri = this.BuildConditionalUrl(parameters, "statuses/mentions.xml");

            data = request.PerformWebRequest(data, "GET");

            return data.Statuses;
        }

        #region Favorites Methods
        /// <summary>
        /// Returns the 20 most recent favorite statuses for the authenticating user or user specified by the ID parameter in the requested format.
        /// </summary>
        /// <param name="Parameters">An optional collection of parameters used to query Twitter.</param>
        /// <returns></returns>
        public TwitterStatusCollection FavoritesTimeline(TwitterParameters parameters)
        {
            TwitterRequest request = new TwitterRequest(this.proxyUri);
            TwitterRequestData data = new TwitterRequestData();
            data.UserName = this.userName;
            data.Password = this.password;

            // Validate the parameters that are given.
            if (parameters != null)
            {
                foreach (TwitterParameterNames param in parameters.Keys)
                {
                    switch (param)
                    {
                        case TwitterParameterNames.ID:
                        case TwitterParameterNames.Page:
                            break;
                        default:
                            throw new InvalidTwitterParameterException(param, InvalidTwitterParameterReason.ParameterNotSupported);
                    }
                }
            }

            data.ActionUri = this.BuildConditionalUrl(parameters, "favorites.xml");

            data = request.PerformWebRequest(data, "GET");

            return data.Statuses;
        }

        /// <summary>
        /// Favorites the status specified in the ID parameter as the authenticating user. Returns the favorite status when successful.
        /// </summary>
        /// <param name="StatusID">The ID of the status to favorite. </param>
        /// <returns></returns>
        public TwitterStatus CreateFavorite(long statusID)
        {
            TwitterRequest request = new TwitterRequest(this.proxyUri);
            TwitterRequestData data = new TwitterRequestData();
            data.UserName = this.userName;
            data.Password = this.password;

            string actionUri = string.Format("{1}favorites/create/{0}.xml", statusID, Twitter.Domain);
            data.ActionUri = new Uri(actionUri);

            data = request.PerformWebRequest(data, "POST");

            return data.Statuses[0];
        }

        /// <summary>
        /// Un-favorites the status specified in the ID parameter as the authenticating user. Returns the un-favorited status in the requested format when successful.
        /// </summary>
        /// <param name="StatusID">The ID of the status to un-favorite. </param>
        /// <returns></returns>
        public TwitterStatus DestroyFavorite(long statusID)
        {
            TwitterRequest request = new TwitterRequest(this.proxyUri);
            TwitterRequestData data = new TwitterRequestData();
            data.UserName = this.userName;
            data.Password = this.password;

            string actionUri = string.Format("{1}favorites/destroy/{0}.xml", statusID, Twitter.Domain);
            data.ActionUri = new Uri(actionUri);

            data = request.PerformWebRequest(data, "POST");

            return data.Statuses[0];
        }

        #endregion
    }
}
