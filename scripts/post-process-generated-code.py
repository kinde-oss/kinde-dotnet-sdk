#!/usr/bin/env python3
"""
Post-processing script for OpenAPI Generator 7.0.1 generated code.

This script applies compatibility fixes to generated C# files to ensure
they work correctly with the Kinde .NET SDK.

Fixes applied:
1. Removes certificate strings from XML comments
2. Adds OpenAPIClientUtils alias to model files that need it
3. Fixes ApiResponse.Ok()?.Data to use AsModel() for 7.0.1 compatibility
4. Fixes Accounts API constructor calls (6 params -> 5 params)
5. Fixes nullable type constructor calls (adds .Value for bool?, int?, etc.)
6. ApiClient.cs: response buffering fix so GetUsersAsync() returns same data as
   GetUsersWithHttpInfoAsync() (buffer response content once, pass to Deserialize
   and ToApiResponse). Patch applied from scripts/patches/ApiClient.cs.
7. Other compatibility fixes as needed
"""

import os
import re
import sys
from pathlib import Path
from typing import List, Tuple


def log(message: str, level: str = "INFO"):
    """Print a log message with color coding."""
    colors = {
        "INFO": "\033[0;34m",      # Blue
        "SUCCESS": "\033[0;32m",   # Green
        "WARNING": "\033[1;33m",  # Yellow
        "ERROR": "\033[0;31m",     # Red
        "RESET": "\033[0m"         # Reset
    }
    prefix = {
        "INFO": "[INFO]",
        "SUCCESS": "[SUCCESS]",
        "WARNING": "[WARNING]",
        "ERROR": "[ERROR]"
    }
    print(f"{colors.get(level, '')}{prefix.get(level, '')} {message}{colors['RESET']}")


def fix_certificate_strings_in_xml_comments(content: str) -> Tuple[str, bool]:
    """
    Remove certificate strings that appear in XML comments.
    
    These are base64-encoded certificate strings that break XML comment syntax.
    Handles multiple patterns including certificate data in <example> tags.
    """
    original = content
    
    # Pattern 1: Certificate string between </value> and [DataMember] (no example tag)
    # Matches: </value>\nMIIDdTCC...\n[DataMember]
    pattern1 = re.compile(
        r'(</value>\s*)([A-Za-z0-9+/=\s]{50,})(\s*\[DataMember)',
        re.MULTILINE
    )
    content = pattern1.sub(r'\1\3', content)
    
    # Pattern 2: Certificate string in the middle of XML comment (no example tag)
    # Matches: /// <value>Certificate...</value>\nMIIDdTCC...\n[DataMember]
    pattern2 = re.compile(
        r'(///\s*<value>[^<]*</value>\s*)([A-Za-z0-9+/=\s]{50,})(\s*\[DataMember)',
        re.MULTILINE
    )
    content = pattern2.sub(r'\1\3', content)
    
    # Pattern 3: Certificate string after </summary> or </value> (no example tag)
    pattern3 = re.compile(
        r'(</(?:summary|value)>\s*)([A-Za-z0-9+/=\s]{50,})(\s*\[DataMember)',
        re.MULTILINE
    )
    content = pattern3.sub(r'\1\3', content)
    
    # Pattern 4: Certificate data inside <example> tags (most common in 7.0.1)
    # Matches: <example>-----BEGIN CERTIFICATE-----\nMIIDdTCC...\n-----END CERTIFICATE-----</example>
    pattern4 = re.compile(
        r'(<example>-----BEGIN\s+(?:CERTIFICATE|PRIVATE\s+KEY|PUBLIC\s+KEY)-----)\s*[A-Za-z0-9+/=\s\n]{50,}?-----END\s+(?:CERTIFICATE|PRIVATE\s+KEY|PUBLIC\s+KEY)-----(</example>)',
        re.MULTILINE | re.DOTALL
    )
    content = pattern4.sub(r'<example>Example certificate or key data</example>', content)
    
    # Pattern 5: Certificate data in example tag without BEGIN/END markers
    # Matches: <example>\nMIIDdTCC...\n</example>
    pattern5 = re.compile(
        r'(<example>)\s*([A-Za-z0-9+/=\s]{50,}?)(</example>)',
        re.MULTILINE | re.DOTALL
    )
    content = pattern5.sub(r'\1Example certificate or key data\3', content)
    
    modified = content != original
    return content, modified


