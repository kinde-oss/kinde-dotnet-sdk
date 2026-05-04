using System.Net; using FluentAssertions; using Xunit;
namespace KiotaTests.Api.V1.Applications.Item.Connections.Item;
public class WithConnection_ItemRequestBuilderTests
{
    private const string AppId = "3b0b5c6c8fcc464fab397f4969b5f482";
    private const string ConnId = "conn_001";
    [Fact] public async Task PostAsync_Returns200() { var (client, handler) = ApiClientFactory.Create(HttpStatusCode.OK, MockData.SuccessResponse); await client.Api.V1.Applications[AppId].Connections[ConnId].PostAsync(); handler.LastRequest!.Method.Should().Be(HttpMethod.Post); }
    [Fact] public async Task DeleteAsync_Returns200() { var (client, handler) = ApiClientFactory.Create(HttpStatusCode.OK, MockData.SuccessResponse); await client.Api.V1.Applications[AppId].Connections[ConnId].DeleteAsync(); handler.LastRequest!.Method.Should().Be(HttpMethod.Delete); }
}
