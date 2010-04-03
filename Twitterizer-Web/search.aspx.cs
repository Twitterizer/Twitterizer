using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Twitterizer;

public partial class search : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void SearchButton_Click(object sender, EventArgs e)
    {
        SearchResultsDataList.DataSource = TwitterSearch.Search(this.QueryTextBox.Text);
        SearchResultsDataList.DataBind();
    }
}
