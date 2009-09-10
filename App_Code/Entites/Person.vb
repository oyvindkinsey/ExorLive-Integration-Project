Imports Microsoft.VisualBasic
Namespace Entities
    Public Class Person

        Private _emailAddress As String
        Public Property EMailAddress() As String
            Get
                Return _emailAddress
            End Get
            Set(ByVal value As String)
                _emailAddress = value
            End Set
        End Property

        Private _fullName As String
        Public Property FullName() As String
            Get
                Return _fullName
            End Get
            Set(ByVal value As String)
                _fullName = value
            End Set
        End Property


        Private _exorLiveId As Integer
        Public Property ExorLiveId() As Integer
            Get
                Return _exorLiveId
            End Get
            Set(ByVal value As Integer)
                _exorLiveId = value
            End Set
        End Property
    End Class
End Namespace
