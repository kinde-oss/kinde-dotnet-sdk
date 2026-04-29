using System.Net;
using FluentAssertions;
using Xunit;
namespace KiotaTests.Api.V1.Users.Item.Feature_flags;
public class Feature_flagsRequestBuilderTests
{
    [Fact] public void ExposesItemBuilder() { var (client, _) = ApiClientFactory.Create(HttpStatusCode.OK, MockData.SuccessResponse); client.Api.V1.Users["kp_user_001"].Feature_flags["flag_001"].Should().NotBeNull(); }
}
