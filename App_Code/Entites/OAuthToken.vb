Imports Microsoft.VisualBasic
Imports Perst
Imports DotNetOpenAuth.OAuth.ChannelElements
Namespace Entities
    Public Class OAuthToken
        Inherits Persistent


        Private _userName As String
        Public Property UserName() As String
            Get
                Return _userName
            End Get
            Set(ByVal value As String)
                _userName = value
            End Set
        End Property

        Private _token As String
        Public Property Token() As String
            Get
                Return _token
            End Get
            Set(ByVal value As String)
                _token = value
            End Set
        End Property

        Private _tokenSecret As String
        Public Property TokenSecret() As String
            Get
                Return _tokenSecret
            End Get
            Set(ByVal value As String)
                _tokenSecret = value
            End Set
        End Property


        Private _tokenType As TokenType
        Public Property TokenType() As TokenType
            Get
                Return _tokenType
            End Get
            Set(ByVal value As TokenType)
                _tokenType = value
            End Set
        End Property
    End Class
End Namespace