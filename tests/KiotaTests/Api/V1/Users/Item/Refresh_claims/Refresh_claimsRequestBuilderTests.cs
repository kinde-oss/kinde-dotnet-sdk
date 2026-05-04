using System.Net;
using FluentAssertions;
using Xunit;
namespace KiotaTests.Api.V1.Users.Item.Refresh_claims;
public class Refresh_claimsRequestBuilderTests
{
    [Fact] public async Task PostAsync_Returns200() { var (client, handler) = ApiClientFactory.Create(HttpStatusCode.OK, MockData.SuccessResponse); await client.Api.V1.Users["kp_user_001"].Refresh_claims.PostAsync(); handler.LastRequest!.Method.Should().Be(HttpMethod.Post); handler.LastRequest.RequestUri!.PathAndQuery.Should().Contain("refresh_claims"); }
}
