<%@ Page Language="VB" AutoEventWireup="false" CodeFile="user.aspx.vb" Inherits="openid_user" %>

<%@ Register Assembly="DotNetOpenAuth" Namespace="DotNetOpenAuth.OpenId.Provider"
    TagPrefix="openid" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
    <openid:IdentityEndpoint ID="IdentityEndpoint20" runat="server" ProviderEndpointUrl="~/openid/Server.aspx"
        XrdsUrl="~/openid/user_xrds.aspx" ProviderVersion="V20" AutoNormalizeRequest="true"
        OnNormalizeUri="IdentityEndpoint20_NormalizeUri" />
    <!-- and for backward compatibility with OpenID 1.x RPs... -->
    <openid:IdentityEndpoint ID="IdentityEndpoint11" runat="server" ProviderEndpointUrl="~/openid/Server.aspx"
        ProviderVersion="V11" />
    <div>
    </div>
    </form>
</body>
</html>
