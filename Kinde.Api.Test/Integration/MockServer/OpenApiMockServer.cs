using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace Kinde.Api.Test.Integration.MockServer
{
    /// <summary>
    /// OpenAPI Mock Server that responds to Management API calls based on the OpenAPI specification.
    /// This server reads the OpenAPI YAML spec and generates mock responses for testing serialization/deserialization.
    /// </summary>
    public class OpenApiMockServer : IDisposable
    {
        private TestServer? _server;
        private HttpClient? _client;
        private readonly string _openApiSpecPath;
        private readonly OpenApiSpec _spec;
        private readonly Dictionary<string, object> _mockDataStore;

        public OpenApiMockServer(string openApiSpecPath)
        {
            _openApiSpecPath = openApiSpecPath;
            _spec = LoadOpenApiSpec(openApiSpecPath);
            _mockDataStore = new Dictionary<string, object>();
            InitializeServer();
        }

        /// <summary>
        /// Gets the base URL of the mock server
        /// </summary>
        public string BaseUrl => _server?.BaseAddress?.ToString().TrimEnd('/') ?? "http://localhost:5000";

        /// <summary>
        /// Gets the HttpClient for making requests to the mock server
        /// </summary>
        public HttpClient Client => _client ?? throw new InvalidOperationException("Server not initialized");

        /// <summary>
        /// Sets mock data for a specific endpoint path and method.
        /// Path can be either a template (e.g., "/api/v1/organizations/{org_code}/users") or actual path.
        /// </summary>
        public void SetMockResponse(string path, string method, object response, HttpStatusCode statusCode = HttpStatusCode.OK)
        {
            var key = $"{method.ToUpper()}:{path}";
            _mockDataStore[key] = new MockResponse { Data = response, StatusCode = statusCode };
        }

        /// <summary>
        /// Gets the OpenAPI specification that was loaded
        /// </summary>
        public OpenApiSpec Specification => _spec;

        private OpenApiSpec LoadOpenApiSpec(string specPath)
        {
            if (!File.Exists(specPath))
            {
                throw new FileNotFoundException($"OpenAPI specification not found at: {specPath}");
            }

            var deserializer = new DeserializerBuilder()
                .WithNamingConvention(UnderscoredNamingConvention.Instance)
                .IgnoreUnmatchedProperties()
                .Build();

            var yamlContent = File.ReadAllText(specPath);
            var spec = deserializer.Deserialize<OpenApiSpec>(yamlContent);

            return spec;
        }

        private void InitializeServer()
        {
            var hostBuilder = new WebHostBuilder()
                .UseKestrel()
                .UseUrls("http://localhost:5000")
                .ConfigureServices(services =>
                {
                    services.AddRouting();
                    services.AddCors(options =>
                    {
                        options.AddDefaultPolicy(policy =>
                        {
                            policy.AllowAnyOrigin()
                                  .AllowAnyMethod()
                                  .AllowAnyHeader();
                        });
                    });
                })
                .Configure(app =>
                {
                    app.UseCors();
                    // Add middleware to log all requests and normalize paths
                    app.Use(async (context, next) =>
                    {
                        // Normalize double slashes in path
                        var path = context.Request.Path.Value ?? "";
                        if (path.StartsWith("//"))
                        {
                            path = path.Substring(1);
                            context.Request.Path = new PathString(path);
                        }
                        Console.WriteLine($"[MockServer] Incoming: {context.Request.Method} {context.Request.Path}");
                        await next();
                        Console.WriteLine($"[MockServer] Response: {context.Response.StatusCode} for {context.Request.Method} {context.Request.Path}");
                    });
                    app.UseRouting();
                    app.UseEndpoints(endpoints =>
                    {
                        ConfigureRoutes(endpoints);
                    });
                });

            _server = new TestServer(hostBuilder);
            _client = _server.CreateClient();
            _client.BaseAddress = new Uri("http://localhost:5000");
        }

        private void ConfigureRoutes(IEndpointRouteBuilder endpoints)
        {
            foreach (var pathItem in _spec.Paths ?? new Dictionary<string, PathItem>())
            {
                var path = pathItem.Key;
                var operations = pathItem.Value;

                // Store operations to avoid captured variable issues
                var getOp = operations.Get;
                var postOp = operations.Post;
                var putOp = operations.Put;
                var patchOp = operations.Patch;
                var deleteOp = operations.Delete;

                // Map GET requests
                if (getOp != null)
                {
                    // Store path and operation locally to avoid closure issues
                    var localPath = path;
                    var localGetOp = getOp;
                    Console.WriteLine($"[MockServer] Registering GET route: {localPath}");
                    endpoints.MapGet(localPath, async (HttpContext context) =>
                    {
                        var actualPath = context.Request.Path.Value ?? "";
                        Console.WriteLine($"[MockServer] GET route matched: {localPath} -> {actualPath}");
                        await HandleRequest(context, localPath, actualPath, "GET", localGetOp);
                    }).AllowAnonymous();
                }

                // Map POST requests
                if (postOp != null)
                {
                    var localPath = path;
                    var localPostOp = postOp;
                    Console.WriteLine($"[MockServer] Registering POST route: {localPath}");
                    endpoints.MapPost(localPath, async (HttpContext context) =>
                    {
                        var actualPath = context.Request.Path.Value ?? "";
                        Console.WriteLine($"[MockServer] POST route matched: {localPath} -> {actualPath}");
                        await HandleRequest(context, localPath, actualPath, "POST", localPostOp);
                    }).AllowAnonymous();
                }

                // Map PUT requests
                if (putOp != null)
                {
                    endpoints.MapPut(path, async (HttpContext context) =>
                    {
                        var actualPath = context.Request.Path.Value ?? "";
                        await HandleRequest(context, path, actualPath, "PUT", putOp);
                    }).AllowAnonymous();
                }

                // Map PATCH requests
                if (patchOp != null)
                {
                    endpoints.MapPatch(path, async (HttpContext context) =>
                    {
                        var actualPath = context.Request.Path.Value ?? "";
                        await HandleRequest(context, path, actualPath, "PATCH", patchOp);
                    }).AllowAnonymous();
                }

                // Map DELETE requests
                if (deleteOp != null)
                {
                    endpoints.MapDelete(path, async (HttpContext context) =>
                    {
                        var actualPath = context.Request.Path.Value ?? "";
                        await HandleRequest(context, path, actualPath, "DELETE", deleteOp);
                    }).AllowAnonymous();
                }
            }
        }

        private async Task HandleRequest(HttpContext context, string pathTemplate, string actualPath, string method, Operation operation)
        {
            try
            {
                // Debug: Log the incoming request
                Console.WriteLine($"[MockServer] HandleRequest: {method} {actualPath} (template: {pathTemplate})");

                // Check if custom mock response is set (use actual path for matching)
                var key = $"{method}:{actualPath}";
                Console.WriteLine($"[MockServer] Checking mock store for key: {key}");
                Console.WriteLine($"[MockServer] Mock store contains {_mockDataStore.Count} entries");
                if (_mockDataStore.TryGetValue(key, out var mockResponse) && mockResponse is MockResponse response)
                {
                    Console.WriteLine($"[MockServer] Found mock response for {key}, StatusCode: {response.StatusCode}");
                    context.Response.StatusCode = (int)response.StatusCode;
                    context.Response.ContentType = "application/json";
                    var json = JsonConvert.SerializeObject(response.Data, new JsonSerializerSettings
                    {
                        NullValueHandling = NullValueHandling.Ignore,
                        // Use snake_case naming to match API response format
                        ContractResolver = new Newtonsoft.Json.Serialization.DefaultContractResolver
                        {
                            NamingStrategy = new Newtonsoft.Json.Serialization.SnakeCaseNamingStrategy()
                        }
                    });
                    Console.WriteLine($"[MockServer] Sending response ({json.Length} bytes): {json.Substring(0, Math.Min(200, json.Length))}");
                    await context.Response.WriteAsync(json);
                    return;
                }

                // Also check with template path (e.g., /api/v1/organizations/{org_code}/users)
                var templateKey = $"{method}:{pathTemplate}";
                Console.WriteLine($"[MockServer] Checking mock store for template key: {templateKey}");
                if (_mockDataStore.TryGetValue(templateKey, out var templateResponse) && templateResponse is MockResponse templateResp)
                {
                    Console.WriteLine($"[MockServer] Found mock response for template {templateKey}, StatusCode: {templateResp.StatusCode}");
                    context.Response.StatusCode = (int)templateResp.StatusCode;
                    context.Response.ContentType = "application/json";
                    var json = JsonConvert.SerializeObject(templateResp.Data, new JsonSerializerSettings
                    {
                        NullValueHandling = NullValueHandling.Ignore,
                        // Use snake_case naming to match API response format
                        ContractResolver = new Newtonsoft.Json.Serialization.DefaultContractResolver
                        {
                            NamingStrategy = new Newtonsoft.Json.Serialization.SnakeCaseNamingStrategy()
                        }
                    });
                    await context.Response.WriteAsync(json);
                    return;
                }

                // Try to match path template with actual path (e.g., /api/v1/organizations/{org_code}/users matches /api/v1/organizations/org123/users)
                var normalizedTemplate = NormalizePathTemplate(pathTemplate, actualPath);
                if (normalizedTemplate != null)
                {
                    var normalizedKey = $"{method}:{normalizedTemplate}";
                    Console.WriteLine($"[MockServer] Normalized template matches, checking key: {normalizedKey}");
                    if (_mockDataStore.TryGetValue(normalizedKey, out var normalizedResponse) && normalizedResponse is MockResponse normResp)
                    {
                        Console.WriteLine($"[MockServer] Found mock response for normalized template {normalizedKey}, StatusCode: {normResp.StatusCode}");
                        context.Response.StatusCode = (int)normResp.StatusCode;
                        context.Response.ContentType = "application/json";
                        var json = JsonConvert.SerializeObject(normResp.Data, new JsonSerializerSettings
                        {
                            NullValueHandling = NullValueHandling.Ignore,
                            ContractResolver = new Newtonsoft.Json.Serialization.DefaultContractResolver
                            {
                                NamingStrategy = new Newtonsoft.Json.Serialization.SnakeCaseNamingStrategy()
                            }
                        });
                        await context.Response.WriteAsync(json);
                        return;
                    }
                }

                // Generate default response from OpenAPI spec
                Console.WriteLine($"[MockServer] No custom mock found, generating default response from OpenAPI spec");
                var defaultResponse = GenerateDefaultResponse(operation);
                if (defaultResponse != null)
                {
                    context.Response.StatusCode = (int)HttpStatusCode.OK;
                    context.Response.ContentType = "application/json";
                    
                    var defaultJson = JsonConvert.SerializeObject(defaultResponse, new JsonSerializerSettings
                    {
                        NullValueHandling = NullValueHandling.Ignore,
                        ContractResolver = new Newtonsoft.Json.Serialization.DefaultContractResolver
                        {
                            NamingStrategy = new Newtonsoft.Json.Serialization.SnakeCaseNamingStrategy()
                        }
                    });
                    Console.WriteLine($"[MockServer] Generated default response ({defaultJson.Length} bytes): {defaultJson.Substring(0, Math.Min(200, defaultJson.Length))}");
                    await context.Response.WriteAsync(defaultJson);
                }
                else
                {
                    // No default response available - return 200 with empty object
                    Console.WriteLine($"[MockServer] No default response available, returning empty object");
                    context.Response.StatusCode = (int)HttpStatusCode.OK;
                    context.Response.ContentType = "application/json";
                    await context.Response.WriteAsync("{}");
                }
            }
            catch (Exception ex)
            {
                // If there's an error, return 500 with error details for debugging
                Console.WriteLine($"[MockServer] ERROR: {ex.GetType().Name}: {ex.Message}");
                Console.WriteLine($"[MockServer] StackTrace: {ex.StackTrace}");
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                context.Response.ContentType = "application/json";
                var errorResponse = new { error = ex.Message, stackTrace = ex.StackTrace };
                await context.Response.WriteAsync(JsonConvert.SerializeObject(errorResponse));
            }
        }

        /// <summary>
        /// Attempts to normalize a path template against an actual path
        /// Returns the template path if it matches, null otherwise
        /// </summary>
        private string? NormalizePathTemplate(string template, string actual)
        {
            // Simple pattern matching: if actual path matches template pattern, return template
            // This is a basic implementation - for production, use proper route matching
            var templateParts = template.Split('/');
            var actualParts = actual.Split('/');
            
            if (templateParts.Length != actualParts.Length)
                return null;

            for (int i = 0; i < templateParts.Length; i++)
            {
                if (templateParts[i].StartsWith("{") && templateParts[i].EndsWith("}"))
                    continue; // This is a parameter, skip
                if (templateParts[i] != actualParts[i])
                    return null; // Paths don't match
            }

            return template; // Matches the pattern
        }

        private object GenerateDefaultResponse(Operation operation)
        {
            // Look for 200 response
            if (operation.Responses?.TryGetValue("200", out var successResponse) == true && 
                successResponse?.Content?.TryGetValue("application/json", out var jsonContent) == true)
            {
                var schema = jsonContent.Schema;
                
                // If schema has example, use it
                if (schema?.Example != null)
                {
                    return schema.Example;
                }

                // If schema has $ref, look up the component
                if (schema?.Ref != null)
                {
                    var refName = schema.Ref.Replace("#/components/schemas/", "");
                    if (_spec.Components?.Schemas?.TryGetValue(refName, out var refSchema) == true)
                    {
                        return GenerateExampleFromSchema(refSchema, refName);
                    }
                }

                // Generate example from schema
                if (schema != null)
                {
                    return GenerateExampleFromSchema(schema);
                }
            }

            // Return empty object if no schema found
            return new { };
        }

        private object GenerateExampleFromSchema(Schema? schema, string? schemaName = null)
        {
            if (schema == null)
            {
                return new { };
            }

            // Check if there's an example in the schema definition
            if (schema.Example != null)
            {
                return schema.Example;
            }

            // For schema references, look up in components
            if (schema.Ref != null)
            {
                var refName = schema.Ref.Replace("#/components/schemas/", "");
                if (_spec.Components?.Schemas?.TryGetValue(refName, out var refSchema) == true)
                {
                    return GenerateExampleFromSchema(refSchema, refName);
                }
            }

            // Handle different schema types
            if (schema.Type == "object" && schema.Properties != null)
            {
                var obj = new Dictionary<string, object>();
                foreach (var property in schema.Properties)
                {
                    obj[property.Key] = GenerateExampleFromSchema(property.Value);
                }
                return obj;
            }

            if (schema.Type == "array" && schema.Items != null)
            {
                return new[] { GenerateExampleFromSchema(schema.Items) };
            }

            // Return type-specific defaults
            return schema.Type switch
            {
                "string" => schema.Example ?? "string",
                "integer" => schema.Example ?? 0,
                "number" => schema.Example ?? 0.0,
                "boolean" => schema.Example ?? false,
                _ => new { }
            };
        }

        public void Dispose()
        {
            _client?.Dispose();
            _server?.Dispose();
        }

        private class MockResponse
        {
            public object Data { get; set; } = new { };
            public HttpStatusCode StatusCode { get; set; } = HttpStatusCode.OK;
        }
    }

    // OpenAPI Spec Models
    public class OpenApiSpec
    {
        [YamlMember(Alias = "openapi")]
        public string? OpenApi { get; set; }

        public Info? Info { get; set; }
        public Dictionary<string, PathItem>? Paths { get; set; }
        public Components? Components { get; set; }
    }

    public class Info
    {
        public string? Title { get; set; }
        public string? Version { get; set; }
    }

    public class PathItem
    {
        public Operation? Get { get; set; }
        public Operation? Post { get; set; }
        public Operation? Put { get; set; }
        public Operation? Patch { get; set; }
        public Operation? Delete { get; set; }
    }

    public class Operation
    {
        [YamlMember(Alias = "operationId")]
        public string? OperationId { get; set; }

        public string? Summary { get; set; }
        public List<Parameter>? Parameters { get; set; }
        public Dictionary<string, Response>? Responses { get; set; }
        public RequestBody? RequestBody { get; set; }
    }

    public class Parameter
    {
        public string? Name { get; set; }
        public string? In { get; set; }
        public Schema? Schema { get; set; }
        public bool Required { get; set; }
    }

    public class RequestBody
    {
        public Dictionary<string, MediaType>? Content { get; set; }
        public bool Required { get; set; }
    }

    public class Response
    {
        public string? Description { get; set; }
        public Dictionary<string, MediaType>? Content { get; set; }
    }

    public class MediaType
    {
        public Schema? Schema { get; set; }
    }

    public class Schema
    {
        public string? Type { get; set; }
        public string? Ref { get; set; }
        public Dictionary<string, Schema>? Properties { get; set; }
        public Schema? Items { get; set; }
        public object? Example { get; set; }
        public List<string>? Required { get; set; }
    }

    public class Components
    {
        public Dictionary<string, Schema>? Schemas { get; set; }
    }
}

