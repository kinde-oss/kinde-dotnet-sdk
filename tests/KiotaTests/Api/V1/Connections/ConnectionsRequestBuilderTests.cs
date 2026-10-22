using ApiSdk.Api.V1.Connections;
using KiotaTests.Helpers;
using Xunit;

namespace KiotaTests.Api.V1.Connections;

public class ConnectionsRequestBuilderTests
{
    [Fact]
    public void Constructor_WithPathParameters_CreatesRequestBuilder()
    {
        var requestAdapter = KindeApiTestHelpers.CreateRequestAdapter();
        var pathParameters = KindeApiTestHelpers.CreatePathParameters();

        var builder = new ConnectionsRequestBuilder(pathParameters, requestAdapter);

        Assert.NotNull(builder);
    }

    [Fact]
    public void Constructor_WithRawUrl_CreatesRequestBuilder()
    {
        var requestAdapter = KindeApiTestHelpers.CreateRequestAdapter();
        var rawUrl = "https://api.example.test/test";

        var builder = new ConnectionsRequestBuilder(rawUrl, requestAdapter);

        Assert.NotNull(builder);
    }
}
