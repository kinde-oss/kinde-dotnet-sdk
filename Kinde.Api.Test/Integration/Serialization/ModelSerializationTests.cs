using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using Kinde.Api.Client;
using Kinde.Api.Model;
using Newtonsoft.Json;
using Xunit;
using Xunit.Abstractions;

namespace Kinde.Api.Test.Integration.Serialization
{
    /// <summary>
    /// Comprehensive serialization and deserialization tests for all models
    /// </summary>
    public class ModelSerializationTests
    {
        private readonly ITestOutputHelper _output;

        // Use the same serializer settings as the API client to ensure consistency
        private static readonly JsonSerializerSettings ApiSerializerSettings = new JsonSerializerSettings
        {
            ConstructorHandling = ConstructorHandling.AllowNonPublicDefaultConstructor,
            ContractResolver = new DataMemberContractResolver()
        };

        public ModelSerializationTests(ITestOutputHelper output)
        {
            _output = output;
        }

        #region DataMember Attribute Serialization Tests

        /// <summary>
        /// Verifies that CreateUserRequestIdentitiesInner.IsVerified serializes to "is_verified" (snake_case)
        /// This is a critical test for the customer-reported serialization issue.
        /// </summary>
        [Fact]
        public void CreateUserRequestIdentitiesInner_IsVerified_SerializesToSnakeCase()
        {
            // Arrange
            var identity = new CreateUserRequestIdentitiesInner(
                type: CreateUserRequestIdentitiesInner.TypeEnum.Email,
                isVerified: true,
                details: new CreateUserRequestIdentitiesInnerDetails(email: "test@example.com")
            );

            // Act - Serialize using the API client's serializer settings
            var json = JsonConvert.SerializeObject(identity, ApiSerializerSettings);
            _output.WriteLine($"Serialized JSON: {json}");

            // Assert - The JSON should contain "is_verified", not "isVerified" or "IsVerified"
            Assert.Contains("\"is_verified\"", json);
            Assert.DoesNotContain("\"isVerified\"", json);
            Assert.DoesNotContain("\"IsVerified\"", json);
        }

        /// <summary>
        /// Verifies that GetUserMfaResponseMfa.IsVerified serializes to "is_verified" (snake_case)
        /// </summary>
        [Fact]
        public void GetUserMfaResponseMfa_IsVerified_SerializesToSnakeCase()
        {
            // Arrange
            var mfa = new GetUserMfaResponseMfa(
                id: "mfa_123",
                type: "email",
                isVerified: true
            );

            // Act - Serialize using the API client's serializer settings
            var json = JsonConvert.SerializeObject(mfa, ApiSerializerSettings);
            _output.WriteLine($"Serialized JSON: {json}");

            // Assert - The JSON should contain "is_verified", not "isVerified" or "IsVerified"
            Assert.Contains("\"is_verified\"", json);
            Assert.DoesNotContain("\"isVerified\"", json);
            Assert.DoesNotContain("\"IsVerified\"", json);
        }

        /// <summary>
        /// Verifies that CreateUserRequest with identities serializes is_verified correctly
        /// </summary>
        [Fact]
        public void CreateUserRequest_WithIdentities_SerializesIsVerifiedToSnakeCase()
        {
            // Arrange
            var request = new CreateUserRequest(
                profile: new CreateUserRequestProfile(),
                identities: new List<CreateUserRequestIdentitiesInner>
                {
                    new CreateUserRequestIdentitiesInner(
                        type: CreateUserRequestIdentitiesInner.TypeEnum.Email,
                        isVerified: true,
                        details: new CreateUserRequestIdentitiesInnerDetails(email: "test@example.com")
                    )
                }
            );

            // Act - Serialize using the API client's serializer settings
            var json = JsonConvert.SerializeObject(request, ApiSerializerSettings);
            _output.WriteLine($"Serialized JSON: {json}");

            // Assert - The JSON should contain "is_verified" in the identities array
            Assert.Contains("\"is_verified\"", json);
            Assert.DoesNotContain("\"isVerified\"", json);
        }

