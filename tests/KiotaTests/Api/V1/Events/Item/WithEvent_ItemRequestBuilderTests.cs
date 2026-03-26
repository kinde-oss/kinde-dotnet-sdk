using System.Net; using FluentAssertions; using Xunit;
namespace KiotaTests.Api.V1.Events.Item;
public class WithEvent_ItemRequestBuilderTests
{
    [Fact]
    public async Task GetAsync_Returns200()
    {
        var (client, handler) = ApiClientFactory.Create(HttpStatusCode.OK, MockData.GetEventResponse);

        var result = await client.Api.V1.Events["evt_001"].GetAsync();

        handler.LastRequest!.RequestUri!.PathAndQuery.Should().Contain("/events/evt_001");
        result.Should().NotBeNull();
        result!.Event!.EventId.Should().Be("evt_001"); 
    }
}
