using System.Net; using FluentAssertions; using Kiota.Api.Models; using Xunit;
namespace KiotaTests.Api.V1.Business;
public class BusinessRequestBuilderTests
{
    [Fact] public async Task GetAsync_Returns200_WithDetails()
    {
        var (client, handler) = ApiClientFactory.Create(HttpStatusCode.OK, MockData.GetBusinessResponse);
        var result = await client.Api.V1.Business.GetAsync();
        handler.LastRequest!.RequestUri!.PathAndQuery.Should().Be("/api/v1/business");
        result!.Business!.Name.Should().Be("Acme Corp");
    }
    [Fact] public async Task PatchAsync_Returns200()
    {
        var (client, handler) = ApiClientFactory.Create(HttpStatusCode.OK, MockData.SuccessResponse);
        await client.Api.V1.Business.PatchAsync(new UpdateBusiness_request { BusinessName = "Acme Updated" });
        handler.LastRequest!.Method.Should().Be(HttpMethod.Patch);
    }
}
