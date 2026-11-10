# Kinde.Api.Model.CreateMeterUsageRecordRequest

## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**CustomerAgreementId** | **string** | The billing agreement against which to record usage | 
**BillingFeatureCode** | **string** | The code of the feature within the agreement against which to record usage | 
**MeterValue** | **int** | The value of usage to record. Must be greater than zero. For absolute this becomes the new running total. For delta this is added to the last calculated running total.  | 
**MeterUsageTimestamp** | **DateTimeOffset** | When this event happened. Defaults to the current date and time. Stored on the record and, for plan-metered entitlements, sent to the billing provider. Not used to choose a billing cycle, to order this record against earlier ones, or to calculate the running total. A timestamp in the past still replaces (absolute) or adds to (delta) the last calculated total.  | [optional] 
**MeterTypeCode** | **string** | How this record updates the running total. &#x60;absolute&#x60; replaces the last calculated total with &#x60;meter_value&#x60;. &#x60;delta&#x60; adds &#x60;meter_value&#x60; to the last calculated total. The caller is responsible for sending the correct type. See the operation description for which total is updated (current billing cycle vs never-expires lifetime).  | [optional] [default to MeterTypeCodeEnum.Absolute]

[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)

