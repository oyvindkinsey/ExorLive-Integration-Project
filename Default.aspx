<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Default.aspx.vb" Inherits="_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>ExorLive Consumer - Sample CRM</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        This sample application demonstrates how a consumer application can successfully integrate with ExorLive.<br />
        The application is the reference design and shows how to
        <ul>
            <li>interface with the ExorLive web application</li>
            <li>create new accounts in ExorLive</li>
            <li>use OpenId to transparently sign users in to ExorLive</li>
            <li>use OAuth to call ExorLive webservices impersonating the ExorLive user<br />
             without requiring access to the users credentials</li>
        </ul>
        <a href="app/">Open application</a><br />
        <a href="setup.aspx">Setup/reset application</a>
    </div>
    </form>
</body>
</html>
