#!/usr/bin/env python3
"""
API Facade Generator for Kinde .NET SDK

This script generates facade wrapper stubs for the existing OpenAPI-generated
API classes to use the Kiota-generated clients under the hood.

The facades maintain backward compatibility with the existing API interfaces
while delegating to Kiota for actual HTTP requests.

Usage:
    python scripts/generate-api-facades.py

This will scan the existing API classes and generate facade implementation stubs.
Manual review and completion of the mappings is required.
"""

import os
import re
import sys
from pathlib import Path
from typing import List, Tuple

# Colors for output
BLUE = '\033[0;34m'
GREEN = '\033[0;32m'
YELLOW = '\033[1;33m'
RED = '\033[0;31m'
NC = '\033[0m'  # No Color


def log(message: str, level: str = "INFO"):
    """Print a log message with color coding."""
    colors = {"INFO": BLUE, "SUCCESS": GREEN, "WARNING": YELLOW, "ERROR": RED}
    prefixes = {"INFO": "[INFO]", "SUCCESS": "[SUCCESS]", "WARNING": "[WARNING]", "ERROR": "[ERROR]"}
    print(f"{colors.get(level, '')}{prefixes.get(level, '')} {message}{NC}")


def get_management_api_classes(api_dir: Path) -> List[str]:
    """Get list of Management API class names."""
    classes = []
    for file in api_dir.glob("*.cs"):
        if file.name not in ["AbstractOpenAPISchema.cs"]:
            class_name = file.stem
            classes.append(class_name)
    return sorted(classes)


def get_accounts_api_classes(accounts_api_dir: Path) -> List[str]:
    """Get list of Accounts API class names."""
    classes = []
    for file in accounts_api_dir.glob("*.cs"):
        if file.name not in ["IApi.cs"]:
            class_name = file.stem
            classes.append(class_name)
    return sorted(classes)


def extract_methods_from_interface(content: str, interface_name: str) -> List[dict]:
    """Extract method signatures from an interface."""
    methods = []
    
    # Find the interface block
    interface_pattern = rf'public interface {interface_name}[^{{]*\{{'
    match = re.search(interface_pattern, content)
    if not match:
        return methods
    
    start = match.end()
    brace_count = 1
    end = start
    
    for i, char in enumerate(content[start:], start):
        if char == '{':
            brace_count += 1
        elif char == '}':
            brace_count -= 1
            if brace_count == 0:
                end = i
                break
    
    interface_content = content[start:end]
    
    # Find method signatures (simplified pattern)
    method_pattern = r'(?:///[^\n]*\n)*\s*([A-Za-z_][A-Za-z0-9_<>,\s]*)\s+([A-Za-z_][A-Za-z0-9_]*)\s*\(([^)]*)\);'
    
    for match in re.finditer(method_pattern, interface_content):
        return_type = match.group(1).strip()
        method_name = match.group(2).strip()
        params = match.group(3).strip()
        
        methods.append({
            'return_type': return_type,
            'name': method_name,
            'params': params,
            'is_async': 'Task' in return_type or 'Async' in method_name
        })
    
    return methods


def generate_facade_stub(class_name: str, api_type: str = "management") -> str:
    """Generate a facade stub for an API class."""
    
    namespace = "Kinde.Api.Api" if api_type == "management" else "Kinde.Accounts.Api"
    kiota_namespace = "Kinde.Api.Kiota.Management" if api_type == "management" else "Kinde.Api.Kiota.Accounts"
    kiota_client = "KindeManagementClient" if api_type == "management" else "KindeAccountsClient"
    
    stub = f'''// FACADE STUB - Requires manual completion
// This file provides the structure for wrapping Kiota calls
// in the existing {class_name} interface.

/*
using System;
using AutoMapper;
using {kiota_namespace};
using {kiota_namespace}.Models;
using Kinde.Api.Mappers;
using Kinde.Api.Facades;

namespace {namespace}
{{
    // Add to existing {class_name} class as partial implementation
    public partial class {class_name}
    {{
        // Kiota client and mapper (to be injected or created)
        private {kiota_client} _kiotaClient;
        private IMapper _mapper => KindeMapperConfiguration.Mapper;
        
        // Initialize Kiota client from existing configuration
        private void InitializeKiotaClient()
        {{
            if (_kiotaClient == null && this.Configuration != null)
            {{
                var factory = new KiotaClientFactory(
                    this.ApiClient?.HttpClient ?? new HttpClient(),
                    this.Configuration.BasePath
                );
                _kiotaClient = factory.CreateManagementClient(this.Configuration.AccessToken);
            }}
        }}
        
        // Example async method implementation
        // Replace existing implementation with Kiota delegation
        /*
        public async Task<SomeResponse> SomeMethodAsync(
            SomeRequest request = default,
            CancellationToken cancellationToken = default)
        {{
            InitializeKiotaClient();
            
            // Map request: OpenAPI -> Kiota
            var kiotaRequest = _mapper.Map<KiotaModels.SomeRequest>(request);
            
            // Call Kiota
            var kiotaResponse = await _kiotaClient.Api.V1.SomePath.PostAsync(kiotaRequest, cancellationToken);
            
            // Map response: Kiota -> OpenAPI
            return _mapper.Map<SomeResponse>(kiotaResponse);
        }}
        
        // Example sync method with deprecation
        [Obsolete("Use SomeMethodAsync instead. Sync methods will be removed in v3.0")]
        public SomeResponse SomeMethod(SomeRequest request = default)
        {{
            return SomeMethodAsync(request).GetAwaiter().GetResult();
        }}
        */
    }}
}}
*/
'''
    return stub


def main():
    """Main entry point."""
    script_dir = Path(__file__).parent
    repo_root = script_dir.parent
    
    management_api_dir = repo_root / "Kinde.Api" / "Api"
    accounts_api_dir = repo_root / "Kinde.Api" / "Accounts" / "Api"
    output_dir = repo_root / "Kinde.Api" / "Facades" / "Stubs"
    
    log("Kinde .NET SDK - API Facade Generator")
    log("=" * 50)
    
    # Create output directory
    output_dir.mkdir(parents=True, exist_ok=True)
    
    # Generate Management API facades
    log("Scanning Management API classes...")
    management_classes = get_management_api_classes(management_api_dir)
    log(f"Found {len(management_classes)} Management API classes")
    
    for class_name in management_classes:
        stub = generate_facade_stub(class_name, "management")
        output_file = output_dir / f"{class_name}.facade.cs.txt"
        output_file.write_text(stub)
    
    # Generate Accounts API facades
    log("Scanning Accounts API classes...")
    accounts_classes = get_accounts_api_classes(accounts_api_dir)
    log(f"Found {len(accounts_classes)} Accounts API classes")
    
    for class_name in accounts_classes:
        stub = generate_facade_stub(class_name, "accounts")
        output_file = output_dir / f"{class_name}.facade.cs.txt"
        output_file.write_text(stub)
    
    log(f"Generated {len(management_classes) + len(accounts_classes)} facade stubs in {output_dir}")
    log("", "SUCCESS")
    log("Next steps:", "INFO")
    log("1. Review each stub in Kinde.Api/Facades/Stubs/")
    log("2. Integrate the patterns into the existing API classes")
    log("3. Map each method to the corresponding Kiota endpoint")
    log("4. Add [Obsolete] attributes to sync methods")


if __name__ == "__main__":
    main()

