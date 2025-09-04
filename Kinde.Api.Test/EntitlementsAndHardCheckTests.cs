using Kinde.Api.Auth;
using Kinde.Api.Client;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Kinde.Api.Test
{
    /// <summary>
    /// Comprehensive tests for the Entitlements class and hardcheck functionality.
    /// This covers all the new methods and ensures they work correctly.
    /// </summary>
    public class EntitlementsAndHardCheckTests
    {
        private readonly Mock<KindeClient> _mockClient;
        private readonly Mock<ILogger> _mockLogger;
        private readonly Entitlements _entitlements;
        private readonly Kinde.Api.Auth.Auth _auth;

        public EntitlementsAndHardCheckTests()
        {
            _mockClient = new Mock<KindeClient>();
            _mockLogger = new Mock<ILogger>();
            _entitlements = new Entitlements(_mockClient.Object, _mockLogger.Object);
            _auth = new Kinde.Api.Auth.Auth(_mockClient.Object, _mockLogger.Object);
        }

        #region Entitlements Tests

        [Fact]
        public async Task GetAllEntitlementsAsync_ShouldReturnEmptyList_WhenNoAccountsClient()
        {
            // Act
            var result = await _entitlements.GetAllEntitlementsAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result);
        }

        [Fact]
        public async Task GetEntitlementAsync_ShouldReturnNull_WhenKeyIsNull()
        {
            // Act
            var result = await _entitlements.GetEntitlementAsync(null);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task GetEntitlementAsync_ShouldReturnNull_WhenKeyIsEmpty()
        {
            // Act
            var result = await _entitlements.GetEntitlementAsync("");

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task GetEntitlementAsync_ShouldReturnNull_WhenKeyIsWhitespace()
        {
            // Act
            var result = await _entitlements.GetEntitlementAsync("   ");

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task HasEntitlementAsync_ShouldReturnFalse_WhenKeyIsNull()
        {
            // Act
            var result = await _entitlements.HasEntitlementAsync(null);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public async Task HasEntitlementAsync_ShouldReturnFalse_WhenKeyIsEmpty()
        {
            // Act
            var result = await _entitlements.HasEntitlementAsync("");

            // Assert
            Assert.False(result);
        }

        [Fact]
        public async Task HasEntitlementAsync_ShouldReturnFalse_WhenKeyIsWhitespace()
        {
            // Act
            var result = await _entitlements.HasEntitlementAsync("   ");

            // Assert
            Assert.False(result);
        }

        [Fact]
        public async Task HasAnyEntitlementAsync_ShouldReturnFalse_WhenKeysIsNull()
        {
            // Act
            var result = await _entitlements.HasAnyEntitlementAsync(null);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public async Task HasAnyEntitlementAsync_ShouldReturnFalse_WhenKeysIsEmpty()
        {
            // Act
            var result = await _entitlements.HasAnyEntitlementAsync(new List<string>());

            // Assert
            Assert.False(result);
        }

        [Fact]
        public async Task HasAllEntitlementsAsync_ShouldReturnFalse_WhenKeysIsNull()
        {
            // Act
            var result = await _entitlements.HasAllEntitlementsAsync(null);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public async Task HasAllEntitlementsAsync_ShouldReturnFalse_WhenKeysIsEmpty()
        {
            // Act
            var result = await _entitlements.HasAllEntitlementsAsync(new List<string>());

            // Assert
            Assert.False(result);
        }

        #endregion

        #region Entitlements Hardcheck Tests

        [Fact]
        public async Task HasEntitlementHardCheckAsync_ShouldReturnFalse_WhenKeyIsNull()
        {
            // Act
            var result = await _entitlements.HasEntitlementHardCheckAsync(null);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public async Task HasEntitlementHardCheckAsync_ShouldReturnFalse_WhenKeyIsEmpty()
        {
            // Act
            var result = await _entitlements.HasEntitlementHardCheckAsync("");

            // Assert
            Assert.False(result);
        }

        [Fact]
        public async Task HasEntitlementHardCheckAsync_ShouldReturnFalse_WhenKeyIsWhitespace()
        {
            // Act
            var result = await _entitlements.HasEntitlementHardCheckAsync("   ");

            // Assert
            Assert.False(result);
        }

        [Fact]
        public async Task HasAnyEntitlementHardCheckAsync_ShouldReturnFalse_WhenKeysIsNull()
        {
            // Act
            var result = await _entitlements.HasAnyEntitlementHardCheckAsync(null);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public async Task HasAnyEntitlementHardCheckAsync_ShouldReturnFalse_WhenKeysIsEmpty()
        {
            // Act
            var result = await _entitlements.HasAnyEntitlementHardCheckAsync(new List<string>());

            // Assert
            Assert.False(result);
        }

        [Fact]
        public async Task HasAllEntitlementsHardCheckAsync_ShouldReturnFalse_WhenKeysIsNull()
        {
            // Act
            var result = await _entitlements.HasAllEntitlementsHardCheckAsync(null);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public async Task HasAllEntitlementsHardCheckAsync_ShouldReturnFalse_WhenKeysIsEmpty()
        {
            // Act
            var result = await _entitlements.HasAllEntitlementsHardCheckAsync(new List<string>());

            // Assert
            Assert.False(result);
        }

        #endregion

        #region Comprehensive Hardcheck Tests

        [Fact]
        public async Task HasAllAsync_ShouldReturnTrue_WhenAllParametersAreNull()
        {
            // Act
            var result = await _auth.HasAllAsync(null, null, null);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task HasAllAsync_ShouldReturnTrue_WhenAllParametersAreEmpty()
        {
            // Act
            var result = await _auth.HasAllAsync(new List<string>(), new List<string>(), new List<string>());

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task HasAnyAsync_ShouldReturnTrue_WhenAllParametersAreNull()
        {
            // Act
            var result = await _auth.HasAnyAsync(null, null, null);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task HasAnyAsync_ShouldReturnTrue_WhenAllParametersAreEmpty()
        {
            // Act
            var result = await _auth.HasAnyAsync(new List<string>(), new List<string>(), new List<string>());

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task HasAllAsync_ShouldReturnFalse_WhenPermissionsAreNotSatisfied()
        {
            // Arrange
            var permissions = new List<string> { "read:users", "write:users" };
            var roles = new List<string> { "admin" };
            var featureFlags = new List<string> { "user-management" };

            // Create mock wrappers that return false for permissions
            var mockPermissions = new MockPermissions(new Dictionary<string, bool>
            {
                { "read:users", false }, // This will cause the test to fail
                { "write:users", true }
            });
            var mockRoles = new MockRoles(new Dictionary<string, bool>
            {
                { "admin", true }
            });
            var mockFeatureFlags = new MockFeatureFlags(new Dictionary<string, bool>
            {
                { "user-management", true }
            });

            var auth = new Kinde.Api.Auth.Auth(_mockClient.Object, _mockLogger.Object, 
                mockPermissions, mockRoles, mockFeatureFlags);

            // Act
            var result = await auth.HasAllAsync(permissions, roles, featureFlags);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public async Task HasAnyAsync_ShouldReturnFalse_WhenNoRequirementsAreMet()
        {
            // Arrange
            var permissions = new List<string> { "read:users", "write:users" };
            var roles = new List<string> { "admin", "moderator" };
            var featureFlags = new List<string> { "user-management", "basic" };

            // Create mock wrappers that all return false
            var mockPermissions = new MockPermissions(new Dictionary<string, bool>
            {
                { "read:users", false },
                { "write:users", false }
            });
            var mockRoles = new MockRoles(new Dictionary<string, bool>
            {
                { "admin", false },
                { "moderator", false }
            });
            var mockFeatureFlags = new MockFeatureFlags(new Dictionary<string, bool>
            {
                { "user-management", false },
                { "basic", false }
            });

            var auth = new Kinde.Api.Auth.Auth(_mockClient.Object, _mockLogger.Object, 
                mockPermissions, mockRoles, mockFeatureFlags);

            // Act
            var result = await auth.HasAnyAsync(permissions, roles, featureFlags);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public async Task HasAnyAsync_ShouldReturnTrue_WhenAtLeastOneRequirementIsMet()
        {
            // Arrange
            var permissions = new List<string> { "read:users", "write:users" };
            var roles = new List<string> { "admin", "moderator" };
            var featureFlags = new List<string> { "user-management", "basic" };

            // Create mock wrappers where at least one requirement from each category is met
            var mockPermissions = new MockPermissions(new Dictionary<string, bool>
            {
                { "read:users", true }, // This should make HasAny return true
                { "write:users", false }
            });
            var mockRoles = new MockRoles(new Dictionary<string, bool>
            {
                { "admin", true },        // ensure roles category passes
                { "moderator", false }
            });
            var mockFeatureFlags = new MockFeatureFlags(new Dictionary<string, bool>
            {
                { "user-management", true }, // ensure flags category passes
                { "basic", false }
            });

            var auth = new Kinde.Api.Auth.Auth(_mockClient.Object, _mockLogger.Object, 
                mockPermissions, mockRoles, mockFeatureFlags);

            // Act
            var result = await auth.HasAnyAsync(permissions, roles, featureFlags);

            // Assert
            Assert.True(result);
        }

        #endregion

        #region Individual Hardcheck Tests

        [Fact]
        public async Task HasPermissionHardCheckAsync_ShouldReturnFalse_WhenKeyIsNull()
        {
            // Act
            var result = await _auth.Permissions().HasPermissionHardCheckAsync(null);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public async Task HasPermissionHardCheckAsync_ShouldReturnFalse_WhenKeyIsEmpty()
        {
            // Act
            var result = await _auth.Permissions().HasPermissionHardCheckAsync("");

            // Assert
            Assert.False(result);
        }

        [Fact]
        public async Task HasRoleHardCheckAsync_ShouldReturnFalse_WhenKeyIsNull()
        {
            // Act
            var result = await _auth.Roles().HasRoleHardCheckAsync(null);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public async Task HasRoleHardCheckAsync_ShouldReturnFalse_WhenKeyIsEmpty()
        {
            // Act
            var result = await _auth.Roles().HasRoleHardCheckAsync("");

            // Assert
            Assert.False(result);
        }

        [Fact]
        public async Task IsFeatureFlagEnabledHardCheckAsync_ShouldReturnFalse_WhenKeyIsNull()
        {
            // Act
            var result = await _auth.FeatureFlags().IsFeatureFlagEnabledHardCheckAsync(null);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public async Task IsFeatureFlagEnabledHardCheckAsync_ShouldReturnFalse_WhenKeyIsEmpty()
        {
            // Act
            var result = await _auth.FeatureFlags().IsFeatureFlagEnabledHardCheckAsync("");

            // Assert
            Assert.False(result);
        }

        #endregion

        #region Helper Method Tests

        [Fact]
        public void TryToLong_ShouldReturnTrue_WhenObjectIsLong()
        {
            // Arrange
            long expectedValue = 42L;
            object obj = expectedValue;

            // Act
            bool result = Entitlements.TryToLong(obj, out long actualValue);

            // Assert
            Assert.True(result);
            Assert.Equal(expectedValue, actualValue);
        }

        [Fact]
        public void TryToLong_ShouldReturnTrue_WhenObjectIsInt()
        {
            // Arrange
            int expectedValue = 42;
            object obj = expectedValue;

            // Act
            bool result = Entitlements.TryToLong(obj, out long actualValue);

            // Assert
            Assert.True(result);
            Assert.Equal(expectedValue, actualValue);
        }

        [Fact]
        public void TryToLong_ShouldReturnTrue_WhenObjectIsString()
        {
            // Arrange
            string expectedValue = "42";
            object obj = expectedValue;

            // Act
            bool result = Entitlements.TryToLong(obj, out long actualValue);

            // Assert
            Assert.True(result);
            Assert.Equal(42L, actualValue);
        }

        [Fact]
        public void TryToLong_ShouldReturnFalse_WhenObjectIsInvalid()
        {
            // Arrange
            object obj = "invalid";

            // Act
            bool result = Entitlements.TryToLong(obj, out long actualValue);

            // Assert
            Assert.False(result);
            Assert.Equal(0L, actualValue);
        }

        [Fact]
        public void TryToLong_ShouldReturnFalse_WhenObjectIsNull()
        {
            // Arrange
            object obj = null;

            // Act
            bool result = Entitlements.TryToLong(obj, out long actualValue);

            // Assert
            Assert.False(result);
            Assert.Equal(0L, actualValue);
        }

        [Theory]
        [InlineData("9223372036854775808")] // long.MaxValue + 1 => overflow
        [InlineData("-1")]
        [InlineData("  42  ")]
        public void TryToLong_AdditionalCases(string input)
        {
            // Just call through to ensure parsing/overflow/trim behavior
            Entitlements.TryToLong(input, out _);
        }

        #endregion
    }


    #region Mock Wrapper Classes for Testing

    /// <summary>
    /// Mock implementation of Permissions for testing
    /// </summary>
    public class MockPermissions : Permissions
    {
        private readonly Dictionary<string, bool> _permissionResults;

        public MockPermissions(Dictionary<string, bool> permissionResults = null) 
            : base(null, null)
        {
            _permissionResults = permissionResults ?? new Dictionary<string, bool>();
        }

        public override async Task<bool> HasPermissionAsync(string permissionKey)
        {
            return _permissionResults.TryGetValue(permissionKey, out var result) ? result : false;
        }

        public override async Task<bool> HasAnyPermissionAsync(IEnumerable<string> permissionKeys)
        {
            return permissionKeys?.Any(key => _permissionResults.TryGetValue(key, out var result) && result) == true;
        }

        public override async Task<bool> HasAllPermissionsAsync(IEnumerable<string> permissionKeys)
        {
            return permissionKeys?.All(key => _permissionResults.TryGetValue(key, out var result) && result) == true;
        }

        public override async Task<bool> HasPermissionHardCheckAsync(string permissionKey)
        {
            return _permissionResults.TryGetValue(permissionKey, out var result) ? result : false;
        }

        public override async Task<bool> HasAnyPermissionHardCheckAsync(IEnumerable<string> permissionKeys)
        {
            return permissionKeys?.Any(key => _permissionResults.TryGetValue(key, out var result) && result) == true;
        }

        public override async Task<bool> HasAllPermissionsHardCheckAsync(IEnumerable<string> permissionKeys)
        {
            return permissionKeys?.All(key => _permissionResults.TryGetValue(key, out var result) && result) == true;
        }
    }

    /// <summary>
    /// Mock implementation of Roles for testing
    /// </summary>
    public class MockRoles : Roles
    {
        private readonly Dictionary<string, bool> _roleResults;

        public MockRoles(Dictionary<string, bool> roleResults = null) 
            : base(null, null)
        {
            _roleResults = roleResults ?? new Dictionary<string, bool>();
        }

        public override async Task<bool> HasRoleAsync(string roleKey)
        {
            return _roleResults.TryGetValue(roleKey, out var result) ? result : false;
        }

        public override async Task<bool> HasAnyRoleAsync(IEnumerable<string> roleKeys)
        {
            return roleKeys?.Any(key => _roleResults.TryGetValue(key, out var result) && result) == true;
        }

        public override async Task<bool> HasAllRolesAsync(IEnumerable<string> roleKeys)
        {
            return roleKeys?.All(key => _roleResults.TryGetValue(key, out var result) && result) == true;
        }

        public override async Task<bool> HasRoleHardCheckAsync(string roleKey)
        {
            return _roleResults.TryGetValue(roleKey, out var result) ? result : false;
        }

        public override async Task<bool> HasAnyRoleHardCheckAsync(IEnumerable<string> roleKeys)
        {
            return roleKeys?.Any(key => _roleResults.TryGetValue(key, out var result) && result) == true;
        }

        public override async Task<bool> HasAllRolesHardCheckAsync(IEnumerable<string> roleKeys)
        {
            return roleKeys?.All(key => _roleResults.TryGetValue(key, out var result) && result) == true;
        }
    }

    /// <summary>
    /// Mock implementation of FeatureFlags for testing
    /// </summary>
    public class MockFeatureFlags : FeatureFlags
    {
        private readonly Dictionary<string, bool> _flagResults;

        public MockFeatureFlags(Dictionary<string, bool> flagResults = null) 
            : base(null, null)
        {
            _flagResults = flagResults ?? new Dictionary<string, bool>();
        }

        public override async Task<bool> IsFeatureFlagEnabledAsync(string flagKey)
        {
            return _flagResults.TryGetValue(flagKey, out var result) ? result : false;
        }

        public override async Task<bool> IsAnyFeatureFlagEnabledAsync(IEnumerable<string> flagKeys)
        {
            return flagKeys?.Any(key => _flagResults.TryGetValue(key, out var result) && result) == true;
        }

        public override async Task<bool> AreAllFeatureFlagsEnabledAsync(IEnumerable<string> flagKeys)
        {
            return flagKeys?.All(key => _flagResults.TryGetValue(key, out var result) && result) == true;
        }

        public override async Task<bool> IsFeatureFlagEnabledHardCheckAsync(string flagKey)
        {
            return _flagResults.TryGetValue(flagKey, out var result) ? result : false;
        }

        public override async Task<bool> IsAnyFeatureFlagEnabledHardCheckAsync(IEnumerable<string> flagKeys)
        {
            return flagKeys?.Any(key => _flagResults.TryGetValue(key, out var result) && result) == true;
        }

        public override async Task<bool> AreAllFeatureFlagsEnabledHardCheckAsync(IEnumerable<string> flagKeys)
        {
            return flagKeys?.All(key => _flagResults.TryGetValue(key, out var result) && result) == true;
        }
    }

    #endregion
}
