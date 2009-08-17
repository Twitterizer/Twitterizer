<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Twitterizer_Web_Test._Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Panel ID="LoginPanel" runat="server">
            <asp:Label ID="ValidationLabel" ForeColor="Red" Font-Bold="true" runat="server" />
            Username:<asp:TextBox ID="UsernameTextBox" runat="server"></asp:TextBox>
            <br />
            Password:<asp:TextBox ID="PasswordTextBox" runat="server"></asp:TextBox>
            <br />
            <asp:Button ID="ValidateLoginButton" runat="server" OnClick="ValidateLoginButton_Click"
                Text="Validate" />
            &nbsp;<asp:Button ID="SaveLoginButton" runat="server" OnClick="SaveLoginButton_Click"
                Text="Save" />
        </asp:Panel>
        <asp:Panel ID="ActionPanel" runat="server">
            <asp:TextBox ID="StatusUpdateTextbox" runat="server" Columns="80" MaxLength="140"></asp:TextBox>
            <asp:Button ID="UpdateStatusButton" runat="server" Text="Update" />
        </asp:Panel>
    </div>
    </form>
</body>
</html>
