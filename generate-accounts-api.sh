#!/usr/bin/env bash
set -euo pipefail

# Colors for output
RED='\033[0;31m'
GREEN='\033[0;32m'
YELLOW='\033[1;33m'
BLUE='\033[0;34m'
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
SPEC_URL="https://api-spec.kinde.com/kinde-frontend-api-spec.yaml"
TEMP_OUTPUT_DIR="${REPO_ROOT}/temp-accounts-generated"
FINAL_OUTPUT_DIR="${REPO_ROOT}/generated-accounts-api-files"
OPENAPI_JAR="${REPO_ROOT}/${OPENAPI_GENERATOR_JAR}"

# Additional properties for .NET 8.0 generation
ADDL_PROPS="packageName=Kinde.Accounts,packageVersion=3.0.0,targetFramework=net8.0,nullableReferenceTypes=true,useDateTimeOffset=true,useCollection=false,returnICollection=false,validatable=false,useOneOfInterfaces=true,arrayType=List,netCoreProjectFile=true,hideGenerationTimestamp=true"

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

# Download the OpenAPI specification
download_spec() {
    print_status "Downloading Kinde Account API specification..." >&2
    
    local spec_file="${REPO_ROOT}/kinde-accounts-api-spec.yaml"
    
    if ! curl -fsSL "${SPEC_URL}" -o "${spec_file}"; then
        print_error "Failed to download OpenAPI specification from ${SPEC_URL}" >&2
        exit 1
    fi
    
    print_success "OpenAPI specification downloaded successfully" >&2
    echo "${spec_file}"
}

# Generate client code
generate_client_code() {
    local spec_file="$1"
    print_status "Generating Account API client code..."
    
    # Preflight checks
    command -v java >/dev/null 2>&1 || { print_error "Java is required but not found on PATH"; exit 1; }
    
    # Require Java 11+
    JAVA_VERSION_RAW="$(java -version 2>&1 | awk -F\" '/version/ {print $2}')"
    if [[ "${JAVA_VERSION_RAW}" == 1.* ]]; then
        JAVA_MAJOR="${JAVA_VERSION_RAW#1.}"; JAVA_MAJOR="${JAVA_MAJOR%%.*}"
    else
        JAVA_MAJOR="${JAVA_VERSION_RAW%%.*}"
    fi
    if ! [[ "${JAVA_MAJOR}" =~ ^[0-9]+$ ]] || (( JAVA_MAJOR < 11 )); then
        print_error "Java 11+ is required (detected: ${JAVA_VERSION_RAW})"
        exit 1
    fi
    
    # Verify spec file exists
    if [ ! -f "${spec_file}" ]; then
        print_error "OpenAPI specification file not found at ${spec_file}"
        exit 1
    fi
    
    # Create output directory
    mkdir -p "${TEMP_OUTPUT_DIR}"
    
    # Generate code using OpenAPI generator
    if ! java -jar "${OPENAPI_JAR}" generate \
        -i "${spec_file}" \
        -g csharp \
        -o "${TEMP_OUTPUT_DIR}" \
        --additional-properties="${ADDL_PROPS}"; then
        print_error "Failed to generate Account API client code"
        exit 1
    fi
    
    print_success "Account API client code generated successfully"
}

# Fix XML comments in generated files
fix_xml_comments() {
    print_status "Fixing problematic XML comments in generated files..."
    
    # Find and fix XML comment issues
    find "${TEMP_OUTPUT_DIR}" -name "*.cs" -type f -exec sed -i '' 's/&lt;/</g; s/&gt;/>/g; s/&amp;/\&/g' {} \;
    
    print_success "XML comments fixed"
}

# Copy generated files to the main project
copy_generated_files() {
    print_status "Copying generated Account API files to the main project..."
    
    # Create final output directory
    mkdir -p "${FINAL_OUTPUT_DIR}"
    
    # Copy all generated files
    if [ -d "${TEMP_OUTPUT_DIR}/src" ]; then
        cp -r "${TEMP_OUTPUT_DIR}/src"/* "${FINAL_OUTPUT_DIR}/"
        print_success "Account API files copied successfully"
    else
        print_error "Generated source files not found in ${TEMP_OUTPUT_DIR}/src"
        exit 1
    fi
    
    # Copy additional files if they exist
    for file in README.md .gitignore; do
        if [ -f "${TEMP_OUTPUT_DIR}/${file}" ]; then
            cp "${TEMP_OUTPUT_DIR}/${file}" "${FINAL_OUTPUT_DIR}/"
        fi
    done
}

# Copy missing client files for Account API
copy_missing_client_files() {
    print_status "Copying missing Client files for Account API..."
    
    # Copy Option.cs if it doesn't exist in the accounts project
    local accounts_client_dir="${FINAL_OUTPUT_DIR}/Kinde.Accounts/Client"
    if [ ! -f "${accounts_client_dir}/Option.cs" ]; then
        if [ -f "Kinde.Api/Client/Option.cs" ]; then
            cp "Kinde.Api/Client/Option.cs" "${accounts_client_dir}/Option.cs"
            print_status "Copied Option.cs to Account API Client directory"
        fi
    else
        print_status "Option.cs already exists in Account API Client directory"
    fi
    
    # Update ClientUtils.cs with TryDeserialize methods if they don't exist
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
    
    print_success "Account API Client files updated successfully"
}

# Clean up temporary files
cleanup_temp_files() {
    print_status "Cleaning up temporary files..."
    rm -rf "${TEMP_OUTPUT_DIR}"
    rm -f "${REPO_ROOT}/kinde-accounts-api-spec.yaml"
    print_success "Cleanup completed"
}

# Main execution
main() {
    print_status "Starting Kinde Account API generation process..."
    
    download_openapi_generator
    
    # Download the spec first
    local spec_file
    spec_file=$(download_spec)
    
    generate_client_code "${spec_file}"
    fix_xml_comments
    copy_generated_files
    copy_missing_client_files
    cleanup_temp_files
    
    print_success "Kinde Account API generation completed successfully!"
    print_status "Generated Account API files are available in: ${FINAL_OUTPUT_DIR}"
    print_status "Run 'dotnet build' to verify the build is successful."
}

# Run main function
main
