#!/bin/bash

# Kiota Client Generation Script for Kinde .NET SDK
# This script generates both Management API and Accounts API clients using Microsoft Kiota
#
# Prerequisites:
#   - .NET SDK 8.0 or later
#   - Kiota CLI: dotnet tool install --global Microsoft.OpenApi.Kiota

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
REPO_ROOT="$(cd "${SCRIPT_DIR}/.." && pwd)"

# Set up .NET environment for Kiota (may need adjustment based on system)
# This is needed when multiple .NET versions are installed
if [ -d "/usr/local/Cellar/dotnet@8" ]; then
    DOTNET8_PATH=$(ls -d /usr/local/Cellar/dotnet@8/*/libexec 2>/dev/null | head -1)
    if [ -n "${DOTNET8_PATH}" ]; then
        export DOTNET_ROOT="${DOTNET8_PATH}"
        export PATH="${DOTNET_ROOT}:${PATH}"
    fi
fi
export PATH="${HOME}/.dotnet/tools:${PATH}"

# API Configuration
MANAGEMENT_API_SPEC_URL="https://api-spec.kinde.com/kinde-management-api-spec.yaml"
ACCOUNTS_API_SPEC_URL="https://api-spec.kinde.com/kinde-frontend-api-spec.yaml"

# Output directories
KIOTA_OUTPUT_DIR="${REPO_ROOT}/Kinde.Api/Kiota"
MANAGEMENT_OUTPUT_DIR="${KIOTA_OUTPUT_DIR}/Management"
ACCOUNTS_OUTPUT_DIR="${KIOTA_OUTPUT_DIR}/Accounts"

# Check if Kiota is installed
check_kiota() {
    print_status "Checking for Kiota CLI..."
    
    if ! command -v kiota &> /dev/null; then
        print_error "Kiota CLI is not installed."
        print_status "Install it with: dotnet tool install --global Microsoft.OpenApi.Kiota"
        exit 1
    fi
    
    KIOTA_VERSION=$(kiota --version 2>/dev/null || echo "unknown")
    print_success "Kiota CLI found: ${KIOTA_VERSION}"
}

# Clean up previous generation
cleanup_previous() {
    print_status "Cleaning up previous Kiota generation..."
    
    if [ -d "${MANAGEMENT_OUTPUT_DIR}" ]; then
        rm -rf "${MANAGEMENT_OUTPUT_DIR}"
        print_status "Removed ${MANAGEMENT_OUTPUT_DIR}"
    fi
    
    if [ -d "${ACCOUNTS_OUTPUT_DIR}" ]; then
        rm -rf "${ACCOUNTS_OUTPUT_DIR}"
        print_status "Removed ${ACCOUNTS_OUTPUT_DIR}"
    fi
    
    # Create output directories
    mkdir -p "${MANAGEMENT_OUTPUT_DIR}"
    mkdir -p "${ACCOUNTS_OUTPUT_DIR}"
    
    print_success "Cleanup complete"
}

# Generate Management API client
generate_management_api() {
    print_header "=========================================="
    print_header "  Generating Management API Client"
    print_header "=========================================="
    
    print_status "Spec URL: ${MANAGEMENT_API_SPEC_URL}"
    print_status "Output: ${MANAGEMENT_OUTPUT_DIR}"
    print_status "Namespace: Kinde.Api.Kiota.Management"
    print_status "Client Class: KindeManagementClient"
    
    kiota generate \
        --language CSharp \
        --openapi "${MANAGEMENT_API_SPEC_URL}" \
        --output "${MANAGEMENT_OUTPUT_DIR}" \
        --namespace-name "Kinde.Api.Kiota.Management" \
        --class-name "KindeManagementClient" \
        --backing-store \
        --exclude-backward-compatible \
        --clean-output \
        --log-level Warning
    
    if [ $? -eq 0 ]; then
        print_success "Management API client generated successfully"
    else
        print_error "Failed to generate Management API client"
        exit 1
    fi
}

# Generate Accounts API client
generate_accounts_api() {
    print_header "=========================================="
    print_header "  Generating Accounts API Client"
    print_header "=========================================="
    
    print_status "Spec URL: ${ACCOUNTS_API_SPEC_URL}"
    print_status "Output: ${ACCOUNTS_OUTPUT_DIR}"
    print_status "Namespace: Kinde.Api.Kiota.Accounts"
    print_status "Client Class: KindeAccountsClient"
    
    kiota generate \
        --language CSharp \
        --openapi "${ACCOUNTS_API_SPEC_URL}" \
        --output "${ACCOUNTS_OUTPUT_DIR}" \
        --namespace-name "Kinde.Api.Kiota.Accounts" \
        --class-name "KindeAccountsClient" \
        --backing-store \
        --exclude-backward-compatible \
        --clean-output \
        --log-level Warning
    
    if [ $? -eq 0 ]; then
        print_success "Accounts API client generated successfully"
    else
        print_error "Failed to generate Accounts API client"
        exit 1
    fi
}

# Count generated files
count_files() {
    print_header "=========================================="
    print_header "  Generation Summary"
    print_header "=========================================="
    
    MANAGEMENT_COUNT=$(find "${MANAGEMENT_OUTPUT_DIR}" -name "*.cs" 2>/dev/null | wc -l | tr -d ' ')
    ACCOUNTS_COUNT=$(find "${ACCOUNTS_OUTPUT_DIR}" -name "*.cs" 2>/dev/null | wc -l | tr -d ' ')
    
    print_status "Management API: ${MANAGEMENT_COUNT} C# files generated"
    print_status "Accounts API: ${ACCOUNTS_COUNT} C# files generated"
    print_status "Total: $((MANAGEMENT_COUNT + ACCOUNTS_COUNT)) C# files"
}

# Main execution
main() {
    print_header "=========================================="
    print_header "  Kinde .NET SDK - Kiota Client Generator"
    print_header "=========================================="
    echo ""
    
    check_kiota
    cleanup_previous
    generate_management_api
    generate_accounts_api
    count_files
    
    echo ""
    print_success "Kiota client generation complete!"
    print_status "Generated clients are in: ${KIOTA_OUTPUT_DIR}"
    echo ""
}

# Run main
main "$@"

