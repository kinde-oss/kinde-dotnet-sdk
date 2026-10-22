using ApiSdk.Api.V1.Roles.Item.Scopes;
using KiotaTests.Helpers;
using Xunit;

namespace KiotaTests.Api.V1.Roles.Item.Scopes;

public class ScopesRequestBuilderTests
{
    [Fact]
    public void Constructor_WithPathParameters_CreatesRequestBuilder()
    {
        var requestAdapter = KindeApiTestHelpers.CreateRequestAdapter();
        var pathParameters = KindeApiTestHelpers.CreatePathParameters();

        var builder = new ScopesRequestBuilder(pathParameters, requestAdapter);

        Assert.NotNull(builder);
    }

    [Fact]
    public void Constructor_WithRawUrl_CreatesRequestBuilder()
    {
        var requestAdapter = KindeApiTestHelpers.CreateRequestAdapter();
        var rawUrl = "https://api.example.test/test";

        var builder = new ScopesRequestBuilder(rawUrl, requestAdapter);

        Assert.NotNull(builder);
    }
}
