using System.Collections.Generic;
using System.Linq;
using Xunit;
using Kinde.Api.Client;

namespace Kinde.Api.Test
{
    /// <summary>
    /// Tests for dictionary serialization behavior in ClientUtils.ParameterToMultiMap
    /// </summary>
    public class DictionarySerializationTests
    {
        [Fact]
        public void Dictionary_UsesIdictionaryBranch_WhenCollectionFormatIsMulti()
        {
            // Arrange
            var dictionary = new Dictionary<string, string>
            {
                { "key1", "value1" },
                { "key2", "value2" }
            };

            // Act
            var map = ClientUtils.ParameterToMultiMap("multi", "ignored", dictionary);
            var grouped = map
                .GroupBy(kvp => kvp.Key)
                .ToDictionary(g => g.Key, g => g.SelectMany(kvp => kvp.Value).ToList());

            // Assert
            Assert.True(grouped.ContainsKey("key1"), "Should contain key1");
            Assert.True(grouped.ContainsKey("key2"), "Should contain key2");
            Assert.Contains("value1", grouped["key1"]);
            Assert.Contains("value2", grouped["key2"]);
        }

        [Fact]
        public void Dictionary_UsesDeepObjectKeys_WhenCollectionFormatIsDeepObject()
        {
            // Arrange
            var dictionary = new Dictionary<string, string>
            {
                { "key1", "value1" },
                { "key2", "value2" }
            };

            // Act
            var map = ClientUtils.ParameterToMultiMap("deepObject", "filter", dictionary);
            var grouped = map
                .GroupBy(kvp => kvp.Key)
                .ToDictionary(g => g.Key, g => g.SelectMany(kvp => kvp.Value).ToList());

            // Assert
            Assert.True(grouped.ContainsKey("filter[key1]"), "Should contain filter[key1]");
            Assert.True(grouped.ContainsKey("filter[key2]"), "Should contain filter[key2]");
            Assert.Contains("value1", grouped["filter[key1]"]);
            Assert.Contains("value2", grouped["filter[key2]"]);
        }

        [Fact]
        public void Dictionary_HandlesEmptyDictionary_Correctly()
        {
            // Arrange
            var dictionary = new Dictionary<string, string>();

            // Act
            var map = ClientUtils.ParameterToMultiMap("multi", "test", dictionary);

            // Assert
            Assert.Empty(map);
        }

        [Fact]
        public void Dictionary_HandlesNullDictionary_Correctly()
        {
            // Arrange
            Dictionary<string, string> dictionary = null;

            // Act
            var map = ClientUtils.ParameterToMultiMap("multi", "test", dictionary);

            // Assert
            Assert.Empty(map);
        }

        [Fact]
        public void Dictionary_HandlesSingleKeyValuePair_Correctly()
        {
            // Arrange
            var dictionary = new Dictionary<string, string>
            {
                { "singleKey", "singleValue" }
            };

            // Act
            var map = ClientUtils.ParameterToMultiMap("multi", "test", dictionary);
            var grouped = map
                .GroupBy(kvp => kvp.Key)
                .ToDictionary(g => g.Key, g => g.SelectMany(kvp => kvp.Value).ToList());

            // Assert
            Assert.Single(grouped);
            Assert.True(grouped.ContainsKey("singleKey"));
            Assert.Contains("singleValue", grouped["singleKey"]);
        }

        [Fact]
        public void Dictionary_HandlesSpecialCharactersInKeys_Correctly()
        {
            // Arrange
            var dictionary = new Dictionary<string, string>
            {
                { "key-with-dash", "value1" },
                { "key_with_underscore", "value2" },
                { "key with space", "value3" }
            };

            // Act
            var map = ClientUtils.ParameterToMultiMap("multi", "test", dictionary);
            var grouped = map
                .GroupBy(kvp => kvp.Key)
                .ToDictionary(g => g.Key, g => g.SelectMany(kvp => kvp.Value).ToList());

            // Assert
            Assert.True(grouped.ContainsKey("key-with-dash"));
            Assert.True(grouped.ContainsKey("key_with_underscore"));
            Assert.True(grouped.ContainsKey("key with space"));
            Assert.Contains("value1", grouped["key-with-dash"]);
            Assert.Contains("value2", grouped["key_with_underscore"]);
            Assert.Contains("value3", grouped["key with space"]);
        }
    }
}
