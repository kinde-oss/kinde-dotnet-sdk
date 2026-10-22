#!/usr/bin/env python3
"""
Generate Kiota facade implementations for all OpenAPI API classes.

This script generates facade classes that implement the existing OpenAPI interfaces
but delegate all calls to the Kiota-generated client internally.
"""

import os
import re
from pathlib import Path
from typing import List, Dict, Tuple, Optional
from dataclasses import dataclass

@dataclass
class MethodParam:
    name: str
    type: str
    is_optional: bool = False
    default: str = None

@dataclass
class ApiMethod:
    name: str
    return_type: str
    params: List[MethodParam]
    is_async: bool
    doc_comment: str = ""

@dataclass
class ApiClass:
    name: str
    sync_interface: str
    async_interface: str
    methods: List[ApiMethod]

def parse_api_file(file_path: str) -> Optional[ApiClass]:
    """Parse an API class file and extract method signatures."""
    with open(file_path, 'r') as f:
        content = f.read()
    
    # Extract class name
    class_match = re.search(r'public partial class (\w+Api)\s*:', content)
    if not class_match:
        return None
    
    class_name = class_match.group(1)
    
    # Extract interface names
    sync_interface = f"I{class_name}Sync"
    async_interface = f"I{class_name}Async"
    
    # Extract public methods from interface definitions
    methods = []
    
    # Find sync interface methods
    sync_interface_match = re.search(
        rf'public interface {sync_interface}[^{{]*\{{([^}}]+)\}}',
        content, re.DOTALL
    )
    
    if sync_interface_match:
        interface_content = sync_interface_match.group(1)
        method_pattern = r'///.*?(?=\n\s*(?:///|[A-Z]))'
        
        # Match method signatures - simplified pattern
        method_matches = re.findall(
            r'(\w+(?:<[^>]+>)?)\s+(\w+)\s*\(([^)]*)\)\s*;',
            interface_content
        )
        
        for return_type, method_name, params_str in method_matches:
            if method_name.endswith('WithHttpInfo'):
                continue  # Skip WithHttpInfo methods, we'll handle them differently
            
            params = parse_params(params_str)
            methods.append(ApiMethod(
                name=method_name,
                return_type=return_type,
                params=params,
                is_async=False
            ))
    
    # Find async interface methods
    async_interface_match = re.search(
        rf'public interface {async_interface}[^{{]*\{{([^}}]+)\}}',
        content, re.DOTALL
    )
    
    if async_interface_match:
        interface_content = async_interface_match.group(1)
        
        method_matches = re.findall(
            r'(Task(?:<[^>]+>)?)\s+(\w+Async)\s*\(([^)]*)\)\s*;',
            interface_content
        )
        
        for return_type, method_name, params_str in method_matches:
            if method_name.endswith('WithHttpInfoAsync'):
                continue
            
            # Remove CancellationToken from params for matching
            params = parse_params(params_str)
            params = [p for p in params if 'CancellationToken' not in p.type]
            
            methods.append(ApiMethod(
                name=method_name,
                return_type=return_type,
                params=params,
                is_async=True
            ))
    
    return ApiClass(
        name=class_name,
        sync_interface=sync_interface,
        async_interface=async_interface,
        methods=methods
    )

def parse_params(params_str: str) -> List[MethodParam]:
    """Parse parameter string into list of MethodParam."""
    params = []
    if not params_str.strip():
        return params
    
    # Split by comma, but respect generics
    current_param = ""
    bracket_depth = 0
    
    for char in params_str:
        if char in '<(':
            bracket_depth += 1
        elif char in '>)':
            bracket_depth -= 1
        elif char == ',' and bracket_depth == 0:
            if current_param.strip():
                params.append(parse_single_param(current_param.strip()))
            current_param = ""
            continue
        current_param += char
    
    if current_param.strip():
        params.append(parse_single_param(current_param.strip()))
    
    return params

def parse_single_param(param_str: str) -> MethodParam:
    """Parse a single parameter declaration."""
    # Handle default values
    default = None
    if '=' in param_str:
        param_str, default = param_str.rsplit('=', 1)
        default = default.strip()
        param_str = param_str.strip()
    
    # Split type and name
    parts = param_str.rsplit(None, 1)
    if len(parts) == 2:
        param_type, param_name = parts
    else:
        param_type = parts[0]
        param_name = "param"
    
    is_optional = default is not None or param_type.endswith('?')
    
    return MethodParam(
        name=param_name,
        type=param_type,
        is_optional=is_optional,
        default=default
    )

