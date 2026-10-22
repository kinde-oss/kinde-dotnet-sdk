using ApiSdk.Api.V1.Property_categories;
using KiotaTests.Helpers;
using Xunit;

namespace KiotaTests.Api.V1.Property_categories;

public class Property_categoriesRequestBuilderTests
{
    [Fact]
    public void Constructor_WithPathParameters_CreatesRequestBuilder()
    {
        var requestAdapter = KindeApiTestHelpers.CreateRequestAdapter();
        var pathParameters = KindeApiTestHelpers.CreatePathParameters();

        var builder = new Property_categoriesRequestBuilder(pathParameters, requestAdapter);

        Assert.NotNull(builder);
    }

    [Fact]
    public void Constructor_WithRawUrl_CreatesRequestBuilder()
    {
        var requestAdapter = KindeApiTestHelpers.CreateRequestAdapter();
        var rawUrl = "https://api.example.test/test";

        var builder = new Property_categoriesRequestBuilder(rawUrl, requestAdapter);

        Assert.NotNull(builder);
    }
}
