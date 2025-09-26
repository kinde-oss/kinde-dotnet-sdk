using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
using Xunit;
using Kinde.Api.Converters;
using Kinde.Api.Model;
using Kinde.Api.Client;

namespace Kinde.Api.Test.Converters
{
    /// <summary>
    /// Simple unit tests for custom enum serialization converters
    /// </summary>
    public class SimpleEnumSerializationTests
    {
        #region Basic Converter Tests

        [Fact]
        public void GenericEnumConverter_CanConvert_ValidEnumType_ReturnsTrue()
        {
            // Arrange
            var converter = new GenericEnumConverter();
            var enumType = typeof(CreateUserRequestIdentitiesInner.TypeEnum);

            // Act
            var canConvert = converter.CanConvert(enumType);

            // Assert
            Assert.True(canConvert);
        }

        [Fact]
        public void GenericEnumConverter_CanConvert_InvalidType_ReturnsFalse()
        {
            // Arrange
            var converter = new GenericEnumConverter();
            var invalidType = typeof(string);

            // Act
            var canConvert = converter.CanConvert(invalidType);

            // Assert
            Assert.False(canConvert);
        }

        [Fact]
        public void CustomEnumConverter_Exists_AndIsCorrectType()
        {
            // Arrange & Act
            var converter = new CustomEnumConverter();

            // Assert
            Assert.NotNull(converter);
            Assert.True(converter is System.Text.Json.Serialization.JsonConverter<CreateUserRequestIdentitiesInner.TypeEnum>);
        }

        [Fact]
        public void NewtonsoftGenericEnumConverter_Exists_AndIsCorrectType()
        {
            // Arrange & Act
            var converter = new NewtonsoftGenericEnumConverter();

            // Assert
            Assert.NotNull(converter);
            Assert.True(converter is Newtonsoft.Json.JsonConverter);
        }

        [Fact]
        public void OptionNewtonsoftConverter_Exists_AndIsCorrectType()
        {
            // Arrange & Act
            var converter = new OptionNewtonsoftConverter();

            // Assert
            Assert.NotNull(converter);
            Assert.True(converter is Newtonsoft.Json.JsonConverter);
        }

        #endregion

        #region Enum Method Tests

        [Fact]
        public void CreateUserRequestIdentitiesInner_TypeEnumFromString_ValidValues_Work()
        {
            // Act & Assert
            Assert.Equal(CreateUserRequestIdentitiesInner.TypeEnum.Email, 
                CreateUserRequestIdentitiesInner.TypeEnumFromString("email"));
            Assert.Equal(CreateUserRequestIdentitiesInner.TypeEnum.Phone, 
                CreateUserRequestIdentitiesInner.TypeEnumFromString("phone"));
            Assert.Equal(CreateUserRequestIdentitiesInner.TypeEnum.Username, 
                CreateUserRequestIdentitiesInner.TypeEnumFromString("username"));
        }

        [Fact]
        public void CreateUserRequestIdentitiesInner_TypeEnumToJsonValue_ValidValues_Work()
        {
            // Act & Assert
            Assert.Equal("email", 
                CreateUserRequestIdentitiesInner.TypeEnumToJsonValue(CreateUserRequestIdentitiesInner.TypeEnum.Email));
            Assert.Equal("phone", 
                CreateUserRequestIdentitiesInner.TypeEnumToJsonValue(CreateUserRequestIdentitiesInner.TypeEnum.Phone));
            Assert.Equal("username", 
                CreateUserRequestIdentitiesInner.TypeEnumToJsonValue(CreateUserRequestIdentitiesInner.TypeEnum.Username));
        }

        [Fact]
        public void CreateUserIdentityRequest_TypeEnumFromString_ValidValues_Work()
        {
            // Act & Assert
            Assert.Equal(CreateUserIdentityRequest.TypeEnum.Email, 
                CreateUserIdentityRequest.TypeEnumFromString("email"));
            Assert.Equal(CreateUserIdentityRequest.TypeEnum.Username, 
                CreateUserIdentityRequest.TypeEnumFromString("username"));
            Assert.Equal(CreateUserIdentityRequest.TypeEnum.Phone, 
                CreateUserIdentityRequest.TypeEnumFromString("phone"));
            Assert.Equal(CreateUserIdentityRequest.TypeEnum.Enterprise, 
                CreateUserIdentityRequest.TypeEnumFromString("enterprise"));
            Assert.Equal(CreateUserIdentityRequest.TypeEnum.Social, 
                CreateUserIdentityRequest.TypeEnumFromString("social"));
        }

        [Fact]
        public void CreateApplicationRequest_TypeEnumFromString_ValidValues_Work()
        {
            // Act & Assert
            Assert.Equal(CreateApplicationRequest.TypeEnum.Reg, 
                CreateApplicationRequest.TypeEnumFromString("reg"));
            Assert.Equal(CreateApplicationRequest.TypeEnum.Spa, 
                CreateApplicationRequest.TypeEnumFromString("spa"));
            Assert.Equal(CreateApplicationRequest.TypeEnum.M2m, 
                CreateApplicationRequest.TypeEnumFromString("m2m"));
            Assert.Equal(CreateApplicationRequest.TypeEnum.Device, 
                CreateApplicationRequest.TypeEnumFromString("device"));
        }

        #endregion

        #region JsonConverter Attribute Tests

