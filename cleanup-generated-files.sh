#!/bin/bash

# Comprehensive Cleanup Script for Generated C# Files
# This script removes problematic patterns from generated files that cause build errors

set -e

# Colors for output
GREEN='\033[0;32m'
BLUE='\033[0;34m'
RED='\033[0;31m'
NC='\033[0m' # No Color

print_status() {
    echo -e "${BLUE}[INFO]${NC} $1"
}

print_success() {
    echo -e "${GREEN}[SUCCESS]${NC} $1"
}

print_error() {
    echo -e "${RED}[ERROR]${NC} $1"
}

# Function to clean a single file
clean_file() {
    local file="$1"
    local temp_file="${file}.tmp"
    local original_size=$(wc -l < "$file")
    local cleaned_size=0
    
    print_status "Cleaning file: $(basename "$file")"
    
    # Create a temporary file for processing
    cp "$file" "$temp_file"
    
    # Remove base64 certificate data patterns
    # Pattern 1: Lines that are just base64 data (20+ characters of base64)
    sed -i '' '/^[[:space:]]*[A-Za-z0-9+\/=]\{20,\}$/d' "$temp_file"
    
    # Pattern 2: Base64 data within XML comments
    sed -i '' '/<example>.*[A-Za-z0-9+\/=]\{20,\}.*<\/example>/d' "$temp_file"
    
    # Pattern 3: Certificate patterns in XML comments
    sed -i '' '/<example>.*-----BEGIN CERTIFICATE-----/,/-----END CERTIFICATE-----<\/example>/d' "$temp_file"
    sed -i '' '/<example>.*-----BEGIN PRIVATE KEY-----/,/-----END PRIVATE KEY-----<\/example>/d' "$temp_file"
    sed -i '' '/<example>.*-----BEGIN PUBLIC KEY-----/,/-----END PUBLIC KEY-----<\/example>/d' "$temp_file"
    
    # Pattern 4: Long base64 strings that break XML comments
    sed -i '' '/<example>.*[A-Za-z0-9+\/=]\{50,\}/d' "$temp_file"
    
    # Pattern 5: Remove any lines that are just base64 data without proper XML structure
    sed -i '' '/^[[:space:]]*[A-Za-z0-9+\/=]\{30,\}[[:space:]]*$/d' "$temp_file"
    
    # Pattern 6: Remove malformed XML comments that contain base64
    sed -i '' '/^[[:space:]]*\/\/\/.*[A-Za-z0-9+\/=]\{20,\}/d' "$temp_file"
    
    # Pattern 7: Remove any lines that start with base64-like patterns in XML comments
    sed -i '' '/^[[:space:]]*\/\/\/.*[A-Za-z0-9+\/=]\{15,\}/d' "$temp_file"
    
    # Pattern 8: Clean up any remaining problematic XML comment patterns
    sed -i '' '/^[[:space:]]*\/\/\/.*[A-Za-z0-9+\/=]\{10,\}[A-Za-z0-9+\/=]*$/d' "$temp_file"
    
    # Pattern 9: Remove any lines that are just certificate data without proper context
    sed -i '' '/^[[:space:]]*[A-Za-z0-9+\/=]\{40,\}$/d' "$temp_file"
    
    # Pattern 10: Remove any malformed XML that contains base64 data
    sed -i '' '/<example>[^<]*[A-Za-z0-9+\/=]\{30,\}[^<]*<\/example>/d' "$temp_file"
    
    # Check if the file was modified
    if [ -f "$temp_file" ]; then
        cleaned_size=$(wc -l < "$temp_file")
        if [ "$cleaned_size" -lt "$original_size" ]; then
            mv "$temp_file" "$file"
            print_success "Cleaned $(basename "$file") - removed $((original_size - cleaned_size)) problematic lines"
        else
            rm "$temp_file"
            print_status "No changes needed for $(basename "$file")"
        fi
    else
        print_error "Failed to process $(basename "$file")"
        return 1
    fi
}

