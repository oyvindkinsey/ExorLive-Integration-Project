<%@ Page Language="VB" AutoEventWireup="false" CodeFile="setup.aspx.vb" Inherits="setup" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Setup Application</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        To be able to use this test project you will have to register at <a href="http://partner.int.exorlive.com">
            partner.int.exorlive.com</a> portal and create an application.<br />
        When you are ready for production you will have to do the same at <a href="http://partner.exorlive.com">
            partner.exorlive.com</a>.<br />
        If you intend to run the example on any other domain than localhost, then the domain
        you register need to match this.<br />
        You will to input the consumer key and the consumer secret in the form below and
        reset the application.<br />
        <br />
        Consumer Key<br />
        <asp:TextBox ID="txtConsumerKey" runat="server" Width="300" /><br />
        Consumer Secret<br />
        <asp:TextBox ID="txtConsumerSecret" runat="server" Width="300" /><br />
        <asp:Button ID="btnSave" Text="Save settings and reset application" runat="server" />
    </div>
    </form>
</body>
</html>
