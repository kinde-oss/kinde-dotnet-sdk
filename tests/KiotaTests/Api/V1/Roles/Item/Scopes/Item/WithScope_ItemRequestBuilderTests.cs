using System.Net;
using FluentAssertions;
using Xunit;
namespace KiotaTests.Api.V1.Roles.Item.Scopes.Item;
public class WithScope_ItemRequestBuilderTests
{
    [Fact] public async Task DeleteAsync_Returns200() { var (client, handler) = ApiClientFactory.Create(HttpStatusCode.OK, MockData.SuccessResponse); await client.Api.V1.Roles["role_001"].Scopes["scope_001"].DeleteAsync(); handler.LastRequest!.Method.Should().Be(HttpMethod.Delete); handler.LastRequest.RequestUri!.PathAndQuery.Should().Contain("/roles/role_001/scopes/scope_001"); }
}
