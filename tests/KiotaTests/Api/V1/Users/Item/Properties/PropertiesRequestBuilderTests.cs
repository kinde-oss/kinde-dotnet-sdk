using System.Net;
using FluentAssertions;
using Kiota.Api.Models;
using Xunit;
namespace KiotaTests.Api.V1.Users.Item.Properties;
public class PropertiesRequestBuilderTests
{
    private const string UserId = "kp_user_001";
    [Fact] public async Task GetAsync_Returns200() { var (client, handler) = ApiClientFactory.Create(HttpStatusCode.OK, """{"code":"OK","properties":{}}"""); var result = await client.Api.V1.Users[UserId].Properties.GetAsync(); result.Should().NotBeNull(); }
    [Fact] public async Task PatchAsync_Returns200() { var (client, handler) = ApiClientFactory.Create(HttpStatusCode.OK, MockData.SuccessResponse); await client.Api.V1.Users[UserId].Properties.PatchAsync(new UpdateOrganizationProperties_request()); handler.LastRequest!.Method.Should().Be(HttpMethod.Patch); }
}
