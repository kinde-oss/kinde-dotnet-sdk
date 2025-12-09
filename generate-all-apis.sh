#!/bin/bash

# Combined OpenAPI Generator Script for Kinde .NET SDK
# This script regenerates both the main API and Accounts API client code from OpenAPI specifications
# and applies .NET Standard 2.1 compatibility fixes

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

# Cleanup any temp files on exit
TMP_JAR=""
TMP_SHA=""
cleanup() {
    [[ -n "${TMP_JAR}" && -f "${TMP_JAR}" ]] && rm -f "${TMP_JAR}" || true
    [[ -n "${TMP_SHA}" && -f "${TMP_SHA}" ]] && rm -f "${TMP_SHA}" || true
}
trap cleanup EXIT

# Resolve paths relative to this script
SCRIPT_DIR="$(cd "$(dirname "${BASH_SOURCE[0]}")" && pwd)"
REPO_ROOT="${SCRIPT_DIR}"

# Configuration
OPENAPI_GENERATOR_VERSION="7.0.1"
OPENAPI_GENERATOR_JAR="openapi-generator-cli-${OPENAPI_GENERATOR_VERSION}.jar"
OPENAPI_JAR="${REPO_ROOT}/${OPENAPI_GENERATOR_JAR}"

# Main API Configuration
MAIN_API_SPEC_URL="https://api-spec.kinde.com/kinde-management-api-spec.yaml"
MAIN_TEMP_OUTPUT_DIR="${REPO_ROOT}/temp-generated"
MAIN_API_COPY_DIR="generated-api-files"

# Accounts API Configuration
ACCOUNTS_API_SPEC_URL="https://api-spec.kinde.com/kinde-frontend-api-spec.yaml"
ACCOUNTS_TEMP_OUTPUT_DIR="${REPO_ROOT}/temp-accounts-generated"
ACCOUNTS_FINAL_OUTPUT_DIR="${REPO_ROOT}/generated-accounts-api-files"

# Download OpenAPI Generator JAR if not present
download_openapi_generator() {
    print_status "Checking for OpenAPI Generator..."
    
    if [ ! -f "${OPENAPI_JAR}" ]; then
        print_status "Downloading OpenAPI Generator v${OPENAPI_GENERATOR_VERSION}..."
        JAR_URL="https://repo1.maven.org/maven2/org/openapitools/openapi-generator-cli/${OPENAPI_GENERATOR_VERSION}/${OPENAPI_GENERATOR_JAR}"
        SHA_URL="${JAR_URL}.sha256"
        TMP_JAR="$(mktemp "${OPENAPI_JAR}.XXXX")"
        TMP_SHA="$(mktemp "${OPENAPI_JAR}.sha256.XXXX")"

        # Fetch jar and checksum
        if ! curl -fsSL "${JAR_URL}" -o "${TMP_JAR}"; then
            print_error "Failed to download OpenAPI Generator JAR"
            exit 1
        fi
        
        if curl -fsSL "${SHA_URL}" -o "${TMP_SHA}"; then
            if command -v sha256sum >/dev/null 2>&1; then
                echo "$(awk '{print $1}' "${TMP_SHA}")  ${TMP_JAR}" | sha256sum -c -
            elif command -v shasum >/dev/null 2>&1; then
                echo "$(awk '{print $1}' "${TMP_SHA}")  ${TMP_JAR}" | shasum -a 256 -c -
            else
                print_warning "sha256 tools not found; skipping checksum verification"
            fi
        else
            print_warning "Unable to retrieve checksum; proceeding without verification"
        fi
        
        mv -f "${TMP_JAR}" "${OPENAPI_JAR}"
        rm -f "${TMP_SHA}" || true
        TMP_JAR=""
        TMP_SHA=""
        print_success "OpenAPI Generator downloaded successfully"
    else
        print_success "OpenAPI Generator already present"
    fi
}

