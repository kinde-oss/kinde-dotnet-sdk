using ApiSdk.Api.V1.Connected_apps.Token;
using KiotaTests.Helpers;
using Xunit;

namespace KiotaTests.Api.V1.Connected_apps.Token;

public class TokenRequestBuilderTests
{
    [Fact]
    public void Constructor_WithPathParameters_CreatesRequestBuilder()
    {
        var requestAdapter = KindeApiTestHelpers.CreateRequestAdapter();
        var pathParameters = KindeApiTestHelpers.CreatePathParameters();

        var builder = new TokenRequestBuilder(pathParameters, requestAdapter);

        Assert.NotNull(builder);
    }

    [Fact]
    public void Constructor_WithRawUrl_CreatesRequestBuilder()
    {
        var requestAdapter = KindeApiTestHelpers.CreateRequestAdapter();
        var rawUrl = "https://api.example.test/test";

        var builder = new TokenRequestBuilder(rawUrl, requestAdapter);

        Assert.NotNull(builder);
    }
}
