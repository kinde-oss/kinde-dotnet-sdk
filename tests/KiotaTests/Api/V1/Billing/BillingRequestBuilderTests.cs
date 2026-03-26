using System.Net; using FluentAssertions; using Xunit;
namespace KiotaTests.Api.V1.Billing;
public class BillingRequestBuilderTests
{
    [Fact] public void BillingRequestBuilder_ExposesSubResources()
    {
        var (client, _) = ApiClientFactory.Create(HttpStatusCode.OK, MockData.SuccessResponse);
        client.Api.V1.Billing.Should().NotBeNull();
        client.Api.V1.Billing.Entitlements.Should().NotBeNull();
        client.Api.V1.Billing.Agreements.Should().NotBeNull();
        client.Api.V1.Billing.Meter_usage.Should().NotBeNull();
    }
}
