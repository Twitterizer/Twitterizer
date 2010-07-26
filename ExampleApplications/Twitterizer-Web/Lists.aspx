<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Lists.aspx.cs" Inherits="Lists"
    MasterPageFile="~/MasterPage.master" %>
<%@ MasterType TypeName="Twitterizer.ExampleWeb.MasterPage" %>

<asp:Content runat="server" ContentPlaceHolderID="PageBodyContentPlaceHolder">
    <h2>Your lists</h2>
    <asp:GridView runat="server" ID="YourListsGridView" />
    
    <h2>Lists you are subscibed to</h2>
    <asp:GridView ID="SubscribedListsGridView" runat="server" EnableViewState="false" />
</asp:Content>
