using System.Net; using FluentAssertions; using Xunit;
namespace KiotaTests.Api.V1.Event_types;
public class Event_typesRequestBuilderTests
{
    [Fact] public async Task GetAsync_Returns200() { var (client, handler) = ApiClientFactory.Create(HttpStatusCode.OK, MockData.GetEventTypesResponse); var result = await client.Api.V1.Event_types.GetAsync(); handler.LastRequest!.RequestUri!.PathAndQuery.Should().Be("/api/v1/event_types"); result!.EventTypes![0].Code.Should().Be("user.created"); }
}
