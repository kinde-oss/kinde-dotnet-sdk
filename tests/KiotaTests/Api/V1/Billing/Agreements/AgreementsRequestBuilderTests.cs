using System.Net; using FluentAssertions; using Kiota.Api.Models; using Xunit;
namespace KiotaTests.Api.V1.Billing.Agreements;
public class AgreementsRequestBuilderTests
{
    [Fact] public async Task GetAsync_Returns200_WithAgreements()
    {
        var (client, handler) = ApiClientFactory.Create(HttpStatusCode.OK, MockData.GetBillingAgreementsResponse);
        var result = await client.Api.V1.Billing.Agreements.GetAsync();
        handler.LastRequest!.RequestUri!.AbsolutePath
    .Should().Be("/api/v1/billing/agreements");
        result!.Agreements.Should().NotBeNullOrEmpty();
    }
    [Fact] public async Task PostAsync_Returns200()
    {
        var (client, handler) = ApiClientFactory.Create(HttpStatusCode.OK, MockData.SuccessResponse);
        await client.Api.V1.Billing.Agreements.PostAsync(new CreateBillingAgreement_request());
        handler.LastRequest!.Method.Should().Be(HttpMethod.Post);
    }
}
