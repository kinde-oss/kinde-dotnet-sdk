#!/usr/bin/env python3
"""
Converter generator for Kinde .NET SDK

Generates Newtonsoft.Json converters for C# models with Option<> properties.
This generator extracts properties from both constructor parameters and property definitions,
handles nested generics, escapes C# reserved keywords, and generates type-safe converters.

Features:
- Type hints and dataclasses for clarity
- Jinja2 templates for maintainable code generation
- Configuration file support (YAML)
- Comprehensive logging
- C# reserved keyword escaping
- Support for all property types (string, enum, List<>, Dictionary<>, Object)
"""
from __future__ import annotations

import os
import re
import sys
import logging
from pathlib import Path
from dataclasses import dataclass, field
from typing import Optional, Dict, List, Set, Tuple, Any

import click
import yaml
from jinja2 import Environment, FileSystemLoader, select_autoescape

# Import ApiClient updater
try:
    from api_client_updater import ApiClientUpdater
except ImportError:
    # If running as module, try relative import
    try:
        from .api_client_updater import ApiClientUpdater
    except ImportError:
        ApiClientUpdater = None

# Configure logging
logging.basicConfig(
    level=logging.INFO,
    format='%(asctime)s - %(name)s - %(levelname)s - %(message)s'
)
logger = logging.getLogger(__name__)


# C# reserved keywords that need @ prefix
RESERVED_KEYWORDS = {
    'event', 'class', 'namespace', 'string', 'object', 'bool', 'int', 'long', 'float', 'double',
    'decimal', 'char', 'byte', 'sbyte', 'short', 'ushort', 'uint', 'ulong', 'void', 'null',
    'true', 'false', 'if', 'else', 'for', 'foreach', 'while', 'do', 'switch', 'case', 'default',
    'break', 'continue', 'return', 'try', 'catch', 'finally', 'throw', 'new', 'this', 'base',
    'public', 'private', 'protected', 'internal', 'static', 'readonly', 'const', 'virtual',
    'override', 'abstract', 'sealed', 'partial', 'async', 'await', 'using', 'namespace',
    'class', 'struct', 'interface', 'enum', 'delegate', 'var', 'dynamic', 'ref', 'out', 'in',
    'params', 'where', 'select', 'from', 'group', 'orderby', 'join', 'let', 'into', 'on',
    'equals', 'by', 'ascending', 'descending'
}

def escape_csharp_keyword(name: str) -> str:
    """Escape C# reserved keywords with @ prefix"""
    if name.lower() in RESERVED_KEYWORDS:
        return '@' + name
    return name


@dataclass
class PropertyInfo:
    """Information about a model property"""
    name: str  # camelCase constructor parameter name
    pascal_name: str  # PascalCase property name
    json_name: str
    csharp_type: str
    is_option: bool
    is_nullable: bool
    is_required: bool
    enum_type: Optional[str] = None
    generic_args: List[str] = field(default_factory=list)
    
    @property
    def is_string(self) -> bool:
        return self.csharp_type in ('string', 'string?')
    
    @property
    def is_enum(self) -> bool:
        return 'Enum' in self.csharp_type
    
    @property
    def is_list(self) -> bool:
        return self.csharp_type.startswith('List<')
    
    @property
    def is_dictionary(self) -> bool:
        return self.csharp_type.startswith('Dictionary<')


@dataclass
class ModelInfo:
    """Information about a C# model class"""
    name: str
    namespace: str
    properties: List[PropertyInfo] = field(default_factory=list)
    required_params: List[PropertyInfo] = field(default_factory=list)
    
    @property
    def has_option_properties(self) -> bool:
        return any(prop.is_option for prop in self.properties)
    
    @property
    def needs_converter(self) -> bool:
        return self.has_option_properties or len(self.required_params) > 0


