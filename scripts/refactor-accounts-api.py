#!/usr/bin/env python3
"""
Refactor Accounts API: Rename classes, reorganize namespaces, and improve structure.
"""

import os
import re
import sys
from pathlib import Path
from typing import Dict, List, Tuple

# Mapping of old class names to new class names
CLASS_RENAMES = {
    # Feature Flags
    'GetFeatureFlagsResponseDataFeatureFlagsInner': 'FeatureFlag',
    'GetFeatureFlagsResponseDataFeatureFlagsInnerValue': 'FeatureFlagValue',
    'GetFeatureFlagsResponseDataFeatureFlagsInnerJsonConverter': 'FeatureFlagJsonConverter',
    'GetFeatureFlagsResponseDataFeatureFlagsInnerValueJsonConverter': 'FeatureFlagValueJsonConverter',
    
    # Permissions
    'GetUserPermissionsResponseDataPermissionsInner': 'Permission',
    'GetUserPermissionsResponseDataPermissionsInnerJsonConverter': 'PermissionJsonConverter',
    
    # Roles
    'GetUserRolesResponseDataRolesInner': 'Role',
    'GetUserRolesResponseDataRolesInnerJsonConverter': 'RoleJsonConverter',
    
    # Properties
    'GetUserPropertiesResponseDataPropertiesInner': 'UserProperty',
    'GetUserPropertiesResponseDataPropertiesInnerValue': 'UserPropertyValue',
    'GetUserPropertiesResponseDataPropertiesInnerJsonConverter': 'UserPropertyJsonConverter',
    'GetUserPropertiesResponseDataPropertiesInnerValueJsonConverter': 'UserPropertyValueJsonConverter',
    
    # Entitlements
    'GetEntitlementsResponseDataEntitlementsInner': 'Entitlement',
    'GetEntitlementsResponseDataPlansInner': 'Plan',
    'GetEntitlementResponseDataEntitlement': 'EntitlementDetail',  # Avoid conflict with Entitlement
    'GetEntitlementsResponseDataEntitlementsInnerJsonConverter': 'EntitlementJsonConverter',
    'GetEntitlementsResponseDataPlansInnerJsonConverter': 'PlanJsonConverter',
    'GetEntitlementResponseDataEntitlementJsonConverter': 'EntitlementDetailJsonConverter',
}

# File renames (old file -> new file)
FILE_RENAMES = {
    'GetFeatureFlagsResponseDataFeatureFlagsInner.cs': 'FeatureFlag.cs',
    'GetFeatureFlagsResponseDataFeatureFlagsInnerValue.cs': 'FeatureFlagValue.cs',
    'GetUserPermissionsResponseDataPermissionsInner.cs': 'Permission.cs',
    'GetUserRolesResponseDataRolesInner.cs': 'Role.cs',
    'GetUserPropertiesResponseDataPropertiesInner.cs': 'UserProperty.cs',
    'GetUserPropertiesResponseDataPropertiesInnerValue.cs': 'UserPropertyValue.cs',
    'GetEntitlementsResponseDataEntitlementsInner.cs': 'Entitlement.cs',
    'GetEntitlementsResponseDataPlansInner.cs': 'Plan.cs',
    'GetEntitlementResponseDataEntitlement.cs': 'EntitlementDetail.cs',
}

def rename_in_file(file_path: Path, renames: Dict[str, str]) -> bool:
    """Rename all occurrences of old names to new names in a file"""
    try:
        with open(file_path, 'r', encoding='utf-8') as f:
            content = f.read()
        
        original_content = content
        
        # Replace all occurrences
        for old_name, new_name in renames.items():
            # Word boundary replacements to avoid partial matches
            content = re.sub(r'\b' + re.escape(old_name) + r'\b', new_name, content)
        
        if content != original_content:
            with open(file_path, 'w', encoding='utf-8') as f:
                f.write(content)
            return True
        return False
    except Exception as e:
        print(f"Error processing {file_path}: {e}", file=sys.stderr)
        return False

def update_namespace_in_file(file_path: Path, old_namespace: str, new_namespace: str) -> bool:
    """Update namespace in a file"""
    try:
        with open(file_path, 'r', encoding='utf-8') as f:
            content = f.read()
        
        original_content = content
        
        # Replace namespace declaration
        content = re.sub(
            r'namespace\s+' + re.escape(old_namespace),
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

def main():
    """Main entry point"""
    if len(sys.argv) < 2:
        print("Usage: refactor-accounts-api.py <accounts-api-dir>")
        sys.exit(1)
    
    accounts_dir = Path(sys.argv[1])
    
    if not accounts_dir.exists():
        print(f"Error: Directory not found: {accounts_dir}")
        sys.exit(1)
    
    model_dir = accounts_dir / "Model"
    if not model_dir.exists():
        print(f"Error: Model directory not found: {model_dir}")
        sys.exit(1)
    
    print("Step 1: Renaming classes in all files...")
    # Find all C# files
    cs_files = list(accounts_dir.rglob("*.cs"))
    
    renamed_count = 0
    for cs_file in cs_files:
        if rename_in_file(cs_file, CLASS_RENAMES):
            renamed_count += 1
            print(f"  Updated: {cs_file.relative_to(accounts_dir)}")
    
    print(f"\nRenamed classes in {renamed_count} files")
    
    print("\nStep 2: Renaming files...")
    renamed_files = 0
    for old_file, new_file in FILE_RENAMES.items():
        old_path = model_dir / old_file
        new_path = model_dir / new_file
        if old_path.exists() and not new_path.exists():
            old_path.rename(new_path)
            renamed_files += 1
            print(f"  Renamed: {old_file} -> {new_file}")
    
    print(f"\nRenamed {renamed_files} files")
    
    print("\nRefactoring complete!")
    print("\nNext steps:")
    print("1. Create sub-namespaces (Responses, Entities, Converters)")
    print("2. Move files to appropriate directories")
    print("3. Update using statements")

if __name__ == '__main__':
    main()

