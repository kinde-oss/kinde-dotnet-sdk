# Kinde.Api.Model.CreateMeterUsageRecordRequest

## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**CustomerAgreementId** | **string** | The billing agreement against which to record usage | 
**BillingFeatureCode** | **string** | The code of the feature within the agreement against which to record usage | 
**MeterValue** | **string** | The value of usage to record | 
**MeterUsageTimestamp** | **DateTime** | The date and time the usage needs to be recorded for (defaults to current date/time) | [optional] 
**MeterTypeCode** | **string** | Absolutes overrides the current usage | [optional] 

[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)

