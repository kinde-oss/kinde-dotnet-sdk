using System.Net;
using FluentAssertions;
using Kiota.Api.Models;
using Xunit;
namespace KiotaTests.Api.V1.Subscribers;

public class SubscribersRequestBuilderTests
{
    [Fact]
    public async Task GetAsync_Returns200()
    {
        var (client, handler) = ApiClientFactory.Create(HttpStatusCode.OK, MockData.GetSubscribersResponse);
        var result = await client.Api.V1.Subscribers.GetAsync();
        handler.LastRequest!.RequestUri!.PathAndQuery.Should().Be("/api/v1/subscribers");
        result!.Subscribers![0].Email.Should().Be("subscriber@example.com");
    }

    [Fact]
    public async Task PostAsync_Returns200()
    {
        var (client, handler) = ApiClientFactory.Create(HttpStatusCode.OK, """{"code":"OK","subscriber":{"subscriber_id":"sub_new"}}""");
        await client.Api.V1.Subscribers.PostAsync(rc =>
        {
            rc.QueryParameters.FirstName = "New";
            rc.QueryParameters.LastName = "Sub";
            rc.QueryParameters.Email = "new@example.com";
        });
        handler.LastRequest!.Method.Should().Be(HttpMethod.Post);
    }
}
