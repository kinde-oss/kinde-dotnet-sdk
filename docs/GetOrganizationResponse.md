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
**Logo** | **string** | The organization&#39;s logo URL. | [optional] 
**LogoDark** | **string** | The organization&#39;s logo URL to be used for dark themes. | [optional] 
**FaviconSvg** | **string** | The organization&#39;s SVG favicon URL. Optimal format for most browsers | [optional] 
**FaviconFallback** | **string** | The favicon URL to be used as a fallback in browsers that don’t support SVG, add a PNG | [optional] 
**LinkColor** | [**GetEnvironmentResponseEnvironmentLinkColor**](GetEnvironmentResponseEnvironmentLinkColor.md) |  | [optional] 
**BackgroundColor** | [**GetEnvironmentResponseEnvironmentBackgroundColor**](GetEnvironmentResponseEnvironmentBackgroundColor.md) |  | [optional] 
**ButtonColor** | [**GetEnvironmentResponseEnvironmentLinkColor**](GetEnvironmentResponseEnvironmentLinkColor.md) |  | [optional] 
**ButtonTextColor** | [**GetEnvironmentResponseEnvironmentBackgroundColor**](GetEnvironmentResponseEnvironmentBackgroundColor.md) |  | [optional] 
**LinkColorDark** | [**GetEnvironmentResponseEnvironmentLinkColor**](GetEnvironmentResponseEnvironmentLinkColor.md) |  | [optional] 
**BackgroundColorDark** | [**GetEnvironmentResponseEnvironmentLinkColor**](GetEnvironmentResponseEnvironmentLinkColor.md) |  | [optional] 
**ButtonTextColorDark** | [**GetEnvironmentResponseEnvironmentLinkColor**](GetEnvironmentResponseEnvironmentLinkColor.md) |  | [optional] 
**ButtonColorDark** | [**GetEnvironmentResponseEnvironmentLinkColor**](GetEnvironmentResponseEnvironmentLinkColor.md) |  | [optional] 
**ButtonBorderRadius** | **int?** | The border radius for buttons. Value is px, Kinde transforms to rem for rendering | [optional] 
**CardBorderRadius** | **int?** | The border radius for cards. Value is px, Kinde transforms to rem for rendering | [optional] 
**InputBorderRadius** | **int?** | The border radius for inputs. Value is px, Kinde transforms to rem for rendering | [optional] 
**ThemeCode** | **string** | Whether the environment is forced into light mode, dark mode or user preference | [optional] 
**ColorScheme** | **string** | The color scheme for the environment used for meta tags based on the theme code | [optional] 
**CreatedOn** | **string** | Date of organization creation in ISO 8601 format. | [optional] 
**IsAllowRegistrations** | **bool?** | Deprecated - Use &#39;is_auto_membership_enabled&#39; instead | [optional] 

[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)

