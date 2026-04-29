using System.Net; using FluentAssertions; using Kiota.Api.Models; using Xunit;
namespace KiotaTests.Api.V1.Organizations.Item.Sessions;
public class SessionsRequestBuilderTests
{
    [Fact] public async Task PatchAsync_Returns200() { var (client, handler) = ApiClientFactory.Create(HttpStatusCode.OK, MockData.SuccessResponse); await client.Api.V1.Organizations["org_001"].Sessions.PatchAsync(new UpdateOrganizationSessions_request()); handler.LastRequest!.Method.Should().Be(HttpMethod.Patch); handler.LastRequest.RequestUri!.PathAndQuery.Should().Contain("/organizations/org_001/sessions"); }
}
