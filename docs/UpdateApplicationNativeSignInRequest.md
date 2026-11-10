# Kinde.Api.Model.UpdateApplicationNativeSignInRequest

## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**IsEnabled** | **bool** |  | [optional] 
**PasskeyRequirement** | **string** | Which passkeys native sign-in accepts. &#x60;device_bound&#x60; refuses synced passkeys (Google Password Manager, iCloud Keychain). Browser sign-in is unaffected. | [optional] 
**IsPasskeyMfaExempt** | **bool** | Skip MFA for native sign-ins with a passkey the user unlocked with biometrics or a PIN, including MFA an organization requires. Presence-only passkeys still go through MFA, a pre-MFA workflow can still require it, and browser sign-in is unaffected. Sending it, whatever the value, also requires the &#x60;update:mfa&#x60; and &#x60;update:organization_mfa&#x60; scopes; without them the request is refused with &#x60;SCOPE_MISSING&#x60;. | [optional] 

[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)

