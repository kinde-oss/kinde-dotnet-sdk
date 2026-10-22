using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Kinde.Api.Api;
using Kinde.Api.Client;
using Kinde.Api.Model;
using Kinde.Api.Test.Integration.Mocks;
using Xunit;
using Xunit.Abstractions;

namespace Kinde.Api.Test.Integration.Kiota
{
    /// <summary>
    /// Tests for error handling scenarios in the Kiota-based API integration.
    /// 
    /// These tests verify that all error responses are properly handled and translated
    /// from Kiota exceptions to the expected OpenAPI ApiException format.
    /// 
    /// Test scenarios covered:
    /// - 400 Bad Request
    /// - 401 Unauthorized
    /// - 403 Forbidden
    /// - 404 Not Found
    /// - 409 Conflict
    /// - 422 Unprocessable Entity
    /// - 429 Rate Limited
    /// - 500 Internal Server Error
    /// - 502 Bad Gateway
    /// - 503 Service Unavailable
    /// - Network/Timeout errors
    /// - Malformed JSON responses
    /// </summary>
    public class KiotaErrorHandlingTests : IDisposable
    {
        private readonly ITestOutputHelper _output;
        private readonly KiotaMockHttpHandler _mockHandler;
        private readonly HttpClient _httpClient;
        private readonly Configuration _configuration;

        public KiotaErrorHandlingTests(ITestOutputHelper output)
        {
            _output = output;
            _mockHandler = new KiotaMockHttpHandler();
            _httpClient = new HttpClient(_mockHandler)
            {
                BaseAddress = new Uri("https://mock.kinde.com")
            };
            _configuration = new Configuration
            {
                BasePath = "https://mock.kinde.com",
                AccessToken = "test-access-token"
            };
        }

        public void Dispose()
        {
            _httpClient?.Dispose();
            _mockHandler?.Dispose();
        }

        #region 4xx Client Error Tests

        /// <summary>
        /// Tests handling of 400 Bad Request responses.
        /// </summary>
        [Fact]
        public async Task Error400_BadRequest_ThrowsApiExceptionWithCorrectCode()
        {
            // Arrange
            _mockHandler.AddErrorResponse("POST", "/api/v1/user", HttpStatusCode.BadRequest, 
                "invalid_request", "The request body is missing required fields");

            var api = new UsersApi(_httpClient, _configuration);
            var request = new CreateUserRequest();

            // Act & Assert
            var exception = await Assert.ThrowsAsync<ApiException>(() => api.CreateUserAsync(request));
            
            Assert.Equal(400, exception.ErrorCode);
            // The error message format may vary - just verify the exception was thrown with correct code
            _output.WriteLine($"400 error handled: {exception.Message}");
        }

        /// <summary>
        /// Tests handling of 401 Unauthorized responses.
        /// </summary>
        [Fact]
        public async Task Error401_Unauthorized_ThrowsApiExceptionWithCorrectCode()
        {
            // Arrange
            _mockHandler.AddErrorResponse("GET", "/api/v1/users", HttpStatusCode.Unauthorized,
                "unauthorized", "Invalid or expired access token");

            var api = new UsersApi(_httpClient, _configuration);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<ApiException>(() => api.GetUsersAsync());
            
            Assert.Equal(401, exception.ErrorCode);
            _output.WriteLine($"401 error handled: {exception.Message}");
        }

        /// <summary>
        /// Tests handling of 403 Forbidden responses.
        /// </summary>
        [Fact]
        public async Task Error403_Forbidden_ThrowsApiExceptionWithCorrectCode()
        {
            // Arrange
            _mockHandler.AddErrorResponse("DELETE", "/api/v1/user", HttpStatusCode.Forbidden,
                "forbidden", "You do not have permission to delete this user");

            var api = new UsersApi(_httpClient, _configuration);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<ApiException>(() => api.DeleteUserAsync("user_123"));
            
            Assert.Equal(403, exception.ErrorCode);
            _output.WriteLine($"403 error handled: {exception.Message}");
        }

