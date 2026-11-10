# Kinde.Api.Model.Identity

## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**Id** | **string** | The unique ID for the identity | [optional] 
**Type** | **string** | The type of identity | [optional] 
**IsConfirmed** | **bool?** | Whether the identity is confirmed. Null for identity types that do not record confirmation, such as username. | [optional] 
**CreatedOn** | **string** | Date of user creation in ISO 8601 format | [optional] 
**LastLoginOn** | **string** | Date of last login in ISO 8601 format. Null if the identity has never been used to sign in. | [optional] 
**TotalLogins** | **int** |  | [optional] 
**Name** | **string** | The value of the identity | [optional] 
**Email** | **string** | The associated email of the identity. Null for phone and username identities. | [optional] 
**ConnectionId** | **string** | The social or enterprise connection ID associated with the identity. Null for email, phone, username, and passkey identities. | [optional] 
**IsPrimary** | **bool?** | Whether the identity is the primary identity for the user | [optional] 

[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)

