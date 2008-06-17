using System;
using System.Collections.Generic;
using System.Text;

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

        private bool isProtected  = false;
        public bool IsProtected
        {
            get { return isProtected; }
            set { isProtected = value; }
        }

        private int numberOfFollowers = 0;
        public int NumberOfFollowers
        {
            get { return numberOfFollowers; }
            set { numberOfFollowers = value; }
        }

        private int friends_count = 0;
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
            return this.userName;
        }
    }
}
