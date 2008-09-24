/*
* TwitterStatusMethods.cs
*
* Copyright © 2008 Patrick "Ricky" Smith <ricky@digitally-born.com>
*
* This file is part of the Twitterizer library
*
* The Twitterizer library is free software: you can redistribute it
* and/or modify it under the terms of the GNU General Public License as
* published by the Free Software Foundation, either version 3 of the
* License, or (at your option) any later version.
* 
* This program is distributed in the hope that it will be useful,
* but WITHOUT ANY WARRANTY; without even the implied warranty of
* MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
* GNU General Public License for more details.
*
* You should have received a copy of the GNU General Public License
* along with this program.  If not, see <http://www.gnu.org/licenses/>.
*/
using System;
using System.Web;

namespace Twitterizer.Framework
{
    public class TwitterStatusMethods
    {
        private readonly string userName;
        private readonly string password;

        public TwitterStatusMethods(string UserName, string Password)
        {
            userName = UserName;
            password = Password;
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
            TwitterRequest Request = new TwitterRequest();
            TwitterRequestData Data = new TwitterRequestData();

            string actionUri = (Parameters == null ? "http://twitter.com/statuses/user_timeline.xml" : Parameters.BuildActionUri("http://twitter.com/statuses/user_timeline.xml"));
            Data.ActionUri = new Uri(actionUri);

            Data = Request.PerformWebRequest(Data);

            return Data.Statuses;
        }

        /// <summary>
        /// Returns the 20 most recent statuses from non-protected users who have set a custom user icon.  Does not require authentication.
        /// </summary>
        /// <returns></returns>
        public TwitterStatusCollection PublicTimeline()
        {
            TwitterRequest Request = new TwitterRequest();
            TwitterRequestData Data = new TwitterRequestData();

            Data.ActionUri = new Uri("http://twitter.com/statuses/public_timeline.xml");

            Data = Request.PerformWebRequest(Data);

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
        public TwitterStatusCollection FriendsTimeline(TwitterParameters Parameters)
        {
            TwitterRequest Request = new TwitterRequest();
            TwitterRequestData Data = new TwitterRequestData();
            Data.UserName = userName;
            Data.Password = password;

            string actionUri = (Parameters == null ? "http://twitter.com/statuses/friends_timeline.xml" : Parameters.BuildActionUri("http://twitter.com/statuses/friends_timeline.xml"));
            Data.ActionUri = new Uri(actionUri);

            Data = Request.PerformWebRequest(Data);

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
        public TwitterStatus Update(string Status, int? InReplyToStatusID)
        {
            TwitterRequest Request = new TwitterRequest();
            TwitterRequestData Data = new TwitterRequestData();
            Data.UserName = userName;
            Data.Password = password;



            Data.ActionUri = new Uri(
                string.Format("http://twitter.com/statuses/update.xml?status={0}&in_reply_to_status_id{1}", HttpUtility.UrlEncode(Status), InReplyToStatusID));

            Data = Request.PerformWebRequest(Data);

            return Data.Statuses[0];
        }

        /// <summary>
        /// Destroys the status specified by the required ID parameter.  The authenticating user must be the author of the specified status.
        /// </summary>
        /// <param name="ID">Required.  The ID of the status to destroy.</param>
        public void Destroy(int ID)
        {
            TwitterRequest Request = new TwitterRequest();
            TwitterRequestData Data = new TwitterRequestData();
            Data.UserName = userName;
            Data.Password = password;

            Data.ActionUri = new Uri(
                string.Format("http://twitter.com/statuses/destroy/{0}.xml", ID));

            Request.PerformWebRequest(Data);
        }

        /// <summary>
        /// Returns a single status, specified by the id parameter
        /// </summary>
        /// <param name="ID">id.  Required.  The numerical ID of the status you're trying to retrieve.</param>
        /// <returns></returns>
        public TwitterUser Show(string ID)
        {
            TwitterRequest Request = new TwitterRequest();
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
            TwitterRequest Request = new TwitterRequest();
            TwitterRequestData Data = new TwitterRequestData();
            Data.UserName = userName;
            Data.Password = password;

            string actionUri = (Parameters == null ? "http://twitter.com/statuses/replies.xml" : Parameters.BuildActionUri("http://twitter.com/statuses/replies.xml"));
            Data.ActionUri = new Uri(actionUri);

            Data = Request.PerformWebRequest(Data);

            return Data.Statuses;
        }
    }
}
