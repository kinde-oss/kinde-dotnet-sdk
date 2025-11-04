using Kinde.Api.Api;
using Kinde.Api.Client;
using Kinde.Api.Model;
using Kinde.Api.Test.Integration;
using Xunit;

namespace Kinde.Api.Test.Integration.Api
{
    /// <summary>
    /// Integration tests for EnvironmentVariablesApi using the OpenAPI Mock Server.
    /// These tests validate serialization and deserialization of API requests and responses.
    /// </summary>
    public class EnvironmentVariablesApiIntegrationTests : IntegrationTestBase
    {
        public EnvironmentVariablesApiIntegrationTests(MockServerFixture fixture) : base(fixture)
        {
        }

        [Fact]
        public async Task GetEnvironmentVariables_DeserializesCorrectly()
        {
            // Arrange
            var expectedResponse = new
            {
                code = "OK",
                message = "Success",
                environment_variables = new[]
                {
                    new
                    {
                        id = "var_123",
                        key = "API_KEY",
                        value = "secret_value",
                        is_sensitive = true
                    }
                },
                has_more = false
            };

            MockServer.SetMockResponse("/api/v1/environment_variables", "GET", expectedResponse);
            var api = CreateEnvironmentVariablesApi();

            // Act
            var result = await api.GetEnvironmentVariablesAsync();

            // Assert
            Assert.NotNull(result);
            Assert.NotNull(result.EnvironmentVariables);
        }

        [Fact]
        public async Task CreateEnvironmentVariable_SerializesAndDeserializesCorrectly()
        {
            // Arrange
            var request = new CreateEnvironmentVariableRequest(
                key: "NEW_API_KEY",
                value: "new_secret_value",
                isSecret: true
            );

            var expectedResponse = new
            {
                code = "CREATED",
                message = "Environment variable created successfully",
                environment_variable = new
                {
                    id = "var_new_123"
                }
            };

            MockServer.SetMockResponse("/api/v1/environment_variables", "POST", expectedResponse);
            var api = CreateEnvironmentVariablesApi();

            // Act
            var result = await api.CreateEnvironmentVariableAsync(request);

            // Assert
            Assert.NotNull(result);
            Assert.NotNull(result.EnvironmentVariable);
            Assert.NotNull(result.EnvironmentVariable.Id);
        }
    }
}

