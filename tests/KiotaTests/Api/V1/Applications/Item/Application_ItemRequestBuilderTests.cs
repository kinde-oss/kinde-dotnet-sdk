// Api/V1/Applications/Item/Application_ItemRequestBuilderTests.cs
using System.Net;
using FluentAssertions;
using Kiota.Api.Models;
using Xunit;
namespace KiotaTests.Api.V1.Applications.Item;
public class Application_ItemRequestBuilderTests
{
    private const string AppId = "3b0b5c6c8fcc464fab397f4969b5f482";
    [Fact]
    public async Task GetAsync_Returns200_WithDetails()
    {
        var (client, handler) = ApiClientFactory.Create(HttpStatusCode.OK, MockData.GetApplicationResponse);
        var result = await client.Api.V1.Applications[AppId].GetAsync();
        handler.LastRequest!.RequestUri!.PathAndQuery.Should().Be($"/api/v1/applications/{AppId}");
        result!.Application!.HomepageUri.Should().Be("https://yourapp.com");
    }
    [Fact]
    public async Task PatchAsync_Returns200()
    {
        var (client, handler) = ApiClientFactory.Create(HttpStatusCode.OK, MockData.SuccessResponse);
        await client.Api.V1.Applications[AppId].PatchAsync(new UpdateApplication_request { Name = "Updated" });
        handler.LastRequest!.Method.Should().Be(HttpMethod.Patch);
    }
    [Fact]
    public async Task DeleteAsync_Returns200()
    {
        var (client, handler) = ApiClientFactory.Create(HttpStatusCode.OK, MockData.SuccessResponse);
        await client.Api.V1.Applications[AppId].DeleteAsync();
        handler.LastRequest!.Method.Should().Be(HttpMethod.Delete);
    }
}
