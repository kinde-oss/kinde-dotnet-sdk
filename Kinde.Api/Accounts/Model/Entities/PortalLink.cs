#nullable enable

using System;
using System.Text;
using System.Text.Json.Serialization;

namespace Kinde.Accounts.Model.Entities
{
    /// <summary>
    /// Represents a portal link
    /// </summary>
    public partial class PortalLink
    {
        [JsonConstructor]
        public PortalLink(string url)
        {
            Url = url;
            OnCreated();
        }

        partial void OnCreated();

        [JsonPropertyName("url")]
        public string Url { get; set; }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("class PortalLink {\n");
            sb.Append("  Url: ").Append(Url).Append("\n");
            sb.Append("}\n");
            return sb.ToString();
        }
    }
}
