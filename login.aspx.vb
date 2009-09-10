
Partial Class login
    Inherits System.Web.UI.Page

    Protected Sub btnSignIn_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSignIn.Click
        Dim user As Entities.User = Database.Instance.GetUser(txtUsername.Text)
        If user.Password = txtPassword.Text Then
            FormsAuthentication.RedirectFromLoginPage(user.Username, False)
        End If
    End Sub
End Class