def get_kiota_endpoint_mapping(api_name: str) -> str:
    """Map OpenAPI API class to Kiota endpoint path."""
    mappings = {
        'APIsApi': 'Apis',
        'ApplicationsApi': 'Applications',
        'BillingAgreementsApi': 'Billing.Agreements',
        'BillingEntitlementsApi': 'Billing.Entitlements',
        'BillingMeterUsageApi': 'Billing.Meter_usage',
        'BusinessApi': 'Business',
        'CallbacksApi': 'Applications',  # Callbacks are under applications
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
    return mappings.get(api_name, api_name.replace('Api', ''))

def generate_facade_class(api_class: ApiClass) -> str:
    """Generate a facade class for the given API class."""
    kiota_endpoint = get_kiota_endpoint_mapping(api_class.name)
    
    facade_code = f'''/*
 * {api_class.name} - Kiota Facade Implementation
 * 
 * This facade class wraps the Kiota-generated client to provide
 * backward compatibility with the existing OpenAPI interface.
 * 
 * Auto-generated by generate-kiota-facades.py
 */

using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Kinde.Api.Client;
using Kinde.Api.Facades;
using Kinde.Api.Mappers;
using Kinde.Api.Model;
using Kinde.Api.Kiota.Management;
using Microsoft.Kiota.Abstractions.Authentication;
using Microsoft.Kiota.Http.HttpClientLibrary;
using KiotaModels = Kinde.Api.Kiota.Management.Models;

namespace Kinde.Api.Api
{{
    /// <summary>
    /// Kiota-backed implementation of {api_class.name}.
    /// This partial class adds Kiota client support while maintaining
    /// the existing OpenAPI interface for backward compatibility.
    /// </summary>
    public partial class {api_class.name}
    {{
        // Kiota infrastructure
        private KindeManagementClient _kiotaManagementClient;
        private HttpClient _kiotaHttpClient;
        private IMapper _autoMapper;
        private readonly object _kiotaLock = new object();

        /// <summary>
        /// Gets the AutoMapper instance for model translation between OpenAPI and Kiota models.
        /// </summary>
        protected IMapper AutoMapper => _autoMapper ??= KindeMapperConfiguration.Mapper;

        /// <summary>
        /// Gets or creates the Kiota Management API client.
        /// </summary>
        protected KindeManagementClient KiotaManagementClient
        {{
            get
            {{
                if (_kiotaManagementClient == null)
                {{
                    lock (_kiotaLock)
                    {{
                        if (_kiotaManagementClient == null)
                        {{
                            _kiotaManagementClient = CreateKiotaManagementClient();
                        }}
                    }}
                }}
                return _kiotaManagementClient;
            }}
        }}

        /// <summary>
        /// Creates a new Kiota Management client with the current configuration.
        /// </summary>
        private KindeManagementClient CreateKiotaManagementClient()
        {{
            var tokenProvider = new KiotaAccessTokenProvider(Configuration.AccessToken);
            var authProvider = new BaseBearerTokenAuthenticationProvider(tokenProvider);
            
            _kiotaHttpClient ??= new HttpClient();
            
            var adapter = new HttpClientRequestAdapter(authProvider, httpClient: _kiotaHttpClient);
            adapter.BaseUrl = Configuration.BasePath;
            
            return new KindeManagementClient(adapter);
        }}

        /// <summary>
        /// Creates an ApiResponse wrapper for the result.
        /// </summary>
        protected ApiResponse<T> CreateKiotaApiResponse<T>(T data, HttpStatusCode statusCode = HttpStatusCode.OK)
        {{
            return new ApiResponse<T>(statusCode, new Multimap<string, string>(), data);
        }}

        /// <summary>
        /// Access token provider for Kiota authentication.
        /// </summary>
        private class KiotaAccessTokenProvider : IAccessTokenProvider
        {{
            private readonly string _token;

            public KiotaAccessTokenProvider(string token)
            {{
                _token = token ?? string.Empty;
            }}

            public AllowedHostsValidator AllowedHostsValidator => new AllowedHostsValidator();

            public Task<string> GetAuthorizationTokenAsync(
                Uri uri,
                Dictionary<string, object> additionalAuthenticationContext = null,
                CancellationToken cancellationToken = default)
            {{
                return Task.FromResult(_token);
            }}
        }}

        #region Kiota API Method Implementations
        
        // TODO: Add Kiota-backed implementations for each API method.
        // Each method should:
        // 1. Map OpenAPI request models to Kiota models using AutoMapper
        // 2. Call the appropriate Kiota endpoint (KiotaManagementClient.Api.V1.{kiota_endpoint})
        // 3. Map the Kiota response back to OpenAPI models
        // 4. Return the OpenAPI response type
        
        // Example implementation pattern:
        //
        // public async Task<SomeResponse> SomeMethodViaKiotaAsync(SomeRequest request, CancellationToken cancellationToken = default)
        // {{
        //     // Map request
        //     var kiotaRequest = AutoMapper.Map<KiotaModels.Some_request>(request);
        //     
        //     // Call Kiota
        //     var kiotaResponse = await KiotaManagementClient.Api.V1.{kiota_endpoint}.PostAsync(kiotaRequest, cancellationToken: cancellationToken);
        //     
        //     // Map response
        //     return AutoMapper.Map<SomeResponse>(kiotaResponse);
        // }}

        #endregion
    }}
}}
'''
    
    return facade_code

def main():
    """Main entry point."""
    script_dir = Path(__file__).parent
    repo_root = script_dir.parent
    api_dir = repo_root / 'Kinde.Api' / 'Api'
    
    if not api_dir.exists():
        print(f"API directory not found: {api_dir}")
        return
    
    # Get all API files
    api_files = sorted(api_dir.glob('*Api.cs'))
    
    print(f"Found {len(api_files)} API files")
    
    generated_count = 0
    
    for api_file in api_files:
        # Skip already generated facade files
        if '.Kiota.cs' in api_file.name:
            continue
        
        print(f"Processing {api_file.name}...")
        
        api_class = parse_api_file(str(api_file))
        if not api_class:
            print(f"  Could not parse {api_file.name}")
            continue
        
        print(f"  Found {len(api_class.methods)} methods")
        
        # Generate facade
        facade_code = generate_facade_class(api_class)
        
        # Write facade file
        facade_file = api_file.parent / f"{api_class.name}.Kiota.cs"
        with open(facade_file, 'w') as f:
            f.write(facade_code)
        
        print(f"  Generated {facade_file.name}")
        generated_count += 1
    
    print(f"\nGenerated {generated_count} facade files")

if __name__ == '__main__':
    main()

