using ApiSdk.Api.V1.Feature_flags.Item;
using KiotaTests.Helpers;
using Xunit;

namespace KiotaTests.Api.V1.Feature_flags.Item;

public class WithFeature_flag_keyItemRequestBuilderTests
{
    [Fact]
    public void Constructor_WithPathParameters_CreatesRequestBuilder()
    {
        var requestAdapter = KindeApiTestHelpers.CreateRequestAdapter();
        var pathParameters = KindeApiTestHelpers.CreatePathParameters();

        var builder = new WithFeature_flag_keyItemRequestBuilder(pathParameters, requestAdapter);

        Assert.NotNull(builder);
    }

    [Fact]
    public void Constructor_WithRawUrl_CreatesRequestBuilder()
    {
        var requestAdapter = KindeApiTestHelpers.CreateRequestAdapter();
        var rawUrl = "https://api.example.test/test";

        var builder = new WithFeature_flag_keyItemRequestBuilder(rawUrl, requestAdapter);

        Assert.NotNull(builder);
    }
}
