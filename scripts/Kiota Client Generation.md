# Kiota CLI – Generate C# API Client from OpenAPI YAML

---

## 1. Install Kiota CLI

Install Kiota as a global .NET tool:

```bash
dotnet tool install --global Microsoft.OpenApi.Kiota
```

Verify the installation:

```bash
kiota --version
```

---

## 2. Generate C# Client from OpenAPI YAML

Navigate to the directory containing your OpenAPI YAML file:

```bash
cd path/to/your/yaml
```

Generate the C# client:

```bash
kiota generate \
  --language CSharp \
  --openapi your-api.yaml \
  --output ./GeneratedClient \
  --namespace-name MyCompany.ApiClient
```

| Flag | Description |
|------|-------------|
| `--language` | Target programming language |
| `--openapi` | Path to your OpenAPI YAML file |
| `--output` | Folder where the client will be generated |
| `--namespace-name` | Namespace used in generated C# files |

---

## 3. Create a Class Library Project

Create a new .NET class library to hold the generated client:

```bash
dotnet new classlib -n MyCompany.ApiClient
```

> Move the generated files into this project directory if you generated them outside the project folder.

---

## 4. Install Required NuGet Packages

Add the core Kiota dependencies required by the generated client:

```bash
dotnet add package Microsoft.Kiota.Abstractions
dotnet add package Microsoft.Kiota.Http.HttpClientLibrary
dotnet add package Microsoft.Kiota.Serialization.Json
dotnet add package Microsoft.Kiota.Serialization.Text
```

**Optional:** Install Azure authentication support (only required if your API uses Azure AD authentication):

```bash
dotnet add package Microsoft.Kiota.Authentication.Azure
```

---

## 5. Use the Generated Client

Example usage in your application code:

```csharp
using Microsoft.Kiota.Http.HttpClientLibrary;
using MyCompany.ApiClient;

// Create HTTP adapter (Anonymous provider used here)
var adapter = new HttpClientRequestAdapter(new AnonymousAuthenticationProvider());

// Initialize the generated API client
var client = new ApiClient(adapter);

// Call an endpoint (example: GET /users)
var users = await client.Api.V1.Apis.GetAsync();
```

---

## 6. Regenerate Client After YAML Changes

Whenever the OpenAPI YAML file is updated, regenerate the client to keep it in sync.

```bash
kiota generate \
  --language CSharp \
  --openapi your-api.yaml \
  --output ./GeneratedClient \
  --namespace-name MyCompany.ApiClient \
  --clean-output
```

> `--clean-output` removes old generated files to prevent stale or conflicting code.


---

## 7. Regenerating the SDK from the hosted specs

Always generate from the hosted specs, never from a copy in the repo. `api/openapi.yaml` is only a reference copy of
`https://api-spec.kinde.com/kinde-management-api-spec.yaml`; refresh it from that URL when you regenerate.

| What | How | Output |
|------|-----|--------|
| Kiota clients | `./generate-all-apis.sh` (or `scripts/generate-kiota-clients.sh --management` / `--accounts`) | `Kinde.Api/Kiota/Management`, `Kinde.Api/Kiota/Accounts` |
| OpenAPI models | `./generate-api-backup.sh` | `Kinde.Api/Model` (the API classes are written to `generated-api-files/` for inspection only) |

`kiota-lock.json` records the spec URL a client was generated from; check that it is the hosted URL.

The scripts regenerate the clients, but several things in the SDK are maintained by hand and are not reproduced by them.
After regenerating, review each of these:

- **Wrapper classes (`Kinde.Api/Api`)** wrap the Kiota clients and map with `IKindeMapper`. New operations in the spec need
  a wrapper method added by hand, following an existing one.
- **Nullable request flags.** Optional `bool`/`int` properties on request models were made nullable with
  `EmitDefaultValue = false` by hand (so an unset flag is not sent as `false`). Raw generator output reverts them.
- **Explicit mapper conversions** (`Kinde.Api/Mapping/KindeManagementMappings.cs`): the converters, including the `oneOf`
  connection-option switches, must follow the order of the variants in the spec. A renumbered variant compiles but throws
  at runtime, so the tests pin each one.
- **`ManagementMappingManifest`** in the tests lists every Kiota/SDK type pair; add the pairs for new types.
- **Spec gaps.** `GET /connections` and its organization and application variants return flat items, but the hosted spec types
  them as the wrapped `connection`. The mapper reads the flat fields from `AdditionalData`. If the spec is fixed, this can go.
