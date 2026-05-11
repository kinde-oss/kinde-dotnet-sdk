using ApiSdk.Api.V1.EnvironmentNamespace;
using KiotaTests.Helpers;
using Xunit;

namespace KiotaTests.Api.V1.EnvironmentNamespace;

public class EnvironmentRequestBuilderTests
{
    [Fact]
    public void Constructor_WithPathParameters_CreatesRequestBuilder()
    {
        var requestAdapter = KindeApiTestHelpers.CreateRequestAdapter();
        var pathParameters = KindeApiTestHelpers.CreatePathParameters();

        var builder = new EnvironmentRequestBuilder(pathParameters, requestAdapter);

        Assert.NotNull(builder);
    }

    [Fact]
    public void Constructor_WithRawUrl_CreatesRequestBuilder()
    {
        var requestAdapter = KindeApiTestHelpers.CreateRequestAdapter();
        var rawUrl = "https://api.example.test/test";

        var builder = new EnvironmentRequestBuilder(rawUrl, requestAdapter);

        Assert.NotNull(builder);
    }
}
