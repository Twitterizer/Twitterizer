using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Twitterizer;

public partial class user : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        int userId = 0;

        if (string.IsNullOrEmpty(Request.QueryString["id"]) || !int.TryParse(Request.QueryString["id"], out userId))
        {
            Response.Redirect("~/", true);
        }

        if (!this.IsPostBack)
        {
            List<TwitterUser> dummyCollection = new List<TwitterUser>();
            dummyCollection.Add(TwitterUser.GetUser(this.Master.Tokens, userId));

            this.UserDetailsView.DataSource = dummyCollection;
            this.UserDetailsView.DataBind();
        }
    }
}
