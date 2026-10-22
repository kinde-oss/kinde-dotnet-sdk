using ApiSdk.Api.V1.Users.Item.Refresh_claims;
using KiotaTests.Helpers;
using Xunit;

namespace KiotaTests.Api.V1.Users.Item.Refresh_claims;

public class Refresh_claimsRequestBuilderTests
{
    [Fact]
    public void Constructor_WithPathParameters_CreatesRequestBuilder()
    {
        var requestAdapter = KindeApiTestHelpers.CreateRequestAdapter();
        var pathParameters = KindeApiTestHelpers.CreatePathParameters();

        var builder = new Refresh_claimsRequestBuilder(pathParameters, requestAdapter);

        Assert.NotNull(builder);
    }

    [Fact]
    public void Constructor_WithRawUrl_CreatesRequestBuilder()
    {
        var requestAdapter = KindeApiTestHelpers.CreateRequestAdapter();
        var rawUrl = "https://api.example.test/test";

        var builder = new Refresh_claimsRequestBuilder(rawUrl, requestAdapter);

        Assert.NotNull(builder);
    }
}
