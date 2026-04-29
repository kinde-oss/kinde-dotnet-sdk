// Api/V1/Apis/Item/Applications/ApplicationsRequestBuilderTests.cs
using System.Net;
using FluentAssertions;
using Kiota.Api.Models;
using Xunit;

namespace KiotaTests.Api.V1.Apis.Item.Applications;

public class ApplicationsRequestBuilderTests
{
    private const string ApiId = "7ccd126599aa422a771abcb341596881";

    // PATCH /api/v1/apis/{api_id}/applications
    [Fact]
    public async Task PatchAsync_Returns200_UpdatesApplications()
    {
        var (client, handler) = ApiClientFactory.Create(HttpStatusCode.OK, MockData.SuccessResponse);

        await client.Api.V1.Apis[ApiId].Applications.PatchAsync(new UpdateAPIApplications_request());

        handler.LastRequest!.Method.Should().Be(HttpMethod.Patch);
        handler.LastRequest.RequestUri!.PathAndQuery.Should().Be($"/api/v1/apis/{ApiId}/applications");
    }
}


public class WithScope_ItemRequestBuilderTests
{
    private const string ApiId   = "7ccd126599aa422a771abcb341596881";
    private const string AppId   = "3b0b5c6c8fcc464fab397f4969b5f482";
    private const string ScopeId = "scope_001";

    // POST /api/v1/apis/{api_id}/applications/{app_id}/scopes/{scope_id}
    [Fact]
    public async Task PostAsync_Returns200_AddsScope()
    {
        var (client, handler) = ApiClientFactory.Create(HttpStatusCode.OK, MockData.SuccessResponse);

        await client.Api.V1.Apis[ApiId].Applications[AppId].Scopes[ScopeId].PostAsync();

        handler.LastRequest!.Method.Should().Be(HttpMethod.Post);
        handler.LastRequest.RequestUri!.PathAndQuery
            .Should().Contain($"/applications/{AppId}/scopes/{ScopeId}");
    }

    // DELETE /api/v1/apis/{api_id}/applications/{app_id}/scopes/{scope_id}
    [Fact]
    public async Task DeleteAsync_Returns200_RemovesScope()
    {
        var (client, handler) = ApiClientFactory.Create(HttpStatusCode.OK, MockData.SuccessResponse);

        await client.Api.V1.Apis[ApiId].Applications[AppId].Scopes[ScopeId].DeleteAsync();

        handler.LastRequest!.Method.Should().Be(HttpMethod.Delete);
        handler.LastRequest.RequestUri!.PathAndQuery
            .Should().Contain($"/applications/{AppId}/scopes/{ScopeId}");
    }
}
