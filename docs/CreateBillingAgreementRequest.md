# Kinde.Api.Model.CreateBillingAgreementRequest

## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**CustomerId** | **string** | The ID of the billing customer to create a new agreement for | 
**PlanCode** | **string** | The code of the billing plan the new agreement will be based on. The plan can be in a different billing group from the customer&#39;s current agreement, but it must match the customer type (organization or user). | 
**PlanVersion** | **int?** | Published version number to assign. Omit it to assign the latest version. Posting the customer&#39;s current plan_code does not move them onto a newer version. Name a different published version to do that. Unknown versions, and versions that were never published, return PLAN_VERSION_NOT_FOUND.  | [optional] 
**IsInvoiceNow** | **bool** | Generate a final invoice for any un-invoiced metered usage. | [optional] 
**IsProrate** | **bool** | Generate a proration invoice item that credits remaining unused features. | [optional] 
**SkipTrial** | **bool** | When true, the new agreement starts paid even if the plan has a trial. Omit it, or send false, to apply the plan&#39;s trial. A repeat trial is not refused unless this is true. Self-serve portal signups are separate and do not start a second trial.  | [optional] 

[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)

