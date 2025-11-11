using System;
using System.Collections.Generic;
using System.Linq;
using Kinde.Api.Api;
using Kinde.Api.Client;
using Kinde.Api.Model;
using Newtonsoft.Json;
using Xunit;
using Xunit.Abstractions;

namespace Kinde.Api.Test.Integration
{
    /// <summary>
    /// Comprehensive integration tests for all Newtonsoft.Json converters
    /// Tests serialization/deserialization round-trips for all API responses
    /// </summary>
    [Collection("Integration Tests")]
    public class ConverterIntegrationTests : BaseIntegrationTest
    {
        private readonly ITestOutputHelper _output;

        public ConverterIntegrationTests(IntegrationTestFixture fixture, ITestOutputHelper output) 
            : base(fixture)
        {
            _output = output;
        }

        #region Read-Only Endpoint Tests

        [Fact]
        public async Task TestGetAPIs_Converter()
        {
            SkipIfNotConfigured();
            
            try
            {
                var api = UseMockMode && MockHttpClient != null
                    ? new APIsApi(MockHttpClient, ApiConfiguration)
                    : new APIsApi(ApiConfiguration);
                
                var result = await api.GetAPIsAsync();
                
                Assert.NotNull(result);
                
                // Show detailed output
                TestOutputHelper.WriteResponseDetails(_output, "GetAPIs", result);
                
                // Test serialization round-trip
                TestSerializationRoundTrip(result, "GetAPIs");
            }
            catch (Exception ex)
            {
                TestOutputHelper.WriteError(_output, "GetAPIs", ex);
                throw;
            }
        }

        [Fact]
        public async Task TestGetApplications_Converter()
        {
            SkipIfNotConfigured();
            
            try
            {
                var api = UseMockMode && MockHttpClient != null
                    ? new ApplicationsApi(MockHttpClient, ApiConfiguration)
                    : new ApplicationsApi(ApiConfiguration);
                
                var result = await api.GetApplicationsAsync();
                
                Assert.NotNull(result);
                
                // Show detailed output
                TestOutputHelper.WriteResponseDetails(_output, "GetApplications", result);
                
                // Test serialization round-trip
                TestSerializationRoundTrip(result, "GetApplications");
            }
            catch (Exception ex)
            {
                TestOutputHelper.WriteError(_output, "GetApplications", ex);
                throw;
            }
        }

        [Fact]
        public async Task TestGetRoles_Converter()
        {
            SkipIfNotConfigured();
            
            try
            {
                var api = UseMockMode && MockHttpClient != null
                    ? new RolesApi(MockHttpClient, ApiConfiguration)
                    : new RolesApi(ApiConfiguration);
                
                var result = await api.GetRolesAsync();
                
                Assert.NotNull(result);
                
                // Show detailed output
                TestOutputHelper.WriteResponseDetails(_output, "GetRoles", result);
                
                // Test serialization round-trip
                TestSerializationRoundTrip(result, "GetRoles");
            }
            catch (Exception ex)
            {
                TestOutputHelper.WriteError(_output, "GetRoles", ex);
                throw;
            }
        }

        [Fact]
        public async Task TestGetPermissions_Converter()
        {
            SkipIfNotConfigured();
            
            try
            {
                var api = UseMockMode && MockHttpClient != null
                    ? new PermissionsApi(MockHttpClient, ApiConfiguration)
                    : new PermissionsApi(ApiConfiguration);
                
                var result = await api.GetPermissionsAsync();
                
                Assert.NotNull(result);
                
                // Show detailed output
                TestOutputHelper.WriteResponseDetails(_output, "GetPermissions", result);
                
                // Test serialization round-trip
                TestSerializationRoundTrip(result, "GetPermissions");
            }
            catch (Exception ex)
            {
                TestOutputHelper.WriteError(_output, "GetPermissions", ex);
                throw;
            }
        }

        [Fact]
        public async Task TestGetProperties_Converter()
        {
            SkipIfNotConfigured();
            
            try
            {
                var api = UseMockMode && MockHttpClient != null
                    ? new PropertiesApi(MockHttpClient, ApiConfiguration)
                    : new PropertiesApi(ApiConfiguration);
                
                var result = await api.GetPropertiesAsync();
                
                Assert.NotNull(result);
                
                // Show detailed output
                TestOutputHelper.WriteResponseDetails(_output, "GetProperties", result);
                
                // Test serialization round-trip
                TestSerializationRoundTrip(result, "GetProperties");
            }
            catch (Exception ex)
            {
                TestOutputHelper.WriteError(_output, "GetProperties", ex);
                throw;
            }
        }

