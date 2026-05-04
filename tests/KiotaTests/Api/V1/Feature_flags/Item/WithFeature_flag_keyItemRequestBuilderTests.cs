using FluentAssertions; 
using System.Net; 
using Xunit;
using Kiota.Api.Api.V1.Feature_flags.Item;
namespace KiotaTests.Api.V1.Feature_flags.Item;
public class WithFeature_flag_keyItemRequestBuilderTests
{
    private const string FlagKey = "beta_feature";
    [Fact]
    public async Task PutAsync_Returns200()
    {
        var (client, handler) = ApiClientFactory.Create(HttpStatusCode.OK, MockData.SuccessResponse);

        await client.Api.V1.Feature_flags[FlagKey].PutAsync(config =>
        {
            config.QueryParameters.Name = "Updated";
            config.QueryParameters.DefaultValue = "true";
            config.QueryParameters.TypeAsPutTypeQueryParameterType = PutTypeQueryParameterType.Bool;
            config.QueryParameters.AllowOverrideLevelAsPutAllowOverrideLevelQueryParameterType
                                                                             = PutAllow_override_levelQueryParameterType.Org;
        });

        handler.LastRequest!.Method.Should().Be(HttpMethod.Put);
        handler.LastRequest.RequestUri!.PathAndQuery.Should().Contain(FlagKey);
    }
    [Fact] public async Task DeleteAsync_Returns200() { var (client, handler) = ApiClientFactory.Create(HttpStatusCode.OK, MockData.SuccessResponse); await client.Api.V1.Feature_flags[FlagKey].DeleteAsync(); handler.LastRequest!.Method.Should().Be(HttpMethod.Delete); }
}
