using ApiSdk.Api.V1.Billing;
using KiotaTests.Helpers;
using Xunit;

namespace KiotaTests.Api.V1.Billing;

public class BillingRequestBuilderTests
{
    [Fact]
    public void Constructor_WithPathParameters_CreatesRequestBuilder()
    {
        var requestAdapter = KindeApiTestHelpers.CreateRequestAdapter();
        var pathParameters = KindeApiTestHelpers.CreatePathParameters();

        var builder = new BillingRequestBuilder(pathParameters, requestAdapter);

        Assert.NotNull(builder);
    }

    [Fact]
    public void Constructor_WithRawUrl_CreatesRequestBuilder()
    {
        var requestAdapter = KindeApiTestHelpers.CreateRequestAdapter();
        var rawUrl = "https://api.example.test/test";

        var builder = new BillingRequestBuilder(rawUrl, requestAdapter);

        Assert.NotNull(builder);
    }
}
