using ApiSdk.Api.V1.Subscribers;
using KiotaTests.Helpers;
using Xunit;

namespace KiotaTests.Api.V1.Subscribers;

public class SubscribersRequestBuilderTests
{
    [Fact]
    public void Constructor_WithPathParameters_CreatesRequestBuilder()
    {
        var requestAdapter = KindeApiTestHelpers.CreateRequestAdapter();
        var pathParameters = KindeApiTestHelpers.CreatePathParameters();

        var builder = new SubscribersRequestBuilder(pathParameters, requestAdapter);

        Assert.NotNull(builder);
    }

    [Fact]
    public void Constructor_WithRawUrl_CreatesRequestBuilder()
    {
        var requestAdapter = KindeApiTestHelpers.CreateRequestAdapter();
        var rawUrl = "https://api.example.test/test";

        var builder = new SubscribersRequestBuilder(rawUrl, requestAdapter);

        Assert.NotNull(builder);
    }
}
