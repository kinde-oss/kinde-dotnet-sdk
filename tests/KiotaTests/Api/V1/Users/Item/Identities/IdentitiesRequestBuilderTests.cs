using System.Net;
using FluentAssertions;
using Kiota.Api.Models;
using Xunit;
namespace KiotaTests.Api.V1.Users.Item.Identities;
public class IdentitiesRequestBuilderTests
{
    private const string UserId = "kp_user_001";
    [Fact]
    public async Task GetAsync_Returns200()
    {
        var (client, handler) = ApiClientFactory.Create(HttpStatusCode.OK, MockData.GetIdentitiesResponse);
        var result = await client.Api.V1.Users[UserId].Identities.GetAsync();
        result!.Identities![0].Email.Should().Be("alice@example.com");
    }
    [Fact]
    public async Task PostAsync_Returns200()
    {
        var (client, handler) = ApiClientFactory.Create(HttpStatusCode.OK, MockData.SuccessResponse);
        await client.Api.V1.Users[UserId].Identities.PostAsync(
            new CreateUserIdentity_request
            {
                Type = CreateUserIdentity_request_type.Email,
                Value = "alice2@example.com"
            }
        );
        handler.LastRequest!.Method.Should().Be(HttpMethod.Post);
    }
}
