using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;

namespace Kinde.Api.Test.Integration.Mocks
{
    /// <summary>
    /// HTTP message handler for mocking Kiota API responses.
    /// 
    /// This handler is designed to work with the Kiota-based API architecture where:
    /// 1. Requests come from Kiota-generated clients
    /// 2. Responses must be in Kiota-compatible JSON format (snake_case by default)
    /// 3. The handler can validate request bodies to ensure proper request mapping
    /// 
    /// Key differences from MockApiResponseHandler:
    /// - Uses System.Text.Json with snake_case naming policy (Kiota default)
    /// - Supports request body validation
    /// - Supports multiple response scenarios (success, error, timeout)
    /// </summary>
    public class KiotaMockHttpHandler : HttpMessageHandler
    {
        private readonly Dictionary<string, MockResponseEntry> _responses = new();
        private readonly List<CapturedRequest> _capturedRequests = new();
        private readonly JsonSerializerOptions _jsonOptions;
        private readonly object _lock = new object();

        /// <summary>
        /// Gets the list of captured requests for verification in tests.
        /// </summary>
        public IReadOnlyList<CapturedRequest> CapturedRequests
        {
            get
            {
                lock (_lock)
                {
                    return _capturedRequests.ToList();
                }
            }
        }

        public KiotaMockHttpHandler()
        {
            _jsonOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower,
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
                WriteIndented = false
            };
        }

        /// <summary>
        /// Add a mock response for a specific request using Kiota model.
        /// The response will be serialized using snake_case naming.
        /// </summary>
        /// <typeparam name="T">The Kiota model type</typeparam>
        /// <param name="method">HTTP method (GET, POST, etc.)</param>
        /// <param name="path">The API path (e.g., /api/v1/users)</param>
        /// <param name="responseData">The Kiota model to return</param>
        /// <param name="statusCode">HTTP status code (default: 200 OK)</param>
        public void AddKiotaResponse<T>(string method, string path, T responseData, HttpStatusCode statusCode = HttpStatusCode.OK)
            where T : class
        {
            var key = CreateKey(method, path);
            var json = JsonSerializer.Serialize(responseData, _jsonOptions);
            _responses[key] = new MockResponseEntry
            {
                Content = json,
                StatusCode = statusCode,
                ContentType = "application/json"
            };
        }

        /// <summary>
        /// Add a raw JSON response (already in snake_case format).
        /// Use this when you need precise control over the JSON structure.
        /// </summary>
        public void AddRawJsonResponse(string method, string path, string jsonContent, HttpStatusCode statusCode = HttpStatusCode.OK)
        {
            var key = CreateKey(method, path);
            _responses[key] = new MockResponseEntry
            {
                Content = jsonContent,
                StatusCode = statusCode,
                ContentType = "application/json"
            };
        }

        /// <summary>
        /// Add an error response with Kiota-compatible error format.
        /// </summary>
        public void AddErrorResponse(string method, string path, HttpStatusCode statusCode, string errorCode = null, string errorMessage = null)
        {
            var key = CreateKey(method, path);
            var errorBody = new Dictionary<string, object>();
            
            if (!string.IsNullOrEmpty(errorCode))
                errorBody["code"] = errorCode;
            if (!string.IsNullOrEmpty(errorMessage))
                errorBody["message"] = errorMessage;
            
            var errors = new[] { errorBody };
            var errorResponse = new { errors };
            
            var json = JsonSerializer.Serialize(errorResponse, _jsonOptions);
            _responses[key] = new MockResponseEntry
            {
                Content = json,
                StatusCode = statusCode,
                ContentType = "application/json"
            };
        }

        /// <summary>
        /// Add an empty response (for 204 No Content or DELETE operations).
        /// </summary>
        public void AddEmptyResponse(string method, string path, HttpStatusCode statusCode = HttpStatusCode.NoContent)
        {
            var key = CreateKey(method, path);
            _responses[key] = new MockResponseEntry
            {
                Content = string.Empty,
                StatusCode = statusCode,
                ContentType = "application/json"
            };
        }

        /// <summary>
        /// Clears all registered responses and captured requests.
        /// </summary>
        public void Clear()
        {
            lock (_lock)
            {
                _responses.Clear();
                _capturedRequests.Clear();
            }
        }

        /// <summary>
        /// Gets the last captured request body deserialized as the specified type.
        /// </summary>
        public T GetLastRequestBody<T>() where T : class
        {
            var lastRequest = CapturedRequests.LastOrDefault();
            if (lastRequest?.Body == null)
                return null;

            return JsonSerializer.Deserialize<T>(lastRequest.Body, _jsonOptions);
        }

