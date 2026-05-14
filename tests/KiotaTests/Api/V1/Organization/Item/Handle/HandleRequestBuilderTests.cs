using ApiSdk.Api.V1.Organization.Item.Handle;
using KiotaTests.Helpers;
using Xunit;

namespace KiotaTests.Api.V1.Organization.Item.Handle;

public class HandleRequestBuilderTests
{
    [Fact]
    public void Constructor_WithPathParameters_CreatesRequestBuilder()
    {
        var requestAdapter = KindeApiTestHelpers.CreateRequestAdapter();
        var pathParameters = KindeApiTestHelpers.CreatePathParameters();

        var builder = new HandleRequestBuilder(pathParameters, requestAdapter);

        Assert.NotNull(builder);
    }

    [Fact]
    public void Constructor_WithRawUrl_CreatesRequestBuilder()
    {
        var requestAdapter = KindeApiTestHelpers.CreateRequestAdapter();
        var rawUrl = "https://api.example.test/test";

        var builder = new HandleRequestBuilder(rawUrl, requestAdapter);

        Assert.NotNull(builder);
    }
}
