# Kinde.Api.Model.VerifyApiKeyResponse

## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**Code** | **string** | Response code. | [optional] 
**Message** | **string** | Response message. | [optional] 
**IsValid** | **bool** | Whether the API key is valid. | [optional] 
**KeyId** | **string** | The unique ID for the API key. | [optional] 
**Status** | **string** | The status of the API key. | [optional] 
**Scopes** | **List&lt;string&gt;** | Array of scopes associated with this key. | [optional] 
**OrgCode** | **string** | The organization code associated with this key. | [optional] 
**UserId** | **string** | The user ID associated with this key. | [optional] 
**LastVerifiedOn** | **DateTimeOffset?** | When the API key was last verified. | [optional] 
**VerificationCount** | **int** | Number of times this API key has been verified. | [optional] 

[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)

