#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;

namespace Kinde.Api.Agents
{
    /// <summary>
    /// The outcome of verifying one inbound access token, plus everything the caller needs to
    /// authorize the request without re-reading the raw claims.
    /// </summary>
    public class KindeAuthContext
    {
        private KindeAuthContext(bool isAuthenticated)
        {
            IsAuthenticated = isAuthenticated;
        }

        /// <summary>True when the token was verified successfully.</summary>
        public bool IsAuthenticated { get; private init; }

        /// <summary>Why verification failed. Null on success.</summary>
        public KindeAuthFailureKind? FailureKind { get; private init; }

        /// <summary>A short diagnostic. Safe to log; do not return it verbatim to the caller.</summary>
        public string? FailureReason { get; private init; }

        /// <summary>The <c>sub</c> claim: the agent identity Kinde issued the token to.</summary>
        public string? Subject { get; private init; }

        /// <summary>The <c>act.sub</c> claim: the acting agent, when the token is delegated.</summary>
        public string? ActorSubject { get; private init; }

        /// <summary>The <c>client_id</c> claim, which for a CIMD client is the client's metadata URL.</summary>
        public string? ClientId { get; private init; }

        /// <summary>Granted scopes.</summary>
        public IReadOnlyList<string> Scopes { get; private init; } = Array.Empty<string>();

        /// <summary>Granted permissions, from the <c>permissions</c> claim.</summary>
        public IReadOnlyList<string> Permissions { get; private init; } = Array.Empty<string>();

        /// <summary>Audiences the token is valid for. Always contains the configured Resource URL.</summary>
        public IReadOnlyList<string> Audiences { get; private init; } = Array.Empty<string>();

        /// <summary>The <c>org_code</c> claim, when the token is organization-scoped.</summary>
        public string? OrganizationCode { get; private init; }

        /// <summary>Expiry, when the token carries <c>exp</c>.</summary>
        public DateTimeOffset? ExpiresAt { get; private init; }

        /// <summary>Every claim the token carried, for anything this type does not surface directly.</summary>
        public IReadOnlyDictionary<string, object> Claims { get; private init; }
            = new Dictionary<string, object>();

        /// <summary>Case-sensitive scope check, as OAuth scopes are case-sensitive.</summary>
        public bool HasScope(string scope) =>
            !string.IsNullOrEmpty(scope) && Scopes.Contains(scope, StringComparer.Ordinal);

        /// <summary>Case-sensitive permission check.</summary>
        public bool HasPermission(string permission) =>
            !string.IsNullOrEmpty(permission) && Permissions.Contains(permission, StringComparer.Ordinal);

        /// <summary>True when every one of <paramref name="permissions"/> is granted.</summary>
        public bool HasAllPermissions(params string[] permissions) =>
            permissions != null && permissions.All(HasPermission);

        /// <summary>True when at least one of <paramref name="permissions"/> is granted.</summary>
        public bool HasAnyPermission(params string[] permissions) =>
            permissions != null && permissions.Any(HasPermission);

        /// <summary>
        /// Builds a successful result. Public because <see cref="IKindeTokenVerifier"/> is public:
        /// a custom verifier has to be able to report success.
        /// </summary>
        public static KindeAuthContext Success(
            string? subject,
            string? actorSubject,
            string? clientId,
            IReadOnlyList<string> scopes,
            IReadOnlyList<string> permissions,
            IReadOnlyList<string> audiences,
            string? organizationCode,
            DateTimeOffset? expiresAt,
            IReadOnlyDictionary<string, object> claims) =>
            new KindeAuthContext(true)
            {
                Subject = subject,
                ActorSubject = actorSubject,
                ClientId = clientId,
                Scopes = scopes ?? Array.Empty<string>(),
                Permissions = permissions ?? Array.Empty<string>(),
                Audiences = audiences ?? Array.Empty<string>(),
                OrganizationCode = organizationCode,
                ExpiresAt = expiresAt,
                Claims = claims ?? new Dictionary<string, object>()
            };

        internal static KindeAuthContext Fail(KindeAuthFailureKind kind, string reason) =>
            new KindeAuthContext(false)
            {
                FailureKind = kind,
                FailureReason = reason
            };

        /// <summary>No credential was presented.</summary>
        public static KindeAuthContext MissingToken(string reason = "No bearer token was presented.") =>
            Fail(KindeAuthFailureKind.MissingToken, reason);

        /// <summary>The credential was presented but is not valid.</summary>
        public static KindeAuthContext Invalid(string reason) =>
            Fail(KindeAuthFailureKind.InvalidToken, reason);

        /// <summary>Kinde could not be reached, or rejected our own credentials.</summary>
        public static KindeAuthContext Unavailable(string reason) =>
            Fail(KindeAuthFailureKind.AuthorizationServerUnavailable, reason);
    }
}
