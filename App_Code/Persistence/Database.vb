Imports Microsoft.VisualBasic
Imports Entities

Public Class Database
    Implements IDisposable
    Private Shared _instance As Database
    Private Const PagePoolSize As Integer = 32 * 1024 * 1024 ' database cache size
    Private _db As Perst.Storage
    Private _dbRoot As DatabaseRoot

    Public Sub New()
        _db = Perst.StorageFactory.Instance.CreateStorage()
        _db.Open(ExorLiveConsumer.AppDataPath & "/db.dbs")
        _dbRoot = CType(_db.Root, DatabaseRoot)

        If _dbRoot Is Nothing Then
            _dbRoot = New DatabaseRoot(_db)
            _db.Root = _dbRoot
            'Fill with default data
            Dim domain As String = Util.ConsumerKey
            If Not domain.Contains(".") Then domain = domain & ".com"
            Dim id As Integer
            For i As Integer = 1 To 3
                SetUser(New Entities.User() With {.Username = "test" & i, .Password = "test", .EMailAddress = "test" & i & "@" & domain, .FullName = "Test" & i & " Test"})
                For i2 As Integer = 1 To 3
                    id += 1
                    SetClient(New Client() With {.Id = id, .Owner = "test" & i, .FullName = "Client" & id, .EMailAddress = "client" & id & "@" & domain})
                Next
            Next
            _db.Commit()
        End If
    End Sub

    Public Shared ReadOnly Property Instance() As Database
        Get
            If _instance Is Nothing Then _instance = New Database
            Return _instance
        End Get
    End Property
    Public Sub Commit()
        _db.Commit()
    End Sub

    Public Function ListClientsByOwner(ByVal key As String) As List(Of Client)
        Dim list As New List(Of Client)
        For Each item In _dbRoot.ClientOwnerIndex.Get(key, key)
            list.Add(CType(item, Client))
        Next
        Return list
    End Function
    Public Function GetClient(ByVal key As Integer) As Client
        Return _dbRoot.ClientIndex(key)
    End Function
    Public Sub SetClient(ByVal entity As Client)
        _dbRoot.ClientIndex.Put(entity)
        _dbRoot.ClientOwnerIndex.Put(entity)
    End Sub
    Public Function GetUser(ByVal key As String) As User
        Return _dbRoot.UserIndex(key)
    End Function

    Public Sub SetUser(ByVal entity As User)
        _dbRoot.UserIndex.Put(entity)
    End Sub

    Public Sub RemoveToken(ByVal entity As OAuthToken)
        _dbRoot.TokenIndex.Remove(entity)
    End Sub

    Public Function GetToken(ByVal key As String) As OAuthToken
        Return _dbRoot.TokenIndex(key)
    End Function
    Public Sub SetToken(ByVal entity As OAuthToken)
        _dbRoot.TokenIndex.Put(entity)
    End Sub

    Public Shared Sub CommitAndCloseIfOpen()
        If _instance IsNot Nothing Then
            _instance.Dispose()
        End If
    End Sub
    Private disposedValue As Boolean = False        ' To detect redundant calls

    ' IDisposable
    Protected Overridable Sub Dispose(ByVal disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                If _db IsNot Nothing Then
                    _db.Commit()
                    _db.Close()
                End If
            End If

            ' TODO: free your own state (unmanaged objects).
            ' TODO: set large fields to null.
        End If
        Me.disposedValue = True
    End Sub

#Region " IDisposable Support "
    ' This code added by Visual Basic to correctly implement the disposable pattern.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region

End Class
