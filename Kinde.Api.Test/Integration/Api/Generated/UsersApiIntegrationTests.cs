using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Kinde.Api.Api;
using Kinde.Api.Model;
using Kinde.Api.Test.Integration;
using Xunit;
using Xunit.Abstractions;

namespace Kinde.Api.Test.Integration.Api.Generated
{
    /// <summary>
    /// Auto-generated integration tests for UsersApi with both mock and real API support
    /// </summary>
    public class UsersApiIntegrationTests : BaseIntegrationTest, IClassFixture<TestResourceFixture>
    {
        private readonly ITestOutputHelper _output;
        private readonly TestResourceFixture _fixture;

        public UsersApiIntegrationTests(ITestOutputHelper output, TestResourceFixture fixture) : base()
        {
            _output = output;
            _fixture = fixture;
        }


        [Fact]
        [Trait("TestMode", "Mock")]
        public async Task GetUsers_Mock_Test()
        {
            // Arrange
            if (UseRealApi)
            {
                _output.WriteLine("Skipping Mock test - using real API");
                return;
            }

            // Act & Assert
            try
            {
                var mockHandler = GetMockHandler();
                if (mockHandler == null)
                {
                    _output.WriteLine("Mock handler not available");
                    return;
                }

                var mockResponse = new UsersResponse();
                mockHandler.AddResponse("GET", "/api/v1/users", mockResponse);

                var api = CreateApi((client, config) => new UsersApi(client, config));

                // Act
                var response = await api.GetUsersAsync();

                // Assert
                Assert.NotNull(response);
                _output.WriteLine($"Mock test successful");
            }
            catch (Exception ex)
            {
                _output.WriteLine($"Error in GetUsers test: {ex.Message}");
                throw;
            }
        }


