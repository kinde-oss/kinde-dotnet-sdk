using System;
using System.Threading.Tasks;
using Kinde.Api.Api;
using Kinde.Api.Model;
using Xunit;
using Xunit.Abstractions;

namespace Kinde.Api.Test.Integration.Api
{
    /// <summary>
    /// Integration tests for OrganizationsApi with both mock and real API support
    /// </summary>
    public class OrganizationsApiIntegrationTests : BaseIntegrationTest
    {
        private readonly ITestOutputHelper _output;

        public OrganizationsApiIntegrationTests(ITestOutputHelper output) : base()
        {
            _output = output;
        }

        [Fact]
        [Trait("TestMode", "Real")]
        public async Task GetOrganizations_RealApi_ReturnsOrganizations()
        {
            if (!UseRealApi)
            {
                _output.WriteLine("Skipping real API test - using mocks");
                return;
            }

            var orgsApi = CreateApi((client, config) => new OrganizationsApi(client, config));

            try
            {
                var response = await orgsApi.GetOrganizationsAsync();
                
                Assert.NotNull(response);
                _output.WriteLine($"Retrieved {response?.Organizations?.Count ?? 0} organizations");
            }
            catch (Exception ex)
            {
                _output.WriteLine($"Error calling GetOrganizations: {ex.Message}");
                throw;
            }
        }

        [Fact]
        [Trait("TestMode", "Real")]
        public async Task CreateOrganization_RealApi_CreatesAndDeserializes()
        {
            if (!UseRealApi)
            {
                _output.WriteLine("Skipping real API test - using mocks");
                return;
            }

            var orgsApi = CreateApi((client, config) => new OrganizationsApi(client, config));

            var createRequest = new CreateOrganizationRequest
            {
                Name = $"Test Org {Guid.NewGuid():N}"
            };

            try
            {
                var createResponse = await orgsApi.CreateOrganizationAsync(createRequest);
                
                Assert.NotNull(createResponse);
                Assert.NotNull(createResponse.Organization);
                _output.WriteLine($"Created organization: {createResponse.Organization.Code}");

                // Verify deserialization by getting the organization
                var getResponse = await orgsApi.GetOrganizationAsync(createResponse.Organization.Code);
                
                Assert.NotNull(getResponse);
                Assert.NotNull(getResponse.Code);
                Assert.Equal(createResponse.Organization.Code, getResponse.Code);
                _output.WriteLine($"Retrieved organization: {getResponse.Code}");
            }
            catch (Exception ex)
            {
                _output.WriteLine($"Error in CreateOrganization test: {ex.Message}");
                throw;
            }
        }
    }
}

