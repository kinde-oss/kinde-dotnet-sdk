# Kinde.Api.Model.ReplaceConnectionRequestOptionsOneOf
Social connection options (e.g., Google SSO).

## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**ClientId** | **string** | OAuth client ID. | [optional] 
**ClientSecret** | **string** | OAuth client secret. | [optional] 
**IsUseCustomDomain** | **bool** | Use custom domain callback URL. | [optional] 
**IsTrusted** | **bool** | Deprecated. Use trust_mode instead. true maps to trust_mode trust, false to none. Ignored when trust_mode is also sent. On update, a stored mode is kept unless this flag contradicts it (false clears trust, true sets trust). Echoing false does not clear validate. | [optional] 
**TrustMode** | **string** | How email addresses from this connection are trusted for account merging. none never merges on email alone; validate sends a one-time code to the address on the first sign-in through the connection, and creates or merges the user only once it is verified; trust merges immediately. | [optional] 
**IsPasskeyAllowed** | **bool** | Allow passkeys for users of this connection. When true, users are offered passkey setup after signing in with this connection (required when the passkey policy is mandatory). Home realm never sends users to a social connection, so a passkey sign-in has nothing to skip here. When omitted, the current value is kept. | [optional] 

[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)

