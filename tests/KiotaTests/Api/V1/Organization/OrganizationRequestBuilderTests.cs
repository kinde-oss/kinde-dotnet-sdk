using ApiSdk.Api.V1.Organization;
using KiotaTests.Helpers;
using Xunit;

namespace KiotaTests.Api.V1.Organization;

public class OrganizationRequestBuilderTests
{
    [Fact]
    public void Constructor_WithPathParameters_CreatesRequestBuilder()
    {
        var requestAdapter = KindeApiTestHelpers.CreateRequestAdapter();
        var pathParameters = KindeApiTestHelpers.CreatePathParameters();

        var builder = new OrganizationRequestBuilder(pathParameters, requestAdapter);

        Assert.NotNull(builder);
    }

    [Fact]
    public void Constructor_WithRawUrl_CreatesRequestBuilder()
    {
        var requestAdapter = KindeApiTestHelpers.CreateRequestAdapter();
        var rawUrl = "https://api.example.test/test";

        var builder = new OrganizationRequestBuilder(rawUrl, requestAdapter);

        Assert.NotNull(builder);
    }
}
