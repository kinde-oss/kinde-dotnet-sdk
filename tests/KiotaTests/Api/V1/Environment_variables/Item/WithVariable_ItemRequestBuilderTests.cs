using System.Net; using FluentAssertions; using Kiota.Api.Models; using Xunit;
namespace KiotaTests.Api.V1.Environment_variables.Item;
public class WithVariable_ItemRequestBuilderTests
{
    private const string VarId = "var_001";
    [Fact] public async Task GetAsync_Returns200() { var (client, handler) = ApiClientFactory.Create(HttpStatusCode.OK, """{"code":"OK","environment_variable":{"id":"var_001","key":"APP_URL","value":"https://myapp.com"}}"""); var result = await client.Api.V1.Environment_variables[VarId].GetAsync(); result!.EnvironmentVariable!.Key.Should().Be("APP_URL"); }
    [Fact] public async Task PatchAsync_Returns200() { var (client, handler) = ApiClientFactory.Create(HttpStatusCode.OK, MockData.SuccessResponse); await client.Api.V1.Environment_variables[VarId].PatchAsync(new UpdateEnvironmentVariable_request { Value = "new_val" }); handler.LastRequest!.Method.Should().Be(HttpMethod.Patch); }
    [Fact] public async Task DeleteAsync_Returns200() { var (client, handler) = ApiClientFactory.Create(HttpStatusCode.OK, MockData.SuccessResponse); await client.Api.V1.Environment_variables[VarId].DeleteAsync(); handler.LastRequest!.Method.Should().Be(HttpMethod.Delete); }
}
