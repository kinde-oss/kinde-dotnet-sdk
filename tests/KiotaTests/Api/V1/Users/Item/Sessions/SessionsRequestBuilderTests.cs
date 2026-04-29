using System.Net;
using FluentAssertions;
using Xunit;
namespace KiotaTests.Api.V1.Users.Item.Sessions;
public class SessionsRequestBuilderTests
{
    private const string UserId = "kp_user_001";
    [Fact]
    public async Task GetAsync_Returns200()
    {
        var (client, handler) = ApiClientFactory.Create(HttpStatusCode.OK, MockData.GetUserSessionsResponse);

        var result = await client.Api.V1.Users[UserId].Sessions.GetAsync();

        result!.Sessions![0].SessionId.Should().Be("sess_001");
    }
    [Fact]
    public async Task DeleteAsync_Returns200()
    {
        var (client, handler) = ApiClientFactory.Create(HttpStatusCode.OK, MockData.SuccessResponse);
        await client.Api.V1.Users[UserId].Sessions.DeleteAsync();
        handler.LastRequest!.Method.Should().Be(HttpMethod.Delete);
    }
}
