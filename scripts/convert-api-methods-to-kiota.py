#!/usr/bin/env python3
"""
Convert API method implementations to use Kiota internally.

This script rewrites the *WithHttpInfoAsync methods to use Kiota,
keeping the same method signatures for backward compatibility.
"""

import os
import re
from pathlib import Path
from typing import List, Dict, Tuple, Optional

# Kiota endpoint mappings - maps API class to Kiota base path
KIOTA_BASE_PATHS = {
    'APIsApi': ('Apis', 'Apis'),
    'ApplicationsApi': ('Applications', 'Applications'),
    'BillingAgreementsApi': ('Billing.Agreements', 'Billing.Agreements'),
    'BillingEntitlementsApi': ('Billing.Entitlements', 'Billing.Entitlements'),
    'BillingMeterUsageApi': ('Billing.Meter_usage', 'Billing.Meter_usage'),
    'BusinessApi': ('Business', 'Business'),
    'CallbacksApi': ('Applications', 'Applications'),
    'ConnectedAppsApi': ('Connected_apps', 'Connected_apps'),
    'ConnectionsApi': ('Connections', 'Connections'),
    'EnvironmentsApi': ('EnvironmentNamespace', 'EnvironmentNamespace'),
    'EnvironmentVariablesApi': ('Environment_variables', 'Environment_variables'),
    'FeatureFlagsApi': ('Feature_flags', 'Feature_flags'),
    'IdentitiesApi': ('Identities', 'Identities'),
    'IndustriesApi': ('Industries', 'Industries'),
    'MFAApi': ('Mfa', 'Mfa'),
    'OrganizationsApi': ('Organizations', 'Organization'),
    'PermissionsApi': ('Permissions', 'Permissions'),
    'PropertiesApi': ('Properties', 'Properties'),
    'PropertyCategoriesApi': ('Property_categories', 'Property_categories'),
    'RolesApi': ('Roles', 'Roles'),
    'SearchApi': ('Search', 'Search'),
    'SubscribersApi': ('Subscribers', 'Subscribers'),
    'TimezonesApi': ('Timezones', 'Timezones'),
    'UsersApi': ('Users', 'User'),
    'WebhooksApi': ('Webhooks', 'Webhooks'),
}

def find_method_bounds(content: str, method_name: str, signature_pattern: str) -> Optional[Tuple[int, int, str]]:
    """Find the bounds of a method implementation."""
    # Find the method signature
    match = re.search(signature_pattern, content)
    if not match:
        return None
    
    start = match.start()
    sig_end = match.end()
    
    # Find the opening brace
    brace_pos = content.find('{', sig_end)
    if brace_pos == -1:
        return None
    
    # Count braces to find matching closing brace
    brace_count = 1
    pos = brace_pos + 1
    while brace_count > 0 and pos < len(content):
        if content[pos] == '{':
            brace_count += 1
        elif content[pos] == '}':
            brace_count -= 1
        pos += 1
    
    return (start, pos, content[start:pos])

def extract_endpoint_from_method(method_body: str) -> Optional[str]:
    """Extract the API endpoint from method body."""
    # Look for patterns like: .GetAsync<...>("/api/v1/users", ...)
    # or .PostAsync<...>("/api/v1/user", ...)
    match = re.search(r'\.(?:Get|Post|Put|Patch|Delete)Async<[^>]+>\("(/api/v1/[^"]+)"', method_body)
    if match:
        return match.group(1)
    
    match = re.search(r'\.(?:Get|Post|Put|Patch|Delete)<[^>]+>\("(/api/v1/[^"]+)"', method_body)
    if match:
        return match.group(1)
    
    return None

def extract_http_method(method_body: str) -> str:
    """Extract HTTP method from method body."""
    if '.PostAsync<' in method_body or '.Post<' in method_body:
        return 'Post'
    elif '.PutAsync<' in method_body or '.Put<' in method_body:
        return 'Put'
    elif '.PatchAsync<' in method_body or '.Patch<' in method_body:
        return 'Patch'
    elif '.DeleteAsync<' in method_body or '.Delete<' in method_body:
        return 'Delete'
    return 'Get'

def extract_return_type(signature: str) -> str:
    """Extract return type from method signature."""
    match = re.search(r'ApiResponse<(\w+)>', signature)
    return match.group(1) if match else 'object'

