using System.Net; using FluentAssertions; using Kiota.Api.Models; using Xunit;
namespace KiotaTests.Api.V1.Identities.Item;
public class WithIdentity_ItemRequestBuilderTests
{
    private const string IdentId = "ident_001";
    [Fact]
    public async Task GetAsync_Returns200()
    {
        var (client, handler) = ApiClientFactory.Create(
            HttpStatusCode.OK,
            """{"id":"ident_001","type":"email"}"""
        );

        var result = await client.Api.V1.Identities[IdentId].GetAsync();

        result!.Id.Should().Be("ident_001");
    }
    [Fact] public async Task PatchAsync_Returns200() { var (client, handler) = ApiClientFactory.Create(HttpStatusCode.OK, MockData.SuccessResponse); await client.Api.V1.Identities[IdentId].PatchAsync(new UpdateIdentity_request()); handler.LastRequest!.Method.Should().Be(HttpMethod.Patch); }
    [Fact] public async Task DeleteAsync_Returns200() { var (client, handler) = ApiClientFactory.Create(HttpStatusCode.OK, MockData.SuccessResponse); await client.Api.V1.Identities[IdentId].DeleteAsync(); handler.LastRequest!.Method.Should().Be(HttpMethod.Delete); }
}
