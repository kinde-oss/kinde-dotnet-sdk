using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Kinde.Api.Api;
using Kinde.Api.Model;
using Kinde.Api.Client;
using Kinde.Api.Test.Integration;
using Xunit;
using Xunit.Abstractions;
using KiotaModels = Kinde.Api.Kiota.Management.Models;

namespace Kinde.Api.Test.Integration.Api.Generated
{
    /// <summary>
    /// Integration tests for ApplicationsApi with both mock and real API support.
    /// Uses Kiota-format mock responses for accurate testing of the API flow.
    /// </summary>
    public class ApplicationsApiIntegrationTests : BaseIntegrationTest, IClassFixture<TestResourceFixture>
    {
        private readonly ITestOutputHelper _output;
        private readonly TestResourceFixture _fixture;

        public ApplicationsApiIntegrationTests(ITestOutputHelper output, TestResourceFixture fixture) : base()
        {
            _output = output;
            _fixture = fixture;
        }

        #region GetApplications Tests

        [Fact]
        [Trait("TestMode", "Mock")]
        public async Task GetApplications_Mock_ReturnsApplications()
        {
            // Arrange
            if (UseRealApi) return;

            var mockHandler = GetKiotaMockHandler();
            if (mockHandler == null) return;

            var kiotaResponse = new KiotaModels.Get_applications_response
            {
                Code = "200",
                Message = "Applications retrieved",
                Applications = new List<KiotaModels.Applications>
                {
                    new KiotaModels.Applications { Id = "app_1", Name = "My App 1" },
                    new KiotaModels.Applications { Id = "app_2", Name = "My App 2" }
                },
                NextToken = "next_page_token"
            };
            mockHandler.AddKiotaResponse("GET", "/api/v1/applications", kiotaResponse);

                var api = CreateApi((client, config) => new ApplicationsApi(client, config));

                // Act
                var response = await api.GetApplicationsAsync();

                // Assert
                Assert.NotNull(response);
            Assert.Equal("200", response.Code);
            Assert.NotNull(response.Applications);
            Assert.Equal(2, response.Applications.Count);
            Assert.Equal("app_1", response.Applications[0].Id);
            Assert.Equal("My App 1", response.Applications[0].Name);
            Assert.Equal("next_page_token", response.NextToken);
            _output.WriteLine($"Mock test successful - Retrieved {response.Applications.Count} applications");
        }

        [Fact]
        [Trait("TestMode", "Mock")]
        public async Task GetApplications_Mock_EmptyList()
        {
            // Arrange
            if (UseRealApi) return;

            var mockHandler = GetKiotaMockHandler();
            if (mockHandler == null) return;

            var kiotaResponse = new KiotaModels.Get_applications_response
            {
                Code = "200",
                Message = "No applications found",
                Applications = new List<KiotaModels.Applications>()
            };
            mockHandler.AddKiotaResponse("GET", "/api/v1/applications", kiotaResponse);

                var api = CreateApi((client, config) => new ApplicationsApi(client, config));

                // Act
            var response = await api.GetApplicationsAsync();

                // Assert
                Assert.NotNull(response);
            Assert.NotNull(response.Applications);
            Assert.Empty(response.Applications);
            _output.WriteLine("Empty applications list handled correctly");
        }

        [Fact]
        [Trait("TestMode", "Real")]
        public async Task GetApplications_Real_Test()
        {
            // Arrange
            if (!UseRealApi) return;

                var api = CreateApi((client, config) => new ApplicationsApi(client, config));

            // Act
            var response = await api.GetApplicationsAsync();

                // Assert
                Assert.NotNull(response);
            _output.WriteLine($"Real API returned {response.Applications?.Count ?? 0} applications");
        }

        #endregion

        #region GetApplication Tests

