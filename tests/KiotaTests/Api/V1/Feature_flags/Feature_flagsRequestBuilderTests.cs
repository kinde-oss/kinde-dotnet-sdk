using System.Net; using FluentAssertions; using Kiota.Api.Models; using Xunit;
namespace KiotaTests.Api.V1.Feature_flags;
public class Feature_flagsRequestBuilderTests
{
    [Fact]
    public async Task PostAsync_Returns200_CreatesFlag()
    {
        var (client, handler) = ApiClientFactory.Create(HttpStatusCode.OK, MockData.SuccessResponse);

        await client.Api.V1.Feature_flags.PostAsync(new CreateFeatureFlag_request
        {
            Name = "Beta",
            Key = "beta_feature",
            Type = CreateFeatureFlag_request_type.Bool,
            DefaultValue = "false",
            AllowOverrideLevel = CreateFeatureFlag_request_allow_override_level.Usr
        });

        handler.LastRequest!.Method.Should().Be(HttpMethod.Post);
        handler.LastRequest.RequestUri!.PathAndQuery.Should().Be("/api/v1/feature_flags");
    }
}
