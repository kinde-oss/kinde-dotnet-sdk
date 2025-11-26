#nullable enable

using System;
using System.Text;
using System.Text.Json.Serialization;

namespace Kinde.Accounts.Model.Entities
{
    /// <summary>
    /// Represents a user profile (v2)
    /// </summary>
    public partial class UserProfileV2
    {
        [JsonConstructor]
        public UserProfileV2(string? email = default, bool? emailVerified = default, string? familyName = default, string? givenName = default, string? id = default, string? name = default, string? sub = default, int? updatedAt = default, string? picture = default, string? preferredUsername = default, string? providedId = default)
        {
            Email = email;
            EmailVerified = emailVerified;
            FamilyName = familyName;
            GivenName = givenName;
            Id = id;
            Name = name;
            Sub = sub;
            UpdatedAt = updatedAt;
            Picture = picture;
            PreferredUsername = preferredUsername;
            ProvidedId = providedId;
            OnCreated();
        }

        partial void OnCreated();

        [JsonPropertyName("email")]
        public string? Email { get; set; }

        [JsonPropertyName("email_verified")]
        public bool? EmailVerified { get; set; }

        [JsonPropertyName("family_name")]
        public string? FamilyName { get; set; }

        [JsonPropertyName("given_name")]
        public string? GivenName { get; set; }

        [JsonPropertyName("id")]
        public string? Id { get; set; }

        [JsonPropertyName("name")]
        public string? Name { get; set; }

        [JsonPropertyName("sub")]
        public string? Sub { get; set; }

        [JsonPropertyName("updated_at")]
        public int? UpdatedAt { get; set; }

        [JsonPropertyName("picture")]
        public string? Picture { get; set; }

        [JsonPropertyName("preferred_username")]
        public string? PreferredUsername { get; set; }

        [JsonPropertyName("provided_id")]
        public string? ProvidedId { get; set; }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("class UserProfileV2 {\n");
            sb.Append("  Email: ").Append(Email).Append("\n");
            sb.Append("}\n");
            return sb.ToString();
        }
    }
}
