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
ADDL_PROPS="${ADDL_PROPS:-packageName=Kinde.Accounts,packageVersion=1.0.0,targetFramework=net7.0,nullableReferenceTypes=true,useDateTimeOffset=true,useCollection=false,library=restsharp}"

echo "Generating Accounts API client code..."

# Preflight checks
command -v java >/dev/null 2>&1 || { echo "Error: Java is required but not found on PATH." >&2; exit 1; }
command -v curl >/dev/null 2>&1 || { echo "Error: curl is required but not found on PATH." >&2; exit 1; }
[ -f "${SPEC_PATH}" ] || { echo "Error: OpenAPI spec not found at ${SPEC_PATH}." >&2; exit 1; }

# Require Java 11+
JAVA_VERSION_RAW="$(java -version 2>&1 | awk -F\" '/version/ {print $2}')"
if [[ "${JAVA_VERSION_RAW}" == 1.* ]]; then
  JAVA_MAJOR="${JAVA_VERSION_RAW#1.}"; JAVA_MAJOR="${JAVA_MAJOR%%.*}"
else
  JAVA_MAJOR="${JAVA_VERSION_RAW%%.*}"
fi
if ! [[ "${JAVA_MAJOR}" =~ ^[0-9]+$ ]] || (( JAVA_MAJOR < 11 )); then
  echo "Error: Java 11+ is required (detected: ${JAVA_VERSION_RAW})." >&2
  exit 1
fi

# Download OpenAPI Generator JAR if not present (with integrity check)
if [ ! -f "${OPENAPI_JAR}" ]; then
  echo "Downloading OpenAPI Generator v${OPENAPI_GENERATOR_VERSION}..."
  JAR_URL="https://repo1.maven.org/maven2/org/openapitools/openapi-generator-cli/${OPENAPI_GENERATOR_VERSION}/${OPENAPI_GENERATOR_JAR}"
  SHA_URL="${JAR_URL}.sha256"
  TMP_JAR="$(mktemp "${OPENAPI_JAR}.XXXX")"
  TMP_SHA="$(mktemp "${OPENAPI_JAR}.sha256.XXXX")"

  # Fetch jar and checksum (fail on HTTP errors, follow redirects)
  curl -fsSL "${JAR_URL}" -o "${TMP_JAR}"
  if curl -fsSL "${SHA_URL}" -o "${TMP_SHA}"; then
    if command -v sha256sum >/dev/null 2>&1; then
      echo "$(cat "${TMP_SHA}")  ${TMP_JAR}" | sha256sum -c -
    elif command -v shasum >/dev/null 2>&1; then
      echo "$(cat "${TMP_SHA}")  ${TMP_JAR}" | shasum -a 256 -c -
    else
      echo "Warning: sha256 tools not found; skipping checksum verification." >&2
    fi
  else
    echo "Warning: Unable to retrieve checksum; proceeding without verification." >&2
  fi
  mv -f "${TMP_JAR}" "${OPENAPI_JAR}"
  rm -f "${TMP_SHA}"
fi

# Create output directory
mkdir -p "${OUT_DIR}"

# Generate code using OpenAPI generator
java -jar "${OPENAPI_JAR}" generate \
  -i "${SPEC_PATH}" \
  -g csharp \
  -o "${OUT_DIR}" \
  --additional-properties="${ADDL_PROPS}" \
  --template-dir="${REPO_ROOT}/templates/csharp"

echo "Accounts API client code generated successfully: ${OUT_DIR}"
