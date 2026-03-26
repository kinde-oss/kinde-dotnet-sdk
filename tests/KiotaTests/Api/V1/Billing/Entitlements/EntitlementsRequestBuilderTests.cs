using System.Net; using FluentAssertions; using Xunit;
namespace KiotaTests.Api.V1.Billing.Entitlements;
public class EntitlementsRequestBuilderTests
{
    [Fact]
    public async Task GetAsync_Returns200_WithEntitlements()
    {
        var (client, handler) = ApiClientFactory.Create(HttpStatusCode.OK, MockData.GetBillingEntitlementsResponse);
        var result = await client.Api.V1.Billing.Entitlements.GetAsync();
        handler.LastRequest!.RequestUri!.PathAndQuery.Should().StartWith("/api/v1/billing/entitlements");
        result!.Entitlements![0].FeatureName.Should().Be("Pro Gym");
    }
}
