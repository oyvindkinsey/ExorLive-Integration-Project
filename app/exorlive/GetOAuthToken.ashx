<%@ WebHandler Language="VB" Class="GetOAuthToken"  %>
Imports System
Imports System.Web
Imports DotNetOpenAuth.OAuth
Imports DotNetOpenAuth.OAuth.Messages

Public Class GetOAuthToken : Implements IHttpHandler
    
    Public Sub ProcessRequest(ByVal context As HttpContext) Implements IHttpHandler.ProcessRequest
        Using consumer As WebConsumer = Util.CreateConsumer()
            Dim accessTokenMessage As authorizedtokenresponse = consumer.ProcessUserAuthorization()
            If accessTokenMessage Is Nothing Then
                'We have no valid OAuth token for this user, lets request one
                Dim callback As UriBuilder = New UriBuilder(context.Request.Url)
                callback.Query = Nothing
                Dim requestParams = New Dictionary(Of String, String)
                'Add the methods that we wish to be able to execute on behalf of the user 
                requestParams.Add("scope", "http://api.exorlive.com/UserService/Authenticate" & _
                                  "|" & "http://api.exorlive.com/UserService/CreateContact")
                consumer.Channel.Send(consumer.PrepareRequestUserAuthorization(callback.Uri, requestParams, Nothing))
            Else
                'Save the token
                Database.Instance.GetUser(context.User.Identity.Name).ExorLiveOAuthToken = accessTokenMessage.AccessToken
                Database.Instance.Commit()
                'Notify the UI that the token has been retrieved
                context.Response.Write("<!DOCTYPE html><html></head>")
                context.Response.Write("<script type=""text/javascript"">window.opener.app.NotifyGotOAuthToken();</script>")
                context.Response.Write("</head><body></body></html>")
            End If
        End Using
    End Sub
 
    Public ReadOnly Property IsReusable() As Boolean Implements IHttpHandler.IsReusable
        Get
            Return True
        End Get
    End Property

End Class