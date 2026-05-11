using ApiSdk.Api.V1.Billing.Entitlements;
using KiotaTests.Helpers;
using Xunit;

namespace KiotaTests.Api.V1.Billing.Entitlements;

public class EntitlementsRequestBuilderTests
{
    [Fact]
    public void Constructor_WithPathParameters_CreatesRequestBuilder()
    {
        var requestAdapter = KindeApiTestHelpers.CreateRequestAdapter();
        var pathParameters = KindeApiTestHelpers.CreatePathParameters();

        var builder = new EntitlementsRequestBuilder(pathParameters, requestAdapter);

        Assert.NotNull(builder);
    }

    [Fact]
    public void Constructor_WithRawUrl_CreatesRequestBuilder()
    {
        var requestAdapter = KindeApiTestHelpers.CreateRequestAdapter();
        var rawUrl = "https://api.example.test/test";

        var builder = new EntitlementsRequestBuilder(rawUrl, requestAdapter);

        Assert.NotNull(builder);
    }
}
