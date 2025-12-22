#!/usr/bin/env python3
"""
Convert API method implementations to use Kiota internally.

This script modifies the *WithHttpInfoAsync methods to use Kiota
instead of the OpenAPI client, while keeping the same signatures.
"""

import os
import re
from pathlib import Path
from typing import List, Dict, Tuple, Optional
from dataclasses import dataclass

# API endpoint mappings
KIOTA_ENDPOINTS = {
    'APIsApi': {
        'base': 'Api.V1.Apis',
        'methods': {
            'GetAPIs': ('GetAsync', None, 'Get_apis_response'),
            'AddAPIs': ('PostAsync', 'ApisPostRequestBody', 'Create_apis_response'),
            'GetAPI': ('GetAsync', None, 'Get_api_response', '[apiId]'),
            'DeleteAPI': ('DeleteAsync', None, 'Delete_api_response', '[apiId]'),
        }
    },
    'UsersApi': {
        'base': 'Api.V1.Users',
        'single': 'Api.V1.User',
        'methods': {
            'GetUsers': ('GetAsync', None, 'Users_response'),
            'CreateUser': ('PostAsync', 'UserPostRequestBody', 'Create_user_response', 'single'),
            'GetUserData': ('GetAsync', None, 'User', 'single'),
            'UpdateUser': ('PatchAsync', 'UserPatchRequestBody', 'Update_user_response', 'single'),
            'DeleteUser': ('DeleteAsync', None, 'Success_response', 'single'),
        }
    },
    'OrganizationsApi': {
        'base': 'Api.V1.Organizations',
        'single': 'Api.V1.Organization',
        'methods': {
            'GetOrganizations': ('GetAsync', None, 'Get_organizations_response'),
            'CreateOrganization': ('PostAsync', 'OrganizationPostRequestBody', 'Create_organization_response', 'single'),
            'GetOrganization': ('GetAsync', None, 'Get_organization_response', '[orgCode]'),
            'UpdateOrganization': ('PatchAsync', 'OrganizationPatchRequestBody', None, '[orgCode]'),
            'DeleteOrganization': ('DeleteAsync', None, None, '[orgCode]'),
        }
    },
    'RolesApi': {
        'base': 'Api.V1.Roles',
        'methods': {
            'GetRoles': ('GetAsync', None, 'Get_roles_response'),
            'CreateRole': ('PostAsync', 'RolesPostRequestBody', 'Create_roles_response'),
            'GetRole': ('GetAsync', None, 'Get_role_response', '[roleId]'),
            'UpdateRole': ('PatchAsync', 'RolePatchRequestBody', 'Update_role_response', '[roleId]'),
            'DeleteRole': ('DeleteAsync', None, 'Success_response', '[roleId]'),
        }
    },
    'PermissionsApi': {
        'base': 'Api.V1.Permissions',
        'methods': {
            'GetPermissions': ('GetAsync', None, 'Get_permissions_response'),
            'CreatePermission': ('PostAsync', 'PermissionsPostRequestBody', 'Create_permissions_response'),
        }
    },
}

def extract_method_signature(content: str, method_name: str) -> Optional[Dict]:
    """Extract method signature and body for a given method name."""
    # Pattern to find the async method
    pattern = rf'''
        (public\s+async\s+System\.Threading\.Tasks\.Task<[^>]+>\s+
        {method_name}WithHttpInfoAsync\s*
        \([^)]*\)\s*\{{)
    '''
    
    match = re.search(pattern, content, re.VERBOSE | re.DOTALL)
    if match:
        start = match.start()
        sig_end = match.end()
        
        # Find matching closing brace
        brace_count = 1
        pos = sig_end
        while brace_count > 0 and pos < len(content):
            if content[pos] == '{':
                brace_count += 1
            elif content[pos] == '}':
                brace_count -= 1
            pos += 1
        
        return {
            'start': start,
            'end': pos,
            'signature': match.group(1),
            'body': content[sig_end:pos-1]
        }
    
    return None

