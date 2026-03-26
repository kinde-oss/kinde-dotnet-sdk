// Api/V1/Apis/ApisRequestBuilderTests.cs
using System.Net;
using FluentAssertions;
using Kiota.Api.Models;
using Xunit;

namespace KiotaTests.Api.V1.Apis;

public class ApisRequestBuilderTests
{
    // GET /api/v1/apis
    [Fact]
    public async Task GetAsync_Returns200_WithApiList()
    {
        var (client, handler) = ApiClientFactory.Create(HttpStatusCode.OK, MockData.GetApisResponse);

        var result = await client.Api.V1.Apis.GetAsync();

        handler.LastRequest!.Method.Should().Be(HttpMethod.Get);
        handler.LastRequest.RequestUri!.PathAndQuery.Should().Be("/api/v1/apis");
        result.Should().NotBeNull();
        result!.Apis.Should().NotBeNullOrEmpty();
        result.Apis![0].Id.Should().Be("7ccd126599aa422a771abcb341596881");
        result.Apis![0].Name.Should().Be("My Test API");
    }

    [Fact]
    public async Task GetAsync_WithScopesExpand_AddsQueryParam()
    {
        var (client, handler) = ApiClientFactory.Create(HttpStatusCode.OK, MockData.GetApisResponse);

        await client.Api.V1.Apis.GetAsync(config =>
            config.QueryParameters.Expand = "scopes");

        handler.LastRequest!.RequestUri!.Query.Should().Contain("expand=scopes");
    }

    [Fact]
    public async Task GetAsync_WhenUnauthorized_Throws()
    {
        var (client, _) = ApiClientFactory.Create(HttpStatusCode.Forbidden, MockData.Error403);

        var act = async () => await client.Api.V1.Apis.GetAsync();

        await act.Should().ThrowAsync<Exception>();
    }

    [Fact]
    public async Task GetAsync_WhenRateLimited_Throws()
    {
        var (client, _) = ApiClientFactory.Create(HttpStatusCode.TooManyRequests, MockData.Error429);

        var act = async () => await client.Api.V1.Apis.GetAsync();

        await act.Should().ThrowAsync<Exception>();
    }

    // POST /api/v1/apis
    [Fact]
    public async Task PostAsync_Returns200_WithNewApiId()
    {
        var (client, handler) = ApiClientFactory.Create(HttpStatusCode.OK, MockData.CreateApiResponse);

        var result = await client.Api.V1.Apis.PostAsync(new AddAPIs_request
        {
            Name     = "New API",
            Audience = "https://new.api.com"
        });

        handler.LastRequest!.Method.Should().Be(HttpMethod.Post);
        result.Should().NotBeNull();
        result!.Api!.Id.Should().Be("new_api_id_001");
    }

    [Fact]
    public async Task PostAsync_WithInvalidBody_Throws()
    {
        var (client, _) = ApiClientFactory.Create(HttpStatusCode.BadRequest, MockData.Error400);

        var act = async () => await client.Api.V1.Apis.PostAsync(new AddAPIs_request());

        await act.Should().ThrowAsync<Exception>();
    }
}
