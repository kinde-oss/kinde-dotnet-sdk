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
            // Get the project directory (where appsettings.json is located)
            // Tests run from bin/Debug/net8.0, so we need to go up to the project directory
            var currentDir = Directory.GetCurrentDirectory();
            var projectDir = currentDir;
            
            // If we're in bin/Debug/net8.0, go up to the project directory
            if (currentDir.Contains("bin" + Path.DirectorySeparatorChar + "Debug") || 
                currentDir.Contains("bin" + Path.DirectorySeparatorChar + "Release"))
            {
                var dir = new DirectoryInfo(currentDir);
                // Go up from bin/Debug/net8.0 to project root
                // Matches any target framework folder, not just net8.0: the project multi-targets,
                // and a hard-coded TFM leaves the walk stranded in bin/Debug/net10.0, where no
                // appsettings.json is found and every real-API test silently falls back to Mock.
                while (dir != null && (dir.Name == "Debug" || dir.Name == "Release" || dir.Name == "bin"
                                       || System.Text.RegularExpressions.Regex.IsMatch(
                                           dir.Name, @"^net(standard|coreapp)?\d",
                                           System.Text.RegularExpressions.RegexOptions.IgnoreCase)))
                {
                    dir = dir.Parent;
                }
                if (dir != null)
                {
                    projectDir = dir.FullName;
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
        /// Kinde AI agents and MCP application configuration, used by the live agent auth tests.
        /// Returns null when the section is absent, so those tests skip instead of failing.
        /// </summary>
        public Kinde.Api.Agents.KindeAgentAuthOptions? AgentAuth
        {
            get
            {
                var domain = GetValue("KINDE_TEST_AGENT_DOMAIN") ?? GetValue("KindeAgentAuth:Domain");
                var clientId = GetValue("KINDE_TEST_AGENT_CLIENT_ID") ?? GetValue("KindeAgentAuth:ClientId");
                var clientSecret = GetValue("KINDE_TEST_AGENT_CLIENT_SECRET") ?? GetValue("KindeAgentAuth:ClientSecret");
                var resourceUrl = GetValue("KINDE_TEST_AGENT_RESOURCE_URL") ?? GetValue("KindeAgentAuth:ResourceUrl");

                if (string.IsNullOrWhiteSpace(domain) || string.IsNullOrWhiteSpace(clientId)
                    || string.IsNullOrWhiteSpace(resourceUrl))
                {
                    return null;
                }

                return new Kinde.Api.Agents.KindeAgentAuthOptions
                {
                    Domain = domain!,
                    ClientId = clientId!,
                    ClientSecret = clientSecret ?? string.Empty,
                    ResourceUrl = resourceUrl!
                };
            }
        }

        /// <summary>
        /// An access token issued to an agent, for the live verification tests. There is no way to
        /// mint one without a browser consent step, so it is supplied rather than fetched.
        /// </summary>
        public string? AgentAccessToken =>
            GetValue("KINDE_TEST_AGENT_ACCESS_TOKEN") ?? GetValue("KindeAgentAuth:AccessToken");

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

