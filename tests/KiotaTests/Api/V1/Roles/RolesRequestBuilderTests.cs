using System.Net;
using FluentAssertions;
using Kiota.Api.Models;
using Xunit;
namespace KiotaTests.Api.V1.Roles;
public class RolesRequestBuilderTests
{
    [Fact] public async Task GetAsync_Returns200() { var (client, handler) = ApiClientFactory.Create(HttpStatusCode.OK, MockData.GetRolesResponse); var result = await client.Api.V1.Roles.GetAsync(); handler.LastRequest!.RequestUri!.PathAndQuery.Should().Be("/api/v1/roles"); result!.Roles![0].Key.Should().Be("admin"); }
    [Fact] public async Task PostAsync_Returns200() { var (client, handler) = ApiClientFactory.Create(HttpStatusCode.OK, """{"code":"OK","role":{"id":"role_new"}}"""); await client.Api.V1.Roles.PostAsync(new CreateRole_request { Name = "Editor", Key = "editor" }); handler.LastRequest!.Method.Should().Be(HttpMethod.Post); }
}
