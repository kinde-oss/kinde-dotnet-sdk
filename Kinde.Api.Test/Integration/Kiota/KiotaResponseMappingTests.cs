using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Kinde.Api.Mapping;
using Kinde.Api.Api;
using Kinde.Api.Client;
using Kinde.Api.Mappers;
using Kinde.Api.Model;
using Kinde.Api.Test.Integration.Mocks;
using Xunit;
using Xunit.Abstractions;
using KiotaManagementModels = Kinde.Api.Kiota.Management.Models;
using KiotaAccountsModels = Kinde.Api.Kiota.Accounts.Models;

namespace Kinde.Api.Test.Integration.Kiota
{
    /// <summary>
    /// Tests that verify end-to-end response flow through the Kiota integration.
    /// </summary>
    public class KiotaResponseMappingTests : IDisposable
    {
        private readonly ITestOutputHelper _output;
        private readonly IKindeMapper _mapper;
        private readonly KiotaMockHttpHandler _mockHandler;
        private readonly HttpClient _httpClient;
        private readonly Configuration _configuration;
        private readonly JsonSerializerOptions _snakeCaseOptions;

        public KiotaResponseMappingTests(ITestOutputHelper output)
        {
            _output = output;
            _mapper = KindeMapperConfiguration.Mapper;
            _mockHandler = new KiotaMockHttpHandler();
            _httpClient = new HttpClient(_mockHandler)
            {
                BaseAddress = new Uri("https://mock.kinde.com")
            };
            _configuration = new Configuration
            {
                BasePath = "https://mock.kinde.com",
                AccessToken = "test-access-token"
            };
            
            _snakeCaseOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower,
                WriteIndented = true
            };
        }

        public void Dispose()
        {
            _httpClient?.Dispose();
            _mockHandler?.Dispose();
        }

        #region JSON Deserialization Tests

        /// <summary>
        /// Verifies that Kiota models can be created and read back. Models are built directly because
        /// BackingStore models do not work with the standard JsonSerializer.
        /// </summary>
        [Fact]
        public void KiotaModel_DirectCreation_WorksCorrectly()
        {
            var kiotaModel = new KiotaManagementModels.User
            {
                Id = "user_123",
                FirstName = "John",
                LastName = "Doe",
                IsSuspended = false,
                TotalSignIns = 42
            };

            Assert.NotNull(kiotaModel);
            Assert.Equal("user_123", kiotaModel.Id);
            Assert.Equal("John", kiotaModel.FirstName);
            Assert.Equal("Doe", kiotaModel.LastName);
            Assert.False(kiotaModel.IsSuspended);
            Assert.Equal(42, kiotaModel.TotalSignIns);
            _output.WriteLine("Kiota model created and accessed correctly");
        }

        /// <summary>
        /// Verifies that nested Kiota models map correctly to OpenAPI models. Models are built directly because
        /// BackingStore models cannot be deserialized with the standard JsonSerializer.
        /// </summary>
        [Fact]
        public void NestedKiotaModels_MapToOpenApi_Correctly()
        {
            var kiotaUser1 = new KiotaManagementModels.Users_response_users
            {
                Id = "user_1",
                FirstName = "John",
                LastName = "Doe"
            };
            var kiotaUser2 = new KiotaManagementModels.Users_response_users
            {
                Id = "user_2",
                FirstName = "Jane",
                LastName = "Smith"
            };
            var kiotaModel = new KiotaManagementModels.Users_response
            {
                Code = "200",
                Message = "Success",
                Users = new List<KiotaManagementModels.Users_response_users> { kiotaUser1, kiotaUser2 },
                NextToken = "token_abc123"
            };

            Assert.NotNull(kiotaModel);
            Assert.Equal("200", kiotaModel.Code);
            Assert.NotNull(kiotaModel.Users);
            Assert.Equal(2, kiotaModel.Users.Count);
            Assert.Equal("user_1", kiotaModel.Users[0].Id);
            Assert.Equal("John", kiotaModel.Users[0].FirstName);
            Assert.Equal("token_abc123", kiotaModel.NextToken);

            var openApiModel = _mapper.Map<UsersResponse>(kiotaModel);
            Assert.NotNull(openApiModel);
            Assert.Equal("200", openApiModel.Code);
        }

