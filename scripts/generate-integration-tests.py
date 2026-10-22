#!/usr/bin/env python3
"""
Generate comprehensive integration tests for all API endpoints from OpenAPI specification.
"""

import json
import os
import sys
import re
from pathlib import Path
from typing import Dict, List, Any, Optional
import yaml

# Template for integration test file
TEST_FILE_TEMPLATE = """using System;
using System.Threading.Tasks;
{api_using_statements}
using Kinde.Api.Test.Integration;
using Xunit;
using Xunit.Abstractions;

namespace {namespace}
{{
    /// <summary>
    /// Auto-generated integration tests for {api_name} with both mock and real API support
    /// </summary>
    public class {test_class_name} : BaseIntegrationTest, IClassFixture<TestResourceFixture>
    {{
        private readonly ITestOutputHelper _output;
        private readonly TestResourceFixture _fixture;

        public {test_class_name}(ITestOutputHelper output, TestResourceFixture fixture) : base()
        {{
            _output = output;
            _fixture = fixture;
        }}

{test_methods}
    }}
}}
"""

# Template for a test method
TEST_METHOD_TEMPLATE = """
        [Fact]
        [Trait("TestMode", "{test_mode}")]
        public async Task {method_name}_{test_mode}_Test()
        {{
            // Arrange
            if ({skip_condition})
            {{
                _output.WriteLine("Skipping {test_mode} test - using {other_mode}");
                return;
            }}

            // Act & Assert
            try
            {{
{test_body}
            }}
            catch (Exception ex)
            {{
                _output.WriteLine($"Error in {method_name} test: {{ex.Message}}");
                throw;
            }}
        }}
"""


def to_pascal_case(snake_str: str) -> str:
    """Convert snake_case to PascalCase"""
    components = snake_str.split('_')
    return ''.join(x.title() for x in components)


def to_camel_case(snake_str: str) -> str:
    """Convert snake_case to camelCase"""
    components = snake_str.split('_')
    return components[0] + ''.join(x.title() for x in components[1:])


def sanitize_method_name(name: str) -> str:
    """Sanitize method name for C#"""
    # Remove special characters, keep alphanumeric and underscores
    name = re.sub(r'[^a-zA-Z0-9_]', '', name)
    # Ensure it starts with a letter
    if name and name[0].isdigit():
        name = 'Method' + name
    return name or 'TestMethod'


def get_csharp_type(schema: Dict[str, Any], is_response: bool = False) -> str:
    """Convert OpenAPI schema to C# type"""
    if not schema:
        return "object"
    
    schema_type = schema.get('type', 'object')
    
    if schema_type == 'string':
        format_type = schema.get('format', '')
        if format_type == 'date-time':
            return "DateTime"
        elif format_type == 'date':
            return "DateTime"
        return "string"
    elif schema_type == 'integer':
        return "int"
    elif schema_type == 'number':
        return "double"
    elif schema_type == 'boolean':
        return "bool"
    elif schema_type == 'array':
        items = schema.get('items', {})
        item_type = get_csharp_type(items, is_response)
        return f"List<{item_type}>"
    elif schema_type == 'object' or '$ref' in schema:
        # For object types, try to get the model name from $ref
        if '$ref' in schema:
            ref_path = schema['$ref']
            # Extract model name from #/components/schemas/ModelName
            model_name = ref_path.split('/')[-1]
            # Convert snake_case to PascalCase
            # e.g., "users_response" -> "UsersResponse"
            if '_' in model_name:
                parts = model_name.split('_')
                model_name = ''.join(word.capitalize() for word in parts)
            else:
                # Already PascalCase or camelCase, capitalize first letter
                model_name = model_name[0].upper() + model_name[1:] if model_name else "object"
            return model_name
        return "object"
    
    return "object"


def get_method_name_from_operation_id(operation_id: str, method: str) -> str:
    """Convert operationId to C# async method name"""
    # OpenAPI operationId is usually like "getUsers" or "createUser"
    # C# methods are usually like "GetUsersAsync" or "CreateUserAsync"
    if not operation_id:
        return f"{method.title()}Async"
    
    # Capitalize first letter
    method_name = operation_id[0].upper() + operation_id[1:]
    
    # Add Async suffix if not already present
    if not method_name.endswith("Async"):
        method_name += "Async"
    
    # Map names that may differ between spec and generated C# SDK (e.g. SDK typo)
    sdk_method_name_overrides = {
        "DeleteAPIApplicationScopeAsync": "DeleteAPIAppliationScopeAsync",  # SDK has typo "Appliation"
    }
    return sdk_method_name_overrides.get(method_name, method_name)


def extract_path_parameters(path: str) -> List[str]:
    """Extract path parameter names from OpenAPI path (e.g., /api/v1/users/{id} -> ['id'])"""
    import re
    # Match {param} or {param_name} patterns
    matches = re.findall(r'\{([^}]+)\}', path)
    return matches


