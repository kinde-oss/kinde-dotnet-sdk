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
    /// Auto-generated integration tests for PropertyCategoriesApi with both mock and real API support
    /// </summary>
    public class PropertyCategoriesApiIntegrationTests : BaseIntegrationTest, IClassFixture<TestResourceFixture>
    {
        private readonly ITestOutputHelper _output;
        private readonly TestResourceFixture _fixture;

        public PropertyCategoriesApiIntegrationTests(ITestOutputHelper output, TestResourceFixture fixture) : base()
        {
            _output = output;
            _fixture = fixture;
        }


        [Fact]
        [Trait("TestMode", "Mock")]
        public async Task GetCategories_Mock_Test()
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

                var mockResponse = new GetCategoriesResponse();
                mockHandler.AddResponse("GET", "/api/v1/property_categories", mockResponse);

                var api = CreateApi((client, config) => new PropertyCategoriesApi(client, config));

                // Act
                var response = await api.GetCategoriesAsync();

                // Assert
                Assert.NotNull(response);
                _output.WriteLine($"Mock test successful");
            }
            catch (Exception ex)
            {
                _output.WriteLine($"Error in GetCategories test: {ex.Message}");
                throw;
            }
        }


        [Fact]
        [Trait("TestMode", "Real")]
        public async Task GetCategories_Real_Test()
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
                var api = CreateApi((client, config) => new PropertyCategoriesApi(client, config));

                var response = await api.GetCategoriesAsync();

                // Assert
                Assert.NotNull(response);
                _output.WriteLine($"Response received: {response?.GetType().Name}");
                _output.WriteLine($"Test completed successfully");
            }
            catch (Exception ex)
            {
                _output.WriteLine($"Error in GetCategories test: {ex.Message}");
                throw;
            }
        }


        [Fact]
        [Trait("TestMode", "Mock")]
        public async Task CreateCategory_Mock_Test()
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

                var mockResponse = new CreateCategoryResponse();
                mockHandler.AddResponse("POST", "/api/v1/property_categories", mockResponse);
                var request = new CreateCategoryRequest(name: "test-name", context: CreateCategoryRequest.ContextEnum.Org);

                var api = CreateApi((client, config) => new PropertyCategoriesApi(client, config));

                // Act
                var response = await api.CreateCategoryAsync(request);

                // Assert
                Assert.NotNull(response);
                _output.WriteLine($"Mock test successful");
            }
            catch (Exception ex)
            {
                _output.WriteLine($"Error in CreateCategory test: {ex.Message}");
                throw;
            }
        }


        [Fact]
        [Trait("TestMode", "Real")]
        public async Task CreateCategory_Real_Test()
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
                var request = new CreateCategoryRequest(name: "test-name", context: CreateCategoryRequest.ContextEnum.Org);

                var api = CreateApi((client, config) => new PropertyCategoriesApi(client, config));

                var response = await api.CreateCategoryAsync(request);

                // Assert
                Assert.NotNull(response);
                _output.WriteLine($"Response received: {response?.GetType().Name}");
                _output.WriteLine($"Test completed successfully");
            }
            catch (Exception ex)
            {
                _output.WriteLine($"Error in CreateCategory test: {ex.Message}");
                throw;
            }
        }


        [Fact]
        [Trait("TestMode", "Mock")]
        public async Task UpdateCategory_Mock_Test()
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

                var category_id = "test-category_id";
                var mockResponse = new SuccessResponse();
                mockHandler.AddResponse("PUT", $"/api/v1/property_categories/{category_id}", mockResponse);
                var request = new UpdateCategoryRequest();

                var api = CreateApi((client, config) => new PropertyCategoriesApi(client, config));

                // Act
                var response = await api.UpdateCategoryAsync(category_id, request);

                // Assert
                Assert.NotNull(response);
                _output.WriteLine($"Mock test successful");
            }
            catch (Exception ex)
            {
                _output.WriteLine($"Error in UpdateCategory test: {ex.Message}");
                throw;
            }
        }


        [Fact]
        [Trait("TestMode", "Real")]
        public async Task UpdateCategory_Real_Test()
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
                // WARNING: Real API test - This operation requires existing category_id
                // This test may fail if the resource doesn't exist in your Kinde instance
                // Consider creating the resource first or using a test environment
                // WARNING: Using placeholder category_id - test will likely fail without real resource ID
                var category_id = "test-category_id";
                var request = new UpdateCategoryRequest();

                var api = CreateApi((client, config) => new PropertyCategoriesApi(client, config));

                var response = await api.UpdateCategoryAsync(category_id, request);

                // Assert
                Assert.NotNull(response);
                _output.WriteLine($"Response received: {response?.GetType().Name}");
                _output.WriteLine($"Test completed successfully");
            }
            catch (Exception ex)
            {
                _output.WriteLine($"Error in UpdateCategory test: {ex.Message}");
                throw;
            }
        }

    }
}
