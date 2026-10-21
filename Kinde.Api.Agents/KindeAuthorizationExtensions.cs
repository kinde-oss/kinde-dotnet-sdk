using Microsoft.AspNetCore.Authorization;

namespace Kinde.Api.Agents
{
    /// <summary>
    /// Requires one or more scopes on the presented token.
    /// </summary>
    /// <remarks>
    /// A missing scope is recoverable: the agent can ask for it the next time it authorizes, so
    /// this is the one case where an <c>insufficient_scope</c> challenge is the right answer.
    /// </remarks>
    public class KindeScopeRequirement : IAuthorizationRequirement
    {
        public KindeScopeRequirement(IReadOnlyList<string> scopes, bool requireAll)
        {
            Scopes = scopes ?? throw new ArgumentNullException(nameof(scopes));
            RequireAll = requireAll;
        }

        public IReadOnlyList<string> Scopes { get; }

        public bool RequireAll { get; }
    }

    /// <summary>
    /// Requires one or more permissions on the presented token.
    /// </summary>
    /// <remarks>
    /// Permissions are assigned in Kinde, not requested by the client, so a missing one yields a
    /// plain 403 with no challenge: re-authorizing could never fix it.
    /// </remarks>
    public class KindePermissionRequirement : IAuthorizationRequirement
    {
        public KindePermissionRequirement(IReadOnlyList<string> permissions, bool requireAll)
        {
            Permissions = permissions ?? throw new ArgumentNullException(nameof(permissions));
            RequireAll = requireAll;
        }

        public IReadOnlyList<string> Permissions { get; }

        public bool RequireAll { get; }
    }

    /// <summary>Evaluates the Kinde scope and permission requirements.</summary>
    public class KindeRequirementHandler : IAuthorizationHandler
    {
        public Task HandleAsync(AuthorizationHandlerContext context)
        {
            foreach (var requirement in context.PendingRequirements.ToList())
            {
                switch (requirement)
                {
                    case KindeScopeRequirement scope when Satisfied(
                        context, KindeClaimTypes.Scope, scope.Scopes, scope.RequireAll):
                        context.Succeed(requirement);
                        break;

                    case KindePermissionRequirement permission when Satisfied(
                        context, KindeClaimTypes.Permission, permission.Permissions, permission.RequireAll):
                        context.Succeed(requirement);
                        break;
                }
            }

            return Task.CompletedTask;
        }

        private static bool Satisfied(
            AuthorizationHandlerContext context,
            string claimType,
            IReadOnlyList<string> required,
            bool requireAll)
        {
            if (required.Count == 0)
            {
                return true;
            }

            // Ordinal: scopes and permission keys are case-sensitive; a loose match would grant
            // access the token does not carry.
            var granted = context.User.FindAll(claimType)
                .Select(claim => claim.Value)
                .ToHashSet(StringComparer.Ordinal);

            return requireAll
                ? required.All(granted.Contains)
                : required.Any(granted.Contains);
        }
    }

    /// <summary>
    /// Adds Kinde scope and permission requirements to an authorization policy.
    /// </summary>
    public static class KindeAuthorizationExtensions
    {
        /// <summary>Requires every one of <paramref name="scopes"/>.</summary>
        public static AuthorizationPolicyBuilder RequireKindeScope(
            this AuthorizationPolicyBuilder builder, params string[] scopes)
        {
            ArgumentNullException.ThrowIfNull(builder);
            return builder.AddRequirements(new KindeScopeRequirement(scopes, requireAll: true));
        }

        /// <summary>Requires at least one of <paramref name="scopes"/>.</summary>
        public static AuthorizationPolicyBuilder RequireAnyKindeScope(
            this AuthorizationPolicyBuilder builder, params string[] scopes)
        {
            ArgumentNullException.ThrowIfNull(builder);
            return builder.AddRequirements(new KindeScopeRequirement(scopes, requireAll: false));
        }

        /// <summary>Requires every one of <paramref name="permissions"/>.</summary>
        public static AuthorizationPolicyBuilder RequireKindePermission(
            this AuthorizationPolicyBuilder builder, params string[] permissions)
        {
            ArgumentNullException.ThrowIfNull(builder);
            return builder.AddRequirements(
                new KindePermissionRequirement(permissions, requireAll: true));
        }

        /// <summary>Requires at least one of <paramref name="permissions"/>.</summary>
        public static AuthorizationPolicyBuilder RequireAnyKindePermission(
            this AuthorizationPolicyBuilder builder, params string[] permissions)
        {
            ArgumentNullException.ThrowIfNull(builder);
            return builder.AddRequirements(
                new KindePermissionRequirement(permissions, requireAll: false));
        }
    }
}
