using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using AutoMapper;
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
        private readonly IMapper _mapper;
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
        /// Verifies that Kiota models can be created and values accessed correctly.
        /// Note: Kiota models use BackingStore and standard JsonSerializer doesn't work with them.
        /// This test creates Kiota models directly and verifies property access.
        /// </summary>
        [Fact]
        public void KiotaModel_DirectCreation_WorksCorrectly()
        {
            // Create Kiota model directly (they use BackingStore internally)
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
        /// Verifies that nested Kiota models map correctly to OpenAPI models.
        /// Note: Kiota models use BackingStore and can't be deserialized with standard JsonSerializer.
        /// This test creates Kiota models directly and verifies AutoMapper mapping.
        /// </summary>
        [Fact]
        public void NestedKiotaModels_MapToOpenApi_Correctly()
        {
            // Create Kiota models directly (they use BackingStore internally)
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

            // Also verify AutoMapper mapping to OpenAPI model
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
    }
}
