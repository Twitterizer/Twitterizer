/*
 * This file is part of the Twitterizer library <http://code.google.com/p/twitterizer/>
 *
 * Copyright (c) 2008, Patrick "Ricky" Smith <ricky@digitally-born.com>
 * All rights reserved.
 * 
 * Redistribution and use in source and binary forms, with or without modification, are 
 * permitted provided that the following conditions are met:
 *
 * - Redistributions of source code must retain the above copyright notice, this list 
 *   of conditions and the following disclaimer.
 * - Redistributions in binary form must reproduce the above copyright notice, this list 
 *   of conditions and the following disclaimer in the documentation and/or other 
 *   materials provided with the distribution.
 * - Neither the name of the Twitterizer nor the names of its contributors may be 
 *   used to endorse or promote products derived from this software without specific 
 *   prior written permission.
 *
 * THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND 
 * ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED 
 * WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. 
 * IN NO EVENT SHALL THE COPYRIGHT OWNER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, 
 * INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT 
 * NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR 
 * PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, 
 * WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) 
 * ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE 
 * POSSIBILITY OF SUCH DAMAGE.
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

        private long id;
        public long ID
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

        private long inReplyToStatusID;
        public long InReplyToStatusID
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
