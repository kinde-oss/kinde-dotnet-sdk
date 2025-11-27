using System;
using System.Threading.Tasks;
using Kinde.Api.Api;
using Kinde.Api.Model;
using Kinde.Api.Test.Integration;
using Xunit;
using Xunit.Abstractions;

namespace Kinde.Api.Test.Integration.Api.Generated
{
    /// <summary>
    /// Auto-generated integration tests for BillingMeterUsageApi with both mock and real API support
    /// </summary>
    public class BillingMeterUsageApiIntegrationTests : BaseIntegrationTest, IClassFixture<TestResourceFixture>
    {
        private readonly ITestOutputHelper _output;
        private readonly TestResourceFixture _fixture;

        public BillingMeterUsageApiIntegrationTests(ITestOutputHelper output, TestResourceFixture fixture) : base()
        {
            _output = output;
            _fixture = fixture;
        }


        [Fact]
        [Trait("TestMode", "Mock")]
        public async Task CreateMeterUsageRecord_Mock_Test()
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

                var mockResponse = new CreateMeterUsageRecordResponse();
                mockHandler.AddResponse("POST", "/api/v1/billing/meter_usage", mockResponse);
                var request = new CreateMeterUsageRecordRequest(customerAgreementId: "test-customer_agreement_id", billingFeatureCode: "test-billing_feature_code", meterValue: "test-meter_value");

                var api = CreateApi((client, config) => new BillingMeterUsageApi(client, config));

                // Act
                var response = await api.CreateMeterUsageRecordAsync(request);

                // Assert
                Assert.NotNull(response);
                _output.WriteLine($"Mock test successful");
            }
            catch (Exception ex)
            {
                _output.WriteLine($"Error in CreateMeterUsageRecord test: {ex.Message}");
                throw;
            }
        }


        [Fact]
        [Trait("TestMode", "Real")]
        public async Task CreateMeterUsageRecord_Real_Test()
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
                var request = new CreateMeterUsageRecordRequest(customerAgreementId: "test-customer_agreement_id", billingFeatureCode: "test-billing_feature_code", meterValue: "test-meter_value");

                var api = CreateApi((client, config) => new BillingMeterUsageApi(client, config));

                var response = await api.CreateMeterUsageRecordAsync(request);

                // Assert
                Assert.NotNull(response);
                _output.WriteLine($"Response received: {response?.GetType().Name}");
                _output.WriteLine($"Test completed successfully");
            }
            catch (Exception ex)
            {
                _output.WriteLine($"Error in CreateMeterUsageRecord test: {ex.Message}");
                throw;
            }
        }

    }
}
