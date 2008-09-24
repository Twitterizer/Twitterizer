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
