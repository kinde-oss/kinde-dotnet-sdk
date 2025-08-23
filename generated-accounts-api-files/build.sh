#!/bin/bash
set -e

echo "Building Kinde.Accounts project..."

# Navigate to the src directory
cd src

# Build the project
dotnet build Kinde.Accounts/Kinde.Accounts.csproj --configuration Release

echo "Build completed successfully!"
echo "The DLL can be found in: src/Kinde.Accounts/bin/Release/net9.0/Kinde.Accounts.dll"
