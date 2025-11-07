#!/usr/bin/env python3
"""
Generate Newtonsoft.Json converters for Response and Request models with Option<> properties
Properly extracts property names, types, and handles C# reserved keywords and enum types
"""
import re
import os

MODEL_DIR = "Kinde.Api/Model"
CONVERTER_DIR = "Kinde.Api/Converters"

# Models that should be excluded from converter generation
# (Empty by default - all converters will be regenerated to ensure consistency)
EXISTING = set()

def extract_properties_from_model(model_file_path):
    """Extract Option<> properties from a model file by parsing the actual property definitions"""
    try:
        with open(model_file_path, 'r', encoding='utf-8') as f:
            content = f.read()
    except Exception as e:
        print(f"Error reading {model_file_path}: {e}")
        return None
    
    # Find the class name
    class_match = re.search(r'public\s+partial\s+class\s+(\w+)', content)
    if not class_match:
        return None
    
    model_name = class_match.group(1)
    
    # Extract constructor parameters first to get the actual parameter names
    constructor_match = re.search(r'public\s+' + model_name + r'\(([^)]+)\)', content)
    if not constructor_match:
        return None
    
    constructor_params_str = constructor_match.group(1)
    constructor_params = {}  # Option<> parameters
    required_params = {}  # Required (non-Option) parameters
    
    # Parse all constructor parameters (both required and Option<>)
    # Split by comma, but be careful with nested generics
    params = []
    current_param = ""
    bracket_depth = 0
    for char in constructor_params_str:
        if char == '<':
            bracket_depth += 1
            current_param += char
        elif char == '>':
            bracket_depth -= 1
            current_param += char
        elif char == ',' and bracket_depth == 0:
            params.append(current_param.strip())
            current_param = ""
        else:
            current_param += char
    if current_param.strip():
        params.append(current_param.strip())
    
    # Parse each parameter
    for param_str in params:
        param_str = param_str.strip()
        # Check if it's an Option<> parameter
        if 'Option<' in param_str:
            # Extract Option<Type?> paramName = default
            option_match = re.search(r'Option<([^>]+)>\s+(\w+)\s*=', param_str)
            if option_match:
                param_type = option_match.group(1).strip()
                param_name = option_match.group(2)
                constructor_params[param_name] = param_type
        else:
            # Required parameter (no Option<>)
            # Format: Type paramName or Type paramName = value
            required_match = re.search(r'(\w+(?:<[^>]+>)?)\s+(\w+)(?:\s*=|$)', param_str)
            if required_match:
                param_type = required_match.group(1).strip()
                param_name = required_match.group(2)
                # Find JSON property name for this required parameter
                # Look for: [JsonPropertyName("json_name")] public Type PropertyName { get; set; }
                # Where PropertyName matches param_name (case-insensitive)
                prop_pattern = rf'\[JsonPropertyName\("([^"]+)"\)\]\s+public\s+[^\s]+\s+{param_name}\s+\{{\s+get'
                prop_match = re.search(prop_pattern, content, re.IGNORECASE)
                if prop_match:
                    json_name = prop_match.group(1)
                    required_params[param_name] = {'type': param_type, 'json_name': json_name}
                else:
                    # Fallback to snake_case conversion
                    json_name = ''.join(['_' + c.lower() if c.isupper() else c for c in param_name]).lstrip('_')
                    required_params[param_name] = {'type': param_type, 'json_name': json_name}
    
    # Extract properties with Option<> types
    # Pattern: [JsonPropertyName("json_name")] public Type? PropertyName { get { return this.PropertyNameOption; } set { this.PropertyNameOption = new Option<Type?>(value); } }
    properties = []
    
    # Find all property definitions with JsonPropertyName
    prop_pattern = r'\[JsonPropertyName\("([^"]+)"\)\]\s+public\s+([^\?]+)\?\s+(\w+)\s+\{\s+get\s+\{\s+return\s+this\.(\w+)Option;\s+\}\s+set'
    
    for match in re.finditer(prop_pattern, content):
        json_name = match.group(1)
        prop_type = match.group(2).strip()
        prop_name = match.group(3)
        option_prop_name = match.group(4)
        
        # Find the constructor parameter name (lowercase version of property name)
        # Try to match by finding the property name in constructor params
        constructor_param_name = None
        for param_name, param_type in constructor_params.items():
            # Match by property name (case-insensitive) or by option property name
            if param_name.lower() == prop_name.lower() or param_name.lower() == option_prop_name.lower():
                constructor_param_name = param_name
                break
        
        # If not found, try to infer from property name
        if not constructor_param_name:
            # Property names are PascalCase, constructor params are camelCase
            constructor_param_name = prop_name[0].lower() + prop_name[1:] if prop_name else None
        
        # Verify the Option property exists (handle nested generics)
        # Look for "public Option<...> {option_prop_name}Option" where ... can contain nested <>
        option_pattern = rf'public\s+Option<[^>]*(?:<[^>]*>)*[^>]*>\s+{option_prop_name}Option\s+\{{\s+get'
        # Also try a simpler pattern that just checks for the property name
        simple_pattern = rf'{option_prop_name}Option\s+\{{\s+get'
        if (re.search(option_pattern, content) or re.search(simple_pattern, content)) and constructor_param_name:
            properties.append({
                'json_name': json_name,
                'prop_type': prop_type,
                'prop_name': prop_name,
                'option_prop_name': option_prop_name,
                'constructor_param_name': constructor_param_name
            })
    
    return model_name, properties, required_params

