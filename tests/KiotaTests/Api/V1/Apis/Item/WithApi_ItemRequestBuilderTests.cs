using ApiSdk.Api.V1.Apis.Item;
using KiotaTests.Helpers;
using Xunit;

namespace KiotaTests.Api.V1.Apis.Item;

public class WithApi_ItemRequestBuilderTests
{
    [Fact]
    public void Constructor_WithPathParameters_CreatesRequestBuilder()
    {
        var requestAdapter = KindeApiTestHelpers.CreateRequestAdapter();
        var pathParameters = KindeApiTestHelpers.CreatePathParameters();

        var builder = new WithApi_ItemRequestBuilder(pathParameters, requestAdapter);

        Assert.NotNull(builder);
    }

    [Fact]
    public void Constructor_WithRawUrl_CreatesRequestBuilder()
    {
        var requestAdapter = KindeApiTestHelpers.CreateRequestAdapter();
        var rawUrl = "https://api.example.test/test";

        var builder = new WithApi_ItemRequestBuilder(rawUrl, requestAdapter);

        Assert.NotNull(builder);
    }
}
