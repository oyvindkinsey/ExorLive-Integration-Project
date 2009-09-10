Imports DotNetOpenAuth.OpenId

Partial Class openid_server
    Inherits System.Web.UI.Page

    Protected Sub oidEndpont_AuthenticationChallenge(ByVal sender As Object, ByVal e As DotNetOpenAuth.OpenId.Provider.AuthenticationChallengeEventArgs) Handles oidEndpont.AuthenticationChallenge


        Dim userOwningOpenIdUrl As String = OpenID.Util.ExtractUserName(e.Request.LocalIdentifier)
        ' NOTE: in a production provider site, you may want to only 
        ' respond affirmatively if the user has already authorized this consumer
        ' to know the answer.
        e.Request.IsAuthenticated = String.Equals(userOwningOpenIdUrl, HttpContext.Current.User.Identity.Name, StringComparison.InvariantCultureIgnoreCase)
    End Sub

End Class