# C# reserved keywords that need @ prefix
RESERVED_KEYWORDS = {'event', 'class', 'namespace', 'string', 'object', 'bool', 'int', 'long', 'float', 'double', 'decimal', 'char', 'byte', 'sbyte', 'short', 'ushort', 'uint', 'ulong', 'void', 'null', 'true', 'false', 'if', 'else', 'for', 'foreach', 'while', 'do', 'switch', 'case', 'default', 'break', 'continue', 'return', 'try', 'catch', 'finally', 'throw', 'new', 'this', 'base', 'public', 'private', 'protected', 'internal', 'static', 'readonly', 'const', 'virtual', 'override', 'abstract', 'sealed', 'partial', 'async', 'await', 'using', 'namespace', 'class', 'struct', 'interface', 'enum', 'delegate', 'var', 'dynamic', 'ref', 'out', 'in', 'params', 'where', 'select', 'from', 'group', 'orderby', 'join', 'let', 'into', 'on', 'equals', 'by', 'ascending', 'descending'}

def escape_csharp_keyword(name):
    """Escape C# reserved keywords with @ prefix"""
    if name.lower() in RESERVED_KEYWORDS:
        return '@' + name
    return name

def get_csharp_type_for_read(prop_type, model_name=None):
    """Get the C# type declaration for reading from JSON"""
    prop_type = prop_type.strip()
    
    if prop_type.startswith('List<'):
        return prop_type
    elif prop_type.startswith('Dictionary<'):
        return prop_type
    elif prop_type == 'bool':
        return 'bool?'
    elif prop_type == 'int':
        return 'int?'
    elif prop_type == 'DateTimeOffset':
        return 'DateTimeOffset?'
    elif 'Enum' in prop_type:
        # Enums are nested in the model class, need to fully qualify
        enum_type = prop_type.rstrip('?')
        if model_name:
            return f"{model_name}.{enum_type}?"
        return prop_type + '?'
    else:
        # Complex object or string
        if prop_type.endswith('?'):
            return prop_type
        else:
            return prop_type + '?'

def get_read_code(prop, var_name):
    """Generate code to read a property from JSON"""
    json_name = prop['json_name']
    prop_type = prop['prop_type'].strip()
    model_name = prop.get('model_name', '')
    csharp_type = get_csharp_type_for_read(prop_type, model_name)
    
    code = f"""            if (jsonObject["{json_name}"] != null)
            {{
"""
    
    if prop_type.startswith('List<'):
        code += f"""                {var_name} = jsonObject["{json_name}"].ToObject<{prop_type}>(serializer);
"""
    elif prop_type.startswith('Dictionary<'):
        code += f"""                {var_name} = jsonObject["{json_name}"].ToObject<{prop_type}>(serializer);
"""
    elif prop_type == 'bool' or prop_type == 'int' or prop_type == 'DateTimeOffset':
        code += f"""                {var_name} = jsonObject["{json_name}"].ToObject<{csharp_type}>();
"""
    elif 'Enum' in prop_type:
        # For enums, we need to handle them specially
        # Enums are nested in the model class, so we need to use ModelName.EnumName
        # Extract the enum name (remove the ? if present)
        enum_type = prop_type.rstrip('?')
        # Use the model name to fully qualify the enum
        model_name_for_enum = prop.get('model_name', '')
        if model_name_for_enum:
            full_enum_name = f"{model_name_for_enum}.{enum_type}"
        else:
            full_enum_name = enum_type
        code += f"""                var {var_name}Str = jsonObject["{json_name}"].ToObject<string>();
                if (!string.IsNullOrEmpty({var_name}Str))
                {{
                    {var_name} = {model_name_for_enum}.{enum_type}FromString({var_name}Str);
                }}
"""
    else:
        # Complex object or string
        if prop_type == 'string':
            # Simple string doesn't need serializer
            code += f"""                {var_name} = jsonObject["{json_name}"].ToObject<string>();
"""
        elif prop_type.endswith('?'):
            base_type = prop_type[:-1]
            if base_type == 'string':
                code += f"""                {var_name} = jsonObject["{json_name}"].ToObject<string>();
"""
            else:
                code += f"""                {var_name} = jsonObject["{json_name}"].ToObject<{base_type}>(serializer);
"""
        else:
            if prop_type == 'string':
                code += f"""                {var_name} = jsonObject["{json_name}"].ToObject<string>();
"""
            else:
                code += f"""                {var_name} = jsonObject["{json_name}"].ToObject<{prop_type}>(serializer);
"""
    
    code += """            }
"""
    return code

