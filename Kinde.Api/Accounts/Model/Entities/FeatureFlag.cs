#nullable enable

using System;
using System.Text;
using System.Text.Json.Serialization;

namespace Kinde.Accounts.Model.Entities
{
    /// <summary>
    /// Represents a feature flag
    /// </summary>
    public partial class FeatureFlag
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FeatureFlag" /> class.
        /// </summary>
        /// <param name="id">The friendly ID of a flag</param>
        /// <param name="key">The key of the flag</param>
        /// <param name="name">The name of the flag</param>
        /// <param name="type">The type of the flag</param>
        /// <param name="value">The value of the flag</param>
        [JsonConstructor]
        public FeatureFlag(string id, string key, string name, string type, FeatureFlagValue value)
        {
            Id = id;
            Key = key;
            Name = name;
            Type = type;
            Value = value;
            OnCreated();
        }

        partial void OnCreated();

        /// <summary>
        /// The friendly ID of a flag
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; set; }

        /// <summary>
        /// The key of the flag
        /// </summary>
        [JsonPropertyName("key")]
        public string Key { get; set; }

        /// <summary>
        /// The name of the flag
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; }

        /// <summary>
        /// The type of the flag
        /// </summary>
        [JsonPropertyName("type")]
        public string Type { get; set; }

        /// <summary>
        /// Gets or Sets Value
        /// </summary>
        [JsonPropertyName("value")]
        public FeatureFlagValue Value { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("class FeatureFlag {\n");
            sb.Append("  Id: ").Append(Id).Append("\n");
            sb.Append("  Key: ").Append(Key).Append("\n");
            sb.Append("  Name: ").Append(Name).Append("\n");
            sb.Append("  Type: ").Append(Type).Append("\n");
            sb.Append("  Value: ").Append(Value).Append("\n");
            sb.Append("}\n");
            return sb.ToString();
        }
    }
}
