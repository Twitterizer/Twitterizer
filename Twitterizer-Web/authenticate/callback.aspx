<%@ Page Language="C#" AutoEventWireup="true" CodeFile="callback.aspx.cs" Inherits="callback" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    Access has been granted by: <asp:Label ID="ScreenNameLabel" runat="server" /> (User ID: <asp:Label ID="UserIdLabel" runat="server" /><br />
    
    Your access token is: <asp:Label ID="AccessTokenLabel" runat="server" /><br />
    Your access token secret is: <asp:Label ID="AccessTokenSecretLabel" runat="server" /><br />
    
    </div>
    </form>
</body>
</html>
