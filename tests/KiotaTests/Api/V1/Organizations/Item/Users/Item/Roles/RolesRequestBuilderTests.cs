using ApiSdk.Api.V1.Organizations.Item.Users.Item.Roles;
using KiotaTests.Helpers;
using Xunit;

namespace KiotaTests.Api.V1.Organizations.Item.Users.Item.Roles;

public class RolesRequestBuilderTests
{
    [Fact]
    public void Constructor_WithPathParameters_CreatesRequestBuilder()
    {
        var requestAdapter = KindeApiTestHelpers.CreateRequestAdapter();
        var pathParameters = KindeApiTestHelpers.CreatePathParameters();

        var builder = new RolesRequestBuilder(pathParameters, requestAdapter);

        Assert.NotNull(builder);
    }

    [Fact]
    public void Constructor_WithRawUrl_CreatesRequestBuilder()
    {
        var requestAdapter = KindeApiTestHelpers.CreateRequestAdapter();
        var rawUrl = "https://api.example.test/test";

        var builder = new RolesRequestBuilder(rawUrl, requestAdapter);

        Assert.NotNull(builder);
    }
}