def generate_test_body_for_endpoint(
    method: str,
    path: str,
    operation: Dict[str, Any],
    api_class_name: str,
    test_mode: str
) -> str:
    """Generate test body for an endpoint"""
    operation_id = operation.get('operationId', '')
    method_name = get_method_name_from_operation_id(operation_id, method)
    
    # Get parameters
    parameters = operation.get('parameters', [])
    request_body = operation.get('requestBody', {})
    
    # Detect if this is Accounts API (paths typically start with /account_api/)
    is_accounts_api = path.startswith('/account_api/') or '/account_api/' in path
    
    # Extract path parameters from the path itself
    path_params_from_path = extract_path_parameters(path)
    
    # Get response schema
    responses = operation.get('responses', {})
    success_response = responses.get('200') or responses.get('201') or responses.get('204')
    is_void_response = bool(responses.get('204')) or (success_response and not success_response.get('content'))
    
    # Check for void return types based on operation ID patterns
    void_operations = ['updateApplication', 'updateAPIScope', 'deleteApplication', 'deleteUser', 'deleteOrganization']
    if operation_id in void_operations:
        is_void_response = True
    
    lines = []
    
    # Generate parameter setup - include all path parameters (they're always required)
    param_vars = []
    param_order = []  # Track order: path params first, then query params
    
    # First, handle path parameters
    path_param_map = {}
    for param in parameters:
        if param.get('in') == 'path':
            param_name = param.get('name', '')
            path_param_map[param_name] = param
    
    # Ensure all path params from the path are included
    for path_param_name in path_params_from_path:
        if path_param_name not in path_param_map:
            # Path param exists in path but not in parameters list - add it anyway
            path_param_map[path_param_name] = {'name': path_param_name, 'schema': {'type': 'string'}}
    
    # Generate path parameters
    # For Real API tests, we still generate path parameters but add warnings
    is_modify_operation = method in ['PUT', 'PATCH', 'DELETE']
    needs_real_ids = test_mode == "Real" and is_modify_operation and path_params_from_path
    
    if needs_real_ids:
        # These operations need real resource IDs - add warning but still generate test
        resource_type = path_params_from_path[0] if path_params_from_path else "resource"
        lines.append(f"                // WARNING: Real API test - This operation requires existing {resource_type}")
        lines.append(f"                // This test may fail if the resource doesn't exist in your Kinde instance")
        lines.append(f"                // Consider creating the resource first or using a test environment")
    
    # Operations that need setup before they can be tested
    # Format: operation_id (lowercase) -> setup configuration
    operations_needing_setup = {
        'removerolepermission': {
            'description': 'First add permission to role before removing',
            'setup_code': '''                // Setup: Add permission to role before testing removal
                var setupApi = CreateApi((client, config) => new RolesApi(client, config));
                try {{
                    var permissionInner = new UpdateRolePermissionsRequestPermissionsInner(id: _fixture.PermissionId);
                    var addRequest = new UpdateRolePermissionsRequest(permissions: new System.Collections.Generic.List<UpdateRolePermissionsRequestPermissionsInner>() {{ permissionInner }});
                    await setupApi.UpdateRolePermissionsAsync(_fixture.RoleId, addRequest);
                }} catch (Exception) {{ /* Permission may already be added */ }}'''
        },
        'deleterolescope': {
            'description': 'First add scope to role before deleting',
            'setup_code': '''                // Setup: Add scope to role before testing deletion
                // Note: This test requires a valid scope_id - skipping if not available
                _output.WriteLine("Skipping DeleteRoleScope test - requires valid scope to be added first");
                return;'''
        },
        'deleteorganizationuserrole': {
            'description': 'First add role to user before removing',
            'setup_code': '''                // Setup: Add role to user in organization before testing removal
                var setupApi = CreateApi((client, config) => new OrganizationsApi(client, config));
                try {{
                    var addRequest = new CreateOrganizationUserRoleRequest(roleId: _fixture.RoleId);
                    await setupApi.CreateOrganizationUserRoleAsync(_fixture.OrganizationCode, _fixture.UserId, addRequest);
                }} catch (Exception) {{ /* Role may already be assigned */ }}'''
        },
        'deleteorganizationuserpermission': {
            'description': 'First add permission to user before removing',
            'setup_code': '''                // Setup: Add permission to user in organization before testing removal
                var setupApi = CreateApi((client, config) => new OrganizationsApi(client, config));
                try {{
                    var addRequest = new CreateOrganizationUserPermissionRequest(permissionId: _fixture.PermissionId);
                    await setupApi.CreateOrganizationUserPermissionAsync(_fixture.OrganizationCode, _fixture.UserId, addRequest);
                }} catch (Exception) {{ /* Permission may already be assigned */ }}'''
        },
        'addrolescope': {
            'description': 'Skip AddRoleScope test - requires valid scope_id',
            'setup_code': '''                // Note: This test requires a valid scope_id - skipping if not available
                _output.WriteLine("Skipping AddRoleScope test - requires valid scope_id");
                return;'''
        },
    }
    
    # Add setup code for operations that need it (Real API mode only, case-insensitive match)
    if test_mode == "Real" and operation_id.lower() in operations_needing_setup:
        setup_info = operations_needing_setup[operation_id.lower()]
        lines.append(setup_info['setup_code'])
        lines.append("")
    
    # Map common parameter names to fixture properties
    # This includes both path and query parameters
    # Note: Be careful with generic names like 'id' - they may conflict with other parameters
    param_to_fixture_map = {
        'org_code': 'OrganizationCode',
        'organization_code': 'OrganizationCode',
        'code': 'OrganizationCode',  # Used by getOrganization as query param
        'user_id': 'UserId',
        'userId': 'UserId',
        # Note: Don't map 'id' generically - it's used for different resource types
        'permission_id': 'PermissionId',
        'permissionId': 'PermissionId',
        'property_id': 'PropertyId',
        'propertyId': 'PropertyId',
        'property_key': 'PropertyKey',
        'propertyKey': 'PropertyKey',
        'role_id': 'RoleId',
        'roleId': 'RoleId',
        'application_id': 'ApplicationId',
        'applicationId': 'ApplicationId',
        'app_id': 'ApplicationId',
    }
    
    # Always generate path parameters (even if test may fail)
    for path_param_name in path_params_from_path:
        param = path_param_map.get(path_param_name, {})
        param_schema = param.get('schema', {'type': 'string'})
        param_type = get_csharp_type(param_schema)
        
        # Generate appropriate test value based on type
        if param_type == 'string' or param_type == 'string?':
            # For Real API operations, use fixture resources if available
            if test_mode == "Real":
                # Check if we can map this parameter to a fixture property
                fixture_property = param_to_fixture_map.get(path_param_name)
                if fixture_property:
                    test_value = f'_fixture.{fixture_property}'
                    lines.append(f"                // Using test resource from fixture: {fixture_property}")
                else:
                    # For unmapped parameters, use placeholder with warning
                    if method == "GET":
                        lines.append(f"                // Note: This test uses a placeholder {path_param_name} and may fail if the resource doesn't exist")
                    elif method in ['PUT', 'PATCH', 'DELETE']:
                        lines.append(f"                // WARNING: Using placeholder {path_param_name} - test will likely fail without real resource ID")
                    test_value = f'"test-{path_param_name}"'
            else:
                # Mock mode - use placeholder
                test_value = f'"test-{path_param_name}"'
        elif param_type == 'int' or param_type == 'int?':
            test_value = '1'
        elif param_type == 'bool' or param_type == 'bool?':
            test_value = 'true'
        else:
            test_value = f'"test-{path_param_name}"'
        
        lines.append(f"                var {path_param_name} = {test_value};")
        param_vars.append(path_param_name)
        param_order.append(path_param_name)
    
    # Then handle query parameters - sort by required first, then optional
    # Note: Required parameters must come before optional ones in C# method signatures
    query_params_required = []
    query_params_optional = []
    
    for param in parameters:
        param_name = param.get('name', '')
        param_schema = param.get('schema', {})
        param_type = get_csharp_type(param_schema)
        param_in = param.get('in', 'query')
        is_required = param.get('required', False)
        
        # Skip if already handled as path param
        if param_name in path_params_from_path:
            continue
        
        if param_in == 'query':
            # Check if this parameter maps to a fixture property (for Real API mode)
            has_fixture_mapping = param_name in param_to_fixture_map
            
            # For GET methods, be conservative - only include query params if they're required
            # OR if we have a fixture mapping in Real API mode (e.g., 'code' for organizations)
            # (OpenAPI Generator may not include all optional query params in method signatures)
            # For PUT/PATCH methods, include query params (they come before request body)
            # For other methods, only include required ones (optional ones will use defaults)
            if method == 'GET':
                # For GET, include required query params OR those with fixture mappings in Real API mode
                include_param = is_required or (test_mode == "Real" and has_fixture_mapping)
            elif method in ['PUT', 'PATCH']:
                # For PUT/PATCH, include query params (they come before request body)
                include_param = True
            elif is_required:
                # For other methods, only include required query params
                include_param = True
            else:
                include_param = False
            
            if include_param:
                # Generate appropriate test value
                # For Real API mode, use fixture properties where available
                fixture_property = param_to_fixture_map.get(param_name) if test_mode == "Real" else None
                fixture_comment = None
                
                if fixture_property and (param_type == 'string' or param_type == 'string?'):
                    # Use fixture resource for Real API tests
                    test_value = f'_fixture.{fixture_property}'
                    fixture_comment = f"                // Using test resource from fixture: {fixture_property}"
                elif param_type == 'string' or param_type == 'string?':
                    if is_accounts_api:
                        test_value = f'new Option<string?>("{param_name}")'
                    else:
                        test_value = f'"{param_name}"'
                elif param_type == 'int' or param_type == 'int?':
                    if is_accounts_api:
                        test_value = f'new Option<int?>(1)'
                    else:
                        test_value = '1'
                elif param_type == 'bool' or param_type == 'bool?':
                    if is_accounts_api:
                        test_value = f'new Option<bool?>(true)'
                    else:
                        test_value = 'true'
                else:
                    if is_accounts_api:
                        test_value = f'new Option<object>("{param_name}")'
                    else:
                        test_value = f'"{param_name}"'
                
                # Use named parameters for optional params with fixture mappings
                use_named = not is_required and fixture_property is not None
                
                param_info = {
                    'name': param_name,
                    'value': test_value,
                    'required': is_required,
                    'fixture_comment': fixture_comment,
                    'use_named': use_named
                }
                
                if is_required:
                    query_params_required.append(param_info)
                else:
                    query_params_optional.append(param_info)
    
    # Add required query params first, then optional
    # Track which params need named parameter syntax
    named_params = []
    for param_info in query_params_required + query_params_optional:
        if param_info.get('fixture_comment'):
            lines.append(param_info['fixture_comment'])
        lines.append(f"                var {param_info['name']} = {param_info['value']};")
        
        # For named parameters, store the named format
        if param_info.get('use_named'):
            # Convert snake_case to camelCase for C# named parameters
            csharp_param_name = param_info['name'].replace('_', ' ').title().replace(' ', '')
            csharp_param_name = csharp_param_name[0].lower() + csharp_param_name[1:] if csharp_param_name else param_info['name']
            named_params.append(f"{csharp_param_name}: {param_info['name']}")
        else:
            param_vars.append(param_info['name'])
        param_order.append(param_info['name'])
    
    # Generate request body if needed
    request_var = None
    file_params = []  # Track file parameters for multipart/form-data
    
    if request_body and method in ['POST', 'PUT', 'PATCH']:
        content = request_body.get('content', {})
        
        # Handle multipart/form-data (file uploads)
        if 'multipart/form-data' in content:
            schema = content['multipart/form-data'].get('schema', {})
            properties = schema.get('properties', {})
            required_props = schema.get('required', [])
            
            for prop_name, prop_schema in properties.items():
                prop_type = prop_schema.get('type', '')
                prop_format = prop_schema.get('format', '')
                
                # Check if this is a file parameter
                if prop_type == 'string' and prop_format == 'binary':
                    # This is a file parameter
                    file_var_name = prop_name
                    lines.append(f"                var {file_var_name} = new FileParameter(\"test-file.txt\", new System.IO.MemoryStream(System.Text.Encoding.UTF8.GetBytes(\"test file content\")));")
                    file_params.append(file_var_name)
                elif prop_name in required_props:
                    # Required non-file parameter
                    param_type = get_csharp_type(prop_schema)
                    if param_type == 'string' or param_type == 'string?':
                        test_value = f'"{prop_name}"'
                    elif param_type == 'int' or param_type == 'int?':
                        test_value = '1'
                    elif param_type == 'bool' or param_type == 'bool?':
                        test_value = 'true'
                    else:
                        test_value = f'"{prop_name}"'
                    lines.append(f"                var {prop_name} = {test_value};")
                    param_vars.append(prop_name)
                    param_order.append(prop_name)
        
        # Handle application/json request body
        if 'application/json' in content:
            schema = content['application/json'].get('schema', {})
            
            # Try to get the request type - check mapping first, then $ref
            request_type = None
            operation_id = operation.get('operationId', '')
            
            # Map known operation IDs to actual model names
            operation_to_model_map = {
                'addRedirectCallbackURLs': 'ReplaceRedirectCallbackURLsRequest',
                'addLogoutRedirectURLs': 'ReplaceLogoutRedirectURLsRequest',
                'updatePermissions': 'CreatePermissionRequest',  # UpdatePermissions uses CreatePermissionRequest
                'UpdatePermissions': 'CreatePermissionRequest',  # UpdatePermissions (capitalized)
                'updateUserProperties': 'UpdateOrganizationPropertiesRequest',  # UpdateUserProperties uses UpdateOrganizationPropertiesRequest
                'UpdateUserProperties': 'UpdateOrganizationPropertiesRequest',  # UpdateUserProperties (capitalized)
            }
            
            # Map known $ref schema names to actual model names
            schema_ref_map = {
                'UpdatePermissionsRequest': 'CreatePermissionRequest',  # UpdatePermissions uses CreatePermissionRequest
                'UpdateUserPropertiesRequest': 'UpdateOrganizationPropertiesRequest',  # UpdateUserProperties uses UpdateOrganizationPropertiesRequest
            }
            
            # Check mapping (case-insensitive)
            if operation_id in operation_to_model_map:
                request_type = operation_to_model_map[operation_id]
            elif operation_id.lower() in [k.lower() for k in operation_to_model_map.keys()]:
                # Find the matching key (case-insensitive)
                for key, value in operation_to_model_map.items():
                    if key.lower() == operation_id.lower():
                        request_type = value
                        break
            elif '$ref' in schema:
                ref_path = schema['$ref']
                ref_name = ref_path.split('/')[-1]
                # Convert snake_case to PascalCase if needed
                if '_' in ref_name:
                    parts = ref_name.split('_')
                    ref_name = ''.join(word.capitalize() for word in parts)
                else:
                    ref_name = ref_name[0].upper() + ref_name[1:] if ref_name else None
                
                # Check if this schema ref needs to be mapped
                if ref_name in schema_ref_map:
                    request_type = schema_ref_map[ref_name]
                else:
                    request_type = ref_name
            else:
                # For inline schemas, try to derive model name from operation ID
                # OpenAPI Generator typically creates models like CreateXxxRequest from operation IDs
                if operation_id:
                    # Convert camelCase operationId to PascalCase model name
                    # e.g., "createFeatureFlag" -> "CreateFeatureFlagRequest"
                    model_name = operation_id[0].upper() + operation_id[1:]
                    # Check if it ends with a verb that should become "Request"
                    if not model_name.endswith('Request'):
                        model_name += 'Request'
                    request_type = model_name
                else:
                    request_type = get_csharp_type(schema)
            
            # Create request object if we have a valid type
            if request_type and request_type != 'object':
                # Get required properties from schema
                required_props = schema.get('required', [])
                properties = schema.get('properties', {})
                
                # Build constructor arguments for required properties using named parameters
                named_args = []
                
                for prop_name in required_props:
                    prop_schema = properties.get(prop_name, {})
                    prop_type = get_csharp_type(prop_schema)
                    
                    # Convert property name from snake_case to camelCase for C# constructor parameter
                    csharp_prop_name = to_camel_case(prop_name)
                    # For enum type names, use PascalCase
                    pascal_prop_name = to_pascal_case(prop_name)
                    
                    # Check if this is a oneOf/anyOf type
                    one_of_type = None
                    if '$ref' in prop_schema:
                        ref_path = prop_schema['$ref']
                        ref_name = ref_path.split('/')[-1]
                        if '_' in ref_name:
                            ref_name = to_pascal_case(ref_name)
                        else:
                            ref_name = ref_name[0].upper() + ref_name[1:] if ref_name else None
                        if ref_name:
                            one_of_type = ref_name
                    elif prop_schema.get('oneOf') or prop_schema.get('anyOf'):
                        if prop_name.lower().endswith('value'):
                            one_of_type = f'{request_type}{pascal_prop_name}'
                        else:
                            one_of_type = f'{request_type}{pascal_prop_name}Value'
                    
                    # Check if this is an enum
                    enum_values = prop_schema.get('enum', [])
                    if enum_values:
                        enum_type = f'{request_type}.{pascal_prop_name}Enum'
                        first_enum_value = enum_values[0] if enum_values else 'Unknown'
                        if '_' in first_enum_value:
                            enum_member = to_pascal_case(first_enum_value)
                        else:
                            enum_member = first_enum_value[0].upper() + first_enum_value[1:].lower() if first_enum_value else 'Unknown'
                        test_value = f'{enum_type}.{enum_member}'
                    elif one_of_type:
                        test_value = f'new {one_of_type}("test-{prop_name}")'
                    elif prop_type.endswith('Value'):
                        test_value = f'new {prop_type}("test-{prop_name}")'
                    elif prop_type == 'string' or prop_type == 'string?':
                        test_value = f'"test-{prop_name}"'
                    elif prop_type == 'int' or prop_type == 'int?':
                        test_value = '1'
                    elif prop_type == 'bool' or prop_type == 'bool?':
                        test_value = 'true'
                    elif prop_type.startswith('Dictionary'):
                        test_value = f'new System.Collections.Generic.Dictionary<string, object>()'
                    elif prop_type.startswith('List'):
                        items_schema = prop_schema.get('items', {})
                        items_enum = items_schema.get('enum', [])
                        if items_enum:
                            enum_type = f'{request_type}.{pascal_prop_name}Enum'
                            test_value = f'new System.Collections.Generic.List<{enum_type}>()'
                        elif '$ref' in items_schema:
                            ref_path = items_schema['$ref']
                            item_type = ref_path.split('/')[-1]
                            if '_' in item_type:
                                item_type = to_pascal_case(item_type)
                            else:
                                item_type = item_type[0].upper() + item_type[1:] if item_type else "object"
                            test_value = f'new System.Collections.Generic.List<{item_type}>()'
                        elif items_schema.get('type') == 'object':
                            item_type = f'{request_type}{pascal_prop_name}Inner'
                            test_value = f'new System.Collections.Generic.List<{item_type}>()'
                        else:
                            test_value = f'new System.Collections.Generic.{prop_type}()'
                    else:
                        test_value = f'"test-{prop_name}"'
                    
                    named_args.append(f'{csharp_prop_name}: {test_value}')
                
                # Create request with required properties using named parameters
                if named_args:
                    args_str = ', '.join(named_args)
                    lines.append(f"                var request = new {request_type}({args_str});")
                else:
                    lines.append(f"                var request = new {request_type}();")
                request_var = "request"
            elif '$ref' in schema:
                # Fallback for $ref - extract model name
                ref_path = schema['$ref']
                model_name = ref_path.split('/')[-1]
                if '_' in model_name:
                    parts = model_name.split('_')
                    model_name = ''.join(word.capitalize() for word in parts)
                else:
                    model_name = model_name[0].upper() + model_name[1:] if model_name else "object"
                lines.append(f"                var request = new {model_name}();")
                lines.append(f"                // TODO: Set required request properties")
                request_var = "request"
            else:
                # Last resort - try to derive from operation ID
                operation_id = operation.get('operationId', '')
                if operation_id:
                    model_name = operation_id[0].upper() + operation_id[1:]
                    if not model_name.endswith('Request'):
                        model_name += 'Request'
                    lines.append(f"                var request = new {model_name}();")
                    lines.append(f"                // TODO: Set required request properties")
                    request_var = "request"
                else:
                    lines.append(f"                // TODO: Create appropriate request object for {method_name}")
                    request_var = None
    
    # Generate API call - method_name already includes "Async" suffix from get_method_name_from_operation_id
    # Parameter order rules:
    # - For PUT/PATCH: path parameters come FIRST, then request body
    # - For POST: request body comes FIRST (unless there are path params, then path params first)
    # - For GET/DELETE: only path/query params
    
    if method in ['PUT', 'PATCH']:
        # PUT/PATCH: path params FIRST, then query params, then request body, then file params
        # Separate path params from query params
        path_params_only = [p for p in param_vars if p in path_params_from_path]
        query_params_only = [p for p in param_vars if p not in path_params_from_path]
        
        all_params = path_params_only + query_params_only
        if request_var:
            all_params.append(request_var)
        all_params.extend(file_params)
        
        if all_params:
            api_call = f"await api.{method_name}({', '.join(all_params)});"
        else:
            api_call = f"await api.{method_name}();"
    elif method == 'POST':
        # POST: if path params exist, they come first; then request body, then file params
        path_params_only = [p for p in param_vars if p in path_params_from_path]
        query_params_only = [p for p in param_vars if p not in path_params_from_path]
        
        all_params = path_params_only + query_params_only
        if request_var:
            all_params.append(request_var)
        all_params.extend(file_params)
        
        if all_params:
            api_call = f"await api.{method_name}({', '.join(all_params)});"
        else:
            api_call = f"await api.{method_name}();"
    elif method == 'GET':
        # GET methods: path/query params, with optional named params for fixture-mapped optionals
        all_call_params = param_vars + named_params
        if all_call_params:
            api_call = f"await api.{method_name}({', '.join(all_call_params)});"
        else:
            api_call = f"await api.{method_name}();"
    elif method == 'DELETE':
        # DELETE methods: usually just path params
        if param_vars:
            api_call = f"await api.{method_name}({', '.join(param_vars)});"
        else:
            api_call = f"await api.{method_name}();"
    else:
        # Other methods
        if request_var:
            if param_vars:
                api_call = f"await api.{method_name}({', '.join(param_vars)}, {request_var});"
            else:
                api_call = f"await api.{method_name}({request_var});"
        elif param_vars:
            api_call = f"await api.{method_name}({', '.join(param_vars)});"
        else:
            api_call = f"await api.{method_name}();"
    
    # Add API creation after parameter setup but before the call
    if lines:  # Only add if there are already lines (parameter setup)
        lines.append("")
    lines.append(f"                var api = CreateApi((client, config) => new {api_class_name}(client, config));")
    lines.append("")
    
    # Handle void return types
    if is_void_response:
        lines.append(f"                {api_call}")
        lines.append("                // Void method - no response to check")
        lines.append("                _output.WriteLine($\"Void method completed successfully\");")
    else:
        lines.append(f"                var response = {api_call}")
        lines.append("")
        lines.append("                // Assert")
        lines.append("                Assert.NotNull(response);")
        
        if success_response:
            response_schema = success_response.get('content', {}).get('application/json', {}).get('schema', {})
            if response_schema:
                response_type = get_csharp_type(response_schema, is_response=True)
                if response_type.startswith('List'):
                    lines.append("                Assert.NotNull(response);")
                    lines.append("                _output.WriteLine($\"Retrieved {{response?.Count ?? 0}} items\");")
                elif response_type != 'object':
                    lines.append(f"                _output.WriteLine($\"Response received: {{response?.GetType().Name}}\");")
    
    lines.append("                _output.WriteLine($\"Test completed successfully\");")
    
    return '\n'.join(lines)


