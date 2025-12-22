#!/usr/bin/env python3
"""
Convert OpenAPI-generated API classes to use Kiota internally.

This script modifies the existing API classes to:
1. Add Kiota client infrastructure
2. Replace the HTTP calls in *WithHttpInfoAsync methods with Kiota calls
3. Map models using AutoMapper

Since we're no longer regenerating OpenAPI code, these modifications are permanent.
"""

import os
import re
from pathlib import Path
from typing import List, Dict, Tuple, Optional
from dataclasses import dataclass

# Kiota infrastructure code to inject into each API class
KIOTA_INFRASTRUCTURE = '''
        // ===== Kiota Infrastructure =====
        private KindeManagementClient _kiotaClient;
        private HttpClient _kiotaHttpClient;
        private IMapper _kiotaMapper;
        private readonly object _kiotaLock = new object();

        /// <summary>
        /// Gets the AutoMapper instance for model translation.
        /// </summary>
        protected IMapper KiotaMapper => _kiotaMapper ??= KindeMapperConfiguration.Mapper;

        /// <summary>
        /// Gets or creates the Kiota Management API client.
        /// </summary>
        protected KindeManagementClient KiotaClient
        {
            get
            {
                if (_kiotaClient == null)
                {
                    lock (_kiotaLock)
                    {
                        if (_kiotaClient == null)
                        {
                            var tokenProvider = new KiotaTokenProvider(Configuration.AccessToken);
                            var authProvider = new BaseBearerTokenAuthenticationProvider(tokenProvider);
                            _kiotaHttpClient ??= new HttpClient();
                            var adapter = new HttpClientRequestAdapter(authProvider, httpClient: _kiotaHttpClient);
                            adapter.BaseUrl = Configuration.BasePath;
                            _kiotaClient = new KindeManagementClient(adapter);
                        }
                    }
                }
                return _kiotaClient;
            }
        }

        private class KiotaTokenProvider : IAccessTokenProvider
        {
            private readonly string _token;
            public KiotaTokenProvider(string token) => _token = token ?? string.Empty;
            public AllowedHostsValidator AllowedHostsValidator => new AllowedHostsValidator();
            public Task<string> GetAuthorizationTokenAsync(Uri uri, Dictionary<string, object> ctx = null, CancellationToken ct = default) 
                => Task.FromResult(_token);
        }
        // ===== End Kiota Infrastructure =====
'''

KIOTA_USINGS = '''using AutoMapper;
using Kinde.Api.Mappers;
using Kinde.Api.Kiota.Management;
using Microsoft.Kiota.Abstractions.Authentication;
using Microsoft.Kiota.Http.HttpClientLibrary;
'''

# Mapping from OpenAPI API class to Kiota endpoint path
KIOTA_ENDPOINT_MAP = {
    'APIsApi': 'Apis',
    'ApplicationsApi': 'Applications',
    'BillingAgreementsApi': 'Billing.Agreements',
    'BillingEntitlementsApi': 'Billing.Entitlements',
    'BillingMeterUsageApi': 'Billing.Meter_usage',
    'BusinessApi': 'Business',
    'CallbacksApi': 'Applications',  # Callbacks are under applications/{app_id}/...
    'ConnectedAppsApi': 'Connected_apps',
    'ConnectionsApi': 'Connections',
    'EnvironmentsApi': 'EnvironmentNamespace',
    'EnvironmentVariablesApi': 'Environment_variables',
    'FeatureFlagsApi': 'Feature_flags',
    'IdentitiesApi': 'Identities',
    'IndustriesApi': 'Industries',
    'MFAApi': 'Mfa',
    'OrganizationsApi': 'Organizations',
    'PermissionsApi': 'Permissions',
    'PropertiesApi': 'Properties',
    'PropertyCategoriesApi': 'Property_categories',
    'RolesApi': 'Roles',
    'SearchApi': 'Search',
    'SubscribersApi': 'Subscribers',
    'TimezonesApi': 'Timezones',
    'UsersApi': 'Users',
    'WebhooksApi': 'Webhooks',
}

def add_kiota_usings(content: str) -> str:
    """Add Kiota-related using statements if not present."""
    if 'using Kinde.Api.Kiota.Management;' in content:
        return content  # Already has Kiota usings
    
    # Find the last using statement
    using_match = re.search(r'(using [^;]+;\s*\n)(?=\s*namespace)', content)
    if using_match:
        insert_pos = using_match.end()
        content = content[:insert_pos] + KIOTA_USINGS + content[insert_pos:]
    
    return content

