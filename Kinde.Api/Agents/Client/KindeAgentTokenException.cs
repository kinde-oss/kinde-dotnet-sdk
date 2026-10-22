#nullable enable
using System;
using System.Net;

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
        /// Whether Kinde rejected the grant itself, as opposed to failing to answer. Only a
        /// rejection means the stored token is beyond saving; a 429 or 5xx is worth retrying with
        /// the same refresh token.
        /// </summary>
        public bool IsGrantRejected =>
            StatusCode is HttpStatusCode.BadRequest
                or HttpStatusCode.Unauthorized
                or HttpStatusCode.Forbidden;
    }
}