def fix_api_response_ok_calls(content: str) -> Tuple[str, bool]:
    """
    Fix ApiResponse.Ok()?.Data calls to use AsModel()?.Data for OpenAPI Generator 7.0.1.
    
    In 7.0.1, ApiResponse<T> uses AsModel() instead of Ok()?.Data.
    AsModel() returns the full response object, so we need to access .Data property.
    """
    original = content
    
    # Pattern 1: response.Ok()?.Data -> response.AsModel()?.Data
    pattern1 = re.compile(
        r'(\w+)\.Ok\(\)\?\.Data',
        re.MULTILINE
    )
    content = pattern1.sub(r'\1.AsModel()?.Data', content)
    
    # Pattern 2: Fix cases where AsModel() was already applied but .Data is missing
    # Only fix if the return type expects a *Data type (not the full Response type)
    # This is a heuristic - we look for return statements that return AsModel() without .Data
    # and the method return type contains "Data"
    # We'll be conservative and only fix obvious cases
    
    modified = content != original
    return content, modified


def fix_accounts_api_constructors(content: str) -> Tuple[str, bool]:
    """
    Fix Accounts API constructor calls from 6 parameters to 5 parameters.
    
    The generated 7.0.1 constructors take:
    1. ILogger<T>
    2. HttpClient
    3. JsonSerializerOptionsProvider
    4. ApiEvents
    5. TokenProvider<BearerToken>
    
    But Auth.cs is calling with:
    1. loggerFactory.CreateLogger<T>()
    2. loggerFactory
    3. httpClient
    4. null
    5. new ApiEvents()
    6. bearerTokenProvider
    
    Need to change to:
    1. loggerFactory.CreateLogger<T>()
    2. httpClient
    3. null (JsonSerializerOptionsProvider)
    4. new ApiEvents()
    5. bearerTokenProvider
    """
    original = content
    
    # Pattern for BillingApi, PermissionsApi, RolesApi, FeatureFlagsApi constructors
    # Match: new ApiName(loggerFactory.CreateLogger<ApiName>(), loggerFactory, httpClient, null, new ApiNameEvents(), bearerTokenProvider)
    # Replace: new ApiName(loggerFactory.CreateLogger<ApiName>(), httpClient, null, new ApiNameEvents(), bearerTokenProvider)
    
    api_names = ['BillingApi', 'PermissionsApi', 'RolesApi', 'FeatureFlagsApi']
    
    for api_name in api_names:
        # Pattern to match the 6-parameter constructor call
        pattern = re.compile(
            rf'new\s+{api_name}\(([^,]+),\s*loggerFactory,\s*httpClient,\s*null,\s*new\s+{api_name}Events\(\)\s*,\s*([^)]+)\)',
            re.MULTILINE
        )
        # Replace with 5-parameter call (remove loggerFactory)
        content = pattern.sub(
            rf'new {api_name}(\1, httpClient, null, new {api_name}Events(), \2)',
            content
        )
    
    modified = content != original
    return content, modified


