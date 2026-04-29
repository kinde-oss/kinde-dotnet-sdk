using System.Net; using FluentAssertions; using Kiota.Api.Models; using Xunit;
namespace KiotaTests.Api.V1.Organizations.Item.Users.Item.Permissions;
public class PermissionsRequestBuilderTests
{
    [Fact] public async Task GetAsync_Returns200() { var (client, handler) = ApiClientFactory.Create(HttpStatusCode.OK, MockData.GetPermissionsResponse); var result = await client.Api.V1.Organizations["org_001"].Users["kp_user_001"].Permissions.GetAsync(); result.Should().NotBeNull(); }
    [Fact] public async Task PostAsync_Returns200() { var (client, handler) = ApiClientFactory.Create(HttpStatusCode.OK, MockData.SuccessResponse); await client.Api.V1.Organizations["org_001"].Users["kp_user_001"].Permissions.PostAsync(new CreateOrganizationUserPermission_request { PermissionId = "perm_001" }); handler.LastRequest!.Method.Should().Be(HttpMethod.Post); }
}
