using Kinde.Api.Api;
using Kinde.Api.Client;
using Kinde.Api.Model;
using Kinde.Api.Test.Integration;
using Xunit;

namespace Kinde.Api.Test.Integration.Api
{
    /// <summary>
    /// Integration tests for MFAApi using the OpenAPI Mock Server.
    /// These tests validate serialization and deserialization of API requests and responses.
    /// </summary>
    public class MFAApiIntegrationTests : IntegrationTestBase
    {
        public MFAApiIntegrationTests(MockServerFixture fixture) : base(fixture)
        {
        }

        [Fact]
        public async Task ReplaceMFA_SerializesAndDeserializesCorrectly()
        {
            // Arrange
            var request = new ReplaceMFARequest(
                policy: ReplaceMFARequest.PolicyEnum.Required,
                enabledFactors: new List<ReplaceMFARequest.EnabledFactorsEnum>
                {
                    ReplaceMFARequest.EnabledFactorsEnum.Email,
                    ReplaceMFARequest.EnabledFactorsEnum.Authenticatorapp
                }
            );

            var expectedResponse = new
            {
                code = "SUCCESS",
                message = "MFA configuration updated successfully"
            };

            MockServer.SetMockResponse("/api/v1/mfa", "PUT", expectedResponse);
            var api = CreateMFAApi();

            // Act
            var result = await api.ReplaceMFAAsync(request);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("SUCCESS", result.Code);
        }
    }
}

