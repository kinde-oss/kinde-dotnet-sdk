#!/usr/bin/env python3
"""
Integration test generator for Kinde .NET SDK

Generates xUnit integration tests for all API endpoints based on OpenAPI specification.
Tests serialization/deserialization round-trips for all converters.
"""
from __future__ import annotations

import os
import sys
import logging
import yaml
from pathlib import Path
from dataclasses import dataclass, field
from typing import Optional, Dict, List, Set, Tuple, Any

import click
from jinja2 import Environment, FileSystemLoader, select_autoescape

# Configure logging
logging.basicConfig(
    level=logging.INFO,
    format='%(asctime)s - %(name)s - %(levelname)s - %(message)s'
)
logger = logging.getLogger(__name__)


@dataclass
class EndpointInfo:
    """Information about an API endpoint"""
    path: str
    method: str  # GET, POST, PUT, DELETE, etc.
    operation_id: str
    summary: Optional[str] = None
    description: Optional[str] = None
    tags: List[str] = field(default_factory=list)
    parameters: List[Dict[str, Any]] = field(default_factory=list)
    request_body: Optional[Dict[str, Any]] = None
    responses: Dict[str, Dict[str, Any]] = field(default_factory=dict)
    api_class: Optional[str] = None  # e.g., "APIsApi", "ApplicationsApi"
    method_name: Optional[str] = None  # e.g., "GetAPIs", "CreateApplication"
    response_type: Optional[str] = None  # e.g., "GetAPIsResponse"
    request_type: Optional[str] = None  # e.g., "CreateApplicationRequest"
    is_read_only: bool = False
    requires_parameters: bool = False
    parameter_names: List[str] = field(default_factory=list)


class OpenAPIParser:
    """Parse OpenAPI specification to extract endpoint information"""
    
    def __init__(self, spec_path: Path):
        self.spec_path = spec_path
        with open(spec_path, 'r', encoding='utf-8') as f:
            self.spec = yaml.safe_load(f)
    
    def parse_endpoints(self) -> List[EndpointInfo]:
        """Parse all endpoints from OpenAPI spec"""
        endpoints = []
        paths = self.spec.get('paths', {})
        
        for path, path_item in paths.items():
            for method, operation in path_item.items():
                if method not in ['get', 'post', 'put', 'delete', 'patch', 'head', 'options']:
                    continue
                
                operation_id = operation.get('operationId', '')
                if not operation_id:
                    continue
                
                # Map operation ID to API class and method name
                api_class, method_name = self._parse_operation_id(operation_id)
                
                # Determine response type from responses
                response_type = self._extract_response_type(operation.get('responses', {}))
                
                # Determine request type from request body
                request_type = self._extract_request_type(operation.get('requestBody'))
                
                # Check if read-only (only GET methods are typically read-only)
                is_read_only = method.lower() == 'get'
                
                # Extract parameters
                parameters = operation.get('parameters', [])
                requires_parameters = len(parameters) > 0
                parameter_names = [p.get('name') for p in parameters if p.get('name')]
                
                endpoint = EndpointInfo(
                    path=path,
                    method=method.upper(),
                    operation_id=operation_id,
                    summary=operation.get('summary'),
                    description=operation.get('description'),
                    tags=operation.get('tags', []),
                    parameters=parameters,
                    request_body=operation.get('requestBody'),
                    responses=operation.get('responses', {}),
                    api_class=api_class,
                    method_name=method_name,
                    response_type=response_type,
                    request_type=request_type,
                    is_read_only=is_read_only,
                    requires_parameters=requires_parameters,
                    parameter_names=parameter_names
                )
                
                endpoints.append(endpoint)
        
        return endpoints
    
    def _parse_operation_id(self, operation_id: str) -> Tuple[Optional[str], Optional[str]]:
        """Parse operation ID to extract API class and method name"""
        # Operation IDs typically follow patterns like:
        # - getAPIs -> APIsApi, GetAPIs
        # - createApplication -> ApplicationsApi, CreateApplication
        # - getOrganization -> OrganizationsApi, GetOrganization
        
        # Map common patterns
        api_class_map = {
            'api': 'APIsApi',
            'application': 'ApplicationsApi',
            'role': 'RolesApi',
            'permission': 'PermissionsApi',
            'property': 'PropertiesApi',
            'organization': 'OrganizationsApi',
            'connection': 'ConnectionsApi',
            'environment': 'EnvironmentsApi',
            'environmentVariable': 'EnvironmentVariablesApi',
            'business': 'BusinessApi',
            'industry': 'IndustriesApi',
            'timezone': 'TimezonesApi',
            'category': 'PropertyCategoriesApi',
            'subscriber': 'SubscribersApi',
            'user': 'UsersApi',
            'identity': 'IdentitiesApi',
            'webhook': 'WebhooksApi',
            'featureFlag': 'FeatureFlagsApi',
            'billing': 'BillingApi',
            'meter': 'BillingMeterUsageApi',
            'agreement': 'BillingAgreementsApi',
            'entitlement': 'BillingEntitlementsApi',
            'search': 'SearchApi',
            'callback': 'CallbacksApi',
            'connectedApp': 'ConnectedAppsApi',
            'mfa': 'MFAApi',
        }
        
        # Try to find matching API class
        # Check for more specific matches first (e.g., "environmentVariable" before "environment")
        operation_lower = operation_id.lower()
        
        # Special handling for environmentVariable vs environment
        # Check for environmentVariable first (more specific)
        if 'environmentvariable' in operation_lower or 'environment_variable' in operation_lower:
            api_class = 'EnvironmentVariablesApi'
            if operation_id:
                method_name = operation_id[0].upper() + operation_id[1:] if len(operation_id) > 1 else operation_id.upper()
            else:
                method_name = operation_id
            return api_class, method_name
        
        # Sort keys by length (longest first) to match more specific patterns first
        sorted_keys = sorted(api_class_map.keys(), key=len, reverse=True)
        
        for key in sorted_keys:
            if key in operation_lower:
                api_class = api_class_map[key]
                # Convert operation ID to method name (PascalCase)
                # Operation IDs are camelCase (e.g., "getBusiness", "getTimezones")
                # Need to convert to PascalCase (e.g., "GetBusiness", "GetTimezones")
                if operation_id:
                    # Capitalize first letter and keep rest as-is (camelCase -> PascalCase)
                    method_name = operation_id[0].upper() + operation_id[1:] if len(operation_id) > 1 else operation_id.upper()
                else:
                    method_name = operation_id
                return api_class, method_name
        
        return None, None
    
    def _extract_response_type(self, responses: Dict[str, Any]) -> Optional[str]:
        """Extract response type from responses object"""
        # Look for 200/201 response with schema
        for status_code in ['200', '201', '202']:
            if status_code in responses:
                response = responses[status_code]
                content = response.get('content', {})
                for content_type, schema_info in content.items():
                    if 'application/json' in content_type:
                        schema = schema_info.get('schema', {})
                        ref = schema.get('$ref', '')
                        if ref:
                            # Extract type name from $ref
                            type_name = ref.split('/')[-1]
                            return type_name
                        # Check for inline schema
                        if 'type' in schema:
                            return None  # Primitive type
        return None
    
    def _extract_request_type(self, request_body: Optional[Dict[str, Any]]) -> Optional[str]:
        """Extract request type from request body"""
        if not request_body:
            return None
        
        content = request_body.get('content', {})
        for content_type, schema_info in content.items():
            if 'application/json' in content_type:
                schema = schema_info.get('schema', {})
                ref = schema.get('$ref', '')
                if ref:
                    type_name = ref.split('/')[-1]
                    return type_name
        return None


