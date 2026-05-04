
using System.Net;
using FluentAssertions;
using Xunit;
namespace KiotaTests.Api.V1.Timezones;
public class TimezonesRequestBuilderTests
{
    [Fact] public async Task GetAsync_Returns200() { var (client, handler) = ApiClientFactory.Create(HttpStatusCode.OK, MockData.GetTimezonesResponse); var result = await client.Api.V1.Timezones.GetAsync(); handler.LastRequest!.RequestUri!.PathAndQuery.Should().Be("/api/v1/timezones"); result!.Timezones![0].Key.Should().Be("Australia/Sydney"); }
}
