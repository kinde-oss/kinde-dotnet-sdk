# Kinde.Api.Model.UpdateConnectionRequest

## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**Name** | **string** | The internal name of the connection. | [optional] 
**DisplayName** | **string** | The public facing name of the connection. | [optional] 
**EnabledApplications** | **List&lt;string&gt;** | Client IDs of applications in which this connection is to be enabled. | [optional] 
**OrganizationCode** | **string** | Enterprise connections only - the code for the organization that manages this connection. Omit to leave ownership unchanged, or send an empty string to clear it. | [optional] 
**Options** | [**UpdateConnectionRequestOptions**](UpdateConnectionRequestOptions.md) |  | [optional] 

[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)

