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
