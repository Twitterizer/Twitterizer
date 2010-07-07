<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="user.aspx.cs" Inherits="user" %>

<%@ MasterType TypeName="MasterPage" %>
<asp:Content ContentPlaceHolderID="PageBodyContentPlaceHolder" runat="Server">
    <asp:DetailsView ID="UserDetailsView" runat="server"
        AutoGenerateRows="false" Width="100%">
        <Fields>
            <asp:BoundField DataField="Id" HeaderText="User Id" />
            <asp:ImageField DataImageUrlField="ProfileImageLocation" />
            <asp:HyperLinkField DataNavigateUrlFields="ScreenName" DataNavigateUrlFormatString="http://www.twitter.com/{0}"
                DataTextField="ScreenName" HeaderText="Screen Name" />
            <asp:BoundField DataField="Name" HeaderText="Real Name" />
            <asp:HyperLinkField DataNavigateUrlFields="Website" DataTextField="Website" HeaderText="Website" />
            <asp:BoundField DataField="Description" HeaderText="Description" />
            <asp:BoundField DataField="CreatedDate" DataFormatString="{0:D}" HeaderText="Date Joined" />
            <asp:BoundField DataField="NumberOfStatuses" DataFormatString="{0:#,###}" HeaderText="Number of Tweets" />
            <asp:BoundField DataField="NumberOfFriends" DataFormatString="{0:#,###}" HeaderText="Number of Friends" />
            <asp:BoundField DataField="NumberOfFollowers" DataFormatString="{0:#,###}" HeaderText="Number of Followers" />
            <asp:CheckBoxField DataField="IsFollowing" HeaderText="Are you following them?" />
            <asp:TemplateField HeaderText="Last Tweet" ItemStyle-Wrap="true">
                <ItemTemplate>
                    <%# Eval("Status.Text") %></ItemTemplate>
            </asp:TemplateField>
            <asp:HyperLinkField DataNavigateUrlFields="Id" DataNavigateUrlFormatString="~/friends.aspx?userid={0}"
                Text="View Friends" />
            <asp:HyperLinkField DataNavigateUrlFields="Id" DataNavigateUrlFormatString="~/followers.aspx?userid={0}"
                Text="View Followers" />
        </Fields>
    </asp:DetailsView>
</asp:Content>
