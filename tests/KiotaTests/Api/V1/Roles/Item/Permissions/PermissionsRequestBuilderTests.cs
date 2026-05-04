using System.Net;
using FluentAssertions;
using Kiota.Api.Models;
using Xunit;
namespace KiotaTests.Api.V1.Roles.Item.Permissions;
public class PermissionsRequestBuilderTests
{
    private const string RoleId = "role_001";
    [Fact] public async Task GetAsync_Returns200() { var (client, handler) = ApiClientFactory.Create(HttpStatusCode.OK, MockData.GetRolePermissionsResponse); var result = await client.Api.V1.Roles[RoleId].Permissions.GetAsync(); result!.Permissions![0].Key.Should().Be("read:reports"); }
    [Fact] public async Task PatchAsync_Returns200() { var (client, handler) = ApiClientFactory.Create(HttpStatusCode.OK, MockData.SuccessResponse); await client.Api.V1.Roles[RoleId].Permissions.PatchAsync(new UpdateRolePermissions_request()); handler.LastRequest!.Method.Should().Be(HttpMethod.Patch); }
}
