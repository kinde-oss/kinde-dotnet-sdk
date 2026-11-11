# Kinde.Api.Model.ReplaceOrganizationMFARequest

## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**EnabledFactors** | **List&lt;ReplaceOrganizationMFARequest.EnabledFactorsEnum&gt;** | The MFA methods to enable. | 
**IsRecoveryCodesEnabled** | **bool** | Determines whether recovery codes are shown to users during MFA setup for this specific organization. This overrides the environment-level setting. Omit the field to keep the current setting. An organization that has never set it uses the environment-level setting. | [optional] 
**ExemptEnterpriseConnections** | **List&lt;string&gt;** | IDs of the enterprise connections whose users are not required to use MFA for this organization. The list replaces the saved exemptions, so include every connection to keep exempt. Send an empty array to remove all exemptions. Omit the field, or send null, to leave the saved exemptions unchanged. Requires the advanced organizations plan entitlement.  | [optional] 

[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)

