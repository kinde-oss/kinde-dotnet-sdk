#nullable enable

using System;
using System.Text;
using System.Text.Json.Serialization;

namespace Kinde.Accounts.Model.Entities
{
    /// <summary>
    /// Represents a user property
    /// </summary>
    public partial class UserProperty
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserProperty" /> class.
        /// </summary>
        /// <param name="id">The friendly ID of a property</param>
        /// <param name="key">The key of the property</param>
        /// <param name="name">The name of the property</param>
        /// <param name="value">The value of the property</param>
        [JsonConstructor]
        public UserProperty(string id, string key, string name, UserPropertyValue value)
        {
            Id = id;
            Key = key;
            Name = name;
            Value = value;
            OnCreated();
        }

        partial void OnCreated();

        /// <summary>
        /// The friendly ID of a property
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; set; }

        /// <summary>
        /// The key of the property
        /// </summary>
        [JsonPropertyName("key")]
        public string Key { get; set; }

        /// <summary>
        /// The name of the property
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or Sets Value
        /// </summary>
        [JsonPropertyName("value")]
        public UserPropertyValue Value { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("class UserProperty {\n");
            sb.Append("  Id: ").Append(Id).Append("\n");
            sb.Append("  Key: ").Append(Key).Append("\n");
            sb.Append("  Name: ").Append(Name).Append("\n");
            sb.Append("  Value: ").Append(Value).Append("\n");
            sb.Append("}\n");
            return sb.ToString();
        }
    }
}
