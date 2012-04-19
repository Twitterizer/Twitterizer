using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Twitterizer;

public partial class Verify : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            CKTextBox.Text = Session["consumerkey"] as string ?? string.Empty;
            CSTextBox.Text = Session["consumersecret"] as string ?? string.Empty;
        }
    }

    protected void VerifyButton_Click(object sender, EventArgs e)
    {
        ResultLabel.Visible = true;

        OAuthTokens tokens = new OAuthTokens()
                                 {
                                     AccessToken = AKTextBox.Text,
                                     AccessTokenSecret = ASTextBox.Text,
                                     ConsumerKey = CKTextBox.Text,
                                     ConsumerSecret = CSTextBox.Text
                                 };

        TwitterResponse<TwitterUser> twitterResponse = TwitterAccount.VerifyCredentials(tokens);
        
        if (twitterResponse.Result == RequestResult.Success)
        {
            ResultLabel.Text = string.Format("Success! Verified as {0}", twitterResponse.ResponseObject.ScreenName);
            ResultLabel.CssClass = "ResultLabelSuccess";
        }
        else
        {
            ResultLabel.Text = string.Format("Failed! \"{0}\"", twitterResponse.ErrorMessage ?? "Not Authorized.");
            ResultLabel.CssClass = "ResultLabelFailed";
        }
    }
}