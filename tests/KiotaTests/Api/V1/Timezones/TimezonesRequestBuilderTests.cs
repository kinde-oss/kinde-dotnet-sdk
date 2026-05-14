using ApiSdk.Api.V1.Timezones;
using KiotaTests.Helpers;
using Xunit;

namespace KiotaTests.Api.V1.Timezones;

public class TimezonesRequestBuilderTests
{
    [Fact]
    public void Constructor_WithPathParameters_CreatesRequestBuilder()
    {
        var requestAdapter = KindeApiTestHelpers.CreateRequestAdapter();
        var pathParameters = KindeApiTestHelpers.CreatePathParameters();

        var builder = new TimezonesRequestBuilder(pathParameters, requestAdapter);

        Assert.NotNull(builder);
    }

    [Fact]
    public void Constructor_WithRawUrl_CreatesRequestBuilder()
    {
        var requestAdapter = KindeApiTestHelpers.CreateRequestAdapter();
        var rawUrl = "https://api.example.test/test";

        var builder = new TimezonesRequestBuilder(rawUrl, requestAdapter);

        Assert.NotNull(builder);
    }
}
