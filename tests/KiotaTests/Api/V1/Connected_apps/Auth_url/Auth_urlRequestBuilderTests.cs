using ApiSdk.Api.V1.Connected_apps.Auth_url;
using KiotaTests.Helpers;
using Xunit;

namespace KiotaTests.Api.V1.Connected_apps.Auth_url;

public class Auth_urlRequestBuilderTests
{
    [Fact]
    public void Constructor_WithPathParameters_CreatesRequestBuilder()
    {
        var requestAdapter = KindeApiTestHelpers.CreateRequestAdapter();
        var pathParameters = KindeApiTestHelpers.CreatePathParameters();

        var builder = new Auth_urlRequestBuilder(pathParameters, requestAdapter);

        Assert.NotNull(builder);
    }

    [Fact]
    public void Constructor_WithRawUrl_CreatesRequestBuilder()
    {
        var requestAdapter = KindeApiTestHelpers.CreateRequestAdapter();
        var rawUrl = "https://api.example.test/test";

        var builder = new Auth_urlRequestBuilder(rawUrl, requestAdapter);

        Assert.NotNull(builder);
    }
}
