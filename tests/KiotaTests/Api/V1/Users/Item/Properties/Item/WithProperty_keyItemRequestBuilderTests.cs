using System.Net;
using FluentAssertions;
using Kiota.Api.Models;
using Xunit;
namespace KiotaTests.Api.V1.Users.Item.Properties.Item;
public class WithProperty_keyItemRequestBuilderTests
{
    [Fact]
    public async Task PutAsync_Returns200()
    {
        var (client, handler) = ApiClientFactory.Create(HttpStatusCode.OK, MockData.SuccessResponse);
        await client.Api.V1.Users["kp_user_001"].Properties["fav_color"].PutAsync();
        handler.LastRequest!.Method.Should().Be(HttpMethod.Put); handler.LastRequest.RequestUri!.PathAndQuery.Should().Contain("fav_color");
    }
}
