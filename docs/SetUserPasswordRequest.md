# Kinde.Api.Model.SetUserPasswordRequest

## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**HashedPassword** | **string** | The hashed password. For aspnet-identity-v2, provide the base64-encoded AspNetUsers.PasswordHash value as-is. | 
**HashingMethod** | **string** | The hashing method or algorithm used to encrypt the user’s password. Default is bcrypt. | [optional] 
**Salt** | **string** | Extra characters added to passwords to make them stronger. Not required for bcrypt. Required for pbkdf2; provide the base64-encoded salt. Required for firebase-scrypt; provide the base64-encoded per-user salt. Not used for aspnet-identity-v2 (the salt is embedded in the hash); do not provide salt, salt_position, iterations, or variant with aspnet-identity-v2. | [optional] 
**SaltPosition** | **string** | Position of salt in password string. Not required for bcrypt. | [optional] 
**Iterations** | **int** | The iteration count (factor) used to derive the hash. Optional for pbkdf2; when omitted, verification defaults to 24000 (the FusionAuth default factor). Rejected for firebase-scrypt. | [optional] 
**Variant** | **string** | The hashing variant. Required for pbkdf2 (e.g. salted-pbkdf2-hmac-sha256, salted-pbkdf2-hmac-sha256-512, salted-pbkdf2-hmac-sha512-512). Rejected for firebase-scrypt. | [optional] 
**SignerKey** | **string** | The base64-encoded signer key from the Firebase project&#39;s password hash parameters. Required for firebase-scrypt; rejected for other hashing methods. | [optional] 
**SaltSeparator** | **string** | The base64-encoded salt separator from the Firebase project&#39;s password hash parameters. Required for firebase-scrypt; rejected for other hashing methods. | [optional] 
**Rounds** | **int** | The scrypt rounds from the Firebase project&#39;s password hash parameters (1-16). Required for firebase-scrypt; rejected for other hashing methods. | [optional] 
**MemCost** | **int** | The scrypt memory cost from the Firebase project&#39;s password hash parameters (1-20). Required for firebase-scrypt; rejected for other hashing methods. | [optional] 
**IsTemporaryPassword** | **bool** | The user will be prompted to set a new password after entering this one. | [optional] 

[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)

