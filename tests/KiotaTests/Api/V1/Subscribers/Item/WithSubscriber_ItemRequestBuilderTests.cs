using System.Net;
using FluentAssertions;
using Xunit;
namespace KiotaTests.Api.V1.Subscribers.Item;
public class WithSubscriber_ItemRequestBuilderTests
{
    [Fact]
    public async Task GetAsync_Returns200()
    {
        var (client, handler) = ApiClientFactory.Create(HttpStatusCode.OK, MockData.GetSubscriberResponse);

        var result = await client.Api.V1.Subscribers["sub_001"].GetAsync();

        handler.LastRequest!.Method.Should().Be(HttpMethod.Get);
        result!.Subscribers![0].PreferredEmail.Should().Be("subscriber@example.com");
    }
}
