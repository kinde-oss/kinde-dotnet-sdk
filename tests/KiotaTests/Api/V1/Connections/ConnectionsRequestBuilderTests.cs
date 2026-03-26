using System.Net; using FluentAssertions; using Kiota.Api.Models; using Xunit;
namespace KiotaTests.Api.V1.Connections;
public class ConnectionsRequestBuilderTests
{
    [Fact]
    public async Task GetAsync_Returns200()
    {
        var (client, handler) = ApiClientFactory.Create(HttpStatusCode.OK, MockData.GetConnectionsResponse);

        var result = await client.Api.V1.Connections.GetAsync();

        handler.LastRequest!.RequestUri!.PathAndQuery.Should().Be("/api/v1/connections");
        result.Should().NotBeNull();
        result!.Connections![0].ConnectionProp!.Name.Should().Be("Google");
    }
    [Fact] public async Task PostAsync_Returns200() { var (client, handler) = ApiClientFactory.Create(HttpStatusCode.OK, """{"code":"OK","connection":{"id":"conn_new"}}"""); await client.Api.V1.Connections.PostAsync(new CreateConnection_request { Name = "Google", DisplayName = "Google", Strategy = CreateConnection_request_strategy.Oauth2Google }); handler.LastRequest!.Method.Should().Be(HttpMethod.Post); }
}
