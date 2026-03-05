using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Kinde.Auth.Middleware;

/// <summary>
/// Extension methods for wiring up the Kinde invitation-redirect middleware
/// in an ASP.NET Core application.
/// </summary>
public static class KindeAuthExtensions
{
    /// <summary>
    /// Registers Kinde configuration options with the DI container.
    ///
    /// <code>
    /// builder.Services.AddKindeAuth(options =>
    /// {
    ///     options.Domain         = "yourapp.kinde.com";
    ///     options.RedirectUrlBase = "https://localhost:7001"; // dev only
    /// });
    /// </code>
    /// </summary>
    public static IServiceCollection AddKindeAuth(
        this IServiceCollection services,
        Action<KindeAuthOptions>? configure = null)
    {
        services.AddOptions<KindeAuthOptions>()
                .BindConfiguration(KindeAuthOptions.SectionName)
                .Validate(
                    o => !string.IsNullOrWhiteSpace(o.RedirectUrlBase) || !string.IsNullOrWhiteSpace(o.Domain),
                    $"{nameof(KindeAuthOptions.RedirectUrlBase)} or {nameof(KindeAuthOptions.Domain)} must be provided.")
                .Validate(
                    o => !string.IsNullOrWhiteSpace(o.RegisterPath) && o.RegisterPath.StartsWith('/'),
                    $"{nameof(KindeAuthOptions.RegisterPath)} must start with '/'.")
                .Validate(
                    o => !string.IsNullOrWhiteSpace(o.LoginPath) && o.LoginPath.StartsWith('/'),
                    $"{nameof(KindeAuthOptions.LoginPath)} must start with '/'.")
                .ValidateOnStart();

        if (configure is not null)
            services.PostConfigure(configure);

        return services;
    }
    /// <summary>
    /// Adds <see cref="KindeInvitationMiddleware"/> to the request pipeline.
    ///
    /// Place this <b>before</b> UseAuthentication() and UseAuthorization()
    /// so invitation links are intercepted before any auth checks run.
    ///
    /// <code>
    /// app.UseKindeInvitationHandler();
    /// app.UseAuthentication();
    /// app.UseAuthorization();
    /// </code>
    /// </summary>
    public static IApplicationBuilder UseKindeInvitationHandler(
        this IApplicationBuilder app)
        => app.UseMiddleware<KindeInvitationMiddleware>();
}