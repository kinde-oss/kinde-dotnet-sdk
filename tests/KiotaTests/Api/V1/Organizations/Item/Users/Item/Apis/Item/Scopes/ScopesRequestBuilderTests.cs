using System.Net; using FluentAssertions; using Xunit;
namespace KiotaTests.Api.V1.Organizations.Item.Users.Item.Apis.Item.Scopes;
public class ScopesRequestBuilderTests
{
    [Fact] public async Task PostAsync_Returns200() { var (client, handler) = ApiClientFactory.Create(HttpStatusCode.OK, MockData.SuccessResponse); await client.Api.V1.Organizations["org_001"].Users["kp_user_001"].Apis["api_001"].Scopes["scope_001"].PostAsync(); handler.LastRequest!.Method.Should().Be(HttpMethod.Post); }
    [Fact] public async Task DeleteAsync_Returns200() { var (client, handler) = ApiClientFactory.Create(HttpStatusCode.OK, MockData.SuccessResponse); await client.Api.V1.Organizations["org_001"].Users["kp_user_001"].Apis["api_001"].Scopes["scope_001"].DeleteAsync(); handler.LastRequest!.Method.Should().Be(HttpMethod.Delete); }
}
