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
    /// Auto-generated integration tests for MFAApi with both mock and real API support
    /// </summary>
    public class MFAApiIntegrationTests : BaseIntegrationTest, IClassFixture<TestResourceFixture>
    {
        private readonly ITestOutputHelper _output;
        private readonly TestResourceFixture _fixture;

        public MFAApiIntegrationTests(ITestOutputHelper output, TestResourceFixture fixture) : base()
        {
            _output = output;
            _fixture = fixture;
        }


        [Fact]
        [Trait("TestMode", "Mock")]
        public async Task ReplaceMFA_Mock_Test()
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
                mockHandler.AddResponse("PUT", "/api/v1/mfa", mockResponse);
                var request = new ReplaceMFARequest(policy: ReplaceMFARequest.PolicyEnum.Required, enabledFactors: new System.Collections.Generic.List<ReplaceMFARequest.EnabledFactorsEnum>());

                var api = CreateApi((client, config) => new MFAApi(client, config));

                // Act
                var response = await api.ReplaceMFAAsync(request);

                // Assert
                Assert.NotNull(response);
                _output.WriteLine($"Mock test successful");
            }
            catch (Exception ex)
            {
                _output.WriteLine($"Error in ReplaceMFA test: {ex.Message}");
                throw;
            }
        }


        [Fact]
        [Trait("TestMode", "Real")]
        public async Task ReplaceMFA_Real_Test()
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
                var request = new ReplaceMFARequest(policy: ReplaceMFARequest.PolicyEnum.Required, enabledFactors: new System.Collections.Generic.List<ReplaceMFARequest.EnabledFactorsEnum>());

                var api = CreateApi((client, config) => new MFAApi(client, config));

                var response = await api.ReplaceMFAAsync(request);

                // Assert
                Assert.NotNull(response);
                _output.WriteLine($"Response received: {response?.GetType().Name}");
                _output.WriteLine($"Test completed successfully");
            }
            catch (Exception ex)
            {
                _output.WriteLine($"Error in ReplaceMFA test: {ex.Message}");
                throw;
            }
        }

    }
}
