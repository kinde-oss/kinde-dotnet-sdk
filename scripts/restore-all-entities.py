#!/usr/bin/env python3
"""
Restore all corrupted entity files from converter information.
"""

import sys
from pathlib import Path

# Entity file templates based on converter information
ENTITIES = {
    "FeatureFlagValue.cs": """#nullable enable

using System;
using System.Text;
using System.Text.Json.Serialization;
using OpenAPIClientUtils = Kinde.Accounts.Client.ClientUtils;

namespace Kinde.Accounts.Model.Entities
{
    /// <summary>
    /// Represents a feature flag value (can be string, bool, int, or object)
    /// </summary>
    public partial class FeatureFlagValue
    {
        internal FeatureFlagValue(string varString)
        {
            VarString = varString;
        }

        internal FeatureFlagValue(bool varBool)
        {
            VarBool = varBool;
        }

        internal FeatureFlagValue(int varInt)
        {
            VarInt = varInt;
        }

        internal FeatureFlagValue(Object varObject)
        {
            VarObject = varObject;
        }

        public string? VarString { get; }
        public bool? VarBool { get; }
        public int? VarInt { get; }
        public Object? VarObject { get; }

        public override string ToString()
        {
            if (VarString != null) return VarString;
            if (VarBool.HasValue) return VarBool.Value.ToString();
            if (VarInt.HasValue) return VarInt.Value.ToString();
            if (VarObject != null) return VarObject.ToString();
            return "";
        }
    }
}
""",
    "UserPropertyValue.cs": """#nullable enable

using System;
using System.Text;
using System.Text.Json.Serialization;
using OpenAPIClientUtils = Kinde.Accounts.Client.ClientUtils;

namespace Kinde.Accounts.Model.Entities
{
    /// <summary>
    /// Represents a user property value (can be string, bool, or int)
    /// </summary>
    public partial class UserPropertyValue
    {
        internal UserPropertyValue(string varString)
        {
            VarString = varString;
        }

        internal UserPropertyValue(bool varBool)
        {
            VarBool = varBool;
        }

        internal UserPropertyValue(int varInt)
        {
            VarInt = varInt;
        }

        public string? VarString { get; }
        public bool? VarBool { get; }
        public int? VarInt { get; }

        public override string ToString()
        {
            if (VarString != null) return VarString;
            if (VarBool.HasValue) return VarBool.Value.ToString();
            if (VarInt.HasValue) return VarInt.Value.ToString();
            return "";
        }
    }
}
""",
    "EntitlementDetail.cs": """#nullable enable

using System;
using System.Text;
using System.Text.Json.Serialization;

namespace Kinde.Accounts.Model.Entities
{
    /// <summary>
    /// Represents an entitlement detail
    /// </summary>
    public partial class EntitlementDetail
    {
        [JsonConstructor]
        public EntitlementDetail(string featureKey, string featureName, string id, string priceName, int? entitlementLimitMax = default, int? entitlementLimitMin = default, int? fixedCharge = default, int? unitAmount = default)
        {
            FeatureKey = featureKey;
            FeatureName = featureName;
            Id = id;
            PriceName = priceName;
            EntitlementLimitMax = entitlementLimitMax;
            EntitlementLimitMin = entitlementLimitMin;
            FixedCharge = fixedCharge;
            UnitAmount = unitAmount;
            OnCreated();
        }

        partial void OnCreated();

        [JsonPropertyName("feature_key")]
        public string FeatureKey { get; set; }

        [JsonPropertyName("feature_name")]
        public string FeatureName { get; set; }

        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("price_name")]
        public string PriceName { get; set; }

        [JsonPropertyName("entitlement_limit_max")]
        public int? EntitlementLimitMax { get; set; }

        [JsonPropertyName("entitlement_limit_min")]
        public int? EntitlementLimitMin { get; set; }

        [JsonPropertyName("fixed_charge")]
        public int? FixedCharge { get; set; }

        [JsonPropertyName("unit_amount")]
        public int? UnitAmount { get; set; }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("class EntitlementDetail {\\n");
            sb.Append("  FeatureKey: ").Append(FeatureKey).Append("\\n");
            sb.Append("  FeatureName: ").Append(FeatureName).Append("\\n");
            sb.Append("  Id: ").Append(Id).Append("\\n");
            sb.Append("}\\n");
            return sb.ToString();
        }
    }
}
""",
    "Error.cs": """#nullable enable

using System;
using System.Text;
using System.Text.Json.Serialization;

namespace Kinde.Accounts.Model.Entities
{
    /// <summary>
    /// Represents an error
    /// </summary>
    public partial class Error
    {
        [JsonConstructor]
        public Error(string code, string message)
        {
            Code = code;
            Message = message;
            OnCreated();
        }

        partial void OnCreated();

        [JsonPropertyName("code")]
        public string Code { get; set; }

        [JsonPropertyName("message")]
        public string Message { get; set; }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("class Error {\\n");
            sb.Append("  Code: ").Append(Code).Append("\\n");
            sb.Append("  Message: ").Append(Message).Append("\\n");
            sb.Append("}\\n");
            return sb.ToString();
        }
    }
}
""",
    "PortalLink.cs": """#nullable enable

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
            sb.Append("class PortalLink {\\n");
            sb.Append("  Url: ").Append(Url).Append("\\n");
            sb.Append("}\\n");
            return sb.ToString();
        }
    }
}
""",
    "TokenIntrospect.cs": """#nullable enable

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
            sb.Append("class TokenIntrospect {\\n");
            sb.Append("  Active: ").Append(Active).Append("\\n");
            sb.Append("}\\n");
            return sb.ToString();
        }
    }
}
""",
    "UserProfileV2.cs": """#nullable enable

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
            sb.Append("class UserProfileV2 {\\n");
            sb.Append("  Email: ").Append(Email).Append("\\n");
            sb.Append("}\\n");
            return sb.ToString();
        }
    }
}
""",
}

def main():
    if len(sys.argv) < 2:
        print("Usage: restore-all-entities.py <accounts-api-dir>")
        sys.exit(1)
    
    accounts_dir = Path(sys.argv[1])
    entities_dir = accounts_dir / "Model" / "Entities"
    
    if not entities_dir.exists():
        print(f"Error: Entities directory not found: {entities_dir}")
        sys.exit(1)
    
    for filename, content in ENTITIES.items():
        filepath = entities_dir / filename
        with open(filepath, 'w', encoding='utf-8') as f:
            f.write(content)
        print(f"Restored: {filename}")

if __name__ == '__main__':
    main()

