Imports System.ServiceModel.Activation

' NOTE: If you change the class name "Service" here, you must also update the reference to "Service" in Web.config and in the associated .svc file.

<AspNetCompatibilityRequirements(RequirementsMode:=AspNetCompatibilityRequirementsMode.Allowed)> _
Public Class Service
    Implements IService

    Public Function GetExorLiveAuthKey() As Entities.ExorLiveAuthKeyResponse Implements IService.GetExorLiveAuthKey
        Dim response As New Entities.ExorLiveAuthKeyResponse
        Dim localUser As Entities.User = Database.Instance.GetUser(HttpContext.Current.User.Identity.Name)
        If localUser.ExorLiveId = 0 AndAlso String.IsNullOrEmpty(localUser.ExorLiveOAuthToken) Then
            response.Status = Entities.ExorLiveAuthKeyResponse.StatusType.NoExorLiveAccount
        ElseIf String.IsNullOrEmpty(localUser.ExorLiveOAuthToken) Then
            response.Status = Entities.ExorLiveAuthKeyResponse.StatusType.NoOAuthToken
        Else
            Try
                response.AuthKey = Util.GetAuthentication(localUser.ExorLiveOAuthToken)
            Catch ex As Exception
                response.Status = Entities.ExorLiveAuthKeyResponse.StatusType.InvalidOAuthToken
            End Try
        End If
        Return response
    End Function

    Public Function GetExorLiveId(ByVal localId As Integer) As Entities.GetExorLiveIdResponse Implements IService.GetExorLiveId
        Dim response As New Entities.GetExorLiveIdResponse
        Dim localClient As Entities.Client = Database.Instance.GetClient(localId)
        If localClient.ExorLiveId <> 0 Then
            response.ExorLiveId = localClient.ExorLiveId
            Return response
        End If

        Dim _exorliveContact As New ExorLive.UserService.User() With { _
        .Firstname = localClient.FullName, _
        .Lastname = localClient.FullName, _
        .Email = localClient.EMailAddress}
        Dim token As String = Database.Instance.GetUser(HttpContext.Current.User.Identity.Name).ExorLiveOAuthToken
        Try
            _exorliveContact = Util.CallUserService(token, Function(client) client.CreateContact(Nothing, _exorliveContact))
            If _exorliveContact.ErrorCode = ExorLive.UserService.User.UserErrorCode.EmailTaken Then
                response.Status = Entities.GetExorLiveIdResponse.StatusType.EmailTaken
                response.ErrorMessage = response.Status.ToString()
                Return response
            End If
            If _exorliveContact.ErrorCode <> ExorLive.UserService.User.UserErrorCode.NoError Then
                response.Status = Entities.GetExorLiveIdResponse.StatusType.UnknownError
                response.ErrorMessage = _exorliveContact.ErrorCode.ToString()
                Return response
            End If
        Catch ex As Exception
            response.Status = Entities.CreateExorLiveAccountResponse.StatusType.Error
            response.ErrorMessage = ex.Message
        End Try
        localClient.ExorLiveId = _exorliveContact.Id
        response.ExorLiveId = _exorliveContact.Id
        Database.Instance.Commit()

        Return response
    End Function
    Public Function CreateExorLiveAccount() As Entities.CreateExorLiveAccountResponse Implements IService.CreateExorLiveAccount
        Dim response As New Entities.CreateExorLiveAccountResponse
        Dim localUser As Entities.User = Database.Instance.GetUser(HttpContext.Current.User.Identity.Name)
        Dim exorLiveUser As New ExorLive.PartnerService.User() With { _
            .Firstname = localUser.FullName, _
            .Lastname = localUser.FullName, _
            .Email = localUser.EMailAddress}
        Using client As New ExorLive.PartnerService.PartnerServiceClient
            Try
                exorLiveUser = client.CreateOrganization(New ExorLive.PartnerService.Credentials() With _
                                                       {.Identification = Util.ConsumerKey, .Secret = Util.ConsumerSecret}, _
                                                       exorLiveUser, OpenID.Util.BuildIdentityUrl(localUser.Username).ToString())
            Catch ex As Exception
                response.Status = Entities.CreateExorLiveAccountResponse.StatusType.Error
                response.ErrorMessage = ex.Message
            Finally
                client.Close()
            End Try
            If exorLiveUser.ErrorCode = ExorLive.PartnerService.User.UserErrorCode.NoError Then
                localUser.ExorLiveId = exorLiveUser.Id
                Database.Instance.Commit()
            Else
                response.Status = Entities.CreateExorLiveAccountResponse.StatusType.Error
                response.ErrorMessage = exorLiveUser.ErrorCode.ToString()
            End If
        End Using
        Return response
    End Function

    Public Function GetIdentityUrl() As String Implements IService.GetIdentityUrl
        Return OpenID.Util.BuildIdentityUrl(HttpContext.Current.User.Identity.Name)
    End Function

    Public Function ListClients() As System.Collections.Generic.List(Of Entities.Client) Implements IService.ListClients
        Return Database.Instance.ListClientsByOwner(HttpContext.Current.User.Identity.Name)
    End Function

End Class
