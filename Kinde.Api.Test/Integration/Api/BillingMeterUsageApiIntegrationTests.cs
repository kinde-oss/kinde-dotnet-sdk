using Kinde.Api.Api;
using Kinde.Api.Client;
using Kinde.Api.Model;
using Kinde.Api.Test.Integration;
using Xunit;

namespace Kinde.Api.Test.Integration.Api
{
    /// <summary>
    /// Integration tests for BillingMeterUsageApi using the OpenAPI Mock Server.
    /// These tests validate serialization and deserialization of API requests and responses.
    /// </summary>
    public class BillingMeterUsageApiIntegrationTests : IntegrationTestBase
    {
        public BillingMeterUsageApiIntegrationTests(MockServerFixture fixture) : base(fixture)
        {
        }

        [Fact]
        public async Task CreateMeterUsageRecord_SerializesAndDeserializesCorrectly()
        {
            // Arrange
            var request = new CreateMeterUsageRecordRequest(
                customerAgreementId: "agreement_123",
                billingFeatureCode: "api_calls",
                meterValue: "1000"
            );

            var expectedResponse = new
            {
                code = "CREATED",
                message = "Meter usage record created successfully"
            };

            MockServer.SetMockResponse("/api/v1/billing/meter_usage", "POST", expectedResponse);
            var api = CreateBillingMeterUsageApi();

            // Act
            var result = await api.CreateMeterUsageRecordAsync(request);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("CREATED", result.Code);
        }
    }
}

