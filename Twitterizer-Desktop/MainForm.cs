//-----------------------------------------------------------------------
// <copyright file="MainForm.cs" company="Patrick 'Ricky' Smith">
//  This file is part of the Twitterizer library (http://code.google.com/p/twitterizer/)
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
// <summary>The main form of the example desktop application.</summary>
//-----------------------------------------------------------------------
namespace Twitterizer_Desktop
{
    using System;
    using System.Configuration;
    using System.Windows.Forms;
    using Twitterizer;

    /// <summary>
    /// The main form of the example desktop application.
    /// </summary>
    public partial class MainForm : Form
    {
        /// <summary>
        /// The authenticated user.
        /// </summary>
        private TwitterUser user;

        /// <summary>
        /// The OAuth Access Tokens
        /// </summary>
        private OAuthTokens oauthTokens;

        /// <summary>
        /// Initializes a new instance of the <see cref="MainForm"/> class.
        /// </summary>
        public MainForm()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Handles the Load event of the MainForm control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void MainForm_Load(object sender, EventArgs e)
        {
            this.AuthorizeAndGetUser();

            this.WelcomeLabel.Text = string.Format(
@"Welcome {0},
You have {1} friends and {2} followers.

Your last status was ""{3}"" on {4:D}
",
 this.user.ScreenName,
 this.user.NumberOfFriends,
 this.user.NumberOfFollowers,
 this.user.Status.Text, 
 this.user.Status.CreatedDate);

            foreach (TwitterStatus status in TwitterStatus.GetHomeTimeline(this.oauthTokens))
            {
                this.HomeTimelinePanel.Controls.Add(new TweetTimelineControl(status));
            }
        }

        /// <summary>
        /// Authorizes the user and gets their information.
        /// </summary>
        private void AuthorizeAndGetUser()
        {
            for (int i = 0; i < 3; i++)
            {
                if (this.IsUserAuthenticated())
                {
                    break;
                }

                new Authenticate().ShowDialog(this);
            }

            if (!this.IsUserAuthenticated())
            {
                MessageBox.Show(
                    this,
                    "You cannot continue without authenticating, please try again later.",
                    "Authentication required",
                    MessageBoxButtons.OK);
                
                Application.Exit();
            }

            this.oauthTokens = new OAuthTokens()
            {
                AccessToken = ConfigurationManager.AppSettings["Twitterizer.Desktop.AccessToken"],
                AccessTokenSecret = ConfigurationManager.AppSettings["Twitterizer.Desktop.AccessTokenSecret"],
                ConsumerKey = ConfigurationManager.AppSettings["Twitterizer.Desktop.ConsumerKey"],
                ConsumerSecret = ConfigurationManager.AppSettings["Twitterizer.Desktop.ConsumerSecret"]
            };

            ulong userId = ulong.Parse(ConfigurationManager.AppSettings["Twitterizer.Desktop.UserId"]);

            this.user = TwitterUser.GetUser(this.oauthTokens, userId);
        }

        /// <summary>
        /// Determines whether the user is authenticated.
        /// </summary>
        /// <returns>
        /// <c>true</c> if the user is authenticated; otherwise, <c>false</c>.
        /// </returns>
        private bool IsUserAuthenticated()
        {
            return !string.IsNullOrEmpty(ConfigurationManager.AppSettings["Twitterizer.Desktop.AccessToken"]);
        }
    }
}
