using System.Net; using FluentAssertions; using Xunit;
namespace KiotaTests.Api.V1.Connected_apps.Revoke;
public class RevokeRequestBuilderTests
{
    [Fact]
    public async Task PostAsync_Returns200_RevokesToken()
    {
        var (client, handler) = ApiClientFactory.Create(HttpStatusCode.OK, """{"message":"Authorization session successfully revoked"}""");
        await client.Api.V1.Connected_apps.Revoke.PostAsync();
        handler.LastRequest!.Method.Should().Be(HttpMethod.Post);
        handler.LastRequest.RequestUri!.AbsolutePath.Should().Be("/api/v1/connected_apps/revoke");
    }
}