def generate_mock_test_body(
    method: str,
    path: str,
    operation: Dict[str, Any],
    api_class_name: str
) -> str:
    """Generate mock test body"""
    operation_id = operation.get('operationId', '')
    method_name = get_method_name_from_operation_id(operation_id, method)
    
    # Detect if this is Accounts API (paths typically start with /account_api/)
    is_accounts_api = path.startswith('/account_api/') or '/account_api/' in path
    
    # Get response schema for mock
    responses = operation.get('responses', {})
    success_response = responses.get('200') or responses.get('201') or responses.get('204')
    is_void_response = bool(responses.get('204')) or (success_response and not success_response.get('content'))
    
    # Check for void return types based on operation ID patterns
    void_operations = ['updateApplication', 'updateAPIScope', 'deleteApplication', 'deleteUser', 'deleteOrganization']
    if operation_id in void_operations:
        is_void_response = True
    
    # Extract path parameters from path
    path_params_from_path = extract_path_parameters(path)
    
    # Get parameters from operation
    parameters = operation.get('parameters', [])
    path_params = [p for p in parameters if p.get('in') == 'path']
    
    # Combine path params from path and parameters
    all_path_params = {}
    for p in path_params:
        all_path_params[p.get('name')] = p
    for path_param_name in path_params_from_path:
        if path_param_name not in all_path_params:
            all_path_params[path_param_name] = {'name': path_param_name, 'schema': {'type': 'string'}}
    
    lines = []
    lines.append("                var mockHandler = GetMockHandler();")
    lines.append("                if (mockHandler == null)")
    lines.append("                {")
    lines.append("                    _output.WriteLine(\"Mock handler not available\");")
    lines.append("                    return;")
    lines.append("                }")
    lines.append("")
    
    # Generate path parameter variables
    param_vars = []
    for path_param_name in path_params_from_path:
        param = all_path_params.get(path_param_name, {})
        param_schema = param.get('schema', {'type': 'string'})
        param_type = get_csharp_type(param_schema)
        
        if param_type == 'string' or param_type == 'string?':
            test_value = f'"test-{path_param_name}"'
        elif param_type == 'int' or param_type == 'int?':
            test_value = '1'
        elif param_type == 'bool' or param_type == 'bool?':
            test_value = 'true'
        else:
            test_value = f'"test-{path_param_name}"'
        
        lines.append(f"                var {path_param_name} = {test_value};")
        param_vars.append(path_param_name)
    
    # Also include required query parameters for mock tests
    # Sort by required first, then optional
    query_params_required = []
    query_params_optional = []
    
    for param in parameters:
        param_name = param.get('name', '')
        param_schema = param.get('schema', {})
        param_type = get_csharp_type(param_schema)
        param_in = param.get('in', 'query')
        is_required = param.get('required', False)
        
        # Skip if already handled as path param
        if param_name in path_params_from_path:
            continue
        
        if param_in == 'query':
            # For GET methods, be conservative - only include query params if they're required
            # (OpenAPI Generator may not include all optional query params in method signatures)
            # For PUT/PATCH methods, include query params (they come before request body)
            # For other methods, only include required ones (optional ones will use defaults)
            if method == 'GET':
                # For GET, only include required query params to avoid signature mismatches
                include_param = is_required
            elif method in ['PUT', 'PATCH']:
                # For PUT/PATCH, include query params (they come before request body)
                include_param = True
            elif is_required:
                # For other methods, only include required query params
                include_param = True
            else:
                include_param = False
            
            if include_param:
                # Accounts API uses Option<T> types, Management API uses direct types
                if param_type == 'string' or param_type == 'string?':
                    if is_accounts_api:
                        test_value = f'new Option<string?>("{param_name}")'
                    else:
                        test_value = f'"{param_name}"'
                elif param_type == 'int' or param_type == 'int?':
                    if is_accounts_api:
                        test_value = f'new Option<int?>(1)'
                    else:
                        test_value = '1'
                elif param_type == 'bool' or param_type == 'bool?':
                    if is_accounts_api:
                        test_value = f'new Option<bool?>(true)'
                    else:
                        test_value = 'true'
                else:
                    if is_accounts_api:
                        test_value = f'new Option<object>("{param_name}")'
                    else:
                        test_value = f'"{param_name}"'
                
                param_info = {
                    'name': param_name,
                    'value': test_value,
                    'required': is_required
                }
                
                if is_required:
                    query_params_required.append(param_info)
                else:
                    query_params_optional.append(param_info)
    
    # Add required query params first, then optional
    for param_info in query_params_required + query_params_optional:
        lines.append(f"                var {param_info['name']} = {param_info['value']};")
        param_vars.append(param_info['name'])
    
    # Build the actual path by replacing placeholders with parameter values
    # Use C# string interpolation: $"/api/v1/organizations/{org_code}/users"
    if path_params_from_path:
        # Build path using string interpolation
        # Escape any existing $ or { in the path, then replace placeholders
        actual_path = path
        for path_param_name in path_params_from_path:
            placeholder = f"{{{path_param_name}}}"
            # Replace {param_name} with {param_name} for string interpolation
            actual_path = actual_path.replace(placeholder, f"{{{path_param_name}}}")
        # Use C# string interpolation syntax
        actual_path_expr = f'$"{actual_path}"'
    else:
        actual_path_expr = f'"{path}"'
    
    # Create mock response
    if success_response:
        response_schema = success_response.get('content', {}).get('application/json', {}).get('schema', {})
        response_type = get_csharp_type(response_schema, is_response=True)
        
        if response_type != 'object' and not response_type.startswith('List'):
            lines.append(f"                var mockResponse = new {response_type}();")
            lines.append(f"                mockHandler.AddResponse(\"{method}\", {actual_path_expr}, mockResponse);")
        elif response_type.startswith('List'):
            # For list responses, create an empty list
            lines.append(f"                var mockResponse = new System.Collections.Generic.List<object>();")
            lines.append(f"                mockHandler.AddResponse(\"{method}\", {actual_path_expr}, mockResponse);")
        else:
            lines.append(f"                var mockResponse = new {{ }};")
            lines.append(f"                mockHandler.AddResponse(\"{method}\", {actual_path_expr}, mockResponse);")
    else:
        lines.append(f"                mockHandler.AddResponse(\"{method}\", {actual_path_expr}, new {{ }});")
    
    # Generate request body for mock test if needed
    request_var = None
    file_params = []  # Track file parameters for multipart/form-data
    request_body = operation.get('requestBody', {})
    if request_body and method in ['POST', 'PUT', 'PATCH']:
        content = request_body.get('content', {})
        
        # Handle multipart/form-data (file uploads)
        if 'multipart/form-data' in content:
            schema = content['multipart/form-data'].get('schema', {})
            properties = schema.get('properties', {})
            required_props = schema.get('required', [])
            
            for prop_name, prop_schema in properties.items():
                prop_type = prop_schema.get('type', '')
                prop_format = prop_schema.get('format', '')
                
                # Check if this is a file parameter
                if prop_type == 'string' and prop_format == 'binary':
                    # This is a file parameter
                    file_var_name = prop_name
                    lines.append(f"                var {file_var_name} = new FileParameter(\"test-file.txt\", new System.IO.MemoryStream(System.Text.Encoding.UTF8.GetBytes(\"test file content\")));")
                    file_params.append(file_var_name)
                elif prop_name in required_props:
                    # Required non-file parameter
                    param_type = get_csharp_type(prop_schema)
                    if param_type == 'string' or param_type == 'string?':
                        test_value = f'"{prop_name}"'
                    elif param_type == 'int' or param_type == 'int?':
                        test_value = '1'
                    elif param_type == 'bool' or param_type == 'bool?':
                        test_value = 'true'
                    else:
                        test_value = f'"{prop_name}"'
                    lines.append(f"                var {prop_name} = {test_value};")
                    param_vars.append(prop_name)
        
        # Handle application/json request body
        if 'application/json' in content:
            schema = content['application/json'].get('schema', {})
            
            # Check mapping first, then $ref - same logic as real test
            request_type = None
            operation_id = operation.get('operationId', '')
            
            # Map known operation IDs to actual model names
            operation_to_model_map = {
                'addRedirectCallbackURLs': 'ReplaceRedirectCallbackURLsRequest',
                'addLogoutRedirectURLs': 'ReplaceLogoutRedirectURLsRequest',
                'updatePermissions': 'CreatePermissionRequest',  # UpdatePermissions uses CreatePermissionRequest
                'UpdatePermissions': 'CreatePermissionRequest',  # UpdatePermissions (capitalized)
                'updateUserProperties': 'UpdateOrganizationPropertiesRequest',  # UpdateUserProperties uses UpdateOrganizationPropertiesRequest
                'UpdateUserProperties': 'UpdateOrganizationPropertiesRequest',  # UpdateUserProperties (capitalized)
            }
            
            # Map known $ref schema names to actual model names
            schema_ref_map = {
                'UpdatePermissionsRequest': 'CreatePermissionRequest',  # UpdatePermissions uses CreatePermissionRequest
                'UpdateUserPropertiesRequest': 'UpdateOrganizationPropertiesRequest',  # UpdateUserProperties uses UpdateOrganizationPropertiesRequest
            }
            
            # Check mapping (case-insensitive)
            if operation_id in operation_to_model_map:
                request_type = operation_to_model_map[operation_id]
            elif operation_id.lower() in [k.lower() for k in operation_to_model_map.keys()]:
                # Find the matching key (case-insensitive)
                for key, value in operation_to_model_map.items():
                    if key.lower() == operation_id.lower():
                        request_type = value
                        break
            elif '$ref' in schema:
                ref_path = schema['$ref']
                ref_name = ref_path.split('/')[-1]
                # Convert snake_case to PascalCase
                if '_' in ref_name:
                    parts = ref_name.split('_')
                    ref_name = ''.join(word.capitalize() for word in parts)
                else:
                    ref_name = ref_name[0].upper() + ref_name[1:] if ref_name else None
                
                # Check if this schema ref needs to be mapped
                if ref_name in schema_ref_map:
                    request_type = schema_ref_map[ref_name]
                else:
                    request_type = ref_name
            else:
                # For inline schemas, derive from operation ID
                if operation_id:
                    model_name = operation_id[0].upper() + operation_id[1:]
                    if not model_name.endswith('Request'):
                        model_name += 'Request'
                    request_type = model_name
                else:
                    request_type = get_csharp_type(schema, is_response=False)
            
            # Create request object if we have a valid type
            if request_type and request_type != 'object':
                # Get required properties from schema
                required_props = schema.get('required', [])
                properties = schema.get('properties', {})
                
                # Build constructor arguments for required properties using named parameters
                # This avoids issues with parameter order
                named_args = []
                
                for prop_name in required_props:
                    prop_schema = properties.get(prop_name, {})
                    prop_type = get_csharp_type(prop_schema)
                    
                    # Convert property name from snake_case to camelCase for C# constructor parameter
                    # e.g., "customer_agreement_id" -> "customerAgreementId"
                    csharp_prop_name = to_camel_case(prop_name)
                    # For enum type names, use PascalCase
                    pascal_prop_name = to_pascal_case(prop_name)
                    
                    # Check if this is a oneOf/anyOf type - these typically have a $ref or oneOf/anyOf in schema
                    # Pattern: {RequestType}{PropertyName}Value
                    one_of_type = None
                    if '$ref' in prop_schema:
                        ref_path = prop_schema['$ref']
                        ref_name = ref_path.split('/')[-1]
                        if '_' in ref_name:
                            ref_name = to_pascal_case(ref_name)
                        else:
                            ref_name = ref_name[0].upper() + ref_name[1:] if ref_name else None
                        if ref_name:
                            one_of_type = ref_name
                    elif prop_schema.get('oneOf') or prop_schema.get('anyOf'):
                        # It's a oneOf/anyOf - construct type name as {RequestType}{PropertyName}Value
                        # But only if property name doesn't already end with "Value"
                        if prop_name.lower().endswith('value'):
                            one_of_type = f'{request_type}{pascal_prop_name}'
                        else:
                            one_of_type = f'{request_type}{pascal_prop_name}Value'
                    
                    # Check if this is an enum
                    enum_values = prop_schema.get('enum', [])
                    if enum_values:
                        # It's an enum - use the first enum value
                        # The enum type is typically {RequestType}.{PropertyName}Enum (PascalCase)
                        enum_type = f'{request_type}.{pascal_prop_name}Enum'
                        # Use the first enum value - convert to PascalCase for enum member name
                        first_enum_value = enum_values[0] if enum_values else 'Unknown'
                        # Convert enum value to enum member name (e.g., "str" -> "Str", "single_line_text" -> "SingleLineText")
                        # Handle underscores by converting to PascalCase
                        if '_' in first_enum_value:
                            enum_member = to_pascal_case(first_enum_value)
                        else:
                            enum_member = first_enum_value[0].upper() + first_enum_value[1:].lower() if first_enum_value else 'Unknown'
                        test_value = f'{enum_type}.{enum_member}'
                    # Check if this is a oneOf/anyOf type
                    elif one_of_type:
                        # It's a oneOf/anyOf type - construct it with a string value
                        test_value = f'new {one_of_type}("test-{prop_name}")'
                    elif prop_type.endswith('Value'):
                        # Type name ends with Value - likely a oneOf/anyOf type
                        test_value = f'new {prop_type}("test-{prop_name}")'
                    elif prop_type == 'string' or prop_type == 'string?':
                        test_value = f'"test-{prop_name}"'
                    elif prop_type == 'int' or prop_type == 'int?':
                        test_value = '1'
                    elif prop_type == 'bool' or prop_type == 'bool?':
                        test_value = 'true'
                    elif prop_type.startswith('Dictionary'):
                        test_value = f'new System.Collections.Generic.Dictionary<string, object>()'
                    elif prop_type.startswith('List'):
                        # For List types, check if items are enums or have $ref
                        items_schema = prop_schema.get('items', {})
                        items_enum = items_schema.get('enum', [])
                        if items_enum:
                            # It's a list of enums - construct the enum type name
                            # The enum type is typically {RequestType}.{PropertyName}Enum
                            enum_type = f'{request_type}.{pascal_prop_name}Enum'
                            test_value = f'new System.Collections.Generic.List<{enum_type}>()'
                        elif '$ref' in items_schema:
                            # It's a list of objects - get the type from $ref
                            ref_path = items_schema['$ref']
                            item_type = ref_path.split('/')[-1]
                            # Convert snake_case to PascalCase
                            if '_' in item_type:
                                item_type = to_pascal_case(item_type)
                            else:
                                item_type = item_type[0].upper() + item_type[1:] if item_type else "object"
                            test_value = f'new System.Collections.Generic.List<{item_type}>()'
                        elif items_schema.get('type') == 'object':
                            # It's a list of inline objects - use the nested type pattern
                            # Pattern: {RequestType}{PropertyName}Inner
                            item_type = f'{request_type}{pascal_prop_name}Inner'
                            test_value = f'new System.Collections.Generic.List<{item_type}>()'
                        else:
                            # Use the actual item type from prop_type
                            # prop_type is already "List<ItemType>" from get_csharp_type
                            test_value = f'new System.Collections.Generic.{prop_type}()'
                    else:
                        test_value = f'"test-{prop_name}"'
                    
                    named_args.append(f'{csharp_prop_name}: {test_value}')
                
                # Create request with required properties using named parameters
                if named_args:
                    args_str = ', '.join(named_args)
                    lines.append(f"                var request = new {request_type}({args_str});")
                else:
                    lines.append(f"                var request = new {request_type}();")
                request_var = "request"
            else:
                # Last resort - try operation ID
                operation_id = operation.get('operationId', '')
                if operation_id:
                    model_name = operation_id[0].upper() + operation_id[1:]
                    if not model_name.endswith('Request'):
                        model_name += 'Request'
                    lines.append(f"                var request = new {model_name}();")
                    request_var = "request"
    
    lines.append("")
    lines.append(f"                var api = CreateApi((client, config) => new {api_class_name}(client, config));")
    lines.append("")
    lines.append("                // Act")
    
    # Generate API call - same parameter order rules as real tests
    # PUT/PATCH: path params first, then query params, then request body
    # POST: path params first if they exist, then request body
    if method in ['PUT', 'PATCH']:
        # PUT/PATCH: path params FIRST, then query params, then request body, then file params
        path_params_only = [p for p in param_vars if p in path_params_from_path]
        query_params_only = [p for p in param_vars if p not in path_params_from_path]
        
        all_params = path_params_only + query_params_only
        if request_var:
            all_params.append(request_var)
        all_params.extend(file_params)
        
        if all_params:
            api_call = f"await api.{method_name}({', '.join(all_params)});"
        else:
            api_call = f"await api.{method_name}();"
    elif method == 'POST':
        # POST: path params first if they exist, then request body, then file params
        path_params_only = [p for p in param_vars if p in path_params_from_path]
        query_params_only = [p for p in param_vars if p not in path_params_from_path]
        
        all_params = path_params_only + query_params_only
        if request_var:
            all_params.append(request_var)
        all_params.extend(file_params)
        
        if all_params:
            api_call = f"await api.{method_name}({', '.join(all_params)});"
        else:
            api_call = f"await api.{method_name}();"
    elif method == 'GET':
        if param_vars:
            param_list = ', '.join(param_vars)
            api_call = f"await api.{method_name}({param_list});"
        else:
            api_call = f"await api.{method_name}();"
    elif method == 'DELETE':
        if param_vars:
            param_list = ', '.join(param_vars)
            api_call = f"await api.{method_name}({param_list});"
        else:
            api_call = f"await api.{method_name}();"
    else:
        if request_var:
            if param_vars:
                param_list = ', '.join(param_vars)
                api_call = f"await api.{method_name}({param_list}, {request_var});"
            else:
                api_call = f"await api.{method_name}({request_var});"
        elif param_vars:
            param_list = ', '.join(param_vars)
            api_call = f"await api.{method_name}({param_list});"
        else:
            api_call = f"await api.{method_name}();"
    
    # Handle void return types
    if is_void_response:
        lines.append(f"                {api_call}")
        lines.append("                // Void method - no response to check")
        lines.append("                _output.WriteLine($\"Mock test successful\");")
    else:
        lines.append(f"                var response = {api_call}")
        lines.append("")
        lines.append("                // Assert")
        lines.append("                Assert.NotNull(response);")
        lines.append("                _output.WriteLine($\"Mock test successful\");")
    
    return '\n'.join(lines)