def fix_nullable_constructor_calls(content: str) -> Tuple[str, bool]:
    """
    Fix nullable type constructor calls that need .Value unwrapping.
    
    In some generated oneOf/anyOf models, constructors take non-nullable types
    (bool, int) but the deserialization code passes nullable types (bool?, int?).
    Since we check for null before calling the constructor, we can safely use .Value.
    
    Pattern to fix:
    - if (varBool != null) return new Type(varBool); -> if (varBool != null) return new Type(varBool.Value);
    - if (varInt != null) return new Type(varInt); -> if (varInt != null) return new Type(varInt.Value);
    """
    original = content
    
    # Pattern 1: bool? to bool constructor call
    # Matches: if (varBool != null) return new TypeName(varBool);
    pattern1 = re.compile(
        r'(if\s+\((\w+Bool)\s*!=\s*null\)\s*return\s+new\s+\w+\()\2(\);)',
        re.MULTILINE
    )
    content = pattern1.sub(r'\1\2.Value\3', content)
    
    # Pattern 2: int? to int constructor call
    # Matches: if (varInt != null) return new TypeName(varInt);
    pattern2 = re.compile(
        r'(if\s+\((\w+Int)\s*!=\s*null\)\s*return\s+new\s+\w+\()\2(\);)',
        re.MULTILINE
    )
    content = pattern2.sub(r'\1\2.Value\3', content)
    
    # Pattern 3: long? to long constructor call
    # Matches: if (varLong != null) return new TypeName(varLong);
    pattern3 = re.compile(
        r'(if\s+\((\w+Long)\s*!=\s*null\)\s*return\s+new\s+\w+\()\2(\);)',
        re.MULTILINE
    )
    content = pattern3.sub(r'\1\2.Value\3', content)
    
    # Pattern 4: double? to double constructor call
    # Matches: if (varDouble != null) return new TypeName(varDouble);
    pattern4 = re.compile(
        r'(if\s+\((\w+Double)\s*!=\s*null\)\s*return\s+new\s+\w+\()\2(\);)',
        re.MULTILINE
    )
    content = pattern4.sub(r'\1\2.Value\3', content)
    
    # Pattern 5: float? to float constructor call
    # Matches: if (varFloat != null) return new TypeName(varFloat);
    pattern5 = re.compile(
        r'(if\s+\((\w+Float)\s*!=\s*null\)\s*return\s+new\s+\w+\()\2(\);)',
        re.MULTILINE
    )
    content = pattern5.sub(r'\1\2.Value\3', content)
    
    modified = content != original
    return content, modified


def add_openapi_client_utils_alias(content: str, file_path: str) -> Tuple[str, bool]:
    """
    Add OpenAPIClientUtils alias if the file uses OpenAPIClientUtils but doesn't have the alias.
    
    Only applies to Accounts API Model files that use OpenAPIClientUtils.
    """
    # Check if file uses OpenAPIClientUtils
    if 'OpenAPIClientUtils' not in content:
        return content, False
    
    # Check if alias already exists
    if 'using OpenAPIClientUtils =' in content:
        return content, False
    
    # Only apply to Accounts Model files
    if 'Kinde.Accounts.Model' not in content:
        return content, False
    
    # Find the last using statement before namespace
    using_pattern = re.compile(
        r'(using\s+[^;]+;\s*\n)',
        re.MULTILINE
    )
    
    # Find where namespace starts
    namespace_match = re.search(r'namespace\s+Kinde\.Accounts\.Model', content)
    if not namespace_match:
        return content, False
    
    # Find all using statements before namespace
    using_statements = []
    for match in using_pattern.finditer(content):
        if match.end() < namespace_match.start():
            using_statements.append(match)
    
    if not using_statements:
        return content, False
    
    # Insert alias after last using statement
    last_using = using_statements[-1]
    alias = 'using OpenAPIClientUtils = Kinde.Accounts.Client.ClientUtils;\n'
    
    # Check if already added
    if alias.strip() in content[:namespace_match.start()]:
        return content, False
    
    insert_pos = last_using.end()
    content = content[:insert_pos] + alias + content[insert_pos:]
    return content, True