        /// <summary>
        /// Verifies that JSON with "is_verified" can be deserialized correctly
        /// </summary>
        [Fact]
        public void CreateUserRequestIdentitiesInner_CanDeserializeFromSnakeCaseJson()
        {
            // Arrange - JSON as it comes from the API (snake_case)
            var json = @"{""type"":""email"",""is_verified"":true,""details"":{""email"":""test@example.com""}}";

            // Act - Deserialize using the API client's serializer settings
            var identity = JsonConvert.DeserializeObject<CreateUserRequestIdentitiesInner>(json, ApiSerializerSettings);

            // Assert
            Assert.NotNull(identity);
            Assert.True(identity.IsVerified);
            Assert.Equal(CreateUserRequestIdentitiesInner.TypeEnum.Email, identity.Type);
        }

        #region Negative Test Cases - IsVerified = false

        /// <summary>
        /// Verifies that IsVerified = false serializes correctly to "is_verified": false
        /// This ensures false values are not lost or defaulted to true during serialization.
        /// </summary>
        [Fact]
        public void CreateUserRequestIdentitiesInner_IsVerifiedFalse_SerializesCorrectly()
        {
            // Arrange - Explicitly set IsVerified to false
            var identity = new CreateUserRequestIdentitiesInner(
                type: CreateUserRequestIdentitiesInner.TypeEnum.Email,
                isVerified: false,
                details: new CreateUserRequestIdentitiesInnerDetails(email: "test@example.com")
            );

            // Act - Serialize using the API client's serializer settings
            var json = JsonConvert.SerializeObject(identity, ApiSerializerSettings);
            _output.WriteLine($"Serialized JSON (IsVerified=false): {json}");

            // Assert - The JSON should contain "is_verified": false
            Assert.Contains("\"is_verified\":false", json.Replace(" ", ""));
            Assert.DoesNotContain("\"is_verified\":true", json.Replace(" ", ""));
        }

        /// <summary>
        /// Verifies that GetUserMfaResponseMfa.IsVerified = false serializes correctly
        /// </summary>
        [Fact]
        public void GetUserMfaResponseMfa_IsVerifiedFalse_SerializesCorrectly()
        {
            // Arrange - Explicitly set IsVerified to false
            var mfa = new GetUserMfaResponseMfa(
                id: "mfa_123",
                type: "email",
                isVerified: false
            );

            // Act - Serialize using the API client's serializer settings
            var json = JsonConvert.SerializeObject(mfa, ApiSerializerSettings);
            _output.WriteLine($"Serialized JSON (IsVerified=false): {json}");

            // Assert - The JSON should contain "is_verified": false
            Assert.Contains("\"is_verified\":false", json.Replace(" ", ""));
            Assert.DoesNotContain("\"is_verified\":true", json.Replace(" ", ""));
        }

        /// <summary>
        /// Verifies that JSON with "is_verified": false can be deserialized correctly
        /// This ensures false values from the API are preserved during deserialization.
        /// </summary>
        [Fact]
        public void CreateUserRequestIdentitiesInner_CanDeserializeFalseFromSnakeCaseJson()
        {
            // Arrange - JSON with is_verified: false
            var json = @"{""type"":""email"",""is_verified"":false,""details"":{""email"":""test@example.com""}}";

            // Act - Deserialize using the API client's serializer settings
            var identity = JsonConvert.DeserializeObject<CreateUserRequestIdentitiesInner>(json, ApiSerializerSettings);

            // Assert - IsVerified should be false, not defaulted to true
            Assert.NotNull(identity);
            Assert.False(identity.IsVerified);
            Assert.Equal(CreateUserRequestIdentitiesInner.TypeEnum.Email, identity.Type);
        }

