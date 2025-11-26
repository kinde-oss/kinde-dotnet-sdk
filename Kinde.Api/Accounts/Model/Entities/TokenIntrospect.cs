#nullable enable

using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Kinde.Accounts.Model.Entities
{
    /// <summary>
    /// Represents token introspection data
    /// </summary>
    public partial class TokenIntrospect
    {
        [JsonConstructor]
        public TokenIntrospect(bool? active = default, List<string>? aud = default, string? clientId = default, int? exp = default, int? iat = default)
        {
            Active = active;
            Aud = aud;
            ClientId = clientId;
            Exp = exp;
            Iat = iat;
            OnCreated();
        }

        partial void OnCreated();

        [JsonPropertyName("active")]
        public bool? Active { get; set; }

        [JsonPropertyName("aud")]
        public List<string>? Aud { get; set; }

        [JsonPropertyName("client_id")]
        public string? ClientId { get; set; }

        [JsonPropertyName("exp")]
        public int? Exp { get; set; }

        [JsonPropertyName("iat")]
        public int? Iat { get; set; }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("class TokenIntrospect {\n");
            sb.Append("  Active: ").Append(Active).Append("\n");
            sb.Append("}\n");
            return sb.ToString();
        }
    }
}
