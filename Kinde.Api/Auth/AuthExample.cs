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
        /// Example of using comprehensive hard check methods.
        /// </summary>
        public async Task DemonstrateComprehensiveHardCheckAsync()
        {
            Console.WriteLine("=== Comprehensive Hard Check Example ===");

            // Check if user has ALL requirements across all types
            var hasAllRequirements = await _auth.HasAllAsync(
                new List<string> { "read:users", "write:users" },  // permissions
                new List<string> { "admin" },                       // roles
                new List<string> { "user-management" }             // feature flags
            );
            Console.WriteLine($"Has ALL requirements: {hasAllRequirements}");

            // Check if user has ANY requirements across all types
            var hasAnyRequirements = await _auth.HasAnyAsync(
                new List<string> { "read:users", "delete:users" }, // permissions
                new List<string> { "admin", "moderator" },         // roles
                new List<string> { "user-management", "basic" }    // feature flags
            );
            Console.WriteLine($"Has ANY requirements: {hasAnyRequirements}");

            // Check with only some types specified
            var hasPermissionsAndRoles = await _auth.HasAllAsync(
                new List<string> { "read:users" },                 // permissions
                new List<string> { "user" },                       // roles
                null                                               // no feature flag requirement
            );
            Console.WriteLine($"Has permissions and roles: {hasPermissionsAndRoles}");
        }

        /// <summary>
        /// Example of using individual hard check methods for each auth type.
        /// </summary>
        public async Task DemonstrateIndividualHardCheckAsync()
        {
            Console.WriteLine("=== Individual Hard Check Example ===");

            // Permission hard checks
            var hasPermissionHardCheck = await _auth.Permissions().HasPermissionHardCheckAsync("create:users");
            Console.WriteLine($"Has permission (hard check): {hasPermissionHardCheck}");

            var hasAnyPermissionHardCheck = await _auth.Permissions().HasAnyPermissionHardCheckAsync(
                new List<string> { "create:users", "read:users", "update:users" });
            Console.WriteLine($"Has any permission (hard check): {hasAnyPermissionHardCheck}");

            var hasAllPermissionsHardCheck = await _auth.Permissions().HasAllPermissionsHardCheckAsync(
                new List<string> { "read:users", "write:users" });
            Console.WriteLine($"Has all permissions (hard check): {hasAllPermissionsHardCheck}");

            // Role hard checks
            var hasRoleHardCheck = await _auth.Roles().HasRoleHardCheckAsync("admin");
            Console.WriteLine($"Has role (hard check): {hasRoleHardCheck}");

            var hasAnyRoleHardCheck = await _auth.Roles().HasAnyRoleHardCheckAsync(
                new List<string> { "admin", "moderator" });
            Console.WriteLine($"Has any role (hard check): {hasAnyRoleHardCheck}");

            var hasAllRolesHardCheck = await _auth.Roles().HasAllRolesHardCheckAsync(
                new List<string> { "user", "verified" });
            Console.WriteLine($"Has all roles (hard check): {hasAllRolesHardCheck}");

            // Feature flag hard checks
            var isFeatureFlagEnabledHardCheck = await _auth.FeatureFlags().IsFeatureFlagEnabledHardCheckAsync("beta_features");
            Console.WriteLine($"Feature flag enabled (hard check): {isFeatureFlagEnabledHardCheck}");

            var isAnyFeatureFlagEnabledHardCheck = await _auth.FeatureFlags().IsAnyFeatureFlagEnabledHardCheckAsync(
                new List<string> { "beta_features", "premium_features" });
            Console.WriteLine($"Any feature flag enabled (hard check): {isAnyFeatureFlagEnabledHardCheck}");

            var areAllFeatureFlagsEnabledHardCheck = await _auth.FeatureFlags().AreAllFeatureFlagsEnabledHardCheckAsync(
                new List<string> { "basic_features", "standard_features" });
            Console.WriteLine($"All feature flags enabled (hard check): {areAllFeatureFlagsEnabledHardCheck}");

            // Entitlement hard checks
            var hasEntitlementHardCheck = await _auth.Entitlements().HasEntitlementHardCheckAsync("premium_features");
            Console.WriteLine($"Has entitlement (hard check): {hasEntitlementHardCheck}");

            var hasAnyEntitlementHardCheck = await _auth.Entitlements().HasAnyEntitlementHardCheckAsync(
                new List<string> { "premium_features", "advanced_analytics" });
            Console.WriteLine($"Has any entitlement (hard check): {hasAnyEntitlementHardCheck}");

            var hasAllEntitlementsHardCheck = await _auth.Entitlements().HasAllEntitlementsHardCheckAsync(
                new List<string> { "basic_features", "standard_features" });
            Console.WriteLine($"Has all entitlements (hard check): {hasAllEntitlementsHardCheck}");
        }

        /// <summary>
        /// Example of real-world usage scenarios using hard check methods.
        /// </summary>
        public async Task DemonstrateRealWorldScenariosAsync()
        {
            Console.WriteLine("=== Real-World Scenarios Example ===");

            // Scenario 1: Admin dashboard access
            var canAccessAdminDashboard = await _auth.HasAllAsync(
                new List<string> { "read:admin", "write:admin" },
                new List<string> { "admin" },
                new List<string> { "admin_dashboard" }
            );
            Console.WriteLine($"Can access admin dashboard: {canAccessAdminDashboard}");

            // Scenario 2: User management operations
            var canManageUsers = await _auth.HasAllAsync(
                new List<string> { "read:users", "write:users", "delete:users" },
                new List<string> { "admin", "moderator" },
                new List<string> { "user_management" }
            );
            Console.WriteLine($"Can manage users: {canManageUsers}");

            // Scenario 3: Analytics access (any from each category)
            var canViewAnalytics = await _auth.HasAnyAsync(
                new List<string> { "read:analytics", "read:reports" },
                new List<string> { "admin", "analyst" },
                new List<string> { "analytics", "advanced_analytics" }
            );
            Console.WriteLine($"Can view analytics: {canViewAnalytics}");

            // Scenario 4: Beta features access
            var canAccessBetaFeatures = await _auth.FeatureFlags().IsFeatureFlagEnabledHardCheckAsync("beta_features");
            Console.WriteLine($"Can access beta features: {canAccessBetaFeatures}");

            // Scenario 5: Premium content access
            var canAccessPremiumContent = await _auth.HasAllAsync(
                new List<string> { "access:premium" },
                null, // no specific role requirement
                new List<string> { "premium_features" }
            ) && await _auth.Entitlements().HasEntitlementHardCheckAsync("premium_features");
            Console.WriteLine($"Can access premium content: {canAccessPremiumContent}");
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

                await DemonstrateComprehensiveHardCheckAsync();
                Console.WriteLine();

                await DemonstrateIndividualHardCheckAsync();
                Console.WriteLine();

                await DemonstrateRealWorldScenariosAsync();
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
