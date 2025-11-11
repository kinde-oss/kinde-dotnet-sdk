#!/usr/bin/env python3
"""
Mock response generator for Kinde .NET SDK integration tests

Generates mock HTTP responses based on OpenAPI specification.
These responses are used by MockHttpHandler for CI/CD testing.
"""
from __future__ import annotations

import os
import sys
import logging
import yaml
import json
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
class MockResponseInfo:
    """Information about a mock response"""
    path: str
    method: str
    status_code: int = 200
    response_schema: Optional[Dict[str, Any]] = None
    response_type: Optional[str] = None
    sample_data: Optional[Dict[str, Any]] = None


class OpenAPIParser:
    """Parse OpenAPI specification to extract response information"""
    
    def __init__(self, spec_path: Path):
        self.spec_path = spec_path
        with open(spec_path, 'r', encoding='utf-8') as f:
            self.spec = yaml.safe_load(f)
        self.components = self.spec.get('components', {}).get('schemas', {})
    
    def parse_mock_responses(self) -> List[MockResponseInfo]:
        """Parse all GET endpoints to generate mock responses"""
        mock_responses = []
        paths = self.spec.get('paths', {})
        
        for path, path_item in paths.items():
            # Only handle GET methods for read-only endpoints
            if 'get' not in path_item:
                continue
            
            operation = path_item['get']
            operation_id = operation.get('operationId', '')
            if not operation_id:
                continue
            
            # Normalize path for pattern matching (replace {param} with pattern)
            # This allows the mock handler to match parameterized paths
            pattern_path = path
            if '{' in path:
                # For parameterized paths, create a pattern that matches any ID
                # e.g., /api/v1/apis/{api_id} -> /api/v1/apis/
                pattern_path = path.rsplit('/', 1)[0] + '/'
            
            # Extract response schema
            responses = operation.get('responses', {})
            response_schema = None
            response_type = None
            status_code = 200
            
            # Look for 200/201 response
            for code in ['200', '201', '202']:
                if code in responses:
                    response = responses[code]
                    content = response.get('content', {})
                    for content_type, schema_info in content.items():
                        if 'application/json' in content_type:
                            schema = schema_info.get('schema', {})
                            response_schema = schema
                            status_code = int(code)
                            
                            # Extract response type name
                            ref = schema.get('$ref', '')
                            if ref:
                                response_type = ref.split('/')[-1]
                            break
                    if response_schema:
                        break
            
            if not response_schema:
                continue
            
            # Generate sample data from schema
            sample_data = self._generate_sample_data(response_schema, response_type)
            
            mock_response = MockResponseInfo(
                path=pattern_path,  # Use pattern path for parameterized endpoints
                method='GET',
                status_code=status_code,
                response_schema=response_schema,
                response_type=response_type,
                sample_data=sample_data
            )
            
            mock_responses.append(mock_response)
        
        return mock_responses
    
    def _generate_sample_data(self, schema: Dict[str, Any], response_type: Optional[str]) -> Dict[str, Any]:
        """Generate sample data from OpenAPI schema"""
        # Handle $ref
        if '$ref' in schema:
            ref = schema['$ref']
            type_name = ref.split('/')[-1]
            if type_name in self.components:
                return self._generate_sample_data(self.components[type_name], type_name)
            return {}
        
        # Handle allOf (common in Kinde responses)
        if 'allOf' in schema:
            result = {}
            for sub_schema in schema['allOf']:
                sub_data = self._generate_sample_data(sub_schema, None)
                result.update(sub_data)
            return result
        
        # Handle object type
        if schema.get('type') == 'object':
            properties = schema.get('properties', {})
            result = {}
            for prop_name, prop_schema in properties.items():
                # Include all properties for responses (readOnly is fine for mock responses)
                result[prop_name] = self._generate_sample_value(prop_schema, prop_name)
            return result
        
        # Handle array type
        if schema.get('type') == 'array':
            items = schema.get('items', {})
            return [self._generate_sample_value(items, 'item')]
        
        return {}
    
    def _generate_sample_value(self, schema: Dict[str, Any], prop_name: str) -> Any:
        """Generate a sample value for a schema property"""
        # Handle $ref
        if '$ref' in schema:
            ref = schema['$ref']
            type_name = ref.split('/')[-1]
            if type_name in self.components:
                return self._generate_sample_data(self.components[type_name], type_name)
            return {}
        
        # Handle allOf
        if 'allOf' in schema:
            result = {}
            for sub_schema in schema['allOf']:
                sub_data = self._generate_sample_data(sub_schema, None)
                result.update(sub_data)
            return result
        
        # Handle enum
        if 'enum' in schema:
            return schema['enum'][0]
        
        # Handle different types
        schema_type = schema.get('type')
        
        if schema_type == 'string':
            # Use property name to generate meaningful sample
            if 'format' in schema:
                if schema['format'] == 'date-time':
                    return "2024-01-01T00:00:00Z"
                elif schema['format'] == 'email':
                    return f"{prop_name}@example.com"
                elif schema['format'] == 'uri':
                    return f"https://example.com/{prop_name}"
            # Generate sample based on property name
            if 'id' in prop_name.lower():
                return f"{prop_name}_001"
            elif 'name' in prop_name.lower():
                return f"Test {prop_name.title()}"
            elif 'code' in prop_name.lower():
                return f"{prop_name[:3]}_test123"
            elif 'key' in prop_name.lower():
                return f"{prop_name}:test"
            elif 'url' in prop_name.lower() or 'uri' in prop_name.lower():
                return f"https://example.com/{prop_name}"
            elif 'email' in prop_name.lower():
                return f"{prop_name}@example.com"
            else:
                return f"test_{prop_name}"
        
        elif schema_type == 'integer':
            return 1
        
        elif schema_type == 'number':
            return 1.0
        
        elif schema_type == 'boolean':
            return True
        
        elif schema_type == 'array':
            items = schema.get('items', {})
            return [self._generate_sample_value(items, 'item')]
        
        elif schema_type == 'object':
            properties = schema.get('properties', {})
            result = {}
            for sub_prop_name, sub_prop_schema in properties.items():
                # Include all properties for responses (readOnly is fine for mock responses)
                result[sub_prop_name] = self._generate_sample_value(sub_prop_schema, sub_prop_name)
            return result
        
        return None