def apply_api_client_patch(file_path: Path) -> bool:
    """
    Apply the ApiClient.cs response buffering fix (GetUsersAsync returns all fields).
    When the generator outputs Kinde.Api/Client/ApiClient.cs, we overwrite it with
    our patched version from scripts/patches/ApiClient.cs.
    """
    if file_path.name != "ApiClient.cs":
        return False
    parts = file_path.parts
    if "Client" not in parts or "Kinde.Api" not in parts:
        return False
    # Must be .../Kinde.Api/Client/ApiClient.cs (main API, not Accounts)
    try:
        kinde_idx = parts.index("Kinde.Api")
        if kinde_idx + 1 >= len(parts) or parts[kinde_idx + 1] != "Client":
            return False
    except ValueError:
        return False
    script_dir = Path(__file__).resolve().parent
    patch_file = script_dir / "patches" / "ApiClient.cs"
    if not patch_file.is_file():
        log(f"Patch file not found: {patch_file}", "WARNING")
        return False
    try:
        patch_content = patch_file.read_text(encoding="utf-8")
        file_path.write_text(patch_content, encoding="utf-8")
        log(f"Applied ApiClient.cs response buffering fix to {file_path}", "SUCCESS")
        return True
    except Exception as e:
        log(f"Failed to apply ApiClient patch: {e}", "ERROR")
        return False


def process_file(file_path: Path) -> bool:
    """Process a single C# file and apply all fixes."""
    try:
        # Apply ApiClient.cs patch first (replaces entire file for Kinde.Api/Client/ApiClient.cs)
        if apply_api_client_patch(file_path):
            return True

        with open(file_path, 'r', encoding='utf-8') as f:
            content = f.read()
        
        original_content = content
        modified = False
        
        # Apply fixes
        content, cert_fixed = fix_certificate_strings_in_xml_comments(content)
        if cert_fixed:
            modified = True
            log(f"Fixed certificate strings in {file_path.name}", "SUCCESS")
        
        content, alias_added = add_openapi_client_utils_alias(content, str(file_path))
        if alias_added:
            modified = True
            log(f"Added OpenAPIClientUtils alias to {file_path.name}", "SUCCESS")
        
        content, ok_fixed = fix_api_response_ok_calls(content)
        if ok_fixed:
            modified = True
            log(f"Fixed ApiResponse.Ok() calls in {file_path.name}", "SUCCESS")
        
        content, constructor_fixed = fix_accounts_api_constructors(content)
        if constructor_fixed:
            modified = True
            log(f"Fixed Accounts API constructor calls in {file_path.name}", "SUCCESS")
        
        content, nullable_fixed = fix_nullable_constructor_calls(content)
        if nullable_fixed:
            modified = True
            log(f"Fixed nullable constructor calls in {file_path.name}", "SUCCESS")
        
        # Write back if modified
        if modified:
            with open(file_path, 'w', encoding='utf-8') as f:
                f.write(content)
            return True
        
        return False
    except Exception as e:
        log(f"Error processing {file_path}: {e}", "ERROR")
        return False


def process_directory(directory: Path, pattern: str = "*.cs") -> int:
    """Process all C# files in a directory recursively."""
    files_processed = 0
    files_modified = 0
    
    for file_path in directory.rglob(pattern):
        if file_path.is_file():
            files_processed += 1
            if process_file(file_path):
                files_modified += 1
    
    return files_modified


def main():
    """Main entry point."""
    if len(sys.argv) < 2:
        log("Usage: post-process-generated-code.py <directory> [directory2 ...]", "ERROR")
        sys.exit(1)
    
    directories = [Path(d) for d in sys.argv[1:]]
    
    log("Starting post-processing of generated code...")
    
    total_modified = 0
    for directory in directories:
        if not directory.exists():
            log(f"Directory does not exist: {directory}", "WARNING")
            continue
        
        log(f"Processing directory: {directory}")
        modified = process_directory(directory)
        total_modified += modified
    
    if total_modified > 0:
        log(f"Post-processing complete. Modified {total_modified} file(s).", "SUCCESS")
    else:
        log("Post-processing complete. No files needed modification.", "INFO")
    
    sys.exit(0)


if __name__ == "__main__":
    main()

