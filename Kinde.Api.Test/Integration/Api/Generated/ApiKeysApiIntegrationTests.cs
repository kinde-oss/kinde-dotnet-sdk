using System.Threading.Tasks;
using Kinde.Api.Api;
using Kinde.Api.Model;
using Kinde.Api.Client;
using Kinde.Api.Test.Integration;
using Xunit;
using Xunit.Abstractions;
using KiotaModels = Kinde.Api.Kiota.Management.Models;

namespace Kinde.Api.Test.Integration.Api.Generated
{
    /// <summary>
    /// Integration tests for ApiKeysApi with both mock and real API support.
    /// </summary>
    public class ApiKeysApiIntegrationTests : BaseIntegrationTest, IClassFixture<TestResourceFixture>
    {
        private readonly ITestOutputHelper _output;
        private readonly TestResourceFixture _fixture;

        public ApiKeysApiIntegrationTests(ITestOutputHelper output, TestResourceFixture fixture) : base()
        {
            _output = output;
            _fixture = fixture;
        }

        #region CreateApiKey Tests

        [Fact]
        [Trait("TestMode", "Mock")]
        public async Task CreateApiKey_Mock_Created()
        {
            if (UseRealApi) return;

            var mockHandler = GetKiotaMockHandler();
            if (mockHandler == null) return;

            var kiotaResponse = new KiotaModels.Create_api_key_response
            {
                Code = "201",
                Message = "API key created",
                ApiKey = new KiotaModels.Create_api_key_response_api_key
                {
                    Id = "key_1",
                    Key = "ak_live_abc123"
                }
            };
            mockHandler.AddKiotaResponse("POST", "/api/v1/api_keys", kiotaResponse, System.Net.HttpStatusCode.Created);

            var api = CreateApi((client, config) => new ApiKeysApi(client, config));
            var request = new CreateApiKeyRequest(name: "Test API Key", type: CreateApiKeyRequest.TypeEnum.Organization, apiId: "api_id_123");

            var response = await api.CreateApiKeyAsync(request);

            Assert.NotNull(response);
            Assert.Equal("201", response.Code);
            Assert.Equal("key_1", response.ApiKey?.Id);
            Assert.True(mockHandler.WasRequestMade("POST", "/api/v1/api_keys"));
            _output.WriteLine("CreateApiKey completed successfully");
        }

        [Fact]
        [Trait("TestMode", "Real")]
        public async Task CreateApiKey_Real_Test()
        {
            if (!UseRealApi) return;

            var api = CreateApi((client, config) => new ApiKeysApi(client, config));
            var request = new CreateApiKeyRequest(name: $"Test API Key {_fixture.OrganizationCode}", type: CreateApiKeyRequest.TypeEnum.Organization, apiId: "api_id_123");

            var response = await api.CreateApiKeyAsync(request);

            Assert.NotNull(response);
            _output.WriteLine($"Real API returned key id {response.ApiKey?.Id}");
        }

        #endregion

        #region VerifyApiKey Tests

        [Fact]
        [Trait("TestMode", "Mock")]
        public async Task VerifyApiKey_Mock_ReturnsValidity()
        {
            if (UseRealApi) return;

            var mockHandler = GetKiotaMockHandler();
            if (mockHandler == null) return;

            var kiotaResponse = new KiotaModels.Verify_api_key_response
            {
                Code = "200",
                Message = "API key verified",
                IsValid = true,
                KeyId = "key_1",
                Status = "active"
            };
            mockHandler.AddKiotaResponse("POST", "/api/v1/api_keys/verify", kiotaResponse);

            var api = CreateApi((client, config) => new ApiKeysApi(client, config));
            var request = new VerifyApiKeyRequest(apiKey: "ak_live_abc123");

            var response = await api.VerifyApiKeyAsync(request);

            Assert.NotNull(response);
            Assert.True(response.IsValid);
            Assert.Equal("key_1", response.KeyId);
            Assert.True(mockHandler.WasRequestMade("POST", "/api/v1/api_keys/verify"));
            _output.WriteLine("VerifyApiKey completed successfully");
        }

        [Fact]
        [Trait("TestMode", "Mock")]
        public async Task VerifyApiKey_Mock_Invalid_ReturnsFalse()
        {
            if (UseRealApi) return;

            var mockHandler = GetKiotaMockHandler();
            if (mockHandler == null) return;

            var kiotaResponse = new KiotaModels.Verify_api_key_response
            {
                Code = "200",
                Message = "API key is not valid",
                IsValid = false
            };
            mockHandler.AddKiotaResponse("POST", "/api/v1/api_keys/verify", kiotaResponse);

            var api = CreateApi((client, config) => new ApiKeysApi(client, config));
            var request = new VerifyApiKeyRequest(apiKey: "not-a-real-key");

            var response = await api.VerifyApiKeyAsync(request);

            Assert.NotNull(response);
            Assert.False(response.IsValid);
            _output.WriteLine("Invalid API key correctly reported as not valid");
        }

        [Fact]
        [Trait("TestMode", "Real")]
        public async Task VerifyApiKey_Real_Test()
        {
            if (!UseRealApi) return;

            var api = CreateApi((client, config) => new ApiKeysApi(client, config));
            var request = new VerifyApiKeyRequest(apiKey: "invalid-test-key");

            var response = await api.VerifyApiKeyAsync(request);

            Assert.NotNull(response);
            _output.WriteLine($"Real API reported IsValid={response.IsValid}");
        }

        #endregion

        #region Error Handling Tests

        [Fact]
        [Trait("TestMode", "Mock")]
        public async Task CreateApiKey_Mock_Conflict_ThrowsException()
        {
            if (UseRealApi) return;

            var mockHandler = GetKiotaMockHandler();
            if (mockHandler == null) return;

            mockHandler.AddErrorResponse("POST", "/api/v1/api_keys",
                System.Net.HttpStatusCode.Conflict, "conflict", "API key with this name already exists");

            var api = CreateApi((client, config) => new ApiKeysApi(client, config));
            var request = new CreateApiKeyRequest(name: "Duplicate", type: CreateApiKeyRequest.TypeEnum.Organization, apiId: "api_id_123");

            var exception = await Assert.ThrowsAsync<ApiException>(() => api.CreateApiKeyAsync(request));
            Assert.Equal(409, exception.ErrorCode);
            _output.WriteLine("409 error handled correctly");
        }

        #endregion
    }
}
