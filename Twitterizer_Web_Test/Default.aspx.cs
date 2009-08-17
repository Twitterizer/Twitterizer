using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

using Twitterizer.Framework;

namespace Twitterizer_Web_Test
{
    public partial class _Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            bool isLoggedIn = (!string.IsNullOrEmpty(Session["UserName"] as string) && !string.IsNullOrEmpty(Session["Password"] as string));
            LoginPanel.Visible = !isLoggedIn;
            ActionPanel.Visible = isLoggedIn;
        }

        protected void SaveLoginButton_Click(object sender, EventArgs e)
        {
            Session["Username"] = UsernameTextBox.Text;
            Session["Password"] = PasswordTextBox.Text;

            Response.Redirect("~/", true);
        }

        protected void ValidateLoginButton_Click(object sender, EventArgs e)
        {
            if (Twitter.VerifyCredentials(UsernameTextBox.Text, PasswordTextBox.Text))
                ValidationLabel.Text = "Login passed validation. <br />";
            else
                ValidationLabel.Text = "Login failed validation. <br />";
        }
    }
}
