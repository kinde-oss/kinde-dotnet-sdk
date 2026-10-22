#nullable enable
using System;
using System.Net;
using System.Text.Json;

namespace Kinde.Api.Agents.Client
{
    /// <summary>
    /// Thrown when Kinde's token endpoint returns a non-success status.
    /// </summary>
    /// <remarks>
    /// Carries the status so the caller can tell a rejected grant from a transient outage. That
    /// distinction decides whether a stored refresh token is discarded, and discarding one costs
    /// the user a fresh consent.
    /// </remarks>
    public class KindeAgentTokenException : InvalidOperationException
    {
        public KindeAgentTokenException(HttpStatusCode statusCode, string responseBody)
            : base($"The token request failed ({(int)statusCode}): {responseBody}")
        {
            StatusCode = statusCode;
            ResponseBody = responseBody ?? string.Empty;
        }

        /// <summary>The status Kinde returned.</summary>
        public HttpStatusCode StatusCode { get; }

        /// <summary>The raw response body, which carries the OAuth error code.</summary>
        public string ResponseBody { get; }

        /// <summary>
        /// Whether Kinde rejected the grant itself, as opposed to failing to answer. Only this
        /// means the stored token is beyond saving; anything else is worth retrying with the same
        /// refresh token.
        /// </summary>
        /// <remarks>
        /// Keyed on the OAuth <c>error</c> code rather than the status. RFC 6749 section 5.2
        /// returns <c>invalid_request</c>, <c>invalid_client</c> and <c>unauthorized_client</c>
        /// with the same 400 or 401, and a 403 can come from a gateway rather than from Kinde.
        /// Treating those as a rejected grant would discard a working refresh token over a
        /// configuration mistake or a proxy.
        /// </remarks>
        public bool IsGrantRejected =>
            string.Equals(Error, "invalid_grant", StringComparison.Ordinal);

        /// <summary>
        /// The OAuth <c>error</c> code from the response body, or null when it carries none.
        /// </summary>
        public string? Error
        {
            get
            {
                if (string.IsNullOrWhiteSpace(ResponseBody))
                {
                    return null;
                }

                try
                {
                    using var document = JsonDocument.Parse(ResponseBody);
                    return document.RootElement.ValueKind == JsonValueKind.Object
                           && document.RootElement.TryGetProperty("error", out var error)
                           && error.ValueKind == JsonValueKind.String
                        ? error.GetString()
                        : null;
                }
                catch (JsonException)
                {
                    // An unparseable body is not evidence of a rejected grant, so the token stays.
                    return null;
                }
            }
        }
    }
}
