using System.Net;
using FluentAssertions;
using Kiota.Api.Models;
using Xunit;
namespace KiotaTests.Api.V1.Roles.Item.Scopes;
public class ScopesRequestBuilderTests
{
    private const string RoleId = "role_001";
    [Fact]
    public async Task GetAsync_Returns200()
    {
        var (client, handler) = ApiClientFactory.Create(HttpStatusCode.OK, MockData.GetRoleScopesResponse);
        var result = await client.Api.V1.Roles[RoleId].Scopes.GetAsync();
        result!.Scopes![0].Key.Should().Be("read:data");
    }
    [Fact]
    public async Task PostAsync_Returns200()
    {
        var (client, handler) = ApiClientFactory.Create(HttpStatusCode.OK, MockData.SuccessResponse);
        await client.Api.V1.Roles[RoleId].Scopes.PostAsync(new AddRoleScope_request { ScopeId = "scope_001" });
        handler.LastRequest!.Method.Should().Be(HttpMethod.Post);
    }
}
