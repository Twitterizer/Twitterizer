<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default"
    MasterPageFile="~/MasterPage.master" Theme="DefaultTheme" %>

<%@ MasterType TypeName="Twitterizer.ExampleWeb.MasterPage" %>
<asp:Content runat="server" ContentPlaceHolderID="PageBodyContentPlaceHolder">
    <h2>
        What's Happening?</h2>
    <div style="width: 750px;">
        <asp:Label runat="server" ID="StatusUpdateLabel" />
        <asp:TextBox runat="server" ID="UpdateTextBox" TextMode="MultiLine" Rows="2" Columns="90" />
        <br />
        <asp:Button ID="UpdateButton" runat="server" Text="Tweet" Style="float: right; height: 26px;"
            OnClick="UpdateButton_Click" /></div>
    <h2>
        Home</h2>
    <asp:DataList runat="server" ID="homeDataList" DataSource='<%# HomePageStatuses %>'
        EnableViewState="false">
        <ItemTemplate>
            <a href="user.aspx?id=<%# Eval("User.Id") %>">
                <asp:Image runat="server" ImageUrl='<%# Eval("User.ProfileImageLocation") %>' CssClass="ProfilePictureSmall"
                    Style="float: left; padding: 8px 8px 8px 8px;" /></a>
            <div style="font-size: smaller;">
                <%# string.Format("{0:D} {0:t}", Eval("CreatedDate")) %></div>
            <asp:Literal runat="server" Visible='<%# Eval("RetweetedStatus") != null %>'>
                <span style="background-image: url(http://s.twimg.com/a/1270236195/images/sprite-icons.png);
                    background-position: -128px -64px; background-repeat: no-repeat; display: inline-block;
                    height: 14px; position: relative; top: 2px; width: 18px;"></span>
            </asp:Literal>
            <a href="user.aspx?id=<%# Eval("User.Id") %>">
                <%# Eval("User.ScreenName") %></a>&nbsp;<%# this.Master.LinkifyText((string)Eval("Text"))%><br />
            <div style="font-size: smaller;">
                via
                <%# Eval("Source") %></div>
        </ItemTemplate>
    </asp:DataList>
    <asp:LinkButton runat="server" ID="NextPageLinkButton" Text="Next Page" OnClick="NextPageLinkButton_Click" />
</asp:Content>
