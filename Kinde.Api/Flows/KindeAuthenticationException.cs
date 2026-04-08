using System.Net;

namespace Kinde.Api.Flows
{
    public class KindeAuthenticationException : Exception
    {
        public HttpStatusCode? StatusCode { get; }
        public string? ResponseContent { get; }

        public KindeAuthenticationException(string message) : base(message) { }

        public KindeAuthenticationException(string message, HttpStatusCode statusCode, string? responseContent = null)
            : base(message)
        {
            StatusCode = statusCode;
            ResponseContent = responseContent;
        }

        public KindeAuthenticationException(string message, Exception innerException)
            : base(message, innerException) { }
    }
}
