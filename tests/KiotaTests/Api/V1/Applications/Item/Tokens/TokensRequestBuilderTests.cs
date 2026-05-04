using System.Net; using FluentAssertions; using Kiota.Api.Models; using Xunit;
namespace KiotaTests.Api.V1.Applications.Item.Tokens;
public class TokensRequestBuilderTests
{
    private const string AppId = "app_001";
    [Fact] public async Task PatchAsync_Returns200() { var (client, handler) = ApiClientFactory.Create(HttpStatusCode.OK, MockData.SuccessResponse); await client.Api.V1.Applications[AppId].Tokens.PatchAsync(new UpdateApplicationTokens_request()); handler.LastRequest!.Method.Should().Be(HttpMethod.Patch); }
}
