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
    /// Auto-generated integration tests for BillingAgreementsApi with both mock and real API support
    /// </summary>
    public class BillingAgreementsApiIntegrationTests : BaseIntegrationTest, IClassFixture<TestResourceFixture>
    {
        private readonly ITestOutputHelper _output;
        private readonly TestResourceFixture _fixture;

        public BillingAgreementsApiIntegrationTests(ITestOutputHelper output, TestResourceFixture fixture) : base()
        {
            _output = output;
            _fixture = fixture;
        }


        [Fact]
        [Trait("TestMode", "Mock")]
        public async Task GetBillingAgreements_Mock_Test()
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

                var customer_id = "customer_id";
                var mockResponse = new GetBillingAgreementsResponse();
                mockHandler.AddResponse("GET", "/api/v1/billing/agreements", mockResponse);

                var api = CreateApi((client, config) => new BillingAgreementsApi(client, config));

                // Act
                var response = await api.GetBillingAgreementsAsync(customer_id);

                // Assert
                Assert.NotNull(response);
                _output.WriteLine($"Mock test successful");
            }
            catch (Exception ex)
            {
                _output.WriteLine($"Error in GetBillingAgreements test: {ex.Message}");
                throw;
            }
        }


        [Fact]
        [Trait("TestMode", "Real")]
        public async Task GetBillingAgreements_Real_Test()
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
                var customer_id = "customer_id";

                var api = CreateApi((client, config) => new BillingAgreementsApi(client, config));

                var response = await api.GetBillingAgreementsAsync(customer_id);

                // Assert
                Assert.NotNull(response);
                _output.WriteLine($"Response received: {response?.GetType().Name}");
                _output.WriteLine($"Test completed successfully");
            }
            catch (Exception ex)
            {
                _output.WriteLine($"Error in GetBillingAgreements test: {ex.Message}");
                throw;
            }
        }


        [Fact]
        [Trait("TestMode", "Mock")]
        public async Task CreateBillingAgreement_Mock_Test()
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

                var mockResponse = new SuccessResponse();
                mockHandler.AddResponse("POST", "/api/v1/billing/agreements", mockResponse);
                var request = new CreateBillingAgreementRequest(customerId: "test-customer_id", planCode: "test-plan_code");

                var api = CreateApi((client, config) => new BillingAgreementsApi(client, config));

                // Act
                var response = await api.CreateBillingAgreementAsync(request);

                // Assert
                Assert.NotNull(response);
                _output.WriteLine($"Mock test successful");
            }
            catch (Exception ex)
            {
                _output.WriteLine($"Error in CreateBillingAgreement test: {ex.Message}");
                throw;
            }
        }


        [Fact]
        [Trait("TestMode", "Real")]
        public async Task CreateBillingAgreement_Real_Test()
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
                var request = new CreateBillingAgreementRequest(customerId: "test-customer_id", planCode: "test-plan_code");

                var api = CreateApi((client, config) => new BillingAgreementsApi(client, config));

                var response = await api.CreateBillingAgreementAsync(request);

                // Assert
                Assert.NotNull(response);
                _output.WriteLine($"Response received: {response?.GetType().Name}");
                _output.WriteLine($"Test completed successfully");
            }
            catch (Exception ex)
            {
                _output.WriteLine($"Error in CreateBillingAgreement test: {ex.Message}");
                throw;
            }
        }

    }
}
