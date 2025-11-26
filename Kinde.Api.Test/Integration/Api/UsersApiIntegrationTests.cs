using System;
using System.Threading.Tasks;
using Kinde.Api.Api;
using Kinde.Api.Model;
using Xunit;
using Xunit.Abstractions;

namespace Kinde.Api.Test.Integration.Api
{
    /// <summary>
    /// Integration tests for UsersApi with both mock and real API support
    /// </summary>
    public class UsersApiIntegrationTests : BaseIntegrationTest
    {
        private readonly ITestOutputHelper _output;

        public UsersApiIntegrationTests(ITestOutputHelper output) : base()
        {
            _output = output;
        }

        [Fact]
        [Trait("TestMode", "Mock")]
        public async Task GetUsers_Mock_ReturnsSuccess()
        {
            // Arrange
            if (UseRealApi)
            {
                _output.WriteLine("Skipping mock test - using real API");
                return;
            }

            var mockHandler = GetMockHandler();
            if (mockHandler == null)
            {
                _output.WriteLine("Mock handler not available");
                return;
            }

            // Setup mock response
            var mockResponse = new Kinde.Api.Model.UsersResponse
            {
                Code = "200",
                Message = "Success",
                Users = new System.Collections.Generic.List<Kinde.Api.Model.UsersResponseUsersInner>
                {
                    new Kinde.Api.Model.UsersResponseUsersInner
                    {
                        Id = "test-user-1",
                        Email = "test@example.com"
                    }
                }
            };

            mockHandler.AddResponse("GET", "/api/v1/users", mockResponse);

            var usersApi = CreateApi((client, config) => new UsersApi(client, config));

            // Act
            var response = await usersApi.GetUsersAsync();

            // Assert
            Assert.NotNull(response);
            Assert.NotNull(response.Users);
            Assert.Single(response.Users);
            Assert.Equal("test-user-1", response.Users[0].Id);
            _output.WriteLine($"Mock test successful - retrieved {response.Users.Count} users");
        }

        [Fact]
        [Trait("TestMode", "Real")]
        public async Task GetUsers_RealApi_ReturnsUsers()
        {
            // Arrange
            if (!UseRealApi)
            {
                _output.WriteLine("Skipping real API test - using mocks");
                return;
            }

            var usersApi = CreateApi((client, config) => new UsersApi(client, config));

            // Act
            try
            {
                var response = await usersApi.GetUsersAsync();
                
                // Assert
                Assert.NotNull(response);
                _output.WriteLine($"Retrieved {response?.Users?.Count ?? 0} users");
            }
            catch (Exception ex)
            {
                _output.WriteLine($"Error calling GetUsers: {ex.Message}");
                throw;
            }
        }

        [Fact]
        [Trait("TestMode", "Real")]
        public async Task CreateUser_RealApi_CreatesAndDeserializes()
        {
            if (!UseRealApi)
            {
                _output.WriteLine("Skipping real API test - using mocks");
                return;
            }

            var usersApi = CreateApi((client, config) => new UsersApi(client, config));

            // Create a test user
            var createRequest = new CreateUserRequest
            {
                Profile = new CreateUserRequestProfile
                {
                    GivenName = "Test",
                    FamilyName = $"User_{Guid.NewGuid():N}"
                },
                Identities = new System.Collections.Generic.List<CreateUserRequestIdentitiesInner>
                {
                    new CreateUserRequestIdentitiesInner
                    {
                        Type = CreateUserRequestIdentitiesInner.TypeEnum.Email,
                        Details = new CreateUserRequestIdentitiesInnerDetails
                        {
                            Email = $"test_{Guid.NewGuid():N}@example.com"
                        }
                    }
                }
            };

            try
            {
                // Act - Create user
                var createResponse = await usersApi.CreateUserAsync(createRequest);
                
                // Assert - Verify serialization worked
                Assert.NotNull(createResponse);
                Assert.NotNull(createResponse.Id);
                _output.WriteLine($"Created user: {createResponse.Id}");

                // Act - Get user to verify deserialization
                var getUserResponse = await usersApi.GetUserDataAsync(createResponse.Id);
                
                // Assert - Verify deserialization worked
                Assert.NotNull(getUserResponse);
                Assert.NotNull(getUserResponse.Id);
                Assert.Equal(createResponse.Id, getUserResponse.Id);
                _output.WriteLine($"Retrieved user: {getUserResponse.Id}");
            }
            catch (Exception ex)
            {
                _output.WriteLine($"Error in CreateUser test: {ex.Message}");
                throw;
            }
        }
    }
}

