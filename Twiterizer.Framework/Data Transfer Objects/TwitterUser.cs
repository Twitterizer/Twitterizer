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
namespace Twitterizer.Framework
{
    using System;
    using System.Drawing;

    [Serializable]
    public class TwitterUser : TwitterObject
    {
        // Information about the user
        public int ID { get; set; }
        public string UserName { get; set; }
        public string ScreenName { get; set; }
        public string Location { get; set; }
        public string Uri { get; set; }
        public string Description { get; set; }
        public DateTime? CreatedAt { get; set; }
        public int NumberOfFollowers { get; set; }
        public int NumberOfFriends { get; set; }
        public int NumberOfStatuses { get; set; }
        public TwitterStatus Status { get; set; }
        public bool IsVerified { get; set; }

        // Profile Links and Colors
        public string ProfileImageUri { get; set; }
        public string ProfileUri { get; set; }
        public Color ProfileBackgroundColor { get; set; }
        public Color ProfileTextColor { get; set; }
        public Color ProfileLinkColor { get; set; }
        public Color ProfileSidebarFillColor { get; set; }
        public Color ProfileSidebarBorderColor { get; set; }
        public string ProfileBackgroundImageUri { get; set; }
        public bool ProfileBackgroundTile { get; set; }
        
        // User settings
        public bool IsProtected { get; set; }
        public int? UTCOffset { get; set; }
        public string TimeZone { get; set; }

        // Information about the relationship beteen this user and the authenticated user
        public bool Notifications { get; set; }
        public bool Following { get; set; }
        
        public override string ToString()
        {
            return (string.IsNullOrEmpty(this.UserName) ? this.ScreenName : this.UserName);
        }
    }
}
