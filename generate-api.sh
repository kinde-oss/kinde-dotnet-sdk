#!/bin/bash

# OpenAPI Generator Script for Kinde .NET SDK
# This script regenerates the .NET client code from the remote OpenAPI specification

set -e

# Configuration
OPENAPI_SPEC_URL="https://api-spec.kinde.com/kinde-combined-api-specs.yaml"
TEMP_OUTPUT_DIR="temp-generated"
OPENAPI_GENERATOR_VERSION="7.0.1"
OPENAPI_GENERATOR_JAR="openapi-generator-cli.jar"
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
        --additional-properties=targetFramework=netstandard2.1 \
        --additional-properties=nullableReferenceTypes=true \
        --additional-properties=useDateTimeOffset=true \
        --additional-properties=useCollection=false \
        --additional-properties=returnICollection=false \
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
    
    # Copy Model files (only existing ones to avoid new incompatible files)
    if [ -d "$TEMP_OUTPUT_DIR/src/Kinde.Api/Model" ]; then
        print_status "Copying Model files..."
        for file in "$TEMP_OUTPUT_DIR/src/Kinde.Api/Model"/*.cs; do
            if [ -f "$file" ]; then
                filename=$(basename "$file")
                if [ -f "Kinde.Api/Model/$filename" ]; then
                    cp "$file" "Kinde.Api/Model/$filename"
                    print_status "Updated: $filename"
                else
                    print_status "Skipped new file: $filename (not in existing project)"
                fi
            fi
        done
    fi
    
    # Copy Enums files (only existing ones)
    if [ -d "$TEMP_OUTPUT_DIR/src/Kinde.Api/Enums" ]; then
        print_status "Copying Enums files..."
        for file in "$TEMP_OUTPUT_DIR/src/Kinde.Api/Enums"/*.cs; do
            if [ -f "$file" ]; then
                filename=$(basename "$file")
                if [ -f "Kinde.Api/Enums/$filename" ]; then
                    cp "$file" "Kinde.Api/Enums/$filename"
                    print_status "Updated: $filename"
                else
                    print_status "Skipped new file: $filename (not in existing project)"
                fi
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
    copy_generated_files
    cleanup
    
    print_success "OpenAPI generation completed successfully!"
    print_status "Generated API files are available in: generated-api-files/Api/"
    print_status "Model and Enums files have been updated in your project."
    print_status "Run 'dotnet build' to verify the build is successful."
}

# Run main function
main 