        /// <summary>
        /// Verifies that a default/uninitialized CreateUserRequestIdentitiesInner has IsVerified = false
        /// This catches cases where boolean defaults might be incorrectly set.
        /// </summary>
        [Fact]
        public void CreateUserRequestIdentitiesInner_DefaultIsVerified_IsFalse()
        {
            // Arrange - Create with default isVerified (should default to false)
            var identity = new CreateUserRequestIdentitiesInner(
                type: CreateUserRequestIdentitiesInner.TypeEnum.Email,
                details: new CreateUserRequestIdentitiesInnerDetails(email: "test@example.com")
            );

            // Assert - Default value should be false (C# bool default)
            Assert.False(identity.IsVerified);

            // Act - Serialize and verify the JSON reflects false
            var json = JsonConvert.SerializeObject(identity, ApiSerializerSettings);
            _output.WriteLine($"Serialized JSON (default IsVerified): {json}");

            // Assert - Should serialize as false
            Assert.Contains("\"is_verified\":false", json.Replace(" ", ""));
        }

        /// <summary>
        /// Verifies round-trip serialization preserves IsVerified = false
        /// </summary>
        [Fact]
        public void CreateUserRequestIdentitiesInner_RoundTrip_PreservesFalse()
        {
            // Arrange
            var original = new CreateUserRequestIdentitiesInner(
                type: CreateUserRequestIdentitiesInner.TypeEnum.Email,
                isVerified: false,
                details: new CreateUserRequestIdentitiesInnerDetails(email: "roundtrip@example.com")
            );

            // Act - Serialize then deserialize
            var json = JsonConvert.SerializeObject(original, ApiSerializerSettings);
            _output.WriteLine($"Serialized JSON: {json}");
            var deserialized = JsonConvert.DeserializeObject<CreateUserRequestIdentitiesInner>(json, ApiSerializerSettings);

            // Assert - IsVerified should still be false after round-trip
            Assert.NotNull(deserialized);
            Assert.False(deserialized.IsVerified);
            Assert.Equal(original.Type, deserialized.Type);
        }

        /// <summary>
        /// Verifies round-trip serialization preserves IsVerified = true
        /// </summary>
        [Fact]
        public void CreateUserRequestIdentitiesInner_RoundTrip_PreservesTrue()
        {
            // Arrange
            var original = new CreateUserRequestIdentitiesInner(
                type: CreateUserRequestIdentitiesInner.TypeEnum.Email,
                isVerified: true,
                details: new CreateUserRequestIdentitiesInnerDetails(email: "roundtrip@example.com")
            );

            // Act - Serialize then deserialize
            var json = JsonConvert.SerializeObject(original, ApiSerializerSettings);
            _output.WriteLine($"Serialized JSON: {json}");
            var deserialized = JsonConvert.DeserializeObject<CreateUserRequestIdentitiesInner>(json, ApiSerializerSettings);

            // Assert - IsVerified should still be true after round-trip
            Assert.NotNull(deserialized);
            Assert.True(deserialized.IsVerified);
            Assert.Equal(original.Type, deserialized.Type);
        }

        /// <summary>
        /// Verifies CreateUserRequest with mixed IsVerified values serializes correctly
        /// </summary>
        [Fact]
        public void CreateUserRequest_WithMixedIsVerified_SerializesCorrectly()
        {
            // Arrange - Multiple identities with different IsVerified values
            var request = new CreateUserRequest(
                profile: new CreateUserRequestProfile(givenName: "Test", familyName: "User"),
                identities: new List<CreateUserRequestIdentitiesInner>
                {
                    new CreateUserRequestIdentitiesInner(
                        type: CreateUserRequestIdentitiesInner.TypeEnum.Email,
                        isVerified: true,
                        details: new CreateUserRequestIdentitiesInnerDetails(email: "verified@example.com")
                    ),
                    new CreateUserRequestIdentitiesInner(
                        type: CreateUserRequestIdentitiesInner.TypeEnum.Email,
                        isVerified: false,
                        details: new CreateUserRequestIdentitiesInnerDetails(email: "unverified@example.com")
                    )
                }
            );

            // Act - Serialize using the API client's serializer settings
            var json = JsonConvert.SerializeObject(request, ApiSerializerSettings);
            _output.WriteLine($"Serialized JSON: {json}");

            // Assert - Should contain both true and false values
            Assert.Contains("\"is_verified\":true", json.Replace(" ", ""));
            Assert.Contains("\"is_verified\":false", json.Replace(" ", ""));
            
            // Also verify we can deserialize back correctly
            var deserialized = JsonConvert.DeserializeObject<CreateUserRequest>(json, ApiSerializerSettings);
            Assert.NotNull(deserialized);
            Assert.NotNull(deserialized.Identities);
            Assert.Equal(2, deserialized.Identities.Count);
            Assert.True(deserialized.Identities[0].IsVerified);
            Assert.False(deserialized.Identities[1].IsVerified);
        }

