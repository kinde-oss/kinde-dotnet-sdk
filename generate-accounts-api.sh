#!/usr/bin/env bash
set -euo pipefail

# Resolve paths relative to this script
SCRIPT_DIR="$(cd "$(dirname "${BASH_SOURCE[0]}")" && pwd)"
REPO_ROOT="${SCRIPT_DIR}"

# Pin OpenAPI Generator version for consistency
OPENAPI_GENERATOR_VERSION=7.0.1
OPENAPI_GENERATOR_JAR="openapi-generator-cli-${OPENAPI_GENERATOR_VERSION}.jar"

SPEC_PATH="${1:-${REPO_ROOT}/api/kinde-accounts-api.yaml}"
OUT_DIR="${OUT_DIR:-${REPO_ROOT}/generated-accounts-api-files}"
OPENAPI_JAR="${OPENAPI_JAR:-${REPO_ROOT}/${OPENAPI_GENERATOR_JAR}}"
ADDL_PROPS="${ADDL_PROPS:-packageName=Kinde.Accounts,packageVersion=1.0.0,targetFramework=net6.0,nullableReferenceTypes=true,useDateTimeOffset=true,useCollection=false}"

echo "Generating Accounts API client code..."

# Preflight checks
command -v java >/dev/null 2>&1 || { echo "Error: Java is required but not found on PATH." >&2; exit 1; }
[ -f "${SPEC_PATH}" ] || { echo "Error: OpenAPI spec not found at ${SPEC_PATH}." >&2; exit 1; }

# Download OpenAPI Generator JAR if not present
if [ ! -f "${OPENAPI_JAR}" ]; then
  echo "Downloading OpenAPI Generator v${OPENAPI_GENERATOR_VERSION}..."
  curl -L "https://repo1.maven.org/maven2/org/openapitools/openapi-generator-cli/${OPENAPI_GENERATOR_VERSION}/${OPENAPI_GENERATOR_JAR}" -o "${OPENAPI_JAR}"
fi

# Create output directory
mkdir -p "${OUT_DIR}"

# Generate code using OpenAPI generator
java -jar "${OPENAPI_JAR}" generate \
  -i "${SPEC_PATH}" \
  -g csharp \
  -o "${OUT_DIR}" \
  --additional-properties="${ADDL_PROPS}"

echo "Accounts API client code generated successfully: ${OUT_DIR}"
