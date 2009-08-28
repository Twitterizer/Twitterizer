/*
 * This file is part of the Twitterizer library <http://code.google.com/p/twitterizer/>
 *
 * Copyright (c) 2008, Patrick "Ricky" Smith <ricky@digitally-born.com>
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
using System;
namespace Twitterizer.Framework
{
    public class TwitterUserMethods
    {
        private readonly string userName;
        private readonly string password;
        private readonly string proxyUri = string.Empty;

        /// <summary>
        /// Initializes a new instance of the <see cref="TwitterUserMethods"/> class.
        /// </summary>
        /// <param name="UserName">Name of the user.</param>
        /// <param name="Password">The password.</param>
        public TwitterUserMethods(string UserName, string Password)
        {
            userName = UserName;
            password = Password;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TwitterUserMethods"/> class.
        /// </summary>
        /// <param name="UserName">Name of the user.</param>
        /// <param name="Password">The password.</param>
        /// <param name="ProxyUri">The proxy URI.</param>
        public TwitterUserMethods(string UserName, string Password, string ProxyUri)
        {
            userName = UserName;
            password = Password;
            proxyUri = ProxyUri;
        }

        /// <summary>
        /// Returns a single status, specified by the id parameter
        /// </summary>
        /// <param name="ID">id.  Required.  The numerical ID of the status you're trying to retrieve.</param>
        /// <returns></returns>
        public TwitterUser Show(string ID)
        {
            TwitterRequest Request = new TwitterRequest(proxyUri);
            TwitterRequestData Data = new TwitterRequestData();
            Data.UserName = userName;
            Data.Password = password;

            Data.ActionUri = new Uri(
                string.Format("http://twitter.com/users/show/{0}.xml", ID));

            Data = Request.PerformWebRequest(Data, "GET");

            return Data.Users[0];
        }

        /// <summary>
        /// Returns the authenticating user's followers, each with current status.
        /// </summary>
        /// <returns></returns>
        public TwitterUserCollection Followers()
        {
            return (Followers(null));
        }

        /// <summary>
        /// Returns the authenticating user's followers, each with current status.
        /// </summary>
        /// <param name="Parameters">Optional. Accepts ID and Page parameters.</param>
        /// <returns></returns>
        /// <remarks>See: http://apiwiki.twitter.com/Twitter-REST-API-Method:-statuses%C2%A0followers </remarks>
        public TwitterUserCollection Followers(TwitterParameters Parameters)
        {
            TwitterRequest Request = new TwitterRequest(proxyUri);
            TwitterRequestData Data = new TwitterRequestData();
            Data.UserName = userName;
            Data.Password = password;

            // Validate the parameters that are given.
            if (Parameters != null)
                foreach (TwitterParameterNames param in Parameters.Keys)
                    switch (param)
                    {
                        case TwitterParameterNames.ID:
                        case TwitterParameterNames.UserID:
                        case TwitterParameterNames.ScreenName:
                        case TwitterParameterNames.Page:
                            break;
                        default:
                            throw new InvalidTwitterParameterException(param, InvalidTwitterParameterReason.ParameterNotSupported);
                    }

            string actionUri = (Parameters == null ? "http://twitter.com/statuses/followers.xml" : Parameters.BuildActionUri("http://twitter.com/statuses/followers.xml"));
            Data.ActionUri = new Uri(actionUri);

            Data = Request.PerformWebRequest(Data, "GET");

            return Data.Users;
        }

        /// <summary>
        /// Returns up to 100 of the authenticating user's friends who have most recently updated, each with current status.
        /// </summary>
        /// <returns></returns>
        public TwitterUserCollection Friends()
        {
            return (Friends(null));
        }

        /// <summary>
        /// Returns up to 100 of the authenticating user's friends who have most recently updated, each with current status.
        /// </summary>
        /// <param name="Parameters">Optional. Accepts ID, Page, and Since parameters.</param>
        /// <returns></returns>
        /// <remarks>See: http://apiwiki.twitter.com/Twitter-REST-API-Method:-statuses%C2%A0friends </remarks>
        public TwitterUserCollection Friends(TwitterParameters Parameters)
        {
            // page 0 == page 1 is the start
            TwitterRequest Request = new TwitterRequest(proxyUri);
            TwitterRequestData Data = new TwitterRequestData();
            Data.UserName = userName;
            Data.Password = password;

            // Validate the parameters that are given.
            if (Parameters != null)
                foreach (TwitterParameterNames param in Parameters.Keys)
                    switch (param)
                    {
                        case TwitterParameterNames.ID:
                        case TwitterParameterNames.UserID:
                        case TwitterParameterNames.ScreenName:
                        case TwitterParameterNames.Page:
                            break;
                        default:
                            throw new InvalidTwitterParameterException(param, InvalidTwitterParameterReason.ParameterNotSupported);
                    }

            string actionUri = (Parameters == null ? "http://twitter.com/statuses/friends.xml" : Parameters.BuildActionUri("http://twitter.com/statuses/friends.xml"));
            Data.ActionUri = new Uri(actionUri);

            Data = Request.PerformWebRequest(Data, "GET");

            return Data.Users;
        }

		/// <summary>
		/// Creates a new friendship with a user. Returns the user that was followed.
		/// </summary>
		/// <param name="User">The User to follow.</param>
		/// <returns></returns>
		public TwitterUserCollection FollowUser(TwitterUser User)
		{
            TwitterRequest Request = new TwitterRequest(proxyUri);
			TwitterRequestData Data = new TwitterRequestData();
			Data.UserName = userName;
			Data.Password = password;

			string actionUri = string.Format("http://twitter.com/friendships/create/{0}.xml", User.ScreenName);
			Data.ActionUri = new Uri(actionUri);
			Data = Request.PerformWebRequest(Data);

			return Data.Users;
		}

        /// <summary>
        /// Destroys an existing friendship with a user.
        /// </summary>
        /// <param name="User">The user with which a friendship exists and should been destroyed.</param>
        /// <returns>The user with which a friendship has been destroyed.</returns>
        public TwitterUserCollection UnFollowUser(TwitterUser User)
        {
            TwitterRequest Request = new TwitterRequest(proxyUri);
            TwitterRequestData Data = new TwitterRequestData();
            Data.UserName = userName;
            Data.Password = password;

            string actionUri = string.Format("http://twitter.com/friendships/destroy/{0}.xml", User.ScreenName);
            Data.ActionUri = new Uri(actionUri);
            Data = Request.PerformWebRequest(Data);

            return Data.Users;
        }

        /// <summary>
        /// Destroys an existing friendship with a user.
        /// </summary>
        /// <param name="ScreenName">The user with which a friendship exists and should been destroyed.</param>
        /// <returns>The user with which a friendship has been destroyed.</returns>
        public TwitterUserCollection UnFollowUser(String ScreenName)
        {
            TwitterRequest Request = new TwitterRequest(proxyUri);
            TwitterRequestData Data = new TwitterRequestData();
            Data.UserName = userName;
            Data.Password = password;

            string actionUri = string.Format("http://twitter.com/friendships/destroy.xml?screen_name={0}", ScreenName);
            Data.ActionUri = new Uri(actionUri);
            Data = Request.PerformWebRequest(Data);

            return Data.Users;
        }

        /// <summary>
        /// Destroys an existing friendship with a user.
        /// </summary>
        /// <param name="UserID">The user with which a friendship exists and should been destroyed.</param>
        /// <returns>The user with which a friendship has been destroyed.</returns>
        public TwitterUserCollection UnFollowUser(Int64 UserID)
        {
            TwitterRequest Request = new TwitterRequest(proxyUri);
            TwitterRequestData Data = new TwitterRequestData();
            Data.UserName = userName;
            Data.Password = password;

            string actionUri = string.Format("http://twitter.com/friendships/destroy.xml?user_id={0}", UserID);
            Data.ActionUri = new Uri(actionUri);
            Data = Request.PerformWebRequest(Data);

            return Data.Users;
        }
    
    }
}
