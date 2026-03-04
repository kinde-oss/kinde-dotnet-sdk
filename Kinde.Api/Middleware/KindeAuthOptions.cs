namespace Kinde.Auth.Middleware;

/// <summary>
/// Configuration options for the Kinde invitation-redirect middleware.
/// Bind from appsettings.json under the "Kinde" section, or configure inline.
/// </summary>
public class KindeAuthOptions
{
    public const string SectionName = "Kinde";

    /// <summary>
    /// Your Kinde domain, e.g. "yourapp.kinde.com".
    /// Used to build the base redirect URL when <see cref="RedirectUrlBase"/> is not set.
    /// </summary>
    public string Domain { get; set; } = string.Empty;

    /// <summary>
    /// Override the absolute base URL used when building redirect URLs,
    /// e.g. "https://localhost:7001" in dev or "https://myapp.com" in production.
    /// When null, falls back to <see cref="RedirectScheme"/>://<see cref="Domain"/>.
    /// Mirrors <c>redirectURLBase || config.redirectURL</c> in the Next.js SDK.
    /// </summary>
    public string? RedirectUrlBase { get; set; }

    /// <summary>
    /// Scheme used when <see cref="RedirectUrlBase"/> is not set. Defaults to "https".
    /// </summary>
    public string RedirectScheme { get; set; } = "https";

    /// <summary>
    /// Relative path of the registration route.
    /// Mirrors <c>registerPage</c> in the Next.js SDK.
    /// Defaults to "/api/auth/register".
    /// </summary>
    public string RegisterPath { get; set; } = "/api/auth/register";

    /// <summary>
    /// Relative path of the login route.
    /// Used as the fallback redirect when an error occurs building the register URL.
    /// Mirrors <c>loginRedirectUrl</c> fallback in the Next.js SDK.
    /// Defaults to "/api/auth/login".
    /// </summary>
    public string LoginPath { get; set; } = "/api/auth/login";

    /// <summary>
    /// When true, verbose diagnostic output is written via ILogger.
    /// Mirrors <c>config.isDebugMode</c> in the Next.js SDK.
    /// </summary>
    public bool IsDebugMode { get; set; } = false;
}