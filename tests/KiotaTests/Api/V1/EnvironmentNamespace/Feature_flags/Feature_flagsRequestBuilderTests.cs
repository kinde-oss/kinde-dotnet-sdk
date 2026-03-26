using System.Net; using FluentAssertions; using Xunit;
namespace KiotaTests.Api.V1.EnvironmentNamespace.Feature_flags;
public class Feature_flagsRequestBuilderTests
{
    [Fact] public async Task GetAsync_Returns200() { var (client, handler) = ApiClientFactory.Create(HttpStatusCode.OK, MockData.GetFeatureFlagsResponse); var result = await client.Api.V1.Environment.Feature_flags.GetAsync(); handler.LastRequest!.RequestUri!.PathAndQuery.Should().Contain("feature_flags"); result.Should().NotBeNull(); }
    [Fact] public async Task DeleteAsync_Returns200() { var (client, handler) = ApiClientFactory.Create(HttpStatusCode.OK, MockData.SuccessResponse); await client.Api.V1.Environment.Feature_flags.DeleteAsync(); handler.LastRequest!.Method.Should().Be(HttpMethod.Delete); }
}
