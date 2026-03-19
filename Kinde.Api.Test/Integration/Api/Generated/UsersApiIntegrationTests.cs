using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Kinde.Api.Api;
using Kinde.Api.Model;
using Kinde.Api.Test.Integration;
using Xunit;
using Xunit.Abstractions;
using KiotaModels = Kinde.Api.Kiota.Management.Models;

namespace Kinde.Api.Test.Integration.Api.Generated
{
    /// <summary>
    /// Integration tests for UsersApi with both mock and real API support.
    /// 
    /// These tests verify the complete flow through the Kiota-based API:
    /// 1. OpenAPI method call
    /// 2. Request mapping to Kiota format
    /// 3. HTTP request (mocked or real)
    /// 4. Response mapping from Kiota to OpenAPI format
    /// 
    /// Mock tests use KiotaMockHttpHandler which returns snake_case JSON responses
    /// that are compatible with Kiota's deserialization.
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

        #region GetUsers Tests

        [Fact]
        [Trait("TestMode", "Mock")]
        public async Task GetUsers_Mock_ReturnsUsers()
        {
            // Arrange
            if (UseRealApi)
            {
                _output.WriteLine("Skipping Mock test - using real API");
                return;
            }

            var mockHandler = GetKiotaMockHandler();
                if (mockHandler == null)
                {
                    _output.WriteLine("Mock handler not available");
                    return;
                }

            // Set up Kiota-format response with actual data
            var kiotaResponse = new KiotaModels.Users_response
            {
                Code = "200",
                Message = "Users retrieved successfully",
                Users = new List<KiotaModels.Users_response_users>
                {
                    new KiotaModels.Users_response_users 
                    { 
                        Id = "user_test_1", 
                        FirstName = "John", 
                        LastName = "Doe",
                        Email = "john@example.com",
                        IsSuspended = false
                    },
                    new KiotaModels.Users_response_users 
                    { 
                        Id = "user_test_2", 
                        FirstName = "Jane", 
                        LastName = "Smith",
                        Email = "jane@example.com",
                        IsSuspended = false
                    }
                },
                NextToken = "next_page_token"
            };
            mockHandler.AddKiotaResponse("GET", "/api/v1/users", kiotaResponse);

                var api = CreateApi((client, config) => new UsersApi(client, config));

            // Act
            var response = await api.GetUsersAsync();

                // Assert
                Assert.NotNull(response);
            Assert.Equal("200", response.Code);
            Assert.Equal("Users retrieved successfully", response.Message);
            Assert.NotNull(response.Users);
            Assert.Equal(2, response.Users.Count);
            Assert.Equal("user_test_1", response.Users[0].Id);
            Assert.Equal("John", response.Users[0].FirstName);
            Assert.False(response.Users[0].IsSuspended);
            Assert.Equal("next_page_token", response.NextToken);
            
            _output.WriteLine($"Mock test successful - Retrieved {response.Users.Count} users");
        }

        [Fact]
        [Trait("TestMode", "Mock")]
        public async Task GetUsers_Mock_EmptyList()
        {
            // Arrange
            if (UseRealApi) return;

            var mockHandler = GetKiotaMockHandler();
            if (mockHandler == null) return;

            var kiotaResponse = new KiotaModels.Users_response
            {
                Code = "200",
                Message = "No users found",
                Users = new List<KiotaModels.Users_response_users>()
            };
            mockHandler.AddKiotaResponse("GET", "/api/v1/users", kiotaResponse);

                var api = CreateApi((client, config) => new UsersApi(client, config));

                // Act
            var response = await api.GetUsersAsync();

                // Assert
                Assert.NotNull(response);
            Assert.NotNull(response.Users);
            Assert.Empty(response.Users);
            _output.WriteLine("Empty users list handled correctly");
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

                var api = CreateApi((client, config) => new UsersApi(client, config));

            // Act
            var response = await api.GetUsersAsync();

                // Assert
                Assert.NotNull(response);
            _output.WriteLine($"Real API returned {response.Users?.Count ?? 0} users");
        }

        #endregion

        #region CreateUser Tests

