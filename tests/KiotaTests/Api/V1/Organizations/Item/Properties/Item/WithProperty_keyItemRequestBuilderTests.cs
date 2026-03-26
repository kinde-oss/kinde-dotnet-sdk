using System.Net; using FluentAssertions; using Kiota.Api.Models; using Xunit;
namespace KiotaTests.Api.V1.Organizations.Item.Properties.Item;
public class WithProperty_keyItemRequestBuilderTests
{
    [Fact] public async Task PutAsync_Returns200() { 
        var (client, handler) = ApiClientFactory.Create(HttpStatusCode.OK, MockData.SuccessResponse); 
        await client.Api.V1.Organizations["org_001"].Properties["prop_key"].PutAsync();
        handler.LastRequest!.Method.Should().Be(HttpMethod.Put); 
    }
}
