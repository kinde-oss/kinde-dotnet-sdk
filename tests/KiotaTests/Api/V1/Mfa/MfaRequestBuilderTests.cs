using System.Net; using FluentAssertions; using Kiota.Api.Models; using Xunit;
namespace KiotaTests.Api.V1.Mfa;
public class MfaRequestBuilderTests
{
    [Fact] public async Task PutAsync_Returns200() { var (client, handler) = ApiClientFactory.Create(HttpStatusCode.OK, MockData.SuccessResponse); await client.Api.V1.Mfa.PutAsync(new ReplaceMFA_request()); handler.LastRequest!.Method.Should().Be(HttpMethod.Put); handler.LastRequest.RequestUri!.PathAndQuery.Should().Be("/api/v1/mfa"); }
}
