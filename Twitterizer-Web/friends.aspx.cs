using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Twitterizer;

public partial class friends : System.Web.UI.Page
{
    public TwitterUserCollection FriendsCollection { get; set; }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            long userId = 0;

            if (!string.IsNullOrEmpty(Request.QueryString["userid"]) && long.TryParse(Request.QueryString["userid"], out userId))
            {
                this.FriendsCollection = TwitterUser.GetFriends(Master.Tokens, userId);
            }
            else
            {
                this.FriendsCollection = TwitterUser.GetFriends(Master.Tokens);
            }

            ViewState.Add("friends", this.FriendsCollection);
            this.DataBind();
        }
        else
        {
            this.FriendsCollection = ViewState["friends"] as TwitterUserCollection;
        }
    }

    protected void NextPageLinkButton_Click(object sender, EventArgs e)
    {
        this.FriendsCollection = this.FriendsCollection.NextPage();
        this.DataBind();

        ViewState["friends"] = this.FriendsCollection;
    }
}
