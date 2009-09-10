Imports Microsoft.VisualBasic
Namespace Entities
    Public Class User
        Inherits Person

        Private _username As String
        Public Property Username() As String
            Get
                Return _username
            End Get
            Set(ByVal value As String)
                _username = value
            End Set
        End Property

        Private _password As String
        Public Property Password() As String
            Get
                Return _password
            End Get
            Set(ByVal value As String)
                _password = value
            End Set
        End Property

        Private _exorLiveOAuthToken As String
        Public Property ExorLiveOAuthToken() As String
            Get
                Return _exorLiveOAuthToken
            End Get
            Set(ByVal value As String)
                _exorLiveOAuthToken = value
            End Set
        End Property
    End Class
End Namespace