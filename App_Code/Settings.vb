Imports Microsoft.VisualBasic

Public Class Settings
    Private _oauthEndpoint As String
    Public ReadOnly Property OAuthEndpoint() As String
        Get
            Return _oauthEndpoint
        End Get
    End Property

    Private _serviceEndpointBase As String
    Public ReadOnly Property ServiceEndpointBase() As String
        Get
            Return _serviceEndpointBase
        End Get
    End Property

    Private _exorliveAuthUrl As String
    Public ReadOnly Property ExorLiveAuthUrl() As String
        Get
            Return _exorliveAuthUrl
        End Get
    End Property

    Private _exorliveUrl As String
    Public ReadOnly Property ExorLiveUrl() As String
        Get
            Return _exorliveUrl
        End Get
    End Property

    Public Sub New(ByVal environment As String)
        Select Case environment
            Case "integration"
                _oauthEndpoint = "http://auth.int.exorlive.com/providers/oauth/Default.ashx"
                _serviceEndpointBase = "http://api.int.exorlive.com/"
                _exorliveAuthUrl = "http://auth.int.exorlive.com"
                _exorliveUrl = "http://int.exorlive.com"
            Case "testing"
                _oauthEndpoint = "http://auth.test.exorlive.com/providers/oauth/Default.ashx"
                _serviceEndpointBase = "http://api.test.exorlive.com/"
                _exorliveAuthUrl = "http://auth.test.exorlive.com"
                _exorliveUrl = "http://test.exorlive.com"
            Case "production"
                _oauthEndpoint = "https://auth.exorlive.com/providers/oauth/Default.ashx"
                _serviceEndpointBase = "https://api.exorlive.com/"
                _exorliveAuthUrl = "https://auth.exorlive.com"
                _exorliveUrl = "https://exorlive.com"

            Case Else
                _oauthEndpoint = "http://localhost:50006/providers/oauth/Default.ashx"
                _serviceEndpointBase = "http://localhost:50002/"
                _exorliveAuthUrl = "http://localhost:50006"
                _exorliveUrl = "http://localhost:50000"
        End Select
    End Sub
End Class
