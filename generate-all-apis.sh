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
OPENAPI_GENERATOR_VERSION="7.15.0"
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
        --additional-properties=targetFramework=net8.0 \
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
    
    # Additional properties for .NET 8.0 generation
    local addl_props="packageName=Kinde.Accounts,packageVersion=3.0.0,targetFramework=net8.0,nullableReferenceTypes=true,useDateTimeOffset=true,useCollection=false,returnICollection=false,validatable=false,useOneOfInterfaces=true,arrayType=List,netCoreProjectFile=true,hideGenerationTimestamp=true"
    
    # Generate code using OpenAPI generator
    if ! java -jar "${OPENAPI_JAR}" generate \
        -i "${spec_file}" \
        -g csharp \
        -o "${ACCOUNTS_TEMP_OUTPUT_DIR}" \
        --additional-properties="${addl_props}"; then
        print_error "Failed to generate Account API client code"
        exit 1
    fi
    
    print_success "Account API client code generated successfully"
}

# Fix problematic XML comments in generated files
fix_xml_comments() {
    print_status "Fixing problematic XML comments in generated files..."
    
    # Fix main API files
    if [ -d "${MAIN_TEMP_OUTPUT_DIR}" ]; then
        find "${MAIN_TEMP_OUTPUT_DIR}" -name "*.cs" -type f -exec sed -i.bak -e '/<!--BEGIN CERTIFICATE-->/d' -e '/<!--END CERTIFICATE-->/d' -e '/-----BEGIN CERTIFICATE-----/d' -e '/-----END CERTIFICATE-----/d' -e '/-----BEGIN PRIVATE KEY-----/d' -e '/-----END PRIVATE KEY-----/d' -e '/-----BEGIN PUBLIC KEY-----/d' -e '/-----END PUBLIC KEY-----/d' {} \;
        find "${MAIN_TEMP_OUTPUT_DIR}" -name "*.cs.bak" -type f -delete
    fi
    
    # Fix accounts API files
    if [ -d "${ACCOUNTS_TEMP_OUTPUT_DIR}" ]; then
        find "${ACCOUNTS_TEMP_OUTPUT_DIR}" -name "*.cs" -type f -exec sed -i '' 's/&lt;/</g; s/&gt;/>/g; s/&amp;/\&/g' {} \;
    fi
    
    print_success "XML comments fixed."
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
            sed -i '' '/using System.Text.RegularExpressions;/a\
using System.Text.Json;
' "Kinde.Api/Client/ClientUtils.cs"
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
            sed -i '' '/using System.Text.RegularExpressions;/a\
using System.Text.Json;
' "${client_utils_file}"
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
        sed -i '' 's/using Polly.Extensions.Http;/using Microsoft.Extensions.Http.Resilience;/' "${extensions_file}"
        
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
    print_header "🚀 Starting Combined Kinde .NET SDK API Generation Process"
    print_header "=========================================================="
    
    download_openapi_generator
    generate_main_api
    generate_accounts_api
    fix_xml_comments
    copy_main_api_files
    copy_accounts_api_files
    copy_missing_client_files
    fix_resilience_issues
    apply_compatibility_fixes
    cleanup_temp_files
    
    print_header "✅ Combined API Generation Completed Successfully!"
    print_success "📁 Main API files are available in: generated-api-files/Api/"
    print_success "📁 Account API files are available in: ${ACCOUNTS_FINAL_OUTPUT_DIR}"
    print_success "📁 Model and Enums files have been updated in your project."
    print_success "🔧 .NET Standard 2.1 compatibility fixes have been applied."
    print_status "🏗️  Run 'dotnet build' to verify the build is successful."
}

# Run main function
main
