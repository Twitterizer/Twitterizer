using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Twitterizer.Framework
{
    public class TwitterStatus
    {
        private DateTime created;
        public DateTime Created
        {
            get { return created; }
            set { created = value; }
        }

        private int id = 0;
        public int ID
        {
            get { return id; }
            set { id = value; }
        }

        private string text = "";
        public string Text
        {
            get { return text; }
            set { text = value; }
        }

        private string source = "";
        public string Source
        {
            get { return source; }
            set { source = value; }
        }

        private int recipientID = 0;
        public int RecipientID
        {
            get { return recipientID; }
            set { recipientID = value; }
        }

        private bool isTruncated = false;
        public bool IsTruncated
        {
            get { return isTruncated; }
            set { isTruncated = value; }
        }

        private bool isFavorited = false;
        public bool IsFavorited
        {
            get { return isFavorited; }
            set { isFavorited = value; }
        }

        private int inReplyToStatusID = 0;
        public int InReplyToStatusID
        {
            get { return inReplyToStatusID; }
            set { inReplyToStatusID = value; }
        }

        private int inReplyToUserID = 0;
        public int InReplyToUserID
        {
            get { return inReplyToUserID; }
            set { inReplyToUserID = value; }
        }
	

        private TwitterUser twitterUser;
        public TwitterUser TwitterUser
        {
            get { return twitterUser; }
            set { twitterUser = value; }
        }
    }
}
