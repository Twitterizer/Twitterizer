using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Twitterizer;

public partial class _2_MultipleUser_callback : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string consumerKey = Session["consumerkey"] as string;
        string consumerSecret = Session["consumersecret"] as string;

        if (string.IsNullOrEmpty(consumerKey) || string.IsNullOrEmpty(consumerSecret))
            Response.Redirect("~/", true);

        //OAuthTokenResponse accessTokenResponse = OAuthUtility.GetAccessToken(consumerKey, consumerSecret,
        //                                                                     Request.QueryString["oauth_token"],
        //                                                                     Request.QueryString["oauth_verifier"]);
        
        OAuthTokenResponse accessTokenResponse = OAuthUtility.GetAccessTokenDuringCallback(consumerKey, consumerSecret);
        AccessTokenLabel.Text = accessTokenResponse.Token;
        AccessSecretLabel.Text = accessTokenResponse.TokenSecret;
        ScreenNameLabel.Text = accessTokenResponse.ScreenName;
        UserIdLabel.Text = accessTokenResponse.UserId.ToString();
    }
}