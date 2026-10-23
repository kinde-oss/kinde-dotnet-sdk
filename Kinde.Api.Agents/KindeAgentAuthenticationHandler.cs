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
            var identity = new ClaimsIdentity(
                authenticationType: Scheme.Name,
                nameType: KindeClaimTypes.Subject,
                roleType: KindeClaimTypes.Permission);

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
                kind, AgentAuth.Options.ProtectedResourceMetadataUrl, description);

            if (challenge != null)
            {
                Response.Headers.WWWAuthenticate = challenge;
            }

            await Task.CompletedTask;
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
                    AgentAuth.Options.ProtectedResourceMetadataUrl,
                    description: null,
                    requiredScopes: missing);

                if (challenge != null)
                {
                    Response.Headers.WWWAuthenticate = challenge;
                }
            }

            // Otherwise no challenge: a missing permission is assigned in Kinde, not requested,
            // so telling the agent to authorize again produces a loop it cannot escape.
            await Task.CompletedTask;
        }
    }
}