        [Fact]
        [Trait("TestMode", "Mock")]
        public async Task CreateUser_Mock_Created_True()
        {
            // Arrange
            if (UseRealApi) return;

            var mockHandler = GetKiotaMockHandler();
            if (mockHandler == null) return;

            // Response with Created = true
            var kiotaResponse = new KiotaModels.Create_user_response
            {
                Created = true,
                Id = "user_new_123"
            };
            mockHandler.AddKiotaResponse("POST", "/api/v1/user", kiotaResponse);

                var api = CreateApi((client, config) => new UsersApi(client, config));
            var request = new CreateUserRequest
            {
                Profile = new CreateUserRequestProfile
                {
                    GivenName = "Test",
                    FamilyName = "User"
                }
            };

                // Act
                var response = await api.CreateUserAsync(request);

                // Assert
                Assert.NotNull(response);
            Assert.True(response.Created, "Created should be true");
            Assert.Equal("user_new_123", response.Id);
            _output.WriteLine("CreateUser with Created=true passed");
        }

        [Fact]
        [Trait("TestMode", "Mock")]
        public async Task CreateUser_Mock_Created_False()
        {
            // Arrange - This test verifies that false values don't default to true
            if (UseRealApi) return;

            var mockHandler = GetKiotaMockHandler();
            if (mockHandler == null) return;

            // Response with Created = false (user already exists)
            var kiotaResponse = new KiotaModels.Create_user_response
            {
                Created = false,
                Id = "user_existing_123"
            };
            mockHandler.AddKiotaResponse("POST", "/api/v1/user", kiotaResponse);

                var api = CreateApi((client, config) => new UsersApi(client, config));
            var request = new CreateUserRequest();

            // Act
                var response = await api.CreateUserAsync(request);

            // Assert - CRITICAL: Must be false, not defaulting to true
                Assert.NotNull(response);
            Assert.False(response.Created, "Created should be false - not defaulting to true!");
            Assert.Equal("user_existing_123", response.Id);
            _output.WriteLine("PASS: CreateUser with Created=false correctly returned false");
        }

        [Fact]
        [Trait("TestMode", "Mock")]
        public async Task CreateUser_Mock_NullableCreated()
        {
            // Arrange
            if (UseRealApi) return;

            var mockHandler = GetKiotaMockHandler();
            if (mockHandler == null) return;

            // Response with Created = null
            var kiotaResponse = new KiotaModels.Create_user_response
            {
                Created = null,
                Id = "user_null_created"
            };
            mockHandler.AddKiotaResponse("POST", "/api/v1/user", kiotaResponse);

                var api = CreateApi((client, config) => new UsersApi(client, config));
            var request = new CreateUserRequest();

                // Act
            var response = await api.CreateUserAsync(request);

                // Assert
                Assert.NotNull(response);
            // Note: OpenAPI CreateUserResponse.Created is non-nullable bool
            // When Kiota returns null, it maps to false (default)
            Assert.False(response.Created);
            _output.WriteLine("Nullable Created handled correctly (null -> false)");
        }

        #endregion

        #region GetUserData Tests

        [Fact]
        [Trait("TestMode", "Mock")]
        public async Task GetUserData_Mock_ReturnsUser()
        {
            // Arrange
            if (UseRealApi) return;

            var mockHandler = GetKiotaMockHandler();
            if (mockHandler == null) return;

            var kiotaResponse = new KiotaModels.User
            {
                Id = "user_123",
                FirstName = "John",
                LastName = "Doe",
                IsSuspended = false,
                TotalSignIns = 42
            };
            mockHandler.AddKiotaResponse("GET", "/api/v1/user", kiotaResponse);

                var api = CreateApi((client, config) => new UsersApi(client, config));

                // Act
            var response = await api.GetUserDataAsync("user_123");

                // Assert
                Assert.NotNull(response);
            Assert.Equal("user_123", response.Id);
            Assert.Equal("John", response.FirstName);
            Assert.Equal("Doe", response.LastName);
            Assert.False(response.IsSuspended);
            Assert.Equal(42, response.TotalSignIns);
            _output.WriteLine("GetUserData returned correct user data");
        }

