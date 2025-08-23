using System;
using System.IO;
using Kinde.Accounts.Model;
using Newtonsoft.Json.Linq;
using Xunit;

namespace Kinde.Accounts.Test.Model
{
    /// <summary>
    /// Tests for FeatureFlagValue.FromJson method to ensure proper oneOf type handling
    /// </summary>
    public class FeatureFlagValueFromJsonTests
    {
        [Theory]
        [InlineData("true", typeof(bool))]
        [InlineData("false", typeof(bool))]
        public void FromJson_BooleanValues_ReturnsExpectedType(string json, Type expectedType)
        {
            // Act
            var result = FeatureFlagValue.FromJson(json);
            
            // Assert
            Assert.NotNull(result);
            Assert.NotNull(result.ActualInstance);
            Assert.Equal(expectedType, result.ActualInstance.GetType());
        }

        [Theory]
        [InlineData("\"feature_enabled\"", typeof(string))]
        [InlineData("\"premium\"", typeof(string))]
        [InlineData("\"\"", typeof(string))]
        public void FromJson_StringValues_ReturnsExpectedType(string json, Type expectedType)
        {
            // Act
            var result = FeatureFlagValue.FromJson(json);
            
            // Assert
            Assert.NotNull(result);
            Assert.NotNull(result.ActualInstance);
            Assert.Equal(expectedType, result.ActualInstance.GetType());
        }

        [Theory]
        [InlineData("42", typeof(decimal))]
        [InlineData("3.14", typeof(decimal))]
        [InlineData("-123", typeof(decimal))]
        [InlineData("0", typeof(decimal))]
        public void FromJson_NumberValues_ReturnsExpectedType(string json, Type expectedType)
        {
            // Act
            var result = FeatureFlagValue.FromJson(json);
            
            // Assert
            Assert.NotNull(result);
            Assert.NotNull(result.ActualInstance);
            Assert.Equal(expectedType, result.ActualInstance.GetType());
        }

        [Theory]
        [InlineData("{\"key\": \"value\"}", typeof(JObject))]
        [InlineData("{\"enabled\": true, \"limit\": 100}", typeof(JObject))]
        [InlineData("{}", typeof(JObject))]
        public void FromJson_ObjectValues_ReturnsExpectedType(string json, Type expectedType)
        {
            // Act
            var result = FeatureFlagValue.FromJson(json);
            
            // Assert
            Assert.NotNull(result);
            Assert.NotNull(result.ActualInstance);
            Assert.Equal(expectedType, result.ActualInstance.GetType());
        }

        [Theory]
        [InlineData("[1, 2, 3]")]
        [InlineData("[\"a\", \"b\", \"c\"]")]
        [InlineData("[]")]
        public void FromJson_ArrayValues_ThrowsException(string json)
        {
            // Act & Assert
            Assert.Throws<InvalidDataException>(() => FeatureFlagValue.FromJson(json));
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        public void FromJson_NullOrEmpty_ReturnsNull(string json)
        {
            // Act
            var result = FeatureFlagValue.FromJson(json);
            
            // Assert
            Assert.Null(result);
        }

        [Theory]
        [InlineData("true", true)]
        [InlineData("false", false)]
        public void FromJson_BooleanValues_ReturnsCorrectValue(string json, bool expectedValue)
        {
            // Act
            var result = FeatureFlagValue.FromJson(json);
            
            // Assert
            Assert.NotNull(result);
            Assert.NotNull(result.ActualInstance);
            Assert.Equal(expectedValue, result.ActualInstance);
        }

        [Theory]
        [InlineData("\"feature_enabled\"", "feature_enabled")]
        [InlineData("\"premium\"", "premium")]
        public void FromJson_StringValues_ReturnsCorrectValue(string json, string expectedValue)
        {
            // Act
            var result = FeatureFlagValue.FromJson(json);
            
            // Assert
            Assert.NotNull(result);
            Assert.NotNull(result.ActualInstance);
            Assert.Equal(expectedValue, result.ActualInstance);
        }

        [Theory]
        [InlineData("42", 42)]
        [InlineData("3.14", 3.14)]
        [InlineData("-123", -123)]
        public void FromJson_NumberValues_ReturnsCorrectValue(string json, decimal expectedValue)
        {
            // Act
            var result = FeatureFlagValue.FromJson(json);
            
            // Assert
            Assert.NotNull(result);
            Assert.NotNull(result.ActualInstance);
            Assert.Equal(expectedValue, result.ActualInstance);
        }

        [Fact]
        public void FromJson_InvalidJson_ThrowsException()
        {
            // Arrange
            var invalidJson = "{ invalid json }";
            
            // Act & Assert
            Assert.ThrowsAny<Exception>(() => FeatureFlagValue.FromJson(invalidJson));
        }

        [Fact]
        public void FromJson_ComplexObject_DeserializesCorrectly()
        {
            // Arrange
            var json = "{\"enabled\": true, \"limit\": 100, \"name\": \"premium_feature\"}";
            
            // Act
            var result = FeatureFlagValue.FromJson(json);
            
            // Assert
            Assert.NotNull(result);
            Assert.NotNull(result.ActualInstance);
            Assert.Equal(typeof(JObject), result.ActualInstance.GetType());
        }

        [Fact]
        public void FromJson_ComplexArray_ThrowsException()
        {
            // Arrange
            var json = "[{\"id\": 1, \"name\": \"item1\"}, {\"id\": 2, \"name\": \"item2\"}]";
            
            // Act & Assert
            Assert.Throws<InvalidDataException>(() => FeatureFlagValue.FromJson(json));
        }
    }
}
