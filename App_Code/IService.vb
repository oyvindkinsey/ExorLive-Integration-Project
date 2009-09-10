Imports System.ServiceModel

' NOTE: If you change the class name "IService" here, you must also update the reference to "IService" in Web.config.
<ServiceContract(Namespace:="ExorLiveConsumer", Name:="Service")> _
Public Interface IService

    <OperationContract()> _
     Function GetExorLiveAuthKey() As Entities.ExorLiveAuthKeyResponse

    <OperationContract()> _
    Function CreateExorLiveAccount() As Entities.CreateExorLiveAccountResponse

    <OperationContract()> _
    Function GetIdentityUrl() As String

    <OperationContract()> _
    Function ListClients() As List(Of Entities.Client)

    <OperationContract()> _
    Function GetExorLiveId(ByVal localId As Integer) As Entities.GetExorLiveIdResponse

End Interface
