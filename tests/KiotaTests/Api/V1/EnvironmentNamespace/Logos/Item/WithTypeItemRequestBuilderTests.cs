using FluentAssertions;
using Microsoft.Kiota.Abstractions;
using System.Net;
using Xunit;

namespace KiotaTests.Api.V1.EnvironmentNamespace.Logos.Item;

public class WithTypeItemRequestBuilderTests
{
    [Fact]
    public async Task PutAsync_Returns200()
    {
        var (client, handler) = ApiClientFactory.Create(HttpStatusCode.OK, MockData.SuccessResponse);

        var body = new MultipartBody();
        body.AddOrReplacePart(
            "logo",
            "image/png",
            System.Text.Encoding.UTF8.GetBytes("fake-image-bytes")
        );

        await client.Api.V1.Environment.Logos["light"].PutAsync(body);

        handler.LastRequest!.Method.Should().Be(HttpMethod.Put);
        handler.LastRequest.RequestUri!.ToString().Should().Contain("/logos/light");
    }

    [Fact]
    public async Task DeleteAsync_Returns200()
    {
        var (client, handler) = ApiClientFactory.Create(HttpStatusCode.OK, MockData.SuccessResponse);

        await client.Api.V1.Environment.Logos["light"].DeleteAsync();

        handler.LastRequest!.Method.Should().Be(HttpMethod.Delete);
        handler.LastRequest.RequestUri!.ToString().Should().Contain("/logos/light");
    }
}