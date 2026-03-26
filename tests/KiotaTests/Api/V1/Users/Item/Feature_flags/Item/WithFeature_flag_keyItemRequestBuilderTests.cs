using System.Net;
using FluentAssertions;
using Kiota.Api.Models;
using Xunit;
namespace KiotaTests.Api.V1.Users.Item.Feature_flags.Item;
public class WithFeature_flag_keyItemRequestBuilderTests
{
    [Fact] public async Task PatchAsync_Returns200() { var (client, handler) = ApiClientFactory.Create(HttpStatusCode.OK, MockData.SuccessResponse); await client.Api.V1.Users["kp_user_001"].Feature_flags["new_dashboard"].PatchAsync(); handler.LastRequest!.Method.Should().Be(HttpMethod.Patch); handler.LastRequest.RequestUri!.PathAndQuery.Should().Contain("new_dashboard"); }
}
