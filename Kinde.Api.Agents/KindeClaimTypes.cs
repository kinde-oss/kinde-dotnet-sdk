namespace Kinde.Api.Agents
{
    /// <summary>
    /// Claim types the Kinde scheme puts on the <see cref="System.Security.Claims.ClaimsPrincipal"/>.
    /// </summary>
    /// <remarks>
    /// These are the JWT claim names, not the legacy SOAP URIs, so what you read here matches what
    /// is actually in the token.
    /// </remarks>
    public static class KindeClaimTypes
    {
        /// <summary>The agent identity the token was issued to.</summary>
        public const string Subject = "sub";

        /// <summary>The acting agent of a delegated token (<c>act.sub</c>).</summary>
        public const string ActorSubject = "act_sub";

        /// <summary>The client the token was issued to. A URL for a CIMD client.</summary>
        public const string ClientId = "client_id";

        /// <summary>One claim per granted scope.</summary>
        public const string Scope = "scope";

        /// <summary>One claim per granted permission.</summary>
        public const string Permission = "permissions";

        /// <summary>The organization the token is scoped to.</summary>
        public const string OrganizationCode = "org_code";
    }
}
