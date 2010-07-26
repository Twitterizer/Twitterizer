<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="search.aspx.cs" Inherits="search" %>

<%@ MasterType TypeName="Twitterizer.ExampleWeb.MasterPage" %>
<asp:Content ContentPlaceHolderID="PageBodyContentPlaceHolder" runat="Server">
    <fieldset>
        <label>
            Search Criteria</label>
        <asp:Label runat="server" Text="Query:" AssociatedControlID="QueryTextBox" /><asp:TextBox
            ID="QueryTextBox" runat="server" Text="Twitterizer" /><asp:Button ID="SearchButton"
                runat="server" Text="Search" OnClick="SearchButton_Click" />
    </fieldset>
    <asp:DataList ID="SearchResultsDataList" runat="server">
        <ItemTemplate>
            <asp:Image runat="server" ImageUrl='<%# Eval("ProfileImageLocation") %>' CssClass="ProfilePictureSmall"
                Style="float: left; padding: 8px 8px 8px 8px;" />
            <div style="font-size: smaller;">
                <%# string.Format("{0:D} {0:t}", Eval("CreatedDate")) %></div>
            <a href="user.aspx?username=<%# Eval("FromUserScreenName") %>">
                <%# Eval("FromUserScreenName") %></a>&nbsp;<%# this.Master.LinkifyText((string)Eval("Text"))%><br />
            <div style="font-size: smaller;">
                via
                <%# HttpUtility.HtmlDecode((string)Eval("Source")) %></div>
        </ItemTemplate>
    </asp:DataList>
</asp:Content>
