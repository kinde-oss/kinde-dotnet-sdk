using System.Net;
using FluentAssertions;
using Kiota.Api.Models;
using Xunit;
namespace KiotaTests.Api.V1.Webhooks.Item;
public class WithWebhook_ItemRequestBuilderTests
{
    private const string WebhookId = "wh_001";
    [Fact] public async Task PatchAsync_Returns200() { var (client, handler) = ApiClientFactory.Create(HttpStatusCode.OK, MockData.SuccessResponse); await client.Api.V1.Webhooks[WebhookId].PatchAsync(new UpdateWebHook_request { Name = "https://app.com/hook-v2" }); handler.LastRequest!.Method.Should().Be(HttpMethod.Patch); }
    [Fact] public async Task DeleteAsync_Returns200() { var (client, handler) = ApiClientFactory.Create(HttpStatusCode.OK, MockData.SuccessResponse); await client.Api.V1.Webhooks[WebhookId].DeleteAsync(); handler.LastRequest!.Method.Should().Be(HttpMethod.Delete); }
}
