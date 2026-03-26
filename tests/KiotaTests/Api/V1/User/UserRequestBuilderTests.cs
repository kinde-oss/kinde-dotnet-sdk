using System.Net;
using FluentAssertions;
using Kiota.Api.Models;
using Xunit;
namespace KiotaTests.Api.V1.User;
public class UserRequestBuilderTests
{
    [Fact]
    public async Task GetAsync_Returns200()
    {
        var (client, handler) = ApiClientFactory.Create(HttpStatusCode.OK, MockData.GetUserResponse);

        var result = await client.Api.V1.User.GetAsync(c => c.QueryParameters.Id = "kp_user_001");

        handler.LastRequest!.RequestUri!.Query.Should().Contain("id=kp_user_001");
        result!.PreferredEmail.Should().Be("alice@example.com");
    }
    [Fact]
    public async Task PostAsync_Returns200()
    {
        var (client, handler) = ApiClientFactory.Create(HttpStatusCode.OK, MockData.CreateUserResponse);

        var result = await client.Api.V1.User.PostAsync(new CreateUser_request
        {
            Profile = new CreateUser_request_profile
            {
                GivenName = "Bob",
                FamilyName = "Jones"
            },
            Identities = new List<CreateUser_request_identities_inner>
        {
            new()
            {
                Type = CreateUser_request_identities_inner_type.Email,
                Details = new CreateUser_request_identities_inner_details
                {
                    Email = "bob@example.com"
                }
            }
        }
        });

        handler.LastRequest!.Method.Should().Be(HttpMethod.Post);
        result!.Id.Should().Be("kp_user_002");
    }
    [Fact] public async Task PatchAsync_Returns200() { var (client, handler) = ApiClientFactory.Create(HttpStatusCode.OK, MockData.GetUserResponse); await client.Api.V1.User.PatchAsync(new UpdateUser_request { GivenName = "Alice" }, c => c.QueryParameters.Id = "kp_user_001"); handler.LastRequest!.Method.Should().Be(HttpMethod.Patch); }
    [Fact] public async Task DeleteAsync_Returns200() { var (client, handler) = ApiClientFactory.Create(HttpStatusCode.OK, MockData.SuccessResponse); await client.Api.V1.User.DeleteAsync(c => c.QueryParameters.Id = "kp_user_001"); handler.LastRequest!.Method.Should().Be(HttpMethod.Delete); }
}
