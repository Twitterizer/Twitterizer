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
using System.Web;

namespace Twitterizer.Framework
{
    public class TwitterStatusMethods
    {
        private readonly string userName;
        private readonly string password;
        private readonly string source;
        private readonly string proxyUri = string.Empty;

        /// <summary>
        /// Initializes a new instance of the <see cref="TwitterStatusMethods"/> class.
        /// </summary>
        /// <param name="UserName">Name of the user.</param>
        /// <param name="Password">The password.</param>
        /// <param name="Source">The source.</param>
        /// <param name="uri_proxy">The uri_proxy.</param>
        public TwitterStatusMethods(string UserName, string Password, string Source, string ProxyUri)
        {
            userName = UserName;
            password = Password;
            source = Source;
            proxyUri = ProxyUri;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TwitterStatusMethods"/> class.
        /// </summary>
        /// <param name="UserName">Name of the user.</param>
        /// <param name="Password">The password.</param>
        /// <param name="Source">The source.</param>
        public TwitterStatusMethods(string UserName, string Password, string Source)
        {
            userName = UserName;
            password = Password;
            source = Source;
        }

        /// <summary>
        /// Returns the 20 most recent statuses posted from the authenticating user.
        /// </summary>
        /// <returns></returns>
        public TwitterStatusCollection UserTimeline()
        {
            return UserTimeline(null);
        }

        /// <summary>
        /// Returns the 20 most recent statuses posted from the authenticating user.
        /// </summary>
        /// <param name="Parameters">Accepts Count, Since, SinceID, and Page parameters.</param>
        /// <returns></returns>
        public TwitterStatusCollection UserTimeline(TwitterParameters Parameters)
        {
            TwitterRequest Request = new TwitterRequest(proxyUri);
            TwitterRequestData Data = new TwitterRequestData();
            Data.UserName = userName;
            Data.Password = password;

            string actionUri = (Parameters == null ? "http://twitter.com/statuses/user_timeline.xml" : Parameters.BuildActionUri("http://twitter.com/statuses/user_timeline.xml"));
            Data.ActionUri = new Uri(actionUri);

            Data = Request.PerformWebRequest(Data, "GET");

            return Data.Statuses;
        }

        /// <summary>
        /// Returns the 20 most recent statuses from non-protected users who have set a custom user icon.  Does not require authentication.
        /// </summary>
        /// <returns></returns>
        public TwitterStatusCollection PublicTimeline()
        {
            TwitterRequest Request = new TwitterRequest(proxyUri);
            TwitterRequestData Data = new TwitterRequestData();

            Data.ActionUri = new Uri("http://twitter.com/statuses/public_timeline.xml");

            Data = Request.PerformWebRequest(Data, "GET");

            return Data.Statuses;
        }

        /// <summary>
        /// Returns the 20 most recent statuses posted by the authenticating user and that user's friends. This is the equivalent of /home on the Web.
        /// </summary>
        /// <returns></returns>
        public TwitterStatusCollection FriendsTimeline()
        {
            return FriendsTimeline(null);
        }

        /// <summary>
        /// Returns the 20 most recent statuses posted by the authenticating user and that user's friends. This is the equivalent of /home on the Web.
        /// </summary>
        /// <param name="Parameters">Accepts Since, SinceID, Count, and Page parameters.</param>
        /// <returns></returns>
        /// <remarks>See: http://apiwiki.twitter.com/Twitter-REST-API-Method:-statuses-friends_timeline </remarks>
        public TwitterStatusCollection FriendsTimeline(TwitterParameters Parameters)
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
                        case TwitterParameterNames.SinceID:
                        case TwitterParameterNames.MaxID:
                        case TwitterParameterNames.Count:
                        case TwitterParameterNames.Page:
                            break;
                        default:
                            throw new InvalidTwitterParameterException(param);
                    }


            string actionUri = (Parameters == null ? "http://twitter.com/statuses/friends_timeline.xml" : Parameters.BuildActionUri("http://twitter.com/statuses/friends_timeline.xml"));
            Data.ActionUri = new Uri(actionUri);

            Data = Request.PerformWebRequest(Data, "GET");

            return Data.Statuses;
        }

        /// <summary>
        /// Updates the authenticating user's status.
        /// </summary>
        /// <param name="Status">Required.  The text of your status update.</param>
        /// <returns></returns>
        public TwitterStatus Update(string Status)
        {
            return Update(Status, null);
        }

