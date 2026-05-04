using System.Net;
using FluentAssertions;
using Kiota.Api;
using Xunit;

namespace KiotaTests;

public class ApiClientTests
{
    [Fact]
    public void ApiClient_CanBeInstantiated_WithAdapter()
    {
        var (client, _) = ApiClientFactory.Create(HttpStatusCode.OK, MockData.SuccessResponse);
        client.Should().NotBeNull();
    }

    [Fact]
    public void ApiClient_ExposesApiProperty()
    {
        var (client, _) = ApiClientFactory.Create(HttpStatusCode.OK, MockData.SuccessResponse);
        client.Api.Should().NotBeNull();
    }

    [Fact]
    public void ApiClient_Api_ExposesV1()
    {
        var (client, _) = ApiClientFactory.Create(HttpStatusCode.OK, MockData.SuccessResponse);
        client.Api.V1.Should().NotBeNull();
    }
}
