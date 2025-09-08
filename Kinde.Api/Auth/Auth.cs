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

        public Auth(KindeClient client, HttpClient httpClient, ILogger logger = null) : base(logger)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
            
            // Create the accounts client internally
            _accountsClient = CreateAccountsClient(httpClient);
            
            _claims = new Claims(client, logger);
            _permissions = new Permissions(client, _accountsClient, logger);
            _featureFlags = new FeatureFlags(client, _accountsClient, logger);
            _roles = new Roles(client, _accountsClient, logger);
            _entitlements = new Entitlements(client, _accountsClient, logger);
        }

        /// <summary>
        /// Creates the accounts client internally
        /// </summary>
        /// <param name="httpClient">The HttpClient to use for API calls</param>
        /// <returns>IKindeAccountsClient instance</returns>
        private IKindeAccountsClient CreateAccountsClient(HttpClient httpClient)
        {
            // Create a logger factory for the accounts client
            var loggerFactory = LoggerFactory.Create(builder => builder.AddConsole().SetMinimumLevel(LogLevel.Warning));
            
            // Create the individual API clients
            var billingApi = new BillingApi(loggerFactory.CreateLogger<BillingApi>(), loggerFactory, httpClient, null, new BillingApiEvents(), null);
            var permissionsApi = new PermissionsApi(loggerFactory.CreateLogger<PermissionsApi>(), loggerFactory, httpClient, null, new PermissionsApiEvents(), null);
            var rolesApi = new RolesApi(loggerFactory.CreateLogger<RolesApi>(), loggerFactory, httpClient, null, new RolesApiEvents(), null);
            var featureFlagsApi = new FeatureFlagsApi(loggerFactory.CreateLogger<FeatureFlagsApi>(), loggerFactory, httpClient, null, new FeatureFlagsApiEvents(), null);
            
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
