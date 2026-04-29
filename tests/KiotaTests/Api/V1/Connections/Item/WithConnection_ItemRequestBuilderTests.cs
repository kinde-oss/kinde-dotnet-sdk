using System.Net;
using FluentAssertions;
using Kiota.Api.Models;
using Xunit;
namespace KiotaTests.Api.V1.Connections.Item;
public class WithConnection_ItemRequestBuilderTests
{
    private const string ConnId = "conn_001";
    [Fact]
    public async Task GetAsync_Returns200()
    {
        var (client, handler) = ApiClientFactory.Create(HttpStatusCode.OK,
            """{"code":"OK","connection":{"id":"conn_001","name":"Google","strategy":"oauth2:google"}}""");
        var result = await client.Api.V1.Connections[ConnId].GetAsync();
        result!.ConnectionProp!.Strategy.Should().Be("oauth2:google");
    }
    [Fact] public async Task PatchAsync_Returns200() { var (client, handler) = ApiClientFactory.Create(HttpStatusCode.OK, MockData.SuccessResponse); await client.Api.V1.Connections[ConnId].PatchAsync(new UpdateConnection_request()); handler.LastRequest!.Method.Should().Be(HttpMethod.Patch); }
    [Fact] public async Task PutAsync_Returns200() { var (client, handler) = ApiClientFactory.Create(HttpStatusCode.OK, MockData.SuccessResponse); await client.Api.V1.Connections[ConnId].PutAsync(new ReplaceConnection_request()); handler.LastRequest!.Method.Should().Be(HttpMethod.Put); }
    [Fact] public async Task DeleteAsync_Returns200() { var (client, handler) = ApiClientFactory.Create(HttpStatusCode.OK, MockData.SuccessResponse); await client.Api.V1.Connections[ConnId].DeleteAsync(); handler.LastRequest!.Method.Should().Be(HttpMethod.Delete); }
}
