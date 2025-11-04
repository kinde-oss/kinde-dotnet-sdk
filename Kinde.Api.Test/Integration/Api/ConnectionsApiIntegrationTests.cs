using Kinde.Api.Api;
using Kinde.Api.Client;
using Kinde.Api.Model;
using Kinde.Api.Test.Integration;
using Xunit;

namespace Kinde.Api.Test.Integration.Api
{
    /// <summary>
    /// Integration tests for ConnectionsApi using the OpenAPI Mock Server.
    /// These tests validate serialization and deserialization of API requests and responses.
    /// </summary>
    public class ConnectionsApiIntegrationTests : IntegrationTestBase
    {
        public ConnectionsApiIntegrationTests(MockServerFixture fixture) : base(fixture)
        {
        }

        [Fact]
        public async Task GetConnections_DeserializesCorrectly()
        {
            // Arrange
            var expectedResponse = new
            {
                connections = new[]
                {
                    new
                    {
                        id = "conn_123",
                        name = "Google Connection",
                        type = "saml",
                        domain = "example.com"
                    }
                },
                next_token = (string?)null
            };

            MockServer.SetMockResponse("/api/v1/connections", "GET", expectedResponse);
            var api = CreateConnectionsApi();

            // Act
            var result = await api.GetConnectionsAsync();

            // Assert
            Assert.NotNull(result);
            Assert.NotNull(result.Connections);
        }

        [Fact]
        public async Task GetConnection_DeserializesCorrectly()
        {
            // Arrange
            var connectionId = "conn_test_123";
            var expectedResponse = new
            {
                connection = new
                {
                    id = connectionId,
                    name = "Google Connection",
                    type = "saml",
                    domain = "example.com"
                }
            };

            MockServer.SetMockResponse($"/api/v1/connections/{connectionId}", "GET", expectedResponse);
            var api = CreateConnectionsApi();

            // Act
            var result = await api.GetConnectionAsync(connectionId);

            // Assert
            Assert.NotNull(result);
            Assert.NotNull(result._Connection);
            Assert.NotNull(result._Connection.Id);
        }

        [Fact]
        public async Task CreateConnection_SerializesAndDeserializesCorrectly()
        {
            // Arrange
            var request = new CreateConnectionRequest(
                name: "New Connection",
                displayName: "New Connection Display",
                strategy: CreateConnectionRequest.StrategyEnum.Samlcustom
            );

            var expectedResponse = new
            {
                connection = new
                {
                    id = "conn_new_123",
                    name = "New Connection",
                    display_name = "New Connection Display",
                    strategy = "saml:custom"
                },
                message = "Connection created successfully"
            };

            MockServer.SetMockResponse("/api/v1/connections", "POST", expectedResponse);
            var api = CreateConnectionsApi();

            // Act
            var result = await api.CreateConnectionAsync(request);

            // Assert
            Assert.NotNull(result);
            Assert.NotNull(result.Connection);
            Assert.NotNull(result.Connection.Id);
        }
    }
}

