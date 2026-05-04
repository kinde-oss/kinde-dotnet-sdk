using System.Net;
using FluentAssertions;
using Xunit;
namespace KiotaTests.Api.V1.Organizations.Item.Connections;
public class ConnectionsRequestBuilderTests
{
    [Fact] public async Task GetAsync_Returns200() { var (client, handler) = ApiClientFactory.Create(HttpStatusCode.OK, MockData.GetConnectionsResponse); var result = await client.Api.V1.Organizations["org_001"].Connections.GetAsync(); result!.Connections![0].ConnectionProp!.Name.Should().Be("Google"); }
}
