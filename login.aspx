<%@ Page Language="VB" AutoEventWireup="false" CodeFile="login.aspx.vb" Inherits="login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>ExorLive Consumer - Sample CRM</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        Username:<br />
        <asp:TextBox ID="txtUsername" runat="server" /><br />
        Password:<br />
        <asp:TextBox TextMode="Password" ID="txtPassword" runat="server" /><br />
        <asp:Button ID="btnSignIn" runat="server" Text="Sign in" /><br />
        <br />
        Users:
        <ul>
            <li>test1/test</li>
            <li>test2/test</li>
            <li>test3/test</li>
        </ul>
    </div>
    </form>
</body>
</html>