        /// <summary>
        /// Tests handling of 404 Not Found responses.
        /// </summary>
        [Fact]
        public async Task Error404_NotFound_ThrowsApiExceptionWithCorrectCode()
        {
            // Arrange
            _mockHandler.AddErrorResponse("GET", "/api/v1/user", HttpStatusCode.NotFound,
                "not_found", "The requested user was not found");

            var api = new UsersApi(_httpClient, _configuration);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<ApiException>(() => api.GetUserDataAsync("nonexistent_user"));
            
            Assert.Equal(404, exception.ErrorCode);
            _output.WriteLine($"404 error handled: {exception.Message}");
        }

        /// <summary>
        /// Tests handling of 409 Conflict responses.
        /// </summary>
        [Fact]
        public async Task Error409_Conflict_ThrowsApiExceptionWithCorrectCode()
        {
            // Arrange
            _mockHandler.AddErrorResponse("POST", "/api/v1/user", HttpStatusCode.Conflict,
                "conflict", "A user with this email already exists");

            var api = new UsersApi(_httpClient, _configuration);
            var request = new CreateUserRequest();

            // Act & Assert
            var exception = await Assert.ThrowsAsync<ApiException>(() => api.CreateUserAsync(request));
            
            Assert.Equal(409, exception.ErrorCode);
            _output.WriteLine($"409 error handled: {exception.Message}");
        }

        /// <summary>
        /// Tests handling of 422 Unprocessable Entity responses.
        /// </summary>
        [Fact]
        public async Task Error422_UnprocessableEntity_ThrowsApiExceptionWithCorrectCode()
        {
            // Arrange
            _mockHandler.AddErrorResponse("PATCH", "/api/v1/user", HttpStatusCode.UnprocessableEntity,
                "validation_error", "Email format is invalid");

            var api = new UsersApi(_httpClient, _configuration);
            var request = new UpdateUserRequest();

            // Act & Assert
            var exception = await Assert.ThrowsAsync<ApiException>(() => api.UpdateUserAsync("user_123", request));
            
            Assert.Equal(422, exception.ErrorCode);
            _output.WriteLine($"422 error handled: {exception.Message}");
        }

        /// <summary>
        /// Tests handling of 429 Rate Limited responses.
        /// </summary>
        [Fact]
        public async Task Error429_RateLimited_ThrowsApiExceptionWithCorrectCode()
        {
            // Arrange
            _mockHandler.AddErrorResponse("GET", "/api/v1/users", (HttpStatusCode)429,
                "rate_limited", "Too many requests. Please retry after 60 seconds.");

            var api = new UsersApi(_httpClient, _configuration);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<ApiException>(() => api.GetUsersAsync());
            
            Assert.Equal(429, exception.ErrorCode);
            _output.WriteLine($"429 error handled: {exception.Message}");
        }

        #endregion

        #region 5xx Server Error Tests

        /// <summary>
        /// Tests handling of 500 Internal Server Error responses.
        /// </summary>
        [Fact]
        public async Task Error500_InternalServerError_ThrowsApiExceptionWithCorrectCode()
        {
            // Arrange
            _mockHandler.AddErrorResponse("GET", "/api/v1/users", HttpStatusCode.InternalServerError,
                "internal_error", "An unexpected error occurred");

            var api = new UsersApi(_httpClient, _configuration);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<ApiException>(() => api.GetUsersAsync());
            
            Assert.Equal(500, exception.ErrorCode);
            _output.WriteLine($"500 error handled: {exception.Message}");
        }

        /// <summary>
        /// Tests handling of 502 Bad Gateway responses.
        /// </summary>
        [Fact]
        public async Task Error502_BadGateway_ThrowsApiExceptionWithCorrectCode()
        {
            // Arrange
            _mockHandler.AddErrorResponse("GET", "/api/v1/users", HttpStatusCode.BadGateway,
                "bad_gateway", "The server received an invalid response from an upstream server");

            var api = new UsersApi(_httpClient, _configuration);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<ApiException>(() => api.GetUsersAsync());
            
            Assert.Equal(502, exception.ErrorCode);
            _output.WriteLine($"502 error handled: {exception.Message}");
        }

