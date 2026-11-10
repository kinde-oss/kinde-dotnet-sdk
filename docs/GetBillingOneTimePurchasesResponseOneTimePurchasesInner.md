# Kinde.Api.Model.GetBillingOneTimePurchasesResponseOneTimePurchasesInner

## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**Id** | **string** | The friendly id of the one-time purchase | [optional] 
**AgreementId** | **string** | The friendly id of the billing agreement the purchase belongs to | [optional] 
**FeatureCode** | **string** | The feature code of the purchased one-time feature | [optional] 
**FeatureName** | **string** | The feature name of the purchased one-time feature | [optional] 
**UnitsGranted** | **int?** | The number of units granted by this purchase. Null when the purchase grants unlimited usage.  | [optional] 
**Amount** | **decimal** | The amount paid for this purchase, in major currency units | [optional] 
**CurrencyCode** | **string** | The ISO currency code of the amount paid | [optional] 
**PurchasedOn** | **DateTimeOffset** | When the purchase was paid | [optional] 
**ExpiresOn** | **DateTimeOffset?** | When the purchase expires. Null when the purchase never expires. | [optional] 
**ExpirationPolicyCode** | **string** | The expiration policy captured at purchase time | [optional] 
**IsMetered** | **bool** | Whether usage is reported against this purchased feature | [optional] 

[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)

