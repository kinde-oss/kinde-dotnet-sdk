using ApiSdk.Api.V1.Organizations.Item.Properties;
using KiotaTests.Helpers;
using Xunit;

namespace KiotaTests.Api.V1.Organizations.Item.Properties;

public class PropertiesRequestBuilderTests
{
    [Fact]
    public void Constructor_WithPathParameters_CreatesRequestBuilder()
    {
        var requestAdapter = KindeApiTestHelpers.CreateRequestAdapter();
        var pathParameters = KindeApiTestHelpers.CreatePathParameters();

        var builder = new PropertiesRequestBuilder(pathParameters, requestAdapter);

        Assert.NotNull(builder);
    }

    [Fact]
    public void Constructor_WithRawUrl_CreatesRequestBuilder()
    {
        var requestAdapter = KindeApiTestHelpers.CreateRequestAdapter();
        var rawUrl = "https://api.example.test/test";

        var builder = new PropertiesRequestBuilder(rawUrl, requestAdapter);

        Assert.NotNull(builder);
    }
}