        [Fact]
        [Trait("TestMode", "Mock")]
        public async Task GetApplication_Mock_ReturnsApplication()
        {
            // Arrange
            if (UseRealApi) return;

            var mockHandler = GetKiotaMockHandler();
            if (mockHandler == null) return;

            var kiotaResponse = new KiotaModels.Get_application_response
            {
                Code = "200",
                Message = "Application retrieved",
                Application = new KiotaModels.Get_application_response_application
                {
                    Id = "app_123",
                    Name = "My Application"
                }
            };
            mockHandler.AddKiotaResponse("GET", "/api/v1/applications/{application_id}", kiotaResponse);

                var api = CreateApi((client, config) => new ApplicationsApi(client, config));

                // Act
            var response = await api.GetApplicationAsync("app_123");

                // Assert
                Assert.NotNull(response);
            Assert.Equal("200", response.Code);
            Assert.NotNull(response.Application);
            Assert.Equal("app_123", response.Application.Id);
            Assert.Equal("My Application", response.Application.Name);
            _output.WriteLine("GetApplication returned correct application");
        }

        #endregion

        #region CreateApplication Tests

        [Fact]
        [Trait("TestMode", "Mock")]
        public async Task CreateApplication_Mock_Created()
        {
            // Arrange
            if (UseRealApi) return;

            var mockHandler = GetKiotaMockHandler();
            if (mockHandler == null) return;

            var kiotaResponse = new KiotaModels.Create_application_response
            {
                Code = "201",
                Message = "Application created"
            };
            mockHandler.AddKiotaResponse("POST", "/api/v1/applications", kiotaResponse);

                var api = CreateApi((client, config) => new ApplicationsApi(client, config));
            // CreateApplicationRequest requires name in constructor (throws if null)
            var request = new CreateApplicationRequest(
                name: "New Application",
                type: CreateApplicationRequest.TypeEnum.Reg
            );

                // Act
            var response = await api.CreateApplicationAsync(request);

            // Assert
            Assert.NotNull(response);
            Assert.Equal("201", response.Code);
            Assert.True(mockHandler.WasRequestMade("POST", "/api/v1/applications"));
            _output.WriteLine("CreateApplication completed successfully");
        }

        #endregion

        #region UpdateApplication Tests

        [Fact]
        [Trait("TestMode", "Mock")]
        public async Task UpdateApplication_Mock_Updated()
        {
            // Arrange
            if (UseRealApi) return;

            var mockHandler = GetKiotaMockHandler();
            if (mockHandler == null) return;

            var kiotaResponse = new KiotaModels.Success_response
            {
                Code = "200",
                Message = "Application updated"
            };
            mockHandler.AddKiotaResponse("PATCH", "/api/v1/applications/{application_id}", kiotaResponse);

                var api = CreateApi((client, config) => new ApplicationsApi(client, config));
            var request = new UpdateApplicationRequest
            {
                Name = "Updated Application Name"
            };

            // Act
            await api.UpdateApplicationAsync("app_123", request);

            // Assert
            Assert.True(mockHandler.WasRequestMade("PATCH", "/api/v1/applications/app_123"));
            _output.WriteLine("UpdateApplication completed successfully");
        }

        #endregion

        #region DeleteApplication Tests

        [Fact]
        [Trait("TestMode", "Mock")]
        public async Task DeleteApplication_Mock_Deleted()
        {
            // Arrange
            if (UseRealApi) return;

            var mockHandler = GetKiotaMockHandler();
            if (mockHandler == null) return;

            var kiotaResponse = new KiotaModels.Success_response
            {
                Code = "200",
                Message = "Application deleted"
            };
            mockHandler.AddKiotaResponse("DELETE", "/api/v1/applications/{application_id}", kiotaResponse);

                var api = CreateApi((client, config) => new ApplicationsApi(client, config));

                // Act
            await api.DeleteApplicationAsync("app_to_delete");

                // Assert
            Assert.True(mockHandler.WasRequestMade("DELETE", "/api/v1/applications/app_to_delete"));
            _output.WriteLine("DeleteApplication completed successfully");
        }

        #endregion

        #region Application Connections Tests

