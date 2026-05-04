using System.Net; using FluentAssertions; using Xunit;
namespace KiotaTests.Api.V1.Connected_apps.Token;
public class TokenRequestBuilderTests
{
    [Fact] public async Task GetAsync_Returns200_WithAccessToken()
    {
        var (client, handler) = ApiClientFactory.Create(HttpStatusCode.OK, MockData.GetConnectedAppTokenResponse);
        var result = await client.Api.V1.Connected_apps.Token.GetAsync(c => c.QueryParameters.SessionId = "sess_abc");
        result!.AccessToken.Should().StartWith("eyJhbGci");
    }
}
