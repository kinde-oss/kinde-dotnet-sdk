using System;
using System.IO;
using System.Net.Http;
using Kinde.Api.Client;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace Kinde.Api.Test.Integration
{
    /// <summary>
    /// Base class for integration tests that support both real and mock modes
    /// </summary>
    public abstract class BaseIntegrationTest : IClassFixture<IntegrationTestFixture>, IDisposable
    {
        protected readonly IntegrationTestFixture Fixture;
        protected readonly Configuration ApiConfiguration;
        protected readonly bool IsConfigured;
        protected readonly bool UseMockMode;
        protected readonly HttpClient? MockHttpClient;

        protected BaseIntegrationTest(IntegrationTestFixture fixture)
        {
            Fixture = fixture;
            IsConfigured = fixture.IsConfigured;
            UseMockMode = fixture.UseMockMode;
            
            if (UseMockMode)
            {
                // Create mock HTTP client
                // Don't set BaseAddress - let ApiClient handle the full URL construction
                var mockHandler = new MockHttpHandler();
                MockHttpClient = new HttpClient(mockHandler);
                
                ApiConfiguration = new Configuration
                {
                    BasePath = "https://mock.kinde.com",
                    AccessToken = "mock_token"
                };
            }
            else if (IsConfigured)
            {
                ApiConfiguration = new Configuration
                {
                    BasePath = fixture.Domain,
                    AccessToken = fixture.AccessToken
                };
            }
            else
            {
                ApiConfiguration = new Configuration();
            }
        }

        /// <summary>
        /// Skips the test if neither mock mode nor real credentials are configured
        /// </summary>
        protected void SkipIfNotConfigured()
        {
            if (!UseMockMode && !IsConfigured)
            {
                // Fail the test with a clear message about missing configuration
                Assert.True(false, 
                    "Test mode not configured. " +
                    "Either set USE_MOCK_MODE=true for CI/CD testing, " +
                    "or configure real credentials: KINDE_DOMAIN, KINDE_CLIENT_ID, KINDE_CLIENT_SECRET, and KINDE_AUDIENCE environment variables, " +
                    "or configure appsettings.json with KindeManagementApi section.");
            }
        }

        /// <summary>
        /// Creates an API client instance, using mock HTTP client if in mock mode
        /// </summary>
        protected T CreateApiClient<T>(Func<Configuration, HttpClient?, T> factory) where T : class
        {
            if (UseMockMode && MockHttpClient != null)
            {
                return factory(ApiConfiguration, MockHttpClient);
            }
            return factory(ApiConfiguration, null);
        }

        public virtual void Dispose()
        {
            // Override in derived classes if cleanup is needed
        }
    }
    
    /// <summary>
    /// Test fixture that handles M2M authentication once per test run
    /// Supports both real API mode and mock mode for CI/CD
    /// </summary>
    public class IntegrationTestFixture : IDisposable
    {
        public string? Domain { get; private set; }
        public string? AccessToken { get; private set; }
        public bool IsConfigured { get; private set; }
        public bool UseMockMode { get; private set; }

        public IntegrationTestFixture()
        {
            LoadConfiguration();
        }

        private void LoadConfiguration()
        {
            // Check for mock mode first (for CI/CD)
            var useMockMode = Environment.GetEnvironmentVariable("USE_MOCK_MODE");
            if (!string.IsNullOrWhiteSpace(useMockMode) && 
                (useMockMode.Equals("true", StringComparison.OrdinalIgnoreCase) || 
                 useMockMode == "1"))
            {
                UseMockMode = true;
                IsConfigured = true; // Mock mode is always "configured"
                Console.WriteLine("✓ Using MOCK mode for integration tests (CI/CD mode)");
                return;
            }

            // Try to load from appsettings.json first
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true)
                .AddJsonFile("appsettings.Development.json", optional: true)
                .AddEnvironmentVariables()
                .Build();

            // Check config file for mock mode
            var configMockMode = configuration["KindeManagementApi:UseMockMode"];
            if (!string.IsNullOrWhiteSpace(configMockMode) && 
                (configMockMode.Equals("true", StringComparison.OrdinalIgnoreCase) || 
                 configMockMode == "1"))
            {
                UseMockMode = true;
                IsConfigured = true;
                Console.WriteLine("✓ Using MOCK mode for integration tests (from config)");
                return;
            }

            // Real API mode - load credentials
            var domain = configuration["KindeManagementApi:Domain"] 
                ?? Environment.GetEnvironmentVariable("KINDE_DOMAIN");
            var clientId = configuration["KindeManagementApi:ClientId"] 
                ?? Environment.GetEnvironmentVariable("KINDE_CLIENT_ID");
            var clientSecret = configuration["KindeManagementApi:ClientSecret"] 
                ?? Environment.GetEnvironmentVariable("KINDE_CLIENT_SECRET");
            var audience = configuration["KindeManagementApi:Audience"] 
                ?? Environment.GetEnvironmentVariable("KINDE_AUDIENCE");
            var scope = configuration["KindeManagementApi:Scope"] 
                ?? Environment.GetEnvironmentVariable("KINDE_SCOPE");

            if (string.IsNullOrWhiteSpace(domain) ||
                string.IsNullOrWhiteSpace(clientId) ||
                string.IsNullOrWhiteSpace(clientSecret) ||
                string.IsNullOrWhiteSpace(audience))
            {
                IsConfigured = false;
                Console.WriteLine("WARNING: M2M credentials not configured. Integration tests will be skipped.");
                Console.WriteLine("Configure via appsettings.json or environment variables:");
                Console.WriteLine("  - KINDE_DOMAIN");
                Console.WriteLine("  - KINDE_CLIENT_ID");
                Console.WriteLine("  - KINDE_CLIENT_SECRET");
                Console.WriteLine("  - KINDE_AUDIENCE");
                Console.WriteLine("Or set USE_MOCK_MODE=true for CI/CD testing");
                return;
            }

            Domain = domain;
            IsConfigured = true;
            UseMockMode = false;

            // Get access token synchronously (xUnit doesn't support async in constructor)
            try
            {
                var task = M2MAuthenticationHelper.GetAccessTokenAsync(
                    domain, clientId, clientSecret, audience, scope);
                AccessToken = task.GetAwaiter().GetResult();

                if (string.IsNullOrWhiteSpace(AccessToken))
                {
                    IsConfigured = false;
                    Console.WriteLine("WARNING: Failed to obtain access token. Integration tests will be skipped.");
                }
                else
                {
                    Console.WriteLine("✓ M2M authentication successful for integration tests (REAL API mode)");
                }
            }
            catch (Exception ex)
            {
                IsConfigured = false;
                Console.WriteLine($"WARNING: Failed to authenticate: {ex.Message}. Integration tests will be skipped.");
            }
        }

        public void Dispose()
        {
            // Cleanup if needed
        }
    }
}

