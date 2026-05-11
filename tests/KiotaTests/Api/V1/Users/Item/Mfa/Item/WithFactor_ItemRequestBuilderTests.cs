using ApiSdk.Api.V1.Users.Item.Mfa.Item;
using KiotaTests.Helpers;
using Xunit;

namespace KiotaTests.Api.V1.Users.Item.Mfa.Item;

public class WithFactor_ItemRequestBuilderTests
{
    [Fact]
    public void Constructor_WithPathParameters_CreatesRequestBuilder()
    {
        var requestAdapter = KindeApiTestHelpers.CreateRequestAdapter();
        var pathParameters = KindeApiTestHelpers.CreatePathParameters();

        var builder = new WithFactor_ItemRequestBuilder(pathParameters, requestAdapter);

        Assert.NotNull(builder);
    }

    [Fact]
    public void Constructor_WithRawUrl_CreatesRequestBuilder()
    {
        var requestAdapter = KindeApiTestHelpers.CreateRequestAdapter();
        var rawUrl = "https://api.example.test/test";

        var builder = new WithFactor_ItemRequestBuilder(rawUrl, requestAdapter);

        Assert.NotNull(builder);
    }
}
