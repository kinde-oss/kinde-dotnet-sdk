using ApiSdk.Api.V1.Billing.Meter_usage;
using KiotaTests.Helpers;
using Xunit;

namespace KiotaTests.Api.V1.Billing.Meter_usage;

public class Meter_usageRequestBuilderTests
{
    [Fact]
    public void Constructor_WithPathParameters_CreatesRequestBuilder()
    {
        var requestAdapter = KindeApiTestHelpers.CreateRequestAdapter();
        var pathParameters = KindeApiTestHelpers.CreatePathParameters();

        var builder = new Meter_usageRequestBuilder(pathParameters, requestAdapter);

        Assert.NotNull(builder);
    }

    [Fact]
    public void Constructor_WithRawUrl_CreatesRequestBuilder()
    {
        var requestAdapter = KindeApiTestHelpers.CreateRequestAdapter();
        var rawUrl = "https://api.example.test/test";

        var builder = new Meter_usageRequestBuilder(rawUrl, requestAdapter);

        Assert.NotNull(builder);
    }
}
