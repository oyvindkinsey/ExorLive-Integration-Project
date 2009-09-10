﻿Imports Microsoft.VisualBasic
Namespace Entities
    Public Class GetExorLiveIdResponse

        Private _exorLiveId As Integer
        Public Property ExorLiveId() As Integer
            Get
                Return _exorLiveId
            End Get
            Set(ByVal value As Integer)
                _exorLiveId = value
            End Set
        End Property

        Private _errorMessage As String
        Public Property ErrorMessage() As String
            Get
                Return _errorMessage
            End Get
            Set(ByVal value As String)
                _errorMessage = value
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
            EmailTaken = 1
            UnknownError = 2
        End Enum
    End Class
End Namespace
