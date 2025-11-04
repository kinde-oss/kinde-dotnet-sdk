using Kinde.Api.Api;
using Kinde.Api.Client;
using Kinde.Api.Model;
using Kinde.Api.Test.Integration;
using Xunit;

namespace Kinde.Api.Test.Integration.Api
{
    /// <summary>
    /// Integration tests for BillingEntitlementsApi using the OpenAPI Mock Server.
    /// These tests validate serialization and deserialization of API requests and responses.
    /// </summary>
    public class BillingEntitlementsApiIntegrationTests : IntegrationTestBase
    {
        public BillingEntitlementsApiIntegrationTests(MockServerFixture fixture) : base(fixture)
        {
        }

        [Fact]
        public async Task GetBillingEntitlements_DeserializesCorrectly()
        {
            // Arrange
            var customerId = "customer_123";
            var expectedResponse = new
            {
                entitlements = new[]
                {
                    new
                    {
                        id = "entitlement_123",
                        customer_id = customerId,
                        feature_code = "premium",
                        value = 100
                    }
                },
                next_token = (string?)null
            };

            MockServer.SetMockResponse($"/api/v1/billing/entitlements", "GET", expectedResponse);
            var api = CreateBillingEntitlementsApi();

            // Act
            var result = await api.GetBillingEntitlementsAsync(customerId);

            // Assert
            Assert.NotNull(result);
            Assert.NotNull(result.Entitlements);
        }
    }
}

