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
        protected readonly bool _forceApi;

        protected BaseAuth(bool forceApi, ILogger logger = null)
        {
            _logger = logger;
            _forceApi = forceApi;
        }

        /// <summary>
        /// Gets the KindeClient from the current context.
        /// </summary>
        /// <returns>The KindeClient instance if available</returns>
        protected abstract KindeClient GetClient();

        /// <summary>
        /// Determines if API calls should be used instead of token-based operations.
        /// </summary>
        /// <returns>True if API calls should be used, false for token-based operations</returns>
        protected bool ShouldUseApi()
        {
            return _forceApi;
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
        /// This method should be overridden by derived classes to provide
        /// the proper dependency injection setup.
        /// </summary>
        /// <returns>An accounts client instance</returns>
        /// <remarks>
        /// NOTE: Temporarily changed return type to object for v6 testing - Accounts API was generated with v7
        /// </remarks>
        public virtual object GetAccountsClient()
        {
            _logger?.LogError("GetAccountsClient must be implemented by derived classes with proper dependency injection.");
            throw new NotImplementedException("GetAccountsClient must be implemented by derived classes with proper dependency injection.");
        }
    }
}
