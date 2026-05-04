using System.Net;
using FluentAssertions;
using Kiota.Api.Models;
using Xunit;
namespace KiotaTests.Api.V1.Property_categories.Item;
public class WithCategory_ItemRequestBuilderTests
{
    [Fact] public async Task PutAsync_Returns200() { var (client, handler) = ApiClientFactory.Create(HttpStatusCode.OK, MockData.SuccessResponse); await client.Api.V1.Property_categories["cat_001"].PutAsync(new UpdateCategory_request { Name = "Updated" }); handler.LastRequest!.Method.Should().Be(HttpMethod.Put); handler.LastRequest.RequestUri!.PathAndQuery.Should().Contain("cat_001"); }
}
