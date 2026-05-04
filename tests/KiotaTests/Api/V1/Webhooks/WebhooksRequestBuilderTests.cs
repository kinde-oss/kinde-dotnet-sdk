using System.Net;
using FluentAssertions;
using Kiota.Api.Models;
using Xunit;
namespace KiotaTests.Api.V1.Webhooks;
public class WebhooksRequestBuilderTests
{
    [Fact] public async Task GetAsync_Returns200() { var (client, handler) = ApiClientFactory.Create(HttpStatusCode.OK, MockData.GetWebhooksResponse); var result = await client.Api.V1.Webhooks.GetAsync(); handler.LastRequest!.RequestUri!.PathAndQuery.Should().Be("/api/v1/webhooks"); result!.Webhooks.Should().NotBeNullOrEmpty(); }
    [Fact] public async Task PostAsync_Returns200() { var (client, handler) = ApiClientFactory.Create(HttpStatusCode.OK, MockData.CreateWebhookResponse); var result = await client.Api.V1.Webhooks.PostAsync(new CreateWebHook_request { Endpoint = "https://app.com/hook", EventTypes = new List<string> { "user.created" }, Name = "Hook" }); handler.LastRequest!.Method.Should().Be(HttpMethod.Post); result!.Webhook!.Id.Should().Be("wh_new"); }
}
