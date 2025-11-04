using Kinde.Api.Api;
using Kinde.Api.Client;
using Kinde.Api.Model;
using Kinde.Api.Test.Integration;
using Xunit;

namespace Kinde.Api.Test.Integration.Api
{
    /// <summary>
    /// Integration tests for ApplicationsApi using the OpenAPI Mock Server.
    /// These tests validate serialization and deserialization of API requests and responses.
    /// </summary>
    public class ApplicationsApiIntegrationTests : IntegrationTestBase
    {
        public ApplicationsApiIntegrationTests(MockServerFixture fixture) : base(fixture)
        {
        }

        [Fact]
        public async Task GetApplications_DeserializesCorrectly()
        {
            // Arrange
            var expectedResponse = new
            {
                applications = new[]
                {
                    new
                    {
                        id = "app_123",
                        name = "Test Application",
                        type = "reg",
                        client_id = "client_123"
                    }
                },
                next_token = (string?)null
            };

            MockServer.SetMockResponse("/api/v1/applications", "GET", expectedResponse);
            var api = CreateApplicationsApi();

            // Act
            var result = await api.GetApplicationsAsync();

            // Assert
            Assert.NotNull(result);
            Assert.NotNull(result.Applications);
        }

        [Fact]
        public async Task GetApplication_DeserializesCorrectly()
        {
            // Arrange
            var applicationId = "app_test_123";
            var expectedResponse = new
            {
                application = new
                {
                    id = applicationId,
                    name = "Test Application",
                    type = "reg",
                    client_id = "client_123"
                }
            };

            MockServer.SetMockResponse($"/api/v1/applications/{applicationId}", "GET", expectedResponse);
            var api = CreateApplicationsApi();

            // Act
            var result = await api.GetApplicationAsync(applicationId);

            // Assert
            Assert.NotNull(result);
            Assert.NotNull(result.Application);
            Assert.Equal(applicationId, result.Application.Id);
        }

        [Fact]
        public async Task CreateApplication_SerializesAndDeserializesCorrectly()
        {
            // Arrange
            var request = new CreateApplicationRequest(
                name: "New Application",
                type: CreateApplicationRequest.TypeEnum.Reg
            );

            var expectedResponse = new
            {
                application = new
                {
                    id = "app_new_123",
                    name = "New Application",
                    type = "reg",
                    client_id = "client_new_123"
                },
                message = "Application created successfully"
            };

            MockServer.SetMockResponse("/api/v1/applications", "POST", expectedResponse);
            var api = CreateApplicationsApi();

            // Act
            var result = await api.CreateApplicationAsync(request);

            // Assert
            Assert.NotNull(result);
            Assert.NotNull(result.Application);
            Assert.NotNull(result.Application.Id);
        }
    }
}

