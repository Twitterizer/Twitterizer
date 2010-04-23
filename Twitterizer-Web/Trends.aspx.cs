using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Twitterizer;

public partial class Trends : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        this.TrendsGridView.DataSource = TwitterTrend.Current(false).Trends;
        this.TrendsGridView.DataBind();
    }
}