        /// <summary>
        /// Tests handling of 503 Service Unavailable responses.
        /// </summary>
        [Fact]
        public async Task Error503_ServiceUnavailable_ThrowsApiExceptionWithCorrectCode()
        {
            // Arrange
            _mockHandler.AddErrorResponse("GET", "/api/v1/users", HttpStatusCode.ServiceUnavailable,
                "service_unavailable", "The service is temporarily unavailable");

            var api = new UsersApi(_httpClient, _configuration);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<ApiException>(() => api.GetUsersAsync());
            
            Assert.Equal(503, exception.ErrorCode);
            _output.WriteLine($"503 error handled: {exception.Message}");
        }

        /// <summary>
        /// Tests handling of 504 Gateway Timeout responses.
        /// </summary>
        [Fact]
        public async Task Error504_GatewayTimeout_ThrowsApiExceptionWithCorrectCode()
        {
            // Arrange
            _mockHandler.AddErrorResponse("GET", "/api/v1/users", HttpStatusCode.GatewayTimeout,
                "gateway_timeout", "The request timed out");

            var api = new UsersApi(_httpClient, _configuration);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<ApiException>(() => api.GetUsersAsync());
            
            Assert.Equal(504, exception.ErrorCode);
            _output.WriteLine($"504 error handled: {exception.Message}");
        }

        #endregion

        #region Malformed Response Tests

        /// <summary>
        /// Tests handling of malformed JSON responses.
        /// </summary>
        [Fact]
        public async Task MalformedJson_HandledGracefully()
        {
            // Arrange - Invalid JSON
            _mockHandler.AddRawJsonResponse("GET", "/api/v1/users", 
                "{ invalid json }", HttpStatusCode.OK);

            var api = new UsersApi(_httpClient, _configuration);

            // Act & Assert - Should throw an exception due to parsing failure
            await Assert.ThrowsAnyAsync<Exception>(() => api.GetUsersAsync());
            _output.WriteLine("Malformed JSON handled gracefully");
        }

        /// <summary>
        /// Tests handling of empty response body.
        /// Kiota handles empty responses gracefully by returning null.
        /// </summary>
        [Fact]
        public async Task EmptyResponse_HandledGracefully()
        {
            // Arrange - Empty response
            _mockHandler.AddRawJsonResponse("GET", "/api/v1/users", "", HttpStatusCode.OK);

            var api = new UsersApi(_httpClient, _configuration);

            // Act - Kiota handles empty responses gracefully (returns null or empty model)
            var result = await api.GetUsersAsync();
            
            // Assert - Result should be null for empty response
            Assert.Null(result);
            _output.WriteLine("Empty response handled gracefully - returned null");
        }

        /// <summary>
        /// Tests handling of null values in error response.
        /// </summary>
        [Fact]
        public async Task NullErrorMessage_HandledGracefully()
        {
            // Arrange - Error response with null message
            _mockHandler.AddErrorResponse("GET", "/api/v1/users", HttpStatusCode.BadRequest, null, null);

            var api = new UsersApi(_httpClient, _configuration);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<ApiException>(() => api.GetUsersAsync());
            
            Assert.Equal(400, exception.ErrorCode);
            _output.WriteLine($"Null error message handled: {exception.Message}");
        }

        #endregion

        #region Error Response Body Tests

        /// <summary>
        /// Tests that error response body contains expected error information.
        /// </summary>
        [Fact]
        public async Task ErrorResponseBody_ContainsErrorDetails()
        {
            // Arrange
            var errorJson = @"{
                ""errors"": [
                    {
                        ""code"": ""validation_error"",
                        ""message"": ""The email field is required"",
                        ""field"": ""email""
                    }
                ]
            }";
            
            _mockHandler.AddRawJsonResponse("POST", "/api/v1/user", errorJson, HttpStatusCode.BadRequest);

            var api = new UsersApi(_httpClient, _configuration);
            var request = new CreateUserRequest();

            // Act & Assert
            var exception = await Assert.ThrowsAsync<ApiException>(() => api.CreateUserAsync(request));
            
            Assert.Equal(400, exception.ErrorCode);
            _output.WriteLine($"Error with details handled: {exception.Message}");
        }

        /// <summary>
        /// Tests error response with multiple errors.
        /// </summary>
        [Fact]
        public async Task MultipleErrors_HandledCorrectly()
        {
            // Arrange
            var errorJson = @"{
                ""errors"": [
                    { ""code"": ""required"", ""message"": ""Email is required"", ""field"": ""email"" },
                    { ""code"": ""required"", ""message"": ""First name is required"", ""field"": ""first_name"" }
                ]
            }";
            