class CSharpModelParser:
    """Parse C# model files using proven regex-based approach from v1"""
    
    def parse_model(self, file_path: Path) -> Optional[ModelInfo]:
        """Parse a C# model file and extract model information"""
        try:
            with open(file_path, 'r', encoding='utf-8') as f:
                content = f.read()
        except Exception as e:
            logger.error(f"Error reading {file_path}: {e}")
            return None
        
        # Find the class name
        class_match = re.search(r'public\s+partial\s+class\s+(\w+)', content)
        if not class_match:
            return None
        
        model_name = class_match.group(1)
        
        # Extract namespace
        namespace_match = re.search(r'namespace\s+([\w.]+)', content)
        namespace = namespace_match.group(1) if namespace_match else "Kinde.Api.Model"
        
        # Extract constructor
        constructor_match = re.search(
            r'public\s+' + re.escape(model_name) + r'\(([^)]+)\)',
            content
        )
        if not constructor_match:
            return None
        
        properties = []
        required_params = []
        
        # Parse constructor parameters first to get all parameter names
        constructor_params_str = constructor_match.group(1)
        params = self._split_parameters(constructor_params_str)
        
        # Build a map of constructor parameters
        constructor_params = {}
        for param in params:
            prop_info = self._parse_parameter(param, model_name, content)
            if prop_info:
                if prop_info.is_required:
                    required_params.append(prop_info)
                else:
                    constructor_params[prop_info.name] = prop_info
        
        # Extract properties from property definitions (like v1 does)
        # Pattern: [JsonPropertyName("json_name")] public Type? PropertyName { get { return this.PropertyNameOption; } set { this.PropertyNameOption = new Option<Type?>(value); } }
        prop_pattern = r'\[JsonPropertyName\("([^"]+)"\)\]\s+public\s+([^\?]+)\?\s+(\w+)\s+\{\s+get\s+\{\s+return\s+this\.(\w+)Option;\s+\}\s+set'
        
        for match in re.finditer(prop_pattern, content):
            json_name = match.group(1)
            prop_type = match.group(2).strip()
            prop_name = match.group(3)  # PascalCase property name
            option_prop_name = match.group(4)  # Option property name (without "Option" suffix)
            
            # Find the constructor parameter name (camelCase version of property name)
            constructor_param_name = None
            for param_name in constructor_params.keys():
                # Match by property name (case-insensitive) or by option property name
                if param_name.lower() == prop_name.lower() or param_name.lower() == option_prop_name.lower():
                    constructor_param_name = param_name
                    break
            
            # If not found, try to infer from property name
            if not constructor_param_name:
                # Property names are PascalCase, constructor params are camelCase
                constructor_param_name = prop_name[0].lower() + prop_name[1:] if prop_name else None
            
            # Verify the Option property exists
            option_pattern = rf'{option_prop_name}Option\s+\{{\s+get'
            if re.search(option_pattern, content) and constructor_param_name:
                # Check if we already have this in constructor_params
                if constructor_param_name in constructor_params:
                    # Update with property name info
                    prop_info = constructor_params[constructor_param_name]
                    prop_info.json_name = json_name
                    prop_info.pascal_name = prop_name  # Store PascalCase property name
                    properties.append(prop_info)
                else:
                    # Create new property info
                    # Extract enum type if applicable
                    enum_type = None
                    if 'Enum' in prop_type:
                        enum_match = re.search(r'(\w+Enum)', prop_type)
                        if enum_match:
                            enum_type = enum_match.group(1)
                    
                    # Check if nullable
                    is_nullable = prop_type.endswith('?')
                    if is_nullable:
                        prop_type = prop_type.rstrip('?')
                    
                    # Escape reserved keywords
                    escaped_name = escape_csharp_keyword(constructor_param_name)
                    
                    prop_info = PropertyInfo(
                        name=escaped_name,  # Use camelCase constructor param name (escaped)
                        pascal_name=prop_name,  # Store PascalCase property name
                        json_name=json_name,
                        csharp_type=prop_type,
                        is_option=True,
                        is_nullable=is_nullable,
                        is_required=False,
                        enum_type=enum_type
                    )
                    properties.append(prop_info)
        
        return ModelInfo(
            name=model_name,
            namespace=namespace,
            properties=properties,
            required_params=required_params
        )
    
    def _split_parameters(self, params_str: str) -> List[str]:
        """Split constructor parameters, handling nested generics (from v1)"""
        params = []
        current_param = ""
        bracket_depth = 0
        
        for char in params_str:
            if char == '<':
                bracket_depth += 1
                current_param += char
            elif char == '>':
                bracket_depth -= 1
                current_param += char
            elif char == ',' and bracket_depth == 0:
                params.append(current_param.strip())
                current_param = ""
            else:
                current_param += char
        
        if current_param.strip():
            params.append(current_param.strip())
        
        return params
    
    def _parse_parameter(
        self, 
        param_str: str, 
        model_name: str,
        file_content: str
    ) -> Optional[PropertyInfo]:
        """Parse a single constructor parameter (based on v1 logic)"""
        param_str = param_str.strip()
        
        # Check if it's an Option<> parameter
        if 'Option<' in param_str:
            # Extract Option<Type?> paramName = default
            option_match = re.search(r'Option<([^>]+)>\s+(\w+)\s*=', param_str)
            if option_match:
                param_type = option_match.group(1).strip()
                param_name = option_match.group(2)
                
                # Find JSON property name
                json_name = self._find_json_property_name(param_name, file_content)
                
                # Extract enum type if applicable
                enum_type = None
                if 'Enum' in param_type:
                    enum_match = re.search(r'(\w+Enum)', param_type)
                    if enum_match:
                        enum_type = enum_match.group(1)
                
                # Check if nullable
                is_nullable = param_type.endswith('?')
                if is_nullable:
                    param_type = param_type.rstrip('?')
                
                # Convert camelCase to PascalCase for property name
                pascal_name = param_name[0].upper() + param_name[1:] if param_name else param_name
                
                # Escape reserved keywords
                escaped_name = escape_csharp_keyword(param_name)
                
                return PropertyInfo(
                    name=escaped_name,
                    pascal_name=pascal_name,
                    json_name=json_name,
                    csharp_type=param_type,
                    is_option=True,
                    is_nullable=is_nullable,
                    is_required=False,
                    enum_type=enum_type
                )
        else:
            # Required parameter (no Option<>)
            # Format: Type paramName or Type paramName = value
            required_match = re.search(r'(\w+(?:<[^>]+>)?)\s+(\w+)(?:\s*=|$)', param_str)
            if required_match:
                param_type = required_match.group(1).strip()
                param_name = required_match.group(2)
                
                # Find JSON property name
                json_name = self._find_json_property_name(param_name, file_content)
                
                # Extract enum type if applicable
                enum_type = None
                if 'Enum' in param_type:
                    enum_match = re.search(r'(\w+Enum)', param_type)
                    if enum_match:
                        enum_type = enum_match.group(1)
                
                # Check if nullable
                is_nullable = param_type.endswith('?')
                if is_nullable:
                    param_type = param_type.rstrip('?')
                
                # Convert camelCase to PascalCase for property name
                pascal_name = param_name[0].upper() + param_name[1:] if param_name else param_name
                
                # Escape reserved keywords
                escaped_name = escape_csharp_keyword(param_name)
                
                return PropertyInfo(
                    name=escaped_name,
                    pascal_name=pascal_name,
                    json_name=json_name,
                    csharp_type=param_type,
                    is_option=False,
                    is_nullable=is_nullable,
                    is_required=True,
                    enum_type=enum_type
                )
        
        return None
    
    def _find_json_property_name(self, param_name: str, file_content: str) -> str:
        """Find the JSON property name from [JsonPropertyName] attribute (from v1)"""
        # Look for property with this name
        # Pattern: [JsonPropertyName("json_name")] public Type PropertyName
        pattern = rf'\[JsonPropertyName\("([^"]+)"\)\]\s+public\s+\S+\s+{re.escape(param_name)}\s*{{'
        match = re.search(pattern, file_content)
        if match:
            return match.group(1)
        
        # Fallback: convert camelCase to snake_case
        return re.sub(r'(?<!^)(?=[A-Z])', '_', param_name).lower()


