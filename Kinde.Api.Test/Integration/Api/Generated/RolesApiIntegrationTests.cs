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
    /// Integration tests for RolesApi with both mock and real API support.
    /// </summary>
    public class RolesApiIntegrationTests : BaseIntegrationTest, IClassFixture<TestResourceFixture>
    {
        private readonly ITestOutputHelper _output;
        private readonly TestResourceFixture _fixture;

        public RolesApiIntegrationTests(ITestOutputHelper output, TestResourceFixture fixture) : base()
        {
            _output = output;
            _fixture = fixture;
        }

        #region GetRoles Tests

        [Fact]
        [Trait("TestMode", "Mock")]
        public async Task GetRoles_Mock_ReturnsRoles()
        {
            if (UseRealApi) return;

            var mockHandler = GetKiotaMockHandler();
            if (mockHandler == null) return;

            var kiotaResponse = new KiotaModels.Get_roles_response
            {
                Code = "200",
                Message = "Roles retrieved",
                Roles = new List<KiotaModels.Roles>
                {
                    new KiotaModels.Roles 
                    { 
                        Id = "role_1", 
                        Key = "admin",
                        Name = "Administrator",
                        IsDefaultRole = false
                    },
                    new KiotaModels.Roles 
                    { 
                        Id = "role_2", 
                        Key = "user",
                        Name = "User",
                        IsDefaultRole = true
                    }
                },
                NextToken = "next_page_token"
            };
            mockHandler.AddKiotaResponse("GET", "/api/v1/roles", kiotaResponse);

            var api = CreateApi((client, config) => new RolesApi(client, config));

            var response = await api.GetRolesAsync();

            Assert.NotNull(response);
            Assert.Equal("200", response.Code);
            Assert.NotNull(response.Roles);
            Assert.Equal(2, response.Roles.Count);
            Assert.Equal("role_1", response.Roles[0].Id);
            Assert.Equal("admin", response.Roles[0].Key);
            Assert.False(response.Roles[0].IsDefaultRole);
            Assert.True(response.Roles[1].IsDefaultRole);
            Assert.Equal("next_page_token", response.NextToken);
            
            _output.WriteLine($"Mock test successful - Retrieved {response.Roles.Count} roles");
        }

        [Fact]
        [Trait("TestMode", "Mock")]
        public async Task GetRoles_Mock_IsDefaultRole_False()
        {
            if (UseRealApi) return;

            var mockHandler = GetKiotaMockHandler();
            if (mockHandler == null) return;

            var kiotaResponse = new KiotaModels.Get_roles_response
            {
                Code = "200",
                Roles = new List<KiotaModels.Roles>
                {
                    new KiotaModels.Roles 
                    { 
                        Id = "role_not_default", 
                        Key = "custom",
                        Name = "Custom Role",
                        IsDefaultRole = false
                    }
                }
            };
            mockHandler.AddKiotaResponse("GET", "/api/v1/roles", kiotaResponse);

            var api = CreateApi((client, config) => new RolesApi(client, config));

            var response = await api.GetRolesAsync();

            Assert.NotNull(response);
            Assert.False(response.Roles[0].IsDefaultRole, "IsDefaultRole should be false!");
            _output.WriteLine("PASS: IsDefaultRole=false was preserved correctly");
        }

        [Fact]
        [Trait("TestMode", "Real")]
        public async Task GetRoles_Real_Test()
        {
            if (!UseRealApi) return;

            var api = CreateApi((client, config) => new RolesApi(client, config));
            var response = await api.GetRolesAsync();

            Assert.NotNull(response);
            _output.WriteLine($"Real API returned {response.Roles?.Count ?? 0} roles");
        }

        #endregion

        #region CreateRole Tests

        [Fact]
        [Trait("TestMode", "Mock")]
        public async Task CreateRole_Mock_Created()
        {
            if (UseRealApi) return;

            var mockHandler = GetKiotaMockHandler();
            if (mockHandler == null) return;

            var kiotaResponse = new KiotaModels.Create_roles_response
            {
                Code = "201",
                Message = "Role created"
            };
            mockHandler.AddKiotaResponse("POST", "/api/v1/roles", kiotaResponse);

            var api = CreateApi((client, config) => new RolesApi(client, config));
            var request = new CreateRoleRequest
            {
                Name = "New Role",
                Key = "new_role"
            };

            var response = await api.CreateRoleAsync(request);

            Assert.NotNull(response);
            Assert.Equal("201", response.Code);
            Assert.True(mockHandler.WasRequestMade("POST", "/api/v1/roles"));
            _output.WriteLine("CreateRole completed successfully");
        }

        #endregion

        #region DeleteRole Tests

        [Fact]
        [Trait("TestMode", "Mock")]
        public async Task DeleteRole_Mock_Deleted()
        {
            if (UseRealApi) return;

            var mockHandler = GetKiotaMockHandler();
            if (mockHandler == null) return;

            var kiotaResponse = new KiotaModels.Success_response
            {
                Code = "200",
                Message = "Role deleted"
            };
            mockHandler.AddKiotaResponse("DELETE", "/api/v1/roles/{role_id}", kiotaResponse);

            var api = CreateApi((client, config) => new RolesApi(client, config));

            var response = await api.DeleteRoleAsync("role_to_delete");

            Assert.NotNull(response);
            Assert.Equal("200", response.Code);
            _output.WriteLine("DeleteRole completed successfully");
        }

        #endregion

        #region Null Handling Tests

        [Fact]
        [Trait("TestMode", "Mock")]
        public async Task GetRoles_Mock_NullRolesList_StaysNull()
        {
            if (UseRealApi) return;

            var mockHandler = GetKiotaMockHandler();
            if (mockHandler == null) return;

            var kiotaResponse = new KiotaModels.Get_roles_response
            {
                Code = "200",
                Message = "Success",
                Roles = null
            };
            mockHandler.AddKiotaResponse("GET", "/api/v1/roles", kiotaResponse);

            var api = CreateApi((client, config) => new RolesApi(client, config));

            var response = await api.GetRolesAsync();

            Assert.NotNull(response);
            Assert.Null(response.Roles);
            _output.WriteLine("Null roles list preserved correctly");
        }

        #endregion

        #region Error Handling Tests

        [Fact]
        [Trait("TestMode", "Mock")]
        public async Task DeleteRole_Mock_NotFound_ThrowsException()
        {
            if (UseRealApi) return;

            var mockHandler = GetKiotaMockHandler();
            if (mockHandler == null) return;

            mockHandler.AddErrorResponse("DELETE", "/api/v1/roles/{role_id}", 
                System.Net.HttpStatusCode.NotFound, "not_found", "Role not found");

            var api = CreateApi((client, config) => new RolesApi(client, config));

            var exception = await Assert.ThrowsAsync<ApiException>(() => api.DeleteRoleAsync("nonexistent"));
            Assert.Equal(404, exception.ErrorCode);
            _output.WriteLine("404 error handled correctly");
        }

        [Fact]
        [Trait("TestMode", "Mock")]
        public async Task CreateRole_Mock_Conflict_ThrowsException()
        {
            if (UseRealApi) return;

            var mockHandler = GetKiotaMockHandler();
            if (mockHandler == null) return;

            mockHandler.AddErrorResponse("POST", "/api/v1/roles", 
                System.Net.HttpStatusCode.Conflict, "conflict", "Role with this key already exists");

            var api = CreateApi((client, config) => new RolesApi(client, config));
            var request = new CreateRoleRequest { Name = "Duplicate", Key = "duplicate" };

            var exception = await Assert.ThrowsAsync<ApiException>(() => api.CreateRoleAsync(request));
            Assert.Equal(409, exception.ErrorCode);
            _output.WriteLine("409 error handled correctly");
        }

        #endregion
    }
}
