# Kinde.Api.Model.CreateOrganizationRequest

## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**Name** | **string** | The organization&#39;s name. | 
**FeatureFlags** | **Dictionary&lt;string, CreateOrganizationRequest.InnerEnum&gt;** | The organization&#39;s feature flag settings. | [optional] 
**ExternalId** | **string** | The organization&#39;s external identifier - commonly used when migrating from or mapping to other systems. | [optional] 
**BackgroundColor** | **string** | The organization&#39;s brand settings - background color. | [optional] 
**ButtonColor** | **string** | The organization&#39;s brand settings - button color. | [optional] 
**ButtonTextColor** | **string** | The organization&#39;s brand settings - button text color. | [optional] 
**LinkColor** | **string** | The organization&#39;s brand settings - link color. | [optional] 
**BackgroundColorDark** | **string** | The organization&#39;s brand settings - dark mode background color. | [optional] 
**ButtonColorDark** | **string** | The organization&#39;s brand settings - dark mode button color. | [optional] 
**ButtonTextColorDark** | **string** | The organization&#39;s brand settings - dark mode button text color. | [optional] 
**LinkColorDark** | **string** | The organization&#39;s brand settings - dark mode link color. | [optional] 
**ThemeCode** | **string** | The organization&#39;s brand settings - theme/mode &#39;light&#39; | &#39;dark&#39; | &#39;user_preference&#39;. | [optional] 
**Handle** | **string** | A unique handle for the organization - can be used for dynamic callback urls. | [optional] 
**IsAllowRegistrations** | **bool** | If users become members of this organization when the org code is supplied during authentication. | [optional] 
**SenderName** | **string** | The name of the organization that will be used in emails | [optional] 
**SenderEmail** | **string** | The email address that will be used in emails. Requires custom SMTP to be set up. | [optional] 

[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)

