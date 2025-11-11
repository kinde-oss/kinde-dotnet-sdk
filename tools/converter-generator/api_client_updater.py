"""
ApiClient.cs updater - automatically updates CreateStandardConverters method
with all generated converters found in the converter directory.
"""
from __future__ import annotations

import re
from pathlib import Path
from typing import List, Dict, Set, Tuple, Optional
import logging

logger = logging.getLogger(__name__)


class ApiClientUpdater:
    """Updates ApiClient.cs to register all generated converters"""
    
    # Manually maintained converters that should always be included
    # These are special converters that may not exist in the converter directory
    # but should be registered if they exist
    MANUALLY_MAINTAINED_NAMES = {
        'CreateUserRequestIdentitiesInnerNewtonsoftConverter',
        'CreateUserIdentityRequestNewtonsoftConverter'
    }
    
    # Generic converters that should always be included
    GENERIC_CONVERTERS = [
        'NewtonsoftGenericEnumConverter',
        'OptionNewtonsoftConverter'
    ]
    
    def __init__(self, converter_dir: Path, api_client_path: Path, additional_converter_dirs: Optional[List[Path]] = None, registry_path: Optional[Path] = None):
        self.converter_dir = converter_dir
        self.api_client_path = api_client_path
        self.additional_converter_dirs = additional_converter_dirs or []
        # Registry file path - defaults to same directory as ApiClient.cs
        if registry_path:
            self.registry_path = registry_path
        else:
            self.registry_path = self.api_client_path.parent / "JsonConverterRegistry.cs"
    
    def scan_converters(self) -> Dict[str, List[Tuple[str, str]]]:
        """
        Scan converter directory and additional directories, then categorize converters.
        
        Returns:
            Dict with keys: 'request', 'response', 'inner', 'manually_maintained'
            Values are lists of tuples: (converter_name, namespace_prefix)
        """
        converters = {
            'request': [],
            'response': [],
            'inner': [],
            'manually_maintained': []
        }
        
        # Scan main converter directory
        all_dirs = [self.converter_dir] + self.additional_converter_dirs
        
        for converter_dir in all_dirs:
            if not converter_dir.exists():
                logger.debug(f"Converter directory not found: {converter_dir}")
                continue
            
            # Find all converter files
            converter_files = list(converter_dir.glob('*NewtonsoftConverter.cs'))
            
            # Determine namespace prefix based on directory location
            # Check the actual namespace in the generated converter file
            # Default to Kinde.Api.Converters (Accounts converters also use this namespace)
            namespace_prefix = 'Kinde.Api.Converters'
            
            # Try to read the first converter file to determine namespace
            if converter_files:
                try:
                    with open(converter_files[0], 'r', encoding='utf-8') as f:
                        sample_content = f.read()
                        # Check for namespace declaration
                        namespace_match = re.search(r'namespace\s+([\w.]+)', sample_content)
                        if namespace_match:
                            namespace_prefix = namespace_match.group(1)
                            logger.debug(f"Detected namespace {namespace_prefix} from {converter_files[0].name}")
                except Exception as e:
                    logger.debug(f"Could not read converter file to detect namespace: {e}")
            
            for converter_file in converter_files:
                converter_name = converter_file.stem  # e.g., "CreateUserRequestNewtonsoftConverter"
                
                # Skip if already added (avoid duplicates)
                already_added = any(converter_name == name for cat_name in ['request', 'response', 'inner', 'manually_maintained'] for name, _ in converters[cat_name])
                if already_added:
                    continue
                
                # Check if this is a manually maintained converter
                if converter_name in self.MANUALLY_MAINTAINED_NAMES:
                    converters['manually_maintained'].append((converter_name, namespace_prefix))
                    continue
                
                # Categorize by naming pattern
                if 'Request' in converter_name and 'Inner' not in converter_name:
                    converters['request'].append((converter_name, namespace_prefix))
                elif 'Response' in converter_name and 'Inner' not in converter_name:
                    converters['response'].append((converter_name, namespace_prefix))
                elif 'Inner' in converter_name:
                    converters['inner'].append((converter_name, namespace_prefix))
                else:
                    # Fallback: try to categorize by other patterns
                    if converter_name.startswith('Create') or converter_name.startswith('Update') or \
                       converter_name.startswith('Add') or converter_name.startswith('Replace') or \
                       converter_name.startswith('Set') or converter_name.startswith('Verify'):
                        converters['request'].append((converter_name, namespace_prefix))
                    else:
                        converters['response'].append((converter_name, namespace_prefix))
        
        # Sort each category alphabetically by converter name (first element of tuple)
        for category in converters:
            converters[category].sort(key=lambda x: x[0] if isinstance(x, tuple) else x)
        
        return converters
    
    def generate_converter_registrations(self, converters: Dict[str, List[Tuple[str, str]]]) -> str:
        """
        Generate the converter registration code.
        
        Args:
            converters: Categorized converter lists
            
        Returns:
            C# code for converter registrations
        """
        lines = []
        
        # Generic converters
        lines.append("                // Generic converters")
        for converter in self.GENERIC_CONVERTERS:
            lines.append(f"                new Kinde.Api.Converters.{converter}(),")
        lines.append("")
        
        # Manually maintained converters (always include - they're defined in CustomEnumConverter.cs)
        # These should always be registered even if they don't exist as separate files
        manually_maintained_to_include = []
        
        # First, add any that were found in the converter directory
        for converter_name, namespace_prefix in converters['manually_maintained']:
            if converter_name not in [name for name, _ in manually_maintained_to_include]:
                manually_maintained_to_include.append((converter_name, namespace_prefix))
        
        # Then, always include all known manually maintained converters
        # (they're defined in CustomEnumConverter.cs and should always be registered)
        for converter_name in sorted(self.MANUALLY_MAINTAINED_NAMES):
            if converter_name not in [name for name, _ in manually_maintained_to_include]:
                manually_maintained_to_include.append((converter_name, 'Kinde.Api.Converters'))
        
        if manually_maintained_to_include:
            lines.append("                // Request/Identity converters (manually maintained)")
            for converter_name, namespace_prefix in sorted(manually_maintained_to_include, key=lambda x: x[0]):
                lines.append(f"                new {namespace_prefix}.{converter_name}(),")
            lines.append("")
        
        # Request converters
        if converters['request']:
            lines.append("                // Request converters (alphabetically ordered)")
            for converter_name, namespace_prefix in converters['request']:
                lines.append(f"                new {namespace_prefix}.{converter_name}(),")
            lines.append("")
        
        # Response converters
        if converters['response']:
            lines.append("                // Response converters (alphabetically ordered)")
            for converter_name, namespace_prefix in converters['response']:
                lines.append(f"                new {namespace_prefix}.{converter_name}(),")
            lines.append("")
        
        # Inner model converters
        if converters['inner']:
            lines.append("                // Inner model converters (alphabetically ordered)")
            for converter_name, namespace_prefix in converters['inner']:
                lines.append(f"                new {namespace_prefix}.{converter_name}(),")
        
        # Remove trailing newline
        if lines and lines[-1] == "":
            lines.pop()
        
        return "\n".join(lines)
    
    def update_api_client(self) -> bool:
        """
        Generate JsonConverterRegistry.cs with all found converters and update ApiClient.cs to use it.
        
        Returns:
            True if update was successful, False otherwise
        """
        if not self.api_client_path.exists():
            logger.error(f"ApiClient.cs not found: {self.api_client_path}")
            return False
        
        # Scan for converters
        converters = self.scan_converters()
        
        # Read existing registry if it exists to preserve manually maintained converters
        existing_manual = []
        if self.registry_path.exists():
            try:
                with open(self.registry_path, 'r', encoding='utf-8') as f:
                    registry_content = f.read()
                # Extract manually maintained converters from existing registry
                manually_maintained_pattern = r'// Request/Identity converters \(manually maintained\)\s*\n(.*?)(?=\n\s*// [A-Z]|\n\s*$)'
                manually_maintained_match = re.search(manually_maintained_pattern, registry_content, re.DOTALL)
                if manually_maintained_match:
                    existing_manual = re.findall(r'new (?:Kinde\.Api\.(?:Accounts\.)?Converters)\.(\w+)\(\)', manually_maintained_match.group(1))
            except Exception as e:
                logger.debug(f"Could not read existing registry: {e}")
        
        # Add manually maintained converters if they exist in the registry
        for converter_name in existing_manual:
            if converter_name in self.MANUALLY_MAINTAINED_NAMES:
                already_added = any(converter_name == name for name, _ in converters['manually_maintained'])
                if not already_added:
                    converters['manually_maintained'].append((converter_name, 'Kinde.Api.Converters'))
                    logger.debug(f"Preserved manually maintained converter from registry: {converter_name}")
        
        logger.info(f"Found {sum(len(v) for v in converters.values())} converters: "
                   f"{len(converters['request'])} Request, "
                   f"{len(converters['response'])} Response, "
                   f"{len(converters['inner'])} Inner, "
                   f"{len(converters['manually_maintained'])} manually maintained")
        
        # Generate converter registrations
        converter_registrations = self.generate_converter_registrations(converters)
        
        # Generate the registry file
        try:
            from jinja2 import Template
            template_content = """// <auto-generated>
// This file is automatically generated by the converter generator.
// DO NOT EDIT THIS FILE MANUALLY - your changes will be overwritten.
// To regenerate this file, run: python generate_converters.py --config <config-file>
// </auto-generated>

using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Kinde.Api.Client
{
    /// <summary>
    /// Auto-generated registry of all JSON converters.
    /// This class is automatically generated - do not edit manually.
    /// </summary>
    internal static class JsonConverterRegistry
    {
        /// <summary>
        /// Creates the standard converter collection for JSON serialization.
        /// This method is auto-generated - do not edit manually.
        /// </summary>
        public static IList<JsonConverter> CreateStandardConverters()
        {
            return new List<JsonConverter>
            {
{{ converter_registrations }}
            };
        }
    }
}
"""
            template = Template(template_content)
            registry_content = template.render(converter_registrations=converter_registrations)
            
            with open(self.registry_path, 'w', encoding='utf-8') as f:
                f.write(registry_content)
            logger.info(f"Generated {self.registry_path} with {sum(len(v) for v in converters.values())} converters")
        except Exception as e:
            logger.error(f"Failed to generate registry file: {e}")
            return False
        
        # Update ApiClient.cs to use the registry
        try:
            with open(self.api_client_path, 'r', encoding='utf-8') as f:
                content = f.read()
        except Exception as e:
            logger.error(f"Failed to read ApiClient.cs: {e}")
            return False
        
        # Check if ApiClient.cs already uses the registry
        if 'JsonConverterRegistry.CreateStandardConverters()' in content:
            logger.debug("ApiClient.cs already uses JsonConverterRegistry")
            return True
        
        # Find the CreateStandardConverters method and replace it
        method_pattern = r'(public static IList<JsonConverter> CreateStandardConverters\(\)\s*\{[^}]*return new List<JsonConverter>\s*\{)(.*?)(\s*\};\s*\})'
        
        match = re.search(method_pattern, content, re.DOTALL)
        if match:
            # Replace the entire method body with a call to the registry
            replacement = """public static IList<JsonConverter> CreateStandardConverters()
        {
            return JsonConverterRegistry.CreateStandardConverters();
        }"""
            new_content = content[:match.start()] + replacement + content[match.end():]
        else:
            # Method not found, try to find JsonConverterHelper class
            helper_pattern = r'(internal static class JsonConverterHelper\s*\{[^}]*/// <summary>[^}]*/// Creates the standard converter collection[^}]*/// </summary>[^}]*public static IList<JsonConverter> CreateStandardConverters\(\)\s*\{[^}]*return new List<JsonConverter>\s*\{)(.*?)(\s*\};\s*\})'
            match = re.search(helper_pattern, content, re.DOTALL)
            if match:
                replacement = """internal static class JsonConverterHelper
    {
        /// <summary>
        /// Creates the standard converter collection for JSON serialization
        /// </summary>
        public static IList<JsonConverter> CreateStandardConverters()
        {
            return JsonConverterRegistry.CreateStandardConverters();
        }"""
                new_content = content[:match.start()] + replacement + content[match.end():]
            else:
                logger.warning("Could not find CreateStandardConverters method in ApiClient.cs - registry generated but ApiClient.cs not updated")
                return True
        
        # Write back
        try:
            with open(self.api_client_path, 'w', encoding='utf-8') as f:
                f.write(new_content)
            logger.info(f"Updated {self.api_client_path} to use JsonConverterRegistry")
            return True
        except Exception as e:
            logger.error(f"Failed to write ApiClient.cs: {e}")
            return False
    
    def find_api_client_path(self, project_root: Path, converter_dir: Path) -> Optional[Path]:
        """
        Find ApiClient.cs path based on converter directory location.
        
        Args:
            project_root: Project root directory
            converter_dir: Converter directory (e.g., Kinde.Api/Converters)
            
        Returns:
            Path to ApiClient.cs or None if not found
        """
        # Try to infer the API client path from converter directory
        # If converter_dir is "Kinde.Api/Converters", ApiClient should be at "Kinde.Api/Client/ApiClient.cs"
        # If converter_dir is "Kinde.Api/Accounts/Converters", ApiClient should be at "Kinde.Api/Accounts/Client/ApiClient.cs"
        
        converter_path = Path(converter_dir)
        
        # Remove "Converters" from path and add "Client/ApiClient.cs"
        if converter_path.name == "Converters":
            client_dir = converter_path.parent / "Client"
            api_client = client_dir / "ApiClient.cs"
            full_path = project_root / api_client
            if full_path.exists():
                return full_path
        
        # Fallback: try common locations
        common_paths = [
            project_root / "Kinde.Api" / "Client" / "ApiClient.cs",
            project_root / "Kinde.Api" / "ApiClient.cs",
        ]
        
        for path in common_paths:
            if path.exists():
                return path
        
        return None

