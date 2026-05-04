using System.Net;
using FluentAssertions;
using Kiota.Api.Models;
using Xunit;
namespace KiotaTests.Api.V1.Organizations.Item.Feature_flags.Item;
public class WithFeature_flag_keyItemRequestBuilderTests
{
    [Fact]
    public async Task PatchAsync_Returns200()
    {
        var (client, handler) = ApiClientFactory.Create(HttpStatusCode.OK, MockData.SuccessResponse);
        await client.Api.V1.Organizations["org_001"].Feature_flags["flag_001"].PatchAsync();
        handler.LastRequest!.Method.Should().Be(HttpMethod.Patch);
    }
    [Fact] public async Task DeleteAsync_Returns200() { var (client, handler) = ApiClientFactory.Create(HttpStatusCode.OK, MockData.SuccessResponse); await client.Api.V1.Organizations["org_001"].Feature_flags["flag_001"].DeleteAsync(); handler.LastRequest!.Method.Should().Be(HttpMethod.Delete); }

}