def endpoint_to_kiota_path(endpoint: str, api_class: str) -> str:
    """Convert an API endpoint to a Kiota path."""
    # /api/v1/users -> Api.V1.Users
    # /api/v1/users/{user_id}/sessions -> Api.V1.Users[userId].Sessions
    # /api/v1/user -> Api.V1.User
    
    parts = endpoint.strip('/').split('/')
    kiota_parts = []
    
    for part in parts:
        if part.startswith('{') and part.endswith('}'):
            # Path parameter like {user_id}
            param_name = part[1:-1].replace('_', '')  # Remove underscores
            # Convert to camelCase
            param_name = param_name[0].lower() + param_name[1:] if param_name else param_name
            kiota_parts.append(f'[{param_name}]')
        else:
            # Static path segment
            # Convert snake_case to PascalCase for Kiota
            pascal_part = ''.join(word.capitalize() for word in part.split('_'))
            kiota_parts.append(pascal_part)
    
    return '.'.join(kiota_parts)

def generate_kiota_implementation(
    method_name: str,
    return_type: str,
    http_method: str,
    endpoint: str,
    api_class: str,
    has_request_body: bool = False
) -> str:
    """Generate a Kiota-based method implementation."""
    
    kiota_path = endpoint_to_kiota_path(endpoint, api_class)
    kiota_method = http_method + 'Async'
    
    # Determine if we need path parameters
    path_params = re.findall(r'\{(\w+)\}', endpoint)
    
    impl = f'''
            // ===== Kiota Implementation =====
            try
            {{
                var kiotaResponse = await KiotaClient.{kiota_path}.{kiota_method}(
                    cancellationToken: cancellationToken
                ).ConfigureAwait(false);

                var mappedResponse = KiotaMapper.Map<{return_type}>(kiotaResponse);
                return new Kinde.Api.Client.ApiResponse<{return_type}>(
                    System.Net.HttpStatusCode.OK,
                    new Multimap<string, string>(),
                    mappedResponse
                );
            }}
            catch (Microsoft.Kiota.Abstractions.ApiException ex)
            {{
                throw new Kinde.Api.Client.ApiException((int)ex.ResponseStatusCode, $"Error calling {method_name}: {{ex.Message}}", ex);
            }}
'''
    return impl

def process_api_file(file_path: Path, dry_run: bool = True) -> Tuple[int, List[str]]:
    """Process a single API file."""
    with open(file_path, 'r') as f:
        content = f.read()
    
    api_class = file_path.stem
    messages = []
    converted = 0
    
    # Find all WithHttpInfoAsync methods
    pattern = r'public async System\.Threading\.Tasks\.Task<Kinde\.Api\.Client\.ApiResponse<(\w+)>> (\w+)WithHttpInfoAsync\([^)]*\)\s*\{'
    
    for match in re.finditer(pattern, content):
        return_type = match.group(1)
        method_name = match.group(2)
        
        # Find method bounds
        sig_start = match.start()
        brace_start = match.end() - 1
        
        # Count braces to find end
        brace_count = 1
        pos = brace_start + 1
        while brace_count > 0 and pos < len(content):
            if content[pos] == '{':
                brace_count += 1
            elif content[pos] == '}':
                brace_count -= 1
            pos += 1
        
        method_body = content[brace_start:pos]
        
        # Skip if already converted
        if '// ===== Kiota Implementation =====' in method_body:
            messages.append(f"  ⊘ {method_name}: Already converted")
            continue
        
        # Extract endpoint
        endpoint = extract_endpoint_from_method(method_body)
        if not endpoint:
            messages.append(f"  ⊘ {method_name}: Could not extract endpoint")
            continue
        
        http_method = extract_http_method(method_body)
        has_body = 'localVarRequestOptions.Data' in method_body
        
        messages.append(f"  ✓ {method_name}: {http_method} {endpoint} -> {return_type}")
        converted += 1
    
    return converted, messages

def main():
    """Main entry point."""
    import argparse
    
    parser = argparse.ArgumentParser(description='Convert API methods to use Kiota')
    parser.add_argument('--apply', action='store_true', help='Actually apply changes (default: dry run)')
    parser.add_argument('--api', type=str, help='Only process specific API class')
    args = parser.parse_args()
    
    script_dir = Path(__file__).parent
    repo_root = script_dir.parent
    api_dir = repo_root / 'Kinde.Api' / 'Api'
    
    print("Kiota Method Conversion")
    print("=" * 60)
    print(f"Mode: {'APPLY' if args.apply else 'DRY RUN'}")
    print()
    
    total_converted = 0
    
    api_files = sorted(api_dir.glob('*Api.cs'))
    
    for api_file in api_files:
        if '.Kiota.' in api_file.name:
            continue
        
        if args.api and args.api not in api_file.name:
            continue
        
        print(f"\n{api_file.stem}:")
        print("-" * 40)
        
        converted, messages = process_api_file(api_file, dry_run=not args.apply)
        total_converted += converted
        
        for msg in messages:
            print(msg)
    
    print()
    print("=" * 60)
    print(f"Total methods: {total_converted}")
    
    if not args.apply:
        print()
        print("To apply changes, run with --apply flag")

if __name__ == '__main__':
    main()



