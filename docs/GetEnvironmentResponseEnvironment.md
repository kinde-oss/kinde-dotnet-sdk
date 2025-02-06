# Kinde.Api.Model.GetEnvironmentResponseEnvironment

## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**Code** | **string** | The unique identifier for the environment. | [optional] 
**Name** | **string** | The environment&#39;s name. | [optional] 
**HotjarSiteId** | **string** | Your HotJar site ID. | [optional] 
**GoogleAnalyticsTag** | **string** | Your Google Analytics tag. | [optional] 
**IsDefault** | **bool** | Whether the environment is the default. Typically this is your production environment. | [optional] 
**IsLive** | **bool** | Whether the environment is live. | [optional] 
**KindeDomain** | **string** | Your domain on Kinde | [optional] 
**CustomDomain** | **string** | Your custom domain for the environment | [optional] 
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
**CreatedOn** | **string** | Date of environment creation in ISO 8601 format. | [optional] 

[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)