        [Fact]
        public async Task TestGetOrganizations_Converter()
        {
            SkipIfNotConfigured();
            
            try
            {
                var api = UseMockMode && MockHttpClient != null
                    ? new OrganizationsApi(MockHttpClient, ApiConfiguration)
                    : new OrganizationsApi(ApiConfiguration);
                
                var result = await api.GetOrganizationsAsync();
                
                Assert.NotNull(result);
                
                // Show detailed output
                TestOutputHelper.WriteResponseDetails(_output, "GetOrganizations", result);
                
                // Test serialization round-trip
                TestSerializationRoundTrip(result, "GetOrganizations");
            }
            catch (Exception ex)
            {
                TestOutputHelper.WriteError(_output, "GetOrganizations", ex);
                throw;
            }
        }

        [Fact]
        public async Task TestGetConnections_Converter()
        {
            SkipIfNotConfigured();
            
            try
            {
                var api = UseMockMode && MockHttpClient != null
                    ? new ConnectionsApi(MockHttpClient, ApiConfiguration)
                    : new ConnectionsApi(ApiConfiguration);
                
                var result = await api.GetConnectionsAsync();
                
                Assert.NotNull(result);
                
                // Show detailed output
                TestOutputHelper.WriteResponseDetails(_output, "GetConnections", result);
                
                // Test serialization round-trip
                TestSerializationRoundTrip(result, "GetConnections");
            }
            catch (Exception ex)
            {
                TestOutputHelper.WriteError(_output, "GetConnections", ex);
                throw;
            }
        }

        [Fact]
        public async Task TestGetEnvironment_Converter()
        {
            SkipIfNotConfigured();
            
            try
            {
                var api = UseMockMode && MockHttpClient != null
                    ? new EnvironmentsApi(MockHttpClient, ApiConfiguration)
                    : new EnvironmentsApi(ApiConfiguration);
                
                var result = await api.GetEnvironmentAsync();
                
                Assert.NotNull(result);
                
                // Show detailed output
                TestOutputHelper.WriteResponseDetails(_output, "GetEnvironment", result);
                
                // Test serialization round-trip
                TestSerializationRoundTrip(result, "GetEnvironment");
            }
            catch (Exception ex)
            {
                TestOutputHelper.WriteError(_output, "GetEnvironment", ex);
                throw;
            }
        }

        [Fact]
        public async Task TestGetEnvironmentVariables_Converter()
        {
            SkipIfNotConfigured();
            
            try
            {
                var api = UseMockMode && MockHttpClient != null
                    ? new EnvironmentVariablesApi(MockHttpClient, ApiConfiguration)
                    : new EnvironmentVariablesApi(ApiConfiguration);
                
                var result = await api.GetEnvironmentVariablesAsync();
                
                Assert.NotNull(result);
                
                // Show detailed output
                TestOutputHelper.WriteResponseDetails(_output, "GetEnvironmentVariables", result);
                
                // Test serialization round-trip
                TestSerializationRoundTrip(result, "GetEnvironmentVariables");
            }
            catch (Exception ex)
            {
                TestOutputHelper.WriteError(_output, "GetEnvironmentVariables", ex);
                throw;
            }
        }

        [Fact]
        public async Task TestGetBusiness_Converter()
        {
            SkipIfNotConfigured();
            
            try
            {
                var api = UseMockMode && MockHttpClient != null
                    ? new BusinessApi(MockHttpClient, ApiConfiguration)
                    : new BusinessApi(ApiConfiguration);
                
                var result = await api.GetBusinessAsync();
                
                Assert.NotNull(result);
                
                // Show detailed output
                TestOutputHelper.WriteResponseDetails(_output, "GetBusiness", result);
                
                // Test serialization round-trip
                TestSerializationRoundTrip(result, "GetBusiness");
            }
            catch (Exception ex)
            {
                TestOutputHelper.WriteError(_output, "GetBusiness", ex);
                throw;
            }
        }

        [Fact]
        public async Task TestGetIndustries_Converter()
        {
            SkipIfNotConfigured();
            
            try
            {
                var api = UseMockMode && MockHttpClient != null
                    ? new IndustriesApi(MockHttpClient, ApiConfiguration)
                    : new IndustriesApi(ApiConfiguration);
                
                var result = await api.GetIndustriesAsync();
                
                Assert.NotNull(result);
                
                // Show detailed output
                TestOutputHelper.WriteResponseDetails(_output, "GetIndustries", result);
                
                // Test serialization round-trip
                TestSerializationRoundTrip(result, "GetIndustries");
            }
            catch (Exception ex)
            {
                TestOutputHelper.WriteError(_output, "GetIndustries", ex);
                throw;
            }
        }

