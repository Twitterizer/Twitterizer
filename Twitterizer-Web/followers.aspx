<%@ Page Language="C#" AutoEventWireup="true" CodeFile="followers.aspx.cs" Inherits="followers"
    MasterPageFile="~/MasterPage.master" %>

<%@ MasterType TypeName="MasterPage" %>
<asp:Content runat="server" ContentPlaceHolderID="PageBodyContentPlaceHolder">
    <asp:GridView runat="server" ID="FollowersGridView" DataSource='<%# FollowersCollection %>'
        AutoGenerateColumns="false" DataKeyNames="Id" 
        OnRowDataBound="FollowersGridView_RowDataBound" 
        onrowcommand="FollowersGridView_RowCommand">
        <Columns>
            <asp:TemplateField>
                <ItemTemplate>
                    <img src="<%# Eval("ProfileImageLocation") %>" style="max-width: 100px;" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:ButtonField DataTextField="ScreenName" HeaderText="User" CommandName="DrillDownUser" />
            <asp:BoundField DataField="Name" HeaderText="Real Name" />
            <asp:TemplateField HeaderText="Last Update">
                <ItemTemplate>
                    <%# Eval("Status.Text") %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="NumberOfFriends" HeaderText="Friends" DataFormatString="{0:#,###}" />
            <asp:BoundField DataField="NumberOfFollowers" HeaderText="Followers" DataFormatString="{0:#,###}" />
            <asp:TemplateField HeaderText="Are you following them?">
                <ItemTemplate>
                    <asp:Label runat="server" EnableViewState="false" Text='<%# SafeBooleanText((bool?)Eval("IsFollowing")) %>' />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
    <asp:LinkButton runat="server" ID="NextPageLinkButton" Text="Next Page" OnClick="NextPageLinkButton_Click" />
</asp:Content>
