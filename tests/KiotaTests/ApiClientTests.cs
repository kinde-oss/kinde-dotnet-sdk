using ApiSdk;
using KiotaTests.Helpers;
using Xunit;

namespace KiotaTests;

public class ApiClientTests
{
    [Fact]
    public void Constructor_WithRequestAdapter_CreatesClient()
    {
        var requestAdapter = KindeApiTestHelpers.CreateRequestAdapter();

        var client = new ApiClient(requestAdapter);

        Assert.NotNull(client);
    }
}