def get_write_code(prop):
    """Generate code to write a property to JSON"""
    json_name = prop['json_name']
    prop_name = prop['prop_name']
    option_prop_name = prop['option_prop_name']
    prop_type = prop['prop_type'].strip()
    
    code = f"""            if (value.{option_prop_name}Option.IsSet && value.{prop_name} != null)
            {{
                writer.WritePropertyName("{json_name}");
"""
    
    if prop_type.startswith('List<'):
        code += f"""                serializer.Serialize(writer, value.{prop_name});
"""
    elif prop_type.startswith('Dictionary<'):
        code += f"""                serializer.Serialize(writer, value.{prop_name});
"""
    elif prop_type == 'bool' or prop_type == 'int' or prop_type == 'DateTimeOffset':
        code += f"""                writer.WriteValue(value.{prop_name}.Value);
"""
    elif 'Enum' in prop_type:
        # Enums are nested in the model class
        enum_type = prop_type.rstrip('?')
        model_name_for_enum = prop.get('model_name', '')
        if model_name_for_enum:
            full_enum_name = f"{model_name_for_enum}.{enum_type}"
        else:
            full_enum_name = enum_type
        code += f"""                var {prop_name.lower()}Str = {model_name_for_enum}.{enum_type}ToJsonValue(value.{prop_name}.Value);
                writer.WriteValue({prop_name.lower()}Str);
"""
    else:
        # Complex object or string
        code += f"""                serializer.Serialize(writer, value.{prop_name});
"""
    
    code += """            }
"""
    return code

