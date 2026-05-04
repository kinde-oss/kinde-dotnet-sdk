using System.Net;
using FluentAssertions;
using Kiota.Api.Models;
using Xunit;
namespace KiotaTests.Api.V1.Permissions;
public class PermissionsRequestBuilderTests
{
    [Fact] public async Task GetAsync_Returns200() { var (client, handler) = ApiClientFactory.Create(HttpStatusCode.OK, MockData.GetPermissionsResponse); var result = await client.Api.V1.Permissions.GetAsync(); handler.LastRequest!.RequestUri!.PathAndQuery.Should().Be("/api/v1/permissions"); result!.Permissions![0].Key.Should().Be("read:reports"); }
    [Fact] public async Task PostAsync_Returns200() { var (client, handler) = ApiClientFactory.Create(HttpStatusCode.OK, MockData.SuccessResponse); await client.Api.V1.Permissions.PostAsync(new CreatePermission_request { Name = "Write", Key = "write:data", Description = "Write" }); handler.LastRequest!.Method.Should().Be(HttpMethod.Post); }
}