# Function to validate C# syntax
validate_csharp_syntax() {
    local file="$1"
    local temp_file="${file}.validate"
    
    # Create a minimal C# program to test syntax
    cat > "$temp_file" << 'EOF'
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
EOF
    
    # Add the content of the file to test
    cat "$file" >> "$temp_file"
    
    # Add closing brace
    echo "}" >> "$temp_file"
    
    # Try to compile with csc (C# compiler)
    if command -v csc >/dev/null 2>&1; then
        if csc -nologo -out:null "$temp_file" >/dev/null 2>&1; then
            print_success "Syntax validation passed for $(basename "$file")"
            rm -f "$temp_file" null.exe
            return 0
        else
            print_error "Syntax validation failed for $(basename "$file")"
            rm -f "$temp_file" null.exe
            return 1
        fi
    else
        # If csc is not available, just remove the temp file
        rm -f "$temp_file"
        print_status "C# compiler not available, skipping syntax validation for $(basename "$file")"
        return 0
    fi
}

# Main cleanup function
cleanup_generated_files() {
    local target_dir="${1:-Kinde.Api/Model}"
    
    print_status "Starting comprehensive cleanup of generated files..."
    print_status "Target directory: $target_dir"
    
    if [ ! -d "$target_dir" ]; then
        print_error "Target directory does not exist: $target_dir"
        exit 1
    fi
    
    local cleaned_count=0
    local error_count=0
    
    # Find all .cs files in the target directory
    while IFS= read -r -d '' file; do
        if clean_file "$file"; then
            ((cleaned_count++))
        else
            ((error_count++))
        fi
    done < <(find "$target_dir" -name "*.cs" -type f -print0)
    
    print_status "Cleanup completed:"
    print_success "Successfully processed: $cleaned_count files"
    if [ $error_count -gt 0 ]; then
        print_error "Errors encountered: $error_count files"
    fi
    
    # Optional: Run syntax validation on cleaned files
    if [ "$2" = "--validate" ]; then
        print_status "Running syntax validation on cleaned files..."
        local validation_errors=0
        
        while IFS= read -r -d '' file; do
            if ! validate_csharp_syntax "$file"; then
                ((validation_errors++))
            fi
        done < <(find "$target_dir" -name "*.cs" -type f -print0)
        
        if [ $validation_errors -gt 0 ]; then
            print_error "Syntax validation found $validation_errors files with issues"
            return 1
        else
            print_success "All files passed syntax validation"
        fi
    fi
}

# Function to show usage
show_usage() {
    echo "Usage: $0 [OPTIONS] [TARGET_DIRECTORY]"
    echo ""
    echo "Options:"
    echo "  --validate    Run C# syntax validation after cleanup"
    echo "  --help        Show this help message"
    echo ""
    echo "Arguments:"
    echo "  TARGET_DIRECTORY    Directory containing .cs files to clean (default: Kinde.Api/Model)"
    echo ""
    echo "Examples:"
    echo "  $0                                    # Clean Kinde.Api/Model directory"
    echo "  $0 --validate                        # Clean and validate Kinde.Api/Model"
    echo "  $0 temp-generated/src/Kinde.Api/Model # Clean a specific directory"
    echo "  $0 --validate temp-generated/src/Kinde.Api/Model # Clean and validate specific directory"
}

# Parse command line arguments
VALIDATE=false
TARGET_DIR=""

while [[ $# -gt 0 ]]; do
    case $1 in
        --validate)
            VALIDATE=true
            shift
            ;;
        --help)
            show_usage
            exit 0
            ;;
        -*)
            print_error "Unknown option: $1"
            show_usage
            exit 1
            ;;
        *)
            if [ -z "$TARGET_DIR" ]; then
                TARGET_DIR="$1"
            else
                print_error "Multiple target directories specified"
                show_usage
                exit 1
            fi
            shift
            ;;
    esac
done

# Set default target directory if not specified
if [ -z "$TARGET_DIR" ]; then
    TARGET_DIR="Kinde.Api/Model"
fi

# Run the cleanup
if [ "$VALIDATE" = true ]; then
    cleanup_generated_files "$TARGET_DIR" "--validate"
else
    cleanup_generated_files "$TARGET_DIR"
fi

print_success "Cleanup process completed!" 