def generate_converter(model_name, properties, required_params=None):
    """Generate converter code for a model"""
    if required_params is None:
        required_params = {}
    
    # Variable declarations for reading
    var_declarations = []
    read_codes = []
    constructor_args = []
    
    # Handle required (non-Option) parameters first
    for param_name, param_info in required_params.items():
        if isinstance(param_info, dict):
            param_type = param_info['type']
            json_name = param_info['json_name']
        else:
            # Backward compatibility
            param_type = param_info
            json_name = ''.join(['_' + c.lower() if c.isupper() else c for c in param_name]).lstrip('_')
        
        var_name = param_name  # Keep original case
        var_name = escape_csharp_keyword(var_name)
        
        # Determine C# type for reading
        # Required parameters should match the constructor parameter type exactly (no nullable unless specified)
        if param_type.startswith('List<'):
            csharp_type = param_type
        elif param_type.startswith('Dictionary<'):
            # Dictionary types should use System.Collections.Generic.Dictionary, not model.Dictionary
            # Extract the generic parameters
            dict_match = re.search(r'Dictionary<([^>]+)>', param_type)
            if dict_match:
                generic_params = dict_match.group(1)
                # Check if enum types need to be fully qualified
                if 'Enum' in generic_params:
                    # Replace enum references with fully qualified names
                    enum_match = re.search(r'(\w+)\.(\w+Enum)', generic_params)
                    if enum_match:
                        # Already has model name prefix, keep it
                        csharp_type = f"Dictionary<{generic_params}>"
                    else:
                        # Need to add model name prefix
                        enum_type = re.search(r'(\w+Enum)', generic_params)
                        if enum_type:
                            generic_params = generic_params.replace(enum_type.group(1), f"{model_name}.{enum_type.group(1)}")
                        csharp_type = f"Dictionary<{generic_params}>"
                else:
                    csharp_type = f"Dictionary<{generic_params}>"
            else:
                csharp_type = param_type
        elif param_type == 'bool':
            csharp_type = 'bool'  # Required, not nullable
        elif param_type == 'int':
            csharp_type = 'int'  # Required, not nullable
        elif param_type == 'string':
            csharp_type = 'string'  # Required, not nullable
        elif 'Enum' in param_type:
            enum_type = param_type.rstrip('?')
            # Check if the original type was nullable
            if param_type.endswith('?'):
                csharp_type = f"{model_name}.{enum_type}?"
            else:
                csharp_type = f"{model_name}.{enum_type}"  # Required, not nullable
        else:
            # Use the type as-is (preserve nullable if present)
            csharp_type = param_type
        
        var_declarations.append(f"            {csharp_type} {var_name} = default({csharp_type});")
        
        # Generate read code
        if param_type == 'string':
            read_codes.append(f"""            if (jsonObject["{json_name}"] != null)
            {{
                {var_name} = jsonObject["{json_name}"].ToObject<string>();
            }}
""")
        elif param_type.startswith('List<'):
            read_codes.append(f"""            if (jsonObject["{json_name}"] != null)
            {{
                {var_name} = jsonObject["{json_name}"].ToObject<{param_type}>(serializer);
            }}
""")
        elif param_type.startswith('Dictionary<'):
            read_codes.append(f"""            if (jsonObject["{json_name}"] != null)
            {{
                {var_name} = jsonObject["{json_name}"].ToObject<{param_type}>(serializer);
            }}
""")
        elif 'Enum' in param_type:
            enum_type = param_type.rstrip('?')
            read_codes.append(f"""            if (jsonObject["{json_name}"] != null)
            {{
                var {var_name}Str = jsonObject["{json_name}"].ToObject<string>();
                if (!string.IsNullOrEmpty({var_name}Str))
                {{
                    {var_name} = {model_name}.{enum_type}FromString({var_name}Str);
                }}
            }}
""")
        else:
            read_codes.append(f"""            if (jsonObject["{json_name}"] != null)
            {{
                {var_name} = jsonObject["{json_name}"].ToObject<{csharp_type}>(serializer);
            }}
""")
        
        # Add to constructor args
        constructor_args.append(f"{param_name}: {var_name}")
    
    # Handle Option<> properties
    for prop in properties:
        prop_type = prop['prop_type'].strip()
        var_name = prop['prop_name'][0].lower() + prop['prop_name'][1:]  # camelCase
        var_name = escape_csharp_keyword(var_name)  # Escape reserved keywords
        
        # Add model name to prop for enum handling
        prop['model_name'] = model_name
        csharp_type = get_csharp_type_for_read(prop_type, model_name)
        
        var_declarations.append(f"            {csharp_type} {var_name} = null;")
        read_codes.append(get_read_code(prop, var_name))
        
        # Constructor argument - use constructor parameter name (lowercase)
        constructor_param_name = prop.get('constructor_param_name', prop['prop_name'][0].lower() + prop['prop_name'][1:])
        # Escape reserved keywords in constructor parameter name
        constructor_param_name = escape_csharp_keyword(constructor_param_name)
        
        # For enum types, use fully qualified type in Option<>
        if 'Enum' in prop_type:
            enum_type = prop_type.rstrip('?')
            option_type = f"{model_name}.{enum_type}?"
        else:
            option_type = f"{prop_type}?"
        
        constructor_args.append(f"{constructor_param_name}: {var_name} != null ? new Option<{option_type}>({var_name}) : default")
    
    # Write codes
    write_codes = [get_write_code(prop) for prop in properties]
    
    # Generate converter code
    converter_code = f"""using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Kinde.Api.Model;
using Kinde.Api.Client;

namespace Kinde.Api.Converters
{{
    /// <summary>
    /// Newtonsoft.Json converter for {model_name} that handles the Option<> structure
    /// </summary>
    public class {model_name}NewtonsoftConverter : Newtonsoft.Json.JsonConverter<{model_name}>
    {{
        public override bool CanRead => true;
        public override bool CanWrite => true;

        public override {model_name} ReadJson(Newtonsoft.Json.JsonReader reader, Type objectType, {model_name} existingValue, bool hasExistingValue, Newtonsoft.Json.JsonSerializer serializer)
        {{
            if (reader.TokenType != Newtonsoft.Json.JsonToken.StartObject)
            {{
                throw new Newtonsoft.Json.JsonException($"Expected StartObject, got {{reader.TokenType}}");
            }}

{chr(10).join(var_declarations)}

            var jsonObject = JObject.Load(reader);

{chr(10).join(read_codes)}
            return new {model_name}(
                {', '.join(constructor_args)}
            );
        }}

        public override void WriteJson(Newtonsoft.Json.JsonWriter writer, {model_name} value, Newtonsoft.Json.JsonSerializer serializer)
        {{
            writer.WriteStartObject();

{chr(10).join(write_codes)}
            writer.WriteEndObject();
        }}
    }}
}}
"""
    
    return converter_code

