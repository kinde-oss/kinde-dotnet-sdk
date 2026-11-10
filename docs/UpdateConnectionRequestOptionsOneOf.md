# Kinde.Api.Model.UpdateConnectionRequestOptionsOneOf
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
**IsCreateMissingUser** | **bool** | Create users if they don&#39;t exist in the system. | [optional] 
**IsForceShowSsoButton** | **bool** | Deprecated - Use &#39;sso_button_display&#39; instead. True maps to \&quot;show\&quot;, false maps to \&quot;auto\&quot;. Ignored when sso_button_display is also sent. | [optional] 
**SsoButtonDisplay** | **string** | Controls when the SSO sign-in button is shown for this connection. Replaces is_force_show_sso_button. \&quot;auto\&quot; shows the button unless a home realm domain is set, \&quot;show\&quot; always shows it, \&quot;hide\&quot; never shows it. Takes precedence over is_force_show_sso_button when both are sent. | [optional] 
**UpstreamParams** | **Dictionary&lt;string, Object&gt;** | Additional upstream parameters to pass to the identity provider. | [optional] 
**IsUseCustomDomain** | **bool** | Use custom domain callback URL. | [optional] 
**IsAutoJoinOrganizationEnabled** | **bool** | Users automatically join the organization that owns this connection when they sign in through it. Stored on any enterprise connection, but only takes effect once the connection has an owning organization. | [optional] 
**IsTrusted** | **bool** | Deprecated. Use trust_mode instead. true maps to trust_mode trust, false to none. Ignored when trust_mode is also sent. On update, a stored mode is kept unless this flag contradicts it (false clears trust, true sets trust). Echoing false does not clear validate. | [optional] 
**TrustMode** | **string** | How email addresses from this connection are trusted for account merging. none never merges on email alone; validate sends a one-time code to the address before it merges with an existing user, and before it creates a new user when is_strict_mode is false; trust merges immediately. | [optional] 
**IsPasskeyAllowed** | **bool** | Allow passkeys for users of this connection. When true, users are offered passkey setup after signing in with this connection (required when the passkey policy is mandatory), and a passkey sign-in is not sent to this connection by home realm for a user who has signed in with this connection before (for a connection an organization owns, also only for its members); anyone else signs in through the connection once. A passkey sign-in does not contact the identity provider, so disabling a user there does not stop it; remove the user&#39;s passkeys in Kinde as well. When omitted, the current value is kept. | [optional] 

[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)

