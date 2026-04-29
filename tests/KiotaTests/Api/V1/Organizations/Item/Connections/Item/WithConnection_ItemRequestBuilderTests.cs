using System.Net; using FluentAssertions; using Xunit;
namespace KiotaTests.Api.V1.Organizations.Item.Connections.Item;
public class WithConnection_ItemRequestBuilderTests
{
    [Fact] public async Task PostAsync_Returns200() { var (client, handler) = ApiClientFactory.Create(HttpStatusCode.OK, MockData.SuccessResponse); await client.Api.V1.Organizations["org_001"].Connections["conn_001"].PostAsync(); handler.LastRequest!.Method.Should().Be(HttpMethod.Post); }
    [Fact] public async Task DeleteAsync_Returns200() { var (client, handler) = ApiClientFactory.Create(HttpStatusCode.OK, MockData.SuccessResponse); await client.Api.V1.Organizations["org_001"].Connections["conn_001"].DeleteAsync(); handler.LastRequest!.Method.Should().Be(HttpMethod.Delete); }
}