        [Fact]
        public async Task TestGetTimezones_Converter()
        {
            SkipIfNotConfigured();
            
            try
            {
                var api = UseMockMode && MockHttpClient != null
                    ? new TimezonesApi(MockHttpClient, ApiConfiguration)
                    : new TimezonesApi(ApiConfiguration);
                
                var result = await api.GetTimezonesAsync();
                
                Assert.NotNull(result);
                
                // Show detailed output
                TestOutputHelper.WriteResponseDetails(_output, "GetTimezones", result);
                
                // Test serialization round-trip
                TestSerializationRoundTrip(result, "GetTimezones");
            }
            catch (Exception ex)
            {
                TestOutputHelper.WriteError(_output, "GetTimezones", ex);
                throw;
            }
        }

        [Fact]
        public async Task TestGetCategories_Converter()
        {
            SkipIfNotConfigured();
            
            try
            {
                var api = UseMockMode && MockHttpClient != null
                    ? new PropertyCategoriesApi(MockHttpClient, ApiConfiguration)
                    : new PropertyCategoriesApi(ApiConfiguration);
                
                var result = await api.GetCategoriesAsync();
                
                Assert.NotNull(result);
                
                // Show detailed output
                TestOutputHelper.WriteResponseDetails(_output, "GetCategories", result);
                
                // Test serialization round-trip
                TestSerializationRoundTrip(result, "GetCategories");
            }
            catch (Exception ex)
            {
                TestOutputHelper.WriteError(_output, "GetCategories", ex);
                throw;
            }
        }

        [Fact]
        public async Task TestGetSubscribers_Converter()
        {
            SkipIfNotConfigured();
            
            try
            {
                var api = UseMockMode && MockHttpClient != null
                    ? new SubscribersApi(MockHttpClient, ApiConfiguration)
                    : new SubscribersApi(ApiConfiguration);
                
                var result = await api.GetSubscribersAsync();
                
                Assert.NotNull(result);
                
                // Show detailed output
                TestOutputHelper.WriteResponseDetails(_output, "GetSubscribers", result);
                
                // Test serialization round-trip
                TestSerializationRoundTrip(result, "GetSubscribers");
            }
            catch (Exception ex)
            {
                TestOutputHelper.WriteError(_output, "GetSubscribers", ex);
                throw;
            }
        }

        #endregion

        #region Parameterized Endpoint Tests

        [Fact]
        public async Task TestGetAPI_WithId_Converter()
        {
            SkipIfNotConfigured();
            
            try
            {
                var apisApi = UseMockMode && MockHttpClient != null
                    ? new APIsApi(MockHttpClient, ApiConfiguration)
                    : new APIsApi(ApiConfiguration);
                
                var apis = await apisApi.GetAPIsAsync();
                
                if (apis?.Apis != null && apis.Apis.Count > 0)
                {
                    var apiId = apis.Apis[0].Id;
                    var result = await apisApi.GetAPIAsync(apiId);
                    
                    Assert.NotNull(result);
                    TestSerializationRoundTrip(result, $"GetAPI-{apiId}");
                }
            }
            catch (Exception ex)
            {
                TestOutputHelper.WriteError(_output, "GetAPI_WithId", ex);
                throw;
            }
        }

        [Fact]
        public async Task TestGetAPIScopes_WithId_Converter()
        {
            SkipIfNotConfigured();
            
            try
            {
                var apisApi = UseMockMode && MockHttpClient != null
                    ? new APIsApi(MockHttpClient, ApiConfiguration)
                    : new APIsApi(ApiConfiguration);
                
                var apis = await apisApi.GetAPIsAsync();
                
                if (apis?.Apis != null && apis.Apis.Count > 0)
                {
                    var apiId = apis.Apis[0].Id;
                    var result = await apisApi.GetAPIScopesAsync(apiId);
                    
                    Assert.NotNull(result);
                    TestSerializationRoundTrip(result, $"GetAPIScopes-{apiId}");
                }
            }
            catch (Exception ex)
            {
                TestOutputHelper.WriteError(_output, "GetAPIScopes_WithId", ex);
                throw;
            }
        }

        [Fact]
        public async Task TestGetApplication_WithId_Converter()
        {
            SkipIfNotConfigured();
            
            try
            {
                var appsApi = UseMockMode && MockHttpClient != null
                    ? new ApplicationsApi(MockHttpClient, ApiConfiguration)
                    : new ApplicationsApi(ApiConfiguration);
                
                var applications = await appsApi.GetApplicationsAsync();
                
                if (applications?.Applications != null && applications.Applications.Count > 0)
                {
                    var appId = applications.Applications[0].Id;
                    var result = await appsApi.GetApplicationAsync(appId);
                    
                    Assert.NotNull(result);
                    TestSerializationRoundTrip(result, $"GetApplication-{appId}");
                }
            }
            catch (Exception ex)
            {
                TestOutputHelper.WriteError(_output, "GetApplication_WithId", ex);
                throw;
            }
        }

