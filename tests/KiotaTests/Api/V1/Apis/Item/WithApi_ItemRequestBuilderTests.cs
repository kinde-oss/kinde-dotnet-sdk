// Api/V1/Apis/Item/WithApi_ItemRequestBuilderTests.cs
using System.Net;
using FluentAssertions;
using Xunit;

namespace KiotaTests.Api.V1.Apis.Item;

public class WithApi_ItemRequestBuilderTests
{
    private const string ApiId = "7ccd126599aa422a771abcb341596881";

    // GET /api/v1/apis/{api_id}
    [Fact]
    public async Task GetAsync_Returns200_WithApiDetails()
    {
        var (client, handler) = ApiClientFactory.Create(HttpStatusCode.OK, MockData.GetApiResponse);

        var result = await client.Api.V1.Apis[ApiId].GetAsync();

        handler.LastRequest!.Method.Should().Be(HttpMethod.Get);
        handler.LastRequest.RequestUri!.PathAndQuery.Should().Be($"/api/v1/apis/{ApiId}");
        result.Should().NotBeNull();
        result!.Api!.Id.Should().Be(ApiId);
        result.Api.Name.Should().Be("My Test API");
    }

    [Fact]
    public async Task GetAsync_WhenUnauthorized_Throws()
    {
        var (client, _) = ApiClientFactory.Create(HttpStatusCode.Forbidden, MockData.Error403);

        var act = async () => await client.Api.V1.Apis[ApiId].GetAsync();

        await act.Should().ThrowAsync<Exception>();
    }

    // DELETE /api/v1/apis/{api_id}
    [Fact]
    public async Task DeleteAsync_Returns200_WithSuccessMessage()
    {
        var (client, handler) = ApiClientFactory.Create(HttpStatusCode.OK, MockData.DeleteApiResponse);

        var result = await client.Api.V1.Apis[ApiId].DeleteAsync();

        handler.LastRequest!.Method.Should().Be(HttpMethod.Delete);
        handler.LastRequest.RequestUri!.PathAndQuery.Should().Be($"/api/v1/apis/{ApiId}");
        result.Should().NotBeNull();
        result!.Message.Should().Contain("deleted");
    }
}