        /// <summary>
        /// Updates the authenticating user's status.
        /// </summary>
        /// <param name="Status">Required.  The text of your status update.</param>
        /// <param name="InReplyToStatusID">Optional.  The ID of an existing status that the status to be posted is in reply to.</param>
        /// <returns></returns>
        public TwitterStatus Update(string Status, Int64? InReplyToStatusID)
        {
            TwitterRequest Request = new TwitterRequest(proxyUri);
            TwitterRequestData Data = new TwitterRequestData();
            Data.UserName = userName;
            Data.Password = password;
            Data.Source = source;

            if (InReplyToStatusID.HasValue)
            {
                Data.ActionUri = new Uri(
                    string.Format("http://twitter.com/statuses/update.xml?status={0}&in_reply_to_status_id={1}&source={2}", HttpUtility.UrlEncode(Status), InReplyToStatusID.Value, source));
            }
            else
            {
                Data.ActionUri = new Uri(
                    string.Format("http://twitter.com/statuses/update.xml?status={0}&source={1}", HttpUtility.UrlEncode(Status), source));
            }

            Data = Request.PerformWebRequest(Data);

            return Data.Statuses[0];
        }

        /// <summary>
        /// Destroys the status specified by the required ID parameter.  The authenticating user must be the author of the specified status.
        /// </summary>
        /// <param name="ID">Required.  The ID of the status to destroy.</param>
        /// <returns></returns>
        /// <remarks>See: http://apiwiki.twitter.com/Twitter-REST-API-Method:-statuses%C2%A0destroy </remarks>
        public TwitterStatus Destroy(Int64 ID)
        {
            TwitterRequest Request = new TwitterRequest(proxyUri);
            TwitterRequestData Data = new TwitterRequestData();
            Data.UserName = userName;
            Data.Password = password;

            Data.ActionUri = new Uri(
                string.Format("http://twitter.com/statuses/destroy/{0}.xml", ID));

            Data = Request.PerformWebRequest(Data, "POST");
            return Data.Statuses[0];
        }

        /// <summary>
        /// Destroys the status specified by the required ID parameter.  The authenticating user must be the author of the specified status.
        /// </summary>
        /// <param name="Status">The status.</param>
        /// <returns></returns>
        /// <remarks>See: http://apiwiki.twitter.com/Twitter-REST-API-Method:-statuses%C2%A0destroy </remarks>
        public TwitterStatus Destroy(TwitterStatus Status)
        {
            return Destroy(Status.ID);
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
        /// Returns the 20 most recent @replies (status updates prefixed with @username) for the authenticating user.
        /// </summary>
        /// <returns></returns>
        public TwitterStatusCollection Replies()
        {
            return Replies(null);
        }

        /// <summary>
        /// Returns the 20 most recent @replies (status updates prefixed with @username) for the authenticating user.
        /// </summary>
        /// <param name="Parameters">Optional. Accepts Page, Since, and SinceID parameters.</param>
        /// <returns></returns>
        public TwitterStatusCollection Replies(TwitterParameters Parameters)
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
                        case TwitterParameterNames.SinceID:
                        case TwitterParameterNames.MaxID:
                        case TwitterParameterNames.Count:
                        case TwitterParameterNames.Page:
                            break;
                        default:
                            throw new InvalidTwitterParameterException(param);
                    }

            string actionUri = (Parameters == null ? "http://twitter.com/statuses/replies.xml" : Parameters.BuildActionUri("http://twitter.com/statuses/replies.xml"));
            Data.ActionUri = new Uri(actionUri);

            Data = Request.PerformWebRequest(Data, "GET");

            return Data.Statuses;
        }


        /// <summary>
        /// Returns a timeline of statuses where the authenticated user is mentioned.
        /// </summary>
        /// <returns></returns>
        /// <remarks>See: http://apiwiki.twitter.com/Twitter-REST-API-Method:-statuses-mentions </remarks>
        public TwitterStatusCollection Mentions()
        {
            return Mentions(null);
        }

        /// <summary>
        /// Returns a timeline of statuses where the authenticated user is mentioned.
        /// </summary>
        /// <param name="Parameters">The parameters.</param>
        /// <returns></returns>
        /// <remarks>See: http://apiwiki.twitter.com/Twitter-REST-API-Method:-statuses-mentions </remarks>
        public TwitterStatusCollection Mentions(TwitterParameters Parameters)
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
                        case TwitterParameterNames.SinceID:
                        case TwitterParameterNames.MaxID:
                        case TwitterParameterNames.Count:
                        case TwitterParameterNames.Page:
                            break;
                        default:
                            throw new InvalidTwitterParameterException(param);
                    }

            string actionUri = (Parameters == null ? "http://twitter.com/statuses/mentions.xml" : Parameters.BuildActionUri("http://twitter.com/statuses/replies.xml"));
            Data.ActionUri = new Uri(actionUri);

            Data = Request.PerformWebRequest(Data, "GET");

            return Data.Statuses;
        }
    }
}
