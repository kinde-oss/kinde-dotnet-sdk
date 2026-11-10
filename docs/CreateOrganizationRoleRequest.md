# Kinde.Api.Model.CreateOrganizationRoleRequest

## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**Name** | **string** | The role&#39;s name. | 
**Description** | **string** | The role&#39;s description. | [optional] 
**Key** | **string** | The role identifier to use in code. Unique within the organization, and never the same as a business-wide role&#39;s key. | 
**Permissions** | **List&lt;string&gt;** | The IDs of the permissions the role grants. Only business-wide permissions marked &#x60;is_organization_role_assignable&#x60;, and the organization&#39;s own permissions, can be used. | [optional] 

[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)