        [Fact]
        [Trait("TestMode", "Mock")]
        public async Task GetApplicationConnections_Mock_ReturnsConnections()
        {
            // Arrange
            if (UseRealApi) return;

            var mockHandler = GetKiotaMockHandler();
            if (mockHandler == null) return;

            var kiotaResponse = new KiotaModels.Get_connections_response
            {
                Code = "200",
                Message = "Connections retrieved"
            };
            mockHandler.AddKiotaResponse("GET", "/api/v1/applications/{application_id}/connections", kiotaResponse);

                var api = CreateApi((client, config) => new ApplicationsApi(client, config));

                // Act
            var response = await api.GetApplicationConnectionsAsync("app_123");

            // Assert
            Assert.NotNull(response);
            Assert.Equal("200", response.Code);
            _output.WriteLine("GetApplicationConnections returned correctly");
        }

        #endregion

        #region Null Handling Tests

        [Fact]
        [Trait("TestMode", "Mock")]
        public async Task GetApplications_Mock_NullApplicationsList_StaysNull()
        {
            // Arrange
            if (UseRealApi) return;

            var mockHandler = GetKiotaMockHandler();
            if (mockHandler == null) return;

            var kiotaResponse = new KiotaModels.Get_applications_response
            {
                Code = "200",
                Message = "Success",
                Applications = null
            };
            mockHandler.AddKiotaResponse("GET", "/api/v1/applications", kiotaResponse);

                var api = CreateApi((client, config) => new ApplicationsApi(client, config));

                // Act
            var response = await api.GetApplicationsAsync();

                // Assert
                Assert.NotNull(response);
            Assert.Null(response.Applications);
            _output.WriteLine("Null applications list preserved correctly");
        }

        #endregion

        #region Error Handling Tests

        [Fact]
        [Trait("TestMode", "Mock")]
        public async Task GetApplication_Mock_NotFound_ThrowsException()
        {
            // Arrange
            if (UseRealApi) return;

            var mockHandler = GetKiotaMockHandler();
            if (mockHandler == null) return;

            mockHandler.AddErrorResponse("GET", "/api/v1/applications/{application_id}", 
                System.Net.HttpStatusCode.NotFound, "not_found", "Application not found");

                var api = CreateApi((client, config) => new ApplicationsApi(client, config));

            // Act & Assert
            var exception = await Assert.ThrowsAsync<ApiException>(() => api.GetApplicationAsync("nonexistent"));
            Assert.Equal(404, exception.ErrorCode);
            _output.WriteLine("404 error handled correctly");
        }

        [Fact]
        [Trait("TestMode", "Mock")]
        public async Task CreateApplication_Mock_Conflict_ThrowsException()
        {
            // Arrange
            if (UseRealApi) return;

            var mockHandler = GetKiotaMockHandler();
            if (mockHandler == null) return;

            mockHandler.AddErrorResponse("POST", "/api/v1/applications", 
                System.Net.HttpStatusCode.Conflict, "conflict", "Application with this name already exists");

                var api = CreateApi((client, config) => new ApplicationsApi(client, config));
            // CreateApplicationRequest requires name in constructor (throws if null)
            var request = new CreateApplicationRequest(
                name: "Duplicate App",
                type: CreateApplicationRequest.TypeEnum.Reg
            );

            // Act & Assert
            var exception = await Assert.ThrowsAsync<ApiException>(() => api.CreateApplicationAsync(request));
            Assert.Equal(409, exception.ErrorCode);
            _output.WriteLine("409 error handled correctly");
        }

        [Fact]
        [Trait("TestMode", "Mock")]
        public async Task DeleteApplication_Mock_Unauthorized_ThrowsException()
        {
            // Arrange
            if (UseRealApi) return;

            var mockHandler = GetKiotaMockHandler();
            if (mockHandler == null) return;

            mockHandler.AddErrorResponse("DELETE", "/api/v1/applications/{application_id}", 
                System.Net.HttpStatusCode.Unauthorized, "unauthorized", "Invalid access token");

                var api = CreateApi((client, config) => new ApplicationsApi(client, config));

            // Act & Assert
            var exception = await Assert.ThrowsAsync<ApiException>(() => api.DeleteApplicationAsync("app_123"));
            Assert.Equal(401, exception.ErrorCode);
            _output.WriteLine("401 error handled correctly");
        }

        #endregion
    }
}