# Generate main API client code
generate_main_api() {
    print_header "=== Generating Main API Client Code ==="
    print_status "Generating .NET client code from main OpenAPI specification..."
    
    # Clean up previous generation
    if [ -d "${MAIN_TEMP_OUTPUT_DIR}" ]; then
        rm -rf "${MAIN_TEMP_OUTPUT_DIR}"
    fi
    
    # Check if Java is available
    if ! command -v java &> /dev/null; then
        print_error "Java is required but not found. Please install Java to continue."
        exit 1
    fi
    
    # Generate the client code
    java -jar "${OPENAPI_JAR}" generate \
        --input-spec "${MAIN_API_SPEC_URL}" \
        --generator-name csharp \
        --output "${MAIN_TEMP_OUTPUT_DIR}" \
        --additional-properties=packageName=Kinde.Api \
        --additional-properties="targetFramework=netstandard2.0;netstandard2.1;net7.0" \
        --additional-properties=multiTarget=true \
        --additional-properties=nullableReferenceTypes=true \
        --additional-properties=useDateTimeOffset=true \
        --additional-properties=useCollection=false \
        --additional-properties=returnICollection=false \
        --additional-properties=useOneOfInterfaces=true \
        --additional-properties=arrayType=List \
        --additional-properties=netCoreProjectFile=true \
        --additional-properties=validatable=false \
        --additional-properties=hideGenerationTimestamp=true
    
    print_success ".NET main API client code generated in temporary directory."
}

# Generate accounts API client code
generate_accounts_api() {
    print_header "=== Generating Accounts API Client Code ==="
    print_status "Generating Account API client code..."
    
    # Download the spec first
    local spec_file="${REPO_ROOT}/kinde-accounts-api-spec.yaml"
    
    if ! curl -fsSL "${ACCOUNTS_API_SPEC_URL}" -o "${spec_file}"; then
        print_error "Failed to download OpenAPI specification from ${ACCOUNTS_API_SPEC_URL}"
        exit 1
    fi
    
    # Create output directory
    mkdir -p "${ACCOUNTS_TEMP_OUTPUT_DIR}"
    
    # Additional properties for .NET generation (using net7.0 as max - 7.0.1 doesn't support net8.0)
    local addl_props="packageName=Kinde.Accounts,packageVersion=3.0.0,targetFramework=netstandard2.0;netstandard2.1;net7.0,multiTarget=true,nullableReferenceTypes=true,useDateTimeOffset=true,useCollection=false,returnICollection=false,validatable=false,useOneOfInterfaces=true,arrayType=List,netCoreProjectFile=true,hideGenerationTimestamp=true"
    
    # Generate code using OpenAPI generator with generichost library for HttpClient support
    if ! java -jar "${OPENAPI_JAR}" generate \
        -i "${spec_file}" \
        -g csharp \
        -o "${ACCOUNTS_TEMP_OUTPUT_DIR}" \
        --library generichost \
        --additional-properties="${addl_props}"; then
        print_error "Failed to generate Account API client code"
        exit 1
    fi
    
    print_success "Account API client code generated successfully"
}

# Setup Python virtual environment for post-processing
setup_python_venv() {
    print_status "Setting up Python virtual environment for post-processing..."
    
    local scripts_dir="${REPO_ROOT}/scripts"
    local venv_dir="${scripts_dir}/venv"
    local setup_script="${scripts_dir}/setup-venv.sh"
    
    # Check if Python 3 is available
    if ! command -v python3 &> /dev/null; then
        print_warning "Python 3 not found. Post-processing will be skipped."
        return 1
    fi
    
    # Setup venv if it doesn't exist
    if [ ! -d "${venv_dir}" ]; then
        if [ -f "${setup_script}" ]; then
            bash "${setup_script}"
        else
            print_status "Creating Python virtual environment..."
            python3 -m venv "${venv_dir}"
            if [ -f "${scripts_dir}/requirements.txt" ]; then
                source "${venv_dir}/bin/activate"
                pip install --quiet --upgrade pip
                pip install --quiet -r "${scripts_dir}/requirements.txt"
                deactivate
            fi
        fi
    fi
    
    print_success "Python virtual environment ready"
    return 0
}

