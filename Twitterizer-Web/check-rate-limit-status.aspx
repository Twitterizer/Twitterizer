<%@ Page Language="C#" AutoEventWireup="true" CodeFile="check-rate-limit-status.aspx.cs" Inherits="check_rate_limit_status" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <asp:Label ID="RemainingHits" runat="server" /> hits remaining.<br />
    <asp:Label ID="HourlyLimit" runat="server" /> hits per hour allowed.<br />
    Counts will reset at <asp:Label ID="ResetTimeString" runat="server" />.<br />
    </div>
    </form>
</body>
</html>
