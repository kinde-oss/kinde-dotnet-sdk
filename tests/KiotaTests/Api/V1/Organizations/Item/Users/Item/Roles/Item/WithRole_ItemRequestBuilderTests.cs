using System.Net;
using FluentAssertions;
using Xunit;

namespace KiotaTests.Api.V1.Organizations.Item.Users.Item.Roles.Item;

public class WithRole_ItemRequestBuilderTests
{
    [Fact]
    public async Task DeleteAsync_Returns200()
    {
        var (client, handler) = ApiClientFactory.Create(HttpStatusCode.OK, MockData.SuccessResponse);

        await client.Api.V1.Organizations["org_001"]
            .Users["kp_user_001"]
            .Roles["role_001"]
            .DeleteAsync();

        handler.LastRequest!.Method.Should().Be(HttpMethod.Delete);
        handler.LastRequest!.RequestUri!.PathAndQuery.Should().Contain("organizations/org_001");
        handler.LastRequest!.RequestUri!.PathAndQuery.Should().Contain("users/kp_user_001");
        handler.LastRequest!.RequestUri!.PathAndQuery.Should().Contain("roles/role_001");
    }
}