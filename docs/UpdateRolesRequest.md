# Kinde.Api.Model.UpdateRolesRequest

## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**Name** | **string** | The role&#39;s name. | 
**Description** | **string** | The role&#39;s description. | [optional] 
**Key** | **string** | The role identifier to use in code. | 
**IsDefaultRole** | **bool** | Set role as default for new users. | [optional] 
**AssignmentPermissionId** | **Guid?** | The public ID of the permission required to assign this role to users. If null, no change to the assignment permission is made. If set to &#39;NO_PERMISSION_REQUIRED&#39;, no permission is required. | [optional] 

[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)

