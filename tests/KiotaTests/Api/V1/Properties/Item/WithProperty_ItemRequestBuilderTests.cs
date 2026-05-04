using System.Net;
using FluentAssertions;
using Kiota.Api.Models;
using Xunit;
namespace KiotaTests.Api.V1.Properties.Item;
public class WithProperty_ItemRequestBuilderTests
{
    private const string PropId = "prop_001";
    [Fact] public async Task PutAsync_Returns200() { var (client, handler) = ApiClientFactory.Create(HttpStatusCode.OK, MockData.SuccessResponse); await client.Api.V1.Properties[PropId].PutAsync(new UpdateProperty_request { Name = "Updated" }); handler.LastRequest!.Method.Should().Be(HttpMethod.Put); }
    [Fact] public async Task DeleteAsync_Returns200() { var (client, handler) = ApiClientFactory.Create(HttpStatusCode.OK, MockData.SuccessResponse); await client.Api.V1.Properties[PropId].DeleteAsync(); handler.LastRequest!.Method.Should().Be(HttpMethod.Delete); }
}
