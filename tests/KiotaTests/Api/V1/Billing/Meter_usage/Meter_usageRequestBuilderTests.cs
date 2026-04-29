using System.Net; using FluentAssertions; using Kiota.Api.Models; using Xunit;
namespace KiotaTests.Api.V1.Billing.Meter_usage;
public class Meter_usageRequestBuilderTests
{
    [Fact]
    public async Task PostAsync_Returns200_RecordsUsage()
    {
        var (client, handler) = ApiClientFactory.Create(HttpStatusCode.OK, MockData.SuccessResponse);

        await client.Api.V1.Billing.Meter_usage.PostAsync(new CreateMeterUsageRecord_request
        {
            BillingFeatureCode = "CcdkvEXpbg6UY",  
            MeterValue = "5",              
            CustomerAgreementId = "org_001"      
        });

        handler.LastRequest!.Method.Should().Be(HttpMethod.Post);
        handler.LastRequest.RequestUri!.PathAndQuery.Should().Be("/api/v1/billing/meter_usage");
    }
}
