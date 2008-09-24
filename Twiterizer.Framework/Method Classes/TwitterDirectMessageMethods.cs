/*
* TwitterDirectMessageMethods.cs
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
    public class TwitterDirectMessageMethods
    {
        private readonly string userName;
        private readonly string password;

        public TwitterDirectMessageMethods(string UserName, string Password)
        {
            userName = UserName;
            password = Password;
        }

        /// <summary>
        /// Returns a list of the 20 most recent direct messages sent to the authenticating user.
        /// </summary>
        /// <returns>A collection of <typeparamref name="TwitterStatus"/>TwitterStatus</returns> objects
        public TwitterStatusCollection DirectMessages()
        {
            return DirectMessages(null);
        }

        /// <summary>
        /// Returns a list of the most recent direct messages sent to the authenticating user.
        /// </summary>
        /// <param name="Parameters">Accepts Since, SinceID, and Page parameters</param>
        /// <returns></returns>
        public TwitterStatusCollection DirectMessages(TwitterParameters Parameters)
        {
            TwitterRequest Request = new TwitterRequest();
            TwitterRequestData Data = new TwitterRequestData();
            Data.UserName = userName;
            Data.Password = password;

            string actionUri = (Parameters == null ? "http://twitter.com/direct_messages.xml" : Parameters.BuildActionUri("http://twitter.com/direct_messages.xml"));
            Data.ActionUri = new Uri(actionUri);

            Data = Request.PerformWebRequest(Data, "GET");

            return Data.Statuses;
        }

        /// <summary>
        /// Returns a list of the 20 most recent direct messages sent by the authenticating user.
        /// </summary>
        /// <returns></returns>
        public TwitterStatusCollection DirectMessagesSent()
        {
            return DirectMessagesSent(null);
        }

        /// <summary>
        /// Returns a list of the most recent direct messages sent by the authenticating user.
        /// </summary>
        /// <param name="Parameters">Accepts Since, SinceID, and Page parameters</param>
        /// <returns></returns>
        public TwitterStatusCollection DirectMessagesSent(TwitterParameters Parameters)
        {
            TwitterRequest Request = new TwitterRequest();
            TwitterRequestData Data = new TwitterRequestData();
            Data.UserName = userName;
            Data.Password = password;

            string actionUri = (Parameters == null ? "http://twitter.com/direct_messages/sent.xml" : Parameters.BuildActionUri("http://twitter.com/direct_messages/sent.xml"));
            Data.ActionUri = new Uri(actionUri);

            Data = Request.PerformWebRequest(Data);

            return Data.Statuses;
        }
    }
}