        [Fact]
        [Trait("TestMode", "Real")]
        public async Task GetUsers_Real_Test()
        {
            // Arrange
            if (!UseRealApi)
            {
                _output.WriteLine("Skipping Real test - using mocks");
                return;
            }

            // Act & Assert
            try
            {
                // Using test resource from fixture: UserId
                var user_id = _fixture.UserId;

                var api = CreateApi((client, config) => new UsersApi(client, config));

                var response = await api.GetUsersAsync(userId: user_id);

                // Assert
                Assert.NotNull(response);
                _output.WriteLine($"Response received: {response?.GetType().Name}");
                _output.WriteLine($"Test completed successfully");
            }
            catch (Exception ex)
            {
                _output.WriteLine($"Error in GetUsers test: {ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// Test that GetUsersAsync returns all fields correctly (fixes issue where fields were null)
        /// This test verifies that GetUsersAsync behaves the same as GetUsersWithHttpInfoAsync
        /// </summary>
        [Fact]
        [Trait("TestMode", "Mock")]
        public async Task GetUsersAsync_ReturnsAllFields_Test()
        {
            // Arrange
            if (UseRealApi)
            {
                _output.WriteLine("Skipping Mock test - using real API");
                return;
            }

            try
            {
                var mockHandler = GetMockHandler();
                if (mockHandler == null)
                {
                    _output.WriteLine("Mock handler not available");
                    return;
                }

                // Create a mock response with all fields populated
                var mockUser = new UsersResponseUsersInner
                {
                    Id = "test_user_id",
                    Email = "test@example.com",
                    FirstName = "John",
                    LastName = "Doe",
                    Username = "johndoe",
                    Phone = "1234567890",
                    IsSuspended = false,
                    Picture = "https://example.com/picture.jpg",
                    TotalSignIns = 10,
                    FailedSignIns = 0,
                    LastSignedIn = "2024-01-01T00:00:00Z",
                    CreatedOn = "2023-01-01T00:00:00Z"
                };

                var mockResponse = new UsersResponse
                {
                    Code = "200",
                    Message = "Success",
                    Users = new List<UsersResponseUsersInner> { mockUser },
                    NextToken = "next_token_value"
                };

                mockHandler.AddResponse("GET", "/api/v1/users", mockResponse);

                var api = CreateApi((client, config) => new UsersApi(client, config));

                // Act - Test GetUsersAsync
                var responseAsync = await api.GetUsersAsync();

                // Act - Test GetUsersWithHttpInfoAsync for comparison
                var responseWithHttpInfo = await api.GetUsersWithHttpInfoAsync();

                // Assert - Verify GetUsersAsync returns all fields
                Assert.NotNull(responseAsync);
                Assert.NotNull(responseAsync.Users);
                Assert.Single(responseAsync.Users);
                
                var user = responseAsync.Users[0];
                Assert.Equal("test_user_id", user.Id);
                Assert.Equal("test@example.com", user.Email);
                Assert.Equal("John", user.FirstName);
                Assert.Equal("Doe", user.LastName);
                Assert.Equal("johndoe", user.Username);
                Assert.Equal("1234567890", user.Phone);
                Assert.False(user.IsSuspended);
                Assert.Equal("https://example.com/picture.jpg", user.Picture);
                Assert.Equal(10, user.TotalSignIns);
                Assert.Equal(0, user.FailedSignIns);
                Assert.Equal("2024-01-01T00:00:00Z", user.LastSignedIn);
                Assert.Equal("2023-01-01T00:00:00Z", user.CreatedOn);
                Assert.Equal("next_token_value", responseAsync.NextToken);

                // Assert - Verify GetUsersWithHttpInfoAsync also returns all fields (should be the same)
                Assert.NotNull(responseWithHttpInfo);
                Assert.NotNull(responseWithHttpInfo.Data);
                Assert.NotNull(responseWithHttpInfo.Data.Users);
                Assert.Single(responseWithHttpInfo.Data.Users);
                
                var userWithHttpInfo = responseWithHttpInfo.Data.Users[0];
                Assert.Equal("test_user_id", userWithHttpInfo.Id);
                Assert.Equal("test@example.com", userWithHttpInfo.Email);
                Assert.Equal("John", userWithHttpInfo.FirstName);
                Assert.Equal("Doe", userWithHttpInfo.LastName);
                Assert.Equal("johndoe", userWithHttpInfo.Username);
                Assert.Equal("1234567890", userWithHttpInfo.Phone);
                Assert.False(userWithHttpInfo.IsSuspended);
                Assert.Equal("https://example.com/picture.jpg", userWithHttpInfo.Picture);
                Assert.Equal(10, userWithHttpInfo.TotalSignIns);
                Assert.Equal(0, userWithHttpInfo.FailedSignIns);
                Assert.Equal("2024-01-01T00:00:00Z", userWithHttpInfo.LastSignedIn);
                Assert.Equal("2023-01-01T00:00:00Z", userWithHttpInfo.CreatedOn);
                Assert.Equal("next_token_value", responseWithHttpInfo.Data.NextToken);

                // Assert - Both methods should return the same data
                Assert.Equal(responseAsync.NextToken, responseWithHttpInfo.Data.NextToken);
                Assert.Equal(responseAsync.Users[0].Id, responseWithHttpInfo.Data.Users[0].Id);
                Assert.Equal(responseAsync.Users[0].FirstName, responseWithHttpInfo.Data.Users[0].FirstName);
                Assert.Equal(responseAsync.Users[0].LastName, responseWithHttpInfo.Data.Users[0].LastName);

                _output.WriteLine("GetUsersAsync_ReturnsAllFields_Test passed - all fields are populated correctly");
            }
            catch (Exception ex)
            {
                _output.WriteLine($"Error in GetUsersAsync_ReturnsAllFields_Test: {ex.Message}");
                _output.WriteLine($"Stack trace: {ex.StackTrace}");
                throw;
            }
        }


        [Fact]
        [Trait("TestMode", "Mock")]
        public async Task RefreshUserClaims_Mock_Test()
        {
            // Arrange
            if (UseRealApi)
            {
                _output.WriteLine("Skipping Mock test - using real API");
                return;
            }

            // Act & Assert
            try
            {
                var mockHandler = GetMockHandler();
                if (mockHandler == null)
                {
                    _output.WriteLine("Mock handler not available");
                    return;
                }

                var user_id = "test-user_id";
                var mockResponse = new SuccessResponse();
                mockHandler.AddResponse("POST", $"/api/v1/users/{user_id}/refresh_claims", mockResponse);

                var api = CreateApi((client, config) => new UsersApi(client, config));

                // Act
                var response = await api.RefreshUserClaimsAsync(user_id);

                // Assert
                Assert.NotNull(response);
                _output.WriteLine($"Mock test successful");
            }
            catch (Exception ex)
            {
                _output.WriteLine($"Error in RefreshUserClaims test: {ex.Message}");
                throw;
            }
        }


        [Fact]
        [Trait("TestMode", "Real")]
        public async Task RefreshUserClaims_Real_Test()
        {
            // Arrange
            if (!UseRealApi)
            {
                _output.WriteLine("Skipping Real test - using mocks");
                return;
            }

            // Act & Assert
            try
            {
                // Using test resource from fixture: UserId
                var user_id = _fixture.UserId;

                var api = CreateApi((client, config) => new UsersApi(client, config));

                var response = await api.RefreshUserClaimsAsync(user_id);

                // Assert
                Assert.NotNull(response);
                _output.WriteLine($"Response received: {response?.GetType().Name}");
                _output.WriteLine($"Test completed successfully");
            }
            catch (Exception ex)
            {
                _output.WriteLine($"Error in RefreshUserClaims test: {ex.Message}");
                throw;
            }
        }


        [Fact]
        [Trait("TestMode", "Mock")]
        public async Task GetUserData_Mock_Test()
        {
            // Arrange
            if (UseRealApi)
            {
                _output.WriteLine("Skipping Mock test - using real API");
                return;
            }

            // Act & Assert
            try
            {
                var mockHandler = GetMockHandler();
                if (mockHandler == null)
                {
                    _output.WriteLine("Mock handler not available");
                    return;
                }

                var id = "id";
                var mockResponse = new User();
                mockHandler.AddResponse("GET", "/api/v1/user", mockResponse);

                var api = CreateApi((client, config) => new UsersApi(client, config));

                // Act
                var response = await api.GetUserDataAsync(id);

                // Assert
                Assert.NotNull(response);
                _output.WriteLine($"Mock test successful");
            }
            catch (Exception ex)
            {
                _output.WriteLine($"Error in GetUserData test: {ex.Message}");
                throw;
            }
        }


        [Fact]
        [Trait("TestMode", "Real")]
        public async Task GetUserData_Real_Test()
        {
            // Arrange
            if (!UseRealApi)
            {
                _output.WriteLine("Skipping Real test - using mocks");
                return;
            }

            // Act & Assert
            try
            {
                var id = "id";

                var api = CreateApi((client, config) => new UsersApi(client, config));

                var response = await api.GetUserDataAsync(id);

                // Assert
                Assert.NotNull(response);
                _output.WriteLine($"Response received: {response?.GetType().Name}");
                _output.WriteLine($"Test completed successfully");
            }
            catch (Exception ex)
            {
                _output.WriteLine($"Error in GetUserData test: {ex.Message}");
                throw;
            }
        }


        [Fact]
        [Trait("TestMode", "Mock")]
        public async Task CreateUser_Mock_Test()
        {
            // Arrange
            if (UseRealApi)
            {
                _output.WriteLine("Skipping Mock test - using real API");
                return;
            }

            // Act & Assert
            try
            {
                var mockHandler = GetMockHandler();
                if (mockHandler == null)
                {
                    _output.WriteLine("Mock handler not available");
                    return;
                }

                var mockResponse = new CreateUserResponse();
                mockHandler.AddResponse("POST", "/api/v1/user", mockResponse);
                var request = new CreateUserRequest();

                var api = CreateApi((client, config) => new UsersApi(client, config));

                // Act
                var response = await api.CreateUserAsync(request);

                // Assert
                Assert.NotNull(response);
                _output.WriteLine($"Mock test successful");
            }
            catch (Exception ex)
            {
                _output.WriteLine($"Error in CreateUser test: {ex.Message}");
                throw;
            }
        }


        [Fact]
        [Trait("TestMode", "Real")]
        public async Task CreateUser_Real_Test()
        {
            // Arrange
            if (!UseRealApi)
            {
                _output.WriteLine("Skipping Real test - using mocks");
                return;
            }

            // Act & Assert
            try
            {
                var request = new CreateUserRequest();

                var api = CreateApi((client, config) => new UsersApi(client, config));

                var response = await api.CreateUserAsync(request);

                // Assert
                Assert.NotNull(response);
                _output.WriteLine($"Response received: {response?.GetType().Name}");
                _output.WriteLine($"Test completed successfully");
            }
            catch (Exception ex)
            {
                _output.WriteLine($"Error in CreateUser test: {ex.Message}");
                throw;
            }
        }


        [Fact]
        [Trait("TestMode", "Mock")]
        public async Task UpdateUser_Mock_Test()
        {
            // Arrange
            if (UseRealApi)
            {
                _output.WriteLine("Skipping Mock test - using real API");
                return;
            }

            // Act & Assert
            try
            {
                var mockHandler = GetMockHandler();
                if (mockHandler == null)
                {
                    _output.WriteLine("Mock handler not available");
                    return;
                }

                var id = "id";
                var mockResponse = new UpdateUserResponse();
                mockHandler.AddResponse("PATCH", "/api/v1/user", mockResponse);
                var request = new UpdateUserRequest();

                var api = CreateApi((client, config) => new UsersApi(client, config));

                // Act
                var response = await api.UpdateUserAsync(id, request);

                // Assert
                Assert.NotNull(response);
                _output.WriteLine($"Mock test successful");
            }
            catch (Exception ex)
            {
                _output.WriteLine($"Error in UpdateUser test: {ex.Message}");
                throw;
            }
        }


        [Fact]
        [Trait("TestMode", "Real")]
        public async Task UpdateUser_Real_Test()
        {
            // Arrange
            if (!UseRealApi)
            {
                _output.WriteLine("Skipping Real test - using mocks");
                return;
            }

            // Act & Assert
            try
            {
                var id = "id";
                var request = new UpdateUserRequest();

                var api = CreateApi((client, config) => new UsersApi(client, config));

                var response = await api.UpdateUserAsync(id, request);

                // Assert
                Assert.NotNull(response);
                _output.WriteLine($"Response received: {response?.GetType().Name}");
                _output.WriteLine($"Test completed successfully");
            }
            catch (Exception ex)
            {
                _output.WriteLine($"Error in UpdateUser test: {ex.Message}");
                throw;
            }
        }


        [Fact]
        [Trait("TestMode", "Mock")]
        public async Task DeleteUser_Mock_Test()
        {
            // Arrange
            if (UseRealApi)
            {
                _output.WriteLine("Skipping Mock test - using real API");
                return;
            }

            // Act & Assert
            try
            {
                var mockHandler = GetMockHandler();
                if (mockHandler == null)
                {
                    _output.WriteLine("Mock handler not available");
                    return;
                }

                var id = "id";
                var mockResponse = new SuccessResponse();
                mockHandler.AddResponse("DELETE", "/api/v1/user", mockResponse);

                var api = CreateApi((client, config) => new UsersApi(client, config));

                // Act
                await api.DeleteUserAsync(id);
                // Void method - no response to check
                _output.WriteLine($"Mock test successful");
            }
            catch (Exception ex)
            {
                _output.WriteLine($"Error in DeleteUser test: {ex.Message}");
                throw;
            }
        }


        [Fact]
        [Trait("TestMode", "Real")]
        public async Task DeleteUser_Real_Test()
        {
            // Arrange
            if (!UseRealApi)
            {
                _output.WriteLine("Skipping Real test - using mocks");
                return;
            }

            // Act & Assert
            try
            {
                var id = "id";

                var api = CreateApi((client, config) => new UsersApi(client, config));

                await api.DeleteUserAsync(id);
                // Void method - no response to check
                _output.WriteLine($"Void method completed successfully");
                _output.WriteLine($"Test completed successfully");
            }
            catch (Exception ex)
            {
                _output.WriteLine($"Error in DeleteUser test: {ex.Message}");
                throw;
            }
        }


        [Fact]
        [Trait("TestMode", "Mock")]
        public async Task UpdateUserFeatureFlagOverride_Mock_Test()
        {
            // Arrange
            if (UseRealApi)
            {
                _output.WriteLine("Skipping Mock test - using real API");
                return;
            }

            // Act & Assert
            try
            {
                var mockHandler = GetMockHandler();
                if (mockHandler == null)
                {
                    _output.WriteLine("Mock handler not available");
                    return;
                }

                var user_id = "test-user_id";
                var feature_flag_key = "test-feature_flag_key";
                var value = "value";
                var mockResponse = new SuccessResponse();
                mockHandler.AddResponse("PATCH", $"/api/v1/users/{user_id}/feature_flags/{feature_flag_key}", mockResponse);

                var api = CreateApi((client, config) => new UsersApi(client, config));

                // Act
                var response = await api.UpdateUserFeatureFlagOverrideAsync(user_id, feature_flag_key, value);

                // Assert
                Assert.NotNull(response);
                _output.WriteLine($"Mock test successful");
            }
            catch (Exception ex)
            {
                _output.WriteLine($"Error in UpdateUserFeatureFlagOverride test: {ex.Message}");
                throw;
            }
        }


        [Fact]
        [Trait("TestMode", "Real")]
        public async Task UpdateUserFeatureFlagOverride_Real_Test()
        {
            // Arrange
            if (!UseRealApi)
            {
                _output.WriteLine("Skipping Real test - using mocks");
                return;
            }

            // Act & Assert
            try
            {
                // WARNING: Real API test - This operation requires existing user_id
                // This test may fail if the resource doesn't exist in your Kinde instance
                // Consider creating the resource first or using a test environment
                // Using test resource from fixture: UserId
                var user_id = _fixture.UserId;
                // WARNING: Using placeholder feature_flag_key - test will likely fail without real resource ID
                var feature_flag_key = "test-feature_flag_key";
                var value = "value";

                var api = CreateApi((client, config) => new UsersApi(client, config));

                var response = await api.UpdateUserFeatureFlagOverrideAsync(user_id, feature_flag_key, value);

                // Assert
                Assert.NotNull(response);
                _output.WriteLine($"Response received: {response?.GetType().Name}");
                _output.WriteLine($"Test completed successfully");
            }
            catch (Exception ex)
            {
                _output.WriteLine($"Error in UpdateUserFeatureFlagOverride test: {ex.Message}");
                throw;
            }
        }


        [Fact]
        [Trait("TestMode", "Mock")]
        public async Task UpdateUserProperty_Mock_Test()
        {
            // Arrange
            if (UseRealApi)
            {
                _output.WriteLine("Skipping Mock test - using real API");
                return;
            }

            // Act & Assert
            try
            {
                var mockHandler = GetMockHandler();
                if (mockHandler == null)
                {
                    _output.WriteLine("Mock handler not available");
                    return;
                }

                var user_id = "test-user_id";
                var property_key = "test-property_key";
                var value = "value";
                var mockResponse = new SuccessResponse();
                mockHandler.AddResponse("PUT", $"/api/v1/users/{user_id}/properties/{property_key}", mockResponse);

                var api = CreateApi((client, config) => new UsersApi(client, config));

                // Act
                var response = await api.UpdateUserPropertyAsync(user_id, property_key, value);

                // Assert
                Assert.NotNull(response);
                _output.WriteLine($"Mock test successful");
            }
            catch (Exception ex)
            {
                _output.WriteLine($"Error in UpdateUserProperty test: {ex.Message}");
                throw;
            }
        }


        [Fact]
        [Trait("TestMode", "Real")]
        public async Task UpdateUserProperty_Real_Test()
        {
            // Arrange
            if (!UseRealApi)
            {
                _output.WriteLine("Skipping Real test - using mocks");
                return;
            }

            // Act & Assert
            try
            {
                // WARNING: Real API test - This operation requires existing user_id
                // This test may fail if the resource doesn't exist in your Kinde instance
                // Consider creating the resource first or using a test environment
                // Using test resource from fixture: UserId
                var user_id = _fixture.UserId;
                // Using test resource from fixture: PropertyKey
                var property_key = _fixture.PropertyKey;
                var value = "value";

                var api = CreateApi((client, config) => new UsersApi(client, config));

                var response = await api.UpdateUserPropertyAsync(user_id, property_key, value);

                // Assert
                Assert.NotNull(response);
                _output.WriteLine($"Response received: {response?.GetType().Name}");
                _output.WriteLine($"Test completed successfully");
            }
            catch (Exception ex)
            {
                _output.WriteLine($"Error in UpdateUserProperty test: {ex.Message}");
                throw;
            }
        }


        [Fact]
        [Trait("TestMode", "Mock")]
        public async Task GetUserPropertyValues_Mock_Test()
        {
            // Arrange
            if (UseRealApi)
            {
                _output.WriteLine("Skipping Mock test - using real API");
                return;
            }

            // Act & Assert
            try
            {
                var mockHandler = GetMockHandler();
                if (mockHandler == null)
                {
                    _output.WriteLine("Mock handler not available");
                    return;
                }

                var user_id = "test-user_id";
                var mockResponse = new GetPropertyValuesResponse();
                mockHandler.AddResponse("GET", $"/api/v1/users/{user_id}/properties", mockResponse);

                var api = CreateApi((client, config) => new UsersApi(client, config));

                // Act
                var response = await api.GetUserPropertyValuesAsync(user_id);

                // Assert
                Assert.NotNull(response);
                _output.WriteLine($"Mock test successful");
            }
            catch (Exception ex)
            {
                _output.WriteLine($"Error in GetUserPropertyValues test: {ex.Message}");
                throw;
            }
        }


        [Fact]
        [Trait("TestMode", "Real")]
        public async Task GetUserPropertyValues_Real_Test()
        {
            // Arrange
            if (!UseRealApi)
            {
                _output.WriteLine("Skipping Real test - using mocks");
                return;
            }

            // Act & Assert
            try
            {
                // Using test resource from fixture: UserId
                var user_id = _fixture.UserId;

                var api = CreateApi((client, config) => new UsersApi(client, config));

                var response = await api.GetUserPropertyValuesAsync(user_id);

                // Assert
                Assert.NotNull(response);
                _output.WriteLine($"Response received: {response?.GetType().Name}");
                _output.WriteLine($"Test completed successfully");
            }
            catch (Exception ex)
            {
                _output.WriteLine($"Error in GetUserPropertyValues test: {ex.Message}");
                throw;
            }
        }


        [Fact]
        [Trait("TestMode", "Mock")]
        public async Task UpdateUserProperties_Mock_Test()
        {
            // Arrange
            if (UseRealApi)
            {
                _output.WriteLine("Skipping Mock test - using real API");
                return;
            }

            // Act & Assert
            try
            {
                var mockHandler = GetMockHandler();
                if (mockHandler == null)
                {
                    _output.WriteLine("Mock handler not available");
                    return;
                }

                var user_id = "test-user_id";
                var mockResponse = new SuccessResponse();
                mockHandler.AddResponse("PATCH", $"/api/v1/users/{user_id}/properties", mockResponse);
                var request = new UpdateOrganizationPropertiesRequest(properties: "test-properties");

                var api = CreateApi((client, config) => new UsersApi(client, config));

                // Act
                var response = await api.UpdateUserPropertiesAsync(user_id, request);

                // Assert
                Assert.NotNull(response);
                _output.WriteLine($"Mock test successful");
            }
            catch (Exception ex)
            {
                _output.WriteLine($"Error in UpdateUserProperties test: {ex.Message}");
                throw;
            }
        }


        [Fact]
        [Trait("TestMode", "Real")]
        public async Task UpdateUserProperties_Real_Test()
        {
            // Arrange
            if (!UseRealApi)
            {
                _output.WriteLine("Skipping Real test - using mocks");
                return;
            }

            // Act & Assert
            try
            {
                // WARNING: Real API test - This operation requires existing user_id
                // This test may fail if the resource doesn't exist in your Kinde instance
                // Consider creating the resource first or using a test environment
                // Using test resource from fixture: UserId
                var user_id = _fixture.UserId;
                var request = new UpdateOrganizationPropertiesRequest(properties: "test-properties");

                var api = CreateApi((client, config) => new UsersApi(client, config));

                var response = await api.UpdateUserPropertiesAsync(user_id, request);

                // Assert
                Assert.NotNull(response);
                _output.WriteLine($"Response received: {response?.GetType().Name}");
                _output.WriteLine($"Test completed successfully");
            }
            catch (Exception ex)
            {
                _output.WriteLine($"Error in UpdateUserProperties test: {ex.Message}");
                throw;
            }
        }


        [Fact]
        [Trait("TestMode", "Mock")]
        public async Task SetUserPassword_Mock_Test()
        {
            // Arrange
            if (UseRealApi)
            {
                _output.WriteLine("Skipping Mock test - using real API");
                return;
            }

            // Act & Assert
            try
            {
                var mockHandler = GetMockHandler();
                if (mockHandler == null)
                {
                    _output.WriteLine("Mock handler not available");
                    return;
                }

                var user_id = "test-user_id";
                var mockResponse = new SuccessResponse();
                mockHandler.AddResponse("PUT", $"/api/v1/users/{user_id}/password", mockResponse);
                var request = new SetUserPasswordRequest(hashedPassword: "test-hashed_password");

                var api = CreateApi((client, config) => new UsersApi(client, config));

                // Act
                var response = await api.SetUserPasswordAsync(user_id, request);

                // Assert
                Assert.NotNull(response);
                _output.WriteLine($"Mock test successful");
            }
            catch (Exception ex)
            {
                _output.WriteLine($"Error in SetUserPassword test: {ex.Message}");
                throw;
            }
        }


        [Fact]
        [Trait("TestMode", "Real")]
        public async Task SetUserPassword_Real_Test()
        {
            // Arrange
            if (!UseRealApi)
            {
                _output.WriteLine("Skipping Real test - using mocks");
                return;
            }

            // Act & Assert
            try
            {
                // WARNING: Real API test - This operation requires existing user_id
                // This test may fail if the resource doesn't exist in your Kinde instance
                // Consider creating the resource first or using a test environment
                // Using test resource from fixture: UserId
                var user_id = _fixture.UserId;
                var request = new SetUserPasswordRequest(hashedPassword: "test-hashed_password");

                var api = CreateApi((client, config) => new UsersApi(client, config));

                var response = await api.SetUserPasswordAsync(user_id, request);

                // Assert
                Assert.NotNull(response);
                _output.WriteLine($"Response received: {response?.GetType().Name}");
                _output.WriteLine($"Test completed successfully");
            }
            catch (Exception ex)
            {
                _output.WriteLine($"Error in SetUserPassword test: {ex.Message}");
                throw;
            }
        }


        [Fact]
        [Trait("TestMode", "Mock")]
        public async Task GetUserIdentities_Mock_Test()
        {
            // Arrange
            if (UseRealApi)
            {
                _output.WriteLine("Skipping Mock test - using real API");
                return;
            }

            // Act & Assert
            try
            {
                var mockHandler = GetMockHandler();
                if (mockHandler == null)
                {
                    _output.WriteLine("Mock handler not available");
                    return;
                }

                var user_id = "test-user_id";
                var mockResponse = new GetIdentitiesResponse();
                mockHandler.AddResponse("GET", $"/api/v1/users/{user_id}/identities", mockResponse);

                var api = CreateApi((client, config) => new UsersApi(client, config));

                // Act
                var response = await api.GetUserIdentitiesAsync(user_id);

                // Assert
                Assert.NotNull(response);
                _output.WriteLine($"Mock test successful");
            }
            catch (Exception ex)
            {
                _output.WriteLine($"Error in GetUserIdentities test: {ex.Message}");
                throw;
            }
        }


        [Fact]
        [Trait("TestMode", "Real")]
        public async Task GetUserIdentities_Real_Test()
        {
            // Arrange
            if (!UseRealApi)
            {
                _output.WriteLine("Skipping Real test - using mocks");
                return;
            }

            // Act & Assert
            try
            {
                // Using test resource from fixture: UserId
                var user_id = _fixture.UserId;

                var api = CreateApi((client, config) => new UsersApi(client, config));

                var response = await api.GetUserIdentitiesAsync(user_id);

                // Assert
                Assert.NotNull(response);
                _output.WriteLine($"Response received: {response?.GetType().Name}");
                _output.WriteLine($"Test completed successfully");
            }
            catch (Exception ex)
            {
                _output.WriteLine($"Error in GetUserIdentities test: {ex.Message}");
                throw;
            }
        }


        [Fact]
        [Trait("TestMode", "Mock")]
        public async Task CreateUserIdentity_Mock_Test()
        {
            // Arrange
            if (UseRealApi)
            {
                _output.WriteLine("Skipping Mock test - using real API");
                return;
            }

            // Act & Assert
            try
            {
                var mockHandler = GetMockHandler();
                if (mockHandler == null)
                {
                    _output.WriteLine("Mock handler not available");
                    return;
                }

                var user_id = "test-user_id";
                var mockResponse = new CreateIdentityResponse();
                mockHandler.AddResponse("POST", $"/api/v1/users/{user_id}/identities", mockResponse);
                var request = new CreateUserIdentityRequest();

                var api = CreateApi((client, config) => new UsersApi(client, config));

                // Act
                var response = await api.CreateUserIdentityAsync(user_id, request);

                // Assert
                Assert.NotNull(response);
                _output.WriteLine($"Mock test successful");
            }
            catch (Exception ex)
            {
                _output.WriteLine($"Error in CreateUserIdentity test: {ex.Message}");
                throw;
            }
        }


        [Fact]
        [Trait("TestMode", "Real")]
        public async Task CreateUserIdentity_Real_Test()
        {
            // Arrange
            if (!UseRealApi)
            {
                _output.WriteLine("Skipping Real test - using mocks");
                return;
            }

            // Act & Assert
            try
            {
                // Using test resource from fixture: UserId
                var user_id = _fixture.UserId;
                var request = new CreateUserIdentityRequest();

                var api = CreateApi((client, config) => new UsersApi(client, config));

                var response = await api.CreateUserIdentityAsync(user_id, request);

                // Assert
                Assert.NotNull(response);
                _output.WriteLine($"Response received: {response?.GetType().Name}");
                _output.WriteLine($"Test completed successfully");
            }
            catch (Exception ex)
            {
                _output.WriteLine($"Error in CreateUserIdentity test: {ex.Message}");
                throw;
            }
        }


        [Fact]
        [Trait("TestMode", "Mock")]
        public async Task GetUserSessions_Mock_Test()
        {
            // Arrange
            if (UseRealApi)
            {
                _output.WriteLine("Skipping Mock test - using real API");
                return;
            }

            // Act & Assert
            try
            {
                var mockHandler = GetMockHandler();
                if (mockHandler == null)
                {
                    _output.WriteLine("Mock handler not available");
                    return;
                }

                var user_id = "test-user_id";
                var mockResponse = new GetUserSessionsResponse();
                mockHandler.AddResponse("GET", $"/api/v1/users/{user_id}/sessions", mockResponse);

                var api = CreateApi((client, config) => new UsersApi(client, config));

                // Act
                var response = await api.GetUserSessionsAsync(user_id);

                // Assert
                Assert.NotNull(response);
                _output.WriteLine($"Mock test successful");
            }
            catch (Exception ex)
            {
                _output.WriteLine($"Error in GetUserSessions test: {ex.Message}");
                throw;
            }
        }


        [Fact]
        [Trait("TestMode", "Real")]
        public async Task GetUserSessions_Real_Test()
        {
            // Arrange
            if (!UseRealApi)
            {
                _output.WriteLine("Skipping Real test - using mocks");
                return;
            }

            // Act & Assert
            try
            {
                // Using test resource from fixture: UserId
                var user_id = _fixture.UserId;

                var api = CreateApi((client, config) => new UsersApi(client, config));

                var response = await api.GetUserSessionsAsync(user_id);

                // Assert
                Assert.NotNull(response);
                _output.WriteLine($"Response received: {response?.GetType().Name}");
                _output.WriteLine($"Test completed successfully");
            }
            catch (Exception ex)
            {
                _output.WriteLine($"Error in GetUserSessions test: {ex.Message}");
                throw;
            }
        }


        [Fact]
        [Trait("TestMode", "Mock")]
        public async Task DeleteUserSessions_Mock_Test()
        {
            // Arrange
            if (UseRealApi)
            {
                _output.WriteLine("Skipping Mock test - using real API");
                return;
            }

            // Act & Assert
            try
            {
                var mockHandler = GetMockHandler();
                if (mockHandler == null)
                {
                    _output.WriteLine("Mock handler not available");
                    return;
                }

                var user_id = "test-user_id";
                var mockResponse = new SuccessResponse();
                mockHandler.AddResponse("DELETE", $"/api/v1/users/{user_id}/sessions", mockResponse);

                var api = CreateApi((client, config) => new UsersApi(client, config));

                // Act
                var response = await api.DeleteUserSessionsAsync(user_id);

                // Assert
                Assert.NotNull(response);
                _output.WriteLine($"Mock test successful");
            }
            catch (Exception ex)
            {
                _output.WriteLine($"Error in DeleteUserSessions test: {ex.Message}");
                throw;
            }
        }


        [Fact]
        [Trait("TestMode", "Real")]
        public async Task DeleteUserSessions_Real_Test()
        {
            // Arrange
            if (!UseRealApi)
            {
                _output.WriteLine("Skipping Real test - using mocks");
                return;
            }

            // Act & Assert
            try
            {
                // WARNING: Real API test - This operation requires existing user_id
                // This test may fail if the resource doesn't exist in your Kinde instance
                // Consider creating the resource first or using a test environment
                // Using test resource from fixture: UserId
                var user_id = _fixture.UserId;

                var api = CreateApi((client, config) => new UsersApi(client, config));

                var response = await api.DeleteUserSessionsAsync(user_id);

                // Assert
                Assert.NotNull(response);
                _output.WriteLine($"Response received: {response?.GetType().Name}");
                _output.WriteLine($"Test completed successfully");
            }
            catch (Exception ex)
            {
                _output.WriteLine($"Error in DeleteUserSessions test: {ex.Message}");
                throw;
            }
        }


        [Fact]
        [Trait("TestMode", "Mock")]
        public async Task GetUsersMFA_Mock_Test()
        {
            // Arrange
            if (UseRealApi)
            {
                _output.WriteLine("Skipping Mock test - using real API");
                return;
            }

            // Act & Assert
            try
            {
                var mockHandler = GetMockHandler();
                if (mockHandler == null)
                {
                    _output.WriteLine("Mock handler not available");
                    return;
                }

                var user_id = "test-user_id";
                var mockResponse = new GetUserMfaResponse();
                mockHandler.AddResponse("GET", $"/api/v1/users/{user_id}/mfa", mockResponse);

                var api = CreateApi((client, config) => new UsersApi(client, config));

                // Act
                var response = await api.GetUsersMFAAsync(user_id);

                // Assert
                Assert.NotNull(response);
                _output.WriteLine($"Mock test successful");
            }
            catch (Exception ex)
            {
                _output.WriteLine($"Error in GetUsersMFA test: {ex.Message}");
                throw;
            }
        }


        [Fact]
        [Trait("TestMode", "Real")]
        public async Task GetUsersMFA_Real_Test()
        {
            // Arrange
            if (!UseRealApi)
            {
                _output.WriteLine("Skipping Real test - using mocks");
                return;
            }

            // Act & Assert
            try
            {
                // Using test resource from fixture: UserId
                var user_id = _fixture.UserId;

                var api = CreateApi((client, config) => new UsersApi(client, config));

                var response = await api.GetUsersMFAAsync(user_id);

                // Assert
                Assert.NotNull(response);
                _output.WriteLine($"Response received: {response?.GetType().Name}");
                _output.WriteLine($"Test completed successfully");
            }
            catch (Exception ex)
            {
                _output.WriteLine($"Error in GetUsersMFA test: {ex.Message}");
                throw;
            }
        }


        [Fact]
        [Trait("TestMode", "Mock")]
        public async Task ResetUsersMFAAll_Mock_Test()
        {
            // Arrange
            if (UseRealApi)
            {
                _output.WriteLine("Skipping Mock test - using real API");
                return;
            }

            // Act & Assert
            try
            {
                var mockHandler = GetMockHandler();
                if (mockHandler == null)
                {
                    _output.WriteLine("Mock handler not available");
                    return;
                }

                var user_id = "test-user_id";
                var mockResponse = new SuccessResponse();
                mockHandler.AddResponse("DELETE", $"/api/v1/users/{user_id}/mfa", mockResponse);

                var api = CreateApi((client, config) => new UsersApi(client, config));

                // Act
                var response = await api.ResetUsersMFAAllAsync(user_id);

                // Assert
                Assert.NotNull(response);
                _output.WriteLine($"Mock test successful");
            }
            catch (Exception ex)
            {
                _output.WriteLine($"Error in ResetUsersMFAAll test: {ex.Message}");
                throw;
            }
        }


        [Fact]
        [Trait("TestMode", "Real")]
        public async Task ResetUsersMFAAll_Real_Test()
        {
            // Arrange
            if (!UseRealApi)
            {
                _output.WriteLine("Skipping Real test - using mocks");
                return;
            }

            // Act & Assert
            try
            {
                // WARNING: Real API test - This operation requires existing user_id
                // This test may fail if the resource doesn't exist in your Kinde instance
                // Consider creating the resource first or using a test environment
                // Using test resource from fixture: UserId
                var user_id = _fixture.UserId;

                var api = CreateApi((client, config) => new UsersApi(client, config));

                var response = await api.ResetUsersMFAAllAsync(user_id);

                // Assert
                Assert.NotNull(response);
                _output.WriteLine($"Response received: {response?.GetType().Name}");
                _output.WriteLine($"Test completed successfully");
            }
            catch (Exception ex)
            {
                _output.WriteLine($"Error in ResetUsersMFAAll test: {ex.Message}");
                throw;
            }
        }


        [Fact]
        [Trait("TestMode", "Mock")]
        public async Task ResetUsersMFA_Mock_Test()
        {
            // Arrange
            if (UseRealApi)
            {
                _output.WriteLine("Skipping Mock test - using real API");
                return;
            }

            // Act & Assert
            try
            {
                var mockHandler = GetMockHandler();
                if (mockHandler == null)
                {
                    _output.WriteLine("Mock handler not available");
                    return;
                }

                var user_id = "test-user_id";
                var factor_id = "test-factor_id";
                var mockResponse = new SuccessResponse();
                mockHandler.AddResponse("DELETE", $"/api/v1/users/{user_id}/mfa/{factor_id}", mockResponse);

                var api = CreateApi((client, config) => new UsersApi(client, config));

                // Act
                var response = await api.ResetUsersMFAAsync(user_id, factor_id);

                // Assert
                Assert.NotNull(response);
                _output.WriteLine($"Mock test successful");
            }
            catch (Exception ex)
            {
                _output.WriteLine($"Error in ResetUsersMFA test: {ex.Message}");
                throw;
            }
        }


        [Fact]
        [Trait("TestMode", "Real")]
        public async Task ResetUsersMFA_Real_Test()
        {
            // Arrange
            if (!UseRealApi)
            {
                _output.WriteLine("Skipping Real test - using mocks");
                return;
            }

            // Act & Assert
            try
            {
                // WARNING: Real API test - This operation requires existing user_id
                // This test may fail if the resource doesn't exist in your Kinde instance
                // Consider creating the resource first or using a test environment
                // Using test resource from fixture: UserId
                var user_id = _fixture.UserId;
                // WARNING: Using placeholder factor_id - test will likely fail without real resource ID
                var factor_id = "test-factor_id";

                var api = CreateApi((client, config) => new UsersApi(client, config));

                var response = await api.ResetUsersMFAAsync(user_id, factor_id);

                // Assert
                Assert.NotNull(response);
                _output.WriteLine($"Response received: {response?.GetType().Name}");
                _output.WriteLine($"Test completed successfully");
            }
            catch (Exception ex)
            {
                _output.WriteLine($"Error in ResetUsersMFA test: {ex.Message}");
                throw;
            }
        }

    }
}
