using ApiSdk.Api.V1.Mfa;
using KiotaTests.Helpers;
using Xunit;

namespace KiotaTests.Api.V1.Mfa;

public class MfaRequestBuilderTests
{
    [Fact]
    public void Constructor_WithPathParameters_CreatesRequestBuilder()
    {
        var requestAdapter = KindeApiTestHelpers.CreateRequestAdapter();
        var pathParameters = KindeApiTestHelpers.CreatePathParameters();

        var builder = new MfaRequestBuilder(pathParameters, requestAdapter);

        Assert.NotNull(builder);
    }

    [Fact]
    public void Constructor_WithRawUrl_CreatesRequestBuilder()
    {
        var requestAdapter = KindeApiTestHelpers.CreateRequestAdapter();
        var rawUrl = "https://api.example.test/test";

        var builder = new MfaRequestBuilder(rawUrl, requestAdapter);

        Assert.NotNull(builder);
    }
}
