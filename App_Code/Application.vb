Imports Microsoft.VisualBasic

Public Class ExorLiveConsumer
    Inherits System.Web.HttpApplication
    Private Shared _appDataPath As String
    Public Shared ReadOnly Property AppDataPath() As String
        Get
            Return _appDataPath
        End Get
    End Property

    Sub Application_Start(ByVal sender As Object, ByVal e As EventArgs)
        ' Code that runs on application startup
        _appDataPath = HttpContext.Current.Server.MapPath("~/App_Data")
    End Sub

    Sub Application_End(ByVal sender As Object, ByVal e As EventArgs)
        ' Code that runs on application shutdown
        Database.CommitAndCloseIfOpen()
    End Sub

End Class
