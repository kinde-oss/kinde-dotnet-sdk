#!/usr/bin/env python3
"""
Reorganize Accounts API models into sub-namespaces.
"""

import os
import re
import sys
from pathlib import Path
from shutil import move

# Entity models (simple domain objects)
ENTITY_MODELS = [
    'FeatureFlag.cs',
    'FeatureFlagValue.cs',
    'Permission.cs',
    'Role.cs',
    'UserProperty.cs',
    'UserPropertyValue.cs',
    'Entitlement.cs',
    'Plan.cs',
    'EntitlementDetail.cs',
    'UserProfileV2.cs',
    'PortalLink.cs',
    'TokenIntrospect.cs',
    'Error.cs',
]

# Response models (API response wrappers)
RESPONSE_MODELS = [
    'GetFeatureFlagsResponse.cs',
    'GetFeatureFlagsResponseData.cs',
    'GetUserPermissionsResponse.cs',
    'GetUserPermissionsResponseData.cs',
    'GetUserPermissionsResponseMetadata.cs',
    'GetUserRolesResponse.cs',
    'GetUserRolesResponseData.cs',
    'GetUserRolesResponseMetadata.cs',
    'GetUserPropertiesResponse.cs',
    'GetUserPropertiesResponseData.cs',
    'GetUserPropertiesResponseMetadata.cs',
    'GetEntitlementsResponse.cs',
    'GetEntitlementsResponseData.cs',
    'GetEntitlementsResponseMetadata.cs',
    'GetEntitlementResponse.cs',
    'GetEntitlementResponseData.cs',
    'ErrorResponse.cs',
    'TokenErrorResponse.cs',
]

def update_namespace_in_file(file_path: Path, new_namespace: str) -> bool:
    """Update namespace in a file"""
    try:
        with open(file_path, 'r', encoding='utf-8') as f:
            content = f.read()
        
        original_content = content
        
        # Replace namespace declaration
        content = re.sub(
            r'namespace\s+Kinde\.Accounts\.Model\b',
            f'namespace {new_namespace}',
            content
        )
        
        if content != original_content:
            with open(file_path, 'w', encoding='utf-8') as f:
                f.write(content)
            return True
        return False
    except Exception as e:
        print(f"Error updating namespace in {file_path}: {e}", file=sys.stderr)
        return False

def extract_converter_to_separate_file(model_file: Path, entities_dir: Path, converters_dir: Path):
    """Extract JsonConverter class to a separate file"""
    try:
        with open(model_file, 'r', encoding='utf-8') as f:
            content = f.read()
        
        # Check if file has a converter
        converter_match = re.search(
            r'(public\s+class\s+(\w+JsonConverter)\s*:\s*JsonConverter<[^>]+>.*?})',
            content,
            re.DOTALL
        )
        
        if not converter_match:
            return False
        
        converter_class = converter_match.group(1)
        converter_class_name = converter_match.group(2)
        
        # Get the model class name from the file
        model_class_match = re.search(r'public\s+partial\s+class\s+(\w+)', content)
        if not model_class_match:
            return False
        
        model_class_name = model_class_match[1]
        
        # Remove converter from model file
        content_without_converter = re.sub(
            r'\s*///\s*<summary>.*?A Json converter.*?</summary>.*?public\s+class\s+\w+JsonConverter.*?}\s*}',
            '',
            content,
            flags=re.DOTALL
        )
        
        # Clean up extra blank lines
        content_without_converter = re.sub(r'\n\n\n+', '\n\n', content_without_converter)
        
        # Write updated model file
        with open(model_file, 'w', encoding='utf-8') as f:
            f.write(content_without_converter)
        
        # Create converter file
        converter_file = converters_dir / f"{converter_class_name}.cs"
        
        # Get namespace from model file
        namespace_match = re.search(r'namespace\s+([^\s{]+)', content_without_converter)
        namespace = namespace_match.group(1) if namespace_match else "Kinde.Accounts.Model.Converters"
        
        converter_content = f"""#nullable enable

using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using {namespace.replace('.Converters', '.Entities')};

namespace {namespace}
{{
    /// <summary>
    /// JSON converter for <see cref="{model_class_name}" />
    /// </summary>
{converter_class}
}}
"""
        
        with open(converter_file, 'w', encoding='utf-8') as f:
            f.write(converter_content)
        
        return True
    except Exception as e:
        print(f"Error extracting converter from {model_file}: {e}", file=sys.stderr)
        return False

def main():
    """Main entry point"""
    if len(sys.argv) < 2:
        print("Usage: reorganize-accounts-models.py <accounts-api-dir>")
        sys.exit(1)
    
    accounts_dir = Path(sys.argv[1])
    model_dir = accounts_dir / "Model"
    
    if not model_dir.exists():
        print(f"Error: Model directory not found: {model_dir}")
        sys.exit(1)
    
    entities_dir = model_dir / "Entities"
    responses_dir = model_dir / "Responses"
    converters_dir = model_dir / "Converters"
    
    entities_dir.mkdir(exist_ok=True)
    responses_dir.mkdir(exist_ok=True)
    converters_dir.mkdir(exist_ok=True)
    
    print("Step 1: Moving entity models...")
    entity_count = 0
    for entity_file in ENTITY_MODELS:
        source = model_dir / entity_file
        if source.exists():
            dest = entities_dir / entity_file
            move(str(source), str(dest))
            update_namespace_in_file(dest, "Kinde.Accounts.Model.Entities")
            entity_count += 1
            print(f"  Moved: {entity_file}")
    
    print(f"\nMoved {entity_count} entity models")
    
    print("\nStep 2: Moving response models...")
    response_count = 0
    for response_file in RESPONSE_MODELS:
        source = model_dir / response_file
        if source.exists():
            dest = responses_dir / response_file
            move(str(source), str(dest))
            update_namespace_in_file(dest, "Kinde.Accounts.Model.Responses")
            response_count += 1
            print(f"  Moved: {response_file}")
    
    print(f"\nMoved {response_count} response models")
    
    print("\nStep 3: Extracting converters...")
    converter_count = 0
    for entity_file in entities_dir.glob("*.cs"):
        if extract_converter_to_separate_file(entity_file, entities_dir, converters_dir):
            converter_count += 1
            print(f"  Extracted converter from: {entity_file.name}")
    
    print(f"\nExtracted {converter_count} converters")
    
    print("\nReorganization complete!")

if __name__ == '__main__':
    main()

