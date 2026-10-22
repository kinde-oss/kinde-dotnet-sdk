using ApiSdk.Api.V1.Permissions;
using KiotaTests.Helpers;
using Xunit;

namespace KiotaTests.Api.V1.Permissions;

public class PermissionsRequestBuilderTests
{
    [Fact]
    public void Constructor_WithPathParameters_CreatesRequestBuilder()
    {
        var requestAdapter = KindeApiTestHelpers.CreateRequestAdapter();
        var pathParameters = KindeApiTestHelpers.CreatePathParameters();

        var builder = new PermissionsRequestBuilder(pathParameters, requestAdapter);

        Assert.NotNull(builder);
    }

    [Fact]
    public void Constructor_WithRawUrl_CreatesRequestBuilder()
    {
        var requestAdapter = KindeApiTestHelpers.CreateRequestAdapter();
        var rawUrl = "https://api.example.test/test";

        var builder = new PermissionsRequestBuilder(rawUrl, requestAdapter);

        Assert.NotNull(builder);
    }
}
