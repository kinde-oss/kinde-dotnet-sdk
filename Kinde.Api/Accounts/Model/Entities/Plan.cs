#nullable enable

using System;
using System.Text;
using System.Text.Json.Serialization;

namespace Kinde.Accounts.Model.Entities
{
    /// <summary>
    /// Represents a plan
    /// </summary>
    public partial class Plan
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Plan" /> class.
        /// </summary>
        /// <param name="key">The key of the plan</param>
        /// <param name="name">The name of the plan</param>
        /// <param name="subscribedOn">The date the plan was subscribed to</param>
        [JsonConstructor]
        public Plan(string key, string name, DateTime subscribedOn)
        {
            Key = key;
            Name = name;
            SubscribedOn = subscribedOn;
            OnCreated();
        }

        partial void OnCreated();

        /// <summary>
        /// The key of the plan
        /// </summary>
        [JsonPropertyName("key")]
        public string Key { get; set; }

        /// <summary>
        /// The name of the plan
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; }

        /// <summary>
        /// The date the plan was subscribed to
        /// </summary>
        [JsonPropertyName("subscribed_on")]
        public DateTime SubscribedOn { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("class Plan {\n");
            sb.Append("  Key: ").Append(Key).Append("\n");
            sb.Append("  Name: ").Append(Name).Append("\n");
            sb.Append("  SubscribedOn: ").Append(SubscribedOn).Append("\n");
            sb.Append("}\n");
            return sb.ToString();
        }
    }
}
