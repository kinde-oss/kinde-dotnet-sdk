using System.Net; using FluentAssertions; using Kiota.Api.Models; using Xunit;
namespace KiotaTests.Api.V1.Organization;
public class OrganizationRequestBuilderTests
{
    [Fact]
    public async Task GetAsync_Returns200()
    {
        var (client, handler) = ApiClientFactory.Create(HttpStatusCode.OK, MockData.GetOrganizationResponse);
        var result = await client.Api.V1.Organization.GetAsync();
        handler.LastRequest!.RequestUri!.PathAndQuery.Should().Be("/api/v1/organization");
        result!.Code.Should().Be("org_1767f11ce62");
    }
    [Fact] public async Task PostAsync_Returns200() { var (client, handler) = ApiClientFactory.Create(HttpStatusCode.OK, """{"code":"OK","organization":{"code":"org_new"}}"""); var result = await client.Api.V1.Organization.PostAsync(new CreateOrganization_request { Name = "New Org" }); handler.LastRequest!.Method.Should().Be(HttpMethod.Post); result!.Organization!.Code.Should().Be("org_new"); }
}
