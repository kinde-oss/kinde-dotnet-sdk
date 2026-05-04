using System.Net; using FluentAssertions; using Xunit;
namespace KiotaTests.Api.V1.Industries;
public class IndustriesRequestBuilderTests
{
    [Fact] public async Task GetAsync_Returns200() { var (client, handler) = ApiClientFactory.Create(HttpStatusCode.OK, MockData.GetIndustriesResponse); var result = await client.Api.V1.Industries.GetAsync(); handler.LastRequest!.RequestUri!.PathAndQuery.Should().Be("/api/v1/industries"); result!.Industries![0].Name.Should().Be("Technology"); }
}
