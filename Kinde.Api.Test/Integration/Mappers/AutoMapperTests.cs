using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using AutoMapper;
using Kinde.Api.Mappers;
using Kinde.Api.Model;
using Xunit;
using Xunit.Abstractions;
using KiotaManagementModels = Kinde.Api.Kiota.Management.Models;
using KiotaAccountsModels = Kinde.Api.Kiota.Accounts.Models;

namespace Kinde.Api.Test.Integration.Mappers
{
    /// <summary>
    /// Tests for AutoMapper configuration and mappings between OpenAPI and Kiota models.
    /// These tests ensure that the facade layer can correctly translate models.
    /// </summary>
    public class AutoMapperTests
    {
        private readonly ITestOutputHelper _output;
        private readonly IMapper _mapper;

        public AutoMapperTests(ITestOutputHelper output)
        {
            _output = output;
            _mapper = KindeMapperConfiguration.Mapper;
        }

        #region Configuration Validation Tests

        /// <summary>
        /// Verifies that the AutoMapper configuration can be created without exceptions.
        /// </summary>
        [Fact]
        public void AutoMapperConfiguration_CanBeCreated()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<ManagementApiMapperProfile>();
                cfg.AddProfile<AccountsApiMapperProfile>();
            });

            Assert.NotNull(config);
            var mapper = config.CreateMapper();
            Assert.NotNull(mapper);
        }

        /// <summary>
        /// Verifies that the singleton mapper is properly initialized.
        /// </summary>
        [Fact]
        public void KindeMapperConfiguration_ReturnsSameMapperInstance()
        {
            var mapper1 = KindeMapperConfiguration.Mapper;
            var mapper2 = KindeMapperConfiguration.Mapper;
            Assert.Same(mapper1, mapper2);
        }

        #endregion

        #region Management API Model Mapping Tests

        /// <summary>
        /// Tests mapping CreateUserResponse from Kiota to OpenAPI model.
        /// </summary>
        [Fact]
        public void CreateUserResponse_MapsFromKiotaToOpenApi()
        {
            var kiotaModel = new KiotaManagementModels.Create_user_response
            {
                Created = true,
                Id = "user_12345"
            };

            var openApiModel = _mapper.Map<CreateUserResponse>(kiotaModel);

            Assert.NotNull(openApiModel);
            Assert.True(openApiModel.Created);
            Assert.Equal("user_12345", openApiModel.Id);
            _output.WriteLine($"Mapped CreateUserResponse: Created={openApiModel.Created}, Id={openApiModel.Id}");
        }

        /// <summary>
        /// Tests mapping SuccessResponse from Kiota to OpenAPI model.
        /// </summary>
        [Fact]
        public void SuccessResponse_MapsFromKiotaToOpenApi()
        {
            var kiotaModel = new KiotaManagementModels.Success_response
            {
                Message = "Operation successful",
                Code = "200"
            };

            var openApiModel = _mapper.Map<SuccessResponse>(kiotaModel);

            Assert.NotNull(openApiModel);
            Assert.Equal("Operation successful", openApiModel.Message);
            Assert.Equal("200", openApiModel.Code);
        }

        /// <summary>
        /// Tests mapping Users_response_users from Kiota to OpenAPI User model.
        /// </summary>
        [Fact]
        public void User_MapsFromKiotaToOpenApi()
        {
            var kiotaModel = new KiotaManagementModels.Users_response_users
            {
                Id = "user_abc123",
                FirstName = "John",
                LastName = "Doe"
            };

            var openApiModel = _mapper.Map<User>(kiotaModel);

            Assert.NotNull(openApiModel);
            Assert.Equal("user_abc123", openApiModel.Id);
            Assert.Equal("John", openApiModel.FirstName);
            Assert.Equal("Doe", openApiModel.LastName);
            _output.WriteLine($"Mapped User: Id={openApiModel.Id}, FirstName={openApiModel.FirstName}");
        }

        /// <summary>
        /// Tests mapping UsersResponse from Kiota to OpenAPI model.
        /// </summary>
        [Fact]
        public void UsersResponse_MapsFromKiotaToOpenApi()
        {
            var kiotaModel = new KiotaManagementModels.Users_response
            {
                Code = "200",
                Message = "Users retrieved successfully"
            };

            var openApiModel = _mapper.Map<UsersResponse>(kiotaModel);

            Assert.NotNull(openApiModel);
            Assert.Equal("200", openApiModel.Code);
            Assert.Equal("Users retrieved successfully", openApiModel.Message);
        }

        /// <summary>
        /// Tests mapping GetOrganizationsResponse from Kiota to OpenAPI model.
        /// </summary>
        [Fact]
        public void GetOrganizationsResponse_MapsFromKiotaToOpenApi()
        {
            var kiotaModel = new KiotaManagementModels.Get_organizations_response
            {
                Code = "200",
                Message = "Organizations retrieved"
            };

            var openApiModel = _mapper.Map<GetOrganizationsResponse>(kiotaModel);

            Assert.NotNull(openApiModel);
            Assert.Equal("200", openApiModel.Code);
        }

        #endregion

        #region Accounts API Model Mapping Tests

        /// <summary>
        /// Tests mapping PortalLink from Kiota to OpenAPI model.
        /// </summary>
        [Fact]
        public void PortalLink_MapsFromKiotaToOpenApi()
        {
            var kiotaModel = new KiotaAccountsModels.Portal_link
            {
                Url = "https://portal.kinde.com/link/abc123"
            };

            var openApiModel = _mapper.Map<Kinde.Accounts.Model.Entities.PortalLink>(kiotaModel);

            Assert.NotNull(openApiModel);
            Assert.Equal("https://portal.kinde.com/link/abc123", openApiModel.Url);
        }

        /// <summary>
        /// Tests mapping UserProfileV2 from Kiota to OpenAPI model.
        /// </summary>
        [Fact]
        public void UserProfileV2_MapsFromKiotaToOpenApi()
        {
            var kiotaModel = new KiotaAccountsModels.User_profile_v2
            {
                Id = "user_profile_123",
                Email = "profile@example.com"
            };

            var openApiModel = _mapper.Map<Kinde.Accounts.Model.Entities.UserProfileV2>(kiotaModel);

            Assert.NotNull(openApiModel);
            Assert.Equal("user_profile_123", openApiModel.Id);
            Assert.Equal("profile@example.com", openApiModel.Email);
        }

        #endregion

        #region Null Handling Tests

        /// <summary>
        /// Verifies that mapping null returns null.
        /// </summary>
        [Fact]
        public void Mapping_NullSource_ReturnsNull()
        {
            var result = _mapper.Map<User>((KiotaManagementModels.Users_response_users)null);
            Assert.Null(result);
        }

        /// <summary>
        /// Verifies that mapping models with null properties handles them correctly.
        /// </summary>
        [Fact]
        public void Mapping_ModelWithNullProperties_HandlesCorrectly()
        {
            var kiotaModel = new KiotaManagementModels.Users_response_users
            {
                Id = "user_with_nulls",
                FirstName = null,
                LastName = null
            };

            var openApiModel = _mapper.Map<User>(kiotaModel);

            Assert.NotNull(openApiModel);
            Assert.Equal("user_with_nulls", openApiModel.Id);
            Assert.Null(openApiModel.FirstName);
            Assert.Null(openApiModel.LastName);
        }

        /// <summary>
        /// Tests nested object null handling.
        /// </summary>
        [Fact]
        public void NestedNullObjects_HandleCorrectly()
        {
            var kiotaModel = new KiotaManagementModels.Users_response
            {
                Code = "200",
                Message = "Success",
                Users = null
            };

            var openApiModel = _mapper.Map<UsersResponse>(kiotaModel);

            Assert.NotNull(openApiModel);
            Assert.Equal("200", openApiModel.Code);
            Assert.Null(openApiModel.Users);
        }

        #endregion

        #region Collection Mapping Tests

        /// <summary>
        /// Verifies that mapping collections of models works correctly.
        /// </summary>
        [Fact]
        public void Mapping_CollectionOfModels_WorksCorrectly()
        {
            var kiotaModels = new List<KiotaManagementModels.Users_response_users>
            {
                new KiotaManagementModels.Users_response_users { Id = "user_1", FirstName = "User1" },
                new KiotaManagementModels.Users_response_users { Id = "user_2", FirstName = "User2" },
                new KiotaManagementModels.Users_response_users { Id = "user_3", FirstName = "User3" }
            };

            var openApiModels = _mapper.Map<List<User>>(kiotaModels);

            Assert.NotNull(openApiModels);
            Assert.Equal(3, openApiModels.Count);
            Assert.Equal("user_1", openApiModels[0].Id);
            Assert.Equal("user_2", openApiModels[1].Id);
            Assert.Equal("user_3", openApiModels[2].Id);
            _output.WriteLine($"Mapped {openApiModels.Count} users successfully");
        }

        /// <summary>
        /// Verifies that mapping empty collections returns empty collections.
        /// </summary>
        [Fact]
        public void Mapping_EmptyCollection_ReturnsEmptyCollection()
        {
            var emptyList = new List<KiotaManagementModels.Users_response_users>();

            var result = _mapper.Map<List<User>>(emptyList);

            Assert.NotNull(result);
            Assert.Empty(result);
        }

        /// <summary>
        /// Tests that collections with items map correctly.
        /// </summary>
        [Fact]
        public void CollectionWithItems_PreservesItems()
        {
            var kiotaModel = new KiotaManagementModels.Users_response
            {
                Code = "200",
                Message = "Success",
                Users = new List<KiotaManagementModels.Users_response_users>
                {
                    new KiotaManagementModels.Users_response_users { Id = "user_1", FirstName = "John" },
                    new KiotaManagementModels.Users_response_users { Id = "user_2", FirstName = "Jane" }
                }
            };

            var openApiModel = _mapper.Map<UsersResponse>(kiotaModel);

            Assert.NotNull(openApiModel);
            Assert.NotNull(openApiModel.Users);
            Assert.Equal(2, openApiModel.Users.Count);
            Assert.Equal("user_1", openApiModel.Users[0].Id);
            Assert.Equal("user_2", openApiModel.Users[1].Id);
        }

        #endregion

        #region Boolean Field Tests

        /// <summary>
        /// Tests that boolean values are correctly mapped.
        /// </summary>
        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void CreateUserResponse_Created_MapsCorrectly(bool expectedValue)
        {
            var kiotaModel = new KiotaManagementModels.Create_user_response
            {
                Created = expectedValue,
                Id = "user_boolean_test"
            };

            var openApiModel = _mapper.Map<CreateUserResponse>(kiotaModel);

            Assert.NotNull(openApiModel);
            Assert.Equal(expectedValue, openApiModel.Created);
            _output.WriteLine($"Boolean test: Created={expectedValue} mapped correctly");
        }

        /// <summary>
        /// Tests that explicitly false boolean values don't default to true.
        /// </summary>
        [Fact]
        public void CreateUserResponse_Created_False_DoesNotDefaultToTrue()
        {
            var kiotaModel = new KiotaManagementModels.Create_user_response
            {
                Created = false,
                Id = "user_not_created"
            };

            var openApiModel = _mapper.Map<CreateUserResponse>(kiotaModel);

            Assert.NotNull(openApiModel);
            Assert.False(openApiModel.Created, "Created should be false, not defaulting to true");
            _output.WriteLine("PASS: Created=false was preserved correctly");
        }

        /// <summary>
        /// Tests nullable boolean to non-nullable boolean mapping.
        /// When Kiota has null and OpenAPI has non-nullable bool, null becomes false (default).
        /// </summary>
        [Fact]
        public void NullableBoolean_ConvertsNullToDefaultForNonNullableTarget()
        {
            var kiotaModel = new KiotaManagementModels.Create_user_response
            {
                Created = null,
                Id = "user_null_created"
            };

            var openApiModel = _mapper.Map<CreateUserResponse>(kiotaModel);

            Assert.NotNull(openApiModel);
            // When mapping null bool? to bool, the result is false (default)
            Assert.False(openApiModel.Created);
        }

        /// <summary>
        /// Tests IsSuspended boolean mapping on User.
        /// </summary>
        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void User_IsSuspended_MapsCorrectly(bool expectedValue)
        {
            var kiotaModel = new KiotaManagementModels.Users_response_users
            {
                Id = "user_suspended_test",
                IsSuspended = expectedValue
            };

            var openApiModel = _mapper.Map<User>(kiotaModel);

            Assert.NotNull(openApiModel);
            Assert.Equal(expectedValue, openApiModel.IsSuspended);
            _output.WriteLine($"IsSuspended={expectedValue} mapped correctly");
        }

        #endregion

        #region Accounts API Extended Tests

        /// <summary>
        /// Tests TokenIntrospect mapping with boolean active field.
        /// </summary>
        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void TokenIntrospect_Active_MapsCorrectly(bool expectedValue)
        {
            var kiotaModel = new KiotaAccountsModels.Token_introspect
            {
                Active = expectedValue
            };

            var openApiModel = _mapper.Map<Kinde.Accounts.Model.Entities.TokenIntrospect>(kiotaModel);

            Assert.NotNull(openApiModel);
            Assert.Equal(expectedValue, openApiModel.Active);
            _output.WriteLine($"TokenIntrospect.Active={expectedValue} mapped correctly");
        }

        #endregion

        #region Edge Case Tests

        /// <summary>
        /// Tests mapping with special characters in strings.
        /// </summary>
        [Fact]
        public void SpecialCharacters_InStrings_PreservedCorrectly()
        {
            var kiotaModel = new KiotaManagementModels.Users_response_users
            {
                Id = "user_special",
                FirstName = "Tëst™",
                LastName = "Üser®"
            };

            var openApiModel = _mapper.Map<User>(kiotaModel);

            Assert.NotNull(openApiModel);
            Assert.Equal("Tëst™", openApiModel.FirstName);
            Assert.Equal("Üser®", openApiModel.LastName);
        }

        /// <summary>
        /// Tests mapping with very long strings.
        /// </summary>
        [Fact]
        public void LongStrings_PreservedCorrectly()
        {
            var longString = new string('x', 10000);
            var kiotaModel = new KiotaManagementModels.Users_response_users
            {
                Id = "user_long",
                FirstName = longString
            };

            var openApiModel = _mapper.Map<User>(kiotaModel);

            Assert.NotNull(openApiModel);
            Assert.Equal(longString, openApiModel.FirstName);
            Assert.Equal(10000, openApiModel.FirstName.Length);
        }

        /// <summary>
        /// Tests mapping with whitespace-only strings.
        /// </summary>
        [Fact]
        public void WhitespaceStrings_PreservedCorrectly()
        {
            var kiotaModel = new KiotaManagementModels.Users_response_users
            {
                Id = "user_whitespace",
                FirstName = "   ",
                LastName = "\t\n\r"
            };

            var openApiModel = _mapper.Map<User>(kiotaModel);

            Assert.NotNull(openApiModel);
            Assert.Equal("   ", openApiModel.FirstName);
            Assert.Equal("\t\n\r", openApiModel.LastName);
        }

        #endregion

        #region Request body mapping smoke tests

        [Fact] public void CreateApiKeyRequest_Maps()                  => AssertMaps<CreateApiKeyRequest, Kinde.Api.Kiota.Management.Api.V1.Api_keys.Api_keysPostRequestBody>();
        [Fact] public void VerifyApiKeyRequest_Maps()                  => AssertMaps<VerifyApiKeyRequest, Kinde.Api.Kiota.Management.Api.V1.Api_keys.Verify.VerifyPostRequestBody>();
        [Fact] public void AddAPIsRequest_Maps()                       => AssertMaps<AddAPIsRequest, Kinde.Api.Kiota.Management.Api.V1.Apis.ApisPostRequestBody>();
        [Fact] public void UpdateAPIApplicationsRequest_Maps()         => AssertMaps<UpdateAPIApplicationsRequest, Kinde.Api.Kiota.Management.Api.V1.Apis.Item.Applications.ApplicationsPatchRequestBody>();
        [Fact] public void UpdateAPIScopeRequest_Maps()                => AssertMaps<UpdateAPIScopeRequest, Kinde.Api.Kiota.Management.Api.V1.Apis.Item.Scopes.Item.WithScope_PatchRequestBody>();
        [Fact] public void UpdateOrganizationRequest_Maps()            => AssertMaps<UpdateOrganizationRequest, Kinde.Api.Kiota.Management.Api.V1.Organization.Item.WithOrg_codePatchRequestBody>();
        [Fact] public void UpdateOrganizationPropertiesRequest_OrgEndpoint_Maps()  => AssertMaps<UpdateOrganizationPropertiesRequest, Kinde.Api.Kiota.Management.Api.V1.Organizations.Item.Properties.PropertiesPatchRequestBody>();
        [Fact] public void UpdateOrganizationSessionsRequest_Maps()    => AssertMaps<UpdateOrganizationSessionsRequest, Kinde.Api.Kiota.Management.Api.V1.Organizations.Item.Sessions.SessionsPatchRequestBody>();
        [Fact] public void UpdateOrganizationUsersRequest_Maps()       => AssertMaps<UpdateOrganizationUsersRequest, Kinde.Api.Kiota.Management.Api.V1.Organizations.Item.Users.UsersPatchRequestBody>();
        [Fact] public void CreateUserIdentityRequest_Maps()            => AssertMaps<CreateUserIdentityRequest, Kinde.Api.Kiota.Management.Api.V1.Users.Item.Identities.IdentitiesPostRequestBody>();
        [Fact] public void SetUserPasswordRequest_Maps()               => AssertMaps<SetUserPasswordRequest, Kinde.Api.Kiota.Management.Api.V1.Users.Item.Password.PasswordPutRequestBody>();
        [Fact] public void UpdateOrganizationPropertiesRequest_UserEndpoint_Maps() => AssertMaps<UpdateOrganizationPropertiesRequest, Kinde.Api.Kiota.Management.Api.V1.Users.Item.Properties.PropertiesPatchRequestBody>();

        private void AssertMaps<TSrc, TDst>()
        {
            var src = (TSrc)System.Runtime.CompilerServices.RuntimeHelpers.GetUninitializedObject(typeof(TSrc));
            var dst = _mapper.Map<TDst>(src);
            Assert.NotNull(dst);
        }

        #endregion

        #region Kiota constructor config propagation

        [Fact]
        public void XApi_FromApiClient_PropagatesBasePath()
        {
            const string domain = "https://example.kinde.com";
            using var httpClient = new System.Net.Http.HttpClient();
            var client = new Kinde.Api.Client.ApiClient(httpClient, domain);

            using var api = new Kinde.Api.Api.ApplicationsApi(client);

            Assert.Equal(domain, api.Configuration.BasePath);
            Assert.NotEqual("https://your_kinde_subdomain.kinde.com", api.Configuration.BasePath);
        }

        [Fact]
        public void XApi_FromApiClient_PropagatesAccessToken()
        {
            using var httpClient = new System.Net.Http.HttpClient();
            var client = new TokenStubApiClient(httpClient, "https://example.kinde.com", "stub-token-abc");

            using var api = new Kinde.Api.Api.ApplicationsApi(client);

            Assert.Equal("https://example.kinde.com", api.Configuration.BasePath);
            Assert.Equal("stub-token-abc", api.Configuration.AccessToken);
        }
        private sealed class TokenStubApiClient : Kinde.Api.Client.ApiClient
        {
            private readonly string _token;
            public TokenStubApiClient(System.Net.Http.HttpClient http, string basePath, string token)
                : base(http, basePath) { _token = token; }
            public override string AccessToken => _token;
        }

        private sealed class MutableTokenStubApiClient : Kinde.Api.Client.ApiClient
        {
            public string CurrentToken { get; set; }
            public MutableTokenStubApiClient(System.Net.Http.HttpClient http, string basePath, string initialToken)
                : base(http, basePath) { CurrentToken = initialToken; }
            public override string AccessToken => CurrentToken;
        }

        [Fact]
        public async System.Threading.Tasks.Task XApi_TokenRefresh_IsPickedUpLive()
        {
            using var httpClient = new System.Net.Http.HttpClient();
            var client = new MutableTokenStubApiClient(httpClient, "https://example.kinde.com", "token-v1");
            using var api = new Kinde.Api.Api.ApplicationsApi(client);

var kiotaClientProp = typeof(Kinde.Api.Api.ApplicationsApi).GetProperty(
                "KiotaClient", BindingFlags.Instance | BindingFlags.NonPublic);
            Assert.NotNull(kiotaClientProp);
            var kiotaClient = kiotaClientProp.GetValue(api);
            Assert.NotNull(kiotaClient);

var providerType = typeof(Kinde.Api.Api.ApplicationsApi)
                .GetNestedType("KiotaTokenProvider", BindingFlags.NonPublic);
            Assert.NotNull(providerType);
            var cachedProvider = FindReachableInstance(kiotaClient, providerType, maxDepth: 6);
            Assert.NotNull(cachedProvider);

var getTokenField = providerType.GetField("_getToken", BindingFlags.Instance | BindingFlags.NonPublic);
            Assert.NotNull(getTokenField);
            var liveDelegate = (Func<string>)getTokenField.GetValue(cachedProvider);
            Assert.NotNull(liveDelegate);

            client.CurrentToken = "token-v2";
            Assert.Equal("token-v2", liveDelegate());

            client.CurrentToken = "token-v3";
            Assert.Equal("token-v3", liveDelegate());

            var getAuthMethod = providerType.GetMethod("GetAuthorizationTokenAsync");
            Assert.NotNull(getAuthMethod);
            var task = (System.Threading.Tasks.Task<string>)getAuthMethod.Invoke(cachedProvider,
                new object[] { new Uri("https://example.kinde.com/api/v1/applications"), null, default(System.Threading.CancellationToken) });
            Assert.Equal("token-v3", await task);
        }

