using System.Net; using FluentAssertions; using Kiota.Api.Models; using Xunit;
namespace KiotaTests.Api.V1.Applications.Item.Auth_redirect_urls;
public class Auth_redirect_urlsRequestBuilderTests
{
    private const string AppId = "app_001";
    [Fact] public async Task GetAsync_Returns200() { var (client, handler) = ApiClientFactory.Create(HttpStatusCode.OK, MockData.GetRedirectUrlsResponse); var result = await client.Api.V1.Applications[AppId].Auth_redirect_urls.GetAsync(); handler.LastRequest!.RequestUri!.PathAndQuery.Should().Contain("auth_redirect_urls"); result.Should().NotBeNull(); }
    [Fact] public async Task PostAsync_Returns200() { var (client, handler) = ApiClientFactory.Create(HttpStatusCode.OK, MockData.SuccessResponse); await client.Api.V1.Applications[AppId].Auth_redirect_urls.PostAsync(new ReplaceRedirectCallbackURLs_request { Urls = new List<string> { "https://app.com/cb" } }); handler.LastRequest!.Method.Should().Be(HttpMethod.Post); }
    [Fact] public async Task PutAsync_Returns200() { var (client, handler) = ApiClientFactory.Create(HttpStatusCode.OK, MockData.SuccessResponse); await client.Api.V1.Applications[AppId].Auth_redirect_urls.PutAsync(new ReplaceRedirectCallbackURLs_request { Urls = new List<string> { "https://app.com/cb" } }); handler.LastRequest!.Method.Should().Be(HttpMethod.Put); }
    [Fact]
    public async Task DeleteAsync_Returns200()
    {
        var (client, handler) = ApiClientFactory.Create(HttpStatusCode.OK, MockData.SuccessResponse);

        await client.Api.V1.Applications[AppId].Auth_redirect_urls.DeleteAsync(c =>
            c.QueryParameters.Urls = "https://app.com/cb");

        handler.LastRequest!.Method.Should().Be(HttpMethod.Delete);
    }
}
