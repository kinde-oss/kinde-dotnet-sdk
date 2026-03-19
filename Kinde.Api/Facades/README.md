# Kiota Facade Layer

This directory contains the infrastructure for wrapping Kiota-generated clients with facades that maintain backward compatibility with the existing OpenAPI-generated API interfaces.

## Architecture

```
Consumer Code
     │
     ▼
┌─────────────────────────┐
│  OpenAPI API Classes    │  ← Existing interfaces (IUsersApi, IOrganizationsApi, etc.)
│  (Kinde.Api.Api/*.cs)   │
└─────────────────────────┘
     │
     ▼
┌─────────────────────────┐
│  KiotaClientFactory     │  ← Creates Kiota clients with proper auth
└─────────────────────────┘
     │
     ▼
┌─────────────────────────┐
│  AutoMapper             │  ← Translates between OpenAPI and Kiota models
│  (Kinde.Api.Mappers)    │
└─────────────────────────┘
     │
     ▼
┌─────────────────────────┐
│  Kiota Clients          │  ← KindeManagementClient, KindeAccountsClient
│  (Kinde.Api.Kiota)      │
└─────────────────────────┘
```

## Key Components

### KiotaClientFactory

Creates properly configured Kiota clients with:
- Bearer token authentication
- AutoMapper instance for model translation
- Base URL configuration

### Model Mapping

All model translation is handled by AutoMapper profiles in `Kinde.Api/Mappers/`:
- `ManagementApiMapperProfile.cs` - Maps between OpenAPI and Kiota Management API models
- `AccountsApiMapperProfile.cs` - Maps between OpenAPI and Kiota Accounts API models

### Facade Pattern Example

Each API class wraps Kiota calls:

```csharp
public partial class UsersApi : IUsersApi
{
    private KindeManagementClient _kiotaClient;
    private IMapper _mapper;

    // Async method (primary implementation)
    public async Task<CreateUserResponse> CreateUserAsync(
        CreateUserRequest createUserRequest = default,
        CancellationToken cancellationToken = default)
    {
        // 1. Map OpenAPI request → Kiota request
        var kiotaRequest = _mapper.Map<KiotaModels.CreateUserRequest>(createUserRequest);
        
        // 2. Call Kiota client
        var kiotaResponse = await _kiotaClient.Api.V1.Users.PostAsync(kiotaRequest, cancellationToken);
        
        // 3. Map Kiota response → OpenAPI response
        return _mapper.Map<CreateUserResponse>(kiotaResponse);
    }

    // Sync method (deprecated wrapper)
    [Obsolete("Use CreateUserAsync instead. Sync methods will be removed in v3.0")]
    public CreateUserResponse CreateUser(CreateUserRequest createUserRequest = default)
    {
        return CreateUserAsync(createUserRequest).GetAwaiter().GetResult();
    }
}
```

## Migration Status

- [x] Kiota generation infrastructure (`scripts/generate-kiota-clients.sh`)
- [x] AutoMapper profiles for model translation
- [x] KiotaClientFactory for client creation
- [ ] 25 Management API facades (in progress)
- [ ] 8 Accounts API facades (pending)

## Direct Kiota Usage

Consumers who want to use Kiota directly can do so:

```csharp
using Kinde.Api.Kiota.Management;
using Microsoft.Kiota.Http.HttpClientLibrary;

var authProvider = new BaseBearerTokenAuthenticationProvider(tokenProvider);
var adapter = new HttpClientRequestAdapter(authProvider);
adapter.BaseUrl = "https://your-subdomain.kinde.com";

var client = new KindeManagementClient(adapter);
var users = await client.Api.V1.Users.GetAsync();
```

## Versioning Strategy

- v2.1.0: Kiota backend with facade layer (current)
- v3.0.0: Remove sync methods, potentially expose Kiota models directly

