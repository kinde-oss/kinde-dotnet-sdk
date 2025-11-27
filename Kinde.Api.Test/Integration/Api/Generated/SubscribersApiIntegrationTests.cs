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
    /// Auto-generated integration tests for SubscribersApi with both mock and real API support
    /// </summary>
    public class SubscribersApiIntegrationTests : BaseIntegrationTest, IClassFixture<TestResourceFixture>
    {
        private readonly ITestOutputHelper _output;
        private readonly TestResourceFixture _fixture;

        public SubscribersApiIntegrationTests(ITestOutputHelper output, TestResourceFixture fixture) : base()
        {
            _output = output;
            _fixture = fixture;
        }


        [Fact]
        [Trait("TestMode", "Mock")]
        public async Task GetSubscribers_Mock_Test()
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

                var mockResponse = new GetSubscribersResponse();
                mockHandler.AddResponse("GET", "/api/v1/subscribers", mockResponse);

                var api = CreateApi((client, config) => new SubscribersApi(client, config));

                // Act
                var response = await api.GetSubscribersAsync();

                // Assert
                Assert.NotNull(response);
                _output.WriteLine($"Mock test successful");
            }
            catch (Exception ex)
            {
                _output.WriteLine($"Error in GetSubscribers test: {ex.Message}");
                throw;
            }
        }


        [Fact]
        [Trait("TestMode", "Real")]
        public async Task GetSubscribers_Real_Test()
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
                var api = CreateApi((client, config) => new SubscribersApi(client, config));

                var response = await api.GetSubscribersAsync();

                // Assert
                Assert.NotNull(response);
                _output.WriteLine($"Response received: {response?.GetType().Name}");
                _output.WriteLine($"Test completed successfully");
            }
            catch (Exception ex)
            {
                _output.WriteLine($"Error in GetSubscribers test: {ex.Message}");
                throw;
            }
        }


        [Fact]
        [Trait("TestMode", "Mock")]
        public async Task CreateSubscriber_Mock_Test()
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

                var first_name = "first_name";
                var last_name = "last_name";
                var email = "email";
                var mockResponse = new CreateSubscriberSuccessResponse();
                mockHandler.AddResponse("POST", "/api/v1/subscribers", mockResponse);

                var api = CreateApi((client, config) => new SubscribersApi(client, config));

                // Act
                var response = await api.CreateSubscriberAsync(first_name, last_name, email);

                // Assert
                Assert.NotNull(response);
                _output.WriteLine($"Mock test successful");
            }
            catch (Exception ex)
            {
                _output.WriteLine($"Error in CreateSubscriber test: {ex.Message}");
                throw;
            }
        }


        [Fact]
        [Trait("TestMode", "Real")]
        public async Task CreateSubscriber_Real_Test()
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
                var first_name = "first_name";
                var last_name = "last_name";
                var email = "email";

                var api = CreateApi((client, config) => new SubscribersApi(client, config));

                var response = await api.CreateSubscriberAsync(first_name, last_name, email);

                // Assert
                Assert.NotNull(response);
                _output.WriteLine($"Response received: {response?.GetType().Name}");
                _output.WriteLine($"Test completed successfully");
            }
            catch (Exception ex)
            {
                _output.WriteLine($"Error in CreateSubscriber test: {ex.Message}");
                throw;
            }
        }


        [Fact]
        [Trait("TestMode", "Mock")]
        public async Task GetSubscriber_Mock_Test()
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

                var subscriber_id = "test-subscriber_id";
                var mockResponse = new GetSubscriberResponse();
                mockHandler.AddResponse("GET", $"/api/v1/subscribers/{subscriber_id}", mockResponse);

                var api = CreateApi((client, config) => new SubscribersApi(client, config));

                // Act
                var response = await api.GetSubscriberAsync(subscriber_id);

                // Assert
                Assert.NotNull(response);
                _output.WriteLine($"Mock test successful");
            }
            catch (Exception ex)
            {
                _output.WriteLine($"Error in GetSubscriber test: {ex.Message}");
                throw;
            }
        }


        [Fact]
        [Trait("TestMode", "Real")]
        public async Task GetSubscriber_Real_Test()
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
                // Note: This test uses a placeholder subscriber_id and may fail if the resource doesn't exist
                var subscriber_id = "test-subscriber_id";

                var api = CreateApi((client, config) => new SubscribersApi(client, config));

                var response = await api.GetSubscriberAsync(subscriber_id);

                // Assert
                Assert.NotNull(response);
                _output.WriteLine($"Response received: {response?.GetType().Name}");
                _output.WriteLine($"Test completed successfully");
            }
            catch (Exception ex)
            {
                _output.WriteLine($"Error in GetSubscriber test: {ex.Message}");
                throw;
            }
        }

    }
}
