
Partial Class openid_user
    Inherits System.Web.UI.Page

    Protected Sub IdentityEndpoint20_NormalizeUri(ByVal sender As Object, ByVal e As DotNetOpenAuth.OpenId.Provider.IdentityEndpointNormalizationEventArgs) Handles IdentityEndpoint20.NormalizeUri
        Dim username As String = OpenID.Util.ExtractUserName(Page.Request.Url)
        e.NormalizedIdentifier = New Uri(OpenID.Util.BuildIdentityUrl(username))
    End Sub
End Class
