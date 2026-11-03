using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

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
            Scopes = Require(scopes, nameof(scopes));
            RequireAll = requireAll;
        }

        // An empty list would authorize every request: the check has nothing to fail on. That is
        // easy to reach when the list comes from configuration and the setting is missing, so the
        // mistake surfaces here, when the policy is built, rather than silently failing open.
        internal static IReadOnlyList<string> Require(IReadOnlyList<string> values, string name)
        {
            ArgumentNullException.ThrowIfNull(values, name);

            if (values.Count == 0 || values.Any(string.IsNullOrWhiteSpace))
            {
                throw new ArgumentException(
                    "At least one non-empty value is required; an empty list would authorize "
                    + "every request.", name);
            }

            return values;
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
            Permissions = KindeScopeRequirement.Require(permissions, nameof(permissions));
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
                    case KindeScopeRequirement scope:
                        if (Satisfied(context, KindeClaimTypes.Scope, scope.Scopes, scope.RequireAll))
                        {
                            context.Succeed(requirement);
                        }
                        else
                        {
                            // Recorded so the challenge can name the missing scopes. The MCP
                            // step-up flow depends on insufficient_scope plus scope="...", which
                            // is how a client knows to re-authorize for more.
                            RecordMissingScopes(context, scope);
                        }
                        break;

                    case KindePermissionRequirement permission when Satisfied(
                        context, KindeClaimTypes.Permission, permission.Permissions, permission.RequireAll):
                        context.Succeed(requirement);
                        break;
                }
            }

            return Task.CompletedTask;
        }

        /// <summary>Key under which the scopes a request lacked are stored.</summary>
        internal const string MissingScopesItemKey = "Kinde.AgentAuth.MissingScopes";

        /// <summary>
        /// Notes the scopes a failed requirement wanted, so the 403 can carry an
        /// <c>insufficient_scope</c> challenge naming them.
        /// </summary>
        private static void RecordMissingScopes(
            AuthorizationHandlerContext context, KindeScopeRequirement requirement)
        {
            if (context.Resource is not HttpContext http)
            {
                return;
            }

            var missing = http.Items.TryGetValue(MissingScopesItemKey, out var existing)
                          && existing is HashSet<string> set
                ? set
                : new HashSet<string>(StringComparer.Ordinal);

            foreach (var scope in requirement.Scopes)
            {
                missing.Add(scope);
            }

            http.Items[MissingScopesItemKey] = missing;
        }

        private static bool Satisfied(
            AuthorizationHandlerContext context,
            string claimType,
            IReadOnlyList<string> required,
            bool requireAll)
        {
            // Unreachable: both requirement constructors reject an empty list, so a requirement
            // cannot carry one. Kept as a belt-and-braces guard, and deliberately denying rather
            // than allowing, because a requirement with nothing to check should never be the reason
            // a request is authorized.
            if (required.Count == 0)
            {
                return false;
            }

            // Only identities a Kinde agent scheme created. Kinde user tokens carry a permissions
            // claim too, so searching every identity would let a browser cookie session satisfy
            // an agent policy on an app that runs both.
            var schemes = (context.Resource as HttpContext)?.RequestServices
                .GetService<KindeAgentAuthSchemes>()?.Names;

            var identities = context.User.Identities.Where(identity =>
                identity.IsAuthenticated
                && identity.AuthenticationType != null
                && (schemes == null
                    // Exact match, not a prefix: a scheme merely named like the default one is
                    // somebody else's and must not satisfy a Kinde policy.
                    ? string.Equals(identity.AuthenticationType,
                        KindeAgentAuthenticationOptions.DefaultScheme, StringComparison.Ordinal)
                    : schemes.Contains(identity.AuthenticationType, StringComparer.Ordinal)));

            // Ordinal: scopes and permission keys are case-sensitive; a loose match would grant
            // access the token does not carry.
            var granted = identities
                .SelectMany(identity => identity.FindAll(claimType))
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