        [Fact]
        public void CreateUserRequestIdentitiesInner_TypeProperty_HasGenericEnumConverterAttribute()
        {
            // Arrange
            var property = typeof(CreateUserRequestIdentitiesInner).GetProperty("Type");
            
            // Act
            var converterAttribute = property?.GetCustomAttributes(typeof(System.Text.Json.Serialization.JsonConverterAttribute), false)
                .FirstOrDefault() as System.Text.Json.Serialization.JsonConverterAttribute;

            // Assert
            Assert.NotNull(converterAttribute);
            Assert.Equal(typeof(GenericEnumConverter), converterAttribute.ConverterType);
        }

        [Fact]
        public void CreateUserIdentityRequest_TypeProperty_HasGenericEnumConverterAttribute()
        {
            // Arrange
            var property = typeof(CreateUserIdentityRequest).GetProperty("Type");
            
            // Act
            var converterAttribute = property?.GetCustomAttributes(typeof(System.Text.Json.Serialization.JsonConverterAttribute), false)
                .FirstOrDefault() as System.Text.Json.Serialization.JsonConverterAttribute;

            // Assert
            Assert.NotNull(converterAttribute);
            Assert.Equal(typeof(GenericEnumConverter), converterAttribute.ConverterType);
        }

        [Fact]
        public void CreateApplicationRequest_TypeProperty_HasGenericEnumConverterAttribute()
        {
            // Arrange
            var property = typeof(CreateApplicationRequest).GetProperty("Type");
            
            // Act
            var converterAttribute = property?.GetCustomAttributes(typeof(System.Text.Json.Serialization.JsonConverterAttribute), false)
                .FirstOrDefault() as System.Text.Json.Serialization.JsonConverterAttribute;

            // Assert
            Assert.NotNull(converterAttribute);
            Assert.Equal(typeof(GenericEnumConverter), converterAttribute.ConverterType);
        }

        #endregion

        #region Basic Serialization Tests

        [Fact]
        public void CreateUserRequestIdentitiesInner_Serialization_Works()
        {
            // Arrange - Create a complete JSON that includes all properties
            var json = """{"type":"email","is_verified":true}""";

            // Act - Deserialize using the custom converter
            var options = new System.Text.Json.JsonSerializerOptions();
            options.Converters.Add(new CreateUserRequestIdentitiesInnerJsonConverter());
            var deserialized = System.Text.Json.JsonSerializer.Deserialize<CreateUserRequestIdentitiesInner>(json, options);

            // Assert
            Assert.NotNull(deserialized);
            Assert.Equal(CreateUserRequestIdentitiesInner.TypeEnum.Email, deserialized.Type);
            Assert.True(deserialized.IsVerified);

            // Act - Serialize back to verify enum serialization
            var serializedJson = System.Text.Json.JsonSerializer.Serialize(deserialized);
            
            // Assert - Serialization contains correct enum value
            Assert.Contains("\"type\":\"email\"", serializedJson);
            Assert.Contains("\"is_verified\":true", serializedJson);
        }

        [Fact]
        public void CreateUserIdentityRequest_Serialization_Works()
        {
            // Arrange - Create a complete JSON that includes all properties
            var json = """{"value":"test@example.com","type":"email"}""";

            // Act - Deserialize using the custom converter
            var options = new System.Text.Json.JsonSerializerOptions();
            options.Converters.Add(new CreateUserIdentityRequestJsonConverter());
            var deserialized = System.Text.Json.JsonSerializer.Deserialize<CreateUserIdentityRequest>(json, options);

            // Assert
            Assert.NotNull(deserialized);
            Assert.Equal("test@example.com", deserialized.Value);
            Assert.Equal(CreateUserIdentityRequest.TypeEnum.Email, deserialized.Type);

            // Act - Serialize back to verify enum serialization
            var serializedJson = System.Text.Json.JsonSerializer.Serialize(deserialized);
            
            // Assert - Serialization contains correct enum value
            Assert.Contains("\"type\":\"email\"", serializedJson);
            Assert.Contains("\"value\":\"test@example.com\"", serializedJson);
        }

        [Fact]
        public void CreateApplicationRequest_Serialization_Works()
        {
            // Arrange
            var request = new CreateApplicationRequest("Test App", CreateApplicationRequest.TypeEnum.Spa);

            // Act - Serialize
            var json = System.Text.Json.JsonSerializer.Serialize(request);
            
            // Assert - Serialization contains correct enum value
            Assert.Contains("\"type\":\"spa\"", json);
            Assert.Contains("\"name\":\"Test App\"", json);

            // Act - Deserialize using the custom converter
            var options = new System.Text.Json.JsonSerializerOptions();
            options.Converters.Add(new CreateApplicationRequestJsonConverter());
            var deserialized = System.Text.Json.JsonSerializer.Deserialize<CreateApplicationRequest>(json, options);

            // Assert
            Assert.NotNull(deserialized);
            Assert.Equal("Test App", deserialized.Name);
            Assert.Equal(CreateApplicationRequest.TypeEnum.Spa, deserialized.Type);
        }

        #endregion

        #region Error Handling Tests

        [Fact]
        public void CreateUserRequestIdentitiesInner_TypeEnumFromString_InvalidValue_ThrowsException()
        {
            // Act & Assert
            Assert.Throws<NotImplementedException>(() => 
                CreateUserRequestIdentitiesInner.TypeEnumFromString("invalid"));
        }

        [Fact]
        public void CreateUserRequestIdentitiesInner_TypeEnumToJsonValue_InvalidEnum_ThrowsException()
        {
            // Act & Assert
            Assert.Throws<NotImplementedException>(() => 
                CreateUserRequestIdentitiesInner.TypeEnumToJsonValue((CreateUserRequestIdentitiesInner.TypeEnum)999));
        }

        #endregion
    }
}
