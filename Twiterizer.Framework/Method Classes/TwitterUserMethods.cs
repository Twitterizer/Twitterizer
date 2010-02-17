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

    public class TwitterUserMethods : TwitterMethodBase
    {
        private readonly string userName;
        private readonly string password;
        private readonly string proxyUri = string.Empty;

        /// <summary>
        /// Initializes a new instance of the <see cref="TwitterUserMethods"/> class.
        /// </summary>
        /// <param name="UserName">Name of the user.</param>
        /// <param name="Password">The password.</param>
        public TwitterUserMethods(string userName, string password)
        {
            this.userName = userName;
            this.password = password;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TwitterUserMethods"/> class.
        /// </summary>
        /// <param name="UserName">Name of the user.</param>
        /// <param name="Password">The password.</param>
        /// <param name="ProxyUri">The proxy URI.</param>
        public TwitterUserMethods(string userName, string password, string proxyUri)
        {
            this.userName = userName;
            this.password = password;
            this.proxyUri = proxyUri;
        }

        /// <summary>
        /// Returns a single status, specified by the parameters
        /// </summary>
        /// <param name="Parameters">The parameters.</param>
        /// <returns></returns>
        public TwitterUser Show(TwitterParameters parameters)
        {
            TwitterRequest request = new TwitterRequest(this.proxyUri);
            TwitterRequestData data = new TwitterRequestData();
            data.UserName = this.userName;
            data.Password = this.password;

            data.ActionUri = this.BuildConditionalUrl(parameters, "users/show.xml");

            // Validate the parameters that are given.
            if (parameters != null)
            {
                foreach (TwitterParameterNames param in parameters.Keys)
                {
                    switch (param)
                    {
                        case TwitterParameterNames.ID:
                        case TwitterParameterNames.ScreenName:
                        case TwitterParameterNames.UserID:
                            break;
                        default:
                            throw new InvalidTwitterParameterException(param, InvalidTwitterParameterReason.ParameterNotSupported);
                    }
                }
            }

            data = request.PerformWebRequest(data, "GET");

            return data.Users[0];
        }

        /// <summary>
        /// Returns a single status, specified by the id parameter
        /// </summary>
        /// <param name="ID">id.  Required.  The numerical ID of the status you're trying to retrieve.</param>
        /// <returns></returns>
        public TwitterUser Show(string id)
        {
            TwitterRequest request = new TwitterRequest(this.proxyUri);
            TwitterRequestData data = new TwitterRequestData();
            data.UserName = this.userName;
            data.Password = this.password;

            data.ActionUri = new Uri(string.Format("{1}users/show/{0}.xml", id, Twitter.Domain));

            data = request.PerformWebRequest(data, "GET");

            return data.Users[0];
        }

        /// <summary>
        /// Returns the authenticating user's followers, each with current status.
        /// </summary>
        /// <returns></returns>
        public TwitterUserCollection Followers()
        {
            return this.Followers(null);
        }

        /// <summary>
        /// Returns the authenticating user's followers, each with current status.
        /// </summary>
        /// <param name="Parameters">Optional. Accepts ID and Page parameters.</param>
        /// <returns></returns>
        /// <remarks>See: http://apiwiki.twitter.com/Twitter-REST-API-Method:-statuses%C2%A0followers </remarks>
        public TwitterUserCollection Followers(TwitterParameters parameters)
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
                        case TwitterParameterNames.UserID:
                        case TwitterParameterNames.ScreenName:
                        case TwitterParameterNames.Cursor:
                            break;
                        default:
                            throw new InvalidTwitterParameterException(param, InvalidTwitterParameterReason.ParameterNotSupported);
                    }
                }
            }

            data.ActionUri = this.BuildConditionalUrl(parameters, "statuses/followers.xml");

            data = request.PerformWebRequest(data, "GET");

            return data.Users;
        }

        /// <summary>
        /// Returns up to 100 of the authenticating user's friends who have most recently updated, each with current status.
        /// </summary>
        /// <returns></returns>
        public TwitterUserCollection Friends()
        {
            return this.Friends(null);
        }

        /// <summary>
        /// Returns up to 100 of the authenticating user's friends who have most recently updated, each with current status.
        /// </summary>
        /// <param name="Parameters">Optional. Accepts ID, Page, and Since parameters.</param>
        /// <returns></returns>
        /// <remarks>See: http://apiwiki.twitter.com/Twitter-REST-API-Method:-statuses%C2%A0friends </remarks>
        public TwitterUserCollection Friends(TwitterParameters parameters)
        {
            // page 0 == page 1 is the start
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
                        case TwitterParameterNames.UserID:
                        case TwitterParameterNames.ScreenName:
                        case TwitterParameterNames.Cursor:
                            break;
                        default:
                            throw new InvalidTwitterParameterException(param, InvalidTwitterParameterReason.ParameterNotSupported);
                    }
                }
            }

            data.ActionUri = this.BuildConditionalUrl(parameters, "statuses/friends.xml");

            data = request.PerformWebRequest(data, "GET");

            return data.Users;
        }

        /// <summary>
        /// Creates a new friendship with a user. Returns the user that was followed.
        /// </summary>
        /// <param name="User">The User to follow.</param>
        /// <returns></returns>
        public TwitterUserCollection FollowUser(TwitterUser user)
        {
            TwitterRequest request = new TwitterRequest(this.proxyUri);
            TwitterRequestData data = new TwitterRequestData();
            data.UserName = this.userName;
            data.Password = this.password;

            string actionUri = string.Format("friendships/create/{0}.xml", user.ScreenName);
            data.ActionUri = this.BuildConditionalUrl(null, actionUri);
            data = request.PerformWebRequest(data);

            return data.Users;
        }

        /// <summary>
        /// Destroys an existing friendship with a user.
        /// </summary>
        /// <param name="User">The user with which a friendship exists and should been destroyed.</param>
        /// <returns>The user with which a friendship has been destroyed.</returns>
        public TwitterUserCollection UnFollowUser(TwitterUser user)
        {
            TwitterRequest request = new TwitterRequest(this.proxyUri);
            TwitterRequestData data = new TwitterRequestData();
            data.UserName = this.userName;
            data.Password = this.password;

            string actionUri = string.Format("friendships/destroy/{0}.xml", user.ScreenName);
            data.ActionUri = this.BuildConditionalUrl(null, actionUri);
            data = request.PerformWebRequest(data);

            return data.Users;
        }

        /// <summary>
        /// Destroys an existing friendship with a user.
        /// </summary>
        /// <param name="ScreenName">The user with which a friendship exists and should been destroyed.</param>
        /// <returns>The user with which a friendship has been destroyed.</returns>
        public TwitterUserCollection UnFollowUser(string screenName)
        {
            TwitterRequest request = new TwitterRequest(this.proxyUri);
            TwitterRequestData data = new TwitterRequestData();
            data.UserName = this.userName;
            data.Password = this.password;

            TwitterParameters parameters = new TwitterParameters(TwitterParameterNames.ScreenName, screenName);

            data.ActionUri = this.BuildConditionalUrl(parameters, "friendships/destroy.xml");
            data = request.PerformWebRequest(data);

            return data.Users;
        }

        /// <summary>
        /// Destroys an existing friendship with a user.
        /// </summary>
        /// <param name="UserID">The user with which a friendship exists and should been destroyed.</param>
        /// <returns>The user with which a friendship has been destroyed.</returns>
        public TwitterUserCollection UnFollowUser(long userID)
        {
            TwitterRequest request = new TwitterRequest(this.proxyUri);
            TwitterRequestData data = new TwitterRequestData();
            data.UserName = this.userName;
            data.Password = this.password;

            TwitterParameters parameters = new TwitterParameters(TwitterParameterNames.UserID, userID);

            data.ActionUri = this.BuildConditionalUrl(parameters, "friendships/destroy.xml");
            data = request.PerformWebRequest(data);

            return data.Users;
        }
    }
}