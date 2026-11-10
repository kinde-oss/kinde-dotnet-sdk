# Kinde.Api.Model.DeleteBillingAgreement200Response

## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**Message** | **string** |  | [optional] 
**Code** | **string** | &#x60;AGREEMENT_DELETED&#x60; for an immediate cancellation, &#x60;AGREEMENT_CANCELLATION_SCHEDULED&#x60; for a scheduled one. | [optional] 
**CancelsOn** | **DateTimeOffset?** | When a scheduled cancellation takes effect. Absent for an immediate cancellation. | [optional] 

[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)