        /// <summary>
        /// Verifies that a request was made to the specified endpoint.
        /// Supports both exact path matching and path-only matching (ignoring query string).
        /// </summary>
        public bool WasRequestMade(string method, string path)
        {
            return CapturedRequests.Any(r => 
                r.Method.Equals(method, StringComparison.OrdinalIgnoreCase) && 
                (r.Path.Equals(path, StringComparison.OrdinalIgnoreCase) ||
                 r.Path.StartsWith(path + "?", StringComparison.OrdinalIgnoreCase) ||
                 GetPathWithoutQuery(r.Path).Equals(path, StringComparison.OrdinalIgnoreCase)));
        }

        /// <summary>
        /// Extracts the path portion without query string.
        /// </summary>
        private static string GetPathWithoutQuery(string pathAndQuery)
        {
            var queryIndex = pathAndQuery.IndexOf('?');
            return queryIndex >= 0 ? pathAndQuery.Substring(0, queryIndex) : pathAndQuery;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            // Capture the request for verification
            await CaptureRequestAsync(request);

            var method = request.Method.Method;
            var path = request.RequestUri?.PathAndQuery ?? "";
            var pathOnly = request.RequestUri?.AbsolutePath ?? "";

            // Try exact match first (with query string)
            var key = CreateKey(method, path);
            if (_responses.TryGetValue(key, out var response))
            {
                return CreateResponse(response);
            }

            // Try path-only match (without query string)
            key = CreateKey(method, pathOnly);
            if (_responses.TryGetValue(key, out response))
            {
                return CreateResponse(response);
            }

            // Try to match with pattern (for paths with dynamic segments like /users/{id})
            response = FindPatternMatch(method, pathOnly);
            if (response != null)
            {
                return CreateResponse(response);
            }

            // Return 404 if no match found
            return new HttpResponseMessage(HttpStatusCode.NotFound)
            {
                Content = new StringContent(
                    JsonSerializer.Serialize(new 
                    { 
                        errors = new[] 
                        { 
                            new { code = "not_found", message = $"No mock response found for {method} {path}" } 
                        } 
                    }, _jsonOptions),
                    System.Text.Encoding.UTF8,
                    "application/json")
            };
        }

        private async Task CaptureRequestAsync(HttpRequestMessage request)
        {
            string body = null;
            if (request.Content != null)
            {
                body = await request.Content.ReadAsStringAsync();
            }

            lock (_lock)
            {
                _capturedRequests.Add(new CapturedRequest
                {
                    Method = request.Method.Method,
                    Path = request.RequestUri?.PathAndQuery ?? "",
                    Headers = request.Headers.ToDictionary(h => h.Key, h => string.Join(", ", h.Value)),
                    Body = body,
                    Timestamp = DateTime.UtcNow
                });
            }
        }

        private HttpResponseMessage CreateResponse(MockResponseEntry entry)
        {
            var response = new HttpResponseMessage(entry.StatusCode);
            
            if (!string.IsNullOrEmpty(entry.Content))
            {
                response.Content = new StringContent(entry.Content, System.Text.Encoding.UTF8, entry.ContentType);
            }

            return response;
        }

        private MockResponseEntry FindPatternMatch(string method, string path)
        {
            // Try to find a pattern match by replacing path segments with wildcards
            var segments = path.Split('/', StringSplitOptions.RemoveEmptyEntries);
            
            foreach (var (key, entry) in _responses)
            {
                var parts = key.Split(':');
                if (parts.Length != 2 || !parts[0].Equals(method, StringComparison.OrdinalIgnoreCase))
                    continue;

                var patternPath = parts[1];
                var patternSegments = patternPath.Split('/', StringSplitOptions.RemoveEmptyEntries);

                if (segments.Length != patternSegments.Length)
                    continue;

                var isMatch = true;
                for (int i = 0; i < segments.Length; i++)
                {
                    // Check if this is a parameter placeholder (e.g., {id} or {user_id})
                    if (patternSegments[i].StartsWith("{") && patternSegments[i].EndsWith("}"))
                        continue;

                    if (!segments[i].Equals(patternSegments[i], StringComparison.OrdinalIgnoreCase))
                    {
                        isMatch = false;
                        break;
                    }
                }

                if (isMatch)
                    return entry;
            }

            return null;
        }

        private static string CreateKey(string method, string path)
        {
            return $"{method.ToUpperInvariant()}:{path}";
        }

        private class MockResponseEntry
        {
            public string Content { get; set; }
            public HttpStatusCode StatusCode { get; set; }
            public string ContentType { get; set; }
        }
    }

    /// <summary>
    /// Represents a captured HTTP request for verification in tests.
    /// </summary>
    public class CapturedRequest
    {
        public string Method { get; set; }
        public string Path { get; set; }
        public Dictionary<string, string> Headers { get; set; }
        public string Body { get; set; }
        public DateTime Timestamp { get; set; }

        /// <summary>
        /// Deserializes the request body to the specified type.
        /// </summary>
        public T GetBodyAs<T>() where T : class
        {
            if (string.IsNullOrEmpty(Body))
                return null;

            return JsonSerializer.Deserialize<T>(Body, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower,
                PropertyNameCaseInsensitive = true
            });
        }
    }
}