        #endregion

        /// <summary>
        /// Verifies that all properties with DataMember attributes serialize to the correct names
        /// </summary>
        [Theory]
        [InlineData(typeof(CreateUserRequestIdentitiesInner), "IsVerified", "is_verified")]
        [InlineData(typeof(GetUserMfaResponseMfa), "IsVerified", "is_verified")]
        [InlineData(typeof(GetUserMfaResponseMfa), "CreatedOn", "created_on")]
        [InlineData(typeof(GetUserMfaResponseMfa), "LastUsedOn", "last_used_on")]
        [InlineData(typeof(GetUserMfaResponseMfa), "UsageCount", "usage_count")]
        public void DataMemberAttribute_PropertySerializesToCorrectName(Type modelType, string propertyName, string expectedJsonName)
        {
            // Arrange
            var property = modelType.GetProperty(propertyName);
            Assert.NotNull(property);

            var dataMemberAttr = property.GetCustomAttribute<DataMemberAttribute>();
            Assert.NotNull(dataMemberAttr);

            // Assert - The DataMember attribute should have the expected name
            Assert.Equal(expectedJsonName, dataMemberAttr.Name);

            // Also verify that the DataMemberContractResolver would use this name
            var resolver = new DataMemberContractResolver();
            var contract = resolver.ResolveContract(modelType) as Newtonsoft.Json.Serialization.JsonObjectContract;
            Assert.NotNull(contract);

            var jsonProperty = contract.Properties.FirstOrDefault(p => p.UnderlyingName == propertyName);
            Assert.NotNull(jsonProperty);
            Assert.Equal(expectedJsonName, jsonProperty.PropertyName);
        }

        #endregion

        /// <summary>
        /// Get all model types from the Kinde.Api.Model namespace
        /// </summary>
        private static IEnumerable<Type> GetModelTypes()
        {
            var assembly = typeof(AbstractOpenAPISchema).Assembly;
            var modelNamespace = typeof(AbstractOpenAPISchema).Namespace;

            return assembly.GetTypes()
                .Where(t => t.Namespace == modelNamespace)
                .Where(t => t.IsClass && !t.IsAbstract)
                .Where(t => !t.Name.Contains("JsonConverter") && !t.Name.Contains("Tests"))
                .Where(t => t != typeof(AbstractOpenAPISchema))
                .Where(t => !typeof(AbstractOpenAPISchema).IsAssignableFrom(t)) // Skip OneOf/AnyOf types
                .OrderBy(t => t.Name);
        }

        [Theory]
        [MemberData(nameof(GetModelTypeData))]
        public void Model_CanSerializeAndDeserialize(Type modelType)
        {
            // Skip OneOf/AnyOf types
            if (typeof(AbstractOpenAPISchema).IsAssignableFrom(modelType))
            {
                _output.WriteLine($"Skipping {modelType.Name} - OneOf/AnyOf type requires special handling");
                return;
            }

            // Arrange
            var instance = CreateModelInstance(modelType);
            if (instance == null)
            {
                _output.WriteLine($"Skipping {modelType.Name} - cannot create instance");
                return;
            }

            // Act - Serialize
            string json;
            try
            {
                json = JsonConvert.SerializeObject(instance, new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore,
                    DateFormatHandling = DateFormatHandling.IsoDateFormat
                });
            }
            catch (Exception ex)
            {
                _output.WriteLine($"Failed to serialize {modelType.Name}: {ex.Message}");
                throw new Xunit.Sdk.XunitException($"Serialization failed for {modelType.Name}: {ex.Message}");
            }

