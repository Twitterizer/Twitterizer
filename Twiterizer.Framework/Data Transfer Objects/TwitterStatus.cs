/*
* TwitterStatus.cs
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
    public class TwitterStatus
    {
        private DateTime created;
        public DateTime Created
        {
            get { return created; }
            set { created = value; }
        }

        private int id;
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

        private int recipientID;
        public int RecipientID
        {
            get { return recipientID; }
            set { recipientID = value; }
        }

        private bool isTruncated;
        public bool IsTruncated
        {
            get { return isTruncated; }
            set { isTruncated = value; }
        }

        private bool isFavorited;
        public bool IsFavorited
        {
            get { return isFavorited; }
            set { isFavorited = value; }
        }

        private int inReplyToStatusID;
        public int InReplyToStatusID
        {
            get { return inReplyToStatusID; }
            set { inReplyToStatusID = value; }
        }

        private int inReplyToUserID;
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
