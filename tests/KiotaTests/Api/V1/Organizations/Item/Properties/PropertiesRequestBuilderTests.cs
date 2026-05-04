using System.Net; using FluentAssertions; using Kiota.Api.Models; using Xunit;
namespace KiotaTests.Api.V1.Organizations.Item.Properties;
public class PropertiesRequestBuilderTests
{
    [Fact] public async Task GetAsync_Returns200() { var (client, handler) = ApiClientFactory.Create(HttpStatusCode.OK, MockData.GetPropertiesResponse); var result = await client.Api.V1.Organizations["org_001"].Properties.GetAsync(); result.Should().NotBeNull(); }
    [Fact] public async Task PatchAsync_Returns200() { var (client, handler) = ApiClientFactory.Create(HttpStatusCode.OK, MockData.SuccessResponse); await client.Api.V1.Organizations["org_001"].Properties.PatchAsync(new UpdateOrganizationProperties_request()); handler.LastRequest!.Method.Should().Be(HttpMethod.Patch); }
}
