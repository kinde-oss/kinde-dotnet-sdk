// Api/V1/Apis/Item/Scopes/ScopesRequestBuilderTests.cs
using System.Net;
using FluentAssertions;
using Kiota.Api.Models;
using Xunit;

namespace KiotaTests.Api.V1.Apis.Item.Scopes;

public class ScopesRequestBuilderTests
{
    private const string ApiId = "7ccd126599aa422a771abcb341596881";

    // GET /api/v1/apis/{api_id}/scopes
    [Fact]
    public async Task GetAsync_Returns200_WithScopeList()
    {
        var (client, handler) = ApiClientFactory.Create(HttpStatusCode.OK, MockData.GetApiScopesResponse);

        var result = await client.Api.V1.Apis[ApiId].Scopes.GetAsync();

        handler.LastRequest!.Method.Should().Be(HttpMethod.Get);
        handler.LastRequest.RequestUri!.PathAndQuery.Should().Be($"/api/v1/apis/{ApiId}/scopes");
        result.Should().NotBeNull();
        result!.Scopes![0].Key.Should().Be("read:data");
    }

    // POST /api/v1/apis/{api_id}/scopes
    [Fact]
    public async Task PostAsync_Returns200_CreatesScope()
    {
        var (client, handler) = ApiClientFactory.Create(HttpStatusCode.OK,
            """{"code":"OK","scope":{"id":"scope_new"}}""");

        var result = await client.Api.V1.Apis[ApiId].Scopes.PostAsync(new AddAPIScope_request
        {
            Key        = "write:data",
            Description = "Write access"
        });

        handler.LastRequest!.Method.Should().Be(HttpMethod.Post);
        result.Should().NotBeNull();
    }
}
