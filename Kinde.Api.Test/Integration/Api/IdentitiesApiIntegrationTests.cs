using Kinde.Api.Api;
using Kinde.Api.Client;
using Kinde.Api.Model;
using Kinde.Api.Test.Integration;
using Xunit;

namespace Kinde.Api.Test.Integration.Api
{
    /// <summary>
    /// Integration tests for IdentitiesApi using the OpenAPI Mock Server.
    /// These tests validate serialization and deserialization of API requests and responses.
    /// </summary>
    public class IdentitiesApiIntegrationTests : IntegrationTestBase
    {
        public IdentitiesApiIntegrationTests(MockServerFixture fixture) : base(fixture)
        {
        }

        [Fact]
        public async Task GetIdentity_DeserializesCorrectly()
        {
            // Arrange
            var identityId = "identity_123";
            var expectedResponse = new
            {
                id = identityId,
                type = "email",
                name = "test@example.com",
                email = "test@example.com",
                is_confirmed = true,
                is_primary = true
            };

            MockServer.SetMockResponse($"/api/v1/identities/{identityId}", "GET", expectedResponse);
            var api = CreateIdentitiesApi();

            // Act
            var result = await api.GetIdentityAsync(identityId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(identityId, result.Id);
        }

        [Fact]
        public async Task UpdateIdentity_SerializesAndDeserializesCorrectly()
        {
            // Arrange
            var identityId = "identity_123";
            var request = new UpdateIdentityRequest
            {
                // Add properties as needed based on the model
            };

            var expectedResponse = new
            {
                code = "SUCCESS",
                message = "Identity updated successfully"
            };

            MockServer.SetMockResponse($"/api/v1/identities/{identityId}", "PATCH", expectedResponse);
            var api = CreateIdentitiesApi();

            // Act
            var result = await api.UpdateIdentityAsync(identityId, request);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("SUCCESS", result.Code);
        }
    }
}

