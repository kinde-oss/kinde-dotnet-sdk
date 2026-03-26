using System.Net;
using FluentAssertions;
using Xunit;
namespace KiotaTests.Api.V1.Search.Users;
public class UsersRequestBuilderTests
{
    [Fact] public async Task GetAsync_Returns200_WithSearchResults() { var (client, handler) = ApiClientFactory.Create(HttpStatusCode.OK, MockData.SearchUsersResponse); var result = await client.Api.V1.Search.Users.GetAsync(c => c.QueryParameters.Query = "alice"); handler.LastRequest!.RequestUri!.Query.Should().Contain("query=alice"); handler.LastRequest.RequestUri.PathAndQuery.Should().Contain("/search/users"); result.Should().NotBeNull(); }
}