class MockResponseGenerator:
    """Generate mock response code"""
    
    def __init__(self, template_dir: Path):
        self.template_dir = template_dir
        self.env = Environment(
            loader=FileSystemLoader(str(template_dir)),
            autoescape=select_autoescape(['html', 'xml'])
        )
    
    def generate_mock_responses_file(self, mock_responses: List[MockResponseInfo], 
                                     output_path: Path) -> str:
        """Generate C# file with mock response setup"""
        template = self.env.get_template('mock_responses.cs.j2')
        
        return template.render(
            mock_responses=mock_responses,
            output_path=output_path
        )


@click.command()
@click.option('--spec', type=click.Path(exists=True), required=True, 
              help='Path to OpenAPI specification YAML file')
@click.option('--output', type=click.Path(), required=True,
              help='Output path for generated mock responses file')
@click.option('--template-dir', type=click.Path(exists=True),
              help='Directory containing Jinja2 templates (default: templates/)')
@click.option('--verbose', '-v', is_flag=True, help='Verbose output')
def main(spec: str, output: str, template_dir: Optional[str], verbose: bool):
    """Generate mock responses from OpenAPI specification"""
    
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
    mock_responses = parser.parse_mock_responses()
    logger.info(f"Found {len(mock_responses)} GET endpoints for mock responses")
    
    # Generate mock responses
    logger.info(f"Generating mock responses...")
    generator = MockResponseGenerator(template_path)
    code = generator.generate_mock_responses_file(mock_responses, output_path)
    
    # Write output
    output_path.parent.mkdir(parents=True, exist_ok=True)
    with open(output_path, 'w', encoding='utf-8') as f:
        f.write(code)
    
    logger.info(f"Generated mock responses: {output_path}")
    logger.info(f"Generated {len(mock_responses)} mock response definitions")


if __name__ == '__main__':
    main()

