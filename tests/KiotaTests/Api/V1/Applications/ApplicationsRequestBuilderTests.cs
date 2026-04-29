// Api/V1/Applications/ApplicationsRequestBuilderTests.cs
using System.Net;
using FluentAssertions;
using Kiota.Api.Models;
using Xunit;
namespace KiotaTests.Api.V1.Applications;
public class ApplicationsRequestBuilderTests
{
    [Fact]
    public async Task GetAsync_Returns200_WithList()
    {
        var (client, handler) = ApiClientFactory.Create(HttpStatusCode.OK, MockData.GetApplicationsResponse);
        var result = await client.Api.V1.Applications.GetAsync();
        handler.LastRequest!.Method.Should().Be(HttpMethod.Get);
        handler.LastRequest.RequestUri!.PathAndQuery.Should().Be("/api/v1/applications");
        result!.Applications![0].Name.Should().Be("My React app");
    }
    [Fact]
    public async Task GetAsync_WithPageSize_SendsQueryParam()
    {
        var (client, handler) = ApiClientFactory.Create(HttpStatusCode.OK, MockData.GetApplicationsResponse);
        await client.Api.V1.Applications.GetAsync(c => { c.QueryParameters.PageSize = 10; c.QueryParameters.NextToken = "tok"; });
        handler.LastRequest!.RequestUri!.Query.Should().Contain("page_size=10");
    }
    [Fact]
    public async Task PostAsync_Returns200_WithClientCredentials()
    {
        var (client, handler) = ApiClientFactory.Create(HttpStatusCode.OK, MockData.CreateApplicationResponse);
        var result = await client.Api.V1.Applications.PostAsync(new CreateApplication_request { Name = "New SPA", Type = CreateApplication_request_type.Spa });
        handler.LastRequest!.Method.Should().Be(HttpMethod.Post);
        result!.Application!.ClientSecret.Should().Contain("sUJSHI3Z");
    }
}
