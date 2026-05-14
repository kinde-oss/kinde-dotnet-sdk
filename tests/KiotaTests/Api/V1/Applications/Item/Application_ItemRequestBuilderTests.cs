using ApiSdk.Api.V1.Applications.Item;
using KiotaTests.Helpers;
using Xunit;

namespace KiotaTests.Api.V1.Applications.Item;

public class Application_ItemRequestBuilderTests
{
    [Fact]
    public void Constructor_WithPathParameters_CreatesRequestBuilder()
    {
        var requestAdapter = KindeApiTestHelpers.CreateRequestAdapter();
        var pathParameters = KindeApiTestHelpers.CreatePathParameters();

        var builder = new Application_ItemRequestBuilder(pathParameters, requestAdapter);

        Assert.NotNull(builder);
    }

    [Fact]
    public void Constructor_WithRawUrl_CreatesRequestBuilder()
    {
        var requestAdapter = KindeApiTestHelpers.CreateRequestAdapter();
        var rawUrl = "https://api.example.test/test";

        var builder = new Application_ItemRequestBuilder(rawUrl, requestAdapter);

        Assert.NotNull(builder);
    }
}