class TestGenerator:
    """Generate integration test code"""
    
    def __init__(self, template_dir: Path):
        self.template_dir = template_dir
        self.env = Environment(
            loader=FileSystemLoader(str(template_dir)),
            autoescape=select_autoescape(['html', 'xml'])
        )
    
    def generate_test_file(self, endpoints: List[EndpointInfo], output_path: Path, 
                          namespace: str = "Kinde.Api.Test.Integration") -> str:
        """Generate integration test file"""
        template = self.env.get_template('integration_test.cs.j2')
        
        # Group endpoints by API class
        endpoints_by_api = {}
        for endpoint in endpoints:
            if not endpoint.api_class:
                continue
            if endpoint.api_class not in endpoints_by_api:
                endpoints_by_api[endpoint.api_class] = []
            endpoints_by_api[endpoint.api_class].append(endpoint)
        
        return template.render(
            namespace=namespace,
            endpoints_by_api=endpoints_by_api,
            all_endpoints=endpoints
        )


@click.command()
@click.option('--spec', type=click.Path(exists=True), required=True, 
              help='Path to OpenAPI specification YAML file')
@click.option('--output', type=click.Path(), required=True,
              help='Output path for generated test file')
@click.option('--template-dir', type=click.Path(exists=True),
              help='Directory containing Jinja2 templates (default: templates/)')
@click.option('--namespace', default='Kinde.Api.Test.Integration',
              help='C# namespace for generated tests')
@click.option('--verbose', '-v', is_flag=True, help='Verbose output')
def main(spec: str, output: str, template_dir: Optional[str], 
         namespace: str, verbose: bool):
    """Generate integration tests from OpenAPI specification"""
    
    if verbose:
        logging.getLogger().setLevel(logging.DEBUG)
    
    spec_path = Path(spec)
    output_path = Path(output)
    
    # Determine template directory
    if template_dir:
        template_path = Path(template_dir)
    else:
        # Default to templates/ directory relative to script
        script_dir = Path(__file__).parent
        template_path = script_dir / 'templates'
    
    if not template_path.exists():
        logger.error(f"Template directory not found: {template_path}")
        sys.exit(1)
    
    # Parse OpenAPI spec
    logger.info(f"Parsing OpenAPI spec: {spec_path}")
    parser = OpenAPIParser(spec_path)
    endpoints = parser.parse_endpoints()
    logger.info(f"Found {len(endpoints)} endpoints")
    
    # Generate tests
    logger.info(f"Generating integration tests...")
    generator = TestGenerator(template_path)
    test_code = generator.generate_test_file(endpoints, output_path, namespace)
    
    # Write output
    output_path.parent.mkdir(parents=True, exist_ok=True)
    with open(output_path, 'w', encoding='utf-8') as f:
        f.write(test_code)
    
    logger.info(f"Generated integration tests: {output_path}")
    logger.info(f"Generated {len(endpoints)} test methods")


if __name__ == '__main__':
    main()

