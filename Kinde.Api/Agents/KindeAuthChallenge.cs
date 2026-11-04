#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kinde.Api.Agents
{
    /// <summary>Why a request was not authenticated.</summary>
    public enum KindeAuthFailureKind
    {
        /// <summary>No Authorization header, or not a bearer credential.</summary>
        MissingToken = 0,

        /// <summary>A token was presented but failed verification.</summary>
        InvalidToken = 1,

        /// <summary>The token is valid but lacks a required scope.</summary>
        InsufficientScope = 2,

        /// <summary>Kinde could not be reached, or rejected this resource server's own credentials.</summary>
        AuthorizationServerUnavailable = 3
    }

    /// <summary>
    /// Builds the <c>WWW-Authenticate</c> header that tells an MCP client where to authenticate,
    /// per RFC 9728. Without the <c>resource_metadata</c> parameter a client has no way to discover
    /// the authorization server and simply reports a failure to the user.
    /// </summary>
    public static class KindeAuthChallenge
    {
        /// <summary>
        /// The status code for a failure. 403 for insufficient scope, 503 when Kinde is unavailable
        /// (so a client retries rather than restarting authorization), 401 otherwise.
        /// </summary>
        public static int StatusCodeFor(KindeAuthFailureKind kind) => kind switch
        {
            KindeAuthFailureKind.InsufficientScope => 403,
            KindeAuthFailureKind.AuthorizationServerUnavailable => 503,
            _ => 401
        };

        /// <summary>
        /// Builds the header value, or returns null when no challenge applies — an outage is not an
        /// authentication problem, and challenging would send the agent into a pointless login loop.
        /// </summary>
        /// <param name="kind">Why the request failed.</param>
        /// <param name="resourceMetadataUrl">Absolute URL of this resource's metadata document.</param>
        /// <param name="description">Optional human-readable detail.</param>
        /// <param name="requiredScopes">Scopes the request needed, included for insufficient scope.</param>
        public static string? Build(
            KindeAuthFailureKind kind,
            string resourceMetadataUrl,
            string? description = null,
            IEnumerable<string>? requiredScopes = null)
        {
            if (kind == KindeAuthFailureKind.AuthorizationServerUnavailable)
            {
                return null;
            }

            var parameters = new List<string>();

            if (!string.IsNullOrWhiteSpace(resourceMetadataUrl))
            {
                parameters.Add(Parameter("resource_metadata", resourceMetadataUrl));
            }

            switch (kind)
            {
                case KindeAuthFailureKind.InvalidToken:
                    parameters.Add(Parameter("error", "invalid_token"));
                    break;
                case KindeAuthFailureKind.InsufficientScope:
                    parameters.Add(Parameter("error", "insufficient_scope"));
                    var scopes = (requiredScopes ?? Enumerable.Empty<string>())
                        .Where(s => !string.IsNullOrWhiteSpace(s))
                        .ToArray();
                    if (scopes.Length > 0)
                    {
                        parameters.Add(Parameter("scope", string.Join(" ", scopes)));
                    }
                    break;
            }

            if (!string.IsNullOrWhiteSpace(description) && kind != KindeAuthFailureKind.MissingToken)
            {
                parameters.Add(Parameter("error_description", description!));
            }

            return parameters.Count == 0 ? "Bearer" : "Bearer " + string.Join(", ", parameters);
        }

        // Every value is quoted and escaped: an unescaped quote in a URL or description would
        // truncate the header and lose the parameters that follow it.
        private static string Parameter(string name, string value) =>
            $"{name}=\"{Sanitize(value).Replace("\\", "\\\\").Replace("\"", "\\\"")}\"";

        /// <summary>
        /// Reduces a value to characters that are legal in an HTTP header.
        /// </summary>
        /// <remarks>
        /// IdentityModel validation messages can contain newlines -- IDX10503 lists each candidate
        /// key on its own line -- and those reach here through <c>FailureReason</c>. Kestrel throws
        /// on a response header containing control or non-ASCII characters, which would turn a 401
        /// carrying <c>resource_metadata</c> into a 500 and break discovery for that request.
        /// </remarks>
        private static string Sanitize(string value)
        {
            var builder = new StringBuilder(value.Length);

            foreach (var character in value)
            {
                if (character is '\r' or '\n' or '\t')
                {
                    // Keep the words, lose the line break.
                    if (builder.Length > 0 && builder[^1] != ' ')
                    {
                        builder.Append(' ');
                    }
                }
                else if (character >= ' ' && character <= '~')
                {
                    builder.Append(character);
                }
                // Anything else, control or non-ASCII, is dropped.
            }

            return builder.ToString().Trim();
        }
    }
}
