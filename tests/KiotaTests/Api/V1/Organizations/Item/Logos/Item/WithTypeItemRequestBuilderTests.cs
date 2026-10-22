using ApiSdk.Api.V1.Organizations.Item.Logos.Item;
using KiotaTests.Helpers;
using Xunit;

namespace KiotaTests.Api.V1.Organizations.Item.Logos.Item;

public class WithTypeItemRequestBuilderTests
{
    [Fact]
    public void Constructor_WithPathParameters_CreatesRequestBuilder()
    {
        var requestAdapter = KindeApiTestHelpers.CreateRequestAdapter();
        var pathParameters = KindeApiTestHelpers.CreatePathParameters();

        var builder = new WithTypeItemRequestBuilder(pathParameters, requestAdapter);

        Assert.NotNull(builder);
    }

    [Fact]
    public void Constructor_WithRawUrl_CreatesRequestBuilder()
    {
        var requestAdapter = KindeApiTestHelpers.CreateRequestAdapter();
        var rawUrl = "https://api.example.test/test";

        var builder = new WithTypeItemRequestBuilder(rawUrl, requestAdapter);

        Assert.NotNull(builder);
    }
}
