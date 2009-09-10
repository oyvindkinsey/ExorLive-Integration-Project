Imports Microsoft.VisualBasic
Imports Perst
Imports Entities

Namespace Persistence
    Public Class OAuthTokenManager
        Implements DotNetOpenAuth.OAuth.ChannelElements.IConsumerTokenManager
        Private Shared _instance As OAuthTokenManager
        Public Shared ReadOnly Property Instance() As OAuthTokenManager
            Get
                If _instance Is Nothing Then _instance = New OAuthTokenManager
                Return _instance
            End Get
        End Property

        Public ReadOnly Property ConsumerKey() As String Implements DotNetOpenAuth.OAuth.ChannelElements.IConsumerTokenManager.ConsumerKey
            Get
                Return Util.ConsumerKey
            End Get
        End Property

        Public ReadOnly Property ConsumerSecret() As String Implements DotNetOpenAuth.OAuth.ChannelElements.IConsumerTokenManager.ConsumerSecret
            Get
                Return Util.ConsumerSecret
            End Get
        End Property

        Public Sub ExpireRequestTokenAndStoreNewAccessToken(ByVal consumerKey As String, ByVal requestToken As String, ByVal accessToken As String, ByVal accessTokenSecret As String) Implements DotNetOpenAuth.OAuth.ChannelElements.ITokenManager.ExpireRequestTokenAndStoreNewAccessToken
            Dim token = Database.Instance.GetToken(requestToken)
            Database.Instance.RemoveToken(token)
            token.Token = accessToken
            token.TokenSecret = accessTokenSecret
            token.TokenType = DotNetOpenAuth.OAuth.ChannelElements.TokenType.AccessToken
            Database.Instance.SetToken(token)
            Database.Instance.Commit()
        End Sub

        Public Function GetTokenSecret(ByVal token As String) As String Implements DotNetOpenAuth.OAuth.ChannelElements.ITokenManager.GetTokenSecret
            Return Database.Instance.GetToken(token).TokenSecret
        End Function

        Public Function GetTokenType(ByVal token As String) As DotNetOpenAuth.OAuth.ChannelElements.TokenType Implements DotNetOpenAuth.OAuth.ChannelElements.ITokenManager.GetTokenType
            Return Database.Instance.GetToken(token).TokenType
        End Function

        Public Function IsRequestTokenAuthorized(ByVal requestToken As String) As Boolean Implements DotNetOpenAuth.OAuth.ChannelElements.ITokenManager.IsRequestTokenAuthorized
            Throw New NotImplementedException()
        End Function

        Public Sub StoreNewRequestToken(ByVal request As DotNetOpenAuth.OAuth.Messages.UnauthorizedTokenRequest, ByVal response As DotNetOpenAuth.OAuth.Messages.ITokenSecretContainingMessage) Implements DotNetOpenAuth.OAuth.ChannelElements.ITokenManager.StoreNewRequestToken
            Dim token As New OAuthToken() With {.UserName = HttpContext.Current.User.Identity.Name, _
            .Token = response.Token, _
            .TokenSecret = response.TokenSecret, _
            .TokenType = DotNetOpenAuth.OAuth.ChannelElements.TokenType.RequestToken}
            Database.Instance.SetToken(token)
            Database.Instance.Commit()
        End Sub
    End Class
End Namespace