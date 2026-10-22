using ApiSdk.Api.V1.Users;
using KiotaTests.Helpers;
using Xunit;

namespace KiotaTests.Api.V1.Users;

public class UsersRequestBuilderTests
{
    [Fact]
    public void Constructor_WithPathParameters_CreatesRequestBuilder()
    {
        var requestAdapter = KindeApiTestHelpers.CreateRequestAdapter();
        var pathParameters = KindeApiTestHelpers.CreatePathParameters();

        var builder = new UsersRequestBuilder(pathParameters, requestAdapter);

        Assert.NotNull(builder);
    }

    [Fact]
    public void Constructor_WithRawUrl_CreatesRequestBuilder()
    {
        var requestAdapter = KindeApiTestHelpers.CreateRequestAdapter();
        var rawUrl = "https://api.example.test/test";

        var builder = new UsersRequestBuilder(rawUrl, requestAdapter);

        Assert.NotNull(builder);
    }
}
