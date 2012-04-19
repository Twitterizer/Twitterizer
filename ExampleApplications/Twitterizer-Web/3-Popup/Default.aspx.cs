using System;
using System.Linq;
using Twitterizer;

public partial class _3_Popup_Default : System.Web.UI.Page
{
    protected string AuthenticationUrl;

    protected void Page_Load(object sender, EventArgs e)
    {
        string consumerKey = Session["consumerkey"] as string;
        string consumerSecret = Session["consumersecret"] as string;

        if (string.IsNullOrEmpty(consumerKey) || string.IsNullOrEmpty(consumerSecret))
            Response.Redirect("~/", true);

        if (IsPostBack) return;

        if (Request.QueryString.AllKeys.Contains("oauth_token"))
        {
            CallbackPanel.Visible = true;
            SetupPanel.Visible = false;

            OAuthTokenResponse accessTokenResponse = OAuthUtility.GetAccessTokenDuringCallback(consumerKey, consumerSecret);
            AccessTokenLabel.Text = accessTokenResponse.Token;
            AccessSecretLabel.Text = accessTokenResponse.TokenSecret;
            ScreenNameLabel.Text = accessTokenResponse.ScreenName;
            UserIdLabel.Text = accessTokenResponse.UserId.ToString();
        }
        else
        {
            string callbackUrl = "http://localhost:59813/Twitterizer-Web/3-Popup/callback.htm";

            string requestToken = OAuthUtility.GetRequestToken(consumerKey, consumerSecret, callbackUrl).Token;
            RequestTokenLabel.Text = requestToken;

            Uri authenticationUri = OAuthUtility.BuildAuthorizationUri(requestToken);
            AuthenticationUrlLink.Text = authenticationUri.AbsoluteUri;
            AuthenticationUrlLink.NavigateUrl = authenticationUri.AbsoluteUri;

            AuthenticationUrlLink.Attributes.Add("onclick",
                                                 "window.open(this.href, \"twitter-auth-window\", \"status=0,toolbar=0,location=1,menubar=0\"); return false;");

            AuthenticationUrl = authenticationUri.AbsoluteUri;
        }
    }
}