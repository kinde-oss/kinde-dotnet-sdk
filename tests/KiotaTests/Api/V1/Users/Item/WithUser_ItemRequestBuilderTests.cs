using System.Net;
using FluentAssertions;
using Xunit;
namespace KiotaTests.Api.V1.Users.Item;
public class WithUser_ItemRequestBuilderTests
{
    private const string UserId = "kp_user_001";
    [Fact] public void ExposesSubBuilders() { var (client, _) = ApiClientFactory.Create(HttpStatusCode.OK, MockData.SuccessResponse); client.Api.V1.Users[UserId].Identities.Should().NotBeNull(); client.Api.V1.Users[UserId].Properties.Should().NotBeNull(); client.Api.V1.Users[UserId].Feature_flags.Should().NotBeNull(); client.Api.V1.Users[UserId].Mfa.Should().NotBeNull(); client.Api.V1.Users[UserId].Sessions.Should().NotBeNull(); client.Api.V1.Users[UserId].Password.Should().NotBeNull(); client.Api.V1.Users[UserId].Refresh_claims.Should().NotBeNull(); }
}
