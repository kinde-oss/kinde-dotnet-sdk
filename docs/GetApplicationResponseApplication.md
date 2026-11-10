# Kinde.Api.Model.GetApplicationResponseApplication

## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**Id** | **string** | The application&#39;s identifier. | [optional] 
**Name** | **string** | The application&#39;s name. | [optional] 
**Type** | **string** | The application&#39;s type. | [optional] 
**ClientId** | **string** | The application&#39;s client ID. | [optional] 
**ClientSecret** | **string** | The application&#39;s client secret. | [optional] 
**LoginUri** | **string** | The default login route for resolving session issues. | [optional] 
**HomepageUri** | **string** | The homepage link to your application. | [optional] 
**HasCancelButton** | **bool** | Whether the application has a cancel button to allow users to exit the auth flow [Beta]. | [optional] 
**IsAccessControlEnabled** | **bool** | Whether role-based access control is enforced for the application. Always false for application types that do not support connections.  | [optional] 
**IsAllowFacelessAuth** | **bool** | Bypass Kinde&#39;s sign up and sign in screens and use your own design. | [optional] 
**IsAskForName** | **bool** | Show fields to collect name details from users signing up with email or phone. | [optional] 
**HasMarketingConsent** | **bool** | Show a marketing consent checkbox on the sign-up page. | [optional] 
**HasSignInLinkOnSignUpPage** | **bool** | Allow users to switch to the login page from the sign-up page. | [optional] 
**HasRegisterLinkOnSignInPage** | **bool** | Allow users to switch to the register page from the sign-in page. | [optional] 
**HasSignInWithSsoButton** | **bool** | When home realm discovery is configured, users see a button to prompt them to use their work email. | [optional] 
**UseGravatarFallback** | **bool** | Use a backup image if a profile picture is not available. | [optional] 

[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)

