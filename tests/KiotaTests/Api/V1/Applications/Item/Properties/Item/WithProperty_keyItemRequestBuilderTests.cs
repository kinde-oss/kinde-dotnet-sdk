using System.Net; using FluentAssertions; using Kiota.Api.Models; using Xunit;
namespace KiotaTests.Api.V1.Applications.Item.Properties.Item;
public class WithProperty_keyItemRequestBuilderTests
{
    private const string AppId = "app_001";
    private const string PropKey = "favorite_color";
    [Fact]
    public async Task PutAsync_Returns200()
    {
        var (client, handler) = ApiClientFactory.Create(HttpStatusCode.OK, MockData.SuccessResponse);

        await client.Api.V1.Applications[AppId].Properties[PropKey].PutAsync(
            new UpdateApplicationsProperty_request
            {
                Value = new UpdateApplicationsProperty_request_value
                {
                    String = "blue" 
                }
            });

        handler.LastRequest!.Method.Should().Be(HttpMethod.Put);
        handler.LastRequest.RequestUri!.PathAndQuery.Should().Contain(PropKey);
    }
}
