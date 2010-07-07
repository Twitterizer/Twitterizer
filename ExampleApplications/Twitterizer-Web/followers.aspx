<%@ Page Language="C#" AutoEventWireup="true" CodeFile="followers.aspx.cs" Inherits="followers"
    MasterPageFile="~/MasterPage.master" %>

<%@ MasterType TypeName="MasterPage" %>
<asp:Content runat="server" ContentPlaceHolderID="PageBodyContentPlaceHolder">
     <asp:DataList ID="FollowersDataList" runat="server" EnableViewState="false" RepeatColumns="6"
        RepeatDirection="Horizontal" DataSource='<%# FollowersCollection %>'>
        <ItemStyle Height="90" HorizontalAlign="Center" />
        <HeaderTemplate>
            Your Followers
        </HeaderTemplate>
        <ItemTemplate>
            <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%# string.Format("~/user.aspx?id={0}", Eval("Id")) %>' CssClass="Title">
        <asp:Image ID="Image1" runat="server" ImageUrl='<%# Eval("ProfileImageLocation") %>' CssClass="ProfilePictureSmall" /><br />
        <%# Eval("Name") %>
            </asp:HyperLink>
        </ItemTemplate>
    </asp:DataList>
    <asp:LinkButton runat="server" ID="NextPageLinkButton" Text="Next Page" OnClick="NextPageLinkButton_Click" />
</asp:Content>