private static object FindReachableInstance(object root, Type target, int maxDepth)
        {
            var visited = new HashSet<object>(ReferenceEqualityComparer.Instance);
            var queue = new Queue<(object Node, int Depth)>();
            queue.Enqueue((root, 0));

            while (queue.Count > 0)
            {
                var (node, depth) = queue.Dequeue();
                if (node is null || depth > maxDepth || !visited.Add(node)) continue;
                if (target.IsInstanceOfType(node)) return node;

                var type = node.GetType();
                foreach (var f in type.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic))
                {
                    if (f.FieldType.IsPrimitive || f.FieldType == typeof(string)) continue;
                    object value;
                    try { value = f.GetValue(node); } catch { continue; }
                    if (value is not null) queue.Enqueue((value, depth + 1));
                }
                foreach (var p in type.GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic))
                {
                    if (p.GetIndexParameters().Length > 0) continue;
                    if (!p.CanRead) continue;
                    if (p.PropertyType.IsPrimitive || p.PropertyType == typeof(string)) continue;
                    object value;
                    try { value = p.GetValue(node); } catch { continue; }
                    if (value is not null) queue.Enqueue((value, depth + 1));
                }
            }
            return null;
        }

        [Fact]
        public void ReplaceConnectionRequest_SamlOptions_MapsToMember3AndIncludesEnums()
        {
            var saml = new ReplaceConnectionRequestOptionsOneOf1
            {
                SamlEntityId = "https://example.okta.com/saml/metadata",
                SamlIdpMetadataUrl = "https://example.okta.com/sso/saml/metadata",
                NameIdFormat = ReplaceConnectionRequestOptionsOneOf1.NameIdFormatEnum.Persistent,
                ProtocolBinding = ReplaceConnectionRequestOptionsOneOf1.ProtocolBindingEnum.REDIRECT,
                SignRequestAlgorithm = ReplaceConnectionRequestOptionsOneOf1.SignRequestAlgorithmEnum.SHA1,
            };
            var request = new ReplaceConnectionRequest(
                displayName: "Okta SAML",
                options: new ReplaceConnectionRequestOptions(saml));

            var kiota = _mapper.Map<Kinde.Api.Kiota.Management.Api.V1.Connections.Item.WithConnection_PutRequestBody>(request);
            var member3 = kiota.Options.WithConnectionPutRequestBodyOptionsMember3;
            Assert.NotNull(member3);
            Assert.Null(kiota.Options.WithConnectionPutRequestBodyOptionsMember1);
            Assert.Null(kiota.Options.WithConnectionPutRequestBodyOptionsMember2);
            Assert.Equal("https://example.okta.com/saml/metadata", member3.SamlEntityId);

            using var writer = new Microsoft.Kiota.Serialization.Json.JsonSerializationWriter();
            writer.WriteObjectValue<Kinde.Api.Kiota.Management.Api.V1.Connections.Item.WithConnection_PutRequestBody_optionsMember3>(null, member3);
            using var stream = writer.GetSerializedContent();
            using var reader = new System.IO.StreamReader(stream);
            var json = reader.ReadToEnd();
            _output.WriteLine("PUT wire body: " + json);

            Assert.Contains("\"name_id_format\":\"Persistent\"", json);
            Assert.Contains("\"protocol_binding\":\"HTTP-REDIRECT\"", json);
            Assert.Contains("\"sign_request_algorithm\":\"RSA-SHA1\"", json);
        }

        [Fact]
        public void UpdateConnectionRequest_SamlOptions_MapsToMember3AndIncludesEnums()
        {
            var saml = new UpdateConnectionRequestOptionsOneOf1
            {
                SamlEntityId = "https://example.okta.com/saml/metadata",
                NameIdFormat = UpdateConnectionRequestOptionsOneOf1.NameIdFormatEnum.EmailAddress,
                ProtocolBinding = UpdateConnectionRequestOptionsOneOf1.ProtocolBindingEnum.POST,
                SignRequestAlgorithm = UpdateConnectionRequestOptionsOneOf1.SignRequestAlgorithmEnum.SHA256,
            };
            var request = new UpdateConnectionRequest(
                options: new UpdateConnectionRequestOptions(saml));

            var kiota = _mapper.Map<Kinde.Api.Kiota.Management.Api.V1.Connections.Item.WithConnection_PatchRequestBody>(request);
            var member3 = kiota.Options.WithConnectionPatchRequestBodyOptionsMember3;
            Assert.NotNull(member3);
            Assert.Null(kiota.Options.WithConnectionPatchRequestBodyOptionsMember1);
            Assert.Null(kiota.Options.WithConnectionPatchRequestBodyOptionsMember2);
            Assert.Equal("https://example.okta.com/saml/metadata", member3.SamlEntityId);

            using var writer = new Microsoft.Kiota.Serialization.Json.JsonSerializationWriter();
            writer.WriteObjectValue<Kinde.Api.Kiota.Management.Api.V1.Connections.Item.WithConnection_PatchRequestBody_optionsMember3>(null, member3);
            using var stream = writer.GetSerializedContent();
            using var reader = new System.IO.StreamReader(stream);
            var json = reader.ReadToEnd();
            _output.WriteLine("PATCH wire body: " + json);

            Assert.Contains("\"name_id_format\":\"Email address\"", json);
            Assert.Contains("\"protocol_binding\":\"HTTP-POST\"", json);
            Assert.Contains("\"sign_request_algorithm\":\"RSA-SHA256\"", json);
        }

        #endregion

        #region UpdateUserRequest mapping diagnostics

        [Fact]
        public void UpdateUserRequest_BasicFields_FlowThroughMapping()
        {
            var src = new UpdateUserRequest(
                givenName: "John",
                familyName: "Doe");

            var dst = _mapper.Map<Kinde.Api.Kiota.Management.Api.V1.User.UserPatchRequestBody>(src);

            Assert.NotNull(dst);
            Assert.Equal("John", dst.GivenName);
            Assert.Equal("Doe", dst.FamilyName);
            _output.WriteLine($"GivenName='{dst.GivenName}', FamilyName='{dst.FamilyName}', IsSuspended={dst.IsSuspended}, IsPasswordResetRequested={dst.IsPasswordResetRequested}");
        }

        [Fact]
        public void UpdateUserRequest_SerializedBody_ContainsExpectedFields()
        {
            var src = new UpdateUserRequest(
                givenName: "John",
                familyName: "Doe");

            var dst = _mapper.Map<Kinde.Api.Kiota.Management.Api.V1.User.UserPatchRequestBody>(src);

            using var serWriter = new Microsoft.Kiota.Serialization.Json.JsonSerializationWriter();
            serWriter.WriteObjectValue<Kinde.Api.Kiota.Management.Api.V1.User.UserPatchRequestBody>(null, dst);
            using var stream = serWriter.GetSerializedContent();
            using var reader = new System.IO.StreamReader(stream);
            var json = reader.ReadToEnd();

            _output.WriteLine("Wire body: " + json);
            Assert.Contains("\"given_name\":\"John\"", json);
            Assert.Contains("\"family_name\":\"Doe\"", json);
        }

        [Fact]
        public void UpdateUserRequest_UnsetBools_StayNullOnWire()
        {
            var src = new UpdateUserRequest(givenName: "John", familyName: "Doe");

            var dst = _mapper.Map<Kinde.Api.Kiota.Management.Api.V1.User.UserPatchRequestBody>(src);

            Assert.Null(dst.IsSuspended);
            Assert.Null(dst.IsPasswordResetRequested);
        }

        [Fact]
        public void UpdateUserRequest_ExplicitBools_RoundTripThroughMapping()
        {
            var src = new UpdateUserRequest(givenName: "John", familyName: "Doe",
                isSuspended: true, isPasswordResetRequested: false);

            var dst = _mapper.Map<Kinde.Api.Kiota.Management.Api.V1.User.UserPatchRequestBody>(src);

            Assert.True(dst.IsSuspended);
            Assert.False(dst.IsPasswordResetRequested);
        }

        #endregion

        #region SAML wire payload

        [Fact]
        public void CreateConnectionRequest_SamlEnums_LandOnWirePayload()
        {
            var saml = new CreateConnectionRequestOptionsOneOf2
            {
                SamlEntityId = "https://example.okta.com/saml/metadata",
                SamlIdpMetadataUrl = "https://example.okta.com/sso/saml/metadata",
                NameIdFormat = CreateConnectionRequestOptionsOneOf2.NameIdFormatEnum.EmailAddress,
                ProtocolBinding = CreateConnectionRequestOptionsOneOf2.ProtocolBindingEnum.POST,
                SignRequestAlgorithm = CreateConnectionRequestOptionsOneOf2.SignRequestAlgorithmEnum.SHA256,
                SamlUserIdKeyAttr = "user.id",
                IsTrusted = true,
                IsUseCustomDomain = true,
            };
            var request = new CreateConnectionRequest(
                name: "saml-okta",
                strategy: CreateConnectionRequest.StrategyEnum.Samlokta,
                options: new CreateConnectionRequestOptions(saml));

            var kiota = _mapper.Map<Kinde.Api.Kiota.Management.Api.V1.Connections.ConnectionsPostRequestBody>(request);
            var member3 = kiota.Options.ConnectionsPostRequestBodyOptionsMember3;
            Assert.NotNull(member3);

            using var writer = new Microsoft.Kiota.Serialization.Json.JsonSerializationWriter();
            writer.WriteObjectValue<Kinde.Api.Kiota.Management.Api.V1.Connections.ConnectionsPostRequestBody_optionsMember3>(null, member3);
            using var stream = writer.GetSerializedContent();
            using var reader = new System.IO.StreamReader(stream);
            var json = reader.ReadToEnd();
            _output.WriteLine("Wire body: " + json);

            Assert.Contains("\"name_id_format\":\"Email address\"", json);
            Assert.Contains("\"protocol_binding\":\"HTTP-POST\"", json);
            Assert.Contains("\"sign_request_algorithm\":\"RSA-SHA256\"", json);
            Assert.Contains("\"saml_entity_id\":\"https://example.okta.com/saml/metadata\"", json);
            Assert.Contains("\"saml_user_id_key_attr\":\"user.id\"", json);
            Assert.Contains("\"is_trusted\":true", json);
            Assert.Contains("\"is_use_custom_domain\":true", json);
        }

        #endregion

        #region CreateConnectionRequest oneOf Options Tests

        [Fact]
        public void CreateConnectionRequest_SocialOptions_MapsToMember1()
        {
            var request = new CreateConnectionRequest(
                name: "google-sso",
                displayName: "Google SSO",
                strategy: CreateConnectionRequest.StrategyEnum.Oauth2google,
                options: new CreateConnectionRequestOptions(new CreateConnectionRequestOptionsOneOf
                {
                    ClientId = "client-abc",
                    ClientSecret = "secret-xyz",
                    IsUseCustomDomain = true,
                }));

            var kiota = _mapper.Map<Kinde.Api.Kiota.Management.Api.V1.Connections.ConnectionsPostRequestBody>(request);

            Assert.NotNull(kiota);
            Assert.Equal("google-sso", kiota.Name);
            Assert.NotNull(kiota.Options);
            Assert.NotNull(kiota.Options.ConnectionsPostRequestBodyOptionsMember1);
            Assert.Null(kiota.Options.ConnectionsPostRequestBodyOptionsMember2);
            Assert.Null(kiota.Options.ConnectionsPostRequestBodyOptionsMember3);
            Assert.Equal("client-abc", kiota.Options.ConnectionsPostRequestBodyOptionsMember1.ClientId);
            Assert.Equal("secret-xyz", kiota.Options.ConnectionsPostRequestBodyOptionsMember1.ClientSecret);
            Assert.True(kiota.Options.ConnectionsPostRequestBodyOptionsMember1.IsUseCustomDomain);
        }

        [Fact]
        public void CreateConnectionRequest_EntraOptions_MapsToMember2()
        {
            var request = new CreateConnectionRequest(
                name: "entra-id",
                displayName: "Entra ID",
                strategy: CreateConnectionRequest.StrategyEnum.Oauth2microsoft,
                options: new CreateConnectionRequestOptions(new CreateConnectionRequestOptionsOneOf1
                {
                    ClientId = "entra-client",
                    ClientSecret = "entra-secret",
                    EntraIdDomain = "contoso.com",
                    IsUseCommonEndpoint = true,
                }));

            var kiota = _mapper.Map<Kinde.Api.Kiota.Management.Api.V1.Connections.ConnectionsPostRequestBody>(request);

            Assert.NotNull(kiota);
            Assert.NotNull(kiota.Options);
            Assert.Null(kiota.Options.ConnectionsPostRequestBodyOptionsMember1);
            Assert.NotNull(kiota.Options.ConnectionsPostRequestBodyOptionsMember2);
            Assert.Null(kiota.Options.ConnectionsPostRequestBodyOptionsMember3);
            Assert.Equal("entra-client", kiota.Options.ConnectionsPostRequestBodyOptionsMember2.ClientId);
            Assert.Equal("contoso.com", kiota.Options.ConnectionsPostRequestBodyOptionsMember2.EntraIdDomain);
            Assert.True(kiota.Options.ConnectionsPostRequestBodyOptionsMember2.IsUseCommonEndpoint);
        }

        [Fact]
        public void CreateConnectionRequest_NullOptions_MapsToNull()
        {
            var request = new CreateConnectionRequest(
                name: "no-options",
                displayName: "No options",
                strategy: CreateConnectionRequest.StrategyEnum.Oauth2google);

            var kiota = _mapper.Map<Kinde.Api.Kiota.Management.Api.V1.Connections.ConnectionsPostRequestBody>(request);

            Assert.NotNull(kiota);
            Assert.Null(kiota.Options);
        }

        [Fact]
        public void CreateConnectionRequest_SamlOptions_MapsToMember3()
        {
            var request = new CreateConnectionRequest(
                name: "saml-okta",
                displayName: "Okta SAML",
                strategy: CreateConnectionRequest.StrategyEnum.Samlokta,
                options: new CreateConnectionRequestOptions(new CreateConnectionRequestOptionsOneOf2
                {
                    SamlEntityId = "https://example.okta.com/saml/metadata",
                    SamlIdpMetadataUrl = "https://example.okta.com/sso/saml/metadata",
                    SamlSignInUrl = "https://example.okta.com/sso/saml",
                    SamlEmailKeyAttr = "email",
                    SamlFirstNameKeyAttr = "firstName",
                    SamlLastNameKeyAttr = "lastName",
                    IsCreateMissingUser = true,
                    NameIdFormat = CreateConnectionRequestOptionsOneOf2.NameIdFormatEnum.EmailAddress,
                    ProtocolBinding = CreateConnectionRequestOptionsOneOf2.ProtocolBindingEnum.POST,
                    SignRequestAlgorithm = CreateConnectionRequestOptionsOneOf2.SignRequestAlgorithmEnum.SHA256,
                }));

            var kiota = _mapper.Map<Kinde.Api.Kiota.Management.Api.V1.Connections.ConnectionsPostRequestBody>(request);

            Assert.NotNull(kiota);
            Assert.NotNull(kiota.Options);
            Assert.Null(kiota.Options.ConnectionsPostRequestBodyOptionsMember1);
            Assert.Null(kiota.Options.ConnectionsPostRequestBodyOptionsMember2);

            var member3 = kiota.Options.ConnectionsPostRequestBodyOptionsMember3;
            Assert.NotNull(member3);
            Assert.Equal("https://example.okta.com/saml/metadata", member3.SamlEntityId);
            Assert.Equal("email", member3.SamlEmailKeyAttr);
            Assert.True(member3.IsCreateMissingUser);

            Assert.Equal(Kinde.Api.Kiota.Management.Api.V1.Connections.ConnectionsPostRequestBody_optionsMember3_name_id_format.EmailAddress, member3.NameIdFormat);
            Assert.Equal(Kinde.Api.Kiota.Management.Api.V1.Connections.ConnectionsPostRequestBody_optionsMember3_protocol_binding.HTTPPOST, member3.ProtocolBinding);
            Assert.Equal(Kinde.Api.Kiota.Management.Api.V1.Connections.ConnectionsPostRequestBody_optionsMember3_sign_request_algorithm.RSASHA256, member3.SignRequestAlgorithm);
        }

        #endregion

        #region AdditionalData smuggling

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void ReplaceMFARequest_RoundTrip_PreservesRecoveryCodesFlag(bool flag)
        {
            var src = new ReplaceMFARequest(
                policy: ReplaceMFARequest.PolicyEnum.Required,
                enabledFactors: new List<ReplaceMFARequest.EnabledFactorsEnum> { ReplaceMFARequest.EnabledFactorsEnum.Email },
                isRecoveryCodesEnabled: flag);

            var kiota = _mapper.Map<Kinde.Api.Kiota.Management.Api.V1.Mfa.MfaPutRequestBody>(src);

            Assert.NotNull(kiota);
            Assert.Equal(flag, kiota.IsRecoveryCodesEnabled);

            var roundTrip = _mapper.Map<ReplaceMFARequest>(kiota);
            Assert.Equal(flag, roundTrip.IsRecoveryCodesEnabled);
        }

        /// <summary>
        /// Serializes a Kiota model the way the request adapter does, through the backing
        /// store proxy. A plain JsonSerializationWriter drops nulls, so it hides the
        /// difference between "never assigned" and "assigned null" -- which is exactly the
        /// difference that decides what goes on the wire.
        /// </summary>
        private static string SerializeAsSent<T>(T model) where T : Microsoft.Kiota.Abstractions.Serialization.IParsable
        {
            var factory = new Microsoft.Kiota.Abstractions.Store.BackingStoreSerializationWriterProxyFactory(
                new Microsoft.Kiota.Serialization.Json.JsonSerializationWriterFactory());
            using var writer = factory.GetSerializationWriter("application/json");
            writer.WriteObjectValue(null, model);
            using var stream = writer.GetSerializedContent();
            using var reader = new System.IO.StreamReader(stream);
            return reader.ReadToEnd();
        }

        [Fact]
        public void ReplaceMFARequest_UnsetRecoveryCodes_SendsSchemaDefaultNotNull()
        {
            var src = new ReplaceMFARequest(
                policy: ReplaceMFARequest.PolicyEnum.Required,
                enabledFactors: new List<ReplaceMFARequest.EnabledFactorsEnum> { ReplaceMFARequest.EnabledFactorsEnum.Email });

            var dst = _mapper.Map<Kinde.Api.Kiota.Management.Api.V1.Mfa.MfaPutRequestBody>(src);
            var json = SerializeAsSent(dst);

            _output.WriteLine("Wire body: " + json);

            // The caller left the flag unset, so the Kiota model keeps the schema default
            // (true) rather than having it overwritten with an explicit null. Sending
            // "is_recovery_codes_enabled": null would be rejected by the API.
            Assert.DoesNotContain("\"is_recovery_codes_enabled\":null", json);
            Assert.Contains("\"is_recovery_codes_enabled\":true", json);
        }

        [Fact]
        public void ReplaceMFARequest_ExplicitFalseRecoveryCodes_SurvivesToTheWire()
        {
            var src = new ReplaceMFARequest(
                policy: ReplaceMFARequest.PolicyEnum.Required,
                enabledFactors: new List<ReplaceMFARequest.EnabledFactorsEnum> { ReplaceMFARequest.EnabledFactorsEnum.Email },
                isRecoveryCodesEnabled: false);

            var dst = _mapper.Map<Kinde.Api.Kiota.Management.Api.V1.Mfa.MfaPutRequestBody>(src);

            Assert.False(dst.IsRecoveryCodesEnabled);
            Assert.Contains("\"is_recovery_codes_enabled\":false", SerializeAsSent(dst));
        }

        [Fact]
        public void UpdateOrganizationUsersRequest_UnsetOperation_IsOmittedNotNull()
        {
            var src = new UpdateOrganizationUsersRequest(
                users: new List<UpdateOrganizationUsersRequestUsersInner>
                {
                    new UpdateOrganizationUsersRequestUsersInner(
                        id: "kp_057ee6debc624c70947b6ba512908c35",
                        roles: new List<string> { "admin" }),
                });

            var dst = _mapper.Map<Kinde.Api.Kiota.Management.Api.V1.Organizations.Item.Users.UsersPatchRequestBody>(src);
            var json = SerializeAsSent(dst);

            _output.WriteLine("Wire body: " + json);

            // The API answers 500 for "operation": null, so properties the caller never set
            // must not appear at all.
            Assert.DoesNotContain("\"operation\"", json);
            Assert.DoesNotContain("\"permissions\"", json);
            Assert.Contains("\"roles\":[\"admin\"]", json);
        }

        [Fact]
        public void UpdateOrganizationUsersRequest_ExplicitOperation_SurvivesToTheWire()
        {
            var src = new UpdateOrganizationUsersRequest(
                users: new List<UpdateOrganizationUsersRequestUsersInner>
                {
                    new UpdateOrganizationUsersRequestUsersInner(
                        id: "kp_057ee6debc624c70947b6ba512908c35",
                        operation: "delete"),
                });

            var dst = _mapper.Map<Kinde.Api.Kiota.Management.Api.V1.Organizations.Item.Users.UsersPatchRequestBody>(src);

            Assert.Contains("\"operation\":\"delete\"", SerializeAsSent(dst));
        }

        [Fact]
        public void ReplaceMFARequest_SerializedBody_ContainsRecoveryCodesField()
        {
            var src = new ReplaceMFARequest(
                policy: ReplaceMFARequest.PolicyEnum.Required,
                enabledFactors: new List<ReplaceMFARequest.EnabledFactorsEnum> { ReplaceMFARequest.EnabledFactorsEnum.Email },
                isRecoveryCodesEnabled: true);

            var dst = _mapper.Map<Kinde.Api.Kiota.Management.Api.V1.Mfa.MfaPutRequestBody>(src);

            using var serWriter = new Microsoft.Kiota.Serialization.Json.JsonSerializationWriter();
            serWriter.WriteObjectValue<Kinde.Api.Kiota.Management.Api.V1.Mfa.MfaPutRequestBody>(null, dst);
            using var stream = serWriter.GetSerializedContent();
            using var reader = new System.IO.StreamReader(stream);
            var json = reader.ReadToEnd();

            _output.WriteLine("Wire body: " + json);
            Assert.Contains("\"is_recovery_codes_enabled\":true", json);
        }

        [Fact]
        public void CreateUserRequest_WithEmailIdentity_MapsDetailsThrough()
        {
            var src = new CreateUserRequest(
                profile: new CreateUserRequestProfile(givenName: "Jane", familyName: "Doe"),
                identities: new List<CreateUserRequestIdentitiesInner>
                {
                    new CreateUserRequestIdentitiesInner(
                        type: CreateUserRequestIdentitiesInner.TypeEnum.Email,
                        details: new CreateUserRequestIdentitiesInnerDetails(email: "jane@example.com"))
                });

            var dst = _mapper.Map<Kinde.Api.Kiota.Management.Api.V1.User.UserPostRequestBody>(src);

            Assert.NotNull(dst.Identities);
            Assert.Single(dst.Identities);
            Assert.Equal("jane@example.com", dst.Identities[0].Details!.Email);
        }

        [Fact]
        public void UpdateOrganizationUsersRequest_WithUsersInner_MapsArrayItems()
        {
            var src = new UpdateOrganizationUsersRequest(users: new List<UpdateOrganizationUsersRequestUsersInner>
            {
                new UpdateOrganizationUsersRequestUsersInner(
                    id: "kp_user1",
                    operation: "add",
                    roles: new List<string> { "role_admin" },
                    permissions: new List<string> { "read:users" }),
            });

            var dst = _mapper.Map<Kinde.Api.Kiota.Management.Api.V1.Organizations.Item.Users.UsersPatchRequestBody>(src);

            Assert.NotNull(dst.Users);
            Assert.Single(dst.Users);
            Assert.Equal("kp_user1", dst.Users[0].Id);
            Assert.Equal("add", dst.Users[0].Operation);
            Assert.Equal(new[] { "role_admin" }, dst.Users[0].Roles);
            Assert.Equal(new[] { "read:users" }, dst.Users[0].Permissions);
        }

        [Fact]
        public void UpdateAPIApplicationsRequest_WithApplicationsInner_MapsArrayItems()
        {
            var src = new UpdateAPIApplicationsRequest(applications: new List<UpdateAPIApplicationsRequestApplicationsInner>
            {
                new UpdateAPIApplicationsRequestApplicationsInner(id: "app_abc", operation: "add"),
            });

            var dst = _mapper.Map<Kinde.Api.Kiota.Management.Api.V1.Apis.Item.Applications.ApplicationsPatchRequestBody>(src);

            Assert.NotNull(dst.Applications);
            Assert.Single(dst.Applications);
            Assert.Equal("app_abc", dst.Applications[0].Id);
            Assert.Equal("add", dst.Applications[0].Operation);
        }

        [Fact]
        public void UpdateApplicationsPropertyRequest_StringValue_MapsToOneOfString()
        {
            var src = new UpdateApplicationsPropertyRequest(value: new UpdateApplicationsPropertyRequestValue("custom-value"));

            var dst = _mapper.Map<Kinde.Api.Kiota.Management.Api.V1.Applications.Item.Properties.Item.WithProperty_keyPutRequestBody>(src);

            Assert.NotNull(dst.Value);
            Assert.Equal("custom-value", dst.Value.String);
            Assert.Null(dst.Value.Boolean);
        }

        [Fact]
        public void UpdateApplicationsPropertyRequest_BooleanValue_MapsToOneOfBoolean()
        {
            var src = new UpdateApplicationsPropertyRequest(value: new UpdateApplicationsPropertyRequestValue(true));

            var dst = _mapper.Map<Kinde.Api.Kiota.Management.Api.V1.Applications.Item.Properties.Item.WithProperty_keyPutRequestBody>(src);

            Assert.NotNull(dst.Value);
            Assert.True(dst.Value.Boolean);
            Assert.Null(dst.Value.String);
        }

        [Fact]
        public void UpdateApplicationsPropertyRequestValue_ReverseMap_PreservesEmptyString()
        {
            var kiotaValue = new Kinde.Api.Kiota.Management.Api.V1.Applications.Item.Properties.Item.WithProperty_keyPutRequestBody.WithProperty_keyPutRequestBody_value
            {
                String = string.Empty,
            };

            var dst = _mapper.Map<UpdateApplicationsPropertyRequestValue>(kiotaValue);

            Assert.NotNull(dst);
            Assert.Equal(string.Empty, dst.ActualInstance);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void CreateOrganizationRequest_RoundTrip_PreservesAutoMembershipFlag(bool flag)
        {
            var src = new CreateOrganizationRequest(name: "Acme")
            {
                IsAutoMembershipEnabled = flag,
            };

            var kiota = _mapper.Map<Kinde.Api.Kiota.Management.Api.V1.Organization.OrganizationPostRequestBody>(src);

            Assert.NotNull(kiota);
            Assert.Equal(flag, kiota.IsAutoMembershipEnabled);

            var roundTrip = _mapper.Map<CreateOrganizationRequest>(kiota);
            Assert.Equal(flag, roundTrip.IsAutoMembershipEnabled);
        }

        [Fact]
        public void CreateOrganizationRequest_UnsetAutoMembership_StaysNullOnWire()
        {
            var src = new CreateOrganizationRequest(name: "Acme");

            var dst = _mapper.Map<Kinde.Api.Kiota.Management.Api.V1.Organization.OrganizationPostRequestBody>(src);

            Assert.Null(dst.IsAutoMembershipEnabled);

            using var serWriter = new Microsoft.Kiota.Serialization.Json.JsonSerializationWriter();
            serWriter.WriteObjectValue<Kinde.Api.Kiota.Management.Api.V1.Organization.OrganizationPostRequestBody>(null, dst);
            using var stream = serWriter.GetSerializedContent();
            using var reader = new System.IO.StreamReader(stream);
            var json = reader.ReadToEnd();

            Assert.DoesNotContain("\"is_auto_membership_enabled\"", json);
        }

        [Fact]
        public void CreateOrganizationRequest_SerializedBody_ContainsAutoMembershipField()
        {
            var src = new CreateOrganizationRequest(name: "Acme")
            {
                IsAutoMembershipEnabled = true,
            };

            var dst = _mapper.Map<Kinde.Api.Kiota.Management.Api.V1.Organization.OrganizationPostRequestBody>(src);

            using var serWriter = new Microsoft.Kiota.Serialization.Json.JsonSerializationWriter();
            serWriter.WriteObjectValue<Kinde.Api.Kiota.Management.Api.V1.Organization.OrganizationPostRequestBody>(null, dst);
            using var stream = serWriter.GetSerializedContent();
            using var reader = new System.IO.StreamReader(stream);
            var json = reader.ReadToEnd();

            _output.WriteLine("Wire body: " + json);
            Assert.Contains("\"is_auto_membership_enabled\":true", json);
        }

        [Fact]
        public void GetOrganizationResponse_ReverseSmuggling_PopulatesSuspendedFields()
        {
            var kiotaSrc = new Kinde.Api.Kiota.Management.Models.Get_organization_response
            {
                Code = "org_abc",
                Name = "Acme",
                AdditionalData = new Dictionary<string, object>
                {
                    ["is_suspended"] = true,
                    ["suspended_on"] = "2026-04-01T12:00:00Z",
                },
            };

            var openApi = _mapper.Map<GetOrganizationResponse>(kiotaSrc);

            Assert.NotNull(openApi);
            Assert.True(openApi.IsSuspended);
            Assert.Equal("2026-04-01T12:00:00Z", openApi.SuspendedOn);
        }

        [Fact]
        public void UsersResponse_WithNestedIdentitiesAndSignIns_MapsWithoutMissingTypeMap()
        {
            var kiotaSrc = new Kinde.Api.Kiota.Management.Models.Users_response
            {
                Code = "OK",
                Message = "OK",
                Users = new List<Kinde.Api.Kiota.Management.Models.Users_response_users>
                {
                    new Kinde.Api.Kiota.Management.Models.Users_response_users
                    {
                        Id = "kp_user1",
                        Email = "user1@example.com",
                        Identities = new List<Kinde.Api.Kiota.Management.Models.Users_response_users_identities>
                        {
                            new Kinde.Api.Kiota.Management.Models.Users_response_users_identities { Type = "email", Identity = "user1@example.com" },
                            new Kinde.Api.Kiota.Management.Models.Users_response_users_identities { Type = "phone", Identity = "+15555550100" },
                        },
                        LastOrganizationSignIns = new List<Kinde.Api.Kiota.Management.Models.Users_response_users_last_organization_sign_ins>
                        {
                            new Kinde.Api.Kiota.Management.Models.Users_response_users_last_organization_sign_ins
                            {
                                OrgCode = "org_abc",
                                LastSignedIn = new DateTimeOffset(2026, 4, 1, 12, 0, 0, TimeSpan.Zero),
                            },
                        },
                    },
                },
            };

            var openApi = _mapper.Map<UsersResponse>(kiotaSrc);

            Assert.NotNull(openApi);
            Assert.Single(openApi.Users);
            var user = openApi.Users[0];
            Assert.Equal("kp_user1", user.Id);
            Assert.Equal(2, user.Identities.Count);
            Assert.Equal("email", user.Identities[0].Type);
            Assert.Equal("user1@example.com", user.Identities[0].Identity);
            Assert.Single(user.LastOrganizationSignIns);
            Assert.Equal("org_abc", user.LastOrganizationSignIns[0].OrgCode);
            Assert.Equal(new DateTimeOffset(2026, 4, 1, 12, 0, 0, TimeSpan.Zero), user.LastOrganizationSignIns[0].LastSignedIn);
        }

        [Fact]
        public void GetOrganizationResponse_ForwardSmuggling_WritesSuspendedFieldsToAdditionalData()
        {
            var openApi = new GetOrganizationResponse
            {
                Code = "org_abc",
                Name = "Acme",
                IsSuspended = true,
                SuspendedOn = "2026-04-01T12:00:00Z",
            };

            var kiota = _mapper.Map<Kinde.Api.Kiota.Management.Models.Get_organization_response>(openApi);

            Assert.NotNull(kiota);
            Assert.True(kiota.AdditionalData.ContainsKey("is_suspended"));
            Assert.True(Assert.IsType<bool>(kiota.AdditionalData["is_suspended"]));
            Assert.True(kiota.AdditionalData.ContainsKey("suspended_on"));
            Assert.Equal("2026-04-01T12:00:00Z", Assert.IsType<string>(kiota.AdditionalData["suspended_on"]));
        }

        #endregion
    }
}
