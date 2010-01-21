/*
 * This file is part of the Twitterizer library <http://code.google.com/p/twitterizer/>
 *
 * Copyright (c) 2010, Patrick "Ricky" Smith <ricky@digitally-born.com>
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

    /// <summary>
    /// Represents a twitter account
    /// </summary>
    [Serializable]
    public class TwitterUser : TwitterObject
    {
        #region User Details
        /// <summary>
        /// Gets or sets the ID.
        /// </summary>
        /// <value>The ID.</value>
        public int ID { get; set; }

        /// <summary>
        /// Gets or sets the name of the user.
        /// </summary>
        /// <value>The name of the user.</value>
        public string UserName { get; set; }

        /// <summary>
        /// Gets or sets the name of the screen.
        /// </summary>
        /// <value>The name of the screen.</value>
        public string ScreenName { get; set; }

        /// <summary>
        /// Gets or sets the location.
        /// </summary>
        /// <value>The location.</value>
        public string Location { get; set; }

        /// <summary>
        /// Gets or sets the URI.
        /// </summary>
        /// <value>The URI.</value>
        public string Uri { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>The description.</value>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the created at.
        /// </summary>
        /// <value>The created at.</value>
        public DateTime? CreatedAt { get; set; }

        /// <summary>
        /// Gets or sets the number of followers.
        /// </summary>
        /// <value>The number of followers.</value>
        public int NumberOfFollowers { get; set; }

        /// <summary>
        /// Gets or sets the number of friends.
        /// </summary>
        /// <value>The number of friends.</value>
        public int NumberOfFriends { get; set; }

        /// <summary>
        /// Gets or sets the number of statuses.
        /// </summary>
        /// <value>The number of statuses.</value>
        public int NumberOfStatuses { get; set; }

        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        /// <value>The status.</value>
        public TwitterStatus Status { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is verified.
        /// </summary>
        /// <value>
        ///     <c>true</c> if this instance is verified; otherwise, <c>false</c>.
        /// </value>
        public bool IsVerified { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is protected.
        /// </summary>
        /// <value>
        ///     <c>true</c> if this instance is protected; otherwise, <c>false</c>.
        /// </value>
        public bool IsProtected { get; set; }

        /// <summary>
        /// Gets or sets the UTC offset.
        /// </summary>
        /// <value>The UTC offset.</value>
        public int? UTCOffset { get; set; }

        /// <summary>
        /// Gets or sets the time zone.
        /// </summary>
        /// <value>The time zone.</value>
        public string TimeZone { get; set; }
        #endregion

        #region User Profile Information
        /// <summary>
        /// Gets or sets the profile image URI.
        /// </summary>
        /// <value>The profile image URI.</value>
        public string ProfileImageUri { get; set; }

        /// <summary>
        /// Gets or sets the profile URI.
        /// </summary>
        /// <value>The profile URI.</value>
        public string ProfileUri { get; set; }

        /// <summary>
        /// Gets or sets the color of the profile background.
        /// </summary>
        /// <value>The color of the profile background.</value>
        public Color ProfileBackgroundColor { get; set; }

        /// <summary>
        /// Gets or sets the color of the profile text.
        /// </summary>
        /// <value>The color of the profile text.</value>
        public Color ProfileTextColor { get; set; }

        /// <summary>
        /// Gets or sets the color of the profile link.
        /// </summary>
        /// <value>The color of the profile link.</value>
        public Color ProfileLinkColor { get; set; }

        /// <summary>
        /// Gets or sets the color of the profile sidebar fill.
        /// </summary>
        /// <value>The color of the profile sidebar fill.</value>
        public Color ProfileSidebarFillColor { get; set; }

        /// <summary>
        /// Gets or sets the color of the profile sidebar border.
        /// </summary>
        /// <value>The color of the profile sidebar border.</value>
        public Color ProfileSidebarBorderColor { get; set; }

        /// <summary>
        /// Gets or sets the profile background image URI.
        /// </summary>
        /// <value>The profile background image URI.</value>
        public string ProfileBackgroundImageUri { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [profile background tile].
        /// </summary>
        /// <value>
        ///     <c>true</c> if [profile background tile]; otherwise, <c>false</c>.
        /// </value>
        public bool ProfileBackgroundTile { get; set; } 
        #endregion
        
        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="TwitterUser"/> is notifications.
        /// </summary>
        /// <value><c>true</c> if notifications; otherwise, <c>false</c>.</value>
        public bool Notifications { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="TwitterUser"/> is following.
        /// </summary>
        /// <value><c>true</c> if following; otherwise, <c>false</c>.</value>
        public bool Following { get; set; }

        /// <summary>
        /// Returns a <see cref="System.String"/> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String"/> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return string.IsNullOrEmpty(this.UserName) ? this.ScreenName : this.UserName;
        }
    }
}