def main():
    """Generate converters for all Response and Request models"""
    if not os.path.exists(MODEL_DIR):
        print(f"Error: {MODEL_DIR} not found")
        return
    
    if not os.path.exists(CONVERTER_DIR):
        os.makedirs(CONVERTER_DIR)
    
    # Find all Response model files
    response_files = [f for f in os.listdir(MODEL_DIR) if f.endswith("Response.cs") and f != "ErrorResponse.cs"]
    
    # Find all Request model files
    request_files = [f for f in os.listdir(MODEL_DIR) if f.endswith("Request.cs")]
    
    # Exclude manually maintained Request converters
    excluded_requests = {
        "CreateUserRequestIdentitiesInner",  # Manually maintained
        "CreateUserIdentityRequest"  # Manually maintained
    }
    
    generated = 0
    skipped = 0
    errors = []
    
    # Process Response models
    print("Processing Response models...")
    for response_file in sorted(response_files):
        model_name = response_file.replace(".cs", "")
        
        # Skip if model is in the exclusion list (empty by default to regenerate all)
        if model_name in EXISTING:
            skipped += 1
            print(f"Skipped {model_name} (in exclusion list)")
            continue
        
        model_path = os.path.join(MODEL_DIR, response_file)
        result = extract_properties_from_model(model_path)
        
        if not result:
            errors.append(f"Could not extract properties from {model_name}")
            continue
        
        if len(result) == 3:
            extracted_name, properties, required_params = result
        else:
            extracted_name, properties = result
            required_params = {}
        
        # Generate converter (even if no Option properties, if there are required params)
        if not properties and not required_params:
            continue
        
        # Generate converter
        try:
            converter_code = generate_converter(model_name, properties, required_params)
            converter_file = os.path.join(CONVERTER_DIR, f"{model_name}NewtonsoftConverter.cs")
            
            with open(converter_file, 'w', encoding='utf-8') as f:
                f.write(converter_code)
            
            prop_count = len(properties) + len(required_params)
            print(f"Generated converter for {model_name} ({prop_count} properties)")
            generated += 1
        except Exception as e:
            errors.append(f"Error generating {model_name}: {e}")
    
    # Process Request models
    print("\nProcessing Request models...")
    for request_file in sorted(request_files):
        model_name = request_file.replace(".cs", "")
        
        # Skip manually maintained Request converters
        if model_name in excluded_requests:
            skipped += 1
            print(f"Skipped {model_name} (manually maintained)")
            continue
        
        # Skip if model is in the exclusion list
        if model_name in EXISTING:
            skipped += 1
            print(f"Skipped {model_name} (in exclusion list)")
            continue
        
        model_path = os.path.join(MODEL_DIR, request_file)
        result = extract_properties_from_model(model_path)
        
        if not result:
            errors.append(f"Could not extract properties from {model_name}")
            continue
        
        if len(result) == 3:
            extracted_name, properties, required_params = result
        else:
            extracted_name, properties = result
            required_params = {}
        
        # Generate converter (even if no Option properties, if there are required params)
        if not properties and not required_params:
            continue
        
        # Generate converter
        try:
            converter_code = generate_converter(model_name, properties, required_params)
            converter_file = os.path.join(CONVERTER_DIR, f"{model_name}NewtonsoftConverter.cs")
            
            with open(converter_file, 'w', encoding='utf-8') as f:
                f.write(converter_code)
            
            prop_count = len(properties) + len(required_params)
            print(f"Generated converter for {model_name} ({prop_count} properties)")
            generated += 1
        except Exception as e:
            errors.append(f"Error generating {model_name}: {e}")
    
    print(f"\nGenerated {generated} converters, skipped {skipped} existing ones")
    if errors:
        print(f"\nErrors ({len(errors)}):")
        for error in errors[:10]:  # Show first 10 errors
            print(f"  {error}")

if __name__ == "__main__":
    main()

