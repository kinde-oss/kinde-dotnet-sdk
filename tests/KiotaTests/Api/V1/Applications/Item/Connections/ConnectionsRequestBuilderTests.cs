using System.Net; using FluentAssertions; using Xunit;
namespace KiotaTests.Api.V1.Applications.Item.Connections;
public class ConnectionsRequestBuilderTests
{
    private const string AppId = "3b0b5c6c8fcc464fab397f4969b5f482";
    [Fact] public async Task GetAsync_Returns200() { var (client, handler) = ApiClientFactory.Create(HttpStatusCode.OK, MockData.GetConnectionsResponse); var result = await client.Api.V1.Applications[AppId].Connections.GetAsync(); result.Should().NotBeNull(); }
}
