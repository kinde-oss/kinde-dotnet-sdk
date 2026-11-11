# Kinde.Api.Model.OrganizationRole
A role an organization owns. It can only be assigned to members of that organization.

## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**Id** | **string** | The role&#39;s ID. | [optional] 
**Key** | **string** | The role identifier to use in code. Unique within the organization, and never the same as a business-wide role&#39;s key. | [optional] 
**Name** | **string** | The role&#39;s name. | [optional] 
**Description** | **string** | The role&#39;s description. | [optional] 
**Permissions** | [**List&lt;OrganizationRolePermissionsInner&gt;**](OrganizationRolePermissionsInner.md) | The permissions the role grants. | [optional] 
**CreatedOn** | **DateTimeOffset** | When the role was created. | [optional] 

[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)

