# Kinde.Api.Model.GetOrganizationInviteResponse

## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**Message** | **string** | Response message. | [optional] 
**Id** | **string** | The invitation&#39;s unique identifier. | [optional] 
**Code** | **string** | The invitation&#39;s code. | [optional] 
**Email** | **string** | The email address of the invited user. | [optional] 
**FirstName** | **string** | The first name of the invited user. | [optional] 
**LastName** | **string** | The last name of the invited user. | [optional] 
**FullName** | **string** | The full name of the invited user. | [optional] 
**CreatedOn** | **DateTimeOffset** | When the invitation was created. | [optional] 
**IsSent** | **bool** | Whether the invitation email was sent. | [optional] 
**AcceptedOn** | **DateTimeOffset?** | When the invitation was accepted. | [optional] 
**Roles** | [**List&lt;GetOrganizationInviteResponseRolesInner&gt;**](GetOrganizationInviteResponseRolesInner.md) | The roles assigned to the invitation. | [optional] 
**IsRevoked** | **bool** | Whether the invitation has been revoked. | [optional] 
**InviteLink** | **string** | URL to share with the invitee to accept the invitation. | [optional] 

[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)