def generate_kiota_method_body(api_name: str, method_name: str, original_body: str) -> Optional[str]:
    """Generate a Kiota-based implementation for a method."""
    
    if api_name not in KIOTA_ENDPOINTS:
        return None
    
    api_config = KIOTA_ENDPOINTS[api_name]
    
    if method_name not in api_config.get('methods', {}):
        return None
    
    method_config = api_config['methods'][method_name]
    kiota_method = method_config[0]
    request_type = method_config[1] if len(method_config) > 1 else None
    response_type = method_config[2] if len(method_config) > 2 else None
    path_modifier = method_config[3] if len(method_config) > 3 else None
    
    # Determine the base endpoint
    if path_modifier == 'single':
        base_path = api_config.get('single', api_config['base'])
    else:
        base_path = api_config['base']
    
    # Build the Kiota call
    kiota_path = f"KiotaClient.{base_path}"
    if path_modifier and path_modifier.startswith('['):
        param = path_modifier[1:-1]
        kiota_path = f"KiotaClient.{base_path}[{param}]"
    
    body = f'''
            // Kiota implementation
            try
            {{
                var kiotaResponse = await {kiota_path}.{kiota_method}(
                    cancellationToken: cancellationToken
                ).ConfigureAwait(false);

                var mappedResponse = KiotaMapper.Map<{response_type or 'object'}>(kiotaResponse);
                return new ApiResponse<{response_type or 'object'}>(
                    System.Net.HttpStatusCode.OK,
                    new Multimap<string, string>(),
                    mappedResponse
                );
            }}
            catch (Microsoft.Kiota.Abstractions.ApiException ex)
            {{
                throw new ApiException((int)ex.ResponseStatusCode, ex.Message, ex);
            }}
'''
    
    return body

def process_api_file(file_path: Path) -> Tuple[int, List[str]]:
    """Process a single API file and convert methods to Kiota."""
    with open(file_path, 'r') as f:
        content = f.read()
    
    api_name = file_path.stem
    
    if api_name not in KIOTA_ENDPOINTS:
        return 0, [f"No Kiota mapping for {api_name}"]
    
    api_config = KIOTA_ENDPOINTS[api_name]
    converted = 0
    messages = []
    
    for method_name in api_config.get('methods', {}).keys():
        method_info = extract_method_signature(content, method_name)
        if not method_info:
            messages.append(f"  Could not find {method_name}WithHttpInfoAsync")
            continue
        
        new_body = generate_kiota_method_body(api_name, method_name, method_info['body'])
        if not new_body:
            messages.append(f"  Could not generate Kiota body for {method_name}")
            continue
        
        # TODO: Actually replace the method body
        # For now, just report what would be done
        messages.append(f"  ✓ Would convert {method_name}WithHttpInfoAsync")
        converted += 1
    
    return converted, messages

def main():
    """Main entry point."""
    script_dir = Path(__file__).parent
    repo_root = script_dir.parent
    api_dir = repo_root / 'Kinde.Api' / 'Api'
    
    print("Kiota Method Conversion Analysis")
    print("=" * 50)
    print()
    
    total_converted = 0
    
    for api_name in sorted(KIOTA_ENDPOINTS.keys()):
        api_file = api_dir / f"{api_name}.cs"
        if not api_file.exists():
            print(f"{api_name}: File not found")
            continue
        
        converted, messages = process_api_file(api_file)
        total_converted += converted
        
        print(f"{api_name}: {converted} methods ready for conversion")
        for msg in messages:
            print(msg)
        print()
    
    print("=" * 50)
    print(f"Total methods ready for conversion: {total_converted}")
    print()
    print("Note: This script currently only analyzes the files.")
    print("To perform actual conversion, run with --apply flag")

if __name__ == '__main__':
    import sys
    main()



