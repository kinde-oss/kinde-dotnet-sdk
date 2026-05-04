using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Kinde.Auth.Middleware
{
    /// <summary>
    /// Framework-agnostic middleware-like component that detects an
    /// invitation_code query parameter and redirects to the Kinde
    /// registration flow.
    /// </summary>
    public class KindeInvitationMiddleware
    {
        private readonly KindePipelineDelegate _next;
        private readonly KindeAuthOptions _options;
        private readonly ILogger<KindeInvitationMiddleware> _logger;

        public KindeInvitationMiddleware(
            KindePipelineDelegate next,
            IOptions<KindeAuthOptions> options,
            ILogger<KindeInvitationMiddleware> logger)
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
            _options = options?.Value ?? throw new ArgumentNullException(nameof(options));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task InvokeAsync(KindeContext context)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));

            context.Query.TryGetValue("invitation_code", out var invitationCode);
            invitationCode = invitationCode?.Trim();

            if (!string.IsNullOrEmpty(invitationCode))
            {
                var isSafeMethod =
                    string.Equals(context.Method, "GET", StringComparison.OrdinalIgnoreCase) ||
                    string.Equals(context.Method, "HEAD", StringComparison.OrdinalIgnoreCase);

                if (!isSafeMethod)
                {
                    if (_options.IsDebugMode)
                    {
                        _logger.LogDebug(
                            "KindeInvitationMiddleware: invitation_code present but method is {Method} — skipping redirect to preserve request semantics.",
                            context.Method);
                    }

                    await _next(context);
                    return;
                }

                var isAlreadyOnRegisterPath = PathStartsWithSegment(
                    context.Path,
                    _options.RegisterPath);

                if (isAlreadyOnRegisterPath)
                {
                    if (_options.IsDebugMode)
                    {
                        _logger.LogDebug(
                            "KindeInvitationMiddleware: request is already on register path ({Path}), passing through to avoid redirect loop.",
                            context.Path);
                    }

                    await _next(context);
                    return;
                }

                HandleInvitationCodeRedirect(context, invitationCode);
                return;
            }

            await _next(context);
        }

        /// <summary>
        /// Builds the absolute register URL and invokes redirect action.
        /// Falls back to the login page if anything goes wrong.
        /// </summary>
        private void HandleInvitationCodeRedirect(KindeContext context, string invitationCode)
        {
            try
            {
                var redirectUrl = BuildRegisterUrl(invitationCode);

                if (_options.IsDebugMode)
                {
                    _logger.LogDebug(
                        "KindeInvitationMiddleware: invitation_code detected, redirecting to register → {Url}",
                        redirectUrl);
                }

                context.RedirectAction?.Invoke(redirectUrl);
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "KindeInvitationMiddleware: error building register redirect URL, falling back to login");

                context.RedirectAction?.Invoke(BuildFallbackLoginUrl());
            }
        }

        /// <summary>
        /// Builds the full registration URL with invitation params appended.
        /// Example:
        /// https://yourapp.com/api/auth/register?invitation_code=abc123&is_invitation=true
        /// </summary>
        private string BuildRegisterUrl(string invitationCode)
        {
            var baseUrl = ResolveBaseUrl();
            var separator = _options.RegisterPath.Contains('?') ? "&" : "?";

            var encodedInvitationCode = Uri.EscapeDataString(invitationCode);
            return $"{baseUrl}{_options.RegisterPath}{separator}invitation_code={encodedInvitationCode}&is_invitation=true";
        }

        private string BuildFallbackLoginUrl()
        {
            var baseUrl = ResolveBaseUrl();
            return $"{baseUrl}{_options.LoginPath}";
        }

        private string ResolveBaseUrl()
        {
            return string.IsNullOrWhiteSpace(_options.RedirectUrlBase)
                ? $"{_options.RedirectScheme}://{_options.Domain}"
                : _options.RedirectUrlBase!.TrimEnd('/');
        }

        private static bool PathStartsWithSegment(string requestPath, string segmentPath)
        {
            requestPath = NormalizePath(requestPath);
            segmentPath = NormalizePath(segmentPath);

            if (string.Equals(requestPath, segmentPath, StringComparison.OrdinalIgnoreCase))
                return true;

            return requestPath.StartsWith(segmentPath + "/", StringComparison.OrdinalIgnoreCase);
        }

        private static string NormalizePath(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
                return "/";

            path = path.Trim();

            if (!path.StartsWith("/"))
                path = "/" + path;

            return path.TrimEnd('/');
        }
    }
}