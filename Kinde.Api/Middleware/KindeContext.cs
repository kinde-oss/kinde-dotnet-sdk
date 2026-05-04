using System;

namespace Kinde.Auth.Middleware
{
    public sealed class KindeContext
    {
        public IDictionary<string, string> Query { get; set; } =
            new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

        public string Method { get; set; } = "GET";

        public string Path { get; set; } = "/";

        public Action<string>? RedirectAction { get; set; }
    }
}