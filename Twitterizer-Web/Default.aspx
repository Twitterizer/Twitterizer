<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default"
    MasterPageFile="~/MasterPage.master" %>

<%@ MasterType TypeName="MasterPage" %>
<asp:Content runat="server" ContentPlaceHolderID="PageBodyContentPlaceHolder">
    <asp:GridView ID="myGridView" runat="server" DataSource='<%# HomePageStatuses %>'
        EnableViewState="false" AutoGenerateColumns="false">
        <Columns>
            <asp:TemplateField HeaderText="From">
                <ItemTemplate>
                    <asp:HyperLink runat="server" NavigateUrl='<%# "~/user.aspx?id=" + Eval("User.Id") %>' Text='<%# Eval("User.Name") %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="CreatedDate" HeaderText="Date" DataFormatString="{0:g}" />
            <asp:TemplateField HeaderText="Text">
                <ItemTemplate>
                    <%# LinkifyText((string)Eval("Text")) %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="Source" HeaderText="Source" HtmlEncode="false" />
            <asp:BoundField DataField="InReplyToScreenName" HeaderText="Reply To User" />
        </Columns>
    </asp:GridView>
    <asp:LinkButton runat="server" ID="NextPageLinkButton" Text="Next Page" OnClick="NextPageLinkButton_Click" />
</asp:Content>
