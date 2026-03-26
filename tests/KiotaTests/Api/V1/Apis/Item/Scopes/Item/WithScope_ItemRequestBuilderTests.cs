// Api/V1/Apis/Item/Scopes/Item/WithScope_ItemRequestBuilderTests.cs
using System.Net;
using FluentAssertions;
using Kiota.Api.Models;
using Xunit;

namespace KiotaTests.Api.V1.Apis.Item.Scopes.Item;

public class WithScope_ItemRequestBuilderTests
{
    private const string ApiId   = "7ccd126599aa422a771abcb341596881";
    private const string ScopeId = "scope_001";

    // GET /api/v1/apis/{api_id}/scopes/{scope_id}
    [Fact]
    public async Task GetAsync_Returns200_WithScopeDetails()
    {
        var (client, handler) = ApiClientFactory.Create(HttpStatusCode.OK,
            """{"code":"OK","scope":{"id":"scope_001","name":"read:data","description":"Read access"}}""");

        var result = await client.Api.V1.Apis[ApiId].Scopes[ScopeId].GetAsync();

        handler.LastRequest!.Method.Should().Be(HttpMethod.Get);
        handler.LastRequest.RequestUri!.PathAndQuery.Should().Contain($"/scopes/{ScopeId}");
        result.Should().NotBeNull();
    }

    // PATCH /api/v1/apis/{api_id}/scopes/{scope_id}
    [Fact]
    public async Task PatchAsync_Returns200_UpdatesScope()
    {
        var (client, handler) = ApiClientFactory.Create(HttpStatusCode.OK, MockData.SuccessResponse);

        await client.Api.V1.Apis[ApiId].Scopes[ScopeId].PatchAsync(new UpdateAPIScope_request
        {
            Description = "Updated description"
        });

        handler.LastRequest!.Method.Should().Be(HttpMethod.Patch);
        handler.LastRequest.RequestUri!.PathAndQuery.Should().Contain($"/scopes/{ScopeId}");
    }

    // DELETE /api/v1/apis/{api_id}/scopes/{scope_id}
    [Fact]
    public async Task DeleteAsync_Returns200_DeletesScope()
    {
        var (client, handler) = ApiClientFactory.Create(HttpStatusCode.OK, MockData.SuccessResponse);

        await client.Api.V1.Apis[ApiId].Scopes[ScopeId].DeleteAsync();

        handler.LastRequest!.Method.Should().Be(HttpMethod.Delete);
        handler.LastRequest.RequestUri!.PathAndQuery.Should().Contain($"/scopes/{ScopeId}");
    }
}