# Mapping from OpenAPI tags to actual C# API class names
TAG_TO_API_CLASS = {
    'API Keys': 'APIsApi',
    'APIs': 'APIsApi',
    'API': 'APIsApi',
    'Applications': 'ApplicationsApi',
    'Billing Agreements': 'BillingAgreementsApi',
    'Billing Entitlements': 'BillingEntitlementsApi',
    'Billing Meter Usage': 'BillingMeterUsageApi',
    'Business': 'BusinessApi',
    'Callbacks': 'CallbacksApi',
    'Connected Apps': 'ConnectedAppsApi',
    'Connections': 'ConnectionsApi',
    'Environments': 'EnvironmentsApi',
    'Environment Variables': 'EnvironmentVariablesApi',
    'Environment variables': 'EnvironmentVariablesApi',  # lowercase variant
    'Feature Flags': 'FeatureFlagsApi',
    'Identities': 'IdentitiesApi',
    'Industries': 'IndustriesApi',
    'MFA': 'MFAApi',
    'Organizations': 'OrganizationsApi',
    'Permissions': 'PermissionsApi',
    'Properties': 'PropertiesApi',
    'Property Categories': 'PropertyCategoriesApi',
    'Roles': 'RolesApi',
    'Search': 'SearchApi',
    'Subscribers': 'SubscribersApi',
    'Timezones': 'TimezonesApi',
    'Users': 'UsersApi',
    'Webhooks': 'WebhooksApi',
}


