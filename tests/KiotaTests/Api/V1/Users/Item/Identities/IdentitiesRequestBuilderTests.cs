using ApiSdk.Api.V1.Users.Item.Identities;
using KiotaTests.Helpers;
using Xunit;

namespace KiotaTests.Api.V1.Users.Item.Identities;

public class IdentitiesRequestBuilderTests
{
    [Fact]
    public void Constructor_WithPathParameters_CreatesRequestBuilder()
    {
        var requestAdapter = KindeApiTestHelpers.CreateRequestAdapter();
        var pathParameters = KindeApiTestHelpers.CreatePathParameters();

        var builder = new IdentitiesRequestBuilder(pathParameters, requestAdapter);

        Assert.NotNull(builder);
    }

    [Fact]
    public void Constructor_WithRawUrl_CreatesRequestBuilder()
    {
        var requestAdapter = KindeApiTestHelpers.CreateRequestAdapter();
        var rawUrl = "https://api.example.test/test";

        var builder = new IdentitiesRequestBuilder(rawUrl, requestAdapter);

        Assert.NotNull(builder);
    }
}
