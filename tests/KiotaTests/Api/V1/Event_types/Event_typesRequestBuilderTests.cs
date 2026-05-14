using ApiSdk.Api.V1.Event_types;
using KiotaTests.Helpers;
using Xunit;

namespace KiotaTests.Api.V1.Event_types;

public class Event_typesRequestBuilderTests
{
    [Fact]
    public void Constructor_WithPathParameters_CreatesRequestBuilder()
    {
        var requestAdapter = KindeApiTestHelpers.CreateRequestAdapter();
        var pathParameters = KindeApiTestHelpers.CreatePathParameters();

        var builder = new Event_typesRequestBuilder(pathParameters, requestAdapter);

        Assert.NotNull(builder);
    }

    [Fact]
    public void Constructor_WithRawUrl_CreatesRequestBuilder()
    {
        var requestAdapter = KindeApiTestHelpers.CreateRequestAdapter();
        var rawUrl = "https://api.example.test/test";

        var builder = new Event_typesRequestBuilder(rawUrl, requestAdapter);

        Assert.NotNull(builder);
    }
}
