# Kinde.Api.Model.GetBillingOneTimePurchasesResponseFeatureTotalsInner

## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**AgreementId** | **string** | The friendly id of the billing agreement the purchases belong to | [optional] 
**FeatureCode** | **string** | The feature code of the purchased one-time feature | [optional] 
**FeatureName** | **string** | The feature name of the purchased one-time feature | [optional] 
**UnitsGranted** | **int?** | The sum of units granted by current purchases of this feature. Null when any current purchase grants unlimited usage.  | [optional] 
**Amount** | **decimal** | The sum of amounts paid for current purchases of this feature, in major currency units | [optional] 
**CurrencyCode** | **string** | The ISO currency code of the aggregated amount | [optional] 
**PurchaseCount** | **int** | The number of current purchases included in this total | [optional] 
**LastPurchasedOn** | **DateTimeOffset** | When this feature was last purchased | [optional] 
**ExpiresOn** | **DateTimeOffset?** | Expiry from the latest current purchase of this feature. Null when that purchase never expires.  | [optional] 
**ExpirationPolicyCode** | **string** | The expiration policy from the latest current purchase of this feature | [optional] 
**IsMetered** | **bool** | Whether usage is reported against this purchased feature | [optional] 
**CurrentBillingCycleUsage** | **int** | The current billing cycle running total for this agreement and feature. Present only on metered features when the agreement has a current cycle. Same ingest-order absolute/delta rules as POST meter_usage, scoped to the current cycle. Resets when a new cycle starts.  | [optional] 
**UsageSinceFirstPurchased** | **int** | The ingest-order lifetime running total for this agreement and feature. Absolute replaces it; delta adds to it. Includes every accepted record on the meter, including those reported before the first paid purchase. Does not reset when the billing cycle rolls. Present only on metered features. Comparable to units_granted on this total. Same number as current_usage on GET/POST meter_usage when the latest current purchase never expires.  | [optional] 

[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)

