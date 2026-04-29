using System.Net;
using FluentAssertions;
using Xunit;
namespace KiotaTests.Api.V1.Users;
public class UsersRequestBuilderTests
{
    [Fact] public async Task GetAsync_Returns200() { var (client, handler) = ApiClientFactory.Create(HttpStatusCode.OK, MockData.GetUsersResponse); var result = await client.Api.V1.Users.GetAsync(); handler.LastRequest!.RequestUri!.PathAndQuery.Should().Be("/api/v1/users"); result!.Users![0].Email.Should().Be("alice@example.com"); }
    [Fact] public async Task GetAsync_WithFilters() { var (client, handler) = ApiClientFactory.Create(HttpStatusCode.OK, MockData.GetUsersResponse); await client.Api.V1.Users.GetAsync(c => { /* c.QueryParameters.Sort = "name_asc"; */ c.QueryParameters.PageSize = 25; }); handler.LastRequest!.RequestUri!.Query.Should().Contain("page_size=25"); }
}
