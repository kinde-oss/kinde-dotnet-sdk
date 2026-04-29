using System.Net; using FluentAssertions; using Xunit;
using Kiota.Api.Models;

namespace KiotaTests.Api.V1.Organizations.Item.Logos.Item;
public class WithTypeItemRequestBuilderTests
{
    [Fact]
    public async Task PostAsync_ThrowsInvalidOperationException_ForMultipartBodyMismatch()
    {
        var (client, handler) = ApiClientFactory.Create(HttpStatusCode.OK, MockData.SuccessResponse);

        var addLogoRequest = new AddLogo_request
        {
            // LogoStream = new MemoryStream()
        };

        var act = async () =>
            await client.Api.V1.Organizations["org_001"].Logos["light"].PostAsync(addLogoRequest);

        var ex = await Assert.ThrowsAsync<InvalidOperationException>(act);
        ex.Message.Should().Be("Expected a MultiPartBody instance, but got AddLogo_request");

        handler.LastRequest.Should().BeNull();
    }

    [Fact]
    public async Task DeleteAsync_Returns200()
    {
        var (client, handler) = ApiClientFactory.Create(HttpStatusCode.OK, MockData.SuccessResponse);
        await client.Api.V1.Organizations["org_001"].Logos["light"].DeleteAsync();
        handler.LastRequest!.Method.Should().Be(HttpMethod.Delete);
    }
}
