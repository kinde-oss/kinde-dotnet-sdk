# Kinde.Api.Model.UpdateConnectionRequestOptions
Connection options for the strategy. For SAML and Microsoft Entra ID (OAuth 2.0) strategies, an option not listed for the strategy is rejected with INVALID_OPTIONS.

## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**ClientId** | **string** | Client ID. | [optional] 
**ClientSecret** | **string** | Client secret. | [optional] 
**IsUseCustomDomain** | **bool** | Use custom domain callback URL. | [optional] 
**IsTrusted** | **bool** | Deprecated. Use trust_mode instead. true maps to trust_mode trust, false to none. Ignored when trust_mode is also sent. On update, a stored mode is kept unless this flag contradicts it (false clears trust, true sets trust). Echoing false does not clear validate. | [optional] 
**TrustMode** | **string** | How email addresses from this connection are trusted for account merging. none never merges on email alone; validate sends a one-time code to the address before it merges with an existing user, and before it creates a new user when is_strict_mode is false; trust merges immediately. | [optional] 
**IsPasskeyAllowed** | **bool** | Allow passkeys for users of this connection. When true, users are offered passkey setup after signing in with this connection (required when the passkey policy is mandatory), and a passkey sign-in is not sent to this connection by home realm for a user who has signed in with this connection before (for a connection an organization owns, also only for its members); anyone else signs in through the connection once. A passkey sign-in does not contact the identity provider, so disabling a user there does not stop it; remove the user&#39;s passkeys in Kinde as well. When omitted, the current value is kept. | [optional] 
**HomeRealmDomains** | **List&lt;string&gt;** | List of domains to restrict authentication. | [optional] 
**EntraIdDomain** | **string** | Domain for Entra ID. | [optional] 
**IsUseCommonEndpoint** | **bool** | Use https://login.windows.net/common instead of a default endpoint. | [optional] 
**IsSyncUserProfileOnLogin** | **bool** | Sync user profile data with IDP. | [optional] 
**IsRetrieveProviderUserGroups** | **bool** | Include user group info from MS Entra ID. | [optional] 
**IsExtendedAttributesRequired** | **bool** | Include additional user profile information. | [optional] 
**IsCreateMissingUser** | **bool** | Create user if they don&#39;t exist. | [optional] 
**IsForceShowSsoButton** | **bool** | Deprecated - Use &#39;sso_button_display&#39; instead. True maps to \&quot;show\&quot;, false maps to \&quot;auto\&quot;. Ignored when sso_button_display is also sent. | [optional] 
**SsoButtonDisplay** | **string** | Controls when the SSO sign-in button is shown for this connection. Replaces is_force_show_sso_button. \&quot;auto\&quot; shows the button unless a home realm domain is set, \&quot;show\&quot; always shows it, \&quot;hide\&quot; never shows it. Takes precedence over is_force_show_sso_button when both are sent. | [optional] 
**UpstreamParams** | **Dictionary&lt;string, Object&gt;** | Additional upstream parameters to pass to the identity provider. | [optional] 
**IsAutoJoinOrganizationEnabled** | **bool** | Users automatically join the organization that owns this connection when they sign in through it. Stored on any enterprise connection, but only takes effect once the connection has an owning organization. | [optional] 
**SamlEntityId** | **string** | SAML Entity ID. | [optional] 
**SamlIdpMetadataUrl** | **string** | URL for the IdP metadata. Optional if saml_idp_metadata_xml is provided. | [optional] 
**SamlIdpMetadataXml** | **string** | Raw IdP metadata XML. Use when the IdP does not host a metadata URL (e.g. Google Workspace). Takes precedence over saml_idp_metadata_url when both are set. | [optional] 
**SamlSignInUrl** | **string** | Override the default SSO endpoint with a URL your IdP recognizes. | [optional] 
**SignRequestAlgorithm** | **string** | Algorithm used to sign SAML requests. | [optional] 
**ProtocolBinding** | **string** | Protocol binding used to send SAML requests. | [optional] 
**NameIdFormat** | **string** | Format for the Name ID used to identify users in SAML responses. | [optional] 
**SamlEmailKeyAttr** | **string** | Attribute key for the user&#39;s email. | [optional] 
**SamlUserIdKeyAttr** | **string** | Attribute key for the user&#39;s ID. | [optional] 
**SamlFirstNameKeyAttr** | **string** | Attribute key for the user&#39;s first name. | [optional] 
**SamlLastNameKeyAttr** | **string** | Attribute key for the user&#39;s last name. | [optional] 
**SamlSigningCertificate** | **string** | Certificate for signing SAML requests. | [optional] 
**SamlSigningPrivateKey** | **string** | Private key associated with the signing certificate. | [optional] 
**IsEnableIdpInitiatedSso** | **bool** | Accept unsolicited sign-in requests sent to Kinde by the IdP (IdP-initiated SSO). When enabled, any sign-in request that passes this connection&#39;s connection_id also skips the Kinde sign-in screen and goes straight to the IdP. When omitted, the current value is kept. | [optional] 
**IsEnableLoginHint** | **bool** | Send the user&#39;s email address to the IdP as login_hint, so the IdP can skip account selection when the user is signed in to more than one account. On a sign-in that starts at Kinde, this is the email address the application sent as login_hint, and it is not sent when the user signs in with a different email address. On an IdP-initiated sign-in, it is the email address from the SAML response. It applies whether or not IdP-initiated SSO is enabled for the connection. When omitted, the current value is kept. | [optional] 

[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)

