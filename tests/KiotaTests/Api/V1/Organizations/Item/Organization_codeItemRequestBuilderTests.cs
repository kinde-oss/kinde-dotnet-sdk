using System.Net; using FluentAssertions; using Kiota.Api.Models; using Xunit;
namespace KiotaTests.Api.V1.Organizations.Item;
public class Organization_codeItemRequestBuilderTests
{
    private const string OrgCode = "org_1767f11ce62";
    [Fact] public void ExposesSubBuilders() { var (client, _) = ApiClientFactory.Create(HttpStatusCode.OK, MockData.SuccessResponse); client.Api.V1.Organizations[OrgCode].Users.Should().NotBeNull(); client.Api.V1.Organizations[OrgCode].Feature_flags.Should().NotBeNull(); client.Api.V1.Organizations[OrgCode].Properties.Should().NotBeNull(); client.Api.V1.Organizations[OrgCode].Connections.Should().NotBeNull(); client.Api.V1.Organizations[OrgCode].Logos.Should().NotBeNull(); client.Api.V1.Organizations[OrgCode].Mfa.Should().NotBeNull(); client.Api.V1.Organizations[OrgCode].Sessions.Should().NotBeNull(); }
}