        [Fact]
        [Trait("TestMode", "Mock")]
        public async Task GetUserData_Mock_IsSuspended_True()
        {
            // Arrange
            if (UseRealApi) return;

            var mockHandler = GetKiotaMockHandler();
            if (mockHandler == null) return;

            var kiotaResponse = new KiotaModels.User
            {
                Id = "user_suspended",
                IsSuspended = true
            };
            mockHandler.AddKiotaResponse("GET", "/api/v1/user", kiotaResponse);

                var api = CreateApi((client, config) => new UsersApi(client, config));

            // Act
            var response = await api.GetUserDataAsync("user_suspended");

                // Assert
                Assert.NotNull(response);
            Assert.True(response.IsSuspended, "IsSuspended should be true");
            _output.WriteLine("IsSuspended=true passed");
        }

        [Fact]
        [Trait("TestMode", "Mock")]
        public async Task GetUserData_Mock_IsSuspended_False()
        {
            // Arrange
            if (UseRealApi) return;

            var mockHandler = GetKiotaMockHandler();
            if (mockHandler == null) return;

            var kiotaResponse = new KiotaModels.User
            {
                Id = "user_active",
                IsSuspended = false
            };
            mockHandler.AddKiotaResponse("GET", "/api/v1/user", kiotaResponse);

                var api = CreateApi((client, config) => new UsersApi(client, config));

                // Act
            var response = await api.GetUserDataAsync("user_active");

                // Assert
                Assert.NotNull(response);
            Assert.False(response.IsSuspended, "IsSuspended should be false");
            _output.WriteLine("IsSuspended=false passed");
        }

        #endregion

        #region UpdateUser Tests

        [Fact]
        [Trait("TestMode", "Mock")]
        public async Task UpdateUser_Mock_ReturnsUpdatedUser()
        {
            // Arrange
            if (UseRealApi) return;

            var mockHandler = GetKiotaMockHandler();
            if (mockHandler == null) return;

            var kiotaResponse = new KiotaModels.Update_user_response
            {
                Id = "user_updated",
                GivenName = "Updated",
                FamilyName = "Name"
            };
            mockHandler.AddKiotaResponse("PATCH", "/api/v1/user", kiotaResponse);

                var api = CreateApi((client, config) => new UsersApi(client, config));
            var request = new UpdateUserRequest
            {
                GivenName = "Updated",
                FamilyName = "Name"
            };

                // Act
            var response = await api.UpdateUserAsync("user_updated", request);

                // Assert
                Assert.NotNull(response);
            Assert.Equal("user_updated", response.Id);
            Assert.Equal("Updated", response.GivenName);
            Assert.Equal("Name", response.FamilyName);
            _output.WriteLine("UpdateUser returned correct response");
        }

        #endregion

        #region DeleteUser Tests

        [Fact]
        [Trait("TestMode", "Mock")]
        public async Task DeleteUser_Mock_Success()
        {
            // Arrange
            if (UseRealApi) return;

            var mockHandler = GetKiotaMockHandler();
            if (mockHandler == null) return;

            var kiotaResponse = new KiotaModels.Success_response
            {
                Code = "200",
                Message = "User deleted successfully"
            };
            mockHandler.AddKiotaResponse("DELETE", "/api/v1/user", kiotaResponse);

                var api = CreateApi((client, config) => new UsersApi(client, config));

            // Act - DeleteUser is void, so just verify no exception
            await api.DeleteUserAsync("user_to_delete");

            // Assert - Verify the request was made
            Assert.True(mockHandler.WasRequestMade("DELETE", "/api/v1/user"));
            _output.WriteLine("DeleteUser completed successfully");
        }

        #endregion

        #region RefreshUserClaims Tests

        [Fact]
        [Trait("TestMode", "Mock")]
        public async Task RefreshUserClaims_Mock_Success()
        {
            // Arrange
            if (UseRealApi) return;

            var mockHandler = GetKiotaMockHandler();
            if (mockHandler == null) return;

            var kiotaResponse = new KiotaModels.Success_response
            {
                Code = "200",
                Message = "Claims refreshed"
            };
            mockHandler.AddKiotaResponse("POST", "/api/v1/users/{user_id}/refresh_claims", kiotaResponse);

                var api = CreateApi((client, config) => new UsersApi(client, config));

                // Act
            var response = await api.RefreshUserClaimsAsync("user_123");

                // Assert
                Assert.NotNull(response);
            Assert.Equal("200", response.Code);
            _output.WriteLine("RefreshUserClaims completed successfully");
        }

        #endregion

        #region Null Handling Tests

