# Kinde.Api.Model.GetBillingOneTimePurchasesResponse

## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**Code** | **string** | Response code. | [optional] 
**Message** | **string** | Response message. | [optional] 
**HasMore** | **bool** | Whether more records exist. | [optional] 
**OneTimePurchases** | [**List&lt;GetBillingOneTimePurchasesResponseOneTimePurchasesInner&gt;**](GetBillingOneTimePurchasesResponseOneTimePurchasesInner.md) | A list of current one-time purchases | [optional] 
**FeatureTotals** | [**List&lt;GetBillingOneTimePurchasesResponseFeatureTotalsInner&gt;**](GetBillingOneTimePurchasesResponseFeatureTotalsInner.md) | Per-feature totals for all current purchases matching the filters. Present only when feature_totals&#x3D;true.  | [optional] 

[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)

