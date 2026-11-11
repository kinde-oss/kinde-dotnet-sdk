# Kinde.Api.Model.GetBillingAgreementsResponseAgreementsInner

## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**Id** | **string** | The friendly id of an agreement | [optional] 
**PlanCode** | **string** | The plan code the billing customer is subscribed to | [optional] 
**PlanVersion** | **int?** | Published version number on this agreement. Null when the agreement has no plan version.  | [optional] 
**PlanIsLatest** | **bool?** | Whether this agreement is on the latest published version of its plan. Null when the agreement has no plan version.  | [optional] 
**ExpiresOn** | **DateTimeOffset?** | The date the agreement expired (and was no longer active) | [optional] 
**BillingGroupId** | **string** | The friendly id of the billing group this agreement&#39;s plan is part of | [optional] 
**CurrentPeriodStart** | **DateTimeOffset?** | The start date of the agreement&#39;s current billing period. Null when the agreement has no current billing cycle.  | [optional] 
**CurrentPeriodEnd** | **DateTimeOffset?** | The end date of the agreement&#39;s current billing period (typically the next billing date). Null when the agreement has no current billing cycle; may be in the past if cycle roll or provider sync has lagged.  | [optional] 
**Entitlements** | [**List&lt;GetBillingAgreementsResponseAgreementsInnerEntitlementsInner&gt;**](GetBillingAgreementsResponseAgreementsInnerEntitlementsInner.md) | A list of billing entitlements that is part of this agreement. Empty when the agreement&#39;s plan has no features.  | [optional] 

[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)

