<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="Default.aspx.cs" Inherits="_3_Popup_Default" %>

<asp:Content ID="Content2" ContentPlaceHolderID="PageBodyContentPlaceHolder" runat="Server">
    This example will show you a quick way to use Ajax.net and a javascript popup so
    that the user does not need to leave your site. You should absolutely check out
    the basic example before digging through this example.<br />
    <asp:Panel ID="SetupPanel" runat="server">
        <br />
        Request Token:
        <asp:Label ID="RequestTokenLabel" runat="server" /><br />
        <br />
        Authentication URL:
        <asp:HyperLink ID="AuthenticationUrlLink" runat="server" /><br />
        <br />
        To continue, click the authentication link.
    </asp:Panel>
    <asp:Panel ID="CallbackPanel" runat="server" Visible="false">
        <br />
        Access Token:
        <asp:Label ID="AccessTokenLabel" runat="server" />
        <br />
        Access Secret:
        <asp:Label ID="AccessSecretLabel" runat="server" />
        <br />
        Screen Name:
        <asp:Label ID="ScreenNameLabel" runat="server" />
        <br />
        User ID:
        <asp:Label ID="UserIdLabel" runat="server" />
    </asp:Panel>
</asp:Content>
