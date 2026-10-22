using System;
using System.Collections.Generic;
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
    /// Integration tests for OrganizationsApi with both mock and real API support.
    /// </summary>
    public class OrganizationsApiIntegrationTests : BaseIntegrationTest, IClassFixture<TestResourceFixture>
    {
        private readonly ITestOutputHelper _output;
        private readonly TestResourceFixture _fixture;

        public OrganizationsApiIntegrationTests(ITestOutputHelper output, TestResourceFixture fixture) : base()
        {
            _output = output;
            _fixture = fixture;
        }

        #region GetOrganizations Tests

        [Fact]
        [Trait("TestMode", "Mock")]
        public async Task GetOrganizations_Mock_ReturnsOrganizations()
        {
            if (UseRealApi) return;

            var mockHandler = GetKiotaMockHandler();
            if (mockHandler == null) return;

            var kiotaResponse = new KiotaModels.Get_organizations_response
            {
                Code = "200",
                Message = "Organizations retrieved",
                Organizations = new List<KiotaModels.Organization_item_schema>
                {
                    new KiotaModels.Organization_item_schema 
                    { 
                        Code = "org_1", 
                        Name = "Test Organization 1",
                        Handle = "test-org-1",
                        IsDefault = false
                    },
                    new KiotaModels.Organization_item_schema 
                    { 
                        Code = "org_2", 
                        Name = "Test Organization 2",
                        Handle = "test-org-2",
                        IsDefault = true
                    }
                },
                NextToken = "next_page_token"
            };
            mockHandler.AddKiotaResponse("GET", "/api/v1/organizations", kiotaResponse);

                var api = CreateApi((client, config) => new OrganizationsApi(client, config));

            var response = await api.GetOrganizationsAsync();

                Assert.NotNull(response);
            Assert.Equal("200", response.Code);
            Assert.NotNull(response.Organizations);
            Assert.Equal(2, response.Organizations.Count);
            Assert.Equal("org_1", response.Organizations[0].Code);
            Assert.Equal("Test Organization 1", response.Organizations[0].Name);
            Assert.False(response.Organizations[0].IsDefault);
            Assert.True(response.Organizations[1].IsDefault);
            Assert.Equal("next_page_token", response.NextToken);
            
            _output.WriteLine($"Mock test successful - Retrieved {response.Organizations.Count} organizations");
        }

        [Fact]
        [Trait("TestMode", "Mock")]
        public async Task GetOrganizations_Mock_IsDefault_False()
        {
            if (UseRealApi) return;

            var mockHandler = GetKiotaMockHandler();
            if (mockHandler == null) return;

            var kiotaResponse = new KiotaModels.Get_organizations_response
            {
                Code = "200",
                Organizations = new List<KiotaModels.Organization_item_schema>
                {
                    new KiotaModels.Organization_item_schema 
                    { 
                        Code = "org_not_default", 
                        Name = "Not Default Org",
                        IsDefault = false
                    }
                }
            };
            mockHandler.AddKiotaResponse("GET", "/api/v1/organizations", kiotaResponse);

                var api = CreateApi((client, config) => new OrganizationsApi(client, config));

                var response = await api.GetOrganizationsAsync();

                Assert.NotNull(response);
            Assert.False(response.Organizations[0].IsDefault, "IsDefault should be false!");
            _output.WriteLine("PASS: IsDefault=false was preserved correctly");
        }

        [Fact]
        [Trait("TestMode", "Real")]
        public async Task GetOrganizations_Real_Test()
        {
            if (!UseRealApi) return;

                var api = CreateApi((client, config) => new OrganizationsApi(client, config));
                var response = await api.GetOrganizationsAsync();

                Assert.NotNull(response);
            _output.WriteLine($"Real API returned {response.Organizations?.Count ?? 0} organizations");
        }

        #endregion

        #region CreateOrganization Tests

        [Fact]
        [Trait("TestMode", "Mock")]
        public async Task CreateOrganization_Mock_Created()
        {
            if (UseRealApi) return;

            var mockHandler = GetKiotaMockHandler();
            if (mockHandler == null) return;

            var kiotaResponse = new KiotaModels.Create_organization_response
            {
                Code = "201",
                Message = "Organization created"
            };
            mockHandler.AddKiotaResponse("POST", "/api/v1/organization", kiotaResponse);

                var api = CreateApi((client, config) => new OrganizationsApi(client, config));
            var request = new CreateOrganizationRequest(name: "New Organization");

            var response = await api.CreateOrganizationAsync(request);

                Assert.NotNull(response);
            Assert.Equal("201", response.Code);
            Assert.True(mockHandler.WasRequestMade("POST", "/api/v1/organization"));
            _output.WriteLine("CreateOrganization completed successfully");
        }

        #endregion

        #region DeleteOrganization Tests

        [Fact]
        [Trait("TestMode", "Mock")]
        public async Task DeleteOrganization_Mock_Deleted()
        {
            if (UseRealApi) return;

            var mockHandler = GetKiotaMockHandler();
            if (mockHandler == null) return;

            mockHandler.AddEmptyResponse("DELETE", "/api/v1/organization/{org_code}");

                var api = CreateApi((client, config) => new OrganizationsApi(client, config));

            await api.DeleteOrganizationAsync("org_to_delete");
            _output.WriteLine("DeleteOrganization completed successfully");
        }

        #endregion

        #region Organization Users Tests

        [Fact]
        [Trait("TestMode", "Mock")]
        public async Task GetOrganizationUsers_Mock_ReturnsUsers()
        {
            if (UseRealApi) return;

            var mockHandler = GetKiotaMockHandler();
            if (mockHandler == null) return;

            var kiotaResponse = new KiotaModels.Get_organization_users_response
            {
                Code = "200",
                Message = "Users retrieved",
                OrganizationUsers = new List<KiotaModels.Organization_user>
                {
                    new KiotaModels.Organization_user { Id = "user_1" },
                    new KiotaModels.Organization_user { Id = "user_2" }
                }
            };
            mockHandler.AddKiotaResponse("GET", "/api/v1/organizations/{org_code}/users", kiotaResponse);

                var api = CreateApi((client, config) => new OrganizationsApi(client, config));

            var response = await api.GetOrganizationUsersAsync("org_123");

                Assert.NotNull(response);
            Assert.NotNull(response.OrganizationUsers);
            Assert.Equal(2, response.OrganizationUsers.Count);
            _output.WriteLine("GetOrganizationUsers returned correct users");
        }

        [Fact]
        [Trait("TestMode", "Mock")]
        public async Task AddOrganizationUsers_Mock_Success()
        {
            if (UseRealApi) return;

            var mockHandler = GetKiotaMockHandler();
            if (mockHandler == null) return;

            var kiotaResponse = new KiotaModels.Add_organization_users_response
            {
                Code = "200",
                Message = "Users added to organization"
            };
            mockHandler.AddKiotaResponse("POST", "/api/v1/organizations/{org_code}/users", kiotaResponse);

                var api = CreateApi((client, config) => new OrganizationsApi(client, config));
            var request = new AddOrganizationUsersRequest
            {
                Users = new List<AddOrganizationUsersRequestUsersInner>
                {
                    new AddOrganizationUsersRequestUsersInner { Id = "user_new_1" }
                }
            };

            var response = await api.AddOrganizationUsersAsync("org_123", request);

                Assert.NotNull(response);
            Assert.Equal("200", response.Code);
            _output.WriteLine("AddOrganizationUsers completed successfully");
        }

        [Fact]
        [Trait("TestMode", "Mock")]
        public async Task RemoveOrganizationUser_Mock_Success()
        {
            if (UseRealApi) return;

            var mockHandler = GetKiotaMockHandler();
            if (mockHandler == null) return;

            var kiotaResponse = new KiotaModels.Success_response
            {
                Code = "200",
                Message = "User removed from organization"
            };
            mockHandler.AddKiotaResponse("DELETE", "/api/v1/organizations/{org_code}/users/{user_id}", kiotaResponse);

                var api = CreateApi((client, config) => new OrganizationsApi(client, config));

            var response = await api.RemoveOrganizationUserAsync("org_123", "user_to_remove");

                Assert.NotNull(response);
            Assert.Equal("200", response.Code);
            _output.WriteLine("RemoveOrganizationUser completed successfully");
        }

        #endregion

        #region Organization Feature Flags Tests

        [Fact]
        [Trait("TestMode", "Mock")]
        public async Task GetOrganizationFeatureFlags_Mock_ReturnsFlags()
        {
            if (UseRealApi) return;

            var mockHandler = GetKiotaMockHandler();
            if (mockHandler == null) return;

            var kiotaResponse = new KiotaModels.Get_organization_feature_flags_response
            {
                Code = "200",
                Message = "Feature flags retrieved"
            };
            mockHandler.AddKiotaResponse("GET", "/api/v1/organizations/{org_code}/feature_flags", kiotaResponse);

                var api = CreateApi((client, config) => new OrganizationsApi(client, config));

            var response = await api.GetOrganizationFeatureFlagsAsync("org_123");

                Assert.NotNull(response);
            Assert.Equal("200", response.Code);
            _output.WriteLine("GetOrganizationFeatureFlags returned correctly");
        }

        [Fact]
        [Trait("TestMode", "Mock")]
        public async Task UpdateOrganizationFeatureFlagOverride_Mock_Success()
        {
            if (UseRealApi) return;

            var mockHandler = GetKiotaMockHandler();
            if (mockHandler == null) return;

            var kiotaResponse = new KiotaModels.Success_response
            {
                Code = "200",
                Message = "Feature flag override updated"
            };
            mockHandler.AddKiotaResponse("PATCH", "/api/v1/organizations/{org_code}/feature_flags/{feature_flag_key}", kiotaResponse);

                var api = CreateApi((client, config) => new OrganizationsApi(client, config));

            var response = await api.UpdateOrganizationFeatureFlagOverrideAsync("org_123", "my_feature", "true");

                Assert.NotNull(response);
            Assert.Equal("200", response.Code);
            _output.WriteLine("UpdateOrganizationFeatureFlagOverride completed successfully");
        }

        #endregion

        #region Null Handling Tests

        [Fact]
        [Trait("TestMode", "Mock")]
        public async Task GetOrganizations_Mock_NullOrganizationsList_StaysNull()
        {
            if (UseRealApi) return;

            var mockHandler = GetKiotaMockHandler();
            if (mockHandler == null) return;

            var kiotaResponse = new KiotaModels.Get_organizations_response
            {
                Code = "200",
                Message = "Success",
                Organizations = null
            };
            mockHandler.AddKiotaResponse("GET", "/api/v1/organizations", kiotaResponse);

                var api = CreateApi((client, config) => new OrganizationsApi(client, config));

            var response = await api.GetOrganizationsAsync();

                Assert.NotNull(response);
            Assert.Null(response.Organizations);
            _output.WriteLine("Null organizations list preserved correctly");
        }

        #endregion

        #region Error Handling Tests

        [Fact]
        [Trait("TestMode", "Mock")]
        public async Task GetOrganization_Mock_NotFound_ThrowsException()
        {
            if (UseRealApi) return;

            var mockHandler = GetKiotaMockHandler();
            if (mockHandler == null) return;

            mockHandler.AddErrorResponse("GET", "/api/v1/organization", 
                System.Net.HttpStatusCode.NotFound, "not_found", "Organization not found");

                var api = CreateApi((client, config) => new OrganizationsApi(client, config));

            var exception = await Assert.ThrowsAsync<ApiException>(() => api.GetOrganizationAsync(code: "nonexistent"));
            Assert.Equal(404, exception.ErrorCode);
            _output.WriteLine("404 error handled correctly");
        }

        [Fact]
        [Trait("TestMode", "Mock")]
        public async Task CreateOrganization_Mock_Conflict_ThrowsException()
        {
            if (UseRealApi) return;

            var mockHandler = GetKiotaMockHandler();
            if (mockHandler == null) return;

            mockHandler.AddErrorResponse("POST", "/api/v1/organization", 
                System.Net.HttpStatusCode.Conflict, "conflict", "Organization with this handle already exists");

                var api = CreateApi((client, config) => new OrganizationsApi(client, config));
            var request = new CreateOrganizationRequest(name: "Duplicate Org");

            var exception = await Assert.ThrowsAsync<ApiException>(() => api.CreateOrganizationAsync(request));
            Assert.Equal(409, exception.ErrorCode);
            _output.WriteLine("409 error handled correctly");
        }

        #endregion
    }
}