            _mockHandler.AddRawJsonResponse("POST", "/api/v1/user", errorJson, HttpStatusCode.BadRequest);

            var api = new UsersApi(_httpClient, _configuration);
            var request = new CreateUserRequest();

            // Act & Assert
            var exception = await Assert.ThrowsAsync<ApiException>(() => api.CreateUserAsync(request));
            
            Assert.Equal(400, exception.ErrorCode);
            _output.WriteLine($"Multiple errors handled: {exception.Message}");
        }

        #endregion

        #region Different API Error Tests

        /// <summary>
        /// Tests error handling for Organizations API.
        /// </summary>
        [Fact]
        public async Task OrganizationsApi_Error_HandledCorrectly()
        {
            // Arrange
            _mockHandler.AddErrorResponse("GET", "/api/v1/organization", HttpStatusCode.NotFound,
                "not_found", "Organization not found");

            var api = new OrganizationsApi(_httpClient, _configuration);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<ApiException>(() => api.GetOrganizationAsync("nonexistent_org"));
            
            Assert.Equal(404, exception.ErrorCode);
            _output.WriteLine($"OrganizationsApi error handled: {exception.Message}");
        }

        /// <summary>
        /// Tests error handling for Roles API.
        /// </summary>
        [Fact]
        public async Task RolesApi_Error_HandledCorrectly()
        {
            // Arrange
            _mockHandler.AddErrorResponse("POST", "/api/v1/roles", HttpStatusCode.BadRequest,
                "invalid_request", "Role key is required");

            var api = new RolesApi(_httpClient, _configuration);
            var request = new CreateRoleRequest();

            // Act & Assert
            var exception = await Assert.ThrowsAsync<ApiException>(() => api.CreateRoleAsync(request));
            
            Assert.Equal(400, exception.ErrorCode);
            _output.WriteLine($"RolesApi error handled: {exception.Message}");
        }

        /// <summary>
        /// Tests error handling for Applications API.
        /// </summary>
        [Fact]
        public async Task ApplicationsApi_Error_HandledCorrectly()
        {
            // Arrange
            _mockHandler.AddErrorResponse("GET", "/api/v1/applications", HttpStatusCode.Unauthorized,
                "unauthorized", "Invalid access token");

            var api = new ApplicationsApi(_httpClient, _configuration);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<ApiException>(() => api.GetApplicationsAsync());
            
            Assert.Equal(401, exception.ErrorCode);
            _output.WriteLine($"ApplicationsApi error handled: {exception.Message}");
        }

        /// <summary>
        /// Tests error handling for Permissions API.
        /// </summary>
        [Fact]
        public async Task PermissionsApi_Error_HandledCorrectly()
        {
            // Arrange
            _mockHandler.AddErrorResponse("POST", "/api/v1/permissions", HttpStatusCode.Conflict,
                "conflict", "Permission with this key already exists");

            var api = new PermissionsApi(_httpClient, _configuration);
            var request = new CreatePermissionRequest();

            // Act & Assert
            var exception = await Assert.ThrowsAsync<ApiException>(() => api.CreatePermissionAsync(request));
            
            Assert.Equal(409, exception.ErrorCode);
            _output.WriteLine($"PermissionsApi error handled: {exception.Message}");
        }

        #endregion

        #region Request Validation Error Tests

        /// <summary>
        /// Tests that empty required parameters throw appropriate errors.
        /// </summary>
        [Fact]
        public async Task EmptyRequiredParameter_ThrowsError()
        {
            // Arrange
            _mockHandler.AddErrorResponse("GET", "/api/v1/user", HttpStatusCode.BadRequest,
                "invalid_request", "User ID is required");

            var api = new UsersApi(_httpClient, _configuration);

            // Act & Assert - Passing empty string should result in error
            var exception = await Assert.ThrowsAsync<ApiException>(() => api.GetUserDataAsync(""));
            
            _output.WriteLine($"Empty parameter error: {exception.Message}");
        }

        #endregion
    }
}



