
Partial Class setup
    Inherits System.Web.UI.Page

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Dim config As String = "<appSettings>" & _
                         "<add key=""ExorLiveOAuthConsumerKey"" value=""" & txtConsumerKey.Text & """/>" & _
                         "<add key=""ExorLiveOAuthConsumerSecret"" value=""" & txtConsumerSecret.Text & """/>" & _
                     "</appSettings>"
        My.Computer.FileSystem.WriteAllText(Server.MapPath("~/appSettings.config"), config, False)
        FormsAuthentication.SignOut()
        Database.CommitAndCloseIfOpen()
        Dim dbPath As String = Server.MapPath("~/App_Data/db.dbs")
        If IO.File.Exists(dbPath) Then IO.File.Delete(dbPath)

        System.Web.HttpRuntime.UnloadAppDomain()
        Response.Redirect("~/")

    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            txtConsumerKey.Text = Util.ConsumerKey
            txtConsumerSecret.Text = Util.ConsumerSecret
        End If
    End Sub
End Class
