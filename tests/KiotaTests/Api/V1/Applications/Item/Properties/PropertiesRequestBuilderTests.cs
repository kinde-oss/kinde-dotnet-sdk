using System.Net; using FluentAssertions; using Xunit;
namespace KiotaTests.Api.V1.Applications.Item.Properties;
public class PropertiesRequestBuilderTests
{
    private const string AppId = "app_001";
    [Fact] public async Task GetAsync_Returns200() { var (client, handler) = ApiClientFactory.Create(HttpStatusCode.OK, MockData.GetPropertiesResponse); var result = await client.Api.V1.Applications[AppId].Properties.GetAsync(); result.Should().NotBeNull(); }
}
