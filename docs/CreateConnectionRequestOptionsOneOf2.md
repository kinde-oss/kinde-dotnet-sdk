# Kinde.Api.Model.CreateConnectionRequestOptionsOneOf2
SAML connection options (e.g., Cloudflare SAML).

## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**HomeRealmDomains** | **List&lt;string&gt;** | List of domains to restrict authentication. | [optional] 
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
**IsAutoJoinOrganizationEnabled** | **bool** | Users automatically join organization when using this connection. | [optional] 

[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)

