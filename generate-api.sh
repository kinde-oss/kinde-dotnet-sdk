#!/bin/bash

# OpenAPI Generator Script for Kinde .NET SDK
# This script regenerates the .NET client code from the remote OpenAPI specification

set -e

# Configuration
OPENAPI_SPEC_URL="https://api-spec.kinde.com/kinde-management-api-spec.yaml"
TEMP_OUTPUT_DIR="temp-generated"
OPENAPI_GENERATOR_VERSION="7.15.0"
OPENAPI_GENERATOR_JAR="openapi-generator-cli-${OPENAPI_GENERATOR_VERSION}.jar"
API_COPY_DIR="generated-api-files"

# Colors for output
GREEN='\033[0;32m'
BLUE='\033[0;34m'
YELLOW='\033[1;33m'
NC='\033[0m' # No Color

print_status() {
    echo -e "${BLUE}[INFO]${NC} $1"
}

print_success() {
    echo -e "${GREEN}[SUCCESS]${NC} $1"
}

print_warning() {
    echo -e "${YELLOW}[WARNING]${NC} $1"
}

# Download OpenAPI Generator CLI if not present
download_openapi_generator() {
    if [ ! -f "$OPENAPI_GENERATOR_JAR" ]; then
        print_status "Downloading OpenAPI Generator CLI version $OPENAPI_GENERATOR_VERSION..."
        curl -L "https://repo1.maven.org/maven2/org/openapitools/openapi-generator-cli/$OPENAPI_GENERATOR_VERSION/openapi-generator-cli-$OPENAPI_GENERATOR_VERSION.jar" -o "$OPENAPI_GENERATOR_JAR"
        print_success "OpenAPI Generator CLI downloaded."
    else
        print_status "OpenAPI Generator CLI already present."
    fi
}

# Generate .NET client code
generate_client_code() {
    print_status "Generating .NET client code from OpenAPI specification..."
    
    # Clean up previous generation
    if [ -d "$TEMP_OUTPUT_DIR" ]; then
        rm -rf "$TEMP_OUTPUT_DIR"
    fi
    
    # Check if Java is available
    if ! command -v java &> /dev/null; then
        echo "Error: Java is required but not found. Please install Java to continue."
        exit 1
    fi
    
    # Generate the client code
    java -jar "$OPENAPI_GENERATOR_JAR" generate \
        --input-spec "$OPENAPI_SPEC_URL" \
        --generator-name csharp \
        --output "$TEMP_OUTPUT_DIR" \
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
    
    print_success ".NET client code generated in temporary directory."
}

# Fix problematic XML comments in generated files
fix_xml_comments() {
    print_status "Fixing problematic XML comments in generated files..."
    
    # Remove problematic XML comments that contain certificate-like content
    # Using cross-platform compatible sed syntax
    find "$TEMP_OUTPUT_DIR" -name "*.cs" -type f -exec sed -i.bak -e '/<!--BEGIN CERTIFICATE-->/d' -e '/<!--END CERTIFICATE-->/d' -e '/-----BEGIN CERTIFICATE-----/d' -e '/-----END CERTIFICATE-----/d' -e '/-----BEGIN PRIVATE KEY-----/d' -e '/-----END PRIVATE KEY-----/d' -e '/-----BEGIN PUBLIC KEY-----/d' -e '/-----END PUBLIC KEY-----/d' {} \;
    find "$TEMP_OUTPUT_DIR" -name "*.cs.bak" -type f -delete
    
    print_success "XML comments fixed."
}

# Copy generated files to the main project
copy_generated_files() {
    print_status "Copying generated files to the main project..."
    
    # Copy Model files (including new ones to fix missing type errors)
    if [ -d "$TEMP_OUTPUT_DIR/src/Kinde.Api/Model" ]; then
        print_status "Copying Model files..."
        mkdir -p "Kinde.Api/Model"
        for file in "$TEMP_OUTPUT_DIR/src/Kinde.Api/Model"/*.cs; do
            if [ -f "$file" ]; then
                filename=$(basename "$file")
                cp "$file" "Kinde.Api/Model/$filename"
                print_status "Updated: $filename"
            fi
        done
    fi
    
    # Copy Enums files (including new ones)
    if [ -d "$TEMP_OUTPUT_DIR/src/Kinde.Api/Enums" ]; then
        print_status "Copying Enums files..."
        mkdir -p "Kinde.Api/Enums"
        for file in "$TEMP_OUTPUT_DIR/src/Kinde.Api/Enums"/*.cs; do
            if [ -f "$file" ]; then
                filename=$(basename "$file")
                cp "$file" "Kinde.Api/Enums/$filename"
                print_status "Updated: $filename"
            fi
        done
    fi
    
    # Copy API files to generated-api-files directory for reference
    if [ -d "$TEMP_OUTPUT_DIR/src/Kinde.Api/Api" ]; then
        print_status "Copying API files to generated-api-files directory for reference..."
        mkdir -p generated-api-files/Api
        cp -r "$TEMP_OUTPUT_DIR/src/Kinde.Api/Api"/* generated-api-files/Api/
        print_success "API files copied to generated-api-files/Api/"
    fi
    
    # Run final cleanup on the copied files
    if [ -f "./cleanup-generated-files.sh" ]; then
        print_status "Running final cleanup on copied files..."
        ./cleanup-generated-files.sh "Kinde.Api/Model"
        ./cleanup-generated-files.sh "Kinde.Api/Enums"
    fi
    
    print_success "File copying completed"
}

# Clean up temporary files
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
    
    print_success "Client files updated successfully."
}

cleanup() {
    print_status "Cleaning up temporary files..."
    rm -rf "$TEMP_OUTPUT_DIR"
    print_success "Cleanup completed."
}

# Main execution
main() {
    print_status "Starting OpenAPI generation process..."
    
    download_openapi_generator
    generate_client_code
    fix_xml_comments
    copy_generated_files
    copy_missing_client_files
    cleanup
    
    print_success "OpenAPI generation completed successfully!"
    print_status "Generated API files are available in: generated-api-files/Api/"
    print_status "Model and Enums files have been updated in your project."
    print_status "Run 'dotnet build' to verify the build is successful."
}

# Run main function
main 