using Kinde.Api.Client;
using Microsoft.Extensions.Logging;

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

        public Auth(KindeClient client = null, ILogger logger = null) : base(logger)
        {
            _client = client;
            _claims = new Claims(client, logger);
            _permissions = new Permissions(client, logger);
            _featureFlags = new FeatureFlags(client, logger);
            _roles = new Roles(client, logger);
            _entitlements = new Entitlements(client, logger);
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
    }
}
