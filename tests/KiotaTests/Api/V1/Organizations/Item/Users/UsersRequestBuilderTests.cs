using System.Net; using FluentAssertions; using Kiota.Api.Models; using Xunit;
namespace KiotaTests.Api.V1.Organizations.Item.Users;
public class UsersRequestBuilderTests
{
    private const string OrgCode = "org_001";
    [Fact] public async Task GetAsync_Returns200() { var (client, handler) = ApiClientFactory.Create(HttpStatusCode.OK, MockData.GetOrgUsersResponse); var result = await client.Api.V1.Organizations[OrgCode].Users.GetAsync(); result!.OrganizationUsers![0].Email.Should().Be("alice@example.com"); }
    [Fact] public async Task PostAsync_Returns200() { var (client, handler) = ApiClientFactory.Create(HttpStatusCode.OK, MockData.SuccessResponse); await client.Api.V1.Organizations[OrgCode].Users.PostAsync(new AddOrganizationUsers_request { Users = new List<AddOrganizationUsers_request_users_inner> { new() { Id = "kp_user_001" } } }); handler.LastRequest!.Method.Should().Be(HttpMethod.Post); }
    [Fact] public async Task PatchAsync_Returns200() { var (client, handler) = ApiClientFactory.Create(HttpStatusCode.OK, MockData.SuccessResponse); await client.Api.V1.Organizations[OrgCode].Users.PatchAsync(new UpdateOrganizationUsers_request()); handler.LastRequest!.Method.Should().Be(HttpMethod.Patch); }
}
