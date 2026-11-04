using Microsoft.Extensions.Primitives;
using System.Security.Claims;
using System.Text.Encodings.Web;
using Kinde.Api.Agents;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Kinde.Api.Agents
{
    /// <summary>
    /// Authenticates requests that present an access token from a Kinde AI agents and MCP
    /// application, and issues the RFC 9728 challenge that lets an MCP client discover Kinde.
    /// </summary>
    public class KindeAgentAuthenticationHandler
        : AuthenticationHandler<KindeAgentAuthenticationOptions>
    {
        /// <summary>Key under which the verification result is stored on the request.</summary>
        internal const string AuthContextItemKey = "Kinde.AgentAuth.Context";

        // Keyed per scheme: with two Kinde schemes on one policy, a shared key would let the
        // second handler overwrite the first, so a challenge could report the wrong failure.
        private string SchemeItemKey => AuthContextItemKey + ":" + Scheme.Name;

        private readonly KindeAgentAuthRegistry _registry;

        public KindeAgentAuthenticationHandler(
            IOptionsMonitor<KindeAgentAuthenticationOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            KindeAgentAuthRegistry registry)
            : base(options, logger, encoder)
        {
            _registry = registry;
        }

        /// <summary>
        /// The verifier for this handler's own scheme, so an application running two schemes
        /// does not validate one resource's tokens against the other's configuration.
        /// </summary>
        private KindeAgentAuth AgentAuth => _registry.Get(Scheme.Name);

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            var header = Request.Headers.Authorization.ToString();

            if (!KindeAgentAuth.TryGetBearerToken(header, out var token))
            {
                // NoResult, not Fail: anonymous endpoints must still work, and another scheme may
                // handle the request. The 401 comes from the challenge instead.
                return AuthenticateResult.NoResult();
            }

            KindeAuthContext context;
            try
            {
                context = await AgentAuth.VerifyTokenAsync(token!, Context.RequestAborted);
            }
            catch (OperationCanceledException) when (Context.RequestAborted.IsCancellationRequested)
            {
                throw;
            }

            Context.Items[SchemeItemKey] = context;

            // The shared key is what GetKindeAuthContext() reads. A successful result always
            // wins, so with two schemes on one policy the application sees the identity that
            // actually authenticated rather than whichever handler happened to run last.
            if (context.IsAuthenticated
                || Context.Items[AuthContextItemKey] is not KindeAuthContext { IsAuthenticated: true })
            {
                Context.Items[AuthContextItemKey] = context;
            }

            if (!context.IsAuthenticated)
            {
                if (context.FailureKind == KindeAuthFailureKind.AuthorizationServerUnavailable)
                {
                    Logger.LogError(
                        "Kinde could not verify the token: {Reason}", context.FailureReason);
                }
                else
                {
                    Logger.LogDebug("Token rejected: {Reason}", context.FailureReason);
                }

                return AuthenticateResult.Fail(context.FailureReason ?? "The token is not valid.");
            }

            var principal = new ClaimsPrincipal(BuildIdentity(context));
            return AuthenticateResult.Success(
                new AuthenticationTicket(principal, Scheme.Name));
        }

        private ClaimsIdentity BuildIdentity(KindeAuthContext context)
        {
            // roleType is the role claim, not the permission claim. Pointing it at permissions
            // makes [Authorize(Roles = "admin")] succeed on a permission named "admin" even when
            // no such role was granted, and fail for a genuine role that carries no matching
            // permission. Roles and permissions are separate grants in Kinde and are mapped
            // separately below; use [Authorize(Policy = ...)] with RequireKindePermission for
            // permission checks.
            var identity = new ClaimsIdentity(
                authenticationType: Scheme.Name,
                nameType: KindeClaimTypes.Subject,
                roleType: KindeClaimTypes.Role);

            if (!string.IsNullOrEmpty(context.Subject))
            {
                identity.AddClaim(new Claim(KindeClaimTypes.Subject, context.Subject!));
            }

            if (!string.IsNullOrEmpty(context.ActorSubject))
            {
                identity.AddClaim(new Claim(KindeClaimTypes.ActorSubject, context.ActorSubject!));
            }

            if (!string.IsNullOrEmpty(context.ClientId))
            {
                identity.AddClaim(new Claim(KindeClaimTypes.ClientId, context.ClientId!));
            }

            if (!string.IsNullOrEmpty(context.OrganizationCode))
            {
                identity.AddClaim(new Claim(KindeClaimTypes.OrganizationCode, context.OrganizationCode!));
            }

            // One claim per value, so ClaimsPrincipal.HasClaim and role checks work unparsed.
            foreach (var scope in context.Scopes)
            {
                identity.AddClaim(new Claim(KindeClaimTypes.Scope, scope));
            }

            foreach (var permission in context.Permissions)
            {
                identity.AddClaim(new Claim(KindeClaimTypes.Permission, permission));
            }

            foreach (var role in context.Roles)
            {
                identity.AddClaim(new Claim(KindeClaimTypes.Role, role));
            }

            return identity;
        }

        protected override async Task HandleChallengeAsync(AuthenticationProperties properties)
        {
            var context = Context.Items.TryGetValue(SchemeItemKey, out var scoped)
                ? scoped as KindeAuthContext
                : Context.GetKindeAuthContext();
            var kind = context?.FailureKind ?? KindeAuthFailureKind.MissingToken;

            Response.StatusCode = KindeAuthChallenge.StatusCodeFor(kind);

            var description = Options.IncludeErrorDetails ? context?.FailureReason : null;
            var challenge = KindeAuthChallenge.Build(
                kind, MetadataUrl, description);

            if (challenge != null)
            {
                AppendChallenge(challenge);
            }

            await Task.CompletedTask;
        }

        /// <summary>
        /// The <c>resource_metadata</c> URL to advertise: the override when an integration serves
        /// the document somewhere other than the path this SDK maps, otherwise Kinde's own.
        /// </summary>
        private string MetadataUrl =>
            string.IsNullOrWhiteSpace(Options.ProtectedResourceMetadataUrl)
                ? AgentAuth.Options.ProtectedResourceMetadataUrl
                : Options.ProtectedResourceMetadataUrl!;

        /// <summary>
        /// Adds a challenge to <c>WWW-Authenticate</c> without discarding one already there.
        /// </summary>
        /// <remarks>
        /// A policy naming several Kinde schemes has ASP.NET Core challenge each of them onto the
        /// same response. Assigning the header would keep only the last, so the resource the token
        /// actually authenticated against could be the one dropped, leaving the client pointed at
        /// metadata for a resource it has no token for. RFC 9110 allows the header to repeat.
        /// </remarks>
        private void AppendChallenge(string challenge)
        {
            var existing = Response.Headers.WWWAuthenticate;

            if (existing.Count == 0)
            {
                Response.Headers.WWWAuthenticate = challenge;
                return;
            }

            // Re-challenging the same scheme happens when two handlers share a configuration;
            // repeating an identical value helps nobody.
            if (existing.Contains(challenge, StringComparer.Ordinal))
            {
                return;
            }

            Response.Headers.WWWAuthenticate =
                StringValues.Concat(existing, new StringValues(challenge));
        }

        protected override async Task HandleForbiddenAsync(AuthenticationProperties properties)
        {
            Response.StatusCode = StatusCodes.Status403Forbidden;

            // A missing scope is recoverable: the agent can ask for it next time, and the MCP
            // step-up flow depends on insufficient_scope naming the scopes it needs.
            if (Context.Items.TryGetValue(
                    KindeRequirementHandler.MissingScopesItemKey, out var value)
                && value is IReadOnlyCollection<string> { Count: > 0 } missing)
            {
                var challenge = KindeAuthChallenge.Build(
                    KindeAuthFailureKind.InsufficientScope,
                    MetadataUrl,
                    description: null,
                    requiredScopes: missing);

                if (challenge != null)
                {
                    AppendChallenge(challenge);
                }
            }

            // Otherwise no challenge: a missing permission is assigned in Kinde, not requested,
            // so telling the agent to authorize again produces a loop it cannot escape.
            await Task.CompletedTask;
        }
    }
}
