using System.Net;
using FluentAssertions;
using Kiota.Api.Models;
using Xunit;

namespace KiotaTests.Api.V1.Permissions.Item;
public class WithPermission_ItemRequestBuilderTests
{
    private const string PermId = "perm_001";
    [Fact]
    public async Task PatchAsync_Returns200()
    {
        var (client, handler) = ApiClientFactory.Create(HttpStatusCode.OK, MockData.SuccessResponse);
        await client.Api.V1.Permissions[PermId].PatchAsync(new CreatePermission_request());
        handler.LastRequest!.Method.Should().Be(HttpMethod.Patch);
    }
    [Fact]
    public async Task DeleteAsync_Returns200()
    {
        var (client, handler) = ApiClientFactory.Create(HttpStatusCode.OK, MockData.SuccessResponse);
        await client.Api.V1.Permissions[PermId].DeleteAsync();
        handler.LastRequest!.Method.Should().Be(HttpMethod.Delete);
    }
}
