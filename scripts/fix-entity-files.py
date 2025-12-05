#!/usr/bin/env python3
"""
Fix corrupted entity files by reconstructing them from backup or converter information.
"""

import os
import re
import sys
from pathlib import Path

def reconstruct_feature_flag_entity():
    """Reconstruct FeatureFlag entity file"""
    content = """#nullable enable

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
            sb.Append("class FeatureFlag {\\n");
            sb.Append("  Id: ").Append(Id).Append("\\n");
            sb.Append("  Key: ").Append(Key).Append("\\n");
            sb.Append("  Name: ").Append(Name).Append("\\n");
            sb.Append("  Type: ").Append(Type).Append("\\n");
            sb.Append("  Value: ").Append(Value).Append("\\n");
            sb.Append("}\\n");
            return sb.ToString();
        }
    }
}
"""
    return content

def reconstruct_permission_entity():
    """Reconstruct Permission entity file"""
    content = """#nullable enable

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
            sb.Append("class Permission {\\n");
            sb.Append("  Id: ").Append(Id).Append("\\n");
            sb.Append("  Key: ").Append(Key).Append("\\n");
            sb.Append("  Name: ").Append(Name).Append("\\n");
            sb.Append("}\\n");
            return sb.ToString();
        }
    }
}
"""
    return content

def main():
    """Main entry point"""
    if len(sys.argv) < 2:
        print("Usage: fix-entity-files.py <accounts-api-dir>")
        sys.exit(1)
    
    accounts_dir = Path(sys.argv[1])
    entities_dir = accounts_dir / "Model" / "Entities"
    
    if not entities_dir.exists():
        print(f"Error: Entities directory not found: {entities_dir}")
        sys.exit(1)
    
    # Fix FeatureFlag
    feature_flag_file = entities_dir / "FeatureFlag.cs"
    if feature_flag_file.exists():
        with open(feature_flag_file, 'w', encoding='utf-8') as f:
            f.write(reconstruct_feature_flag_entity())
        print(f"Fixed: {feature_flag_file.name}")
    
    # Fix Permission
    permission_file = entities_dir / "Permission.cs"
    if permission_file.exists():
        with open(permission_file, 'w', encoding='utf-8') as f:
            f.write(reconstruct_permission_entity())
        print(f"Fixed: {permission_file.name}")
    
    print("Entity files fixed!")

if __name__ == '__main__':
    main()

