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
    /// Auto-generated integration tests for SearchApi with both mock and real API support
    /// </summary>
    public class SearchApiIntegrationTests : BaseIntegrationTest, IClassFixture<TestResourceFixture>
    {
        private readonly ITestOutputHelper _output;
        private readonly TestResourceFixture _fixture;

        public SearchApiIntegrationTests(ITestOutputHelper output, TestResourceFixture fixture) : base()
        {
            _output = output;
            _fixture = fixture;
        }


        [Fact]
        [Trait("TestMode", "Mock")]
        public async Task SearchUsers_Mock_Test()
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

                var mockResponse = new SearchUsersResponse();
                mockHandler.AddResponse("GET", "/api/v1/search/users", mockResponse);

                var api = CreateApi((client, config) => new SearchApi(client, config));

                // Act
                var response = await api.SearchUsersAsync();

                // Assert
                Assert.NotNull(response);
                _output.WriteLine($"Mock test successful");
            }
            catch (Exception ex)
            {
                _output.WriteLine($"Error in SearchUsers test: {ex.Message}");
                throw;
            }
        }


        [Fact]
        [Trait("TestMode", "Real")]
        public async Task SearchUsers_Real_Test()
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
                var api = CreateApi((client, config) => new SearchApi(client, config));

                var response = await api.SearchUsersAsync();

                // Assert
                Assert.NotNull(response);
                _output.WriteLine($"Response received: {response?.GetType().Name}");
                _output.WriteLine($"Test completed successfully");
            }
            catch (Exception ex)
            {
                _output.WriteLine($"Error in SearchUsers test: {ex.Message}");
                throw;
            }
        }

    }
}
