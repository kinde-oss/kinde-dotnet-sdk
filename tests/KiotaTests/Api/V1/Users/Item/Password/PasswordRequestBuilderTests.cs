using System.Net;
using FluentAssertions;
using Kiota.Api.Models;
using Xunit;
namespace KiotaTests.Api.V1.Users.Item.Password;
public class PasswordRequestBuilderTests
{
    [Fact] public async Task PutAsync_Returns200() { var (client, handler) = ApiClientFactory.Create(HttpStatusCode.OK, MockData.SuccessResponse); await client.Api.V1.Users["kp_user_001"].Password.PutAsync(new SetUserPassword_request { HashedPassword = "NewStr0ng!Pass", IsTemporaryPassword = false }); handler.LastRequest!.Method.Should().Be(HttpMethod.Put); handler.LastRequest.RequestUri!.PathAndQuery.Should().Contain("/password"); }
}
