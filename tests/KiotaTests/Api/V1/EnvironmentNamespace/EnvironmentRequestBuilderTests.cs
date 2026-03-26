using System.Net; using FluentAssertions; using Xunit;
namespace KiotaTests.Api.V1.EnvironmentNamespace;
public class EnvironmentRequestBuilderTests
{
    [Fact] public async Task GetAsync_Returns200() { var (client, handler) = ApiClientFactory.Create(HttpStatusCode.OK, MockData.GetEnvironmentResponse); var result = await client.Api.V1.Environment.GetAsync(); handler.LastRequest!.RequestUri!.PathAndQuery.Should().Be("/api/v1/environment"); result!.Environment!.Code.Should().Be("env_default"); }
}
