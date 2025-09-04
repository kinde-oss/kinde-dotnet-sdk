using Kinde.Api.Client;
using Kinde.Api.Models.Tokens;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Kinde.Api.Auth
{
    /// <summary>
    /// Base class for authentication-related functionality that provides
    /// shared methods for accessing the session and token manager.
    /// This follows the same pattern as the Java SDK's BaseAuth class.
    /// </summary>
    public abstract class BaseAuth
    {
        protected readonly ILogger _logger;

        protected BaseAuth(ILogger logger = null)
        {
            _logger = logger;
        }

        /// <summary>
        /// Gets the KindeClient from the current context.
        /// </summary>
        /// <returns>The KindeClient instance if available</returns>
        protected virtual KindeClient GetClient()
        {
            try
            {
                // For now, we'll need to pass the client explicitly
                // In a future implementation, this could use dependency injection
                _logger?.LogDebug("Getting KindeClient from context");
                return null; // This will be overridden by derived classes
            }
            catch (Exception e)
            {
                _logger?.LogDebug("Could not get KindeClient from context: {Message}", e.Message);
                return null;
            }
        }

        /// <summary>
        /// Gets the token for the current user.
        /// </summary>
        /// <returns>The OauthToken if available</returns>
        protected OauthToken GetToken()
        {
            var client = GetClient();
            if (client?.Token == null)
            {
                _logger?.LogDebug("No token available in client");
                return null;
            }
            return client.Token;
        }

        /// <summary>
        /// Gets an accounts client for API calls.
        /// </summary>
        /// <returns>An accounts client instance</returns>
        public object GetAccountsClient()
        {
            throw new NotImplementedException("Accounts API integration is temporarily disabled due to build issues");
        }
    }
}
