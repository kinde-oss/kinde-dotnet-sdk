using ApiSdk.Api.V1.Api_keys.Verify;
using KiotaTests.Helpers;
using Xunit;

namespace KiotaTests.Api.V1.Api_keys.Verify;

public class VerifyRequestBuilderTests
{
    [Fact]
    public void Constructor_WithPathParameters_CreatesRequestBuilder()
    {
        var requestAdapter = KindeApiTestHelpers.CreateRequestAdapter();
        var pathParameters = KindeApiTestHelpers.CreatePathParameters();

        var builder = new VerifyRequestBuilder(pathParameters, requestAdapter);

        Assert.NotNull(builder);
    }

    [Fact]
    public void Constructor_WithRawUrl_CreatesRequestBuilder()
    {
        var requestAdapter = KindeApiTestHelpers.CreateRequestAdapter();
        var rawUrl = "https://api.example.test/test";

        var builder = new VerifyRequestBuilder(rawUrl, requestAdapter);

        Assert.NotNull(builder);
    }
}
