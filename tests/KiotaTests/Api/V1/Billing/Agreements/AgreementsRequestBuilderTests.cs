using ApiSdk.Api.V1.Billing.Agreements;
using KiotaTests.Helpers;
using Xunit;

namespace KiotaTests.Api.V1.Billing.Agreements;

public class AgreementsRequestBuilderTests
{
    [Fact]
    public void Constructor_WithPathParameters_CreatesRequestBuilder()
    {
        var requestAdapter = KindeApiTestHelpers.CreateRequestAdapter();
        var pathParameters = KindeApiTestHelpers.CreatePathParameters();

        var builder = new AgreementsRequestBuilder(pathParameters, requestAdapter);

        Assert.NotNull(builder);
    }

    [Fact]
    public void Constructor_WithRawUrl_CreatesRequestBuilder()
    {
        var requestAdapter = KindeApiTestHelpers.CreateRequestAdapter();
        var rawUrl = "https://api.example.test/test";

        var builder = new AgreementsRequestBuilder(rawUrl, requestAdapter);

        Assert.NotNull(builder);
    }
}
