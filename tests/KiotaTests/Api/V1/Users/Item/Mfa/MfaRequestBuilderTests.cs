using System.Net;
using FluentAssertions;
using Xunit;
namespace KiotaTests.Api.V1.Users.Item.Mfa;
public class MfaRequestBuilderTests
{
    private const string UserId = "kp_user_001";
    [Fact] public async Task GetAsync_Returns200() { var (client, handler) = ApiClientFactory.Create(HttpStatusCode.OK, MockData.GetUserMfaResponse); var result = await client.Api.V1.Users[UserId].Mfa.GetAsync(); result.Should().NotBeNull(); }
    [Fact] public async Task DeleteAsync_Returns200() { var (client, handler) = ApiClientFactory.Create(HttpStatusCode.OK, MockData.SuccessResponse); await client.Api.V1.Users[UserId].Mfa.DeleteAsync(); handler.LastRequest!.Method.Should().Be(HttpMethod.Delete); }
}
