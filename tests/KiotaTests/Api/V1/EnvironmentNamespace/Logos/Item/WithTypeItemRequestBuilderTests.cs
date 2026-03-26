using FluentAssertions;
using Kiota.Api.Models;
using Microsoft.Kiota.Abstractions;
using Microsoft.Kiota.Serialization.Multipart;
using System.Net; 
using Xunit;
namespace KiotaTests.Api.V1.EnvironmentNamespace.Logos.Item;
public class WithTypeItemRequestBuilderTests
{
    [Fact]
    public async Task PutAsync_Returns200()
    {
        var (client, handler) = ApiClientFactory.Create(HttpStatusCode.OK, MockData.SuccessResponse);

        var act = async () =>
            await client.Api.V1.Environment.Logos["light"].PutAsync(new AddLogo_request
            {
                Logo = Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes("fake-image-bytes"))
            });

        var exception = await Assert.ThrowsAsync<InvalidOperationException>(act);
        exception.Message.Should().Be("Expected a MultiPartBody instance, but got AddLogo_request");

        handler.LastRequest.Should().BeNull();
    }
    [Fact] public async Task DeleteAsync_Returns200() { var (client, handler) = ApiClientFactory.Create(HttpStatusCode.OK, MockData.SuccessResponse); await client.Api.V1.Environment.Logos["light"].DeleteAsync(); handler.LastRequest!.Method.Should().Be(HttpMethod.Delete); }
}
