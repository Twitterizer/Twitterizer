<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Lists.aspx.cs" Inherits="Lists"
    MasterPageFile="~/MasterPage.master" %>
<%@ MasterType TypeName="MasterPage" %>

<asp:Content runat="server" ContentPlaceHolderID="PageBodyContentPlaceHolder">
    <asp:GridView ID="ListGridView" runat="server" DataSource='<%# ListCollection %>' EnableViewState="false" />
    <asp:LinkButton ID="NextPageLinkButton" runat="server" Text="Next Page" OnClick="NextPageLinkButton_Click" EnableViewState="false" />
</asp:Content>
