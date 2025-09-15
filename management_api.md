# Kinde .NET SDK - Management API Documentation

## Overview

The Kinde .NET SDK provides comprehensive access to the Kinde Management API, allowing you to manage users, organizations, permissions, roles, and other resources programmatically. This document covers the full usage of the Management API and the custom serialization functionality that has been implemented to handle enum types correctly.

## Table of Contents

1. [Getting Started](#getting-started)
2. [Authentication](#authentication)
3. [API Client Configuration](#api-client-configuration)
4. [User Management](#user-management)
5. [Identity Management](#identity-management)
6. [Organization Management](#organization-management)
7. [Permission and Role Management](#permission-and-role-management)
8. [Custom Serialization](#custom-serialization)
9. [Error Handling](#error-handling)
10. [Best Practices](#best-practices)
11. [Examples](#examples)

## Getting Started

### Prerequisites

- .NET 6.0 or later
- Kinde account with Management API access
- Client credentials (Client ID and Client Secret)

### Installation

The Kinde .NET SDK is available as a NuGet package:

```bash
dotnet add package Kinde.Api
```

### Basic Setup

```csharp
using Kinde.Api;
using Kinde.Api.Client;
using Kinde.Api.Model;

// Configure the API client
var configuration = new Configuration
{
    BasePath = "https://your-domain.kinde.com",
    AccessToken = "your-access-token"
};

// Create API instances
var usersApi = new UsersApi(configuration);
var organizationsApi = new OrganizationsApi(configuration);
var permissionsApi = new PermissionsApi(configuration);
```

### Configuration Mapping

The `KindeManagementApiConfiguration` object (used in the dotnet-starter-kit) maps to the `Configuration` object as follows:

```csharp
// KindeManagementApiConfiguration (from appsettings.json)
public class KindeManagementApiConfiguration
{
    public string Domain { get; set; } = string.Empty;           // Maps to BasePath
    public string ClientId { get; set; } = string.Empty;         // Used for authentication
    public string ClientSecret { get; set; } = string.Empty;     // Used for authentication
    public string Scope { get; set; } = "create:users create:user_identities";
    public string Audience { get; set; } = string.Empty;         // Used for authentication
}

// Configuration object (for API client)
var configuration = new Configuration
{
    BasePath = _managementApiConfig.Domain,    // Domain from KindeManagementApiConfiguration
    AccessToken = accessToken                  // Obtained via client credentials flow
};
```

**Key Points:**
- `KindeManagementApiConfiguration.Domain` → `Configuration.BasePath`
- `KindeManagementApiConfiguration.ClientId`, `ClientSecret`, `Scope`, `Audience` → Used for OAuth 2.0 client credentials flow to obtain `AccessToken`
- The `AccessToken` is then used in the `Configuration` object for API calls

### Complete Configuration Flow Example

Here's how the complete flow works from configuration to API client:

```csharp
public class UserManagementService
{
    private readonly KindeManagementApiConfiguration _config;
    private readonly ILogger<UserManagementService> _logger;

    public UserManagementService(
        IOptions<KindeManagementApiConfiguration> config, 
        ILogger<UserManagementService> logger)
    {
        _config = config.Value;
        _logger = logger;
    }

    private async Task<string?> GetAccessTokenAsync()
    {
        // Step 1: Create application configuration
        var appConfig = new ApplicationConfiguration
        {
            Domain = _config.Domain,
            ForceApi = true // Important for management API
        };

        // Step 2: Create client credentials configuration
        var clientCredentialsConfig = new ClientCredentialsConfiguration(
            _config.ClientId,      // From KindeManagementApiConfiguration
            _config.Scope,         // From KindeManagementApiConfiguration
            _config.ClientSecret,  // From KindeManagementApiConfiguration
            _config.Audience       // From KindeManagementApiConfiguration
        );

        // Step 3: Create client credentials flow
        var clientCredentialsFlow = new ClientCredentialsFlow(appConfig, clientCredentialsConfig);

        // Step 4: Authorize and get token
        using var httpClient = new HttpClient();
        var state = await clientCredentialsFlow.Authorize(httpClient);
        
        if (state == AuthorizationStates.NonAuthorized)
        {
            _logger.LogError("Client credentials authorization failed");
            return null;
        }

        return await clientCredentialsFlow.GetOrRefreshToken(httpClient);
    }

    public async Task<CreateUserResponse> CreateUserAsync(string email, string firstName, string lastName)
    {
        // Step 5: Get access token
        var accessToken = await GetAccessTokenAsync();
        if (string.IsNullOrEmpty(accessToken))
        {
            throw new InvalidOperationException("Failed to obtain access token");
        }

        // Step 6: Create API client configuration
        var configuration = new Configuration
        {
            BasePath = _config.Domain,  // From KindeManagementApiConfiguration
            AccessToken = accessToken   // From OAuth 2.0 flow
        };

        // Step 7: Create API client and make calls
        var usersApi = new UsersApi(configuration);
        
        var createUserRequest = new CreateUserRequest
        {
            Profile = new CreateUserRequestProfile
            {
                GivenName = firstName,
                FamilyName = lastName
            },
            Identities = new List<CreateUserRequestIdentitiesInner>
            {
                new CreateUserRequestIdentitiesInner
                {
                    Type = CreateUserRequestIdentitiesInner.TypeEnum.Email,
                    Details = new CreateUserRequestIdentitiesInnerDetails
                    {
                        Email = email
                    }
                }
            }
        };

        return await usersApi.CreateUserAsync(createUserRequest);
    }
}
```

### Configuration in appsettings.json

The `KindeManagementApiConfiguration` is typically configured in `appsettings.json`:

```json
{
  "KindeManagementApiConfiguration": {
    "Domain": "https://your-domain.kinde.com",
    "ClientId": "your-client-id",
    "ClientSecret": "your-client-secret",
    "Scope": "create:users create:user_identities read:users update:users delete:users",
    "Audience": "https://your-domain.kinde.com/api"
  }
}
```

And registered in `Program.cs`:

```csharp
builder.Services.Configure<KindeManagementApiConfiguration>(
    builder.Configuration.GetSection("KindeManagementApiConfiguration"));
```

## Authentication

### Client Credentials Flow

The Management API uses the OAuth 2.0 Client Credentials flow for authentication:

```csharp
public async Task<string> GetManagementApiAccessTokenAsync()
{
    var client = new HttpClient();
    
    var request = new HttpRequestMessage(HttpMethod.Post, "https://your-domain.kinde.com/oauth2/token")
    {
        Content = new FormUrlEncodedContent(new[]
        {
            new KeyValuePair<string, string>("grant_type", "client_credentials"),
            new KeyValuePair<string, string>("client_id", "your-client-id"),
            new KeyValuePair<string, string>("client_secret", "your-client-secret"),
            new KeyValuePair<string, string>("scope", "create:users create:user_identities read:users update:users delete:users"),
            new KeyValuePair<string, string>("audience", "https://your-domain.kinde.com/api")
        })
    };

    var response = await client.SendAsync(request);
    var content = await response.Content.ReadAsStringAsync();
    
    var tokenResponse = JsonSerializer.Deserialize<JsonElement>(content);
    return tokenResponse.GetProperty("access_token").GetString();
}
```

### Required Scopes

Common scopes for Management API operations:

- `create:users` - Create new users
- `read:users` - Read user information
- `update:users` - Update user information
- `delete:users` - Delete users
- `create:user_identities` - Create user identities
- `read:user_identities` - Read user identities
- `update:user_identities` - Update user identities
- `delete:user_identities` - Delete user identities
- `create:organizations` - Create organizations
- `read:organizations` - Read organization information
- `update:organizations` - Update organization information
- `delete:organizations` - Delete organizations

## API Client Configuration

### Configuration Options

```csharp
var configuration = new Configuration
{
    BasePath = "https://your-domain.kinde.com",
    AccessToken = accessToken,
    Timeout = 30000, // 30 seconds
    UserAgent = "MyApp/1.0.0"
};
```

### Custom HTTP Client

```csharp
var httpClient = new HttpClient();
httpClient.DefaultRequestHeaders.Add("User-Agent", "MyApp/1.0.0");

var configuration = new Configuration
{
    BasePath = "https://your-domain.kinde.com",
    AccessToken = accessToken,
    HttpClient = httpClient
};
```

## User Management

### Creating Users

```csharp
public async Task<CreateUserResponse> CreateUserAsync(string email, string firstName, string lastName)
{
    var createUserRequest = new CreateUserRequest
    {
        Profile = new CreateUserRequestProfile
        {
            GivenName = firstName,
            FamilyName = lastName
        },
        Identities = new List<CreateUserRequestIdentitiesInner>
        {
            new CreateUserRequestIdentitiesInner
            {
                Type = CreateUserRequestIdentitiesInner.TypeEnum.Email,
                Details = new CreateUserRequestIdentitiesInnerDetails
                {
                    Email = email
                }
            }
        }
    };

    return await usersApi.CreateUserAsync(createUserRequest);
}
```

### Reading Users

```csharp
// Get user by ID
var user = await usersApi.GetUserAsync(userId);

// Get users with pagination
var users = await usersApi.GetUsersAsync(
    pageSize: 10,
    nextToken: null,
    sort: UsersApi.SortEnum.NameAsc
);

// Search users
var searchResults = await usersApi.GetUsersAsync(
    query: "john@example.com",
    pageSize: 10
);
```

### Updating Users

```csharp
public async Task<User> UpdateUserAsync(string userId, string? firstName = null, string? lastName = null)
{
    var updateUserRequest = new UpdateUserRequest();
    
    if (firstName != null)
        updateUserRequest.GivenName = firstName;
    
    if (lastName != null)
        updateUserRequest.FamilyName = lastName;

    return await usersApi.UpdateUserAsync(userId, updateUserRequest);
}
```

### Deleting Users

```csharp
await usersApi.DeleteUserAsync(userId);
```

## Identity Management

### Creating User Identities

```csharp
public async Task<CreateUserIdentityResponse> CreateUserIdentityAsync(
    string userId, 
    string identityType, 
    string value, 
    string? phoneCountryCode = null)
{
    var createIdentityRequest = new CreateUserIdentityRequest
    {
        Type = Enum.Parse<CreateUserIdentityRequest.TypeEnum>(identityType, true),
        Value = value,
        PhoneCountryId = phoneCountryCode
    };

    return await usersApi.CreateUserIdentityAsync(userId, createIdentityRequest);
}

// Usage examples
await CreateUserIdentityAsync(userId, "email", "user@example.com");
await CreateUserIdentityAsync(userId, "phone", "+1234567890", "US");
await CreateUserIdentityAsync(userId, "username", "johndoe");
```

### Reading User Identities

```csharp
var identities = await usersApi.GetUserIdentitiesAsync(userId);
```

### Updating User Identities

```csharp
public async Task<UserIdentity> UpdateUserIdentityAsync(
    string userId, 
    string identityId, 
    string? value = null, 
    bool? isVerified = null)
{
    var updateRequest = new UpdateUserIdentityRequest();
    
    if (value != null)
        updateRequest.Value = value;
    
    if (isVerified.HasValue)
        updateRequest.IsVerified = isVerified;

    return await usersApi.UpdateUserIdentityAsync(userId, identityId, updateRequest);
}
```

### Deleting User Identities

```csharp
await usersApi.DeleteUserIdentityAsync(userId, identityId);
```

## Organization Management

### Creating Organizations

```csharp
public async Task<Organization> CreateOrganizationAsync(string name, string? code = null)
{
    var createRequest = new CreateOrganizationRequest
    {
        Name = name,
        Code = code
    };

    return await organizationsApi.CreateOrganizationAsync(createRequest);
}
```

### Reading Organizations

```csharp
// Get organization by ID
var organization = await organizationsApi.GetOrganizationAsync(organizationId);

// Get organizations with pagination
var organizations = await organizationsApi.GetOrganizationsAsync(
    pageSize: 10,
    nextToken: null,
    sort: OrganizationsApi.SortEnum.NameAsc
);
```

### Updating Organizations

```csharp
public async Task<Organization> UpdateOrganizationAsync(
    string organizationId, 
    string? name = null, 
    string? code = null)
{
    var updateRequest = new UpdateOrganizationRequest();
    
    if (name != null)
        updateRequest.Name = name;
    
    if (code != null)
        updateRequest.Code = code;

    return await organizationsApi.UpdateOrganizationAsync(organizationId, updateRequest);
}
```

## Permission and Role Management

### Creating Permissions

```csharp
public async Task<Permission> CreatePermissionAsync(string name, string? description = null)
{
    var createRequest = new CreatePermissionRequest
    {
        Name = name,
        Description = description
    };

    return await permissionsApi.CreatePermissionAsync(createRequest);
}
```

### Creating Roles

```csharp
public async Task<Role> CreateRoleAsync(string name, string? description = null)
{
    var createRequest = new CreateRoleRequest
    {
        Name = name,
        Description = description
    };

    return await rolesApi.CreateRoleAsync(createRequest);
}
```

### Assigning Permissions to Roles

```csharp
public async Task<Role> AssignPermissionToRoleAsync(string roleId, string permissionId)
{
    var assignRequest = new AssignPermissionToRoleRequest
    {
        PermissionId = permissionId
    };

    return await rolesApi.AssignPermissionToRoleAsync(roleId, assignRequest);
}
```

### Assigning Roles to Users

```csharp
public async Task<User> AssignRoleToUserAsync(string userId, string roleId)
{
    var assignRequest = new AssignRoleToUserRequest
    {
        RoleId = roleId
    };

    return await usersApi.AssignRoleToUserAsync(userId, assignRequest);
}
```

## Custom Serialization

### Overview

The Kinde .NET SDK includes custom serialization functionality to handle enum types correctly. This ensures that enum values are serialized in the correct format expected by the Kinde API (lowercase) rather than the default .NET enum serialization (PascalCase).

### Custom Enum Converters

The SDK includes two custom enum converters:

1. **GenericEnumConverter** - For System.Text.Json serialization
2. **NewtonsoftGenericEnumConverter** - For Newtonsoft.Json serialization

### How It Works

The custom converters use reflection to call the `TypeEnumToJsonValue` and `TypeEnumFromString` methods that are generated for each enum type. These methods handle the conversion between .NET enum values and their JSON string representations.

### Example: Identity Type Enum

```csharp
// The enum is defined as:
public enum TypeEnum
{
    Email,
    Phone,
    Username
}

// But serializes to JSON as:
{
    "type": "email"  // lowercase, not "Email"
}

// The custom converter handles this conversion automatically
```

### Supported Enum Types

The custom serialization works with all enum types in the SDK, including:

- **Identity Types**: `email`, `phone`, `username`
- **User Status**: `active`, `inactive`
- **Organization Status**: `active`, `inactive`
- **Permission Types**: `read`, `write`, `delete`
- **Role Types**: `admin`, `user`, `guest`

### Configuration

The custom converters are automatically configured in the API client:

```csharp
// In ApiClient.cs
public JsonSerializerSettings SerializerSettings { get; set; } = new JsonSerializerSettings
{
    ConstructorHandling = ConstructorHandling.AllowNonPublicDefaultConstructor,
    ContractResolver = new DefaultContractResolver
    {
        NamingStrategy = new CamelCaseNamingStrategy
        {
            OverrideSpecifiedNames = false
        }
    },
    Converters = {
        new Kinde.Api.Converters.NewtonsoftGenericEnumConverter(),
        new Kinde.Api.Converters.CreateUserResponseNewtonsoftConverter()
    }
};
```

### Custom Response Converters

For complex response types that require special handling, custom converters are also provided:

- **CreateUserResponseNewtonsoftConverter** - Handles the deserialization of `CreateUserResponse` with proper handling of the `identities` array

## Error Handling

### Common API Exceptions

```csharp
try
{
    var user = await usersApi.CreateUserAsync(createUserRequest);
}
catch (ApiException ex)
{
    switch (ex.ErrorCode)
    {
        case 400:
            // Bad Request - validation error
            Console.WriteLine($"Validation error: {ex.Message}");
            break;
        case 401:
            // Unauthorized - invalid token
            Console.WriteLine("Authentication failed");
            break;
        case 403:
            // Forbidden - insufficient permissions
            Console.WriteLine("Insufficient permissions");
            break;
        case 404:
            // Not Found - resource doesn't exist
            Console.WriteLine("Resource not found");
            break;
        case 409:
            // Conflict - resource already exists
            Console.WriteLine("Resource already exists");
            break;
        case 429:
            // Too Many Requests - rate limit exceeded
            Console.WriteLine("Rate limit exceeded");
            break;
        default:
            Console.WriteLine($"API error: {ex.Message}");
            break;
    }
}
```

### Specific Error Codes

Common Kinde API error codes:

- `IDENTITY_TYPE_NOT_SUPPORT` - Invalid identity type
- `IDENTITY_TYPE_INVALID` - Invalid identity type format
- `USER_ALREADY_EXISTS` - User with this identity already exists
- `INVALID_EMAIL_FORMAT` - Email format is invalid
- `INVALID_PHONE_FORMAT` - Phone format is invalid
- `ORGANIZATION_NOT_FOUND` - Organization doesn't exist
- `PERMISSION_NOT_FOUND` - Permission doesn't exist
- `ROLE_NOT_FOUND` - Role doesn't exist

### Retry Logic

```csharp
public async Task<T> ExecuteWithRetryAsync<T>(Func<Task<T>> operation, int maxRetries = 3)
{
    for (int i = 0; i < maxRetries; i++)
    {
        try
        {
            return await operation();
        }
        catch (ApiException ex) when (ex.ErrorCode == 429 && i < maxRetries - 1)
        {
            // Rate limit exceeded, wait and retry
            await Task.Delay(TimeSpan.FromSeconds(Math.Pow(2, i))); // Exponential backoff
        }
    }
    
    throw new InvalidOperationException("Max retries exceeded");
}
```

## Best Practices

### 1. Token Management

```csharp
public class TokenManager
{
    private string? _accessToken;
    private DateTime _tokenExpiry;
    private readonly SemaphoreSlim _semaphore = new(1, 1);

    public async Task<string> GetValidTokenAsync()
    {
        await _semaphore.WaitAsync();
        try
        {
            if (_accessToken == null || DateTime.UtcNow >= _tokenExpiry)
            {
                _accessToken = await GetNewTokenAsync();
                _tokenExpiry = DateTime.UtcNow.AddMinutes(55); // Refresh 5 minutes early
            }
            return _accessToken;
        }
        finally
        {
            _semaphore.Release();
        }
    }
}
```

### 2. Configuration Management

```csharp
public class KindeConfiguration
{
    public string Domain { get; set; } = string.Empty;
    public string ClientId { get; set; } = string.Empty;
    public string ClientSecret { get; set; } = string.Empty;
    public string[] Scopes { get; set; } = Array.Empty<string>();
    
    public void Validate()
    {
        if (string.IsNullOrEmpty(Domain))
            throw new ArgumentException("Domain is required");
        if (string.IsNullOrEmpty(ClientId))
            throw new ArgumentException("ClientId is required");
        if (string.IsNullOrEmpty(ClientSecret))
            throw new ArgumentException("ClientSecret is required");
    }
}
```

### 3. Logging

```csharp
public class KindeApiService
{
    private readonly ILogger<KindeApiService> _logger;
    private readonly UsersApi _usersApi;

    public KindeApiService(ILogger<KindeApiService> logger, UsersApi usersApi)
    {
        _logger = logger;
        _usersApi = usersApi;
    }

    public async Task<CreateUserResponse> CreateUserAsync(CreateUserRequest request)
    {
        _logger.LogInformation("Creating user with email: {Email}", 
            request.Identities?.FirstOrDefault()?.Details?.Email);
        
        try
        {
            var result = await _usersApi.CreateUserAsync(request);
            _logger.LogInformation("User created successfully with ID: {UserId}", result.Id);
            return result;
        }
        catch (ApiException ex)
        {
            _logger.LogError(ex, "Failed to create user. Error: {ErrorCode} - {Message}", 
                ex.ErrorCode, ex.Message);
            throw;
        }
    }
}
```

### 4. Validation

```csharp
public class UserRequestValidator
{
    public static ValidationResult ValidateCreateUserRequest(CreateUserRequest request)
    {
        var result = new ValidationResult();

        if (request.Profile == null)
            result.AddError("Profile is required");

        if (string.IsNullOrEmpty(request.Profile?.GivenName))
            result.AddError("Given name is required");

        if (string.IsNullOrEmpty(request.Profile?.FamilyName))
            result.AddError("Family name is required");

        if (request.Identities == null || !request.Identities.Any())
            result.AddError("At least one identity is required");

        foreach (var identity in request.Identities ?? Enumerable.Empty<CreateUserRequestIdentitiesInner>())
        {
            if (identity.Type == CreateUserRequestIdentitiesInner.TypeEnum.Email)
            {
                if (string.IsNullOrEmpty(identity.Details?.Email))
                    result.AddError("Email is required for email identity");
            }
            else if (identity.Type == CreateUserRequestIdentitiesInner.TypeEnum.Phone)
            {
                if (string.IsNullOrEmpty(identity.Details?.Phone))
                    result.AddError("Phone is required for phone identity");
            }
        }

        return result;
    }
}
```

## Examples

### Complete User Management Example

```csharp
public class UserManagementService
{
    private readonly UsersApi _usersApi;
    private readonly ILogger<UserManagementService> _logger;

    public UserManagementService(UsersApi usersApi, ILogger<UserManagementService> logger)
    {
        _usersApi = usersApi;
        _logger = logger;
    }

    public async Task<User> CreateUserWithEmailAsync(string email, string firstName, string lastName)
    {
        var request = new CreateUserRequest
        {
            Profile = new CreateUserRequestProfile
            {
                GivenName = firstName,
                FamilyName = lastName
            },
            Identities = new List<CreateUserRequestIdentitiesInner>
            {
                new CreateUserRequestIdentitiesInner
                {
                    Type = CreateUserRequestIdentitiesInner.TypeEnum.Email,
                    Details = new CreateUserRequestIdentitiesInnerDetails
                    {
                        Email = email
                    }
                }
            }
        };

        var result = await _usersApi.CreateUserAsync(request);
        _logger.LogInformation("User created: {UserId}", result.Id);
        return result;
    }

    public async Task<UserIdentity> AddPhoneIdentityAsync(string userId, string phone, string countryCode)
    {
        var request = new CreateUserIdentityRequest
        {
            Type = CreateUserIdentityRequest.TypeEnum.Phone,
            Value = phone,
            PhoneCountryId = countryCode
        };

        var result = await _usersApi.CreateUserIdentityAsync(userId, request);
        _logger.LogInformation("Phone identity added: {IdentityId}", result.Identity?.Id);
        return result.Identity!;
    }

    public async Task<User> GetUserWithIdentitiesAsync(string userId)
    {
        var user = await _usersApi.GetUserAsync(userId);
        var identities = await _usersApi.GetUserIdentitiesAsync(userId);
        
        _logger.LogInformation("Retrieved user {UserId} with {IdentityCount} identities", 
            userId, identities.Count);
        
        return user;
    }
}
```

### Organization Management Example

```csharp
public class OrganizationManagementService
{
    private readonly OrganizationsApi _organizationsApi;
    private readonly UsersApi _usersApi;
    private readonly ILogger<OrganizationManagementService> _logger;

    public OrganizationManagementService(
        OrganizationsApi organizationsApi, 
        UsersApi usersApi, 
        ILogger<OrganizationManagementService> logger)
    {
        _organizationsApi = organizationsApi;
        _usersApi = usersApi;
        _logger = logger;
    }

    public async Task<Organization> CreateOrganizationWithUserAsync(
        string organizationName, 
        string userEmail, 
        string userFirstName, 
        string userLastName)
    {
        // Create organization
        var orgRequest = new CreateOrganizationRequest
        {
            Name = organizationName
        };
        var organization = await _organizationsApi.CreateOrganizationAsync(orgRequest);
        _logger.LogInformation("Organization created: {OrgId}", organization.Id);

        // Create user
        var userRequest = new CreateUserRequest
        {
            Profile = new CreateUserRequestProfile
            {
                GivenName = userFirstName,
                FamilyName = userLastName
            },
            Identities = new List<CreateUserRequestIdentitiesInner>
            {
                new CreateUserRequestIdentitiesInner
                {
                    Type = CreateUserRequestIdentitiesInner.TypeEnum.Email,
                    Details = new CreateUserRequestIdentitiesInnerDetails
                    {
                        Email = userEmail
                    }
                }
            }
        };
        var user = await _usersApi.CreateUserAsync(userRequest);
        _logger.LogInformation("User created: {UserId}", user.Id);

        return organization;
    }
}
```

### Error Handling Example

```csharp
public class RobustUserService
{
    private readonly UsersApi _usersApi;
    private readonly ILogger<RobustUserService> _logger;

    public async Task<User?> CreateUserSafelyAsync(CreateUserRequest request)
    {
        try
        {
            return await _usersApi.CreateUserAsync(request);
        }
        catch (ApiException ex) when (ex.ErrorCode == 409)
        {
            _logger.LogWarning("User already exists, attempting to find existing user");
            var email = request.Identities?.FirstOrDefault()?.Details?.Email;
            if (!string.IsNullOrEmpty(email))
            {
                var users = await _usersApi.GetUsersAsync(query: email);
                return users.Users?.FirstOrDefault();
            }
            return null;
        }
        catch (ApiException ex) when (ex.ErrorCode == 400)
        {
            _logger.LogError("Invalid request: {Message}", ex.Message);
            throw new ArgumentException($"Invalid user data: {ex.Message}", ex);
        }
        catch (ApiException ex) when (ex.ErrorCode == 401)
        {
            _logger.LogError("Authentication failed");
            throw new UnauthorizedAccessException("Invalid credentials", ex);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected error creating user");
            throw;
        }
    }
}
```

## Conclusion

The Kinde .NET SDK provides a comprehensive and robust way to interact with the Kinde Management API. The custom serialization functionality ensures that enum types are handled correctly, and the SDK includes proper error handling and logging capabilities.

For more information, refer to the [Kinde API Documentation](https://kinde.com/docs/api/) and the [Kinde .NET SDK GitHub Repository](https://github.com/kinde-oss/kinde-dotnet-sdk).
