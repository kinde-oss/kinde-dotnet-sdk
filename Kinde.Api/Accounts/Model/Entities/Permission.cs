#nullable enable

using System;
using System.Text;
using System.Text.Json.Serialization;

namespace Kinde.Accounts.Model.Entities
{
    /// <summary>
    /// Represents a permission
    /// </summary>
    public partial class Permission
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Permission" /> class.
        /// </summary>
        /// <param name="id">The friendly ID of a permission</param>
        /// <param name="key">The key of the permission</param>
        /// <param name="name">The name of the permission</param>
        [JsonConstructor]
        public Permission(string id, string key, string name)
        {
            Id = id;
            Key = key;
            Name = name;
            OnCreated();
        }

        partial void OnCreated();

        /// <summary>
        /// The friendly ID of a permission
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; set; }

        /// <summary>
        /// The key of the permission
        /// </summary>
        [JsonPropertyName("key")]
        public string Key { get; set; }

        /// <summary>
        /// The name of the permission
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("class Permission {\n");
            sb.Append("  Id: ").Append(Id).Append("\n");
            sb.Append("  Key: ").Append(Key).Append("\n");
            sb.Append("  Name: ").Append(Name).Append("\n");
            sb.Append("}\n");
            return sb.ToString();
        }
    }
}
