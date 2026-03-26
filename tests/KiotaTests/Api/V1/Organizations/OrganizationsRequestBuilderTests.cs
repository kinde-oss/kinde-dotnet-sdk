using System.Net;
using FluentAssertions;
using Xunit;
namespace KiotaTests.Api.V1.Organizations;
public class OrganizationsRequestBuilderTests
{
    [Fact] public async Task GetAsync_Returns200() { var (client, handler) = ApiClientFactory.Create(HttpStatusCode.OK, MockData.GetOrganizationsResponse); var result = await client.Api.V1.Organizations.GetAsync(); handler.LastRequest!.RequestUri!.PathAndQuery.Should().Be("/api/v1/organizations"); result!.Organizations![0].Code.Should().Be("org_1767f11ce62"); }
    [Fact]
    public async Task GetAsync_WithSortParam()
    {
        var (client, handler) = ApiClientFactory.Create(HttpStatusCode.OK, MockData.GetOrganizationsResponse);
        await client.Api.V1.Organizations.GetAsync(c =>
        {
            c.QueryParameters.SortAsGetSortQueryParameterType = Kiota.Api.Api.V1.Organizations.GetSortQueryParameterType.Name_asc;
            c.QueryParameters.PageSize = 20;
        }); handler.LastRequest!.RequestUri!.Query.Should().Contain("page_size=20");
    }
}
