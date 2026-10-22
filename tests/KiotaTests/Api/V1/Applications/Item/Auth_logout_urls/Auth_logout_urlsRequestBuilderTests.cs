using ApiSdk.Api.V1.Applications.Item.Auth_logout_urls;
using KiotaTests.Helpers;
using Xunit;

namespace KiotaTests.Api.V1.Applications.Item.Auth_logout_urls;

public class Auth_logout_urlsRequestBuilderTests
{
    [Fact]
    public void Constructor_WithPathParameters_CreatesRequestBuilder()
    {
        var requestAdapter = KindeApiTestHelpers.CreateRequestAdapter();
        var pathParameters = KindeApiTestHelpers.CreatePathParameters();

        var builder = new Auth_logout_urlsRequestBuilder(pathParameters, requestAdapter);

        Assert.NotNull(builder);
    }

    [Fact]
    public void Constructor_WithRawUrl_CreatesRequestBuilder()
    {
        var requestAdapter = KindeApiTestHelpers.CreateRequestAdapter();
        var rawUrl = "https://api.example.test/test";

        var builder = new Auth_logout_urlsRequestBuilder(rawUrl, requestAdapter);

        Assert.NotNull(builder);
    }
}
