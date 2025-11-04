using Kinde.Api.Api;
using Kinde.Api.Client;
using Kinde.Api.Model;
using Kinde.Api.Test.Integration;
using Xunit;

namespace Kinde.Api.Test.Integration.Api
{
    /// <summary>
    /// Integration tests for IndustriesApi using the OpenAPI Mock Server.
    /// These tests validate serialization and deserialization of API requests and responses.
    /// </summary>
    public class IndustriesApiIntegrationTests : IntegrationTestBase
    {
        public IndustriesApiIntegrationTests(MockServerFixture fixture) : base(fixture)
        {
        }

        [Fact]
        public async Task GetIndustries_DeserializesCorrectly()
        {
            // Arrange
            var expectedResponse = new
            {
                industries = new[]
                {
                    new
                    {
                        id = "industry_1",
                        name = "Technology"
                    },
                    new
                    {
                        id = "industry_2",
                        name = "Healthcare"
                    }
                }
            };

            MockServer.SetMockResponse("/api/v1/industries", "GET", expectedResponse);
            var api = CreateIndustriesApi();

            // Act
            var result = await api.GetIndustriesAsync();

            // Assert
            Assert.NotNull(result);
            Assert.NotNull(result.Industries);
            Assert.True(result.Industries.Count > 0);
        }
    }
}