def add_kiota_infrastructure(content: str, class_name: str) -> str:
    """Add Kiota infrastructure to the class if not present."""
    if '_kiotaClient' in content:
        return content  # Already has Kiota infrastructure
    
    # Find the class declaration and the first field/property after it
    # Look for the pattern: public partial class ClassName : ... { ... first member
    class_pattern = rf'(public partial class {class_name}\s*:[^{{]+\{{)'
    class_match = re.search(class_pattern, content)
    
    if class_match:
        # Find the first member (field, property, or constructor)
        insert_pos = class_match.end()
        
        # Skip any existing comments or whitespace
        remaining = content[insert_pos:]
        whitespace_match = re.match(r'\s*', remaining)
        if whitespace_match:
            insert_pos += whitespace_match.end()
        
        content = content[:insert_pos] + KIOTA_INFRASTRUCTURE + content[insert_pos:]
    
    return content

def extract_method_info(content: str) -> List[Dict]:
    """Extract information about all WithHttpInfoAsync methods."""
    methods = []
    
    # Pattern to find WithHttpInfoAsync methods
    pattern = r'''
        public\s+async\s+
        System\.Threading\.Tasks\.Task<([^>]+)>\s+  # Return type
        (\w+WithHttpInfoAsync)\s*                    # Method name
        \(([^)]*)\)                                  # Parameters
        \s*\{                                        # Opening brace
        ([^}]+(?:\{[^}]*\}[^}]*)*?)                 # Method body (handling nested braces)
        (?=\n\s*public|\n\s*///|\n\s*\}\s*$)        # Look ahead for next method or end
    '''
    
    # Simplified approach: find method signatures
    sig_pattern = r'public\s+async\s+System\.Threading\.Tasks\.Task<([^>]+)>\s+(\w+WithHttpInfoAsync)\s*\(([^)]*)\)'
    
    for match in re.finditer(sig_pattern, content):
        return_type = match.group(1)
        method_name = match.group(2)
        params = match.group(3)
        
        methods.append({
            'return_type': return_type,
            'method_name': method_name,
            'params': params,
            'start_pos': match.start(),
            'end_pos': match.end()
        })
    
    return methods

def get_kiota_endpoint_for_method(api_class: str, method_name: str) -> str:
    """Determine the Kiota endpoint path for a given method."""
    base_endpoint = KIOTA_ENDPOINT_MAP.get(api_class, api_class.replace('Api', ''))
    
    # Remove WithHttpInfoAsync suffix to get base method name
    base_method = method_name.replace('WithHttpInfoAsync', '')
    
    # Map common method patterns to Kiota paths
    # This is a heuristic - may need refinement
    if base_method.startswith('Get') or base_method.startswith('List'):
        return f"Api.V1.{base_endpoint}"
    elif base_method.startswith('Create') or base_method.startswith('Add'):
        return f"Api.V1.{base_endpoint}"
    elif base_method.startswith('Update') or base_method.startswith('Patch'):
        return f"Api.V1.{base_endpoint}"
    elif base_method.startswith('Delete') or base_method.startswith('Remove'):
        return f"Api.V1.{base_endpoint}"
    
    return f"Api.V1.{base_endpoint}"

def process_api_file(file_path: Path) -> Tuple[bool, str]:
    """Process a single API file and add Kiota support."""
    with open(file_path, 'r') as f:
        content = f.read()
    
    class_name = file_path.stem  # e.g., "UsersApi"
    
    # Skip if already has Kiota integration (check for our marker)
    if '// ===== Kiota Infrastructure =====' in content:
        return False, "Already has Kiota infrastructure"
    
    original_content = content
    
    # Step 1: Add using statements
    content = add_kiota_usings(content)
    
    # Step 2: Add Kiota infrastructure to the class
    content = add_kiota_infrastructure(content, class_name)
    
    if content == original_content:
        return False, "No changes made"
    
    # Write the modified content
    with open(file_path, 'w') as f:
        f.write(content)
    
    return True, "Added Kiota infrastructure"

def main():
    """Main entry point."""
    script_dir = Path(__file__).parent
    repo_root = script_dir.parent
    api_dir = repo_root / 'Kinde.Api' / 'Api'
    
    if not api_dir.exists():
        print(f"API directory not found: {api_dir}")
        return
    
    # Get all API files (exclude .Kiota.cs files)
    api_files = [f for f in sorted(api_dir.glob('*Api.cs')) if '.Kiota.' not in f.name]
    
    print(f"Found {len(api_files)} API files to process")
    print()
    
    processed_count = 0
    
    for api_file in api_files:
        print(f"Processing {api_file.name}...", end=" ")
        
        success, message = process_api_file(api_file)
        
        if success:
            print(f"✓ {message}")
            processed_count += 1
        else:
            print(f"⊘ {message}")
    
    print()
    print(f"Processed {processed_count} of {len(api_files)} files")
    print()
    print("Next steps:")
    print("1. Review the modified files")
    print("2. Update the method implementations to use KiotaClient")
    print("3. Run 'dotnet build' to verify compilation")

if __name__ == '__main__':
    main()



