# Kinde.Api.Model.GetBillingMeterUsageResponse

## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**Code** | **string** | Response code. | [optional] 
**Message** | **string** | Response message. | [optional] 
**HasMore** | **bool** | Whether more records exist. | [optional] 
**CustomerAgreementId** | **string** | The billing agreement this page is scoped to. | [optional] 
**FeatureCode** | **string** | The feature this page is scoped to. | [optional] 
**UnitsGranted** | **int** | Units currently granted for this agreement and feature. For one-time features this is the sum of current paid purchases. For plan-metered entitlements this is the entitlement or plan limit. Omitted when there is no current grant, and when the grant is unlimited (any current one-time purchase has a null unit grant, or the entitlement has no numeric cap). An unlimited one-time grant still returns expiration_policy_code. Not limited to the current page.  | [optional] 
**ExpirationPolicyCode** | **string** | Expiration policy of the latest current one-time purchase of this feature. Omitted for plan-metered entitlements and when there is no current one-time grant.  | [optional] 
**CurrentUsage** | **int** | Usage against the current grant. For end_of_current_billing_cycle one-time features and plan-metered entitlements this is the current billing cycle running total (resets each cycle). For none it is the ingest-order lifetime total for this agreement and feature, including records reported before the first paid purchase. Absolute replaces that total; delta adds to it. Not limited to the current page. Omitted when there is no current grant.  | [optional] 
**MeterUsages** | [**List&lt;GetBillingMeterUsageResponseMeterUsagesInner&gt;**](GetBillingMeterUsageResponseMeterUsagesInner.md) | A list of meter usage records | [optional] 

[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)

