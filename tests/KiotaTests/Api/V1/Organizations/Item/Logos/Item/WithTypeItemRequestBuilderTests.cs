using FluentAssertions;
using Microsoft.Kiota.Abstractions;
using System.Net;
using Xunit;

namespace KiotaTests.Api.V1.Organizations.Item.Logos.Item;

public class WithTypeItemRequestBuilderTests
{
    [Fact]
    public async Task PostAsync_ThrowsInvalidOperationException_ForMultipartBodyMismatch()
    {
        var (client, handler) = ApiClientFactory.Create(HttpStatusCode.OK, MockData.SuccessResponse);

        var body = new MultipartBody();
        body.AddOrReplacePart(
            "logo",
            "image/png",
            System.Text.Encoding.UTF8.GetBytes("fake-image-bytes")
        );

        await client.Api.V1.Organizations["org_001"].Logos["light"].PostAsync(body);

        handler.LastRequest!.Method.Should().Be(HttpMethod.Post);
        handler.LastRequest.RequestUri!.ToString().Should().Contain("/logos/light");
    }

    [Fact]
    public async Task DeleteAsync_Returns200()
    {
        var (client, handler) = ApiClientFactory.Create(HttpStatusCode.OK, MockData.SuccessResponse);

        await client.Api.V1.Organizations["org_001"].Logos["light"].DeleteAsync();

        handler.LastRequest!.Method.Should().Be(HttpMethod.Delete);
        handler.LastRequest.RequestUri!.ToString().Should().Contain("/logos/light");
    }
}