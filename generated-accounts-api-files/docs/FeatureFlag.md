# Kinde.Accounts.Model.FeatureFlag

## Properties

Name | Type | Description | Notes
--- | --- | --- | ---
**Id** | **string** | Unique identifier for the feature flag. | 
**Name** | **string** | Human-readable name of the feature flag. | 
**Key** | **string** | Stable key used to reference the flag in code. | 
**Type** | **string** | Data type of the flag (e.g., boolean, string, number, json). | 
**Value** | [**FeatureFlagValue**](FeatureFlagValue.md) | Current evaluated value of the flag for the request context. | 

[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)

