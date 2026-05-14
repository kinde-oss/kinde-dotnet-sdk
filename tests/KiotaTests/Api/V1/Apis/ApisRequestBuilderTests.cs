using ApiSdk.Api.V1.Apis;
using KiotaTests.Helpers;
using Xunit;

namespace KiotaTests.Api.V1.Apis;

public class ApisRequestBuilderTests
{
    [Fact]
    public void Constructor_WithPathParameters_CreatesRequestBuilder()
    {
        var requestAdapter = KindeApiTestHelpers.CreateRequestAdapter();
        var pathParameters = KindeApiTestHelpers.CreatePathParameters();

        var builder = new ApisRequestBuilder(pathParameters, requestAdapter);

        Assert.NotNull(builder);
    }

    [Fact]
    public void Constructor_WithRawUrl_CreatesRequestBuilder()
    {
        var requestAdapter = KindeApiTestHelpers.CreateRequestAdapter();
        var rawUrl = "https://api.example.test/test";

        var builder = new ApisRequestBuilder(rawUrl, requestAdapter);

        Assert.NotNull(builder);
    }
}
