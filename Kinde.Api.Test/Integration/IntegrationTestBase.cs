using Kinde.Api.Api;
using Kinde.Api.Client;
using Kinde.Api.Test.Integration.MockServer;
using Xunit;

namespace Kinde.Api.Test.Integration
{
    /// <summary>
    /// Base class for integration tests that use the OpenAPI Mock Server.
    /// Provides common setup and utilities for testing API serialization/deserialization.
    /// </summary>
    public abstract class IntegrationTestBase : IClassFixture<MockServerFixture>, IDisposable
    {
        protected readonly OpenApiMockServer MockServer;
        protected readonly Configuration ApiConfiguration;
        protected readonly HttpClient HttpClient;

        protected IntegrationTestBase(MockServerFixture fixture)
        {
            MockServer = fixture.MockServer;
            ApiConfiguration = new Configuration
            {
                BasePath = MockServer.BaseUrl,
                AccessToken = "test-token-for-mock-server"
            };
            HttpClient = MockServer.Client;
        }

        /// <summary>
        /// Creates an API client configuration pointing to the mock server
        /// </summary>
        protected Configuration CreateApiConfiguration()
        {
            // Ensure BaseUrl doesn't have a trailing slash to avoid double slashes
            var baseUrl = MockServer.BaseUrl.TrimEnd('/');
            return new Configuration
            {
                BasePath = baseUrl,
                AccessToken = "test-token-for-mock-server"
            };
        }

        /// <summary>
        /// Creates an OrganizationsApi instance using the mock server's HttpClient
        /// </summary>
        protected OrganizationsApi CreateOrganizationsApi()
        {
            var configuration = CreateApiConfiguration();
            return new OrganizationsApi(MockServer.Client, configuration);
        }

        /// <summary>
        /// Creates an APIsApi instance using the mock server's HttpClient
        /// </summary>
        protected APIsApi CreateAPIsApi()
        {
            var configuration = CreateApiConfiguration();
            return new APIsApi(MockServer.Client, configuration);
        }

        /// <summary>
        /// Creates an ApplicationsApi instance using the mock server's HttpClient
        /// </summary>
        protected ApplicationsApi CreateApplicationsApi()
        {
            var configuration = CreateApiConfiguration();
            return new ApplicationsApi(MockServer.Client, configuration);
        }

        /// <summary>
        /// Creates a ConnectionsApi instance using the mock server's HttpClient
        /// </summary>
        protected ConnectionsApi CreateConnectionsApi()
        {
            var configuration = CreateApiConfiguration();
            return new ConnectionsApi(MockServer.Client, configuration);
        }

        /// <summary>
        /// Creates a UsersApi instance using the mock server's HttpClient
        /// </summary>
        protected UsersApi CreateUsersApi()
        {
            var configuration = CreateApiConfiguration();
            return new UsersApi(MockServer.Client, configuration);
        }

        /// <summary>
        /// Creates a RolesApi instance using the mock server's HttpClient
        /// </summary>
        protected RolesApi CreateRolesApi()
        {
            var configuration = CreateApiConfiguration();
            return new RolesApi(MockServer.Client, configuration);
        }

        /// <summary>
        /// Creates a PermissionsApi instance using the mock server's HttpClient
        /// </summary>
        protected PermissionsApi CreatePermissionsApi()
        {
            var configuration = CreateApiConfiguration();
            return new PermissionsApi(MockServer.Client, configuration);
        }

        /// <summary>
        /// Creates a FeatureFlagsApi instance using the mock server's HttpClient
        /// </summary>
        protected FeatureFlagsApi CreateFeatureFlagsApi()
        {
            var configuration = CreateApiConfiguration();
            return new FeatureFlagsApi(MockServer.Client, configuration);
        }

        /// <summary>
        /// Creates an EnvironmentsApi instance using the mock server's HttpClient
        /// </summary>
        protected EnvironmentsApi CreateEnvironmentsApi()
        {
            var configuration = CreateApiConfiguration();
            return new EnvironmentsApi(MockServer.Client, configuration);
        }

