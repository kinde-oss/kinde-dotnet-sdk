using System;
using System.IO;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;

namespace Kinde.Api.Test.Integration
{
    /// <summary>
    /// Configuration for integration tests.
    /// Reads from environment variables first, then falls back to appsettings.json (if exists).
    /// </summary>
    public class TestConfiguration
    {
        private static readonly Lazy<TestConfiguration> _lazyInstance = new Lazy<TestConfiguration>(() => new TestConfiguration());
        private readonly IConfiguration _configuration;

        private TestConfiguration()
        {
            // Tests run from bin/<Configuration>/<TargetFramework>; walk up to the directory holding the .csproj. Matching
            // target-framework folder names instead would silently skip every real-API test once a new TFM is added.
            var currentDir = Directory.GetCurrentDirectory();
            var projectDir = currentDir;

            for (var dir = new DirectoryInfo(currentDir); dir != null; dir = dir.Parent)
            {
                if (dir.GetFiles("*.csproj").Length > 0)
                {
                    projectDir = dir.FullName;
                    break;
                }
            }

            Console.WriteLine($"[TEST CONFIG] Current directory: {currentDir}");
            Console.WriteLine($"[TEST CONFIG] Project directory: {projectDir}");
            
            var appsettingsPath = Path.Combine(projectDir, "appsettings.json");
            var appsettingsExists = File.Exists(appsettingsPath);
            Console.WriteLine($"[TEST CONFIG] appsettings.json path: {appsettingsPath}");
            Console.WriteLine($"[TEST CONFIG] appsettings.json exists: {appsettingsExists}");
            
            var builder = new ConfigurationBuilder()
                .SetBasePath(projectDir)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: false)
                .AddJsonFile("appsettings.Development.json", optional: true, reloadOnChange: false)
                .AddEnvironmentVariables("KINDE_TEST_");

            _configuration = builder.Build();
            
            var envMode = Environment.GetEnvironmentVariable("KINDE_TEST_MODE") ?? Environment.GetEnvironmentVariable("TestMode");
            var configMode = _configuration["TestMode"];
            var configKindeMode = _configuration["KindeManagementApi:Domain"] != null ? "configured" : "not configured";
            
            Console.WriteLine($"[TEST CONFIG] Environment KINDE_TEST_MODE: {envMode ?? "not set"}");
            Console.WriteLine($"[TEST CONFIG] Environment TestMode: {Environment.GetEnvironmentVariable("TestMode") ?? "not set"}");
            Console.WriteLine($"[TEST CONFIG] Config TestMode: {configMode ?? "not set"}");
            Console.WriteLine($"[TEST CONFIG] Config KindeManagementApi: {configKindeMode}");
            
            var testMode = GetValue("KINDE_TEST_MODE") ?? GetValue("TestMode") ?? "Mock";
            Console.WriteLine($"[TEST CONFIG] Final Test Mode: {testMode}");
            Console.WriteLine($"[TEST CONFIG] UseRealApi: {testMode.Equals("Real", StringComparison.OrdinalIgnoreCase)}");
            
            if (!string.IsNullOrEmpty(envMode))
            {
                Console.WriteLine($"[TEST CONFIG] Configuration source: Environment variable");
            }
            else if (!string.IsNullOrEmpty(configMode))
            {
                Console.WriteLine($"[TEST CONFIG] Configuration source: appsettings.json");
            }
            else
            {
                Console.WriteLine($"[TEST CONFIG] Configuration source: Default (Mock)");
            }
        }

        public static TestConfiguration Instance => _lazyInstance.Value;

        /// <summary>
        /// Kinde Management API configuration
        /// </summary>
        public ManagementApiConfig ManagementApi => new ManagementApiConfig
        {
            Domain = GetValue("KINDE_TEST_MANAGEMENT_DOMAIN") ?? GetValue("KindeManagementApi:Domain") ?? throw new InvalidOperationException("KINDE_TEST_MANAGEMENT_DOMAIN or KindeManagementApi:Domain must be set"),
            ClientId = GetValue("KINDE_TEST_MANAGEMENT_CLIENT_ID") ?? GetValue("KindeManagementApi:ClientId") ?? throw new InvalidOperationException("KINDE_TEST_MANAGEMENT_CLIENT_ID or KindeManagementApi:ClientId must be set"),
            ClientSecret = GetValue("KINDE_TEST_MANAGEMENT_CLIENT_SECRET") ?? GetValue("KindeManagementApi:ClientSecret") ?? throw new InvalidOperationException("KINDE_TEST_MANAGEMENT_CLIENT_SECRET or KindeManagementApi:ClientSecret must be set"),
            Audience = GetValue("KINDE_TEST_MANAGEMENT_AUDIENCE") ?? GetValue("KindeManagementApi:Audience") ?? throw new InvalidOperationException("KINDE_TEST_MANAGEMENT_AUDIENCE or KindeManagementApi:Audience must be set")
        };

        /// <summary>
        /// Test mode: "Mock" or "Real"
        /// </summary>
        public string TestMode => GetValue("KINDE_TEST_MODE") ?? GetValue("TestMode") ?? "Mock";

        /// <summary>
        /// Whether to use real API (true) or mocks (false)
        /// </summary>
        public bool UseRealApi => TestMode.Equals("Real", StringComparison.OrdinalIgnoreCase);

        private string? GetValue(string key)
        {
            // Try environment variable first (with KINDE_TEST_ prefix)
            var envKey = key.StartsWith("KINDE_TEST_") ? key : $"KINDE_TEST_{key}";
            var envValue = Environment.GetEnvironmentVariable(envKey);
            if (!string.IsNullOrEmpty(envValue))
            {
                return envValue;
            }

            // Try without prefix
            envValue = Environment.GetEnvironmentVariable(key);
            if (!string.IsNullOrEmpty(envValue))
            {
                return envValue;
            }

            // Try configuration
            return _configuration[key];
        }
    }

    public class ManagementApiConfig
    {
        public string Domain { get; set; } = string.Empty;
        public string ClientId { get; set; } = string.Empty;
        public string ClientSecret { get; set; } = string.Empty;
        public string Audience { get; set; } = string.Empty;
    }
}

