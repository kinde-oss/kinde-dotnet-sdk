using System.Net; using FluentAssertions; using Kiota.Api.Models; using Xunit;
namespace KiotaTests.Api.V1.EnvironmentNamespace.Feature_flags.Item;
public class WithFeature_flag_keyItemRequestBuilderTests
{
    private const string FlagKey = "new_dashboard";
    [Fact] public async Task PatchAsync_Returns200() { var (client, handler) = ApiClientFactory.Create(HttpStatusCode.OK, MockData.SuccessResponse); await client.Api.V1.Environment.Feature_flags[FlagKey].PatchAsync(new UpdateEnvironementFeatureFlagOverride_request { Value = "true" }); handler.LastRequest!.Method.Should().Be(HttpMethod.Patch); handler.LastRequest.RequestUri!.PathAndQuery.Should().Contain(FlagKey); }
    [Fact] public async Task DeleteAsync_Returns200() { var (client, handler) = ApiClientFactory.Create(HttpStatusCode.OK, MockData.SuccessResponse); await client.Api.V1.Environment.Feature_flags[FlagKey].DeleteAsync(); handler.LastRequest!.Method.Should().Be(HttpMethod.Delete); }
}
