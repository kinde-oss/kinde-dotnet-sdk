using System.Net;

namespace Kinde.Api.Flows
{
    public class KindeAuthenticationException : Exception
    {
        public HttpStatusCode? StatusCode { get; }
        public string? ResponseContent { get; }

        public KindeAuthenticationException(string message) : base(message) { }

        public KindeAuthenticationException(
            string message,
            HttpStatusCode statusCode,
            string? responseContent = null,
            bool includeResponseContent = false)
            : base(message)
        {
            StatusCode = statusCode;
            ResponseContent = includeResponseContent ? responseContent : null;
        }

        public KindeAuthenticationException(string message, Exception innerException)
            : base(message, innerException) { }
    }
}