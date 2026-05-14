using ApiSdk.Api.V1.Apis.Item.Applications;
using KiotaTests.Helpers;
using Xunit;

namespace KiotaTests.Api.V1.Apis.Item.Applications;

public class ApplicationsRequestBuilderTests
{
    [Fact]
    public void Constructor_WithPathParameters_CreatesRequestBuilder()
    {
        var requestAdapter = KindeApiTestHelpers.CreateRequestAdapter();
        var pathParameters = KindeApiTestHelpers.CreatePathParameters();

        var builder = new ApplicationsRequestBuilder(pathParameters, requestAdapter);

        Assert.NotNull(builder);
    }

    [Fact]
    public void Constructor_WithRawUrl_CreatesRequestBuilder()
    {
        var requestAdapter = KindeApiTestHelpers.CreateRequestAdapter();
        var rawUrl = "https://api.example.test/test";

        var builder = new ApplicationsRequestBuilder(rawUrl, requestAdapter);

        Assert.NotNull(builder);
    }
}
