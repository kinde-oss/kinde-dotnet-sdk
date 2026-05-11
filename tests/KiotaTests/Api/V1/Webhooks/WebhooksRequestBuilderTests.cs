using ApiSdk.Api.V1.Webhooks;
using KiotaTests.Helpers;
using Xunit;

namespace KiotaTests.Api.V1.Webhooks;

public class WebhooksRequestBuilderTests
{
    [Fact]
    public void Constructor_WithPathParameters_CreatesRequestBuilder()
    {
        var requestAdapter = KindeApiTestHelpers.CreateRequestAdapter();
        var pathParameters = KindeApiTestHelpers.CreatePathParameters();

        var builder = new WebhooksRequestBuilder(pathParameters, requestAdapter);

        Assert.NotNull(builder);
    }

    [Fact]
    public void Constructor_WithRawUrl_CreatesRequestBuilder()
    {
        var requestAdapter = KindeApiTestHelpers.CreateRequestAdapter();
        var rawUrl = "https://api.example.test/test";

        var builder = new WebhooksRequestBuilder(rawUrl, requestAdapter);

        Assert.NotNull(builder);
    }
}
