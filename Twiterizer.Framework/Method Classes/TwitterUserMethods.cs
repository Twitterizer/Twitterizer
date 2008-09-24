/*
* TwitterUserMethods.cs
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
namespace Twitterizer.Framework
{
    public class TwitterUserMethods
    {
        private readonly string userName;
        private readonly string password;

        public TwitterUserMethods(string UserName, string Password)
        {
            userName = UserName;
            password = Password;
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
        public TwitterUserCollection Followers(TwitterParameters Parameters)
        {
            TwitterRequest Request = new TwitterRequest();
            TwitterRequestData Data = new TwitterRequestData();
            Data.UserName = userName;
            Data.Password = password;


            string actionUri = (Parameters == null ? "http://twitter.com/statuses/followers.xml" : Parameters.BuildActionUri("http://twitter.com/statuses/followers.xml"));
            Data.ActionUri = new Uri(actionUri);

            Data = Request.PerformWebRequest(Data);

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
        public TwitterUserCollection Friends(TwitterParameters Parameters)
        {
            // page 0 == page 1 is the start
            TwitterRequest Request = new TwitterRequest();
            TwitterRequestData Data = new TwitterRequestData();
            Data.UserName = userName;
            Data.Password = password;

            string actionUri = (Parameters == null ? "http://twitter.com/statuses/friends.xml" : Parameters.BuildActionUri("http://twitter.com/statuses/friends.xml"));
            Data.ActionUri = new Uri(actionUri);

            Data = Request.PerformWebRequest(Data);

            return Data.Users;
        }
    }
}
