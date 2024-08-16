# Kinde.Api.Model.SetUserPasswordRequest

## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**HashedPassword** | **string** | The hashed password. | 
**HashingMethod** | **string** | The hashing method or algorithm used to encrypt the user’s password. Default is bcrypt. | [optional] 
**Salt** | **string** | Extra characters added to passwords to make them stronger. Not required for bcrypt. | [optional] 
**SaltPosition** | **string** | Position of salt in password string. Not required for bcrypt. | [optional] 
**IsTemporaryPassword** | **bool** | The user will be prompted to set a new password after entering this one. | [optional] 

[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)

