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
            // Get the project directory (where appsettings.json is located).
            // Tests run from bin/<Configuration>/<TargetFramework>, so walk up to the project root.
            // The directory that holds the .csproj is the project root, which avoids matching
            // target-framework folder names one by one - a list like that silently stops working
            // the moment a new target framework is added, leaving every real-API test skipped.
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
            
            // Debug logging - check all sources
            var envMode = Environment.GetEnvironmentVariable("KINDE_TEST_MODE") ?? Environment.GetEnvironmentVariable("TestMode");
            var configMode = _configuration["TestMode"];
            var configKindeMode = _configuration["KindeManagementApi:Domain"] != null ? "configured" : "not configured";
            
            Console.WriteLine($"[TEST CONFIG] Environment KINDE_TEST_MODE: {envMode ?? "not set"}");
            Console.WriteLine($"[TEST CONFIG] Environment TestMode: {Environment.GetEnvironmentVariable("TestMode") ?? "not set"}");
            Console.WriteLine($"[TEST CONFIG] Config TestMode: {configMode ?? "not set"}");
            Console.WriteLine($"[TEST CONFIG] Config KindeManagementApi: {configKindeMode}");
            
            // Get final test mode
            var testMode = GetValue("KINDE_TEST_MODE") ?? GetValue("TestMode") ?? "Mock";
            Console.WriteLine($"[TEST CONFIG] Final Test Mode: {testMode}");
            Console.WriteLine($"[TEST CONFIG] UseRealApi: {testMode.Equals("Real", StringComparison.OrdinalIgnoreCase)}");
            
            // Log configuration source
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

