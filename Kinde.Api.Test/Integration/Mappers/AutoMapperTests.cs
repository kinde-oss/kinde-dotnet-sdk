using System;
using System.Collections.Generic;
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
            // Kiota Create_user_response.Created is bool? (nullable)
            // OpenAPI CreateUserResponse.Created is bool (non-nullable)
            // When source is null, destination should get default value (false)
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
    }
}
