using ApiSdk.Api.V1.Environment_variables;
using KiotaTests.Helpers;
using Xunit;

namespace KiotaTests.Api.V1.Environment_variables;

public class Environment_variablesRequestBuilderTests
{
    [Fact]
    public void Constructor_WithPathParameters_CreatesRequestBuilder()
    {
        var requestAdapter = KindeApiTestHelpers.CreateRequestAdapter();
        var pathParameters = KindeApiTestHelpers.CreatePathParameters();

        var builder = new Environment_variablesRequestBuilder(pathParameters, requestAdapter);

        Assert.NotNull(builder);
    }

    [Fact]
    public void Constructor_WithRawUrl_CreatesRequestBuilder()
    {
        var requestAdapter = KindeApiTestHelpers.CreateRequestAdapter();
        var rawUrl = "https://api.example.test/test";

        var builder = new Environment_variablesRequestBuilder(rawUrl, requestAdapter);

        Assert.NotNull(builder);
    }
}
