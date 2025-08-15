#!/bin/bash

# Generate Accounts API client code
echo "Generating Accounts API client code..."

# Create output directory
mkdir -p generated-accounts-api-files

# Generate code using OpenAPI generator
java -jar openapi-generator-cli.jar generate \
  -i api/kinde-accounts-api.yaml \
  -g csharp \
  -o generated-accounts-api-files \
  --additional-properties=packageName=Kinde.Accounts,packageVersion=1.0.0,targetFramework=net6.0,nullableReferenceTypes=true,useDateTimeOffset=true,useCollection=false

echo "Accounts API client code generated successfully!"
