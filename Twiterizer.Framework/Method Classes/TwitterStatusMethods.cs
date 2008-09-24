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

        public TwitterStatusCollection UserTimeline()
        {
            return UserTimeline(null);
        }

        public TwitterStatusCollection UserTimeline(TwitterParameters Parameters)
        {
            TwitterRequest Request = new TwitterRequest();
            TwitterRequestData Data = new TwitterRequestData();

            string actionUri = (Parameters == null ? "http://twitter.com/statuses/user_timeline.xml" : Parameters.BuildActionUri("http://twitter.com/statuses/user_timeline.xml"));
            Data.ActionUri = new Uri(actionUri);

            Data = Request.PerformWebRequest(Data);

            return Data.Statuses;
        }

        public TwitterStatusCollection PublicTimeline()
        {
            TwitterRequest Request = new TwitterRequest();
            TwitterRequestData Data = new TwitterRequestData();

            Data.ActionUri = new Uri("http://twitter.com/statuses/public_timeline.xml");

            Data = Request.PerformWebRequest(Data);

            return Data.Statuses;
        }

        public TwitterStatusCollection FriendsTimeline()
        {
            return FriendsTimeline(null);
        }

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

        public TwitterStatus Update(string Status)
        {
            return Update(Status, null);
        }

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


        public TwitterUser Show(string id_or_ScreenName)
        {
            TwitterRequest Request = new TwitterRequest();
            TwitterRequestData Data = new TwitterRequestData();
            Data.UserName = userName;
            Data.Password = password;

            Data.ActionUri = new Uri(
                string.Format("http://twitter.com/users/show/{0}.xml", id_or_ScreenName));

            Data = Request.PerformWebRequest(Data, "GET");

            return Data.Users[0];
        }

        public TwitterStatusCollection Replies()
        {
            return Replies(null);
        }

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
