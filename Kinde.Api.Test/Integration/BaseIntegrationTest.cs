using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Kinde.Api.Api;
using Kinde.Api.Client;
using Kinde.Api.Test.Integration.Mocks;
using Microsoft.Kiota.Abstractions;
using Xunit;

namespace Kinde.Api.Test.Integration
{
    /// <summary>
    /// Base class for integration tests that supports both mock and real API testing.
    /// 
    /// The class supports two mock handlers:
    /// 1. MockApiResponseHandler - Original handler for backward compatibility
    /// 2. KiotaMockHttpHandler - New handler designed for Kiota-based API integration
    /// 
    /// Uses lazy initialization to avoid network calls until actually needed.
    /// </summary>
    public abstract class BaseIntegrationTest : IDisposable
    {
        protected readonly TestConfiguration Config;
        protected readonly bool UseRealApi;
        
        private HttpClient? _httpClient;
        private Configuration? _apiConfiguration;
        private MockApiResponseHandler? _legacyMockHandler;
        private KiotaMockHttpHandler? _kiotaMockHandler;
        private bool _initialized = false;
        private readonly object _initLock = new object();

        /// <summary>
        /// Gets the HttpClient. Initializes if not already done.
        /// </summary>
        protected HttpClient HttpClient
        {
            get
            {
                EnsureInitialized();
                return _httpClient!;
            }
        }

        /// <summary>
        /// Gets the API Configuration. Initializes if not already done.
        /// </summary>
        protected Configuration ApiConfiguration
        {
            get
            {
                EnsureInitialized();
                return _apiConfiguration!;
            }
        }

        protected BaseIntegrationTest()
        {
            Config = TestConfiguration.Instance;
            UseRealApi = Config.UseRealApi;
            
            // Don't initialize in constructor - lazy initialization on first access
            Console.WriteLine($"[BASE TEST] Created BaseIntegrationTest - UseRealApi: {UseRealApi}");
        }

        /// <summary>
        /// Ensures the test is initialized. Thread-safe lazy initialization.
        /// </summary>
        private void EnsureInitialized()
        {
            if (_initialized)
                return;

            lock (_initLock)
            {
                if (_initialized)
                    return;

                if (UseRealApi)
                {
                    Console.WriteLine("[BASE TEST] Initializing REAL API mode...");
                    InitializeRealApi();
                }
                else
                {
                    Console.WriteLine("[BASE TEST] Initializing MOCK API mode with Kiota handler...");
                    InitializeKiotaMockApi();
                }

                _initialized = true;
            }
        }

        /// <summary>
        /// Initialize for real API testing
        /// </summary>
        private void InitializeRealApi()
        {
            var mgmtConfig = Config.ManagementApi;
            
            Console.WriteLine($"[REAL API] Domain: {mgmtConfig.Domain}");
            Console.WriteLine("[REAL API] ClientId: [REDACTED]");
            Console.WriteLine("[REAL API] Requesting access token...");

            var tokenStartTime = DateTime.UtcNow;
            var accessToken = GetAccessTokenAsync().GetAwaiter().GetResult();
            var tokenDuration = (DateTime.UtcNow - tokenStartTime).TotalMilliseconds;

            Console.WriteLine($"[REAL API] Access token obtained in {tokenDuration:F0}ms");
            Console.WriteLine("[REAL API] Access token obtained successfully");
            
            _apiConfiguration = new Configuration
            {
                BasePath = mgmtConfig.Domain,
                AccessToken = accessToken
            };

            _httpClient = new HttpClient();
            Console.WriteLine("[REAL API] HttpClient initialized for real API calls");
        }

        /// <summary>
        /// Initialize for mock API testing using the new Kiota-aware mock handler.
        /// This handler returns responses in Kiota-compatible format (snake_case JSON).
        /// </summary>
        private void InitializeKiotaMockApi()
        {
            Console.WriteLine("[MOCK API] Creating KiotaMockHttpHandler...");
            
            // Register Kiota serializers before any API calls
            RegisterKiotaSerializers();
            
            _kiotaMockHandler = new KiotaMockHttpHandler();
            _httpClient = new HttpClient(_kiotaMockHandler)
            {
                BaseAddress = new Uri("https://mock.kinde.com")
            };
            _apiConfiguration = new Configuration
            {
                BasePath = "https://mock.kinde.com",
                AccessToken = "mock-access-token"
            };
            Console.WriteLine("[MOCK API] HttpClient initialized with Kiota mock handler (no real network calls)");
        }
        
        /// <summary>
        /// Registers the Kiota serializers and deserializers globally.
        /// This must be called before any Kiota client is created.
        /// </summary>
        private static void RegisterKiotaSerializers()
        {
            ApiClientBuilder.RegisterDefaultSerializer<Microsoft.Kiota.Serialization.Json.JsonSerializationWriterFactory>();
            ApiClientBuilder.RegisterDefaultSerializer<Microsoft.Kiota.Serialization.Text.TextSerializationWriterFactory>();
            ApiClientBuilder.RegisterDefaultSerializer<Microsoft.Kiota.Serialization.Form.FormSerializationWriterFactory>();
            ApiClientBuilder.RegisterDefaultSerializer<Microsoft.Kiota.Serialization.Multipart.MultipartSerializationWriterFactory>();
            ApiClientBuilder.RegisterDefaultDeserializer<Microsoft.Kiota.Serialization.Json.JsonParseNodeFactory>();
            ApiClientBuilder.RegisterDefaultDeserializer<Microsoft.Kiota.Serialization.Text.TextParseNodeFactory>();
            ApiClientBuilder.RegisterDefaultDeserializer<Microsoft.Kiota.Serialization.Form.FormParseNodeFactory>();
        }

