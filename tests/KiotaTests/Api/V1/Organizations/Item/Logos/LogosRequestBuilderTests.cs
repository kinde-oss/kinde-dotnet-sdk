using System.Net;
using FluentAssertions;
using Xunit;

namespace KiotaTests.Api.V1.Organizations.Item.Logos;

public class LogosRequestBuilderTests
{
    [Fact]
    public async Task GetAsync_Returns200()
    {
        var (client, handler) = ApiClientFactory.Create(HttpStatusCode.OK, MockData.GetOrgLogosResponse);

        var result = await client.Api.V1.Organizations["org_001"].Logos.GetAsync();

        handler.LastRequest!.Method.Should().Be(HttpMethod.Get);
        handler.LastRequest!.RequestUri!.PathAndQuery.Should().Contain("organizations/org_001/logos");
        result.Should().NotBeNull();
    }
}