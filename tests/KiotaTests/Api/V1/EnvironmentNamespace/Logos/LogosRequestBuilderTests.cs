using ApiSdk.Api.V1.EnvironmentNamespace.Logos;
using KiotaTests.Helpers;
using Xunit;

namespace KiotaTests.Api.V1.EnvironmentNamespace.Logos;

public class LogosRequestBuilderTests
{
    [Fact]
    public void Constructor_WithPathParameters_CreatesRequestBuilder()
    {
        var requestAdapter = KindeApiTestHelpers.CreateRequestAdapter();
        var pathParameters = KindeApiTestHelpers.CreatePathParameters();

        var builder = new LogosRequestBuilder(pathParameters, requestAdapter);

        Assert.NotNull(builder);
    }

    [Fact]
    public void Constructor_WithRawUrl_CreatesRequestBuilder()
    {
        var requestAdapter = KindeApiTestHelpers.CreateRequestAdapter();
        var rawUrl = "https://api.example.test/test";

        var builder = new LogosRequestBuilder(rawUrl, requestAdapter);

        Assert.NotNull(builder);
    }
}
