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
│  Kinde model mapper     │  ← Translates between OpenAPI and Kiota models
│  (Kinde.Api.Mapping)    │
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
- The model mapper for OpenAPI/Kiota translation
- Base URL configuration

### Model Mapping

All model translation is handled by the SDK's own mapper in `Kinde.Api/Mapping/`, reached through
`KindeMapperConfiguration.Mapper`:

- `KindeMapper.cs` - Matches properties by name, recurses into nested models and collections, and
  caches a mapping plan per type pair. Both generators produce the same PascalCase names from one
  OpenAPI description, so this covers the great majority of mappings and keeps working when the
  Kiota models are regenerated.
- `KindeManagementMappings.cs` / `KindeAccountsMappings.cs` - The pairs where the two shapes
  genuinely differ: `oneOf` wrappers, dynamically keyed `additionalProperties` objects, and enums
  whose wire names the generators spell differently.
- `KindeValueConverter.cs` - Scalar conversion: strings, primitives, enums and dates.

When the destination is a Kiota model, null source values are skipped rather than assigned. Kiota
initialises some properties with the schema's default, and assigning null erases it: the property
is then dropped from the payload rather than carrying the default the caller relied on. For
example, leaving `ReplaceMFARequest.IsRecoveryCodesEnabled` unset should send
`"is_recovery_codes_enabled": true`; assigning null sends nothing at all.

Assigning null to a property with no default is harmless, because Kiota omits it either way.

> The SDK previously used AutoMapper here. It no longer takes that dependency, and
> `KindeMapperConfiguration.Mapper` returns `IKindeMapper` rather than AutoMapper's `IMapper`. The
> `Map<TDestination>(object)` signature is unchanged, so call sites are unaffected.

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
- [x] Model mapping layer (`Kinde.Api/Mapping/`)
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

