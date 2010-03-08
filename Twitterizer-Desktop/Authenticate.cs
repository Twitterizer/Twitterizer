using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Configuration;
using Twitterizer;
using System.Globalization;

namespace Twitterizer_Desktop
{
    public partial class Authenticate : Form
    {
        private string requestToken;

        public Authenticate()
        {
            InitializeComponent();
        }

        private void Authenticate_Load(object sender, EventArgs e)
        {
            OAuthTokenResponse requestAccessTokens = OAuthUtility.GetRequestToken(
                ConfigurationManager.AppSettings["Twitterizer.Desktop.ConsumerKey"],
                ConfigurationManager.AppSettings["Twitterizer.Desktop.ConsumerSecret"]);

            this.requestToken = requestAccessTokens.Token;

            this.RequestLinkText.Text = @"It appears to be your first time running our sample application. 
To authenticate yourself, please click here. When prompted, come back here and enter your PIN.";
            this.RequestLinkText.LinkArea = new LinkArea(this.RequestLinkText.Text.IndexOf("click here"), "click here".Length);
            this.RequestLinkText.Tag = string.Format("http://twitter.com/oauth/authorize?oauth_token={0}", requestAccessTokens.Token); ;   
        }

        private void RequestLinkText_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start((string)this.RequestLinkText.Tag);
        }

        private void PinButton_Click(object sender, EventArgs e)
        {
            try
            {
                OAuthTokenResponse accessTokens = OAuthUtility.GetAccessToken(
                        ConfigurationManager.AppSettings["Twitterizer.Desktop.ConsumerKey"],
                        ConfigurationManager.AppSettings["Twitterizer.Desktop.ConsumerSecret"],
                        this.requestToken,
                        PinTextBox.Text);

                Configuration appConfig = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                appConfig.AppSettings.Settings.Add("Twitterizer.Desktop.AccessToken", accessTokens.Token);
                appConfig.AppSettings.Settings.Add("Twitterizer.Desktop.AccessTokenSecret", accessTokens.TokenSecret);
                appConfig.AppSettings.Settings.Add("Twitterizer.Desktop.UserId", accessTokens.UserId.ToString(CultureInfo.CurrentCulture));
                appConfig.AppSettings.Settings.Add("Twitterizer.Desktop.ScreenName", accessTokens.ScreenName);
                appConfig.Save();

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
