using ApiSdk.Api.V1.Industries;
using KiotaTests.Helpers;
using Xunit;

namespace KiotaTests.Api.V1.Industries;

public class IndustriesRequestBuilderTests
{
    [Fact]
    public void Constructor_WithPathParameters_CreatesRequestBuilder()
    {
        var requestAdapter = KindeApiTestHelpers.CreateRequestAdapter();
        var pathParameters = KindeApiTestHelpers.CreatePathParameters();

        var builder = new IndustriesRequestBuilder(pathParameters, requestAdapter);

        Assert.NotNull(builder);
    }

    [Fact]
    public void Constructor_WithRawUrl_CreatesRequestBuilder()
    {
        var requestAdapter = KindeApiTestHelpers.CreateRequestAdapter();
        var rawUrl = "https://api.example.test/test";

        var builder = new IndustriesRequestBuilder(rawUrl, requestAdapter);

        Assert.NotNull(builder);
    }
}
