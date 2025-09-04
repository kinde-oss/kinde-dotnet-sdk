using Kinde.Api.Client;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

        public Auth(KindeClient client = null, ILogger logger = null, 
                   Permissions permissions = null, Roles roles = null,
                   FeatureFlags featureFlags = null, Entitlements entitlements = null) : base(logger)
        {
            _client = client;
            _claims = new Claims(client, logger);
            _permissions = permissions ?? new Permissions(client, logger);
            _featureFlags = featureFlags ?? new FeatureFlags(client, logger);
            _roles = roles ?? new Roles(client, logger);
            _entitlements = entitlements ?? new Entitlements(client, logger);
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
        /// Comprehensive hard check: true only if ALL requirements are met across all types.
        /// This method checks permissions, roles, and feature flags simultaneously.
        /// </summary>
        /// <param name="permissions">Required permissions (null/empty = no permission requirement)</param>
        /// <param name="roles">Required roles (null/empty = no role requirement)</param>
        /// <param name="featureFlags">Required feature flags (null/empty = no feature flag requirement)</param>
        /// <returns>True if ALL requirements are met, false otherwise</returns>
        public async Task<bool> HasAllAsync(List<string> permissions = null, List<string> roles = null, List<string> featureFlags = null)
        {
            try
            {
                // Check permissions
                if (permissions != null && permissions.Any())
                {
                    bool permissionsOk = await _permissions.HasAllPermissionsAsync(permissions);
                    if (!permissionsOk)
                    {
                        _logger?.LogDebug("HasAll check failed: not all permissions are satisfied");
                        return false;
                    }
                }

                // Check roles
                if (roles != null && roles.Any())
                {
                    bool rolesOk = await _roles.HasAllRolesAsync(roles);
                    if (!rolesOk)
                    {
                        _logger?.LogDebug("HasAll check failed: not all roles are satisfied");
                        return false;
                    }
                }

                // Check feature flags
                if (featureFlags != null && featureFlags.Any())
                {
                    foreach (string flag in featureFlags)
                    {
                        if (!await _featureFlags.IsFeatureFlagEnabledAsync(flag))
                        {
                            _logger?.LogDebug("HasAll check failed: feature flag '{Flag}' is not enabled", flag);
                            return false;
                        }
                    }
                }

                _logger?.LogDebug("HasAll check passed: all requirements are satisfied");
                return true;
            }
            catch (Exception e)
            {
                _logger?.LogError(e, "Error in comprehensive HasAll check");
                return false;
            }
        }

        /// <summary>
        /// Comprehensive hard check: true if ANY requirements are met across all types.
        /// This method checks permissions, roles, and feature flags simultaneously.
        /// </summary>
        /// <param name="permissions">Required permissions (null/empty = no permission requirement)</param>
        /// <param name="roles">Required roles (null/empty = no role requirement)</param>
        /// <param name="featureFlags">Required feature flags (null/empty = no feature flag requirement)</param>
        /// <returns>True if ANY requirements are met from each category, false otherwise</returns>
        public async Task<bool> HasAnyAsync(List<string> permissions = null, List<string> roles = null, List<string> featureFlags = null)
        {
            try
            {
                // Check permissions
                bool permissionsOk = (permissions == null || !permissions.Any()) || await _permissions.HasAnyPermissionAsync(permissions);
                if (!permissionsOk)
                {
                    _logger?.LogDebug("HasAny check failed: no permissions are satisfied");
                    return false;
                }

                // Check roles
                bool rolesOk = (roles == null || !roles.Any()) || await _roles.HasAnyRoleAsync(roles);
                if (!rolesOk)
                {
                    _logger?.LogDebug("HasAny check failed: no roles are satisfied");
                    return false;
                }

                // Check feature flags
                if (featureFlags != null && featureFlags.Any())
                {
                    bool anyFlagEnabled = false;
                    foreach (string flag in featureFlags)
                    {
                        if (await _featureFlags.IsFeatureFlagEnabledAsync(flag))
                        {
                            anyFlagEnabled = true;
                            break;
                        }
                    }
                    if (!anyFlagEnabled)
                    {
                        _logger?.LogDebug("HasAny check failed: no feature flags are enabled");
                        return false;
                    }
                }

                _logger?.LogDebug("HasAny check passed: at least one requirement from each category is satisfied");
                return true;
            }
            catch (Exception e)
            {
                _logger?.LogError(e, "Error in comprehensive HasAny check");
                return false;
            }
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
