# Kinde.Api.Model.CreateUserIdentityRequest

## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**Value** | **string** | The identity value, which depends on the type.  - &#x60;email&#x60;: the email address. - &#x60;username&#x60;: the username. - &#x60;phone&#x60;: the phone number, with &#x60;phone_country_id&#x60;. - &#x60;social&#x60;: the user&#39;s ID with the social provider. - &#x60;enterprise&#x60;: an email-formatted value that is compared, ignoring case, with what the identity provider sends at sign-in. For Microsoft Entra ID OAuth 2.0 connections, use the user principal name (UPN), which can differ from the user&#39;s mailbox address. A connection owned by an organization also matches the email address Entra sends, for users who are already members of that organization. For Microsoft Entra ID WS-Federation connections, use the email address Entra sends for the user, or their UPN if it sends none. For other enterprise connections, use the email the user signs in with, or the email the identity provider returns.  | [optional] 
**Type** | **string** | The identity type | [optional] 
**PhoneCountryId** | **string** | The country code for the phone number, only required when identity type is &#39;phone&#39;. | [optional] 
**ConnectionId** | **string** | The social or enterprise connection ID, only required when identity type is &#39;social&#39; or &#39;enterprise&#39;. | [optional] 

[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)

