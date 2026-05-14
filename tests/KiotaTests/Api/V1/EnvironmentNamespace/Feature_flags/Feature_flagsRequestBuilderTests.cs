using ApiSdk.Api.V1.EnvironmentNamespace.Feature_flags;
using KiotaTests.Helpers;
using Xunit;

namespace KiotaTests.Api.V1.EnvironmentNamespace.Feature_flags;

public class Feature_flagsRequestBuilderTests
{
    [Fact]
    public void Constructor_WithPathParameters_CreatesRequestBuilder()
    {
        var requestAdapter = KindeApiTestHelpers.CreateRequestAdapter();
        var pathParameters = KindeApiTestHelpers.CreatePathParameters();

        var builder = new Feature_flagsRequestBuilder(pathParameters, requestAdapter);

        Assert.NotNull(builder);
    }

    [Fact]
    public void Constructor_WithRawUrl_CreatesRequestBuilder()
    {
        var requestAdapter = KindeApiTestHelpers.CreateRequestAdapter();
        var rawUrl = "https://api.example.test/test";

        var builder = new Feature_flagsRequestBuilder(rawUrl, requestAdapter);

        Assert.NotNull(builder);
    }
}
