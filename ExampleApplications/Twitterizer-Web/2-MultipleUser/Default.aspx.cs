using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Twitterizer;

public partial class _2_MultipleUser_Default : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {
        string consumerKey = Session["consumerkey"] as string;
        string consumerSecret = Session["consumersecret"] as string;

        if (string.IsNullOrEmpty(consumerKey) || string.IsNullOrEmpty(consumerSecret))
            Response.Redirect("~/", true);

        string callbackUrl = "http://localhost:59813/Twitterizer-Web/2-MultipleUser/callback.aspx";
        string requestToken = OAuthUtility.GetRequestToken(consumerKey, consumerSecret, callbackUrl).Token;
        RequestTokenLabel.Text = requestToken;

        Uri authenticationUri = OAuthUtility.BuildAuthorizationUri(requestToken);
        AuthenticationUrlLink.Text = authenticationUri.AbsoluteUri;
        AuthenticationUrlLink.NavigateUrl = authenticationUri.AbsoluteUri;
    }
}