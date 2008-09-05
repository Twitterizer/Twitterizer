/*
* TwitterRequestData.cs
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
using System.Net;

namespace Twitterizer.Framework
{
    public class TwitterRequestData
    {
        #region Request Properties
        private string userName;
        public string UserName
        {
            get { return userName; }
            set { userName = value; }
        }

        private string password;
        public string Password
        {
            get { return password; }
            set { password = value; }
        }

        private Uri actionUri;
        public Uri ActionUri
        {
            get { return actionUri; }
            set { actionUri = value; }
        }

        private string response;
        public string Response
        {
            get { return response; }
            set { response = value; }
        }
        #endregion

        #region Response Properties
        private WebException responseException;
        public WebException ResponseException
        {
            get { return responseException; }
            set { responseException = value; }
        }

        private TwitterStatusCollection statuses;
        public TwitterStatusCollection Statuses
        {
            get { return statuses; }
            set { statuses = value; }
        }

        private TwitterUserCollection users;
        public TwitterUserCollection Users
        {
            get { return users; }
            set { users = value; }
        }
	
        #endregion
    }
}
