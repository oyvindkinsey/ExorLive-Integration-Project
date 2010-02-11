Imports Microsoft.VisualBasic
Imports DotNetOpenAuth.OAuth
Imports System.Net
Imports System.ServiceModel
Imports DotNetOpenAuth.Messaging
Imports System.ServiceModel.Channels
Imports DotNetOpenAuth.OAuth.ChannelElements

Public Class Util
    Private Shared _consumerKey As String = ConfigurationManager.AppSettings("ExorLiveOAuthConsumerKey")
    Public Shared ReadOnly Property ConsumerKey() As String
        Get
            Return _consumerKey
        End Get
    End Property

    Private Shared _consumerSecret As String = ConfigurationManager.AppSettings("ExorLiveOAuthConsumerSecret")
    Public Shared ReadOnly Property ConsumerSecret() As String
        Get
            Return _consumerSecret
        End Get
    End Property

    'Private Shared _endpoint As String = ConfigurationManager.AppSettings("ExorLiveOAuthEndpoint")

    Public Shared Function CreateConsumer() As WebConsumer

        Dim tokenManager = Persistence.OAuthTokenManager.Instance
        Dim oauthEndpoint As MessageReceivingEndpoint = _
            New MessageReceivingEndpoint(New Uri(ExorLiveConsumer.Settings.OAuthEndpoint), HttpDeliveryMethods.PostRequest)
        Dim consumer As WebConsumer = New WebConsumer( _
            New ServiceProviderDescription With _
                {.RequestTokenEndpoint = oauthEndpoint, _
                .UserAuthorizationEndpoint = oauthEndpoint, _
                .AccessTokenEndpoint = oauthEndpoint, _
                .TamperProtectionElements = _
                    New DotNetOpenAuth.Messaging.ITamperProtectionChannelBindingElement() _
                        {New PlaintextSigningBindingElement(), New HmacSha1SigningBindingElement()} _
                }, _
            tokenManager)

        Return consumer
    End Function

    Public Shared Function GetAuthentication(ByVal token As String) As String
        Dim authKeyResponse = CallUserService(token, Function(client) client.Authenticate(Nothing))
        Dim localUser As Entities.User = Database.Instance.GetUser(HttpContext.Current.User.Identity.Name)
        If localUser.ExorLiveId <> authKeyResponse.Value.UserId Then
            localUser.ExorLiveId = authKeyResponse.Value.UserId
            Database.Instance.Commit()
        End If
        Return authKeyResponse.Value.AuthKeyString
    End Function

    Public Shared Function CallUserService(Of T)(ByVal token As String, ByVal predicate As Func(Of WSUserService.UserServiceClient, T)) As T
        If String.IsNullOrEmpty(token) Then
            Throw New InvalidOperationException("No access token!")
        End If

        Dim endpointAddress As EndpointAddress = New EndpointAddress(New Uri(ExorLiveConsumer.Settings.ServiceEndpointBase & "services/UserService2.svc"))
        Dim binding As Binding = New BasicHttpBinding(If(endpointAddress.Uri.Scheme = Uri.UriSchemeHttps, BasicHttpSecurityMode.Transport, BasicHttpSecurityMode.None))
        
        Dim client As New WSUserService.UserServiceClient(binding, endpointAddress)

        Dim serviceEndpoint = New MessageReceivingEndpoint(client.Endpoint.Address.Uri, HttpDeliveryMethods.AuthorizationHeaderRequest Or HttpDeliveryMethods.PostRequest)

        Dim consumer As WebConsumer = CreateConsumer()
        Dim httpRequest As WebRequest = consumer.PrepareAuthorizedRequest(serviceEndpoint, token)

        Dim httpDetails As New HttpRequestMessageProperty()
        httpDetails.Headers(HttpRequestHeader.Authorization) = httpRequest.Headers(HttpRequestHeader.Authorization)
        Using scope As New OperationContextScope(client.InnerChannel)
            OperationContext.Current.OutgoingMessageProperties(HttpRequestMessageProperty.Name) = httpDetails
            Return predicate(client)
        End Using
    End Function

End Class