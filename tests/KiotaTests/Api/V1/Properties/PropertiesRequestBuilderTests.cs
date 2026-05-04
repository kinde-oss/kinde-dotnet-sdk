using System.Net;
using FluentAssertions;
using Kiota.Api.Models;
using Xunit;
namespace KiotaTests.Api.V1.Properties;
public class PropertiesRequestBuilderTests
{
    [Fact] public async Task GetAsync_Returns200() { var (client, handler) = ApiClientFactory.Create(HttpStatusCode.OK, MockData.GetPropertiesResponse); var result = await client.Api.V1.Properties.GetAsync(); handler.LastRequest!.RequestUri!.PathAndQuery.Should().Be("/api/v1/properties"); result!.Properties![0].Key.Should().Be("favorite_color"); }
    [Fact] public async Task PostAsync_Returns200() { var (client, handler) = ApiClientFactory.Create(HttpStatusCode.OK, """{"code":"OK","property":{"id":"prop_new"}}"""); await client.Api.V1.Properties.PostAsync(new CreateProperty_request { Key = "new_prop", Name = "New", Context = CreateProperty_request_context.Usr, Type = CreateProperty_request_type.Single_line_text }); handler.LastRequest!.Method.Should().Be(HttpMethod.Post); }
}
