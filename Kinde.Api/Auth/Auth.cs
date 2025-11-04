using Kinde.Api.Client;
// NOTE: Temporarily disabled for v6 testing - Accounts API was generated with v7
// using Kinde.Api.Accounts;
// using Kinde.Accounts.Api;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;

namespace Kinde.Api.Auth
{
    /// <summary>
    /// Main authentication client that provides access to all authentication-related functionality.
    /// This follows the same pattern as the Java SDK's main Auth class.
    /// </summary>
    public class Auth : BaseAuth
    {
        private readonly Claims _claims;
        // NOTE: Temporarily disabled for v6 testing - these depend on Accounts API which was generated with v7
        // private readonly Permissions _permissions;
        // private readonly FeatureFlags _featureFlags;
        // private readonly Roles _roles;
        // private readonly Entitlements _entitlements;
        private readonly object? _permissions;
        private readonly object? _featureFlags;
        private readonly object? _roles;
        private readonly object? _entitlements;
        private readonly KindeClient _client;
        // NOTE: Temporarily disabled for v6 testing - Accounts API was generated with v7
        // private readonly IKindeAccountsClient _accountsClient;
        private readonly object? _accountsClient;

        public Auth(KindeClient client, HttpClient httpClient, bool forceApi, ILogger logger = null) : base(forceApi, logger)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
            if (httpClient == null) throw new ArgumentNullException(nameof(httpClient));
            
            // Only create the accounts client if ForceApi is enabled
            _accountsClient = forceApi ? CreateAccountsClient(httpClient) : null;
            
            _claims = new Claims(client, forceApi, logger);
            // NOTE: Temporarily disabled for v6 testing - these depend on Accounts API which was generated with v7
            // _permissions = new Permissions(client, _accountsClient, forceApi, logger);
            // _featureFlags = new FeatureFlags(client, _accountsClient, forceApi, logger);
            // _roles = new Roles(client, _accountsClient, forceApi, logger);
            // _entitlements = new Entitlements(client, _accountsClient, forceApi, logger);
            _permissions = null;
            _featureFlags = null;
            _roles = null;
            _entitlements = null;
        }

        /// <summary>
        /// Gets the accounts client, creating it if necessary when ForceApi is enabled
        /// </summary>
        /// <param name="httpClient">The HttpClient to use for API calls</param>
        /// <returns>An accounts client instance or null if ForceApi is disabled</returns>
        /// <remarks>
        /// NOTE: Temporarily disabled for v6 testing - Accounts API was generated with v7
        /// </remarks>
        private object GetOrCreateAccountsClient(HttpClient httpClient)
        {
            throw new NotImplementedException("Accounts API functionality is temporarily disabled for v6 testing. Accounts API needs to be regenerated with OpenAPI Generator v6.");
        }

        /// <summary>
        /// Creates the accounts client internally with bearer token authentication
        /// </summary>
        /// <param name="httpClient">The HttpClient to use for API calls</param>
        /// <returns>An accounts client instance</returns>
        /// <remarks>
        /// NOTE: Accounts API was generated with OpenAPI Generator v7, but we're testing with v6.
        /// This method is temporarily disabled to allow Management API testing with v6.
        /// The Accounts API needs to be regenerated with v6 to work properly.
        /// </remarks>
        private object CreateAccountsClient(HttpClient httpClient)
        {
            // TODO: Accounts API was generated with OpenAPI Generator v7 and uses HttpClient-based constructors
            // that don't exist in v6. The v6 version uses RestSharp with Configuration objects.
            // This needs to be regenerated with v6 or updated to use v6-style constructors.
            
            throw new NotImplementedException("Accounts API functionality is temporarily disabled for v6 testing. Accounts API needs to be regenerated with OpenAPI Generator v6.");
            
            /*
            // Create a logger factory for the accounts client
            var loggerFactory = LoggerFactory.Create(builder => builder.AddConsole().SetMinimumLevel(LogLevel.Warning));
            
            // Create a bearer token provider that uses the current access token
            var bearerTokenContainer = new Kinde.Accounts.Client.TokenContainer<Kinde.Accounts.Client.BearerToken>(new[] 
            {
                new Kinde.Accounts.Client.BearerToken(_client.Token?.AccessToken ?? throw new InvalidOperationException("No access token available for API calls"))
            });
            var bearerTokenProvider = new Kinde.Accounts.Client.RateLimitProvider<Kinde.Accounts.Client.BearerToken>(bearerTokenContainer);
            
            // Create the individual API clients with bearer token provider
            // NOTE: These constructors don't exist in v6 - they use Configuration instead
            var billingApi = new BillingApi(loggerFactory.CreateLogger<BillingApi>(), loggerFactory, httpClient, null, new BillingApiEvents(), bearerTokenProvider);
            var permissionsApi = new PermissionsApi(loggerFactory.CreateLogger<PermissionsApi>(), loggerFactory, httpClient, null, new PermissionsApiEvents(), bearerTokenProvider);
            var rolesApi = new RolesApi(loggerFactory.CreateLogger<RolesApi>(), loggerFactory, httpClient, null, new RolesApiEvents(), bearerTokenProvider);
            var featureFlagsApi = new FeatureFlagsApi(loggerFactory.CreateLogger<FeatureFlagsApi>(), loggerFactory, httpClient, null, new FeatureFlagsApiEvents(), bearerTokenProvider);
            
            // Create the accounts client with the individual API clients
            return new KindeAccountsClient(loggerFactory, billingApi, permissionsApi, rolesApi, featureFlagsApi);
            */
        }

        /// <summary>
        /// Get access to claims functionality.
        /// </summary>
        /// <returns>Claims instance for accessing token claims</returns>
        public Claims Claims()
        {
            return _claims;
        }

        /// <summary>
        /// Get access to permissions functionality.
        /// </summary>
        /// <returns>Permissions instance for checking user permissions</returns>
        public object Permissions()
        {
            throw new NotImplementedException("Permissions functionality is temporarily disabled for v6 testing. Accounts API needs to be regenerated with OpenAPI Generator v6.");
        }

        /// <summary>
        /// Get access to feature flags functionality.
        /// </summary>
        /// <returns>FeatureFlags instance for accessing feature flags</returns>
        public object FeatureFlags()
        {
            throw new NotImplementedException("FeatureFlags functionality is temporarily disabled for v6 testing. Accounts API needs to be regenerated with OpenAPI Generator v6.");
        }

        /// <summary>
        /// Get access to roles functionality.
        /// </summary>
        /// <returns>Roles instance for checking user roles</returns>
        public object Roles()
        {
            throw new NotImplementedException("Roles functionality is temporarily disabled for v6 testing. Accounts API needs to be regenerated with OpenAPI Generator v6.");
        }

        /// <summary>
        /// Get access to entitlements functionality.
        /// </summary>
        /// <returns>Entitlements instance for accessing user entitlements</returns>
        public object Entitlements()
        {
            throw new NotImplementedException("Entitlements functionality is temporarily disabled for v6 testing. Accounts API needs to be regenerated with OpenAPI Generator v6.");
        }

        /// <summary>
        /// Gets the KindeClient from the current context.
        /// </summary>
        /// <returns>The KindeClient instance if available</returns>
        protected override KindeClient GetClient()
        {
            return _client;
        }

        /// <summary>
        /// Gets an accounts client for API calls.
        /// </summary>
        /// <returns>An accounts client instance</returns>
        public override object GetAccountsClient()
        {
            throw new NotImplementedException("Accounts API functionality is temporarily disabled for v6 testing. Accounts API needs to be regenerated with OpenAPI Generator v6.");
        }
    }
}
