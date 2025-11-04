using Kinde.Api.Api;
using Kinde.Api.Client;
using Kinde.Api.Model;
using Kinde.Api.Test.Integration;
using Xunit;

namespace Kinde.Api.Test.Integration.Api
{
    /// <summary>
    /// Integration tests for BusinessApi using the OpenAPI Mock Server.
    /// These tests validate serialization and deserialization of API requests and responses.
    /// </summary>
    public class BusinessApiIntegrationTests : IntegrationTestBase
    {
        public BusinessApiIntegrationTests(MockServerFixture fixture) : base(fixture)
        {
        }

        [Fact]
        public async Task GetBusiness_DeserializesCorrectly()
        {
            // Arrange
            var expectedResponse = new
            {
                business = new
                {
                    code = "business_123",
                    name = "Test Business",
                    timezone = "America/New_York",
                    is_default = true
                }
            };

            MockServer.SetMockResponse("/api/v1/business", "GET", expectedResponse);
            var api = CreateBusinessApi();

            // Act
            var result = await api.GetBusinessAsync();

            // Assert
            Assert.NotNull(result);
            Assert.NotNull(result.Business);
            Assert.Equal("Test Business", result.Business.Name);
        }

        [Fact]
        public async Task UpdateBusiness_SerializesAndDeserializesCorrectly()
        {
            // Arrange
            var request = new UpdateBusinessRequest(
                businessName: "Updated Business",
                timezoneKey: "America/Los_Angeles"
            );

            var expectedResponse = new
            {
                code = "SUCCESS",
                message = "Business updated successfully",
                business = new
                {
                    code = "business_123",
                    name = "Updated Business",
                    timezone = "America/Los_Angeles",
                    is_default = true
                }
            };

            MockServer.SetMockResponse("/api/v1/business", "PATCH", expectedResponse);
            var api = CreateBusinessApi();

            // Act
            var result = await api.UpdateBusinessAsync(request);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("SUCCESS", result.Code);
        }
    }
}