        /// <summary>
        /// Verifies that boolean values in snake_case JSON are handled correctly.
        /// </summary>
        [Theory]
        [InlineData("true", true)]
        [InlineData("false", false)]
        public void SnakeCaseJson_BooleanValues_DeserializeCorrectly(string jsonBool, bool expected)
        {
            var json = $@"{{
                ""created"": {jsonBool},
                ""id"": ""user_bool_test""
            }}";

            var kiotaModel = JsonSerializer.Deserialize<KiotaManagementModels.Create_user_response>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            Assert.NotNull(kiotaModel);
            Assert.Equal(expected, kiotaModel.Created);
            _output.WriteLine($"Boolean value '{jsonBool}' deserialized to {expected}");
        }

        /// <summary>
        /// Verifies null handling in snake_case JSON.
        /// </summary>
        [Fact]
        public void SnakeCaseJson_NullValues_DeserializeAsNull()
        {
            var json = @"{
                ""id"": ""user_null_test"",
                ""first_name"": null
            }";

            var kiotaModel = JsonSerializer.Deserialize<KiotaManagementModels.User>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            Assert.NotNull(kiotaModel);
            Assert.Equal("user_null_test", kiotaModel.Id);
            Assert.Null(kiotaModel.FirstName);
        }

        #endregion

        #region End-to-End Response Flow Tests

        /// <summary>
        /// Tests the complete flow for GetUsers.
        /// </summary>
        [Fact]
        public async Task GetUsers_EndToEndFlow_ReturnsCorrectData()
        {
            var kiotaResponse = new KiotaManagementModels.Users_response
            {
                Code = "200",
                Message = "Users retrieved successfully",
                Users = new List<KiotaManagementModels.Users_response_users>
                {
                    new KiotaManagementModels.Users_response_users { Id = "user_1", FirstName = "John", LastName = "Doe" },
                    new KiotaManagementModels.Users_response_users { Id = "user_2", FirstName = "Jane", LastName = "Smith" }
                },
                NextToken = "next_page_token"
            };
            
            _mockHandler.AddKiotaResponse("GET", "/api/v1/users", kiotaResponse);

            var api = new UsersApi(_httpClient, _configuration);

            var response = await api.GetUsersAsync();

            Assert.NotNull(response);
            Assert.Equal("200", response.Code);
            Assert.Equal("Users retrieved successfully", response.Message);
            Assert.NotNull(response.Users);
            Assert.Equal(2, response.Users.Count);
            Assert.Equal("user_1", response.Users[0].Id);
            Assert.Equal("John", response.Users[0].FirstName);
            Assert.Equal("next_page_token", response.NextToken);
            
            _output.WriteLine("End-to-end GetUsers flow completed successfully");
        }

        /// <summary>
        /// Tests CreateUser flow with request body mapping.
        /// </summary>
        [Fact]
        public async Task CreateUser_EndToEndFlow_MapsRequestAndResponse()
        {
            var kiotaResponse = new KiotaManagementModels.Create_user_response
            {
                Created = true,
                Id = "user_new_123"
            };
            
            _mockHandler.AddKiotaResponse("POST", "/api/v1/user", kiotaResponse);

            var api = new UsersApi(_httpClient, _configuration);

            var request = new CreateUserRequest
            {
                Profile = new CreateUserRequestProfile
                {
                    GivenName = "Test",
                    FamilyName = "User"
                },
                OrganizationCode = "org_123"
            };

            var response = await api.CreateUserAsync(request);

            Assert.NotNull(response);
            Assert.True(response.Created);
            Assert.Equal("user_new_123", response.Id);
            Assert.True(_mockHandler.WasRequestMade("POST", "/api/v1/user"));
            
            _output.WriteLine("End-to-end CreateUser flow completed successfully");
        }

        /// <summary>
        /// Tests that false boolean values flow correctly through the entire pipeline.
        /// </summary>
        [Fact]
        public async Task CreateUser_Created_False_FlowsCorrectly()
        {
            var kiotaResponse = new KiotaManagementModels.Create_user_response
            {
                Created = false,
                Id = "user_not_created"
            };
            
            _mockHandler.AddKiotaResponse("POST", "/api/v1/user", kiotaResponse);

            var api = new UsersApi(_httpClient, _configuration);
            var request = new CreateUserRequest();

            var response = await api.CreateUserAsync(request);

            Assert.NotNull(response);
            Assert.False(response.Created, "Created should be false, not defaulting to true");
            _output.WriteLine("PASS: Created=false flowed correctly through the pipeline");
        }

        #endregion

        #region Error Response Mapping Tests

        /// <summary>
        /// Tests that error responses are handled correctly.
        /// </summary>
        [Fact]
        public async Task ErrorResponse_400_HandledCorrectly()
        {
            _mockHandler.AddErrorResponse("POST", "/api/v1/user", HttpStatusCode.BadRequest, "invalid_request", "Missing required field");

            var api = new UsersApi(_httpClient, _configuration);
            var request = new CreateUserRequest();

            var exception = await Assert.ThrowsAsync<ApiException>(() => api.CreateUserAsync(request));
            Assert.Equal(400, exception.ErrorCode);
            _output.WriteLine($"Error handled correctly: {exception.Message}");
        }

        /// <summary>
        /// Tests that 404 responses are handled correctly.
        /// </summary>
        [Fact]
        public async Task ErrorResponse_404_HandledCorrectly()
        {
            _mockHandler.AddErrorResponse("GET", "/api/v1/user", HttpStatusCode.NotFound, "not_found", "User not found");

            var api = new UsersApi(_httpClient, _configuration);

            var exception = await Assert.ThrowsAsync<ApiException>(() => api.GetUserDataAsync("nonexistent_id"));
            Assert.Equal(404, exception.ErrorCode);
        }

        #endregion

        #region Organizations API Tests

        /// <summary>
        /// Tests GetOrganizations end-to-end flow.
        /// </summary>
        [Fact]
        public async Task GetOrganizations_EndToEndFlow_ReturnsCorrectData()
        {
            var kiotaResponse = new KiotaManagementModels.Get_organizations_response
            {
                Code = "200",
                Message = "Organizations retrieved",
                Organizations = new List<KiotaManagementModels.Organization_item_schema>
                {
                    new KiotaManagementModels.Organization_item_schema 
                    { 
                        Code = "org_1", 
                        Name = "Test Org 1",
                        Handle = "test-org-1"
                    },
                    new KiotaManagementModels.Organization_item_schema 
                    { 
                        Code = "org_2", 
                        Name = "Test Org 2",
                        Handle = "test-org-2"
                    }
                }
            };
            
            _mockHandler.AddKiotaResponse("GET", "/api/v1/organizations", kiotaResponse);

            var api = new OrganizationsApi(_httpClient, _configuration);

            var response = await api.GetOrganizationsAsync();

            Assert.NotNull(response);
            Assert.Equal("200", response.Code);
            Assert.NotNull(response.Organizations);
            Assert.Equal(2, response.Organizations.Count);
            Assert.Equal("org_1", response.Organizations[0].Code);
            Assert.Equal("Test Org 1", response.Organizations[0].Name);
            
            _output.WriteLine("End-to-end GetOrganizations flow completed successfully");
        }

        #endregion

        #region Roles API Tests

        /// <summary>
        /// Tests GetRoles end-to-end flow.
        /// </summary>
        [Fact]
        public async Task GetRoles_EndToEndFlow_ReturnsCorrectData()
        {
            var kiotaResponse = new KiotaManagementModels.Get_roles_response
            {
                Code = "200",
                Message = "Roles retrieved",
                Roles = new List<KiotaManagementModels.Roles>
                {
                    new KiotaManagementModels.Roles { Id = "role_1", Key = "admin", Name = "Administrator" },
                    new KiotaManagementModels.Roles { Id = "role_2", Key = "user", Name = "User" }
                }
            };
            
            _mockHandler.AddKiotaResponse("GET", "/api/v1/roles", kiotaResponse);

            var api = new RolesApi(_httpClient, _configuration);

            var response = await api.GetRolesAsync();

            Assert.NotNull(response);
            Assert.Equal("200", response.Code);
            Assert.NotNull(response.Roles);
            Assert.Equal(2, response.Roles.Count);
            Assert.Equal("admin", response.Roles[0].Key);
            
            _output.WriteLine("End-to-end GetRoles flow completed successfully");
        }

        #endregion

        #region Applications API Tests

        /// <summary>
        /// Tests GetApplications end-to-end flow.
        /// </summary>
        [Fact]
        public async Task GetApplications_EndToEndFlow_ReturnsCorrectData()
        {
            var kiotaResponse = new KiotaManagementModels.Get_applications_response
            {
                Code = "200",
                Message = "Applications retrieved",
                Applications = new List<KiotaManagementModels.Applications>
                {
                    new KiotaManagementModels.Applications { Id = "app_1", Name = "My App 1" },
                    new KiotaManagementModels.Applications { Id = "app_2", Name = "My App 2" }
                }
            };
            
            _mockHandler.AddKiotaResponse("GET", "/api/v1/applications", kiotaResponse);

            var api = new ApplicationsApi(_httpClient, _configuration);

            var response = await api.GetApplicationsAsync();

            Assert.NotNull(response);
            Assert.Equal("200", response.Code);
            Assert.NotNull(response.Applications);
            Assert.Equal(2, response.Applications.Count);
            
            _output.WriteLine("End-to-end GetApplications flow completed successfully");
        }

        #endregion

        #region Connections API Tests

        // List endpoints return flat connection items, not the wrapped
        // { code, message, connection } shape used by GET /connections/{id}.
        private const string ConnectionsListJson = @"{
  ""code"": ""OK"",
  ""message"": ""Success"",
  ""has_more"": false,
  ""connections"": [
    {
      ""id"": ""conn_01a10fb73a6386b8ae26be602f0aac1b"",
      ""name"": ""Company A account"",
      ""strategy"": ""oauth2:azure_ad"",
      ""display_name"": ""Company A account""
    }
  ]
}";

        private static void AssertConnectionsListMapped(GetConnectionsResponse response)
        {
            Assert.NotNull(response);
            Assert.Equal("OK", response.Code);
            Assert.False(response.HasMore);
            var connection = Assert.Single(response.Connections);
            Assert.NotNull(connection.VarConnection);
            Assert.Equal("conn_01a10fb73a6386b8ae26be602f0aac1b", connection.VarConnection.Id);
            Assert.Equal("Company A account", connection.VarConnection.Name);
            Assert.Equal("Company A account", connection.VarConnection.DisplayName);
            Assert.Equal("oauth2:azure_ad", connection.VarConnection.Strategy);
        }

        [Fact]
        public async Task GetOrganizationConnections_FlatListItems_MapToVarConnection()
        {
            _mockHandler.AddRawJsonResponse("GET", "/api/v1/organizations/org_123/connections", ConnectionsListJson);

            var response = await new OrganizationsApi(_httpClient, _configuration).GetOrganizationConnectionsAsync("org_123");

            AssertConnectionsListMapped(response);
        }

        [Fact]
        public async Task GetConnections_FlatListItems_MapToVarConnection()
        {
            _mockHandler.AddRawJsonResponse("GET", "/api/v1/connections", ConnectionsListJson);

            var response = await new ConnectionsApi(_httpClient, _configuration).GetConnectionsAsync();

            AssertConnectionsListMapped(response);
        }

        [Fact]
        public async Task GetApplicationConnections_FlatListItems_MapToVarConnection()
        {
            _mockHandler.AddRawJsonResponse("GET", "/api/v1/applications/app_123/connections", ConnectionsListJson);

            var response = await new ApplicationsApi(_httpClient, _configuration).GetApplicationConnectionsAsync("app_123");

            AssertConnectionsListMapped(response);
        }

        [Fact]
        public async Task GetConnection_WrappedConnection_MapsToVarConnection()
        {
            const string json = @"{
  ""code"": ""OK"",
  ""message"": ""Success"",
  ""connection"": {
    ""id"": ""conn_01a10fb73a6386b8ae26be602f0aac1b"",
    ""name"": ""Company A account"",
    ""strategy"": ""oauth2:azure_ad"",
    ""display_name"": ""Company A account""
  }
}";
            _mockHandler.AddRawJsonResponse("GET", "/api/v1/connections/conn_01a10fb73a6386b8ae26be602f0aac1b", json);

            var response = await new ConnectionsApi(_httpClient, _configuration).GetConnectionAsync("conn_01a10fb73a6386b8ae26be602f0aac1b");

            Assert.NotNull(response);
            Assert.Equal("OK", response.Code);
            Assert.Equal("Success", response.Message);
            Assert.NotNull(response.VarConnection);
            Assert.Equal("conn_01a10fb73a6386b8ae26be602f0aac1b", response.VarConnection.Id);
            Assert.Equal("Company A account", response.VarConnection.Name);
            Assert.Equal("Company A account", response.VarConnection.DisplayName);
            Assert.Equal("oauth2:azure_ad", response.VarConnection.Strategy);
        }

        #endregion

        #region Newer management operations

        private const string SuccessJson = """{ "code": "OK", "message": "Success" }""";

        private CapturedRequest SingleRequest() => Assert.Single(_mockHandler.CapturedRequests);

        private static string UnescapedPathAndQuery(CapturedRequest request) => Uri.UnescapeDataString(request.Path);

        [Fact]
        public async Task GetOrganizationRoles_SendsSortAndPaging_AndMapsRoles()
        {
            _mockHandler.AddRawJsonResponse("GET", "/api/v1/organizations/org_123/roles", """
                {
                  "code": "OK", "message": "Success", "has_more": true, "next_token": "next_1",
                  "roles": [
                    {
                      "id": "role_1", "key": "billing_approver", "name": "Billing approver",
                      "description": "Can approve invoices.", "created_on": "2025-01-02T03:04:05Z",
                      "permissions": [ { "id": "perm_1", "key": "approve:invoices", "name": "Approve invoices" } ]
                    }
                  ]
                }
                """);

            var response = await new OrganizationsApi(_httpClient, _configuration)
                .GetOrganizationRolesAsync("org_123", sort: "name_asc", pageSize: 5, nextToken: "tok_1");

            var request = SingleRequest();
            var url = UnescapedPathAndQuery(request);
            Assert.Equal("GET", request.Method);
            Assert.StartsWith("/api/v1/organizations/org_123/roles?", url);
            Assert.Contains("sort=name_asc", url);
            Assert.Contains("page_size=5", url);
            Assert.Contains("next_token=tok_1", url);

            Assert.Equal("OK", response.Code);
            Assert.True(response.HasMore);
            Assert.Equal("next_1", response.NextToken);
            var role = Assert.Single(response.Roles);
            Assert.Equal("role_1", role.Id);
            Assert.Equal("billing_approver", role.Key);
            Assert.Equal("Billing approver", role.Name);
            Assert.Equal("Can approve invoices.", role.Description);
            Assert.Equal(new DateTimeOffset(2025, 1, 2, 3, 4, 5, TimeSpan.Zero), role.CreatedOn);
            Assert.Equal("approve:invoices", Assert.Single(role.Permissions).Key);
        }

        [Fact]
        public async Task GetOrganizationPermissions_MapsPermissions()
        {
            _mockHandler.AddRawJsonResponse("GET", "/api/v1/organizations/org_123/permissions", """
                {
                  "code": "OK", "message": "Success", "has_more": false,
                  "permissions": [ { "id": "perm_1", "key": "approve:invoices", "name": "Approve invoices", "description": "Approve." } ]
                }
                """);

            var response = await new OrganizationsApi(_httpClient, _configuration).GetOrganizationPermissionsAsync("org_123");

            var permission = Assert.Single(response.Permissions);
            Assert.Equal("perm_1", permission.Id);
            Assert.Equal("approve:invoices", permission.Key);
            Assert.Equal("Approve invoices", permission.Name);
            Assert.Equal("Approve.", permission.Description);
        }

        [Fact]
        public async Task CreateOrganizationRole_SendsBody_AndMapsResponse()
        {
            _mockHandler.AddRawJsonResponse(
                "POST",
                "/api/v1/organizations/org_123/roles",
                """{ "code": "ROLE_CREATED", "message": "Role created", "role": { "id": "role_9" } }""",
                HttpStatusCode.Created);

            var response = await new OrganizationsApi(_httpClient, _configuration).CreateOrganizationRoleAsync(
                "org_123",
                new CreateOrganizationRoleRequest(name: "Billing approver", description: "Can approve invoices.", key: "billing_approver"));

            var request = SingleRequest();
            Assert.Equal("POST", request.Method);
            using var body = JsonDocument.Parse(request.Body);
            Assert.Equal("billing_approver", body.RootElement.GetProperty("key").GetString());
            Assert.Equal("Billing approver", body.RootElement.GetProperty("name").GetString());
            Assert.Equal("ROLE_CREATED", response.Code);
            Assert.Equal("role_9", response.Role.Id);
        }

        [Fact]
        public async Task UpdateOrganizationPermission_SendsPatchBody()
        {
            _mockHandler.AddRawJsonResponse("PATCH", "/api/v1/organizations/org_123/permissions/perm_1", SuccessJson);

            var response = await new OrganizationsApi(_httpClient, _configuration).UpdateOrganizationPermissionAsync(
                "org_123", "perm_1", new UpdateOrganizationPermissionRequest { Name = "Approve all invoices" });

            var request = SingleRequest();
            Assert.Equal("PATCH", request.Method);
            using var body = JsonDocument.Parse(request.Body);
            Assert.Equal("Approve all invoices", body.RootElement.GetProperty("name").GetString());
            Assert.Equal("OK", response.Code);
        }

        [Fact]
        public async Task DeleteOrganizationRole_UsesBothPathSegments()
        {
            _mockHandler.AddRawJsonResponse("DELETE", "/api/v1/organizations/org_123/roles/role_1", SuccessJson);

            var response = await new OrganizationsApi(_httpClient, _configuration).DeleteOrganizationRoleAsync("org_123", "role_1");

            Assert.Equal("DELETE", SingleRequest().Method);
            Assert.Equal("Success", response.Message);
        }

        [Fact]
        public async Task GetOrganizationInvites_UsesTheSingularOrganizationPath()
        {
            _mockHandler.AddRawJsonResponse("GET", "/api/v1/organization/org_123/invites", """
                { "code": "OK", "message": "Success", "has_more": false, "invites": [] }
                """);

            var response = await new OrganizationsApi(_httpClient, _configuration)
                .GetOrganizationInvitesAsync("org_123", sort: "created_on_desc", includeRevoked: true);

            var url = UnescapedPathAndQuery(SingleRequest());
            Assert.StartsWith("/api/v1/organization/org_123/invites?", url);
            Assert.Contains("sort=created_on_desc", url);
            Assert.Contains("include_revoked=true", url);
            Assert.Equal("OK", response.Code);
        }

        [Fact]
        public async Task DeleteApplicationNativeApp_SendsTheIntegerIdAsAPathSegment()
        {
            _mockHandler.AddRawJsonResponse("DELETE", "/api/v1/applications/app_123/native_apps/42", SuccessJson);

            var response = await new ApplicationsApi(_httpClient, _configuration).DeleteApplicationNativeAppAsync("app_123", 42);

            Assert.Equal("DELETE", SingleRequest().Method);
            Assert.Equal("OK", response.Code);
        }

        [Fact]
        public async Task GetApplicationNativeApps_MapsTheInlineResponse()
        {
            _mockHandler.AddRawJsonResponse("GET", "/api/v1/applications/app_123/native_apps", """
                {
                  "code": "OK", "message": "Success",
                  "native_sign_in": { "is_enabled": true, "passkey_requirement": "device_bound", "is_passkey_mfa_exempt": true },
                  "native_apps": [ { "id": 7, "platform": "android", "package_name": "com.example.app", "sha256_cert_fingerprints": [ "AA:BB" ] } ]
                }
                """);

            var response = await new ApplicationsApi(_httpClient, _configuration).GetApplicationNativeAppsAsync("app_123");

            Assert.True(response.NativeSignIn.IsEnabled);
            Assert.True(response.NativeSignIn.IsPasskeyMfaExempt);
            Assert.Equal(GetApplicationNativeApps200ResponseNativeSignIn.PasskeyRequirementEnum.DeviceBound, response.NativeSignIn.PasskeyRequirement);
            var app = Assert.Single(response.NativeApps);
            Assert.Equal(7, app.Id);
            Assert.Equal(ApplicationNativeApp.PlatformEnum.Android, app.Platform);
            Assert.Equal("com.example.app", app.PackageName);
            Assert.Equal("AA:BB", Assert.Single(app.Sha256CertFingerprints));
        }

        [Fact]
        public async Task CreateApplicationNativeApp_SendsThePlatform_AndMapsTheCreatedApp()
        {
            _mockHandler.AddRawJsonResponse(
                "POST",
                "/api/v1/applications/app_123/native_apps",
                """
                { "code": "OK", "message": "Created", "native_app": { "id": 8, "platform": "ios", "team_id": "TEAM1", "bundle_id": "com.example.app" } }
                """,
                HttpStatusCode.Created);

            var response = await new ApplicationsApi(_httpClient, _configuration).CreateApplicationNativeAppAsync(
                "app_123",
                new CreateApplicationNativeAppRequest(CreateApplicationNativeAppRequest.PlatformEnum.Ios, teamId: "TEAM1", bundleId: "com.example.app"));

            using var body = JsonDocument.Parse(SingleRequest().Body);
            Assert.Equal("ios", body.RootElement.GetProperty("platform").GetString());
            Assert.Equal("TEAM1", body.RootElement.GetProperty("team_id").GetString());
            Assert.Equal(ApplicationNativeApp.PlatformEnum.Ios, response.NativeApp.Platform);
            Assert.Equal("com.example.app", response.NativeApp.BundleId);
        }

        [Fact]
        public async Task GetApplicationAccessRoles_MapsRoles()
        {
            _mockHandler.AddRawJsonResponse("GET", "/api/v1/applications/app_123/access_roles", """
                { "code": "OK", "message": "Success", "has_more": false, "roles": [ { "id": "r1", "key": "admin", "name": "Administrator" } ] }
                """);

            var response = await new ApplicationsApi(_httpClient, _configuration).GetApplicationAccessRolesAsync("app_123", pageSize: 10);

            Assert.Contains("page_size=10", UnescapedPathAndQuery(SingleRequest()));
            var role = Assert.Single(response.Roles);
            Assert.Equal("admin", role.Key);
            Assert.Equal("Administrator", role.Name);
        }

        [Fact]
        public async Task GetOrganizationMfa_MapsEnabledFactorsAndTheInlineFields()
        {
            _mockHandler.AddRawJsonResponse("GET", "/api/v1/organizations/org_123/mfa", """
                {
                  "code": "OK", "message": "Success", "is_mfa_enforced": true, "is_recovery_codes_enabled": true,
                  "enabled_factors": [ "mfa:email", "mfa:authenticator_app" ],
                  "exempt_enterprise_connections": [ "conn_1" ]
                }
                """);

            var response = await new OrganizationsApi(_httpClient, _configuration).GetOrganizationMFAAsync("org_123");

            Assert.True(response.IsMfaEnforced);
            Assert.True(response.IsRecoveryCodesEnabled);
            Assert.Equal(
                new[] { GetOrganizationMFA200Response.EnabledFactorsEnum.Email, GetOrganizationMFA200Response.EnabledFactorsEnum.AuthenticatorApp },
                response.EnabledFactors);
            Assert.Equal("conn_1", Assert.Single(response.ExemptEnterpriseConnections));
        }

        [Fact]
        public async Task GetBillingOneTimePurchases_NewApiClass_SendsQuery_AndMapsPurchases()
        {
            _mockHandler.AddRawJsonResponse("GET", "/api/v1/billing/one_time_purchases", """
                {
                  "code": "OK", "message": "Success", "has_more": false,
                  "one_time_purchases": [
                    { "id": "otp_1", "agreement_id": "agr_1", "feature_code": "seats", "feature_name": "Seats", "units_granted": 10,
                      "amount": 12.5, "currency_code": "usd", "purchased_on": "2025-01-02T03:04:05Z", "expires_on": null,
                      "is_metered": true, "expiration_policy_code": "end_of_current_billing_cycle" }
                  ],
                  "feature_totals": []
                }
                """);

            var response = await new BillingOneTimePurchasesApi(_httpClient, _configuration)
                .GetBillingOneTimePurchasesAsync("cust_1", pageSize: 2, featureTotals: true);

            var url = UnescapedPathAndQuery(SingleRequest());
            Assert.StartsWith("/api/v1/billing/one_time_purchases?", url);
            Assert.Contains("customer_id=cust_1", url);
            Assert.Contains("page_size=2", url);
            Assert.Contains("feature_totals=true", url);

            var purchase = Assert.Single(response.OneTimePurchases);
            Assert.Equal("otp_1", purchase.Id);
            Assert.Equal(10, purchase.UnitsGranted);
            Assert.Equal(12.5m, purchase.Amount);
            Assert.Equal(new DateTimeOffset(2025, 1, 2, 3, 4, 5, TimeSpan.Zero), purchase.PurchasedOn);
            Assert.Null(purchase.ExpiresOn);
            Assert.True(purchase.IsMetered);
            Assert.Equal(
                GetBillingOneTimePurchasesResponseOneTimePurchasesInner.ExpirationPolicyCodeEnum.EndOfCurrentBillingCycle,
                purchase.ExpirationPolicyCode);
        }

        [Fact]
        public async Task GetBillingMeterUsage_SendsRequiredQuery()
        {
            _mockHandler.AddRawJsonResponse("GET", "/api/v1/billing/meter_usage", """
                { "code": "OK", "message": "Success", "has_more": false, "meter_usages": [] }
                """);

            var response = await new BillingMeterUsageApi(_httpClient, _configuration)
                .GetBillingMeterUsageAsync("agr_1", "seats", allCycles: true);

            var url = UnescapedPathAndQuery(SingleRequest());
            Assert.Contains("customer_agreement_id=agr_1", url);
            Assert.Contains("feature_code=seats", url);
            Assert.Contains("all_cycles=true", url);
            Assert.Equal("OK", response.Code);
        }

        [Fact]
        public async Task DeleteBillingAgreement_SendsTheCancellationChoice()
        {
            _mockHandler.AddRawJsonResponse("DELETE", "/api/v1/billing/agreements/agr_1", """
                { "code": "OK", "message": "Cancellation scheduled", "cancels_on": "2025-02-01T00:00:00Z" }
                """);

            var response = await new BillingAgreementsApi(_httpClient, _configuration).DeleteBillingAgreementAsync("agr_1", cancelAt: "immediately");

            Assert.Contains("cancel_at=immediately", UnescapedPathAndQuery(SingleRequest()));
            Assert.Equal(new DateTimeOffset(2025, 2, 1, 0, 0, 0, TimeSpan.Zero), response.CancelsOn);
        }

        [Fact]
        public async Task GetUsers_SendsPhoneAndActiveSince()
        {
            _mockHandler.AddRawJsonResponse("GET", "/api/v1/users", """{ "code": "OK", "message": "Success", "users": [] }""");

            await new UsersApi(_httpClient, _configuration).GetUsersAsync(
                phone: "+15551234567", activeSince: new DateTimeOffset(2025, 1, 2, 3, 4, 5, TimeSpan.Zero));

            var url = UnescapedPathAndQuery(SingleRequest());
            Assert.Contains("phone=+15551234567", url);
            Assert.Contains("active_since=2025-01-02T03:04:05", url);
        }

        [Fact]
        public async Task GetUsers_KeepsEveryExistingPositionalArgumentWorking()
        {
            _mockHandler.AddRawJsonResponse("GET", "/api/v1/users", """{ "code": "OK", "message": "Success", "users": [] }""");

            await new UsersApi(_httpClient, _configuration).GetUsersAsync(10, "kp_1", "tok", "a@b.com", "ann", "organizations", true);

            var url = UnescapedPathAndQuery(SingleRequest());
            Assert.Contains("expand=organizations", url);
            Assert.Contains("has_organization=true", url);
            Assert.DoesNotContain("phone=", url);
        }

        [Fact]
        public async Task SearchUsers_SendsApiScopes()
        {
            _mockHandler.AddRawJsonResponse("GET", "/api/v1/search/users", """{ "code": "OK", "message": "Success", "results": [] }""");

            await new SearchApi(_httpClient, _configuration).SearchUsersAsync(query: "ann", apiScopes: "read:users");

            var url = UnescapedPathAndQuery(SingleRequest());
            Assert.Contains("query=ann", url);
            Assert.Contains("api_scopes=read:users", url);
        }

        [Fact]
        public async Task GetOrganization_SendsExpand()
        {
            _mockHandler.AddRawJsonResponse("GET", "/api/v1/organization", SuccessJson);

            await new OrganizationsApi(_httpClient, _configuration).GetOrganizationAsync("org_123", expand: "billing");

            var url = UnescapedPathAndQuery(SingleRequest());
            Assert.Contains("code=org_123", url);
            Assert.Contains("expand=billing", url);
        }

        [Fact]
        public async Task GetOrganization_WithoutCode_FailsBeforeCallingTheApi()
        {
            var exception = await Assert.ThrowsAsync<ApiException>(
                () => new OrganizationsApi(_httpClient, _configuration).GetOrganizationAsync(null));

            Assert.Equal(400, exception.ErrorCode);
            Assert.Empty(_mockHandler.CapturedRequests);
        }

        [Fact]
        public async Task DeleteAPIApplicationScope_AndItsMisspelledLegacyName_CallTheSameRoute()
        {
            const string path = "/api/v1/apis/api_1/applications/app_1/scopes/scope_1";
            _mockHandler.AddEmptyResponse("DELETE", path, HttpStatusCode.OK);
            var api = new APIsApi(_httpClient, _configuration);

            await api.DeleteAPIApplicationScopeAsync("api_1", "app_1", "scope_1");
#pragma warning disable CS0618
            await api.DeleteAPIAppliationScopeAsync("api_1", "app_1", "scope_1");
#pragma warning restore CS0618

            Assert.Equal(2, _mockHandler.CapturedRequests.Count);
            Assert.All(_mockHandler.CapturedRequests, request =>
            {
                Assert.Equal("DELETE", request.Method);
                Assert.Equal(path, request.Path);
            });
        }

        [Fact]
        public async Task GetIdentities_SendsFilters_AndMapsIdentities()
        {
            _mockHandler.AddRawJsonResponse("GET", "/api/v1/identities", """
                {
                  "code": "OK", "message": "Success",
                  "identities": [
                    { "id": "idn_1", "type": "email", "is_confirmed": true, "created_on": "2025-01-02T03:04:05Z",
                      "last_login_on": "2025-01-03T03:04:05Z", "total_logins": 3, "name": "a@b.com", "email": "a@b.com",
                      "connection_id": "conn_1", "is_primary": true, "user_id": "kp_1" }
                  ]
                }
                """);

            var response = await new IdentitiesApi(_httpClient, _configuration).GetIdentitiesAsync(email: "a@b.com", connectionId: "conn_1");

            var url = UnescapedPathAndQuery(SingleRequest());
            Assert.Contains("email=a@b.com", url);
            Assert.Contains("connection_id=conn_1", url);
            var identity = Assert.Single(response.Identities);
            Assert.Equal("idn_1", identity.Id);
            Assert.Equal("email", identity.Type);
            Assert.True(identity.IsConfirmed);
            Assert.Equal(3, identity.TotalLogins);
            Assert.Equal("kp_1", identity.UserId);
        }

        [Fact]
        public async Task CreateUserBillingCustomer_SendsBody_AndMapsTheNestedCustomer()
        {
            _mockHandler.AddRawJsonResponse("POST", "/api/v1/users/kp_1/billing_customer", """
                {
                  "code": "OK", "message": "Success",
                  "billing_customer": { "id": "bc_1", "agreement_id": "agr_1", "plan_version": 2, "plan_code": "pro", "plan_name": "Pro" }
                }
                """);

            var response = await new UsersApi(_httpClient, _configuration).CreateUserBillingCustomerAsync(
                "kp_1", new CreateUserBillingCustomerRequest(orgCode: "org_123", billingEmail: "a@b.com", billingPlanCode: "pro"));

            using var body = JsonDocument.Parse(SingleRequest().Body);
            Assert.Equal("org_123", body.RootElement.GetProperty("org_code").GetString());
            Assert.Equal("pro", body.RootElement.GetProperty("billing_plan_code").GetString());
            Assert.Equal("bc_1", response.BillingCustomer.Id);
            Assert.Equal(2, response.BillingCustomer.PlanVersion);
            Assert.Equal("Pro", response.BillingCustomer.PlanName);
        }

        [Fact]
        public async Task RotateDirectorySecret_MapsTheDirectory()
        {
            _mockHandler.AddRawJsonResponse("POST", "/api/v1/directories/dir_1/rotate_secret", """
                {
                  "code": "DIRECTORY_SECRET_ROTATED", "message": "rotated",
                  "directory": { "id": "dir_1", "directory_name": "Okta", "secret_token": "new_secret", "created_on": "2025-01-02T03:04:05Z" }
                }
                """);

            var response = await new DirectoriesApi(_httpClient, _configuration).RotateDirectorySecretAsync("dir_1");

            Assert.Equal("POST", SingleRequest().Method);
            Assert.Equal("DIRECTORY_SECRET_ROTATED", response.Code);
            Assert.Equal("dir_1", response.Directory.Id);
            Assert.Equal("new_secret", response.Directory.SecretToken);
        }

        [Fact]
        public async Task GetApiKeys_SendsFilters()
        {
            _mockHandler.AddRawJsonResponse("GET", "/api/v1/api_keys", """{ "code": "OK", "message": "Success", "api_keys": [], "has_more": false }""");

            var keys = await new ApiKeysApi(_httpClient, _configuration).GetApiKeysAsync(keyType: "organization", status: "active");

            var url = UnescapedPathAndQuery(SingleRequest());
            Assert.Contains("key_type=organization", url);
            Assert.Contains("status=active", url);
            Assert.Equal("OK", keys.Code);
        }

        #endregion

        #region Free-form request objects

        [Fact]
        public async Task UpdateUserProperties_SendsEveryProperty()
        {
            _mockHandler.AddRawJsonResponse("PATCH", "/api/v1/users/kp_1/properties", SuccessJson);

            await new UsersApi(_httpClient, _configuration).UpdateUserPropertiesAsync(
                "kp_1",
                new UpdateOrganizationPropertiesRequest(new Dictionary<string, object>
                {
                    ["kp_usr_city"] = "Paris",
                    ["kp_usr_vip"] = true,
                    ["kp_usr_visits"] = 3,
                    ["kp_usr_unset"] = "",
                    ["kp_usr_null"] = null,
                }));

            using var body = JsonDocument.Parse(SingleRequest().Body);
            var properties = body.RootElement.GetProperty("properties");
            Assert.Equal("Paris", properties.GetProperty("kp_usr_city").GetString());
            Assert.True(properties.GetProperty("kp_usr_vip").GetBoolean());
            Assert.Equal(3, properties.GetProperty("kp_usr_visits").GetInt32());
            Assert.Equal(string.Empty, properties.GetProperty("kp_usr_unset").GetString());
            Assert.Equal(JsonValueKind.Null, properties.GetProperty("kp_usr_null").ValueKind);
        }

        [Fact]
        public async Task UpdateOrganizationProperties_SendsEveryProperty_FromAnAnonymousObject()
        {
            _mockHandler.AddRawJsonResponse("PATCH", "/api/v1/organizations/org_123/properties", SuccessJson);

            await new OrganizationsApi(_httpClient, _configuration).UpdateOrganizationPropertiesAsync(
                "org_123",
                new UpdateOrganizationPropertiesRequest(new { kp_org_industry = "retail", kp_org_beta = false }));

            using var body = JsonDocument.Parse(SingleRequest().Body);
            var properties = body.RootElement.GetProperty("properties");
            Assert.Equal("retail", properties.GetProperty("kp_org_industry").GetString());
            Assert.False(properties.GetProperty("kp_org_beta").GetBoolean());
        }

        [Fact]
        public async Task UpdateUserProperties_FromAJsonElement_SendsEveryProperty()
        {
            _mockHandler.AddRawJsonResponse("PATCH", "/api/v1/users/kp_1/properties", SuccessJson);
            using var document = JsonDocument.Parse("""{ "kp_usr_city": "Paris", "kp_usr_visits": 3 }""");

            await new UsersApi(_httpClient, _configuration).UpdateUserPropertiesAsync(
                "kp_1", new UpdateOrganizationPropertiesRequest(document.RootElement.Clone()));

            using var body = JsonDocument.Parse(SingleRequest().Body);
            var properties = body.RootElement.GetProperty("properties");
            Assert.Equal("Paris", properties.GetProperty("kp_usr_city").GetString());
            Assert.Equal(3, properties.GetProperty("kp_usr_visits").GetInt32());
        }

        [Fact]
        public async Task UpdateUserProperties_WithAValueThatIsNotAnObject_FailsInsteadOfSendingNothing()
        {
            await Assert.ThrowsAsync<KindeMappingException>(() =>
                new UsersApi(_httpClient, _configuration).UpdateUserPropertiesAsync(
                    "kp_1", new UpdateOrganizationPropertiesRequest("not an object")));

            Assert.Empty(_mockHandler.CapturedRequests);
        }

        [Fact]
        public async Task CreateConnection_SendsUpstreamParams()
        {
            _mockHandler.AddRawJsonResponse("POST", "/api/v1/connections", SuccessJson, HttpStatusCode.Created);

            await new ConnectionsApi(_httpClient, _configuration).CreateConnectionAsync(
                new CreateConnectionRequest(
                    name: "entra-id",
                    displayName: "Entra ID",
                    strategy: CreateConnectionRequest.StrategyEnum.Oauth2microsoft,
                    options: new CreateConnectionRequestOptions(new CreateConnectionRequestOptionsOneOf1
                    {
                        ClientId = "entra-client",
                        UpstreamParams = new Dictionary<string, object>
                        {
                            ["prompt"] = new Dictionary<string, object> { ["value"] = "select_account" },
                        },
                    })));

            using var body = JsonDocument.Parse(SingleRequest().Body);
            var prompt = body.RootElement.GetProperty("options").GetProperty("upstream_params").GetProperty("prompt");
            Assert.Equal("select_account", prompt.GetProperty("value").GetString());
        }

        [Fact]
        public async Task CreateOrganization_SendsFeatureFlags()
        {
            _mockHandler.AddRawJsonResponse(
                "POST", "/api/v1/organization", """{ "code": "ORGANIZATION_CREATED", "message": "Created" }""", HttpStatusCode.Created);

            await new OrganizationsApi(_httpClient, _configuration).CreateOrganizationAsync(
                new CreateOrganizationRequest(name: "Acme")
                {
                    FeatureFlags = new Dictionary<string, CreateOrganizationRequest.InnerEnum>
                    {
                        ["theme"] = CreateOrganizationRequest.InnerEnum.Str,
                        ["is_dark"] = CreateOrganizationRequest.InnerEnum.Bool,
                    },
                });

            using var body = JsonDocument.Parse(SingleRequest().Body);
            var flags = body.RootElement.GetProperty("feature_flags");
            Assert.Equal("str", flags.GetProperty("theme").GetString());
            Assert.Equal("bool", flags.GetProperty("is_dark").GetString());
        }

        #endregion
    }
}
