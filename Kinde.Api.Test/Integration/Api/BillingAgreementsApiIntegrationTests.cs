using Kinde.Api.Api;
using Kinde.Api.Client;
using Kinde.Api.Model;
using Kinde.Api.Test.Integration;
using Xunit;

namespace Kinde.Api.Test.Integration.Api
{
    /// <summary>
    /// Integration tests for BillingAgreementsApi using the OpenAPI Mock Server.
    /// These tests validate serialization and deserialization of API requests and responses.
    /// </summary>
    public class BillingAgreementsApiIntegrationTests : IntegrationTestBase
    {
        public BillingAgreementsApiIntegrationTests(MockServerFixture fixture) : base(fixture)
        {
        }

        [Fact]
        public async Task GetBillingAgreements_DeserializesCorrectly()
        {
            // Arrange
            var customerId = "customer_123";
            var expectedResponse = new
            {
                agreements = new[]
                {
                    new
                    {
                        id = "agreement_123",
                        customer_id = customerId,
                        feature_code = "premium",
                        status = "active"
                    }
                },
                next_token = (string?)null
            };

            MockServer.SetMockResponse($"/api/v1/billing/agreements", "GET", expectedResponse);
            var api = CreateBillingAgreementsApi();

            // Act
            var result = await api.GetBillingAgreementsAsync(customerId);

            // Assert
            Assert.NotNull(result);
            Assert.NotNull(result.Agreements);
        }

        [Fact]
        public async Task CreateBillingAgreement_SerializesAndDeserializesCorrectly()
        {
            // Arrange
            var request = new CreateBillingAgreementRequest(
                customerId: "customer_123",
                planCode: "premium"
            );

            var expectedResponse = new
            {
                code = "CREATED",
                message = "Agreement created successfully",
                agreement = new
                {
                    id = "agreement_new_123",
                    customer_id = "customer_123",
                    feature_code = "premium",
                    status = "active"
                }
            };

            MockServer.SetMockResponse("/api/v1/billing/agreements", "POST", expectedResponse);
            var api = CreateBillingAgreementsApi();

            // Act
            var result = await api.CreateBillingAgreementAsync(request);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("CREATED", result.Code);
        }
    }
}

