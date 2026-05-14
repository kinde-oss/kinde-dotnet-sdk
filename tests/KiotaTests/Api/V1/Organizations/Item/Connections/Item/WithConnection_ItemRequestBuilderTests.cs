using ApiSdk.Api.V1.Organizations.Item.Connections.Item;
using KiotaTests.Helpers;
using Xunit;

namespace KiotaTests.Api.V1.Organizations.Item.Connections.Item;

public class WithConnection_ItemRequestBuilderTests
{
    [Fact]
    public void Constructor_WithPathParameters_CreatesRequestBuilder()
    {
        var requestAdapter = KindeApiTestHelpers.CreateRequestAdapter();
        var pathParameters = KindeApiTestHelpers.CreatePathParameters();

        var builder = new WithConnection_ItemRequestBuilder(pathParameters, requestAdapter);

        Assert.NotNull(builder);
    }

    [Fact]
    public void Constructor_WithRawUrl_CreatesRequestBuilder()
    {
        var requestAdapter = KindeApiTestHelpers.CreateRequestAdapter();
        var rawUrl = "https://api.example.test/test";

        var builder = new WithConnection_ItemRequestBuilder(rawUrl, requestAdapter);

        Assert.NotNull(builder);
    }
}