class ConverterGenerator:
    """Generate C# converter code using Jinja2 templates"""
    
    def __init__(self, template_dir: Path):
        self.env = Environment(
            loader=FileSystemLoader(str(template_dir)),
            autoescape=select_autoescape(['html', 'xml']),
            trim_blocks=True,
            lstrip_blocks=True
        )
    
    def generate(self, model_info: ModelInfo) -> str:
        """Generate converter code for a model"""
        template = self.env.get_template('converter.cs.j2')
        return template.render(model=model_info)


class ConverterValidator:
    """Validate generated converter code"""
    
    def validate(self, converter_file: Path) -> Tuple[bool, Optional[str]]:
        """Validate that generated converter has correct structure"""
        try:
            with open(converter_file, 'r', encoding='utf-8') as f:
                content = f.read()
            
            # Basic syntax checks
            if 'public class' not in content:
                return False, "Missing class declaration"
            if 'ReadJson' not in content:
                return False, "Missing ReadJson method"
            if 'WriteJson' not in content:
                return False, "Missing WriteJson method"
            
            return True, None
        except Exception as e:
            return False, str(e)


@click.command()
@click.option('--model-dir', help='Directory containing C# model files (relative to project root)')
@click.option('--converter-dir', help='Directory for generated converters (relative to project root)')
@click.option('--config', type=click.Path(exists=True), help='Configuration YAML file')
@click.option('--api-client', type=click.Path(exists=True), help='Path to ApiClient.cs (relative to project root, auto-detected if not provided)')
@click.option('--update-api-client', is_flag=True, default=True, help='Automatically update ApiClient.cs with generated converters')
@click.option('--validate', is_flag=True, help='Validate generated converters')
@click.option('--verbose', '-v', is_flag=True, help='Verbose output')
def main(model_dir: Optional[str], converter_dir: Optional[str], config: Optional[str], 
         api_client: Optional[str], update_api_client: bool, validate: bool, verbose: bool):
    """Generate Newtonsoft.Json converters for C# models"""
    
    if verbose:
        logging.getLogger().setLevel(logging.DEBUG)
    
    # Determine project root (parent of tools directory)
    # Script is in tools/converter-generator/, so project root is tools/converter-generator/../../
    script_dir = Path(__file__).parent
    # If script is in tools/converter-generator/, project root is two levels up
    # Otherwise, assume we're in the project root
    if script_dir.name == 'converter-generator' and script_dir.parent.name == 'tools':
        project_root = script_dir.parent.parent
    else:
        # Fallback: assume script is in project root
        project_root = script_dir
    
    # Load configuration
    config_data = {}
    excluded_models = set()
    if config:
        try:
            config_path = Path(config)
            if not config_path.is_absolute():
                # If relative, try relative to script first, then project root
                if (script_dir / config_path).exists():
                    config_path = script_dir / config_path
                elif (project_root / config_path).exists():
                    config_path = project_root / config_path
            
            with open(config_path, 'r') as f:
                config_data = yaml.safe_load(f) or {}
            excluded_models = set(config_data.get('excluded_models', []))
            
            # Use config values if not provided via CLI
            if not model_dir:
                model_dir = config_data.get('model_dir', 'Kinde.Api/Model')
            if not converter_dir:
                converter_dir = config_data.get('converter_dir', 'Kinde.Api/Converters')
        except Exception as e:
            logger.warning(f"Could not load config file: {e}")
            if not model_dir:
                model_dir = 'Kinde.Api/Model'
            if not converter_dir:
                converter_dir = 'Kinde.Api/Converters'
    else:
        if not model_dir:
            model_dir = 'Kinde.Api/Model'
        if not converter_dir:
            converter_dir = 'Kinde.Api/Converters'
    
    # Resolve paths relative to project root
    model_path = (project_root / model_dir).resolve()
    converter_path = (project_root / converter_dir).resolve()
    converter_path.mkdir(parents=True, exist_ok=True)
    
    # Initialize components
    parser = CSharpModelParser()
    # Template directory is relative to this script
    template_dir = Path(__file__).parent / 'templates'
    if not template_dir.exists():
        logger.error(f"Template directory not found: {template_dir}")
        return
    
    generator = ConverterGenerator(template_dir)
    validator = ConverterValidator()
    
    # Find model files
    # Include Response, Request, Inner models, and nested models (like GetBusinessResponseBusiness)
    model_files = list(model_path.glob('*Response.cs')) + \
                  list(model_path.glob('*Request.cs')) + \
                  list(model_path.glob('*Inner.cs'))
    
    # Also find nested models that don't follow the *Response/*Request/*Inner pattern
    # These are typically nested classes like GetBusinessResponseBusiness, GetEnvironmentResponseEnvironment
    # They're identified by having Option<> properties but not matching the standard patterns
    all_model_files = list(model_path.glob('*.cs'))
    for model_file in all_model_files:
        if model_file.stem not in excluded_models and model_file not in model_files:
            # Check if this model has Option properties by parsing it
            try:
                with open(model_file, 'r', encoding='utf-8') as f:
                    content = f.read()
                    # Check if it's a model class with Option properties
                    if 'public partial class' in content and 'Option<' in content:
                        model_files.append(model_file)
                        logger.debug(f"Found nested model with Option properties: {model_file.name}")
            except Exception as e:
                logger.debug(f"Could not check {model_file.name}: {e}")
    
    # Filter out excluded models
    model_files = [f for f in model_files if f.stem not in excluded_models]
    
    generated = 0
    skipped = 0
    errors = []
    
    for model_file in sorted(model_files):
        model_name = model_file.stem
        
        # Parse model
        model_info = parser.parse_model(model_file)
        if not model_info:
            errors.append(f"Could not parse {model_name}")
            continue
        
        # Skip if no Option properties and no required params
        if not model_info.needs_converter:
            continue
        
        # Generate converter
        try:
            converter_code = generator.generate(model_info)
            converter_file = converter_path / f"{model_name}NewtonsoftConverter.cs"
            
            with open(converter_file, 'w', encoding='utf-8') as f:
                f.write(converter_code)
            
            # Validate if requested
            if validate:
                is_valid, error = validator.validate(converter_file)
                if not is_valid:
                    errors.append(f"{model_name}: {error}")
                    continue
            
            logger.info(f"Generated converter for {model_name} ({len(model_info.properties)} Option properties, {len(model_info.required_params)} required params)")
            generated += 1
        except Exception as e:
            logger.error(f"Error generating {model_name}: {e}")
            errors.append(f"{model_name}: {e}")
    
    # Update ApiClient.cs if requested
    if update_api_client:
        if ApiClientUpdater is None:
            logger.warning("ApiClientUpdater not available, skipping ApiClient.cs update")
        else:
            try:
                # Find ApiClient.cs path
                if api_client:
                    # Resolve path - could be relative to script, project root, or absolute
                    api_client_path = Path(api_client)
                    if not api_client_path.is_absolute():
                        # Try relative to project root first
                        if (project_root / api_client_path).exists():
                            api_client_path = (project_root / api_client_path).resolve()
                        # Try relative to script directory
                        elif (script_dir / api_client_path).exists():
                            api_client_path = (script_dir / api_client_path).resolve()
                        else:
                            # Assume relative to project root
                            api_client_path = (project_root / api_client_path).resolve()
                    else:
                        api_client_path = api_client_path.resolve()
                else:
                    # Try to find it automatically
                    updater_temp = ApiClientUpdater(converter_path, project_root)
                    api_client_path = updater_temp.find_api_client_path(project_root, Path(converter_dir))
                
                if api_client_path and api_client_path.exists():
                    # If updating the main ApiClient.cs, also scan the other API's converters to merge them
                    additional_dirs = []
                    if 'Kinde.Api/Client/ApiClient.cs' in str(api_client_path) or 'Kinde.Api\\Client\\ApiClient.cs' in str(api_client_path):
                        # This is the main ApiClient.cs, merge converters from both APIs
                        # If we're generating Kinde.Api converters, also scan Accounts converters
                        if 'Kinde.Api/Converters' in str(converter_path) or 'Kinde.Api\\Converters' in str(converter_path):
                            accounts_converter_dir = project_root / 'Kinde.Api' / 'Accounts' / 'Converters'
                            if accounts_converter_dir.exists():
                                additional_dirs.append(accounts_converter_dir.resolve())
                                logger.info(f"Also scanning Accounts converters from {accounts_converter_dir}")
                        # If we're generating Accounts converters, also scan main API converters
                        elif 'Accounts/Converters' in str(converter_path) or 'Accounts\\Converters' in str(converter_path):
                            main_converter_dir = project_root / 'Kinde.Api' / 'Converters'
                            if main_converter_dir.exists():
                                additional_dirs.append(main_converter_dir.resolve())
                                logger.info(f"Also scanning main API converters from {main_converter_dir}")
                    
                    # Ensure all paths are absolute
                    converter_path = converter_path.resolve()
                    api_client_path = api_client_path.resolve()
                    
                    updater = ApiClientUpdater(converter_path, api_client_path, additional_dirs)
                    if updater.update_api_client():
                        click.echo(f"✅ Updated {api_client_path.relative_to(project_root)} with converter registrations")
                    else:
                        click.echo(f"⚠️  Failed to update ApiClient.cs")
                else:
                    logger.warning(f"ApiClient.cs not found at expected location, skipping update. Use --api-client to specify path.")
            except Exception as e:
                logger.error(f"Error updating ApiClient.cs: {e}")
                click.echo(f"⚠️  Error updating ApiClient.cs: {e}")
    
    # Summary
    click.echo(f"\n✅ Generated {generated} converters, skipped {skipped}")
    if errors:
        click.echo(f"\n❌ Errors ({len(errors)}):")
        for error in errors[:10]:
            click.echo(f"  {error}")


if __name__ == '__main__':
    main()
