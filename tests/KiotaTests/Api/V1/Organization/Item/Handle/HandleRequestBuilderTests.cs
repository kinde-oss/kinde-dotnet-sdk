using System.Net; using FluentAssertions; using Xunit;
namespace KiotaTests.Api.V1.Organization.Item.Handle;
public class HandleRequestBuilderTests
{
    [Fact] public async Task DeleteAsync_Returns200() { var (client, handler) = ApiClientFactory.Create(HttpStatusCode.OK, MockData.SuccessResponse); await client.Api.V1.Organization["org_001"].Handle.DeleteAsync(); handler.LastRequest!.Method.Should().Be(HttpMethod.Delete); handler.LastRequest.RequestUri!.PathAndQuery.Should().Contain("/handle"); }
}
