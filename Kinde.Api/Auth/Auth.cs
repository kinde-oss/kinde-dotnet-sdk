using Kinde.Api.Client;
using Kinde.Api.Accounts;
using Kinde.Accounts.Api;
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
        private readonly Permissions _permissions;
        private readonly FeatureFlags _featureFlags;
        private readonly Roles _roles;
        private readonly Entitlements _entitlements;
        private readonly KindeClient _client;
        private readonly IKindeAccountsClient _accountsClient;

        public Auth(KindeClient client, HttpClient httpClient, bool forceApi, ILogger logger = null) : base(forceApi, logger)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
            if (httpClient == null) throw new ArgumentNullException(nameof(httpClient));
            
            // Only create the accounts client if ForceApi is enabled
            _accountsClient = forceApi ? CreateAccountsClient(httpClient) : null;
            
            _claims = new Claims(client, forceApi, logger);
            _permissions = new Permissions(client, _accountsClient, forceApi, logger);
            _featureFlags = new FeatureFlags(client, _accountsClient, forceApi, logger);
            _roles = new Roles(client, _accountsClient, forceApi, logger);
            _entitlements = new Entitlements(client, _accountsClient, forceApi, logger);
        }

        /// <summary>
        /// Gets the accounts client, creating it if necessary when ForceApi is enabled
        /// </summary>
        /// <param name="httpClient">The HttpClient to use for API calls</param>
        /// <returns>IKindeAccountsClient instance or null if ForceApi is disabled</returns>
        private IKindeAccountsClient GetOrCreateAccountsClient(HttpClient httpClient)
        {
            if (!ShouldUseApi())
                return null;
                
            if (_accountsClient == null)
            {
                return CreateAccountsClient(httpClient);
            }
            
            return _accountsClient;
        }

        /// <summary>
        /// Creates the accounts client internally with bearer token authentication
        /// </summary>
        /// <param name="httpClient">The HttpClient to use for API calls</param>
        /// <returns>IKindeAccountsClient instance</returns>
        private IKindeAccountsClient CreateAccountsClient(HttpClient httpClient)
        {
            // Create a logger factory for the accounts client
            var loggerFactory = LoggerFactory.Create(builder => builder.AddConsole().SetMinimumLevel(LogLevel.Warning));
            
            // Create a bearer token provider that uses the current access token
            var bearerTokenContainer = new Kinde.Accounts.Client.TokenContainer<Kinde.Accounts.Client.BearerToken>(new[] 
            {
                new Kinde.Accounts.Client.BearerToken(_client.Token?.AccessToken ?? throw new InvalidOperationException("No access token available for API calls"))
            });
            var bearerTokenProvider = new Kinde.Accounts.Client.RateLimitProvider<Kinde.Accounts.Client.BearerToken>(bearerTokenContainer);
            
            // Create the individual API clients with bearer token provider
            var billingApi = new BillingApi(loggerFactory.CreateLogger<BillingApi>(), httpClient, null, new BillingApiEvents(), bearerTokenProvider);
            var permissionsApi = new PermissionsApi(loggerFactory.CreateLogger<PermissionsApi>(), httpClient, null, new PermissionsApiEvents(), bearerTokenProvider);
            var rolesApi = new RolesApi(loggerFactory.CreateLogger<RolesApi>(), httpClient, null, new RolesApiEvents(), bearerTokenProvider);
            var featureFlagsApi = new FeatureFlagsApi(loggerFactory.CreateLogger<FeatureFlagsApi>(), httpClient, null, new FeatureFlagsApiEvents(), bearerTokenProvider);
            
            // Create the accounts client with the individual API clients
            return new KindeAccountsClient(loggerFactory, billingApi, permissionsApi, rolesApi, featureFlagsApi);
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
        public Permissions Permissions()
        {
            return _permissions;
        }

        /// <summary>
        /// Get access to feature flags functionality.
        /// </summary>
        /// <returns>FeatureFlags instance for accessing feature flags</returns>
        public FeatureFlags FeatureFlags()
        {
            return _featureFlags;
        }

        /// <summary>
        /// Get access to roles functionality.
        /// </summary>
        /// <returns>Roles instance for checking user roles</returns>
        public Roles Roles()
        {
            return _roles;
        }

        /// <summary>
        /// Get access to entitlements functionality.
        /// </summary>
        /// <returns>Entitlements instance for accessing user entitlements</returns>
        public Entitlements Entitlements()
        {
            return _entitlements;
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
        public override IKindeAccountsClient GetAccountsClient()
        {
            return _accountsClient;
        }
    }
}
