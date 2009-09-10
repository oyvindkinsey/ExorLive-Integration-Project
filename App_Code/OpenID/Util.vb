Imports Microsoft.VisualBasic
Imports DotNetOpenAuth.OpenId

Namespace OpenID
    Public Class Util

        Public Shared Function BuildIdentityUrl(ByVal username As String) As Identifier
            username = username.Substring(0, 1).ToUpperInvariant() + username.Substring(1).ToLowerInvariant()
            Return New Uri(HttpContext.Current.Request.Url, HttpContext.Current.Response.ApplyAppPathModifier("~/openid/user.aspx/" & username))
        End Function
        Public Shared Function ExtractUserName(ByVal url As Uri) As String
            Return url.Segments(url.Segments.Length - 1)
        End Function
        Public Shared Function ExtractUserName(ByVal identifier As Identifier) As String
            Return ExtractUserName(New Uri(identifier.ToString))
        End Function
    End Class
End Namespace
