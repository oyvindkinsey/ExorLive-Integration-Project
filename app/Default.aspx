<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Default.aspx.vb" Inherits="app_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>ExorLive Consumer - Sample CRM</title>
    <link type="text/css" rel="Stylesheet" href="../resources/libs/ext-3.0.0/resources/css/ext-all.css" />
    <link type="text/css" rel="Stylesheet" href="../resources/libs/Ext.ux.grid.RowActions.css" />
    <link type="text/css" rel="Stylesheet" href="../resources/css/app.css" />

    <script type="text/javascript" src="../resources/libs/ext-3.0.0/adapter/ext/ext-base.js"></script>

    <script type="text/javascript" src="../resources/libs/ext-3.0.0/ext-all.js"></script>

    <script type="text/javascript" src="../resources/libs/Ext.ux.grid.RowActions.js"></script>

</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager runat="server">
        <Services>
            <asp:ServiceReference Path="Service.svc" />
        </Services>
    </asp:ScriptManager>
    </form>

    <script type="text/javascript">
        document.write(unescape('%3Cscript src="' + SETTINGS.ExorLiveUrl + '/api/client.js" type="text/javascript"%3E%3C/script%3E'));    
    </script>

    <script type="text/javascript" src="../resources/scripts/app.js"></script>

</body>
</html>
