# Kinde.Api.Model.UpdateUserRequest

## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**GivenName** | **string** | User&#39;s first name. If the user is the owner of a family billing customer, this update is also propagated to the corresponding billing customer details.  | [optional] 
**FamilyName** | **string** | User&#39;s last name. If the user is the owner of a family billing customer, this update is also propagated to the corresponding billing customer details.  | [optional] 
**Picture** | **string** | The user&#39;s profile picture. | [optional] 
**IsSuspended** | **bool** | Whether the user is currently suspended or not. | [optional] 
**IsPasswordResetRequested** | **bool** | Prompt the user to change their password on next sign in. | [optional] 
**ProvidedId** | **string** | An external id to reference the user. | [optional] 

[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)