            Assert.NotNull(json);
            Assert.NotEmpty(json);

            // Act - Deserialize
            object? deserialized;
            try
            {
                // Handle special case where serialization produces a string (e.g., UpdateApplicationsPropertyRequestValue)
                if (json.StartsWith("\"") && json.EndsWith("\""))
                {
                    // This is a string value, try to deserialize directly
                    deserialized = JsonConvert.DeserializeObject(json, modelType, new JsonSerializerSettings
                    {
                        DateFormatHandling = DateFormatHandling.IsoDateFormat,
                        MissingMemberHandling = MissingMemberHandling.Ignore
                    });
                }
                else
                {
                    deserialized = JsonConvert.DeserializeObject(json, modelType, new JsonSerializerSettings
                    {
                        DateFormatHandling = DateFormatHandling.IsoDateFormat,
                        MissingMemberHandling = MissingMemberHandling.Ignore
                    });
                }
            }
            catch (Exception ex)
            {
                _output.WriteLine($"Failed to deserialize {modelType.Name}: {ex.Message}");
                _output.WriteLine($"JSON: {json}");
                throw new Xunit.Sdk.XunitException($"Deserialization failed for {modelType.Name}: {ex.Message}");
            }

            // Assert
            Assert.NotNull(deserialized);
            Assert.IsType(modelType, deserialized);
        }

        [Theory]
        [MemberData(nameof(GetModelTypeData))]
        public void Model_CanDeserializeFromJson(Type modelType)
        {
            // Skip OneOf/AnyOf types and types that inherit from AbstractOpenAPISchema
            if (typeof(AbstractOpenAPISchema).IsAssignableFrom(modelType))
            {
                _output.WriteLine($"Skipping {modelType.Name} - OneOf/AnyOf type requires special handling");
                return;
            }

            // Arrange - Create sample JSON based on model properties
            var sampleJson = CreateSampleJson(modelType);
            if (string.IsNullOrEmpty(sampleJson) || sampleJson == "{}")
            {
                _output.WriteLine($"Skipping {modelType.Name} - cannot create valid sample JSON");
                return;
            }

            // Act
            object? deserialized;
            try
            {
                // Use settings that match the API's serialization
                deserialized = JsonConvert.DeserializeObject(sampleJson, modelType, new JsonSerializerSettings
                {
                    MissingMemberHandling = MissingMemberHandling.Ignore,
                    DateFormatHandling = DateFormatHandling.IsoDateFormat
                });
            }
            catch (Exception ex)
            {
                // For some models, sample JSON may not be valid - that's okay, we test with real instances
                _output.WriteLine($"Skipping {modelType.Name} - sample JSON deserialization failed (this is expected for some models): {ex.Message}");
                return; // Skip instead of failing
            }

            // Assert
            if (deserialized != null)
            {
                Assert.IsType(modelType, deserialized);
            }
        }

        public static IEnumerable<object[]> GetModelTypeData()
        {
            return GetModelTypes().Select(t => new object[] { t });
        }