# Post-process generated code using Python script
post_process_generated_code() {
    print_header "=== Post-Processing Generated Code ==="
    print_status "Applying compatibility fixes to generated code..."
    
    local scripts_dir="${REPO_ROOT}/scripts"
    local venv_dir="${scripts_dir}/venv"
    local post_process_script="${scripts_dir}/post-process-generated-code.py"
    
    # Setup venv if needed
    if ! setup_python_venv; then
        print_warning "Skipping post-processing (Python not available)"
        return 0
    fi
    
    if [ ! -f "${post_process_script}" ]; then
        print_warning "Post-processing script not found at ${post_process_script}"
        return 0
    fi
    
    # Activate venv and run post-processing script
    if [[ "$OSTYPE" == "msys" || "$OSTYPE" == "win32" ]]; then
        # Windows
        source "${venv_dir}/Scripts/activate"
    else
        # Unix-like (Linux, macOS)
        source "${venv_dir}/bin/activate"
    fi
    
    # Process both temp directories
    local dirs_to_process=()
    if [ -d "${MAIN_TEMP_OUTPUT_DIR}" ]; then
        dirs_to_process+=("${MAIN_TEMP_OUTPUT_DIR}")
    fi
    if [ -d "${ACCOUNTS_TEMP_OUTPUT_DIR}" ]; then
        dirs_to_process+=("${ACCOUNTS_TEMP_OUTPUT_DIR}")
    fi
    
    if [ ${#dirs_to_process[@]} -eq 0 ]; then
        print_warning "No generated code directories found for post-processing"
        deactivate
        return 0
    fi
    
    # Run post-processing script
    if python3 "${post_process_script}" "${dirs_to_process[@]}"; then
        print_success "Post-processing completed successfully"
    else
        print_warning "Post-processing encountered some issues (continuing anyway)"
    fi
    
    deactivate
}

# Post-process final project files (after copying)
post_process_final_files() {
    print_header "=== Post-Processing Final Project Files ==="
    print_status "Applying compatibility fixes to final project files..."
    
    local scripts_dir="${REPO_ROOT}/scripts"
    local venv_dir="${scripts_dir}/venv"
    local post_process_script="${scripts_dir}/post-process-generated-code.py"
    
    # Check if Python is available
    if ! command -v python3 &> /dev/null; then
        print_warning "Python 3 not available, skipping final post-processing"
        return 0
    fi
    
    if [ ! -f "${post_process_script}" ]; then
        print_warning "Post-processing script not found"
        return 0
    fi
    
    # Activate venv and run post-processing on final project directories
    if [[ "$OSTYPE" == "msys" || "$OSTYPE" == "win32" ]]; then
        source "${venv_dir}/Scripts/activate"
    else
        source "${venv_dir}/bin/activate"
    fi
    
    # Process final project directories and specific files that need fixes
    local dirs_to_process=()
    if [ -d "${REPO_ROOT}/Kinde.Api/Model" ]; then
        dirs_to_process+=("${REPO_ROOT}/Kinde.Api/Model")
    fi
    if [ -d "${REPO_ROOT}/Kinde.Api/Client" ]; then
        dirs_to_process+=("${REPO_ROOT}/Kinde.Api/Client")
    fi
    if [ -d "${REPO_ROOT}/Kinde.Api/Accounts/Model" ]; then
        dirs_to_process+=("${REPO_ROOT}/Kinde.Api/Accounts/Model")
    fi
    if [ -d "${REPO_ROOT}/Kinde.Api/Accounts" ]; then
        dirs_to_process+=("${REPO_ROOT}/Kinde.Api/Accounts")
    fi
    if [ -d "${REPO_ROOT}/Kinde.Api/Auth" ]; then
        dirs_to_process+=("${REPO_ROOT}/Kinde.Api/Auth")
    fi
    
    if [ ${#dirs_to_process[@]} -gt 0 ]; then
        if python3 "${post_process_script}" "${dirs_to_process[@]}"; then
            print_success "Final post-processing completed successfully"
        else
            print_warning "Final post-processing encountered some issues (continuing anyway)"
        fi
    fi
    
    deactivate
}

# Generate integration tests from OpenAPI specs
generate_integration_tests() {
    print_header "=== Generating Integration Tests ==="
    print_status "Generating comprehensive integration tests from OpenAPI specifications..."
    
    local scripts_dir="${REPO_ROOT}/scripts"
    local venv_dir="${scripts_dir}/venv"
    local test_gen_script="${scripts_dir}/generate-integration-tests.py"
    local test_output_dir="${REPO_ROOT}/Kinde.Api.Test/Integration/Api/Generated"
    
    # Check if Python is available
    if ! command -v python3 &> /dev/null; then
        print_warning "Python 3 not available, skipping test generation"
        return 0
    fi
    
    if [ ! -f "${test_gen_script}" ]; then
        print_warning "Test generation script not found at ${test_gen_script}"
        return 0
    fi
    
    # Setup venv if needed
    if ! setup_python_venv; then
        print_warning "Skipping test generation (Python not available)"
        return 0
    fi
    
    # Activate venv
    if [[ "$OSTYPE" == "msys" || "$OSTYPE" == "win32" ]]; then
        source "${venv_dir}/Scripts/activate"
    else
        source "${venv_dir}/bin/activate"
    fi
    
    # Create output directory
    mkdir -p "${test_output_dir}"
    
    # Download main API spec if needed
    local main_spec_file="${REPO_ROOT}/temp-kinde-management-api-spec.yaml"
    if [ ! -f "${main_spec_file}" ]; then
        print_status "Downloading main API specification..."
        if ! curl -fsSL "${MAIN_API_SPEC_URL}" -o "${main_spec_file}"; then
            print_warning "Failed to download main API spec, skipping test generation"
            deactivate
            return 0
        fi
    fi
    
    # Generate tests from main API spec
    if python3 "${test_gen_script}" "${main_spec_file}" "${test_output_dir}"; then
        print_success "Integration tests generated successfully"
    else
        print_warning "Test generation encountered some issues (continuing anyway)"
    fi
    
    deactivate
    
    # Clean up temp spec file
    rm -f "${main_spec_file}"
    
    print_success "Integration test generation completed"
}

# Copy main API generated files to the main project
copy_main_api_files() {
    print_status "Copying main API generated files to the main project..."
    
    # Copy Model files (including new ones to fix missing type errors)
    if [ -d "${MAIN_TEMP_OUTPUT_DIR}/src/Kinde.Api/Model" ]; then
        print_status "Copying Model files..."
        mkdir -p "Kinde.Api/Model"
        for file in "${MAIN_TEMP_OUTPUT_DIR}/src/Kinde.Api/Model"/*.cs; do
            if [ -f "$file" ]; then
                filename=$(basename "$file")
                cp "$file" "Kinde.Api/Model/$filename"
                print_status "Updated: $filename"
            fi
        done
    fi
    
    # Copy Enums files (including new ones)
    if [ -d "${MAIN_TEMP_OUTPUT_DIR}/src/Kinde.Api/Enums" ]; then
        print_status "Copying Enums files..."
        mkdir -p "Kinde.Api/Enums"
        for file in "${MAIN_TEMP_OUTPUT_DIR}/src/Kinde.Api/Enums"/*.cs; do
            if [ -f "$file" ]; then
                filename=$(basename "$file")
                cp "$file" "Kinde.Api/Enums/$filename"
                print_status "Updated: $filename"
            fi
        done
    fi
    
    # Copy API files to generated-api-files directory for reference
    if [ -d "${MAIN_TEMP_OUTPUT_DIR}/src/Kinde.Api/Api" ]; then
        print_status "Copying API files to generated-api-files directory for reference..."
        mkdir -p generated-api-files/Api
        cp -r "${MAIN_TEMP_OUTPUT_DIR}/src/Kinde.Api/Api"/* generated-api-files/Api/
        print_success "API files copied to generated-api-files/Api/"
    fi
    
    print_success "Main API file copying completed"
}

# Copy accounts API generated files to the main project
copy_accounts_api_files() {
    print_status "Copying generated Account API files to the main project..."
    
    # Create Accounts API directory in the main project
    mkdir -p "Kinde.Api/Accounts"
    
    # Copy API files to the main project Accounts directory
    if [ -d "${ACCOUNTS_TEMP_OUTPUT_DIR}/src/Kinde.Accounts/Api" ]; then
        print_status "Copying Account API files..."
        mkdir -p "Kinde.Api/Accounts/Api"
        cp -r "${ACCOUNTS_TEMP_OUTPUT_DIR}/src/Kinde.Accounts/Api"/* "Kinde.Api/Accounts/Api/"
        print_success "Account API files copied to Kinde.Api/Accounts/Api/"
    fi
    
    # Copy Model files to the main project
    if [ -d "${ACCOUNTS_TEMP_OUTPUT_DIR}/src/Kinde.Accounts/Model" ]; then
        print_status "Copying Account API Model files..."
        mkdir -p "Kinde.Api/Accounts/Model"
        cp -r "${ACCOUNTS_TEMP_OUTPUT_DIR}/src/Kinde.Accounts/Model"/* "Kinde.Api/Accounts/Model/"
        print_success "Account API Model files copied to Kinde.Api/Accounts/Model/"
    fi
    
    # Copy Client files to the main project
    if [ -d "${ACCOUNTS_TEMP_OUTPUT_DIR}/src/Kinde.Accounts/Client" ]; then
        print_status "Copying Account API Client files..."
        mkdir -p "Kinde.Api/Accounts/Client"
        cp -r "${ACCOUNTS_TEMP_OUTPUT_DIR}/src/Kinde.Accounts/Client"/* "Kinde.Api/Accounts/Client/"
        print_success "Account API Client files copied to Kinde.Api/Accounts/Client/"
    fi
    
    # Copy Extensions files to the main project
    if [ -d "${ACCOUNTS_TEMP_OUTPUT_DIR}/src/Kinde.Accounts/Extensions" ]; then
        print_status "Copying Account API Extensions files..."
        mkdir -p "Kinde.Api/Accounts/Extensions"
        cp -r "${ACCOUNTS_TEMP_OUTPUT_DIR}/src/Kinde.Accounts/Extensions"/* "Kinde.Api/Accounts/Extensions/"
        print_success "Account API Extensions files copied to Kinde.Api/Accounts/Extensions/"
    fi
    
    # Copy the project file
    if [ -f "${ACCOUNTS_TEMP_OUTPUT_DIR}/src/Kinde.Accounts/Kinde.Accounts.csproj" ]; then
        print_status "Copying Account API project file..."
        cp "${ACCOUNTS_TEMP_OUTPUT_DIR}/src/Kinde.Accounts/Kinde.Accounts.csproj" "Kinde.Api/Accounts/Kinde.Accounts.csproj"
        print_success "Account API project file copied"
    fi
    
    # Copy additional files if they exist
    for file in README.md .gitignore; do
        if [ -f "${ACCOUNTS_TEMP_OUTPUT_DIR}/${file}" ]; then
            cp "${ACCOUNTS_TEMP_OUTPUT_DIR}/${file}" "Kinde.Api/Accounts/"
        fi
    done
    
    # Also copy to generated-accounts-api-files for reference
    print_status "Copying Account API files to generated-accounts-api-files directory for reference..."
    mkdir -p "${ACCOUNTS_FINAL_OUTPUT_DIR}"
    if [ -d "${ACCOUNTS_TEMP_OUTPUT_DIR}/src" ]; then
        cp -r "${ACCOUNTS_TEMP_OUTPUT_DIR}/src"/* "${ACCOUNTS_FINAL_OUTPUT_DIR}/"
        print_success "Account API files copied to generated-accounts-api-files for reference"
    fi
    
    print_success "Accounts API file copying completed"
}

# Copy missing client files
copy_missing_client_files() {
    print_status "Copying missing Client files..."
    
    # Copy Option.cs if it doesn't exist
    if [ ! -f "Kinde.Api/Client/Option.cs" ]; then
        if [ -f "../kinde-dotnet-sdk/Kinde.Api/Client/Option.cs" ]; then
            cp "../kinde-dotnet-sdk/Kinde.Api/Client/Option.cs" "Kinde.Api/Client/Option.cs"
            print_status "Copied Option.cs from main SDK"
        fi
    fi
    
    # Update ClientUtils.cs with TryDeserialize methods if they don't exist
    if ! grep -q "TryDeserialize" "Kinde.Api/Client/ClientUtils.cs"; then
        print_status "Adding TryDeserialize methods to ClientUtils.cs..."
        
        # Add System.Text.Json import if not present
        if ! grep -q "using System.Text.Json;" "Kinde.Api/Client/ClientUtils.cs"; then
            if [[ "$OSTYPE" == "darwin"* ]]; then
                sed -i '' '/using System.Text.RegularExpressions;/a\
using System.Text.Json;
' "Kinde.Api/Client/ClientUtils.cs"
            else
                sed -i '/using System.Text.RegularExpressions;/a\
using System.Text.Json;
' "Kinde.Api/Client/ClientUtils.cs"
            fi
        fi
        
        # Add TryDeserialize methods before the closing brace
        cat >> "Kinde.Api/Client/ClientUtils.cs" << 'EOF'

        /// <summary>
        /// Returns true when deserialization succeeds.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="json"></param>
        /// <param name="options"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public static bool TryDeserialize<T>(string json, JsonSerializerOptions options, out T? result)
        {
            try
            {
                result = JsonSerializer.Deserialize<T>(json, options);
                return result != null;
            }
            catch (Exception)
            {
                result = default;
                return false;
            }
        }

        /// <summary>
        /// Returns true when deserialization succeeds.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="reader"></param>
        /// <param name="options"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public static bool TryDeserialize<T>(ref Utf8JsonReader reader, JsonSerializerOptions options, out T? result)
        {
            try
            {
                result = JsonSerializer.Deserialize<T>(ref reader, options);
                return result != null;
            }
            catch (Exception)
            {
                result = default;
                return false;
            }
        }
EOF
        print_status "Added TryDeserialize methods to ClientUtils.cs"
    fi
    
    # Copy missing client files for Account API
    local accounts_client_dir="Kinde.Api/Accounts/Client"
    if [ ! -f "${accounts_client_dir}/Option.cs" ]; then
        if [ -f "Kinde.Api/Client/Option.cs" ]; then
            cp "Kinde.Api/Client/Option.cs" "${accounts_client_dir}/Option.cs"
            print_status "Copied Option.cs to Account API Client directory"
        fi
    else
        print_status "Option.cs already exists in Account API Client directory"
    fi
    
    # Update Account API ClientUtils.cs with TryDeserialize methods if they don't exist
    local client_utils_file="${accounts_client_dir}/ClientUtils.cs"
    if [ -f "${client_utils_file}" ] && ! grep -q "TryDeserialize" "${client_utils_file}"; then
        print_status "Adding TryDeserialize methods to Account API ClientUtils.cs..."
        
        # Add System.Text.Json import if not present
        if ! grep -q "using System.Text.Json;" "${client_utils_file}"; then
            if [[ "$OSTYPE" == "darwin"* ]]; then
                sed -i '' '/using System.Text.RegularExpressions;/a\
using System.Text.Json;
' "${client_utils_file}"
            else
                sed -i '/using System.Text.RegularExpressions;/a\
using System.Text.Json;
' "${client_utils_file}"
            fi
        fi
        
        # Add TryDeserialize methods before the closing brace
        cat >> "${client_utils_file}" << 'EOF'

        /// <summary>
        /// Returns true when deserialization succeeds.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="json"></param>
        /// <param name="options"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public static bool TryDeserialize<T>(string json, JsonSerializerOptions options, out T? result)
        {
            try
            {
                result = JsonSerializer.Deserialize<T>(json, options);
                return result != null;
            }
            catch (Exception)
            {
                result = default;
                return false;
            }
        }

        /// <summary>
        /// Returns true when deserialization succeeds.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="reader"></param>
        /// <param name="options"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public static bool TryDeserialize<T>(ref Utf8JsonReader reader, JsonSerializerOptions options, out T? result)
        {
            try
            {
                result = JsonSerializer.Deserialize<T>(ref reader, options);
                return result != null;
            }
            catch (Exception)
            {
                result = default;
                return false;
            }
        }
EOF
        print_status "Added TryDeserialize methods to Account API ClientUtils.cs"
    fi
    
    print_success "Client files updated successfully."
}

# Fix resilience issues in generated extensions
fix_resilience_issues() {
    print_status "Fixing resilience issues in generated extensions..."
    
    local extensions_file="Kinde.Api/Accounts/Extensions/IHttpClientBuilderExtensions.cs"
    
    if [ -f "${extensions_file}" ]; then
        # Replace the deprecated Polly.Extensions.Http with modern Microsoft.Extensions.Http.Resilience
        if [[ "$OSTYPE" == "darwin"* ]]; then
            sed -i '' 's/using Polly.Extensions.Http;/using Microsoft.Extensions.Http.Resilience;/' "${extensions_file}"
        else
            sed -i 's/using Polly.Extensions.Http;/using Microsoft.Extensions.Http.Resilience;/' "${extensions_file}"
        fi
        
        # Replace the old extension methods with modern resilience patterns
        cat > "${extensions_file}" << 'EOF'
/*
 * Kinde Account API
 *
 *  Provides endpoints to operate on an authenticated user.  ## Intro  ## How to use  1. Get a user access token - this can be obtained when a user signs in via the methods you've setup in Kinde (e.g. Google, passwordless, etc).  2. Call one of the endpoints below using the user access token in the Authorization header as a Bearer token. Typically, you can use the `getToken` command in the relevant SDK. 
 *
 * The version of the OpenAPI document: 1
 * Contact: support@kinde.com
 * Generated by: https://github.com/openapitools/openapi-generator.git
 */

#nullable enable

using System;
using System.Net.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Http.Resilience;
using Polly;

namespace Kinde.Accounts.Extensions
{
    /// <summary>
    /// Extension methods for IHttpClientBuilder
    /// </summary>
    public static class IHttpClientBuilderExtensions
    {
        /// <summary>
        /// Adds a retry policy to your clients using modern resilience patterns.
        /// </summary>
        /// <param name="client"></param>
        /// <param name="retries"></param>
        /// <returns></returns>
        public static IHttpClientBuilder AddRetryPolicy(this IHttpClientBuilder client, int retries)
        {
            client.AddStandardResilienceHandler(options =>
            {
                options.Retry.MaxRetryAttempts = retries;
            });

            return client;
        }

        /// <summary>
        /// Adds a timeout policy to your clients using modern resilience patterns.
        /// </summary>
        /// <param name="client"></param>
        /// <param name="timeout"></param>
        /// <returns></returns>
        public static IHttpClientBuilder AddTimeoutPolicy(this IHttpClientBuilder client, TimeSpan timeout)
        {
            client.AddStandardResilienceHandler(options =>
            {
                options.TotalRequestTimeout.Timeout = timeout;
            });

            return client;
        }

        /// <summary>
        /// Adds a circuit breaker policy to your clients using modern resilience patterns.
        /// </summary>
        /// <param name="client"></param>
        /// <param name="handledEventsAllowedBeforeBreaking"></param>
        /// <param name="durationOfBreak"></param>
        /// <returns></returns>
        public static IHttpClientBuilder AddCircuitBreakerPolicy(this IHttpClientBuilder client, int handledEventsAllowedBeforeBreaking, TimeSpan durationOfBreak)
        {
            client.AddStandardResilienceHandler(options =>
            {
                options.CircuitBreaker.FailureRatio = 0.5; // 50% failure ratio
                options.CircuitBreaker.SamplingDuration = TimeSpan.FromSeconds(30);
                options.CircuitBreaker.MinimumThroughput = handledEventsAllowedBeforeBreaking;
                options.CircuitBreaker.BreakDuration = durationOfBreak;
            });

            return client;
        }

    }
}
EOF
        
        print_success "Resilience issues fixed in IHttpClientBuilderExtensions.cs"
    else
        print_warning "IHttpClientBuilderExtensions.cs not found, skipping resilience fix"
    fi
}

# Apply .NET Standard 2.1 compatibility fixes
apply_compatibility_fixes() {
    print_header "=== Applying .NET Standard 2.1 Compatibility Fixes ==="
    
    if [ -f "./apply-compatibility-fixes.js" ]; then
        print_status "Applying .NET Standard 2.1 compatibility fixes..."
        if node apply-compatibility-fixes.js; then
            print_success ".NET Standard 2.1 compatibility fixes applied successfully"
        else
            print_error "Failed to apply compatibility fixes"
            exit 1
        fi
    else
        print_error "apply-compatibility-fixes.js not found!"
        exit 1
    fi
}

# Clean up temporary files
cleanup_temp_files() {
    print_status "Cleaning up temporary files..."
    rm -rf "${MAIN_TEMP_OUTPUT_DIR}"
    rm -rf "${ACCOUNTS_TEMP_OUTPUT_DIR}"
    rm -f "${REPO_ROOT}/kinde-accounts-api-spec.yaml"
    print_success "Cleanup completed"
}

# Main execution
main() {
    print_header "🚀 Starting Kinde .NET SDK API Generation Process"
    print_header "=========================================================="
    
    download_openapi_generator
    generate_main_api
    # Accounts API is now manually maintained - no longer auto-generated
    # generate_accounts_api
    post_process_generated_code
    copy_main_api_files
    # Accounts API files are manually maintained - no longer copied from generation
    # copy_accounts_api_files
    copy_missing_client_files
    fix_resilience_issues
    # Post-process again on final project directories to ensure fixes are applied
    post_process_final_files
    apply_compatibility_fixes
    generate_integration_tests
    cleanup_temp_files
    
    print_header "✅ API Generation Completed Successfully!"
    print_success "📁 Main API files are available in: generated-api-files/Api/"
    print_success "📁 Model and Enums files have been updated in your project."
    print_success "🔧 .NET Standard 2.1 compatibility fixes have been applied."
    print_status "📝 Note: Accounts API is manually maintained and not auto-generated."
    print_status "🏗️  Run 'dotnet build' to verify the build is successful."
}

# Run main function
main