        /// <summary>
        /// Creates an EnvironmentVariablesApi instance using the mock server's HttpClient
        /// </summary>
        protected EnvironmentVariablesApi CreateEnvironmentVariablesApi()
        {
            var configuration = CreateApiConfiguration();
            return new EnvironmentVariablesApi(MockServer.Client, configuration);
        }

        /// <summary>
        /// Creates a BusinessApi instance using the mock server's HttpClient
        /// </summary>
        protected BusinessApi CreateBusinessApi()
        {
            var configuration = CreateApiConfiguration();
            return new BusinessApi(MockServer.Client, configuration);
        }

        /// <summary>
        /// Creates a BillingAgreementsApi instance using the mock server's HttpClient
        /// </summary>
        protected BillingAgreementsApi CreateBillingAgreementsApi()
        {
            var configuration = CreateApiConfiguration();
            return new BillingAgreementsApi(MockServer.Client, configuration);
        }

        /// <summary>
        /// Creates a BillingEntitlementsApi instance using the mock server's HttpClient
        /// </summary>
        protected BillingEntitlementsApi CreateBillingEntitlementsApi()
        {
            var configuration = CreateApiConfiguration();
            return new BillingEntitlementsApi(MockServer.Client, configuration);
        }

        /// <summary>
        /// Creates a BillingMeterUsageApi instance using the mock server's HttpClient
        /// </summary>
        protected BillingMeterUsageApi CreateBillingMeterUsageApi()
        {
            var configuration = CreateApiConfiguration();
            return new BillingMeterUsageApi(MockServer.Client, configuration);
        }

        /// <summary>
        /// Creates a CallbacksApi instance using the mock server's HttpClient
        /// </summary>
        protected CallbacksApi CreateCallbacksApi()
        {
            var configuration = CreateApiConfiguration();
            return new CallbacksApi(MockServer.Client, configuration);
        }

        /// <summary>
        /// Creates a ConnectedAppsApi instance using the mock server's HttpClient
        /// </summary>
        protected ConnectedAppsApi CreateConnectedAppsApi()
        {
            var configuration = CreateApiConfiguration();
            return new ConnectedAppsApi(MockServer.Client, configuration);
        }

        /// <summary>
        /// Creates an IdentitiesApi instance using the mock server's HttpClient
        /// </summary>
        protected IdentitiesApi CreateIdentitiesApi()
        {
            var configuration = CreateApiConfiguration();
            return new IdentitiesApi(MockServer.Client, configuration);
        }

        /// <summary>
        /// Creates an IndustriesApi instance using the mock server's HttpClient
        /// </summary>
        protected IndustriesApi CreateIndustriesApi()
        {
            var configuration = CreateApiConfiguration();
            return new IndustriesApi(MockServer.Client, configuration);
        }

        /// <summary>
        /// Creates an MFAApi instance using the mock server's HttpClient
        /// </summary>
        protected MFAApi CreateMFAApi()
        {
            var configuration = CreateApiConfiguration();
            return new MFAApi(MockServer.Client, configuration);
        }

        public virtual void Dispose()
        {
            // Don't dispose HttpClient here - it's managed by MockServer
        }
    }

    /// <summary>
    /// XUnit fixture that provides a shared OpenAPI Mock Server instance across all tests
    /// </summary>
    public class MockServerFixture : IDisposable
    {
        public OpenApiMockServer MockServer { get; }

        public MockServerFixture()
        {
            var specPath = Path.Combine(
                AppDomain.CurrentDomain.BaseDirectory,
                "api",
                "openapi.yaml"
            );

            // Fallback to relative path if not found in output directory
            if (!File.Exists(specPath))
            {
                specPath = Path.Combine(
                    Directory.GetCurrentDirectory(),
                    "..", "..", "..", "..",
                    "api",
                    "openapi.yaml"
                );
            }

            MockServer = new OpenApiMockServer(specPath);
        }

        public void Dispose()
        {
            MockServer?.Dispose();
        }
    }
}

