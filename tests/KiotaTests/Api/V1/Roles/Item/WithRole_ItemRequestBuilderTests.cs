using System.Net;
using FluentAssertions;
using Kiota.Api.Models;
using Xunit;
namespace KiotaTests.Api.V1.Roles.Item;
public class WithRole_ItemRequestBuilderTests
{
    private const string RoleId = "role_001";
    [Fact] public async Task GetAsync_Returns200() { var (client, handler) = ApiClientFactory.Create(HttpStatusCode.OK, MockData.GetRoleResponse); var result = await client.Api.V1.Roles[RoleId].GetAsync(); result!.Role!.Key.Should().Be("admin"); }
    [Fact] public async Task PatchAsync_Returns200() { var (client, handler) = ApiClientFactory.Create(HttpStatusCode.OK, MockData.SuccessResponse); await client.Api.V1.Roles[RoleId].PatchAsync(new UpdateRoles_request { Name = "Super Admin" }); handler.LastRequest!.Method.Should().Be(HttpMethod.Patch); }
    [Fact] public async Task DeleteAsync_Returns200() { var (client, handler) = ApiClientFactory.Create(HttpStatusCode.OK, MockData.SuccessResponse); await client.Api.V1.Roles[RoleId].DeleteAsync(); handler.LastRequest!.Method.Should().Be(HttpMethod.Delete); }
}
