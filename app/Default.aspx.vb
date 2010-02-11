
Partial Class app_Default
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim jsSettings As String = "SETTINGS = {ExorLiveAuthUrl:""" & ExorLiveConsumer.Settings.ExorLiveAuthUrl & """," & _
            "ExorLiveUrl: """ & ExorLiveConsumer.Settings.ExorLiveUrl & """};"
        ScriptManager.RegisterClientScriptBlock(Me, Me.GetType, "SETTINGS", jsSettings, True)
    End Sub
End Class
