# Kinde.Api.Model.CreateConnectionRequestOptions

## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**ClientId** | **string** | Client ID. | [optional] 
**ClientSecret** | **string** | Client secret. | [optional] 
**IsUseCustomDomain** | **bool** | Use custom domain callback URL. | [optional] 
**HomeRealmDomains** | **List&lt;string&gt;** | List of domains to restrict authentication. | [optional] 
**EntraIdDomain** | **string** | Domain for Entra ID. | [optional] 
**IsUseCommonEndpoint** | **bool** | Use https://login.windows.net/common instead of a default endpoint. | [optional] 
**IsSyncUserProfileOnLogin** | **bool** | Sync user profile data with IDP. | [optional] 
**IsRetrieveProviderUserGroups** | **bool** | Include user group info from MS Entra ID. | [optional] 
**IsExtendedAttributesRequired** | **bool** | Include additional user profile information. | [optional] 
**IsAutoJoinOrganizationEnabled** | **bool** | Users automatically join organization when using this connection. | [optional] 
**SamlEntityId** | **string** | SAML Entity ID. | [optional] 
**SamlAcsUrl** | **string** | Assertion Consumer Service URL. | [optional] 
**SamlIdpMetadataUrl** | **string** | URL for the IdP metadata. | [optional] 
**SamlSignInUrl** | **string** | Override the default SSO endpoint with a URL your IdP recognizes. | [optional] 
**SamlEmailKeyAttr** | **string** | Attribute key for the user’s email. | [optional] 
**SamlFirstNameKeyAttr** | **string** | Attribute key for the user’s first name. | [optional] 
**SamlLastNameKeyAttr** | **string** | Attribute key for the user’s last name. | [optional] 
**IsCreateMissingUser** | **bool** | Create user if they don’t exist. | [optional] 
**SamlSigningCertificate** | **string** | Certificate for signing SAML requests. | [optional] 
**SamlSigningPrivateKey** | **string** | Private key associated with the signing certificate. | [optional] 

[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)

