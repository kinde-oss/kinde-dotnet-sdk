#!/bin/bash
# Create Release Script for Kinde .NET SDK
# This script helps create a new release by creating a git tag

set -e
set -o pipefail


# Colors for output
RED='\033[0;31m'
GREEN='\033[0;32m'
YELLOW='\033[1;33m'
NC='\033[0m' # No Color

# Function to print colored output
print_status() {
    echo -e "${GREEN}[INFO]${NC} $1"
}

print_warning() {
    echo -e "${YELLOW}[WARNING]${NC} $1"
}

print_error() {
    echo -e "${RED}[ERROR]${NC} $1"
}

# Check if we're in a git repository
if ! git rev-parse --git-dir > /dev/null 2>&1; then
    print_error "Not in a git repository"
    exit 1
fi

# Ensure we're on main branch
CURRENT_BRANCH=$(git rev-parse --abbrev-ref HEAD)
if [ "$CURRENT_BRANCH" != "main" ]; then
    print_error "This script must be run from the main branch (currently on: $CURRENT_BRANCH)"
    exit 1
fi

# Get current version from csproj file
CURRENT_VERSION=$(grep -o '<Version>.*</Version>' Kinde.Api/Kinde.Api.csproj | sed 's/<Version>\(.*\)<\/Version>/\1/')

if [ -z "$CURRENT_VERSION" ]; then
    print_error "Could not extract version from Kinde.Api/Kinde.Api.csproj"
    exit 1
fi

print_status "Current version: $CURRENT_VERSION"

# Check if version parameter is provided
if [ $# -eq 0 ]; then
    print_error "Please provide a version number (e.g., 1.3.2)"
    echo "Usage: $0 <version>"
    echo "Example: $0 1.3.2"
    exit 1
fi

NEW_VERSION=$1

# Validate version format (simple check)
if ! [[ $NEW_VERSION =~ ^[0-9]+\.[0-9]+\.[0-9]+$ ]]; then
    print_error "Invalid version format. Please use semantic versioning (e.g., 1.3.2)"
    exit 1
fi

print_status "Creating release for version: $NEW_VERSION"

# Check if tag already exists
if git tag -l | grep -q "v$NEW_VERSION"; then
    print_error "Tag v$NEW_VERSION already exists"
    exit 1
fi

# Update version in csproj file
print_status "Updating version in Kinde.Api.csproj..."
sed -i.bak "s/<Version>.*<\/Version>/<Version>$NEW_VERSION<\/Version>/" Kinde.Api/Kinde.Api.csproj
rm Kinde.Api/Kinde.Api.csproj.bak

# Verify the update succeeded
UPDATED_VERSION=$(grep -o '<Version>.*</Version>' Kinde.Api/Kinde.Api.csproj | sed 's/<Version>\(.*\)<\/Version>/\1/')
if [ "$UPDATED_VERSION" != "$NEW_VERSION" ]; then
    print_error "Failed to update version in Kinde.Api.csproj"
    exit 1
fi

# Restore packages using lock file
print_status "Restoring packages..."
dotnet restore --locked-mode

# Build and test the project
print_status "Building project..."
dotnet build --configuration Release --no-restore

print_status "Running tests..."
dotnet test --configuration Release --no-restore

# Create NuGet package
print_status "Creating NuGet package..."
dotnet pack Kinde.Api/Kinde.Api.csproj --configuration Release --output ./nupkgs --no-build

# Commit version change
print_status "Committing version change..."
git add Kinde.Api/Kinde.Api.csproj Kinde.Api/packages.lock.json Kinde.Api.Test/packages.lock.json
git commit -m "Bump version to $NEW_VERSION"

# Create and push tag
print_status "Creating tag v$NEW_VERSION..."
git tag -a "v$NEW_VERSION" -m "Release version $NEW_VERSION"

print_status "Pushing changes and tag..."
git push origin main
git push origin "v$NEW_VERSION"

print_status "Release created successfully!"
print_status "Tag: v$NEW_VERSION"
print_status "NuGet package created in: ./nupkgs/"
print_status "GitHub Actions will now create a release and publish to NuGet.org"

# Show next steps
echo ""
print_warning "Next steps:"
echo "1. Wait for GitHub Actions to complete the release process"
echo "2. Verify the release on GitHub: https://github.com/kinde-oss/kinde-dotnet-sdk/releases"
echo "3. Verify the package on NuGet: https://www.nuget.org/packages/Kinde.SDK"
