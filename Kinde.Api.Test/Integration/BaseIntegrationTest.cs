using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Kinde.Api.Api;
using Kinde.Api.Client;
using Xunit;

namespace Kinde.Api.Test.Integration
{
    /// <summary>
    /// Base class for integration tests that supports both mock and real API testing.
    /// Uses lazy initialization to avoid network calls until actually needed.
    /// </summary>
    public abstract class BaseIntegrationTest : IDisposable
    {
        protected readonly TestConfiguration Config;
        protected readonly bool UseRealApi;
        
        private HttpClient? _httpClient;
        private Configuration? _apiConfiguration;
        private Mocks.MockApiResponseHandler? _mockHandler;
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
                    Console.WriteLine("[BASE TEST] Initializing MOCK API mode...");
                    InitializeMockApi();
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
            Console.WriteLine($"[REAL API] ClientId: {mgmtConfig.ClientId.Substring(0, Math.Min(8, mgmtConfig.ClientId.Length))}...");
            Console.WriteLine("[REAL API] Requesting access token...");
            
            var tokenStartTime = DateTime.UtcNow;
            var accessToken = GetAccessTokenAsync().GetAwaiter().GetResult();
            var tokenDuration = (DateTime.UtcNow - tokenStartTime).TotalMilliseconds;
            
            Console.WriteLine($"[REAL API] Access token obtained in {tokenDuration:F0}ms");
            Console.WriteLine($"[REAL API] Token preview: {accessToken.Substring(0, Math.Min(20, accessToken.Length))}...");
            
            _apiConfiguration = new Configuration
            {
                BasePath = mgmtConfig.Domain,
                AccessToken = accessToken
            };

            _httpClient = new HttpClient();
            Console.WriteLine("[REAL API] HttpClient initialized for real API calls");
        }

        /// <summary>
        /// Initialize for mock API testing
        /// </summary>
        private void InitializeMockApi()
        {
            Console.WriteLine("[MOCK API] Creating MockApiResponseHandler...");
            _mockHandler = new Mocks.MockApiResponseHandler();
            _httpClient = new HttpClient(_mockHandler);
            _apiConfiguration = new Configuration
            {
                BasePath = "https://mock.kinde.com"
            };
            Console.WriteLine("[MOCK API] HttpClient initialized with mock handler (no real network calls)");
        }

        /// <summary>
        /// Get the mock handler if using mocks
        /// </summary>
        protected Mocks.MockApiResponseHandler? GetMockHandler()
        {
            EnsureInitialized();
            return _mockHandler;
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

        public virtual void Dispose()
        {
            _httpClient?.Dispose();
        }
    }
}
