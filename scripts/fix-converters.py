#!/usr/bin/env python3
"""
Fix incomplete converter files by completing the Read and Write methods.
"""

import re
import sys
from pathlib import Path

def fix_converter_file(filepath: Path):
    """Fix a single converter file"""
    with open(filepath, 'r', encoding='utf-8') as f:
        content = f.read()
    
    # Check if file is already complete
    if 'public override void Write' in content and content.count('}') >= 4:
        return False
    
    # Extract class name and entity type from the file
    class_match = re.search(r'public class (\w+JsonConverter) : JsonConverter<(\w+)>', content)
    if not class_match:
        return False
    
    converter_class = class_match.group(1)
    entity_type = class_match.group(2)
    
    # Find where the Read method ends (before the missing closing braces)
    read_end_match = re.search(r'(}\s*)$', content, re.MULTILINE)
    if not read_end_match:
        # Try to find the last closing brace before the end
        read_end_match = re.search(r'(\s*default:\s*break;\s*}\s*)$', content, re.MULTILINE)
    
    # Determine entity properties from the Read method
    properties = []
    if 'id = utf8JsonReader.GetString()' in content:
        properties.append(('id', 'string', 'GetString'))
    if 'key = utf8JsonReader.GetString()' in content:
        properties.append(('key', 'string', 'GetString'))
    if 'name = utf8JsonReader.GetString()' in content:
        properties.append(('name', 'string', 'GetString'))
    if 'type = utf8JsonReader.GetString()' in content:
        properties.append(('type', 'string', 'GetString'))
    if 'url = utf8JsonReader.GetString()' in content:
        properties.append(('url', 'string', 'GetString'))
    if 'code = utf8JsonReader.GetString()' in content:
        properties.append(('code', 'string', 'GetString'))
    if 'message = utf8JsonReader.GetString()' in content:
        properties.append(('message', 'string', 'GetString'))
    if 'email = utf8JsonReader.GetString()' in content:
        properties.append(('email', 'string', 'GetString'))
    if 'familyName = utf8JsonReader.GetString()' in content:
        properties.append(('family_name', 'string', 'GetString'))
    if 'givenName = utf8JsonReader.GetString()' in content:
        properties.append(('given_name', 'string', 'GetString'))
    if 'sub = utf8JsonReader.GetString()' in content:
        properties.append(('sub', 'string', 'GetString'))
    if 'picture = utf8JsonReader.GetString()' in content:
        properties.append(('picture', 'string', 'GetString'))
    if 'preferredUsername = utf8JsonReader.GetString()' in content:
        properties.append(('preferred_username', 'string', 'GetString'))
    if 'providedId = utf8JsonReader.GetString()' in content:
        properties.append(('provided_id', 'string', 'GetString'))
    if 'clientId = utf8JsonReader.GetString()' in content:
        properties.append(('client_id', 'string', 'GetString'))
    if 'featureKey = utf8JsonReader.GetString()' in content:
        properties.append(('feature_key', 'string', 'GetString'))
    if 'featureName = utf8JsonReader.GetString()' in content:
        properties.append(('feature_name', 'string', 'GetString'))
    if 'priceName = utf8JsonReader.GetString()' in content:
        properties.append(('price_name', 'string', 'GetString'))
    if 'active = utf8JsonReader.GetBoolean()' in content:
        properties.append(('active', 'bool?', 'GetBoolean'))
    if 'emailVerified = utf8JsonReader.GetBoolean()' in content:
        properties.append(('email_verified', 'bool?', 'GetBoolean'))
    if 'entitlementLimitMax = utf8JsonReader.GetInt32()' in content:
        properties.append(('entitlement_limit_max', 'int?', 'GetInt32'))
    if 'entitlementLimitMin = utf8JsonReader.GetInt32()' in content:
        properties.append(('entitlement_limit_min', 'int?', 'GetInt32'))
    if 'fixedCharge = utf8JsonReader.GetInt32()' in content:
        properties.append(('fixed_charge', 'int?', 'GetInt32'))
    if 'unitAmount = utf8JsonReader.GetInt32()' in content:
        properties.append(('unit_amount', 'int?', 'GetInt32'))
    if 'exp = utf8JsonReader.GetInt32()' in content:
        properties.append(('exp', 'int?', 'GetInt32'))
    if 'iat = utf8JsonReader.GetInt32()' in content:
        properties.append(('iat', 'int?', 'GetInt32'))
    if 'updatedAt = utf8JsonReader.GetInt32()' in content:
        properties.append(('updated_at', 'int?', 'GetInt32'))
    if 'subscribedOn =' in content or 'SubscribedOn' in content:
        properties.append(('subscribed_on', 'DateTime', 'DateTime'))
    if 'value = JsonSerializer.Deserialize<FeatureFlagValue>' in content:
        properties.append(('value', 'FeatureFlagValue', 'Deserialize'))
    if 'value = JsonSerializer.Deserialize<UserPropertyValue>' in content:
        properties.append(('value', 'UserPropertyValue', 'Deserialize'))
    if 'aud = JsonSerializer.Deserialize<List<string>>' in content:
        properties.append(('aud', 'List<string>', 'Deserialize'))
    
    # Build the completion code
    completion = "\n"
    
    # Complete Read method if needed
    if 'return new' not in content and 'throw new ArgumentNullException' not in content:
        # Add validation and return statement
        completion += "            if (id == null)\n"
        completion += f"                throw new ArgumentNullException(nameof(id), \"Property is required for class {entity_type}.\");\n\n"
        completion += f"            return new {entity_type}(id"
        if len(properties) > 1:
            for prop_name, prop_type, _ in properties[1:]:
                var_name = prop_name.replace('_', '')
                if prop_name == 'key':
                    var_name = 'key'
                elif prop_name == 'name':
                    var_name = 'name'
                elif prop_name == 'type':
                    var_name = 'type'
                elif prop_name == 'url':
                    var_name = 'url'
                elif prop_name == 'code':
                    var_name = 'code'
                elif prop_name == 'message':
                    var_name = 'message'
                elif prop_name == 'email':
                    var_name = 'email'
                elif prop_name == 'family_name':
                    var_name = 'familyName'
                elif prop_name == 'given_name':
                    var_name = 'givenName'
                elif prop_name == 'sub':
                    var_name = 'sub'
                elif prop_name == 'picture':
                    var_name = 'picture'
                elif prop_name == 'preferred_username':
                    var_name = 'preferredUsername'
                elif prop_name == 'provided_id':
                    var_name = 'providedId'
                elif prop_name == 'client_id':
                    var_name = 'clientId'
                elif prop_name == 'feature_key':
                    var_name = 'featureKey'
                elif prop_name == 'feature_name':
                    var_name = 'featureName'
                elif prop_name == 'price_name':
                    var_name = 'priceName'
                elif prop_name == 'active':
                    var_name = 'active'
                elif prop_name == 'email_verified':
                    var_name = 'emailVerified'
                elif prop_name == 'entitlement_limit_max':
                    var_name = 'entitlementLimitMax'
                elif prop_name == 'entitlement_limit_min':
                    var_name = 'entitlementLimitMin'
                elif prop_name == 'fixed_charge':
                    var_name = 'fixedCharge'
                elif prop_name == 'unit_amount':
                    var_name = 'unitAmount'
                elif prop_name == 'exp':
                    var_name = 'exp'
                elif prop_name == 'iat':
                    var_name = 'iat'
                elif prop_name == 'updated_at':
                    var_name = 'updatedAt'
                elif prop_name == 'subscribed_on':
                    var_name = 'subscribedOn'
                elif prop_name == 'value':
                    var_name = 'value'
                elif prop_name == 'aud':
                    var_name = 'aud'
                completion += f", {var_name}"
        completion += ");\n        }\n\n"
    
    # Add Write method
    completion += """        /// <summary>
        /// Serializes a <see cref="ENTITY_TYPE" />
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="entity"></param>
        /// <param name="jsonSerializerOptions"></param>
        /// <exception cref="NotImplementedException"></exception>
        public override void Write(Utf8JsonWriter writer, ENTITY_TYPE entity, JsonSerializerOptions jsonSerializerOptions)
        {
            writer.WriteStartObject();
            WriteProperties(ref writer, entity, jsonSerializerOptions);
            writer.WriteEndObject();
        }

        /// <summary>
        /// Serializes the properties of <see cref="ENTITY_TYPE" />
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="entity"></param>
        /// <param name="jsonSerializerOptions"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void WriteProperties(ref Utf8JsonWriter writer, ENTITY_TYPE entity, JsonSerializerOptions jsonSerializerOptions)
        {
""".replace('ENTITY_TYPE', entity_type)
    
    # Add property writes
    for prop_name, prop_type, prop_method in properties:
        var_name = prop_name.replace('_', '')
        if prop_name == 'key':
            var_name = 'Key'
        elif prop_name == 'name':
            var_name = 'Name'
        elif prop_name == 'type':
            var_name = 'Type'
        elif prop_name == 'url':
            var_name = 'Url'
        elif prop_name == 'code':
            var_name = 'Code'
        elif prop_name == 'message':
            var_name = 'Message'
        elif prop_name == 'email':
            var_name = 'Email'
        elif prop_name == 'family_name':
            var_name = 'FamilyName'
        elif prop_name == 'given_name':
            var_name = 'GivenName'
        elif prop_name == 'sub':
            var_name = 'Sub'
        elif prop_name == 'picture':
            var_name = 'Picture'
        elif prop_name == 'preferred_username':
            var_name = 'PreferredUsername'
        elif prop_name == 'provided_id':
            var_name = 'ProvidedId'
        elif prop_name == 'client_id':
            var_name = 'ClientId'
        elif prop_name == 'feature_key':
            var_name = 'FeatureKey'
        elif prop_name == 'feature_name':
            var_name = 'FeatureName'
        elif prop_name == 'price_name':
            var_name = 'PriceName'
        elif prop_name == 'active':
            var_name = 'Active'
        elif prop_name == 'email_verified':
            var_name = 'EmailVerified'
        elif prop_name == 'entitlement_limit_max':
            var_name = 'EntitlementLimitMax'
        elif prop_name == 'entitlement_limit_min':
            var_name = 'EntitlementLimitMin'
        elif prop_name == 'fixed_charge':
            var_name = 'FixedCharge'
        elif prop_name == 'unit_amount':
            var_name = 'UnitAmount'
        elif prop_name == 'exp':
            var_name = 'Exp'
        elif prop_name == 'iat':
            var_name = 'Iat'
        elif prop_name == 'updated_at':
            var_name = 'UpdatedAt'
        elif prop_name == 'subscribed_on':
            var_name = 'SubscribedOn'
        elif prop_name == 'value':
            var_name = 'Value'
        elif prop_name == 'aud':
            var_name = 'Aud'
        
        if prop_method == 'GetString':
            completion += f'            writer.WriteString("{prop_name}", entity.{var_name});\n'
        elif prop_method == 'GetBoolean':
            completion += f'            if (entity.{var_name}.HasValue)\n'
            completion += f'                writer.WriteBoolean("{prop_name}", entity.{var_name}.Value);\n'
        elif prop_method == 'GetInt32':
            completion += f'            if (entity.{var_name}.HasValue)\n'
            completion += f'                writer.WriteNumber("{prop_name}", entity.{var_name}.Value);\n'
        elif prop_method == 'DateTime':
            completion += f'            writer.WriteString("{prop_name}", entity.{var_name}.ToString(SubscribedOnFormat));\n'
        elif prop_method == 'Deserialize':
            completion += f'            writer.WritePropertyName("{prop_name}");\n'
            completion += f'            JsonSerializer.Serialize(writer, entity.{var_name}, jsonSerializerOptions);\n'
    
    completion += "        }\n    }\n}\n"
    
    # Replace the incomplete ending
    content = re.sub(r'}\s*$', completion, content, flags=re.MULTILINE)
    
    with open(filepath, 'w', encoding='utf-8') as f:
        f.write(content)
    
    return True

def main():
    if len(sys.argv) < 2:
        print("Usage: fix-converters.py <accounts-api-dir>")
        sys.exit(1)
    
    accounts_dir = Path(sys.argv[1])
    converters_dir = accounts_dir / "Model" / "Converters"
    
    if not converters_dir.exists():
        print(f"Error: Converters directory not found: {converters_dir}")
        sys.exit(1)
    
    fixed_count = 0
    for converter_file in converters_dir.glob("*.cs"):
        if fix_converter_file(converter_file):
            fixed_count += 1
            print(f"Fixed: {converter_file.name}")
    
    print(f"\nFixed {fixed_count} converter files")

if __name__ == '__main__':
    main()

