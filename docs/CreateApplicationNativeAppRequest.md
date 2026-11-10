# Kinde.Api.Model.CreateApplicationNativeAppRequest

## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**Platform** | **string** |  | 
**PackageName** | **string** | Android only. The application id. | [optional] 
**Sha256CertFingerprints** | **List&lt;string&gt;** | Android only. 1 to 10 SHA-256 signing-certificate fingerprints, usually the upload key and the Play App Signing key. Colons between bytes are optional; fingerprints are stored and returned uppercase and colon-separated. | [optional] 
**TeamId** | **string** | iOS only. The 10-character Apple Team ID. | [optional] 
**BundleId** | **string** | iOS only. The bundle identifier. | [optional] 

[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)

