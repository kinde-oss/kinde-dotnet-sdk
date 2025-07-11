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
    find "$TEMP_OUTPUT_DIR" -name "*.cs" -type f -exec sed -i '' '/<!--BEGIN CERTIFICATE-->/d' {} \;
    find "$TEMP_OUTPUT_DIR" -name "*.cs" -type f -exec sed -i '' '/<!--END CERTIFICATE-->/d' {} \;
    find "$TEMP_OUTPUT_DIR" -name "*.cs" -type f -exec sed -i '' '/-----BEGIN CERTIFICATE-----/d' {} \;
    find "$TEMP_OUTPUT_DIR" -name "*.cs" -type f -exec sed -i '' '/-----END CERTIFICATE-----/d' {} \;
    find "$TEMP_OUTPUT_DIR" -name "*.cs" -type f -exec sed -i '' '/-----BEGIN PRIVATE KEY-----/d' {} \;
    find "$TEMP_OUTPUT_DIR" -name "*.cs" -type f -exec sed -i '' '/-----END PRIVATE KEY-----/d' {} \;
    find "$TEMP_OUTPUT_DIR" -name "*.cs" -type f -exec sed -i '' '/-----BEGIN PUBLIC KEY-----/d' {} \;
    find "$TEMP_OUTPUT_DIR" -name "*.cs" -type f -exec sed -i '' '/-----END PUBLIC KEY-----/d' {} \;
    
    print_success "XML comments fixed."
}

# Copy generated Model files to existing project structure
copy_model_files() {
    print_status "Copying generated Model files to existing project structure..."
    
    # Create Model directory if it doesn't exist
    mkdir -p "Kinde.Api/Model"
    
    # Copy only existing Model files to preserve project structure
    for file in "$TEMP_OUTPUT_DIR"/src/Kinde.Api/Model/*.cs; do
        if [ -f "$file" ]; then
            filename=$(basename "$file")
            if [ -f "Kinde.Api/Model/$filename" ]; then
                cp "$file" "Kinde.Api/Model/"
                print_status "Updated Model file: $filename"
            else
                print_warning "Skipped new Model file: $filename (not in existing project)"
            fi
        fi
    done
    
    print_success "Model files updated."
}

# Copy generated Enums files to existing project structure
copy_enums_files() {
    print_status "Copying generated Enums files to existing project structure..."
    
    # Create Enums directory if it doesn't exist
    mkdir -p "Kinde.Api/Enums"
    
    # Copy only existing Enums files to preserve project structure
    for file in "$TEMP_OUTPUT_DIR"/src/Kinde.Api/Enums/*.cs; do
        if [ -f "$file" ]; then
            filename=$(basename "$file")
            if [ -f "Kinde.Api/Enums/$filename" ]; then
                cp "$file" "Kinde.Api/Enums/"
                print_status "Updated Enums file: $filename"
            else
                print_warning "Skipped new Enums file: $filename (not in existing project)"
            fi
        fi
    done
    
    print_success "Enums files updated."
}

# Copy generated API files to inspection directory
copy_api_files() {
    print_status "Copying generated API files to inspection directory..."
    
    # Clean up previous API copy directory
    if [ -d "$API_COPY_DIR" ]; then
        rm -rf "$API_COPY_DIR"
    fi
    
    # Create API copy directory
    mkdir -p "$API_COPY_DIR"
    
    # Copy all generated API files for inspection
    if [ -d "$TEMP_OUTPUT_DIR/src/Kinde.Api/Api" ]; then
        cp -r "$TEMP_OUTPUT_DIR/src/Kinde.Api/Api" "$API_COPY_DIR/"
        print_success "API files copied to $API_COPY_DIR/ for inspection"
        print_status "You can now review the generated API files in the $API_COPY_DIR/ directory"
        print_warning "Note: These API files are NOT integrated into your project - they're for inspection only"
    else
        print_warning "No API files found in generated output"
    fi
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
    fix_xml_comments
    copy_model_files
    copy_enums_files
    copy_api_files
    cleanup
    
    print_success "All done! Only existing Model and Enums files have been updated in the project structure."
    print_status "Note: New API files are excluded to preserve compatibility with existing client infrastructure."
    print_status "Generated API files are available in $API_COPY_DIR/ for inspection."
}

# Run main function
main 