def tag_to_api_class_name(tag: str) -> str:
    """Convert OpenAPI tag to actual C# API class name"""
    # Check direct mapping first
    if tag in TAG_TO_API_CLASS:
        return TAG_TO_API_CLASS[tag]
    
    # Try case-insensitive match
    tag_lower = tag.lower()
    for mapped_tag, api_class in TAG_TO_API_CLASS.items():
        if mapped_tag.lower() == tag_lower:
            return api_class
    
    # Fallback: convert tag to PascalCase and add Api
    api_name = to_pascal_case(tag.replace('-', '_').replace(' ', '_'))
    if not api_name.endswith('Api'):
        api_name += 'Api'
    return api_name


def process_openapi_spec(spec_path: str, output_dir: str):
    """Process OpenAPI spec and generate integration tests"""
    print(f"Loading OpenAPI spec from {spec_path}...")
    
    with open(spec_path, 'r', encoding='utf-8') as f:
        if spec_path.endswith('.yaml') or spec_path.endswith('.yml'):
            spec = yaml.safe_load(f)
        else:
            spec = json.load(f)
    
    paths = spec.get('paths', {})
    components = spec.get('components', {})
    schemas = components.get('schemas', {})
    
    # Determine namespace based on output directory
    # If output_dir ends with "Accounts", use Accounts namespace
    if output_dir.endswith('Accounts') or 'Accounts' in output_dir.split(os.sep):
        namespace = "Kinde.Api.Test.Integration.Api.Generated.Accounts"
    else:
        namespace = "Kinde.Api.Test.Integration.Api.Generated"
    
    # Group endpoints by tag (which usually corresponds to API class)
    api_groups: Dict[str, List[tuple]] = {}
    
    for path, path_item in paths.items():
        for method, operation in path_item.items():
            if method not in ['get', 'post', 'put', 'patch', 'delete', 'options', 'head']:
                continue
            
            tags = operation.get('tags', ['Default'])
            tag = tags[0] if tags else 'Default'
            
            if tag not in api_groups:
                api_groups[tag] = []
            
            api_groups[tag].append((method.upper(), path, operation))
    
    # Clear output directory before generating
    if os.path.exists(output_dir):
        print(f"Clearing existing test files in {output_dir}...")
        for file in os.listdir(output_dir):
            file_path = os.path.join(output_dir, file)
            if os.path.isfile(file_path) and file.endswith('.cs'):
                os.remove(file_path)
    else:
        os.makedirs(output_dir, exist_ok=True)
    
    # Generate test files for each API group
    for tag, endpoints in api_groups.items():
        # Convert tag to actual API class name using mapping
        api_name = tag_to_api_class_name(tag)
        
        test_class_name = f"{api_name}IntegrationTests"
        
        test_methods = []
        
        for method, path, operation in endpoints:
            # Use operationId for method name
            operation_id = operation.get('operationId', '')
            method_name = get_method_name_from_operation_id(operation_id, method)
            # Remove Async suffix for test method name
            test_method_base = method_name.replace('Async', '')
            
            # Generate mock test
            mock_body = generate_mock_test_body(method, path, operation, api_name)
            mock_test = TEST_METHOD_TEMPLATE.format(
                method_name=test_method_base,
                test_mode="Mock",
                skip_condition="UseRealApi",
                other_mode="real API",
                api_class_name=api_name,
                test_body=mock_body
            )
            test_methods.append(mock_test)
            
            # Generate real API test
            real_body = generate_test_body_for_endpoint(method, path, operation, api_name, "Real")
            real_test = TEST_METHOD_TEMPLATE.format(
                method_name=test_method_base,
                test_mode="Real",
                skip_condition="!UseRealApi",
                other_mode="mocks",
                api_class_name=api_name,
                test_body=real_body
            )
            test_methods.append(real_test)
        
        # Determine API using statements based on namespace
        # Check if any endpoint uses FileParameter
        has_file_params = False
        for method, path, operation in endpoints:
            request_body = operation.get('requestBody', {})
            if request_body:
                content = request_body.get('content', {})
                if 'multipart/form-data' in content:
                    schema = content['multipart/form-data'].get('schema', {})
                    properties = schema.get('properties', {})
                    for prop_schema in properties.values():
                        if prop_schema.get('type') == 'string' and prop_schema.get('format') == 'binary':
                            has_file_params = True
                            break
                if has_file_params:
                    break
        
        if namespace.endswith('.Accounts'):
            api_using_statements = """using Kinde.Accounts.Api;
using Kinde.Accounts.Model;
using Kinde.Accounts.Client;"""
        else:
            api_using_statements = """using Kinde.Api.Api;
using Kinde.Api.Model;"""
            if has_file_params:
                api_using_statements += "\nusing Kinde.Api.Client;"
        
        # Generate test file
        test_file_content = TEST_FILE_TEMPLATE.format(
            namespace=namespace,
            api_name=api_name,
            test_class_name=test_class_name,
            api_using_statements=api_using_statements,
            test_methods='\n'.join(test_methods)
        )
        
        test_file_path = os.path.join(output_dir, f"{test_class_name}.cs")
        with open(test_file_path, 'w', encoding='utf-8') as f:
            f.write(test_file_content)
        
        print(f"Generated: {test_file_path}")


def main():
    """Main entry point"""
    if len(sys.argv) < 3:
        print("Usage: generate-integration-tests.py <openapi-spec> <output-dir>")
        sys.exit(1)
    
    spec_path = sys.argv[1]
    output_dir = sys.argv[2]
    
    if not os.path.exists(spec_path):
        print(f"Error: OpenAPI spec not found: {spec_path}")
        sys.exit(1)
    
    try:
        process_openapi_spec(spec_path, output_dir)
        print(f"\nSuccessfully generated integration tests in {output_dir}")
    except Exception as e:
        print(f"Error generating tests: {e}", file=sys.stderr)
        import traceback
        traceback.print_exc()
        sys.exit(1)


if __name__ == '__main__':
    main()

