using ApiSdk.Api.V1.Applications.Item.Tokens;
using KiotaTests.Helpers;
using Xunit;

namespace KiotaTests.Api.V1.Applications.Item.Tokens;

public class TokensRequestBuilderTests
{
    [Fact]
    public void Constructor_WithPathParameters_CreatesRequestBuilder()
    {
        var requestAdapter = KindeApiTestHelpers.CreateRequestAdapter();
        var pathParameters = KindeApiTestHelpers.CreatePathParameters();

        var builder = new TokensRequestBuilder(pathParameters, requestAdapter);

        Assert.NotNull(builder);
    }

    [Fact]
    public void Constructor_WithRawUrl_CreatesRequestBuilder()
    {
        var requestAdapter = KindeApiTestHelpers.CreateRequestAdapter();
        var rawUrl = "https://api.example.test/test";

        var builder = new TokensRequestBuilder(rawUrl, requestAdapter);

        Assert.NotNull(builder);
    }
}
