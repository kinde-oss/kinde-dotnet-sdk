using System.Net; using FluentAssertions; using Kiota.Api.Models; using Xunit;
namespace KiotaTests.Api.V1.Organization.Item;
public class WithOrg_codeItemRequestBuilderTests
{
    private const string OrgCode = "org_1767f11ce62";
    [Fact] public async Task PatchAsync_Returns200() { var (client, handler) = ApiClientFactory.Create(HttpStatusCode.OK, MockData.SuccessResponse); await client.Api.V1.Organization[OrgCode].PatchAsync(new UpdateOrganization_request { Name = "Updated" }); handler.LastRequest!.Method.Should().Be(HttpMethod.Patch); handler.LastRequest.RequestUri!.PathAndQuery.Should().Contain(OrgCode); }
    [Fact] public async Task DeleteAsync_Returns200() { var (client, handler) = ApiClientFactory.Create(HttpStatusCode.OK, MockData.SuccessResponse); await client.Api.V1.Organization[OrgCode].DeleteAsync(); handler.LastRequest!.Method.Should().Be(HttpMethod.Delete); }
}
