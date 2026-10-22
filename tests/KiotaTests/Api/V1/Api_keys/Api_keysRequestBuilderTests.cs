using ApiSdk.Api.V1.Api_keys;
using KiotaTests.Helpers;
using Xunit;

namespace KiotaTests.Api.V1.Api_keys;

public class Api_keysRequestBuilderTests
{
    [Fact]
    public void Constructor_WithPathParameters_CreatesRequestBuilder()
    {
        var requestAdapter = KindeApiTestHelpers.CreateRequestAdapter();
        var pathParameters = KindeApiTestHelpers.CreatePathParameters();

        var builder = new Api_keysRequestBuilder(pathParameters, requestAdapter);

        Assert.NotNull(builder);
    }

    [Fact]
    public void Constructor_WithRawUrl_CreatesRequestBuilder()
    {
        var requestAdapter = KindeApiTestHelpers.CreateRequestAdapter();
        var rawUrl = "https://api.example.test/test";

        var builder = new Api_keysRequestBuilder(rawUrl, requestAdapter);

        Assert.NotNull(builder);
    }
}
