# Kinde.Api.Model.CreateDirectoryRequest

## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**OrgCode** | **string** | The organization code to create the SCIM directory for. | 
**DirectoryName** | **string** | An optional descriptive name for the SCIM directory. | [optional] 
**ProviderCode** | **string** | The SCIM provider code to use for this directory. When omitted, the provider is inferred from the organization enterprise authentication method. If the inferred (or explicit) provider is disabled or missing endpoint configuration, INVALID_PROVIDER is returned.  | [optional] 
**EnterpriseConnectionId** | **string** | The enterprise connection ID to associate with this directory for SCIM-provisioned users. Required when the organization has multiple enabled enterprise connections.  | [optional] 

[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)