        /// <summary>
        /// Initialize for mock API testing using the legacy mock handler.
        /// This is maintained for backward compatibility with existing tests.
        /// </summary>
        [Obsolete("Use InitializeKiotaMockApi() for new tests. This method is kept for backward compatibility.")]
        private void InitializeLegacyMockApi()
        {
            Console.WriteLine("[MOCK API] Creating legacy MockApiResponseHandler...");
            _legacyMockHandler = new MockApiResponseHandler();
            _httpClient = new HttpClient(_legacyMockHandler);
            _apiConfiguration = new Configuration
            {
                BasePath = "https://mock.kinde.com"
            };
            Console.WriteLine("[MOCK API] HttpClient initialized with legacy mock handler");
        }

        /// <summary>
        /// Gets the Kiota mock handler for configuring mock responses.
        /// This is the preferred method for new tests.
        /// </summary>
        /// <returns>The KiotaMockHttpHandler or null if using real API.</returns>
        protected KiotaMockHttpHandler? GetKiotaMockHandler()
        {
            EnsureInitialized();
            return _kiotaMockHandler;
        }

        /// <summary>
        /// Gets the legacy mock handler for backward compatibility.
        /// Prefer using GetKiotaMockHandler() for new tests.
        /// </summary>
        /// <returns>The MockApiResponseHandler or null if using real API or Kiota mock.</returns>
        [Obsolete("Use GetKiotaMockHandler() for new tests.")]
        protected MockApiResponseHandler? GetMockHandler()
        {
            EnsureInitialized();
            return _legacyMockHandler;
        }

        /// <summary>
        /// Get access token for Management API (M2M)
        /// </summary>
        private async Task<string> GetAccessTokenAsync()
        {
            var mgmtConfig = Config.ManagementApi;
            
            using var client = new HttpClient();
            var formData = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("grant_type", "client_credentials"),
                new KeyValuePair<string, string>("client_id", mgmtConfig.ClientId),
                new KeyValuePair<string, string>("client_secret", mgmtConfig.ClientSecret),
                new KeyValuePair<string, string>("audience", mgmtConfig.Audience)
            };

            var request = new HttpRequestMessage(HttpMethod.Post, $"{mgmtConfig.Domain}/oauth2/token")
            {
                Content = new FormUrlEncodedContent(formData)
            };

            var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            
            var content = await response.Content.ReadAsStringAsync();
            var json = Newtonsoft.Json.JsonConvert.DeserializeObject<Newtonsoft.Json.Linq.JObject>(content);
            return json?["access_token"]?.ToString() ?? throw new InvalidOperationException("Failed to get access token");
        }

        /// <summary>
        /// Create an API instance with proper configuration
        /// </summary>
        protected TApi CreateApi<TApi>(Func<HttpClient, Configuration, TApi> factory) where TApi : class
        {
            EnsureInitialized();
            return factory(_httpClient!, _apiConfiguration!);
        }

        /// <summary>
        /// Helper to set up a mock response for a Kiota-format API call.
        /// </summary>
        /// <typeparam name="T">The Kiota model type for the response.</typeparam>
        /// <param name="method">HTTP method.</param>
        /// <param name="path">API path.</param>
        /// <param name="response">The Kiota model to return.</param>
        protected void SetupKiotaMockResponse<T>(string method, string path, T response) where T : class
        {
            var mockHandler = GetKiotaMockHandler();
            if (mockHandler != null)
            {
                mockHandler.AddKiotaResponse(method, path, response);
            }
        }

        /// <summary>
        /// Helper to set up an error response for a Kiota-format API call.
        /// </summary>
        /// <param name="method">HTTP method.</param>
        /// <param name="path">API path.</param>
        /// <param name="statusCode">HTTP status code.</param>
        /// <param name="errorCode">Error code.</param>
        /// <param name="errorMessage">Error message.</param>
        protected void SetupKiotaErrorResponse(string method, string path, System.Net.HttpStatusCode statusCode,
            string? errorCode = null, string? errorMessage = null)
        {
            var mockHandler = GetKiotaMockHandler();
            if (mockHandler != null)
            {
                mockHandler.AddErrorResponse(method, path, statusCode, errorCode, errorMessage);
            }
        }

        /// <summary>
        /// Verifies that a request was made to the specified endpoint.
        /// </summary>
        protected bool VerifyRequestMade(string method, string path)
        {
            var mockHandler = GetKiotaMockHandler();
            return mockHandler?.WasRequestMade(method, path) ?? false;
        }

        /// <summary>
        /// Gets the captured requests for verification.
        /// </summary>
        protected IReadOnlyList<CapturedRequest>? GetCapturedRequests()
        {
            var mockHandler = GetKiotaMockHandler();
            return mockHandler?.CapturedRequests;
        }

        public virtual void Dispose()
        {
            _httpClient?.Dispose();
            _legacyMockHandler?.Dispose();
            _kiotaMockHandler?.Dispose();
        }
    }
}
