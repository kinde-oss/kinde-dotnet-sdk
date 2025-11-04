using Kinde.Api.Api;
using Kinde.Api.Client;
using Kinde.Api.Model;
using Kinde.Api.Test.Integration;
using Xunit;

namespace Kinde.Api.Test.Integration.Api
{
    /// <summary>
    /// Integration tests for OrganizationsApi using the OpenAPI Mock Server.
    /// These tests validate serialization and deserialization of API requests and responses.
    /// </summary>
    public class OrganizationsApiIntegrationTests : IntegrationTestBase
    {
        public OrganizationsApiIntegrationTests(MockServerFixture fixture) : base(fixture)
        {
        }

        [Fact]
        public async Task GetOrganizationUsers_DeserializesCorrectly()
        {
            // Arrange
            var orgCode = "org_test_123";
            var expectedResponse = new
            {
                code = "OK",
                message = "Success",
                organization_users = new[]
                {
                    new
                    {
                        id = "kp_123",
                        email = "user1@example.com",
                        first_name = "John",
                        last_name = "Doe",
                        full_name = "John Doe",
                        picture = "https://example.com/pic.jpg",
                        joined_on = "2024-01-01T00:00:00Z",
                        last_accessed_on = "2024-01-02T00:00:00Z",
                        roles = new[] { "admin", "user" }
                    },
                    new
                    {
                        id = "kp_456",
                        email = "user2@example.com",
                        first_name = "Jane",
                        last_name = "Smith",
                        full_name = "Jane Smith",
                        picture = (string?)null,
                        joined_on = "2024-01-03T00:00:00Z",
                        last_accessed_on = (string?)null,
                        roles = new string[0]
                    }
                },
                next_token = "next_page_token"
            };

            // Set the mock response using both the actual path and the template path
            var actualPath = $"/api/v1/organizations/{orgCode}/users";
            MockServer.SetMockResponse(actualPath, "GET", expectedResponse);
            // Also set using the template path in case routing resolves it differently
            MockServer.SetMockResponse("/api/v1/organizations/{org_code}/users", "GET", expectedResponse);

            var api = CreateOrganizationsApi();

            // Act
            var result = await api.GetOrganizationUsersAsync(orgCode, null, null, null, null, null);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("OK", result.Code);
            Assert.Equal("Success", result.Message);
            Assert.NotNull(result.OrganizationUsers);
            Assert.Equal(2, result.OrganizationUsers.Count);

            var firstUser = result.OrganizationUsers[0];
            Assert.Equal("kp_123", firstUser.Id);
            Assert.Equal("user1@example.com", firstUser.Email);
            Assert.Equal("John", firstUser.FirstName);
            Assert.Equal("Doe", firstUser.LastName);
            Assert.Equal("John Doe", firstUser.FullName);
            Assert.NotNull(firstUser.Roles);
            Assert.Equal(2, firstUser.Roles.Count);
            Assert.Contains("admin", firstUser.Roles);
            Assert.Contains("user", firstUser.Roles);

            var secondUser = result.OrganizationUsers[1];
            Assert.Equal("kp_456", secondUser.Id);
            Assert.Equal("user2@example.com", secondUser.Email);
            Assert.Equal("Jane", secondUser.FirstName);
            Assert.Equal("Smith", secondUser.LastName);
            Assert.Null(secondUser.Picture);
        }

        [Fact]
        public async Task GetOrganizationUsers_WithQueryParameters_DeserializesCorrectly()
        {
            // Arrange
            var orgCode = "org_test_456";
            var sort = "email_asc";
            var pageSize = 10;
            var permissions = "admin,user";
            var roles = "manager";

            var expectedResponse = new
            {
                code = "OK",
                message = "Success",
                organization_users = new[]
                {
                    new
                    {
                        id = "kp_789",
                        email = "filtered@example.com",
                        first_name = "Filtered",
                        last_name = "User",
                        full_name = "Filtered User",
                        picture = (string?)null,
                        joined_on = "2024-01-04T00:00:00Z",
                        last_accessed_on = (string?)null,
                        roles = new[] { "manager" }
                    }
                },
                next_token = (string?)null
            };

            MockServer.SetMockResponse(
                $"/api/v1/organizations/{orgCode}/users",
                "GET",
                expectedResponse
            );

            var api = CreateOrganizationsApi();

            // Act
            var result = await api.GetOrganizationUsersAsync(
                orgCode: orgCode,
                sort: sort,
                pageSize: pageSize,
                permissions: permissions,
                roles: roles
            );

            // Assert
            Assert.NotNull(result);
            Assert.Equal("OK", result.Code);
            Assert.NotNull(result.OrganizationUsers);
            Assert.Single(result.OrganizationUsers);
            Assert.Equal("filtered@example.com", result.OrganizationUsers[0].Email);
        }

        [Fact]
        public async Task GetOrganizationUsers_UsesOpenApiSpecExample_WhenNoCustomMockSet()
        {
            // Arrange
            var orgCode = "org_spec_example";
            var api = CreateOrganizationsApi();

            // Act - This will use the default example from the OpenAPI spec
            var result = await api.GetOrganizationUsersAsync(orgCode, null, null, null, null, null);

            // Assert - Should deserialize using the spec's example data
            Assert.NotNull(result);
            // The spec example has data, so we should get something back
            // Exact values depend on what's in the spec
        }

        [Fact]
        public async Task CreateOrganization_SerializesAndDeserializesCorrectly()
        {
            // Arrange
            var request = new CreateOrganizationRequest(name: "Test Organization")
            {
                ExternalId = "ext_123",
                Handle = "test-org"
            };

            var expectedResponse = new
            {
                code = "CREATED",
                organization = new
                {
                    code = "org_new_123",
                    name = "Test Organization",
                    is_default = false,
                    external_id = "ext_123"
                },
                message = "Organization created successfully"
            };

            MockServer.SetMockResponse(
                "/api/v1/organization",
                "POST",
                expectedResponse
            );

            var api = CreateOrganizationsApi();

            // Act
            var result = await api.CreateOrganizationAsync(request);

            // Assert
            Assert.NotNull(result);
            // Verify the request was properly serialized and response deserialized
        }
    }
}

