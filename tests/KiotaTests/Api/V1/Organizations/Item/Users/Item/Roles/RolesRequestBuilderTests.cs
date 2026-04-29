using System.Net;
using FluentAssertions;
using Kiota.Api.Models;
using Xunit;
namespace KiotaTests.Api.V1.Organizations.Item.Users.Item.Roles;
public class RolesRequestBuilderTests
{
    [Fact] public async Task GetAsync_Returns200() { var (client, handler) = ApiClientFactory.Create(HttpStatusCode.OK, """{"code":"OK","roles":[{"id":"role_001","key":"admin"}]}"""); var result = await client.Api.V1.Organizations["org_001"].Users["kp_user_001"].Roles.GetAsync(); result!.Roles![0].Key.Should().Be("admin"); }
    [Fact] public async Task PostAsync_Returns200() { var (client, handler) = ApiClientFactory.Create(HttpStatusCode.OK, MockData.SuccessResponse); await client.Api.V1.Organizations["org_001"].Users["kp_user_001"].Roles.PostAsync(new CreateOrganizationUserRole_request { RoleId = "role_001" }); handler.LastRequest!.Method.Should().Be(HttpMethod.Post); }
}
