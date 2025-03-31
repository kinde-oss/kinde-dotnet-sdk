# Kinde.Api.Model.ReplaceConnectionRequestOptionsOneOf
Azure AD connection options.

## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**ClientId** | **string** | Client ID. | [optional] 
**ClientSecret** | **string** | Client secret. | [optional] 
**HomeRealmDomains** | **List&lt;string&gt;** | List of domains to limit authentication. | [optional] 
**EntraIdDomain** | **string** | Domain for Entra ID. | [optional] 
**IsUseCommonEndpoint** | **bool** | Use https://login.windows.net/common instead of a default endpoint. | [optional] 
**IsSyncUserProfileOnLogin** | **bool** | Sync user profile data with IDP. | [optional] 
**IsRetrieveProviderUserGroups** | **bool** | Include user group info from MS Entra ID. | [optional] 
**IsExtendedAttributesRequired** | **bool** | Include additional user profile information. | [optional] 

[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)

