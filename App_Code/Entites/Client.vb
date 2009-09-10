Imports Microsoft.VisualBasic
Namespace Entities
    Public Class Client
        Inherits Person

        Private _id As Integer
        Public Property Id() As Integer
            Get
                Return _id
            End Get
            Set(ByVal value As Integer)
                _id = value
            End Set
        End Property


        Private _owner As String
        Public Property Owner() As String
            Get
                Return _owner
            End Get
            Set(ByVal value As String)
                _owner = value
            End Set
        End Property

    End Class

End Namespace