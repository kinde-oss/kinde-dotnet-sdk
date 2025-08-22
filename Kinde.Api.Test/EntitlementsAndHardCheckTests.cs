using Kinde.Api.Models.Tokens;
using Kinde.Api.Accounts;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Kinde.Api.Test
{
    public class EntitlementsAndHardCheckTests
    {
        private readonly Mock<ILogger<KindeTokenChecker>> _mockLogger;
        private readonly Mock<IKindeAccountsClient> _mockAccountsClient;
        private readonly OauthToken _testToken;

        public EntitlementsAndHardCheckTests()
        {
            _mockLogger = new Mock<ILogger<KindeTokenChecker>>();
            _mockAccountsClient = new Mock<IKindeAccountsClient>();
            _testToken = new OauthToken
            {
                AccessToken = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIxMjM0NTY3ODkwIiwibmFtZSI6IkpvaG4gRG9lIiwiaWF0IjoxNTE2MjM5MDIyLCJwZXJtaXNzaW9ucyI6WyJyZWFkOnVzZXJzIiwid3JpdGU6dXNlcnMiXSwicm9sZXMiOlsiYWRtaW4iXX0.valid_signature_for_testing"
            };
        }

        [Fact]
        public void OauthToken_GetPermissions_WithValidToken_ReturnsPermissions()
        {
            // Act
            var permissions = _testToken.GetPermissions();

            // Assert
            Assert.NotNull(permissions);
            Assert.Contains("read:users", permissions);
            Assert.Contains("write:users", permissions);
        }

        [Fact]
        public void OauthToken_GetRoles_WithValidToken_ReturnsRoles()
        {
            // Act
            var roles = _testToken.GetRoles();

            // Assert
            Assert.NotNull(roles);
            Assert.Contains("admin", roles);
        }

        [Fact]
        public void OauthToken_HasPermission_WithValidPermission_ReturnsTrue()
        {
            // Act
            var hasPermission = _testToken.HasPermission("read:users");

            // Assert
            Assert.True(hasPermission);
        }

        [Fact]
        public void OauthToken_HasPermission_WithInvalidPermission_ReturnsFalse()
        {
            // Act
            var hasPermission = _testToken.HasPermission("invalid:permission");

            // Assert
            Assert.False(hasPermission);
        }

        [Fact]
        public void OauthToken_HasRole_WithValidRole_ReturnsTrue()
        {
            // Act
            var hasRole = _testToken.HasRole("admin");

            // Assert
            Assert.True(hasRole);
        }

        [Fact]
        public void OauthToken_HasRole_WithInvalidRole_ReturnsFalse()
        {
            // Act
            var hasRole = _testToken.HasRole("invalid-role");

            // Assert
            Assert.False(hasRole);
        }

        [Fact]
        public void OauthToken_GetAllClaims_WithValidToken_ReturnsClaims()
        {
            // Act
            var claims = _testToken.GetAllClaims();

            // Assert
            Assert.NotNull(claims);
            Assert.True(claims.Count > 0);
        }

        [Fact]
        public void OauthToken_GetPermissions_WithNullToken_ReturnsNull()
        {
            // Arrange
            var nullToken = new OauthToken { AccessToken = null };

            // Act
            var permissions = nullToken.GetPermissions();

            // Assert
            Assert.Null(permissions);
        }

        [Fact]
        public void OauthToken_GetRoles_WithNullToken_ReturnsNull()
        {
            // Arrange
            var nullToken = new OauthToken { AccessToken = null };

            // Act
            var roles = nullToken.GetRoles();

            // Assert
            Assert.Null(roles);
        }

        [Fact]
        public void OauthToken_GetFeatureFlags_WithNullToken_ReturnsNull()
        {
            // Arrange
            var nullToken = new OauthToken { AccessToken = null };

            // Act
            var flags = nullToken.GetFeatureFlags();

            // Assert
            Assert.Null(flags);
        }

        [Fact]
        public void OauthToken_GetFeatureFlag_WithNullFlagKey_ReturnsNull()
        {
            // Act
            var flag = _testToken.GetFeatureFlag(null);

            // Assert
            Assert.Null(flag);
        }

        [Fact]
        public void OauthToken_GetFeatureFlag_WithEmptyFlagKey_ReturnsNull()
        {
            // Act
            var flag = _testToken.GetFeatureFlag("");

            // Assert
            Assert.Null(flag);
        }

        [Fact]
        public void OauthToken_HasPermission_WithNullPermissionKey_ReturnsFalse()
        {
            // Act
            var hasPermission = _testToken.HasPermission(null);

            // Assert
            Assert.False(hasPermission);
        }

        [Fact]
        public void OauthToken_HasRole_WithNullRoleKey_ReturnsFalse()
        {
            // Act
            var hasRole = _testToken.HasRole(null);

            // Assert
            Assert.False(hasRole);
        }

        [Fact]
        public void OauthToken_IsFeatureFlagEnabled_WithNullFlagKey_ReturnsFalse()
        {
            // Act
            var isEnabled = _testToken.IsFeatureFlagEnabled(null);

            // Assert
            Assert.False(isEnabled);
        }

        [Fact]
        public void KindeTokenChecker_Constructor_WithNullToken_ThrowsException()
        {
            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => 
                new KindeTokenChecker(null, _mockAccountsClient.Object));
        }

        [Fact]
        public void KindeTokenChecker_Constructor_WithNullAccountsClient_ThrowsException()
        {
            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => 
                new KindeTokenChecker(_testToken, (IKindeAccountsClient)null));
        }



        [Fact]
        public async Task KindeTokenChecker_HasPermissionAsync_WithNullPermissionKey_ThrowsException()
        {
            // Arrange
            var checker = new KindeTokenChecker(_testToken, _mockAccountsClient.Object);

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(() => 
                checker.HasPermissionAsync(null));
        }

        [Fact]
        public async Task KindeTokenChecker_HasPermissionAsync_WithEmptyPermissionKey_ThrowsException()
        {
            // Arrange
            var checker = new KindeTokenChecker(_testToken, _mockAccountsClient.Object);

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(() => 
                checker.HasPermissionAsync(""));
        }

        [Fact]
        public async Task KindeTokenChecker_HasRoleAsync_WithNullRoleKey_ThrowsException()
        {
            // Arrange
            var checker = new KindeTokenChecker(_testToken, _mockAccountsClient.Object);

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(() => 
                checker.HasRoleAsync(null));
        }

        [Fact]
        public async Task KindeTokenChecker_HasRoleAsync_WithEmptyRoleKey_ThrowsException()
        {
            // Arrange
            var checker = new KindeTokenChecker(_testToken, _mockAccountsClient.Object);

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(() => 
                checker.HasRoleAsync(""));
        }

        [Fact]
        public async Task KindeTokenChecker_IsFeatureFlagEnabledAsync_WithNullFlagKey_ThrowsException()
        {
            // Arrange
            var checker = new KindeTokenChecker(_testToken, _mockAccountsClient.Object);

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(() => 
                checker.IsFeatureFlagEnabledAsync(null));
        }

        [Fact]
        public async Task KindeTokenChecker_IsFeatureFlagEnabledAsync_WithEmptyFlagKey_ThrowsException()
        {
            // Arrange
            var checker = new KindeTokenChecker(_testToken, _mockAccountsClient.Object);

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(() => 
                checker.IsFeatureFlagEnabledAsync(""));
        }

        [Fact]
        public async Task KindeTokenChecker_HasAnyPermissionAsync_WithNullPermissionKeys_ThrowsException()
        {
            // Arrange
            var checker = new KindeTokenChecker(_testToken, _mockAccountsClient.Object);

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(() => 
                checker.HasAnyPermissionAsync(null));
        }

        [Fact]
        public async Task KindeTokenChecker_HasAnyPermissionAsync_WithEmptyPermissionKeys_ThrowsException()
        {
            // Arrange
            var checker = new KindeTokenChecker(_testToken, _mockAccountsClient.Object);

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(() => 
                checker.HasAnyPermissionAsync(new List<string>()));
        }

        [Fact]
        public async Task KindeTokenChecker_HasAllPermissionsAsync_WithNullPermissionKeys_ThrowsException()
        {
            // Arrange
            var checker = new KindeTokenChecker(_testToken, _mockAccountsClient.Object);

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(() => 
                checker.HasAllPermissionsAsync(null));
        }

        [Fact]
        public async Task KindeTokenChecker_HasAllPermissionsAsync_WithEmptyPermissionKeys_ThrowsException()
        {
            // Arrange
            var checker = new KindeTokenChecker(_testToken, _mockAccountsClient.Object);

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(() => 
                checker.HasAllPermissionsAsync(new List<string>()));
        }

        [Fact]
        public async Task KindeTokenChecker_HasAnyRoleAsync_WithNullRoleKeys_ThrowsException()
        {
            // Arrange
            var checker = new KindeTokenChecker(_testToken, _mockAccountsClient.Object);

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(() => 
                checker.HasAnyRoleAsync(null));
        }

        [Fact]
        public async Task KindeTokenChecker_HasAllRoleAsync_WithNullRoleKeys_ThrowsException()
        {
            // Arrange
            var checker = new KindeTokenChecker(_testToken, _mockAccountsClient.Object);

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(() => 
                checker.HasAllRolesAsync(null));
        }

        [Fact]
        public async Task KindeTokenChecker_GetEntitlementAsync_WithNullKey_ThrowsException()
        {
            // Arrange
            var checker = new KindeTokenChecker(_testToken, _mockAccountsClient.Object);

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(() => 
                checker.GetEntitlementAsync(null));
        }

        [Fact]
        public async Task KindeTokenChecker_GetEntitlementAsync_WithEmptyKey_ThrowsException()
        {
            // Arrange
            var checker = new KindeTokenChecker(_testToken, _mockAccountsClient.Object);

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(() => 
                checker.GetEntitlementAsync(""));
        }

        [Fact]
        public void OauthToken_GetFeatureFlags_WithNoFeatureFlags_ReturnsNull()
        {
            // Act
            var flags = _testToken.GetFeatureFlags();

            // Assert
            Assert.Null(flags);
        }

        [Fact]
        public void OauthToken_GetFeatureFlag_WithNoFeatureFlags_ReturnsNull()
        {
            // Act
            var flag = _testToken.GetFeatureFlag("test-flag");

            // Assert
            Assert.Null(flag);
        }

        [Fact]
        public void OauthToken_IsFeatureFlagEnabled_WithNoFeatureFlags_ReturnsFalse()
        {
            // Act
            var isEnabled = _testToken.IsFeatureFlagEnabled("test-flag");

            // Assert
            Assert.False(isEnabled);
        }

        [Fact]
        public async Task KindeTokenChecker_HasPermissionAsync_WithTokenPermission_ReturnsTrue()
        {
            // Arrange
            var checker = new KindeTokenChecker(_testToken, _mockAccountsClient.Object);

            // Act
            var hasPermission = await checker.HasPermissionAsync("read:users");

            // Assert
            Assert.True(hasPermission);
        }

        [Fact]
        public async Task KindeTokenChecker_HasRoleAsync_WithTokenRole_ReturnsTrue()
        {
            // Arrange
            var checker = new KindeTokenChecker(_testToken, _mockAccountsClient.Object);

            // Act
            var hasRole = await checker.HasRoleAsync("admin");

            // Assert
            Assert.True(hasRole);
        }

        [Fact]
        public async Task KindeTokenChecker_HasPermissionAsync_WithInvalidPermission_ReturnsFalse()
        {
            // Arrange
            var checker = new KindeTokenChecker(_testToken, _mockAccountsClient.Object);

            // Act
            var hasPermission = await checker.HasPermissionAsync("invalid:permission");

            // Assert
            Assert.False(hasPermission);
        }

        [Fact]
        public async Task KindeTokenChecker_HasRoleAsync_WithInvalidRole_ReturnsFalse()
        {
            // Arrange
            var checker = new KindeTokenChecker(_testToken, _mockAccountsClient.Object);

            // Act
            var hasRole = await checker.HasRoleAsync("invalid-role");

            // Assert
            Assert.False(hasRole);
        }
    }
}
