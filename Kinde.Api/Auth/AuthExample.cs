using Kinde.Api.Client;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Kinde.Api.Auth
{
    /// <summary>
    /// Example demonstrating how to use the Auth wrapper classes.
    /// This follows the same pattern as the Java SDK's AuthExample.
    /// </summary>
    public class AuthExample
    {
        private readonly Auth _auth;
        private readonly ILogger _logger;

        public AuthExample(KindeClient client, ILogger logger = null)
        {
            _auth = new Auth(client, logger);
            _logger = logger;
        }

        /// <summary>
        /// Example of using claims functionality.
        /// </summary>
        public async Task DemonstrateClaimsAsync()
        {
            Console.WriteLine("=== Claims Example ===");

            // Get a specific claim
            var userClaim = _auth.Claims().GetClaim("sub");
            Console.WriteLine($"User claim: {userClaim}");

            // Get all claims
            var allClaims = _auth.Claims().GetAllClaims();
            Console.WriteLine($"All claims count: {allClaims.Count}");

            // Get typed claims
            var email = _auth.Claims().GetClaim<string>("email");
            Console.WriteLine($"Email: {email}");

            // Check if claim exists
            var hasEmail = _auth.Claims().HasClaim("email");
            Console.WriteLine($"Has email claim: {hasEmail}");
        }

        /// <summary>
        /// Example of using permissions functionality.
        /// </summary>
        public async Task DemonstratePermissionsAsync()
        {
            Console.WriteLine("=== Permissions Example ===");

            // Check if user has a specific permission
            var hasPermission = await _auth.Permissions().HasPermissionAsync("create:todos");
            Console.WriteLine($"Has create:todos permission: {hasPermission}");

            // Check if user has any of multiple permissions
            var permissionKeys = new List<string> { "create:todos", "read:todos", "update:todos" };
            var hasAnyPermission = await _auth.Permissions().HasAnyPermissionAsync(permissionKeys);
            Console.WriteLine($"Has any permission: {hasAnyPermission}");

            // Check if user has all permissions
            var hasAllPermissions = await _auth.Permissions().HasAllPermissionsAsync(permissionKeys);
            Console.WriteLine($"Has all permissions: {hasAllPermissions}");

            // Get all permissions
            var allPermissions = await _auth.Permissions().GetPermissionsAsync();
            Console.WriteLine($"All permissions count: {allPermissions.Count}");
        }

        /// <summary>
        /// Example of using feature flags functionality.
        /// </summary>
        public async Task DemonstrateFeatureFlagsAsync()
        {
            Console.WriteLine("=== Feature Flags Example ===");

            // Check if a feature flag is enabled
            var isEnabled = await _auth.FeatureFlags().IsFeatureFlagEnabledAsync("new_ui");
            Console.WriteLine($"New UI feature flag enabled: {isEnabled}");

            // Get feature flag value
            var flagValue = await _auth.FeatureFlags().GetFeatureFlagValueAsync("max_items");
            Console.WriteLine($"Max items flag value: {flagValue}");

            // Get typed feature flag values
            var stringFlag = await _auth.FeatureFlags().GetFeatureFlagStringAsync("theme");
            Console.WriteLine($"Theme flag: {stringFlag}");

            var intFlag = await _auth.FeatureFlags().GetFeatureFlagIntegerAsync("max_items");
            Console.WriteLine($"Max items flag: {intFlag}");

            // Get all feature flags
            var allFlags = await _auth.FeatureFlags().GetFeatureFlagsAsync();
            Console.WriteLine($"All feature flags count: {allFlags.Count}");
        }

        /// <summary>
        /// Example of using roles functionality.
        /// </summary>
        public async Task DemonstrateRolesAsync()
        {
            Console.WriteLine("=== Roles Example ===");

            // Check if user has a specific role
            var isAdmin = await _auth.Roles().HasRoleAsync("admin");
            Console.WriteLine($"Is admin: {isAdmin}");

            // Check if user has any of multiple roles
            var roleKeys = new List<string> { "admin", "moderator", "user" };
            var hasAnyRole = await _auth.Roles().HasAnyRoleAsync(roleKeys);
            Console.WriteLine($"Has any role: {hasAnyRole}");

            // Check if user has all roles
            var hasAllRoles = await _auth.Roles().HasAllRolesAsync(roleKeys);
            Console.WriteLine($"Has all roles: {hasAllRoles}");

            // Get all roles
            var allRoles = await _auth.Roles().GetRolesAsync();
            Console.WriteLine($"All roles count: {allRoles.Count}");
        }

        /// <summary>
        /// Example of using entitlements functionality.
        /// </summary>
        public async Task DemonstrateEntitlementsAsync()
        {
            Console.WriteLine("=== Entitlements Example ===");

            // Check if user has a specific entitlement
            var hasPremium = await _auth.Entitlements().HasEntitlementAsync("premium_features");
            Console.WriteLine($"Has premium features: {hasPremium}");

            // Check if user has any of multiple entitlements
            var entitlementKeys = new List<string> { "premium_features", "advanced_analytics" };
            var hasAnyEntitlement = await _auth.Entitlements().HasAnyEntitlementAsync(entitlementKeys);
            Console.WriteLine($"Has any entitlement: {hasAnyEntitlement}");

            // Check if user has all entitlements
            var hasAllEntitlements = await _auth.Entitlements().HasAllEntitlementsAsync(entitlementKeys);
            Console.WriteLine($"Has all entitlements: {hasAllEntitlements}");

            // Get all entitlements
            var allEntitlements = await _auth.Entitlements().GetAllEntitlementsAsync();
            Console.WriteLine($"All entitlements count: {allEntitlements.Count}");

            // Get a specific entitlement with full details
            var entitlement = await _auth.Entitlements().GetEntitlementAsync("premium_features");
            if (entitlement != null)
            {
                Console.WriteLine($"Premium features entitlement: {entitlement.GetValueOrDefault("name", "Unknown")}");
            }
        }

        /// <summary>
        /// Example of complex access control combining multiple wrappers.
        /// </summary>
        public async Task DemonstrateComplexAccessControlAsync()
        {
            Console.WriteLine("=== Complex Access Control Example ===");

            // Check if user can access a premium feature
            var canAccessPremium = await _auth.Permissions().HasPermissionAsync("access:premium") &&
                                  await _auth.FeatureFlags().IsFeatureFlagEnabledAsync("premium_features") &&
                                  await _auth.Entitlements().HasEntitlementAsync("premium_features");
            Console.WriteLine($"Can access premium: {canAccessPremium}");

            // Check if user is an admin or has specific permissions
            var isAuthorized = await _auth.Roles().HasRoleAsync("admin") ||
                              await _auth.Permissions().HasAnyPermissionAsync(new List<string> { "manage:users", "manage:system" });
            Console.WriteLine($"Is authorized: {isAuthorized}");

            // Check multiple requirements
            var hasRequirements = await _auth.Permissions().HasAllPermissionsAsync(new List<string> { "read:users", "write:users" }) &&
                                 await _auth.Roles().HasRoleAsync("admin") &&
                                 await _auth.FeatureFlags().IsFeatureFlagEnabledAsync("user-management");
            Console.WriteLine($"Has all requirements: {hasRequirements}");
        }

        /// <summary>
        /// Run all examples.
        /// </summary>
        public async Task RunAllExamplesAsync()
        {
            try
            {
                await DemonstrateClaimsAsync();
                Console.WriteLine();

                await DemonstratePermissionsAsync();
                Console.WriteLine();

                await DemonstrateFeatureFlagsAsync();
                Console.WriteLine();

                await DemonstrateRolesAsync();
                Console.WriteLine();

                await DemonstrateEntitlementsAsync();
                Console.WriteLine();

                await DemonstrateComplexAccessControlAsync();
                Console.WriteLine();

                Console.WriteLine("All examples completed successfully!");
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "Error running examples");
                Console.WriteLine($"Error running examples: {ex.Message}");
            }
        }
    }
}
