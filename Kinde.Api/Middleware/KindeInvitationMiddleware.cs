using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Net.Http;
using System.Web;

namespace Kinde.Auth.Middleware;

/// <summary>
/// ASP.NET Core middleware that detects an <c>invitation_code</c> query
/// parameter and automatically redirects the user to the Kinde registration
/// auth-flow (302), so the invitation is processed without the consuming
/// application needing to handle it manually.
///
/// Generated from OpenAPI spec:
///   operationId: handleInvitationRedirect
///   path:        GET /api/auth/invitation
///   tag:         Invitations
///   x-dotnet-middleware: KindeInvitationMiddleware
/// </summary>
public class KindeInvitationMiddleware
{
    private readonly RequestDelegate _next;
    private readonly KindeAuthOptions _options;
    private readonly ILogger<KindeInvitationMiddleware> _logger;

    public KindeInvitationMiddleware(
        RequestDelegate next,
        IOptions<KindeAuthOptions> options,
        ILogger<KindeInvitationMiddleware> logger)
    {
        _next = next;
        _options = options.Value;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var invitationCode = context.Request.Query["invitation_code"]
                                    .ToString()
                                    .Trim();

        if (!string.IsNullOrEmpty(invitationCode))
        {
            HandleInvitationCodeRedirect(context, invitationCode);
            return;
        }

        await _next(context);
    }

    /// <summary>
    /// Builds the absolute register URL and issues a 302 redirect.
    /// Falls back to the login page if anything goes wrong.
    ///
    /// Mirrors handleInvitationCodeRedirect() in the Kinde Next.js SDK.
    /// </summary>
    private void HandleInvitationCodeRedirect(HttpContext context, string invitationCode)
    {
        try
        {
            var redirectUrl = BuildRegisterUrl(invitationCode);

            if (_options.IsDebugMode)
                _logger.LogDebug(
                    "KindeInvitationMiddleware: invitation_code detected, " +
                    "redirecting to register → {Url}", redirectUrl);

            context.Response.Redirect(redirectUrl, permanent: false);
        }
        catch (Exception ex)
        {
            if (_options.IsDebugMode)
                _logger.LogError(ex,
                    "KindeInvitationMiddleware: error building register redirect URL, " +
                    "falling back to login");

            context.Response.Redirect(BuildFallbackLoginUrl(), permanent: false);
        }
    }

    /// <summary>
    /// Builds the full registration URL with invitation params appended.
    /// Example output:
    ///   https://yourapp.com/api/auth/register?invitation_code=abc123&amp;is_invitation=true
    /// </summary>
    private string BuildRegisterUrl(string invitationCode)
    {
        var baseUrl = ResolveBaseUrl();
        var qs = HttpUtility.ParseQueryString(string.Empty);
        qs["invitation_code"] = invitationCode;
        qs["is_invitation"] = "true";
        return $"{baseUrl}{_options.RegisterPath}?{qs}";
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
            : _options.RedirectUrlBase.TrimEnd('/');
    }
}
