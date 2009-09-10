
Partial Class _Default
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If String.IsNullOrEmpty(Util.ConsumerKey) Then Response.Redirect("~/setup.aspx")
    End Sub
End Class
