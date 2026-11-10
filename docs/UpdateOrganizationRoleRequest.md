# Kinde.Api.Model.UpdateOrganizationRoleRequest

## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**Name** | **string** | The role&#39;s name. | [optional] 
**Description** | **string** | The role&#39;s description. | [optional] 
**Permissions** | **List&lt;string&gt;** | The IDs of the permissions the role grants, replacing its current set. Omit to leave them unchanged. Only business-wide permissions marked &#x60;is_organization_role_assignable&#x60;, and the organization&#39;s own permissions, can be used. | [optional] 

[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)

