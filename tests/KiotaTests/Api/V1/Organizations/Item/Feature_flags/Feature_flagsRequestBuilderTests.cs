using System.Net; using FluentAssertions; using Xunit;
namespace KiotaTests.Api.V1.Organizations.Item.Feature_flags;
public class Feature_flagsRequestBuilderTests
{
    private const string OrgCode = "org_001";
    [Fact] public async Task GetAsync_Returns200() { var (client, handler) = ApiClientFactory.Create(HttpStatusCode.OK, MockData.GetFeatureFlagsResponse); var result = await client.Api.V1.Organizations[OrgCode].Feature_flags.GetAsync(); result.Should().NotBeNull(); }
    [Fact] public async Task DeleteAsync_Returns200() { var (client, handler) = ApiClientFactory.Create(HttpStatusCode.OK, MockData.SuccessResponse); await client.Api.V1.Organizations[OrgCode].Feature_flags.DeleteAsync(); handler.LastRequest!.Method.Should().Be(HttpMethod.Delete); }
}
