using System;
using System.Collections.Generic;
using System.Text;
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
