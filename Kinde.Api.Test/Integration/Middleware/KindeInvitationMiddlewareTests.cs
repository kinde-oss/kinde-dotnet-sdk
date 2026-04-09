using Kinde.Auth.Middleware;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Options;
using Xunit;

namespace Kinde.Api.Tests.Middleware
{
    public class KindeInvitationMiddlewareTests
    {
        private static KindeAuthOptions CreateOptions()
        {
            return new KindeAuthOptions
            {
                RegisterPath = "/api/auth/register",
                LoginPath = "/api/auth/login",
                RedirectScheme = "https",
                Domain = "example.com",
                RedirectUrlBase = null,
                IsDebugMode = true
            };
        }

        private static KindeInvitationMiddleware CreateMiddleware(
            KindePipelineDelegate next,
            KindeAuthOptions? options = null)
        {
            return new KindeInvitationMiddleware(
                next,
                Options.Create(options ?? CreateOptions()),
                NullLogger<KindeInvitationMiddleware>.Instance);
        }

        [Fact]
        public async Task InvokeAsync_Should_CallNext_When_NoInvitationCode()
        {
            // Arrange
            var nextCalled = false;

            KindePipelineDelegate next = context =>
            {
                nextCalled = true;
                return Task.CompletedTask;
            };

            var middleware = CreateMiddleware(next);

            var context = new KindeContext
            {
                Method = "GET",
                Path = "/home",
                Query = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
            };

            // Act
            await middleware.InvokeAsync(context);

            // Assert
            Assert.True(nextCalled);
        }

        [Fact]
        public async Task InvokeAsync_Should_Redirect_When_InvitationCodeExists_And_MethodIsGet()
        {
            // Arrange
            var nextCalled = false;
            string? redirectedUrl = null;

            KindePipelineDelegate next = context =>
            {
                nextCalled = true;
                return Task.CompletedTask;
            };

            var middleware = CreateMiddleware(next);

            var context = new KindeContext
            {
                Method = "GET",
                Path = "/welcome",
                Query = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
                {
                    ["invitation_code"] = "abc123"
                },
                RedirectAction = url => redirectedUrl = url
            };

            // Act
            await middleware.InvokeAsync(context);

            // Assert
            Assert.False(nextCalled);
            Assert.NotNull(redirectedUrl);
            Assert.Equal(
                "https://example.com/api/auth/register?invitation_code=abc123&is_invitation=true",
                redirectedUrl);
        }

        [Fact]
        public async Task InvokeAsync_Should_Redirect_When_InvitationCodeExists_And_MethodIsHead()
        {
            // Arrange
            var nextCalled = false;
            string? redirectedUrl = null;

            KindePipelineDelegate next = context =>
            {
                nextCalled = true;
                return Task.CompletedTask;
            };

            var middleware = CreateMiddleware(next);

            var context = new KindeContext
            {
                Method = "HEAD",
                Path = "/welcome",
                Query = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
                {
                    ["invitation_code"] = "head-code"
                },
                RedirectAction = url => redirectedUrl = url
            };

            // Act
            await middleware.InvokeAsync(context);

            // Assert
            Assert.False(nextCalled);
            Assert.Equal(
                "https://example.com/api/auth/register?invitation_code=head-code&is_invitation=true",
                redirectedUrl);
        }

        [Fact]
        public async Task InvokeAsync_Should_CallNext_When_MethodIsNotSafe()
        {
            // Arrange
            var nextCalled = false;
            string? redirectedUrl = null;

            KindePipelineDelegate next = context =>
            {
                nextCalled = true;
                return Task.CompletedTask;
            };

            var middleware = CreateMiddleware(next);

            var context = new KindeContext
            {
                Method = "POST",
                Path = "/submit",
                Query = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
                {
                    ["invitation_code"] = "abc123"
                },
                RedirectAction = url => redirectedUrl = url
            };

            // Act
            await middleware.InvokeAsync(context);

            // Assert
            Assert.True(nextCalled);
            Assert.Null(redirectedUrl);
        }

        [Fact]
        public async Task InvokeAsync_Should_CallNext_When_AlreadyOnRegisterPath()
        {
            // Arrange
            var nextCalled = false;
            string? redirectedUrl = null;

            KindePipelineDelegate next = context =>
            {
                nextCalled = true;
                return Task.CompletedTask;
            };

            var middleware = CreateMiddleware(next);

            var context = new KindeContext
            {
                Method = "GET",
                Path = "/api/auth/register",
                Query = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
                {
                    ["invitation_code"] = "abc123"
                },
                RedirectAction = url => redirectedUrl = url
            };

            // Act
            await middleware.InvokeAsync(context);

            // Assert
            Assert.True(nextCalled);
            Assert.Null(redirectedUrl);
        }

        [Fact]
        public async Task InvokeAsync_Should_CallNext_When_PathStartsWithRegisterPathSegment()
        {
            // Arrange
            var nextCalled = false;
            string? redirectedUrl = null;

            KindePipelineDelegate next = context =>
            {
                nextCalled = true;
                return Task.CompletedTask;
            };

            var middleware = CreateMiddleware(next);

            var context = new KindeContext
            {
                Method = "GET",
                Path = "/api/auth/register/extra",
                Query = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
                {
                    ["invitation_code"] = "abc123"
                },
                RedirectAction = url => redirectedUrl = url
            };

            // Act
            await middleware.InvokeAsync(context);

            // Assert
            Assert.True(nextCalled);
            Assert.Null(redirectedUrl);
        }

