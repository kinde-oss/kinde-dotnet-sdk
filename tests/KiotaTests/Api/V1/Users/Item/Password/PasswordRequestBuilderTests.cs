using ApiSdk.Api.V1.Users.Item.Password;
using KiotaTests.Helpers;
using Xunit;

namespace KiotaTests.Api.V1.Users.Item.Password;

public class PasswordRequestBuilderTests
{
    [Fact]
    public void Constructor_WithPathParameters_CreatesRequestBuilder()
    {
        var requestAdapter = KindeApiTestHelpers.CreateRequestAdapter();
        var pathParameters = KindeApiTestHelpers.CreatePathParameters();

        var builder = new PasswordRequestBuilder(pathParameters, requestAdapter);

        Assert.NotNull(builder);
    }

    [Fact]
    public void Constructor_WithRawUrl_CreatesRequestBuilder()
    {
        var requestAdapter = KindeApiTestHelpers.CreateRequestAdapter();
        var rawUrl = "https://api.example.test/test";

        var builder = new PasswordRequestBuilder(rawUrl, requestAdapter);

        Assert.NotNull(builder);
    }
}
