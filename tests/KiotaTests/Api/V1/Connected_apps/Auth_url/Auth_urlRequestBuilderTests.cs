using System.Net; using FluentAssertions; using Xunit;
namespace KiotaTests.Api.V1.Connected_apps.Auth_url;
public class Auth_urlRequestBuilderTests
{
    [Fact] public async Task GetAsync_Returns200_WithAuthUrl()
    {
        var (client, handler) = ApiClientFactory.Create(HttpStatusCode.OK, MockData.GetConnectedAppAuthUrlResponse);
        var result = await client.Api.V1.Connected_apps.Auth_url.GetAsync(c => { c.QueryParameters.KeyCodeRef = "google"; c.QueryParameters.UserId = "kp_user_001"; });
        result!.Url.Should().Contain("https://auth.provider.com");
        result.SessionId.Should().Be("sess_abc");
    }
}
