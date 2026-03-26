using System.Net; using FluentAssertions; using Xunit;
namespace KiotaTests.Api.V1.EnvironmentNamespace.Logos;
public class LogosRequestBuilderTests
{
    [Fact] public async Task GetAsync_Returns200() { var (client, handler) = ApiClientFactory.Create(HttpStatusCode.OK, MockData.GetEnvLogosResponse); var result = await client.Api.V1.Environment.Logos.GetAsync(); handler.LastRequest!.RequestUri!.PathAndQuery.Should().Contain("/environment/logos"); result.Should().NotBeNull(); }
}
