<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="friends.aspx.cs" Inherits="friends" %>

<%@ MasterType TypeName="MasterPage" %>
<asp:Content ContentPlaceHolderID="PageBodyContentPlaceHolder" runat="Server">
    <asp:DataList ID="FriendsDataList" runat="server" EnableViewState="false" RepeatColumns="6"
        RepeatDirection="Horizontal" DataSource='<%# FriendsCollection %>'>
        <ItemStyle Height="90" HorizontalAlign=Center />
        <HeaderTemplate>
            Your Friends
        </HeaderTemplate>
        <ItemTemplate>
            <asp:HyperLink runat="server" NavigateUrl='<%# string.Format("~/user.aspx?id={0}", Eval("Id")) %>' CssClass="Title">
        <asp:Image runat="server" ImageUrl='<%# Eval("ProfileImageLocation") %>' CssClass="ProfilePictureSmall" /><br />
        <%# Eval("Name") %>
            </asp:HyperLink>
        </ItemTemplate>
    </asp:DataList>
    <asp:LinkButton runat="server" ID="NextPageLinkButton" Text="Next Page" OnClick="NextPageLinkButton_Click" />
</asp:Content>
