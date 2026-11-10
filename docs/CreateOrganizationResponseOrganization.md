# Kinde.Api.Model.CreateOrganizationResponseOrganization

## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**Code** | **string** | The organization&#39;s unique code. | [optional] 
**BillingCustomerId** | **string** | The billing customer id if the organization was created with the is_create_billing_customer as true | [optional] 
**AgreementId** | **string** | The billing agreement id if the organization was created with is_create_billing_customer as true | [optional] 
**PlanVersion** | **int?** | The published version number assigned with that agreement | [optional] 
**PlanCode** | **string** | The published version&#39;s code. This can differ from billing_plan_code on the request after a draft edit. | [optional] 
**PlanName** | **string** | The published version&#39;s name. A draft rename is not returned. | [optional] 

[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)

