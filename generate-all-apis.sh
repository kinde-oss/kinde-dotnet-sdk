#!/bin/bash

# Kinde .NET SDK - API Client Generator
# 
# This script generates API clients using Microsoft Kiota from OpenAPI specifications.
# The generated Kiota clients are used internally by the static facade classes that
# maintain backward compatibility with the existing API interfaces.
#
# Prerequisites:
#   - .NET SDK 8.0 or later
#   - Kiota CLI: dotnet tool install --global Microsoft.OpenApi.Kiota
#
# Usage:
#   ./generate-all-apis.sh              # Generate all API clients
#   ./generate-all-apis.sh --management # Generate only Management API client
#   ./generate-all-apis.sh --accounts   # Generate only Accounts API client

set -euo pipefail

# Colors for output
RED='\033[0;31m'
GREEN='\033[0;32m'
YELLOW='\033[1;33m'
BLUE='\033[0;34m'
CYAN='\033[0;36m'
NC='\033[0m' # No Color

# Print functions
print_status() {
    echo -e "${BLUE}[INFO]${NC} $1"
}

print_success() {
    echo -e "${GREEN}[SUCCESS]${NC} $1"
}

print_warning() {
    echo -e "${YELLOW}[WARNING]${NC} $1"
}

print_error() {
    echo -e "${RED}[ERROR]${NC} $1"
}

print_header() {
    echo -e "${CYAN}$1${NC}"
}

# Resolve paths relative to this script
SCRIPT_DIR="$(cd "$(dirname "${BASH_SOURCE[0]}")" && pwd)"
REPO_ROOT="${SCRIPT_DIR}"

# Set up .NET environment for Kiota (may need adjustment based on system)
if [ -d "/usr/local/Cellar/dotnet@8" ]; then
    DOTNET8_PATH=$(ls -d /usr/local/Cellar/dotnet@8/*/libexec 2>/dev/null | head -1)
    if [ -n "${DOTNET8_PATH}" ]; then
        export DOTNET_ROOT="${DOTNET8_PATH}"
        export PATH="${DOTNET_ROOT}:${PATH}"
    fi
fi
export PATH="${HOME}/.dotnet/tools:${PATH}"

# Parse command line arguments
GENERATE_MANAGEMENT=true
GENERATE_ACCOUNTS=true

while [[ $# -gt 0 ]]; do
    case $1 in
        --management)
            GENERATE_MANAGEMENT=true
            GENERATE_ACCOUNTS=false
            shift
            ;;
        --accounts)
            GENERATE_MANAGEMENT=false
            GENERATE_ACCOUNTS=true
            shift
            ;;
        --help|-h)
            echo "Usage: $0 [OPTIONS]"
            echo ""
            echo "Options:"
            echo "  --management  Generate only Management API client"
            echo "  --accounts    Generate only Accounts API client"
            echo "  --help, -h    Show this help message"
            exit 0
            ;;
        *)
            print_error "Unknown option: $1"
            exit 1
            ;;
    esac
done

# Main execution
print_header "=========================================="
print_header "  Kinde .NET SDK - API Client Generator"
print_header "=========================================="
echo ""

# Run Kiota generation
print_status "Running Kiota client generation..."

if ! "${SCRIPT_DIR}/scripts/generate-kiota-clients.sh"; then
    print_error "Kiota generation failed"
        exit 1
    fi
    
# Build the project to verify generation was successful
print_header "=========================================="
print_header "  Building Project"
print_header "=========================================="

print_status "Restoring NuGet packages..."
if ! dotnet restore "${REPO_ROOT}/Kinde.Api/Kinde.Api.csproj"; then
    print_error "Package restore failed"
        exit 1
    fi
    
print_status "Building project..."
if ! dotnet build "${REPO_ROOT}/Kinde.Api/Kinde.Api.csproj" --no-restore; then
    print_error "Build failed"
        exit 1
    fi
    
print_success "Build completed successfully"

# Summary
print_header "=========================================="
print_header "  Summary"
print_header "=========================================="

MANAGEMENT_COUNT=$(find "${REPO_ROOT}/Kinde.Api/Kiota/Management" -name "*.cs" 2>/dev/null | wc -l | tr -d ' ')
ACCOUNTS_COUNT=$(find "${REPO_ROOT}/Kinde.Api/Kiota/Accounts" -name "*.cs" 2>/dev/null | wc -l | tr -d ' ')

print_status "Management API: ${MANAGEMENT_COUNT} C# files"
print_status "Accounts API: ${ACCOUNTS_COUNT} C# files"
print_status "Total: $((MANAGEMENT_COUNT + ACCOUNTS_COUNT)) C# files generated"

echo ""
print_success "API client generation complete!"
echo ""
print_status "The generated Kiota clients are in:"
print_status "  - ${REPO_ROOT}/Kinde.Api/Kiota/Management/"
print_status "  - ${REPO_ROOT}/Kinde.Api/Kiota/Accounts/"
echo ""
print_status "The existing OpenAPI API classes in Kinde.Api/Api/ wrap these"
print_status "Kiota clients and maintain backward compatibility."
echo ""