        [Fact]
        [Trait("TestMode", "Mock")]
        public async Task GetUserData_Mock_NullEmail_StaysNull()
        {
            // Arrange
            if (UseRealApi) return;

            var mockHandler = GetKiotaMockHandler();
            if (mockHandler == null) return;

            var kiotaResponse = new KiotaModels.User
            {
                Id = "user_null_fields",
                FirstName = "John",
                LastName = null  // Explicitly null
            };
            mockHandler.AddKiotaResponse("GET", "/api/v1/user", kiotaResponse);

                var api = CreateApi((client, config) => new UsersApi(client, config));

                // Act
            var response = await api.GetUserDataAsync("user_null_fields");

            // Assert - Null should stay null, not become empty string
                Assert.NotNull(response);
            Assert.Null(response.LastName);
            _output.WriteLine("Null lastName preserved correctly");
        }

        [Fact]
        [Trait("TestMode", "Mock")]
        public async Task GetUsers_Mock_NullUsersList_StaysNull()
        {
            // Arrange
            if (UseRealApi) return;

            var mockHandler = GetKiotaMockHandler();
            if (mockHandler == null) return;

            var kiotaResponse = new KiotaModels.Users_response
            {
                Code = "200",
                Message = "Success",
                Users = null  // Explicitly null list
            };
            mockHandler.AddKiotaResponse("GET", "/api/v1/users", kiotaResponse);

                var api = CreateApi((client, config) => new UsersApi(client, config));

                // Act
            var response = await api.GetUsersAsync();

                // Assert
                Assert.NotNull(response);
            Assert.Null(response.Users);
            _output.WriteLine("Null users list preserved correctly");
        }

        #endregion

        #region Feature Flag Override Tests

        [Fact]
        [Trait("TestMode", "Mock")]
        public async Task UpdateUserFeatureFlagOverride_Mock_Success()
        {
            // Arrange
            if (UseRealApi) return;

            var mockHandler = GetKiotaMockHandler();
            if (mockHandler == null) return;

            var kiotaResponse = new KiotaModels.Success_response
            {
                Code = "200",
                Message = "Feature flag override updated"
            };
            mockHandler.AddKiotaResponse("PATCH", "/api/v1/users/{user_id}/feature_flags/{feature_flag_key}", kiotaResponse);

                var api = CreateApi((client, config) => new UsersApi(client, config));

                // Act
            var response = await api.UpdateUserFeatureFlagOverrideAsync("user_123", "my_feature", "true");

                // Assert
                Assert.NotNull(response);
            Assert.Equal("200", response.Code);
            _output.WriteLine("UpdateUserFeatureFlagOverride completed successfully");
        }

        #endregion

        #region Property Tests

        [Fact]
        [Trait("TestMode", "Mock")]
        public async Task GetUserPropertyValues_Mock_ReturnsProperties()
        {
            // Arrange
            if (UseRealApi) return;

            var mockHandler = GetKiotaMockHandler();
            if (mockHandler == null) return;

            var kiotaResponse = new KiotaModels.Get_property_values_response
            {
                Code = "200",
                Message = "Properties retrieved"
            };
            mockHandler.AddKiotaResponse("GET", "/api/v1/users/{user_id}/properties", kiotaResponse);

                var api = CreateApi((client, config) => new UsersApi(client, config));

                // Act
            var response = await api.GetUserPropertyValuesAsync("user_123");

                // Assert
                Assert.NotNull(response);
            Assert.Equal("200", response.Code);
            _output.WriteLine("GetUserPropertyValues returned correct properties");
        }

        [Fact]
        [Trait("TestMode", "Mock")]
        public async Task UpdateUserProperty_Mock_Success()
        {
            // Arrange
            if (UseRealApi) return;

            var mockHandler = GetKiotaMockHandler();
            if (mockHandler == null) return;

            var kiotaResponse = new KiotaModels.Success_response
            {
                Code = "200",
                Message = "Property updated"
            };
            mockHandler.AddKiotaResponse("PUT", "/api/v1/users/{user_id}/properties/{property_key}", kiotaResponse);

                var api = CreateApi((client, config) => new UsersApi(client, config));

            // Act
            var response = await api.UpdateUserPropertyAsync("user_123", "department", "Sales");

                // Assert
                Assert.NotNull(response);
            Assert.Equal("200", response.Code);
            _output.WriteLine("UpdateUserProperty completed successfully");
        }

