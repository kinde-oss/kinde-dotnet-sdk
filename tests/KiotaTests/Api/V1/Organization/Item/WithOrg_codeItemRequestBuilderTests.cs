using ApiSdk.Api.V1.Organization.Item;
using KiotaTests.Helpers;
using Xunit;

namespace KiotaTests.Api.V1.Organization.Item;

public class WithOrg_codeItemRequestBuilderTests
{
    [Fact]
    public void Constructor_WithPathParameters_CreatesRequestBuilder()
    {
        var requestAdapter = KindeApiTestHelpers.CreateRequestAdapter();
        var pathParameters = KindeApiTestHelpers.CreatePathParameters();

        var builder = new WithOrg_codeItemRequestBuilder(pathParameters, requestAdapter);

        Assert.NotNull(builder);
    }

    [Fact]
    public void Constructor_WithRawUrl_CreatesRequestBuilder()
    {
        var requestAdapter = KindeApiTestHelpers.CreateRequestAdapter();
        var rawUrl = "https://api.example.test/test";

        var builder = new WithOrg_codeItemRequestBuilder(rawUrl, requestAdapter);

        Assert.NotNull(builder);
    }
}
