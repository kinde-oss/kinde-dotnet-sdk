using System.Net; using FluentAssertions; using Kiota.Api.Models; using Xunit;
namespace KiotaTests.Api.V1.Environment_variables;
public class Environment_variablesRequestBuilderTests
{
    [Fact] public async Task GetAsync_Returns200() { var (client, handler) = ApiClientFactory.Create(HttpStatusCode.OK, MockData.GetEnvironmentVariablesResponse); var result = await client.Api.V1.Environment_variables.GetAsync(); handler.LastRequest!.RequestUri!.PathAndQuery.Should().Be("/api/v1/environment_variables"); result!.EnvironmentVariables![0].Key.Should().Be("APP_URL"); }
    [Fact] public async Task PostAsync_Returns200() { var (client, handler) = ApiClientFactory.Create(HttpStatusCode.OK, """{"code":"OK","environment_variable":{"id":"var_new"}}"""); await client.Api.V1.Environment_variables.PostAsync(new CreateEnvironmentVariable_request { Key = "NEW_VAR", Value = "val", IsSecret = false }); handler.LastRequest!.Method.Should().Be(HttpMethod.Post); }
}