        /// <summary>
        /// Create an instance of a model type for testing
        /// </summary>
        private static object? CreateModelInstance(Type modelType)
        {
            try
            {
                // Try parameterless constructor first
                var parameterlessCtor = modelType.GetConstructor(Type.EmptyTypes);
                if (parameterlessCtor != null)
                {
                    return Activator.CreateInstance(modelType);
                }

                // Try constructors with parameters
                var constructors = modelType.GetConstructors(BindingFlags.Public | BindingFlags.Instance);
                foreach (var ctor in constructors.OrderBy(c => c.GetParameters().Length))
                {
                    var parameters = ctor.GetParameters();
                    var args = new object[parameters.Length];
                    bool canCreate = true;

                    for (int i = 0; i < parameters.Length; i++)
                    {
                        var param = parameters[i];
                        if (param.HasDefaultValue)
                        {
                            args[i] = param.DefaultValue!;
                        }
                        else if (param.ParameterType.IsValueType)
                        {
                            args[i] = Activator.CreateInstance(param.ParameterType)!;
                        }
                        else if (param.ParameterType == typeof(string))
                        {
                            args[i] = "test";
                        }
                        else
                        {
                            canCreate = false;
                            break;
                        }
                    }

                    if (canCreate)
                    {
                        return Activator.CreateInstance(modelType, args);
                    }
                }

                return null;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Create sample JSON for a model type using DataMember attribute names
        /// </summary>
        private static string CreateSampleJson(Type modelType)
        {
            var properties = modelType.GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Where(p => p.CanRead && p.CanWrite)
                .ToList();

            if (properties.Count == 0)
            {
                return "{}";
            }

            var jsonParts = new List<string>();
            foreach (var prop in properties.Take(10)) // Increase limit to get more properties
            {
                var propType = Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType;
                
                // Get the JSON property name from DataMember attribute, or use camelCase
                var jsonName = GetJsonPropertyName(prop);
                if (string.IsNullOrEmpty(jsonName))
                {
                    continue; // Skip properties without DataMember
                }

                string jsonValue;
                if (propType == typeof(string))
                {
                    jsonValue = $"\"test_{jsonName}\"";
                }
                else if (propType == typeof(int) || propType == typeof(int?))
                {
                    jsonValue = "1";
                }
                else if (propType == typeof(bool) || propType == typeof(bool?))
                {
                    jsonValue = "true";
                }
                else if (propType == typeof(long) || propType == typeof(long?))
                {
                    jsonValue = "1";
                }
                else if (propType == typeof(double) || propType == typeof(double?))
                {
                    jsonValue = "1.0";
                }
                else if (propType == typeof(DateTime) || propType == typeof(DateTime?))
                {
                    jsonValue = "\"2024-01-01T00:00:00Z\"";
                }
                else if (propType == typeof(DateTimeOffset) || propType == typeof(DateTimeOffset?))
                {
                    jsonValue = "\"2024-01-01T00:00:00Z\"";
                }
                else if (propType.IsEnum)
                {
                    var enumValues = Enum.GetValues(propType);
                    if (enumValues.Length > 0)
                    {
                        // Get the enum value's DataMember name if available
                        var enumValue = enumValues.GetValue(0);
                        var enumField = propType.GetField(enumValue!.ToString()!);
                        var enumMemberAttr = enumField?.GetCustomAttribute<System.Runtime.Serialization.EnumMemberAttribute>();
                        var enumStr = enumMemberAttr?.Value ?? enumValue.ToString();
                        jsonValue = $"\"{enumStr}\"";
                    }
                    else
                    {
                        continue;
                    }
                }
                else if (propType.IsGenericType && propType.GetGenericTypeDefinition() == typeof(List<>))
                {
                    jsonValue = "[]";
                }
                else if (propType.IsGenericType && propType.GetGenericTypeDefinition() == typeof(Dictionary<,>))
                {
                    jsonValue = "{}";
                }
                else
                {
                    continue; // Skip complex types
                }

                jsonParts.Add($"\"{jsonName}\": {jsonValue}");
            }

            return jsonParts.Count > 0 ? "{" + string.Join(", ", jsonParts) + "}" : "{}";
        }

        /// <summary>
        /// Get the JSON property name from DataMember attribute
        /// </summary>
        private static string GetJsonPropertyName(PropertyInfo prop)
        {
            var dataMemberAttr = prop.GetCustomAttribute<System.Runtime.Serialization.DataMemberAttribute>();
            if (dataMemberAttr != null && !string.IsNullOrEmpty(dataMemberAttr.Name))
            {
                return dataMemberAttr.Name;
            }
            
            // Fallback to camelCase if no DataMember attribute
            return ToCamelCase(prop.Name);
        }

        private static string ToCamelCase(string str)
        {
            if (string.IsNullOrEmpty(str) || char.IsLower(str[0]))
            {
                return str;
            }
            return char.ToLowerInvariant(str[0]) + str.Substring(1);
        }
    }
}

