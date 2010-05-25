//-----------------------------------------------------------------------
// <copyright file="TweetTimelineControl.cs" company="Patrick 'Ricky' Smith">
//  This file is part of the Twitterizer library (http://www.twitterizer.net/)
// 
//  Copyright (c) 2010, Patrick "Ricky" Smith (ricky@digitally-born.com)
//  All rights reserved.
//  
//  Redistribution and use in source and binary forms, with or without modification, are 
//  permitted provided that the following conditions are met:
// 
//  - Redistributions of source code must retain the above copyright notice, this list 
//    of conditions and the following disclaimer.
//  - Redistributions in binary form must reproduce the above copyright notice, this list 
//    of conditions and the following disclaimer in the documentation and/or other 
//    materials provided with the distribution.
//  - Neither the name of the Twitterizer nor the names of its contributors may be 
//    used to endorse or promote products derived from this software without specific 
//    prior written permission.
// 
//  THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND 
//  ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED 
//  WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. 
//  IN NO EVENT SHALL THE COPYRIGHT OWNER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, 
//  INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT 
//  NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR 
//  PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, 
//  WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) 
//  ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE 
//  POSSIBILITY OF SUCH DAMAGE.
// </copyright>
// <author>Ricky Smith</author>
// <summary>The tweet timeline user control class.</summary>
//-----------------------------------------------------------------------

namespace Twitterizer_Desktop
{
    using System;
    using System.Windows.Forms;
    using Twitterizer;

    /// <summary>
    /// The tweet timeline user control class.
    /// </summary>
    public partial class TweetTimelineControl : UserControl
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TweetTimelineControl"/> class.
        /// </summary>
        /// <param name="status">The status.</param>
        public TweetTimelineControl(TwitterStatus status)
        {
            if (status == null)
            {
                throw new ArgumentNullException("status");
            }

            this.InitializeComponent();

            this.userPictureBox.LoadAsync(status.User.ProfileImageLocation);
            this.UserNameLabel.Text = status.User.ScreenName;
            this.DateLabel.Text = status.CreatedDate.ToShortDateString();
            this.TextLabel.Text = status.Text;
        }
    }
}
