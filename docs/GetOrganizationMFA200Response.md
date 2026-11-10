# Kinde.Api.Model.GetOrganizationMFA200Response

## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**Message** | **string** |  | [optional] 
**Code** | **string** |  | [optional] 
**IsMfaEnforced** | **bool** | Whether sign-in requires organization MFA. True when the organization has advanced organizations enabled and at least one MFA factor. | [optional] 
**EnabledFactors** | **List&lt;GetOrganizationMFA200Response.EnabledFactorsEnum&gt;** | The MFA methods enabled for this organization. | [optional] 
**IsRecoveryCodesEnabled** | **bool** | Whether recovery codes are shown to users during MFA setup for this organization. | [optional] 
**ExemptEnterpriseConnections** | **List&lt;string&gt;** | IDs of the enterprise connections whose users are not required to use MFA for this organization. | [optional] 

[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)

