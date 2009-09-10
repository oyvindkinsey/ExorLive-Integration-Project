Imports Microsoft.VisualBasic
Namespace Entities
    Public Class ExorLiveAuthKeyResponse

        Private _authKey As String
        Public Property AuthKey() As String
            Get
                Return _authKey
            End Get
            Set(ByVal value As String)
                _authKey = value
            End Set
        End Property

        Private _status As StatusType
        Public Property Status() As StatusType
            Get
                Return _status
            End Get
            Set(ByVal value As StatusType)
                _status = value
            End Set
        End Property

        Public Enum StatusType
            Ok = 0
            NoOAuthToken = 1
            InvalidOAuthToken = 2
            NoExorLiveAccount = 3
        End Enum
    End Class
End Namespace