# Kinde.Api.Model.GetApiKeysResponseApiKeysInner

## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**Id** | **string** | The unique ID for the API key. | [optional] 
**Name** | **string** | The API key&#39;s name. | [optional] 
**Type** | **string** | The type of API key. | [optional] 
**Status** | **string** | The status of the API key. | [optional] 
**KeyPrefix** | **string** | The first 6 characters of the API key for identification. | [optional] 
**KeySuffix** | **string** | The last 4 characters of the API key for identification. | [optional] 
**CreatedOn** | **DateTimeOffset** | When the API key was created. | [optional] 
**LastVerifiedOn** | **DateTimeOffset?** | When the API key was last verified. | [optional] 
**LastVerifiedIp** | **string** | The IP address from which the API key was last verified. | [optional] 
**CreatedBy** | **string** | The name of the user who created the API key. | [optional] 
**ApiIds** | **List&lt;string&gt;** | Array of API IDs associated with this key. | [optional] 
**Scopes** | **List&lt;string&gt;** | Array of scopes associated with this key. | [optional] 

[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)

