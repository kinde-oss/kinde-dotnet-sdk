using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Kinde.Api.Test.Integration
{
    /// <summary>
    /// Mock HTTP message handler for integration tests
    /// Returns predefined responses based on request patterns
    /// </summary>
    public class MockHttpHandler : HttpMessageHandler
    {
        // Store JSON strings instead of HttpResponseMessage to avoid content reuse issues
        private readonly Dictionary<string, (HttpStatusCode StatusCode, string JsonContent)> _responses = new();
        private readonly Dictionary<string, Func<HttpRequestMessage, HttpResponseMessage>> _responseFactories = new();

        public MockHttpHandler()
        {
            SetupDefaultResponses();
            // Load auto-generated mock responses
            GeneratedMockResponses.SetupResponses(this);
        }

        /// <summary>
        /// Sets up default mock responses for common endpoints
        /// </summary>
        private void SetupDefaultResponses()
        {
            // GetBusiness endpoint
            AddResponse("GET", "/api/v1/business", new
            {
                code = "OK",
                message = "Success",
                business = new
                {
                    code = "bus_test123",
                    name = "Test Business",
                    phone = "+1234567890",
                    email = "test@example.com",
                    industry = "Technology",
                    timezone = "America/Los_Angeles",
                    privacy_url = "https://example.com/privacy",
                    terms_url = "https://example.com/terms",
                    has_clickwrap = true,
                    has_kinde_branding = false,
                    created_on = "2024-01-01T00:00:00Z"
                }
            });

            // GetEnvironment endpoint
            AddResponse("GET", "/api/v1/environment", new
            {
                code = "OK",
                message = "Success",
                environment = new
                {
                    code = "production",
                    name = "Production",
                    is_default = true,
                    is_live = true,
                    kinde_domain = "test.kinde.com",
                    created_on = "2024-01-01T00:00:00Z"
                }
            });

            // GetEnvironmentVariables endpoint
            AddResponse("GET", "/api/v1/environment-variables", new
            {
                code = "OK",
                message = "Success",
                environment_variables = new[]
                {
                    new { key = "API_KEY", value = "secret_value", is_secret = true },
                    new { key = "DEBUG_MODE", value = "false", is_secret = false }
                }
            });

            // GetOrganizations endpoint
            AddResponse("GET", "/api/v1/organizations", new
            {
                code = "OK",
                message = "Success",
                organizations = new[]
                {
                    new { code = "org_001", name = "Organization 1", is_default = true },
                    new { code = "org_002", name = "Organization 2", is_default = false }
                },
                next_token = (string?)null
            });

            // GetAPIs endpoint
            AddResponse("GET", "/api/v1/apis", new
            {
                code = "OK",
                message = "Success",
                apis = new[]
                {
                    new
                    {
                        id = "api_001",
                        name = "Test API",
                        audience = "https://api.example.com",
                        is_management_api = true
                    }
                }
            });

            // GetApplications endpoint
            AddResponse("GET", "/api/v1/applications", new
            {
                code = "OK",
                message = "Success",
                applications = new[]
                {
                    new
                    {
                        id = "app_001",
                        name = "Test Application",
                        type = "reg",
                        client_id = "client_123"
                    }
                }
            });

            // GetRoles endpoint
            AddResponse("GET", "/api/v1/roles", new
            {
                code = "OK",
                message = "Success",
                roles = new[]
                {
                    new { id = "role_001", name = "Admin", key = "admin" },
                    new { id = "role_002", name = "User", key = "user" }
                }
            });

            // GetPermissions endpoint
            AddResponse("GET", "/api/v1/permissions", new
            {
                code = "OK",
                message = "Success",
                permissions = new[]
                {
                    new { id = "perm_001", name = "read:users", key = "read:users" },
                    new { id = "perm_002", name = "write:users", key = "write:users" }
                }
            });

            // GetProperties endpoint
            AddResponse("GET", "/api/v1/properties", new
            {
                code = "OK",
                message = "Success",
                properties = new[]
                {
                    new { id = "prop_001", name = "theme", key = "theme", type = "str" }
                }
            });

            // GetTimezones endpoint
            AddResponse("GET", "/api/v1/timezones", new
            {
                code = "OK",
                message = "Success",
                timezones = new[]
                {
                    new { key = "America/Los_Angeles", name = "Pacific Time" },
                    new { key = "America/New_York", name = "Eastern Time" }
                }
            });

            // GetIndustries endpoint
            AddResponse("GET", "/api/v1/industries", new
            {
                code = "OK",
                message = "Success",
                industries = new[]
                {
                    new { name = "Technology" },
                    new { name = "Healthcare" }
                }
            });

            // GetConnections endpoint
            AddResponse("GET", "/api/v1/connections", new
            {
                code = "OK",
                message = "Success",
                connections = new[]
                {
                    new
                    {
                        id = "conn_001",
                        name = "Test Connection",
                        type = "saml"
                    }
                }
            });

            // GetWebHooks endpoint
            AddResponse("GET", "/api/v1/webhooks", new
            {
                code = "OK",
                message = "Success",
                webhooks = new[]
                {
                    new
                    {
                        id = "webhook_001",
                        endpoint = "https://example.com/webhook",
                        events = new[] { "user.created", "user.updated" }
                    }
                }
            });

            // GetCategories endpoint (Property Categories)
            AddResponse("GET", "/api/v1/property_categories", new
            {
                code = "OK",
                message = "Success",
                property_categories = new[]
                {
                    new { id = "cat_001", name = "User Properties" },
                    new { id = "cat_002", name = "Organization Properties" }
                }
            });

            // GetSubscribers endpoint
            AddResponse("GET", "/api/v1/subscribers", new
            {
                code = "OK",
                message = "Success",
                subscribers = new[]
                {
                    new
                    {
                        id = "sub_001",
                        email = "subscriber@example.com",
                        full_name = "Test Subscriber"
                    }
                }
            });

            // Parameterized endpoints - use more specific path patterns
            // GetAPIScopes by ID - /api/v1/apis/{api_id}/scopes (more specific, check first)
            AddResponse("GET", "/api/v1/apis/", new
            {
                code = "OK",
                message = "Success",
                scopes = new[]
                {
                    new { id = "scope_001", name = "read:users" },
                    new { id = "scope_002", name = "write:users" }
                }
            }, pathContains: "/scopes");

            // GetAPI by ID - /api/v1/apis/{api_id} (less specific, check after scopes)
            AddResponse("GET", "/api/v1/apis/", new
            {
                code = "OK",
                message = "Success",
                api = new
                {
                    id = "api_001",
                    name = "Test API",
                    audience = "https://api.example.com",
                    is_management_api = true
                }
            });

            // GetRolePermissions by ID - /api/v1/roles/{role_id}/permissions (most specific)
            AddResponse("GET", "/api/v1/roles/", new
            {
                code = "OK",
                message = "Success",
                permissions = new[]
                {
                    new { id = "perm_001", name = "read:users", key = "read:users" }
                }
            }, pathContains: "/permissions");

            // GetRoleScopes by ID - /api/v1/roles/{role_id}/scopes
            AddResponse("GET", "/api/v1/roles/", new
            {
                code = "OK",
                message = "Success",
                scopes = new[]
                {
                    new { id = "scope_001", name = "read:users" }
                }
            }, pathContains: "/scopes");

            // GetRole by ID - /api/v1/roles/{role_id}
            AddResponse("GET", "/api/v1/roles/", new
            {
                code = "OK",
                message = "Success",
                role = new
                {
                    id = "role_001",
                    name = "Admin",
                    key = "admin"
                }
            });

            // GetApplication by ID - /api/v1/applications/{application_id}
            AddResponse("GET", "/api/v1/applications/", new
            {
                code = "OK",
                message = "Success",
                application = new
                {
                    id = "app_001",
                    name = "Test Application",
                    type = "reg",
                    client_id = "client_123"
                }
            });
        }

        /// <summary>
        /// Adds a mock response for a specific HTTP method and path pattern
        /// </summary>
        public void AddResponse(string method, string pathPattern, object responseData, HttpStatusCode statusCode = HttpStatusCode.OK, string? pathContains = null)
        {
            var key = $"{method.ToUpper()}:{pathPattern}";
            if (!string.IsNullOrEmpty(pathContains))
            {
                key += $":{pathContains}";
            }
            var json = JsonConvert.SerializeObject(responseData);
            // Store JSON string to avoid content reuse issues
            _responses[key] = (statusCode, json);
        }
        
        /// <summary>
        /// Adds a mock response from a JSON string (used by generated mock responses)
        /// </summary>
        public void AddResponseFromJson(string method, string pathPattern, string jsonContent, HttpStatusCode statusCode = HttpStatusCode.OK)
        {
            var key = $"{method.ToUpper()}:{pathPattern}";
            _responses[key] = (statusCode, jsonContent);
        }

        /// <summary>
        /// Adds a dynamic response factory for a specific HTTP method and path pattern
        /// </summary>
        public void AddResponseFactory(string method, string pathPattern, Func<HttpRequestMessage, HttpResponseMessage> factory)
        {
            var key = $"{method.ToUpper()}:{pathPattern}";
            _responseFactories[key] = factory;
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var method = request.Method.Method;
            var path = request.RequestUri?.AbsolutePath ?? "";
            var fullUrl = request.RequestUri?.ToString() ?? "";
            
            // Debug: Log the request (can be removed later)
            System.Diagnostics.Debug.WriteLine($"MockHttpHandler: {method} {path} (full: {fullUrl})");
            
            // Try to find exact match first
            var exactKey = $"{method}:{path}";
            if (_responses.TryGetValue(exactKey, out var exactResponse))
            {
                System.Diagnostics.Debug.WriteLine($"MockHttpHandler: Found exact match for {exactKey}");
                var response = new HttpResponseMessage(exactResponse.StatusCode)
                {
                    Content = new StringContent(exactResponse.JsonContent, Encoding.UTF8, "application/json")
                };
                return Task.FromResult(response);
            }

            // Try to find path pattern match (check most specific patterns first)
            // Sort by key length descending to check more specific patterns first
            var sortedResponses = _responses.OrderByDescending(kvp => kvp.Key.Length);
            foreach (var kvp in sortedResponses)
            {
                var keyParts = kvp.Key.Split(':');
                if (keyParts.Length >= 2 && keyParts[0] == method && path.StartsWith(keyParts[1]))
                {
                    // If there's a pathContains requirement (keyParts[2]), check it
                    if (keyParts.Length >= 3)
                    {
                        var pathContains = keyParts[2];
                        if (!path.Contains(pathContains))
                        {
                            continue; // Skip this response, path doesn't contain required substring
                        }
                    }
                    
                    System.Diagnostics.Debug.WriteLine($"MockHttpHandler: Found pattern match for {kvp.Key}");
                    var response = new HttpResponseMessage(kvp.Value.StatusCode)
                    {
                        Content = new StringContent(kvp.Value.JsonContent, Encoding.UTF8, "application/json")
                    };
                    return Task.FromResult(response);
                }
            }

            // Try response factories
            foreach (var kvp in _responseFactories)
            {
                var keyParts = kvp.Key.Split(':', 2);
                if (keyParts.Length == 2 && keyParts[0] == method && path.StartsWith(keyParts[1]))
                {
                    System.Diagnostics.Debug.WriteLine($"MockHttpHandler: Using factory for {kvp.Key}");
                    return Task.FromResult(kvp.Value(request));
                }
            }

            // Default: return 404 with JSON (not HTML)
            System.Diagnostics.Debug.WriteLine($"MockHttpHandler: No match found for {method} {path}, returning 404");
            return Task.FromResult(new HttpResponseMessage(HttpStatusCode.NotFound)
            {
                Content = new StringContent(JsonConvert.SerializeObject(new { error = "Not found", path, method }), Encoding.UTF8, "application/json")
            });
        }
    }
}