        [Fact]
        public async Task InvokeAsync_Should_Trim_InvitationCode()
        {
            // Arrange
            string? redirectedUrl = null;

            KindePipelineDelegate next = context => Task.CompletedTask;

            var middleware = CreateMiddleware(next);

            var context = new KindeContext
            {
                Method = "GET",
                Path = "/welcome",
                Query = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
                {
                    ["invitation_code"] = "   abc123   "
                },
                RedirectAction = url => redirectedUrl = url
            };

            // Act
            await middleware.InvokeAsync(context);

            // Assert
            Assert.Equal(
                "https://example.com/api/auth/register?invitation_code=abc123&is_invitation=true",
                redirectedUrl);
        }

        [Fact]
        public async Task InvokeAsync_Should_UrlEncode_InvitationCode()
        {
            // Arrange
            string? redirectedUrl = null;

            KindePipelineDelegate next = context => Task.CompletedTask;

            var middleware = CreateMiddleware(next);

            var context = new KindeContext
            {
                Method = "GET",
                Path = "/welcome",
                Query = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
                {
                    ["invitation_code"] = "abc 123+test@example.com"
                },
                RedirectAction = url => redirectedUrl = url
            };

            // Act
            await middleware.InvokeAsync(context);

            // Assert
            Assert.Equal(
                "https://example.com/api/auth/register?invitation_code=abc%20123%2Btest%40example.com&is_invitation=true",
                redirectedUrl);
        }

        [Fact]
        public async Task InvokeAsync_Should_UseRedirectUrlBase_When_Provided()
        {
            // Arrange
            string? redirectedUrl = null;

            var options = CreateOptions();
            options.RedirectUrlBase = "https://localhost:5001/";

            KindePipelineDelegate next = context => Task.CompletedTask;

            var middleware = CreateMiddleware(next, options);

            var context = new KindeContext
            {
                Method = "GET",
                Path = "/welcome",
                Query = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
                {
                    ["invitation_code"] = "abc123"
                },
                RedirectAction = url => redirectedUrl = url
            };

            // Act
            await middleware.InvokeAsync(context);

            // Assert
            Assert.Equal(
                "https://localhost:5001/api/auth/register?invitation_code=abc123&is_invitation=true",
                redirectedUrl);
        }

        [Fact]
        public async Task InvokeAsync_Should_FallbackToLogin_When_RedirectBuildFails()
        {
            // Arrange
            string? redirectedUrl = null;

            var options = CreateOptions();
            options.Domain = null!; // force failure while building base URL
            options.RedirectUrlBase = null;

            KindePipelineDelegate next = context => Task.CompletedTask;

            var middleware = CreateMiddleware(next, options);

            var context = new KindeContext
            {
                Method = "GET",
                Path = "/welcome",
                Query = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
                {
                    ["invitation_code"] = "abc123"
                },
                RedirectAction = url => redirectedUrl = url
            };

            // Act
            await middleware.InvokeAsync(context);

            // Assert
            Assert.NotNull(redirectedUrl);
        }

        [Fact]
        public async Task InvokeAsync_Should_NotThrow_When_RedirectActionIsNull()
        {
            // Arrange
            var nextCalled = false;

            KindePipelineDelegate next = context =>
            {
                nextCalled = true;
                return Task.CompletedTask;
            };

            var middleware = CreateMiddleware(next);

            var context = new KindeContext
            {
                Method = "GET",
                Path = "/welcome",
                Query = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
                {
                    ["invitation_code"] = "abc123"
                },
                RedirectAction = null
            };

            // Act
            var exception = await Record.ExceptionAsync(() => middleware.InvokeAsync(context));

            // Assert
            Assert.Null(exception);
            Assert.False(nextCalled); // redirect path handled, but no callback assigned
        }

        [Fact]
        public async Task InvokeAsync_Should_Treat_QueryKey_CaseInsensitive()
        {
            // Arrange
            string? redirectedUrl = null;

            KindePipelineDelegate next = context => Task.CompletedTask;

            var middleware = CreateMiddleware(next);

            var context = new KindeContext
            {
                Method = "GET",
                Path = "/welcome",
                Query = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
                {
                    ["INVITATION_CODE"] = "abc123"
                },
                RedirectAction = url => redirectedUrl = url
            };

            // Act
            await middleware.InvokeAsync(context);

            // Assert
            Assert.Equal(
                "https://example.com/api/auth/register?invitation_code=abc123&is_invitation=true",
                redirectedUrl);
        }

        [Fact]
        public async Task InvokeAsync_Should_CallNext_When_InvitationCodeIsWhitespace()
        {
            // Arrange
            var nextCalled = false;
            string? redirectedUrl = null;

            KindePipelineDelegate next = context =>
            {
                nextCalled = true;
                return Task.CompletedTask;
            };

            var middleware = CreateMiddleware(next);

            var context = new KindeContext
            {
                Method = "GET",
                Path = "/home",
                Query = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
                {
                    ["invitation_code"] = "   "
                },
                RedirectAction = url => redirectedUrl = url
            };

            // Act
            await middleware.InvokeAsync(context);

            // Assert
            Assert.True(nextCalled);
            Assert.Null(redirectedUrl);
        }
    }
}