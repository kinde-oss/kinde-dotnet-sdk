using System.Net;
using FluentAssertions;
using Kiota.Api.Models;
using Xunit;
namespace KiotaTests.Api.V1.Property_categories;
public class Property_categoriesRequestBuilderTests
{
    [Fact] public async Task GetAsync_Returns200() { var (client, handler) = ApiClientFactory.Create(HttpStatusCode.OK, MockData.GetPropertyCategoriesResponse); var result = await client.Api.V1.Property_categories.GetAsync(); handler.LastRequest!.RequestUri!.PathAndQuery.Should().Be("/api/v1/property_categories"); result!.Categories![0].Name.Should().Be("Profile"); }
    [Fact] public async Task PostAsync_Returns200() { var (client, handler) = ApiClientFactory.Create(HttpStatusCode.OK, """{"code":"OK","category":{"id":"cat_new"}}"""); await client.Api.V1.Property_categories.PostAsync(new CreateCategory_request { Name = "New Cat", Context = CreateCategory_request_context.Usr }); handler.LastRequest!.Method.Should().Be(HttpMethod.Post); }
}
