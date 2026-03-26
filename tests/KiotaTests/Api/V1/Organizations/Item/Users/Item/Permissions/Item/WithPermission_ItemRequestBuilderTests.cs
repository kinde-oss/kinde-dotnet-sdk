using System.Net; using FluentAssertions; using Xunit;
namespace KiotaTests.Api.V1.Organizations.Item.Users.Item.Permissions.Item;
public class WithPermission_ItemRequestBuilderTests
{
    [Fact] public async Task DeleteAsync_Returns200() { var (client, handler) = ApiClientFactory.Create(HttpStatusCode.OK, MockData.SuccessResponse); await client.Api.V1.Organizations["org_001"].Users["kp_user_001"].Permissions["perm_001"].DeleteAsync(); handler.LastRequest!.Method.Should().Be(HttpMethod.Delete); }
}
