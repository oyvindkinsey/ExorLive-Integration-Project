Imports Perst
Public Class DatabaseRoot
    Inherits Perst.Persistent

    Private _tokenIndex As FieldIndex
    Private _userIndex As FieldIndex
    Private _clientIndex As FieldIndex
    Private _clientOwnerIndex As FieldIndex

    Public Sub New(ByVal db As Storage)
        MyBase.new(db)
        _tokenIndex = db.CreateFieldIndex(GetType(Entities.OAuthToken), "Token", True)
        _userIndex = db.CreateFieldIndex(GetType(Entities.User), "Username", True)
        _clientIndex = db.CreateFieldIndex(GetType(Entities.Client), "Id", True)
        _clientOwnerIndex = db.CreateFieldIndex(GetType(Entities.Client), "Owner", False)
    End Sub

    Public Sub New()
    End Sub

    Public ReadOnly Property TokenIndex() As FieldIndex
        Get
            Return _tokenIndex
        End Get
    End Property

    Public ReadOnly Property UserIndex() As FieldIndex
        Get
            Return _userIndex
        End Get
    End Property
    Public ReadOnly Property ClientIndex() As FieldIndex
        Get
            Return _clientIndex
        End Get
    End Property
    Public ReadOnly Property ClientOwnerIndex() As FieldIndex
        Get
            Return _clientOwnerIndex
        End Get
    End Property
End Class