using ApiSdk.Api.V1.Business;
using KiotaTests.Helpers;
using Xunit;

namespace KiotaTests.Api.V1.Business;

public class BusinessRequestBuilderTests
{
    [Fact]
    public void Constructor_WithPathParameters_CreatesRequestBuilder()
    {
        var requestAdapter = KindeApiTestHelpers.CreateRequestAdapter();
        var pathParameters = KindeApiTestHelpers.CreatePathParameters();

        var builder = new BusinessRequestBuilder(pathParameters, requestAdapter);

        Assert.NotNull(builder);
    }

    [Fact]
    public void Constructor_WithRawUrl_CreatesRequestBuilder()
    {
        var requestAdapter = KindeApiTestHelpers.CreateRequestAdapter();
        var rawUrl = "https://api.example.test/test";

        var builder = new BusinessRequestBuilder(rawUrl, requestAdapter);

        Assert.NotNull(builder);
    }
}
