# Kinde.Api.Model.GetBillingMeterUsageResponseMeterUsagesInner

## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**Id** | **string** | The friendly id of the meter usage record | [optional] 
**MeterValue** | **int** | The value recorded on this usage row | [optional] 
**MeterTypeCode** | **string** | How this record updated the running total. &#x60;absolute&#x60; replaced it with meter_value. &#x60;delta&#x60; added meter_value to the last calculated total.  | [optional] 
**MeterUsageTimestamp** | **DateTimeOffset** | When this event happened, as supplied by the caller (or the server time if omitted). Stored only. Not used to calculate the running total.  | [optional] 
**CurrentCycleAbsoluteUsage** | **int?** | The current-billing-cycle running total after and including this record, using the same ingest-order absolute/delta rules as POST. Independent of the never-expires lifetime total.  | [optional] 

[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)

