# Kinde.Api.Model.GetOrganizationResponse

## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**Code** | **string** | The unique identifier for the organization. | [optional] 
**Name** | **string** | The organization&#39;s name. | [optional] 
**Handle** | **string** | A unique handle for the organization - can be used for dynamic callback urls. | [optional] 
**IsDefault** | **bool** | Whether the organization is the default organization. | [optional] 
**ExternalId** | **string** | The organization&#39;s external identifier - commonly used when migrating from or mapping to other systems. | [optional] 
**IsAutoMembershipEnabled** | **bool** | If users become members of this organization when the org code is supplied during authentication. | [optional] 
**Logo** | **string** |  | [optional] 
**LinkColor** | [**GetOrganizationResponseLinkColor**](GetOrganizationResponseLinkColor.md) |  | [optional] 
**BackgroundColor** | [**GetOrganizationResponseBackgroundColor**](GetOrganizationResponseBackgroundColor.md) |  | [optional] 
**ButtonColor** | [**GetOrganizationResponseLinkColor**](GetOrganizationResponseLinkColor.md) |  | [optional] 
**ButtonTextColor** | [**GetOrganizationResponseBackgroundColor**](GetOrganizationResponseBackgroundColor.md) |  | [optional] 
**LinkColorDark** | [**GetOrganizationResponseLinkColor**](GetOrganizationResponseLinkColor.md) |  | [optional] 
**BackgroundColorDark** | [**GetOrganizationResponseLinkColor**](GetOrganizationResponseLinkColor.md) |  | [optional] 
**ButtonTextColorDark** | [**GetOrganizationResponseLinkColor**](GetOrganizationResponseLinkColor.md) |  | [optional] 
**ButtonColorDark** | [**GetOrganizationResponseLinkColor**](GetOrganizationResponseLinkColor.md) |  | [optional] 
**IsAllowRegistrations** | **bool?** | Deprecated - Use &#39;is_auto_membership_enabled&#39; instead | [optional] 

[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)

