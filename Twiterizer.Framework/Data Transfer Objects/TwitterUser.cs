/*
* TwitterUser.cs
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
    public class TwitterUser
    {
        private int id;
        public int ID
        {
            get { return id; }
            set { id = value; }
        }

        private string userName = "";
        public string UserName
        {
            get { return userName; }
            set { userName = value; }
        }

        private string screenName = "";
        public string ScreenName
        {
            get { return screenName; }
            set { screenName = value; }
        }

        private string location = "";
        public string Location
        {
            get { return location; }
            set { location = value; }
        }

        private string timeZone = "";
        public string TimeZone
        {
            get { return timeZone; }
            set { timeZone = value; }
        }

        private string description = "";
        public string Description
        {
            get { return description; }
            set { description = value; }
        }

        private Uri profileImageUri;
        public Uri ProfileImageUri
        {
            get { return profileImageUri; }
            set { profileImageUri = value; }
        }

        private Uri profileUri;
        public Uri ProfileUri
        {
            get { return profileUri; }
            set { profileUri = value; }
        }

        private bool isProtected;
        public bool IsProtected
        {
            get { return isProtected; }
            set { isProtected = value; }
        }

        private int numberOfFollowers;
        public int NumberOfFollowers
        {
            get { return numberOfFollowers; }
            set { numberOfFollowers = value; }
        }

        private int friends_count;
        public int Friends_count
        {
            get { return friends_count; }
            set { friends_count = value; }
        }

        private TwitterStatus status;
        public TwitterStatus Status
        {
            get { return status; }
            set { status = value; }
        }
	

        public override string ToString()
        {
            return userName;
        }
    }
}