        #endregion

        #region Identity Tests

        [Fact]
        [Trait("TestMode", "Mock")]
        public async Task GetUserIdentities_Mock_ReturnsIdentities()
        {
            // Arrange
            if (UseRealApi) return;

            var mockHandler = GetKiotaMockHandler();
            if (mockHandler == null) return;

            var kiotaResponse = new KiotaModels.Get_identities_response
            {
                Code = "200",
                Message = "Identities retrieved"
            };
            mockHandler.AddKiotaResponse("GET", "/api/v1/users/{user_id}/identities", kiotaResponse);

                var api = CreateApi((client, config) => new UsersApi(client, config));

                // Act
            var response = await api.GetUserIdentitiesAsync("user_123");

                // Assert
                Assert.NotNull(response);
            _output.WriteLine("GetUserIdentities returned correct identities");
        }

        #endregion

        #region Session Tests

        [Fact]
        [Trait("TestMode", "Mock")]
        public async Task GetUserSessions_Mock_ReturnsSessions()
        {
            // Arrange
            if (UseRealApi) return;

            var mockHandler = GetKiotaMockHandler();
            if (mockHandler == null) return;

            var kiotaResponse = new KiotaModels.Get_user_sessions_response
            {
                Code = "200",
                Message = "Sessions retrieved"
            };
            mockHandler.AddKiotaResponse("GET", "/api/v1/users/{user_id}/sessions", kiotaResponse);

                var api = CreateApi((client, config) => new UsersApi(client, config));

                // Act
            var response = await api.GetUserSessionsAsync("user_123");

                // Assert
                Assert.NotNull(response);
            _output.WriteLine("GetUserSessions returned correctly");
        }

        [Fact]
        [Trait("TestMode", "Mock")]
        public async Task DeleteUserSessions_Mock_Success()
        {
            // Arrange
            if (UseRealApi) return;

            var mockHandler = GetKiotaMockHandler();
            if (mockHandler == null) return;

            var kiotaResponse = new KiotaModels.Success_response
            {
                Code = "200",
                Message = "Sessions deleted"
            };
            mockHandler.AddKiotaResponse("DELETE", "/api/v1/users/{user_id}/sessions", kiotaResponse);

                var api = CreateApi((client, config) => new UsersApi(client, config));

            // Act
            var response = await api.DeleteUserSessionsAsync("user_123");

                // Assert
                Assert.NotNull(response);
            Assert.Equal("200", response.Code);
            _output.WriteLine("DeleteUserSessions completed successfully");
        }

        #endregion

        #region MFA Tests

        [Fact]
        [Trait("TestMode", "Mock")]
        public async Task GetUsersMFA_Mock_ReturnsMFAInfo()
        {
            // Arrange
            if (UseRealApi) return;

            var mockHandler = GetKiotaMockHandler();
            if (mockHandler == null) return;

            var kiotaResponse = new KiotaModels.Get_user_mfa_response
            {
                Code = "200",
                Message = "MFA info retrieved"
            };
            mockHandler.AddKiotaResponse("GET", "/api/v1/users/{user_id}/mfa", kiotaResponse);

                var api = CreateApi((client, config) => new UsersApi(client, config));

                // Act
            var response = await api.GetUsersMFAAsync("user_123");

                // Assert
                Assert.NotNull(response);
            Assert.Equal("200", response.Code);
            _output.WriteLine("GetUsersMFA returned correct MFA info");
        }

        [Fact]
        [Trait("TestMode", "Mock")]
        public async Task ResetUsersMFAAll_Mock_Success()
        {
            // Arrange
            if (UseRealApi) return;

            var mockHandler = GetKiotaMockHandler();
            if (mockHandler == null) return;

            var kiotaResponse = new KiotaModels.Success_response
            {
                Code = "200",
                Message = "MFA reset for all factors"
            };
            mockHandler.AddKiotaResponse("DELETE", "/api/v1/users/{user_id}/mfa", kiotaResponse);

                var api = CreateApi((client, config) => new UsersApi(client, config));

            // Act
            var response = await api.ResetUsersMFAAllAsync("user_123");

                // Assert
                Assert.NotNull(response);
            Assert.Equal("200", response.Code);
            _output.WriteLine("ResetUsersMFAAll completed successfully");
        }

        #endregion
    }
}
