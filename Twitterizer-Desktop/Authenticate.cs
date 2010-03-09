//-----------------------------------------------------------------------
// <copyright file="Authenticate.cs" company="Patrick 'Ricky' Smith">
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
// <summary>The authentication form for pin-based oauth</summary>
//-----------------------------------------------------------------------

namespace Twitterizer_Desktop
{
    using System;
    using System.Configuration;
    using System.Globalization;
    using System.Windows.Forms;
    using Twitterizer;

    /// <summary>
    /// The authentication form for pin-based oauth
    /// </summary>
    public partial class Authenticate : Form
    {
        /// <summary>
        /// The request token
        /// </summary>
        private string requestToken;

        /// <summary>
        /// Initializes a new instance of the <see cref="Authenticate"/> class.
        /// </summary>
        public Authenticate()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Handles the Load event of the Authenticate control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void Authenticate_Load(object sender, EventArgs e)
        {
            OAuthTokenResponse requestAccessTokens = OAuthUtility.GetRequestToken(
                ConfigurationManager.AppSettings["Twitterizer.Desktop.ConsumerKey"],
                ConfigurationManager.AppSettings["Twitterizer.Desktop.ConsumerSecret"]);

            this.requestToken = requestAccessTokens.Token;

            this.RequestLinkText.Text = @"It appears to be your first time running our sample application. 
To authenticate yourself, please click here. When prompted, come back here and enter your PIN.";
            this.RequestLinkText.LinkArea = new LinkArea(this.RequestLinkText.Text.IndexOf("click here"), "click here".Length);
            this.RequestLinkText.Tag = OAuthUtility.BuildAuthorizationUri(requestAccessTokens.Token).AbsoluteUri;
        }

        /// <summary>
        /// Handles the LinkClicked event of the RequestLinkText control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.LinkLabelLinkClickedEventArgs"/> instance containing the event data.</param>
        private void RequestLinkText_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start((string)this.RequestLinkText.Tag);
        }

        /// <summary>
        /// Handles the Click event of the PinButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void PinButton_Click(object sender, EventArgs e)
        {
            try
            {
                OAuthTokenResponse accessTokens = OAuthUtility.GetAccessToken(
                        ConfigurationManager.AppSettings["Twitterizer.Desktop.ConsumerKey"],
                        ConfigurationManager.AppSettings["Twitterizer.Desktop.ConsumerSecret"],
                        this.requestToken,
                        this.PinTextBox.Text);

                Configuration appConfig = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                appConfig.AppSettings.Settings.Add("Twitterizer.Desktop.AccessToken", accessTokens.Token);
                appConfig.AppSettings.Settings.Add("Twitterizer.Desktop.AccessTokenSecret", accessTokens.TokenSecret);
                appConfig.AppSettings.Settings.Add("Twitterizer.Desktop.UserId", accessTokens.UserId.ToString(CultureInfo.CurrentCulture));
                appConfig.AppSettings.Settings.Add("Twitterizer.Desktop.ScreenName", accessTokens.ScreenName);
                appConfig.Save();

                ConfigurationManager.RefreshSection("appSettings");

                MessageBox.Show(
                    string.Format("Thanks for authenticating, {0}.", accessTokens.ScreenName));
            }
            catch (TwitterizerException tex)
            {
                MessageBox.Show(
                    string.Format("You could not be authenticated: {0}", tex.ErrorDetails.ErrorMessage));
            }
        }
    }
}
