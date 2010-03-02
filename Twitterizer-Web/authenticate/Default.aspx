<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        Your access request token is:
        <asp:Label ID="RequestTokenLabel" runat="server" /><br />
        <asp:HyperLink ID="GetAccessHyperLink" runat="server" Text="Click Here" />
        to authorize twitterizer.
    </div>
    </form>
</body>
</html>