        [Fact]
        public async Task TestGetRole_WithId_Converter()
        {
            SkipIfNotConfigured();
            
            try
            {
                var rolesApi = UseMockMode && MockHttpClient != null
                    ? new RolesApi(MockHttpClient, ApiConfiguration)
                    : new RolesApi(ApiConfiguration);
                
                var roles = await rolesApi.GetRolesAsync();
                
                if (roles?.Roles != null && roles.Roles.Count > 0)
                {
                    var roleId = roles.Roles[0].Id;
                    var result = await rolesApi.GetRoleAsync(roleId);
                    
                    Assert.NotNull(result);
                    TestSerializationRoundTrip(result, $"GetRole-{roleId}");
                }
            }
            catch (Exception ex)
            {
                TestOutputHelper.WriteError(_output, "GetRole_WithId", ex);
                throw;
            }
        }

        [Fact]
        public async Task TestGetRoleScopes_WithId_Converter()
        {
            SkipIfNotConfigured();
            
            try
            {
                var rolesApi = UseMockMode && MockHttpClient != null
                    ? new RolesApi(MockHttpClient, ApiConfiguration)
                    : new RolesApi(ApiConfiguration);
                
                var roles = await rolesApi.GetRolesAsync();
                
                if (roles?.Roles != null && roles.Roles.Count > 0)
                {
                    var roleId = roles.Roles[0].Id;
                    var result = await rolesApi.GetRoleScopesAsync(roleId);
                    
                    Assert.NotNull(result);
                    TestSerializationRoundTrip(result, $"GetRoleScopes-{roleId}");
                }
            }
            catch (Exception ex)
            {
                TestOutputHelper.WriteError(_output, "GetRoleScopes_WithId", ex);
                throw;
            }
        }

        [Fact]
        public async Task TestGetRolePermissions_WithId_Converter()
        {
            SkipIfNotConfigured();
            
            try
            {
                var rolesApi = UseMockMode && MockHttpClient != null
                    ? new RolesApi(MockHttpClient, ApiConfiguration)
                    : new RolesApi(ApiConfiguration);
                
                var roles = await rolesApi.GetRolesAsync();
                
                if (roles?.Roles != null && roles.Roles.Count > 0)
                {
                    var roleId = roles.Roles[0].Id;
                    var result = await rolesApi.GetRolePermissionsAsync(roleId);
                    
                    Assert.NotNull(result);
                    TestSerializationRoundTrip(result, $"GetRolePermissions-{roleId}");
                }
            }
            catch (Exception ex)
            {
                TestOutputHelper.WriteError(_output, "GetRolePermissions_WithId", ex);
                throw;
            }
        }

        #endregion

        #region Helper Methods

        /// <summary>
        /// Tests serialization/deserialization round-trip for a response object
        /// </summary>
        private void TestSerializationRoundTrip<T>(T original, string testName) where T : class
        {
            try
            {
                // Get the standard converters from ApiClient using reflection
                var apiClientType = typeof(Kinde.Api.Client.ApiClient);
                var helperType = apiClientType.Assembly.GetType("Kinde.Api.Client.JsonConverterHelper");
                IList<JsonConverter> converters;
                if (helperType != null)
                {
                    var method = helperType.GetMethod("CreateStandardConverters", 
                        System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Static);
                    if (method != null)
                    {
                        converters = (IList<JsonConverter>)method.Invoke(null, null)!;
                    }
                    else
                    {
                        throw new InvalidOperationException("Could not find CreateStandardConverters method");
                    }
                }
                else
                {
                    throw new InvalidOperationException("Could not find JsonConverterHelper type");
                }
                var settings = new JsonSerializerSettings
                {
                    Converters = converters,
                    NullValueHandling = NullValueHandling.Ignore
                };

                // Serialize
                var json = JsonConvert.SerializeObject(original, settings);
                Assert.False(string.IsNullOrEmpty(json), 
                    $"{testName}: Serialization produced empty JSON");

                _output.WriteLine($"{testName}: Serialized to {json.Length} characters");

                // Deserialize
                var deserialized = JsonConvert.DeserializeObject<T>(json, settings);
                Assert.NotNull(deserialized);

                // Round-trip comparison
                var originalJson = JsonConvert.SerializeObject(original, settings);
                var deserializedJson = JsonConvert.SerializeObject(deserialized, settings);
                
                Assert.Equal(originalJson, deserializedJson);

                _output.WriteLine($"✓ {testName}: Converter test passed");
            }
            catch (Exception ex)
            {
                _output.WriteLine($"✗ {testName}: Converter test failed - {ex.Message}");
                throw;
            }
        }

        #endregion
    }
}

