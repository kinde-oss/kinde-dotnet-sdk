#nullable enable

using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using Kinde.Accounts.Client;
using Kinde.Accounts.Api;
using Kinde.Accounts.Model;
using Kinde.Accounts.Model.Entities;

namespace Kinde.Accounts.Api
{
    /// <summary>
    /// Represents a collection of functions to interact with the API endpoints
    /// This class is registered as transient.
    /// </summary>
    public interface IOAuthApi : IApi
    {
        /// <summary>
        /// The class containing the events
        /// </summary>
        OAuthApiEvents Events { get; }

        /// <summary>
        /// Get user profile
        /// </summary>
        /// <remarks>
        /// This endpoint returns a user&#39;s ID, names, profile picture URL and email of the currently logged in user. 
        /// </remarks>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task&lt;ApiResponse&lt;UserProfileV2&gt;&gt;</returns>
        Task<ApiResponse<UserProfileV2>> GetUserProfileV2Async(System.Threading.CancellationToken cancellationToken = default);

        /// <summary>
        /// Get user profile
        /// </summary>
        /// <remarks>
        /// This endpoint returns a user&#39;s ID, names, profile picture URL and email of the currently logged in user. 
        /// </remarks>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task&lt;ApiResponse&gt;UserProfileV2&gt;?&gt;</returns>
        Task<ApiResponse<UserProfileV2>?> GetUserProfileV2OrDefaultAsync(System.Threading.CancellationToken cancellationToken = default);

        /// <summary>
        /// Introspect
        /// </summary>
        /// <remarks>
        /// Retrieve information about the provided token.
        /// </remarks>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="token">The token to be introspected.</param>
        /// <param name="tokenTypeHint">A hint about the token type being queried in the request. (optional)</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task&lt;ApiResponse&lt;TokenIntrospect&gt;&gt;</returns>
        Task<ApiResponse<TokenIntrospect>> TokenIntrospectionAsync(string token, Option<string> tokenTypeHint = default, System.Threading.CancellationToken cancellationToken = default);

        /// <summary>
        /// Introspect
        /// </summary>
        /// <remarks>
        /// Retrieve information about the provided token.
        /// </remarks>
        /// <param name="token">The token to be introspected.</param>
        /// <param name="tokenTypeHint">A hint about the token type being queried in the request. (optional)</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task&lt;ApiResponse&gt;TokenIntrospect&gt;?&gt;</returns>
        Task<ApiResponse<TokenIntrospect>?> TokenIntrospectionOrDefaultAsync(string token, Option<string> tokenTypeHint = default, System.Threading.CancellationToken cancellationToken = default);

        /// <summary>
        /// Revoke token
        /// </summary>
        /// <remarks>
        /// Use this endpoint to invalidate an access or refresh token. The token will no longer be valid for use.
        /// </remarks>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="clientId">The &#x60;client_id&#x60; of your application.</param>
        /// <param name="token">The token to be revoked.</param>
        /// <param name="clientSecret">The &#x60;client_secret&#x60; of your application. Required for backend apps only. (optional)</param>
        /// <param name="tokenTypeHint">The type of token to be revoked. (optional)</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task&lt;ApiResponse&lt;object&gt;&gt;</returns>
        Task<ApiResponse<object>> TokenRevocationAsync(string clientId, string token, Option<string> clientSecret = default, Option<string> tokenTypeHint = default, System.Threading.CancellationToken cancellationToken = default);

        /// <summary>
        /// Revoke token
        /// </summary>
        /// <remarks>
        /// Use this endpoint to invalidate an access or refresh token. The token will no longer be valid for use.
        /// </remarks>
        /// <param name="clientId">The &#x60;client_id&#x60; of your application.</param>
        /// <param name="token">The token to be revoked.</param>
        /// <param name="clientSecret">The &#x60;client_secret&#x60; of your application. Required for backend apps only. (optional)</param>
        /// <param name="tokenTypeHint">The type of token to be revoked. (optional)</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task&lt;ApiResponse&gt;object&gt;?&gt;</returns>
        Task<ApiResponse<object>?> TokenRevocationOrDefaultAsync(string clientId, string token, Option<string> clientSecret = default, Option<string> tokenTypeHint = default, System.Threading.CancellationToken cancellationToken = default);
    }

    /// <summary>
    /// Represents a collection of functions to interact with the API endpoints
    /// This class is registered as transient.
    /// </summary>
    public class OAuthApiEvents
    {
        /// <summary>
        /// The event raised after the server response
        /// </summary>
        public event EventHandler<ApiResponseEventArgs<UserProfileV2>>? OnGetUserProfileV2;

        /// <summary>
        /// The event raised after an error querying the server
        /// </summary>
        public event EventHandler<ExceptionEventArgs>? OnErrorGetUserProfileV2;

        internal void ExecuteOnGetUserProfileV2(ApiResponse<UserProfileV2> apiResponse)
        {
            OnGetUserProfileV2?.Invoke(this, new ApiResponseEventArgs<UserProfileV2>(apiResponse));
        }

        internal void ExecuteOnErrorGetUserProfileV2(Exception exception)
        {
            OnErrorGetUserProfileV2?.Invoke(this, new ExceptionEventArgs(exception));
        }

        /// <summary>
        /// The event raised after the server response
        /// </summary>
        public event EventHandler<ApiResponseEventArgs<TokenIntrospect>>? OnTokenIntrospection;

        /// <summary>
        /// The event raised after an error querying the server
        /// </summary>
        public event EventHandler<ExceptionEventArgs>? OnErrorTokenIntrospection;

        internal void ExecuteOnTokenIntrospection(ApiResponse<TokenIntrospect> apiResponse)
        {
            OnTokenIntrospection?.Invoke(this, new ApiResponseEventArgs<TokenIntrospect>(apiResponse));
        }

        internal void ExecuteOnErrorTokenIntrospection(Exception exception)
        {
            OnErrorTokenIntrospection?.Invoke(this, new ExceptionEventArgs(exception));
        }

        /// <summary>
        /// The event raised after the server response
        /// </summary>
        public event EventHandler<ApiResponseEventArgs<object>>? OnTokenRevocation;

        /// <summary>
        /// The event raised after an error querying the server
        /// </summary>
        public event EventHandler<ExceptionEventArgs>? OnErrorTokenRevocation;

        internal void ExecuteOnTokenRevocation(ApiResponse<object> apiResponse)
        {
            OnTokenRevocation?.Invoke(this, new ApiResponseEventArgs<object>(apiResponse));
        }

        internal void ExecuteOnErrorTokenRevocation(Exception exception)
        {
            OnErrorTokenRevocation?.Invoke(this, new ExceptionEventArgs(exception));
        }
    }

    /// <summary>
    /// Represents a collection of functions to interact with the API endpoints
    /// </summary>
    public sealed partial class OAuthApi : KiotaAccountsBase, IOAuthApi
    {
        private JsonSerializerOptions _jsonSerializerOptions;
        private readonly HttpClient _httpClient;
        private readonly TokenProvider<BearerToken> _bearerTokenProvider;

        /// <summary>
        /// The logger
        /// </summary>
        public ILogger<OAuthApi> Logger { get; }

        /// <summary>
        /// The HttpClient
        /// </summary>
        public HttpClient HttpClient => _httpClient;

        /// <summary>
        /// The class containing the events
        /// </summary>
        public OAuthApiEvents Events { get; }

        /// <summary>
        /// A token provider of type <see cref="BearerToken"/>
        /// </summary>
        public TokenProvider<BearerToken> BearerTokenProvider => _bearerTokenProvider;

        /// <summary>
        /// HttpClient for KiotaAccountsBase
        /// </summary>
        protected override HttpClient KiotaHttpClient => _httpClient;

        /// <summary>
        /// BearerTokenProvider for KiotaAccountsBase
        /// </summary>
        protected override TokenProvider<BearerToken> KiotaBearerTokenProvider => _bearerTokenProvider;

        /// <summary>
        /// Initializes a new instance of the <see cref="OAuthApi"/> class.
        /// </summary>
        /// <returns></returns>
        public OAuthApi(ILogger<OAuthApi> logger, HttpClient httpClient, JsonSerializerOptionsProvider jsonSerializerOptionsProvider, OAuthApiEvents oAuthApiEvents,
            TokenProvider<BearerToken> bearerTokenProvider)
        {
            _jsonSerializerOptions = jsonSerializerOptionsProvider.Options;
            Logger = logger;
            _httpClient = httpClient;
            Events = oAuthApiEvents;
            _bearerTokenProvider = bearerTokenProvider;
        }

        /// <summary>
        /// Processes the server response
        /// </summary>
        /// <param name="apiResponseLocalVar"></param>
        private void AfterGetUserProfileV2DefaultImplementation(ApiResponse<UserProfileV2> apiResponseLocalVar)
        {
            bool suppressDefaultLog = false;
            AfterGetUserProfileV2(ref suppressDefaultLog, apiResponseLocalVar);
            if (!suppressDefaultLog)
                Logger.LogInformation("{0,-9} | {1} | {3}", (apiResponseLocalVar.DownloadedAt - apiResponseLocalVar.RequestedAt).TotalSeconds, apiResponseLocalVar.StatusCode, apiResponseLocalVar.Path);
        }

        /// <summary>
        /// Processes the server response
        /// </summary>
        /// <param name="suppressDefaultLog"></param>
        /// <param name="apiResponseLocalVar"></param>
        partial void AfterGetUserProfileV2(ref bool suppressDefaultLog, ApiResponse<UserProfileV2> apiResponseLocalVar);

        /// <summary>
        /// Logs exceptions that occur while retrieving the server response
        /// </summary>
        /// <param name="exception"></param>
        /// <param name="pathFormat"></param>
        /// <param name="path"></param>
        private void OnErrorGetUserProfileV2DefaultImplementation(Exception exception, string pathFormat, string path)
        {
            bool suppressDefaultLog = false;
            OnErrorGetUserProfileV2(ref suppressDefaultLog, exception, pathFormat, path);
            if (!suppressDefaultLog)
                Logger.LogError(exception, "An error occurred while sending the request to the server.");
        }

        /// <summary>
        /// A partial method that gives developers a way to provide customized exception handling
        /// </summary>
        /// <param name="suppressDefaultLog"></param>
        /// <param name="exception"></param>
        /// <param name="pathFormat"></param>
        /// <param name="path"></param>
        partial void OnErrorGetUserProfileV2(ref bool suppressDefaultLog, Exception exception, string pathFormat, string path);

        /// <summary>
        /// Get user profile This endpoint returns a user&#39;s ID, names, profile picture URL and email of the currently logged in user. 
        /// </summary>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns><see cref="Task"/>&lt;<see cref="ApiResponse{T}"/>&gt; where T : <see cref="UserProfileV2"/></returns>
        public async Task<ApiResponse<UserProfileV2>?> GetUserProfileV2OrDefaultAsync(System.Threading.CancellationToken cancellationToken = default)
        {
            try
            {
                return await GetUserProfileV2Async(cancellationToken).ConfigureAwait(false);
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// Get user profile This endpoint returns a user&#39;s ID, names, profile picture URL and email of the currently logged in user. 
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns><see cref="Task"/>&lt;<see cref="ApiResponse{T}"/>&gt; where T : <see cref="UserProfileV2"/></returns>
        public async Task<ApiResponse<UserProfileV2>> GetUserProfileV2Async(System.Threading.CancellationToken cancellationToken = default)
        {
            DateTime requestedAtLocalVar = DateTime.UtcNow;

            try
            {
                // Create a fresh Kiota client with current token
                var kiotaClient = await CreateKiotaClientWithTokenAsync(cancellationToken).ConfigureAwait(false);

                // Call Kiota client
                var kiotaResponse = await kiotaClient.Oauth2.V2.User_profile.GetAsync(
                    cancellationToken: cancellationToken).ConfigureAwait(false);

                // Map Kiota response to the expected model
                var mappedResponse = KiotaMapper.Map<UserProfileV2>(kiotaResponse);

                // Create API response
                var apiResponseLocalVar = new ApiResponse<UserProfileV2>(
                    "/oauth2/v2/user_profile",
                    mappedResponse,
                    HttpStatusCode.OK,
                    requestedAtLocalVar);

                        AfterGetUserProfileV2DefaultImplementation(apiResponseLocalVar);
                        Events.ExecuteOnGetUserProfileV2(apiResponseLocalVar);

                        return apiResponseLocalVar;
            }
            catch (Exception e)
            {
                OnErrorGetUserProfileV2DefaultImplementation(e, "/oauth2/v2/user_profile", "/oauth2/v2/user_profile");
                Events.ExecuteOnErrorGetUserProfileV2(e);
                throw;
            }
        }

        partial void FormatTokenIntrospection(ref string token, ref Option<string> tokenTypeHint);

        /// <summary>
        /// Validates the request parameters
        /// </summary>
        /// <param name="token"></param>
        /// <param name="tokenTypeHint"></param>
        /// <returns></returns>
        private void ValidateTokenIntrospection(string token, Option<string> tokenTypeHint)
        {
            if (token == null)
                throw new ArgumentNullException(nameof(token));

            if (tokenTypeHint.IsSet && tokenTypeHint.Value == null)
                throw new ArgumentNullException(nameof(tokenTypeHint));
        }

        /// <summary>
        /// Processes the server response
        /// </summary>
        /// <param name="apiResponseLocalVar"></param>
        /// <param name="token"></param>
        /// <param name="tokenTypeHint"></param>
        private void AfterTokenIntrospectionDefaultImplementation(ApiResponse<TokenIntrospect> apiResponseLocalVar, string token, Option<string> tokenTypeHint)
        {
            bool suppressDefaultLog = false;
            AfterTokenIntrospection(ref suppressDefaultLog, apiResponseLocalVar, token, tokenTypeHint);
            if (!suppressDefaultLog)
                Logger.LogInformation("{0,-9} | {1} | {3}", (apiResponseLocalVar.DownloadedAt - apiResponseLocalVar.RequestedAt).TotalSeconds, apiResponseLocalVar.StatusCode, apiResponseLocalVar.Path);
        }

        /// <summary>
        /// Processes the server response
        /// </summary>
        /// <param name="suppressDefaultLog"></param>
        /// <param name="apiResponseLocalVar"></param>
        /// <param name="token"></param>
        /// <param name="tokenTypeHint"></param>
        partial void AfterTokenIntrospection(ref bool suppressDefaultLog, ApiResponse<TokenIntrospect> apiResponseLocalVar, string token, Option<string> tokenTypeHint);

        /// <summary>
        /// Logs exceptions that occur while retrieving the server response
        /// </summary>
        /// <param name="exception"></param>
        /// <param name="pathFormat"></param>
        /// <param name="path"></param>
        /// <param name="token"></param>
        /// <param name="tokenTypeHint"></param>
        private void OnErrorTokenIntrospectionDefaultImplementation(Exception exception, string pathFormat, string path, string token, Option<string> tokenTypeHint)
        {
            bool suppressDefaultLog = false;
            OnErrorTokenIntrospection(ref suppressDefaultLog, exception, pathFormat, path, token, tokenTypeHint);
            if (!suppressDefaultLog)
                Logger.LogError(exception, "An error occurred while sending the request to the server.");
        }

        /// <summary>
        /// A partial method that gives developers a way to provide customized exception handling
        /// </summary>
        /// <param name="suppressDefaultLog"></param>
        /// <param name="exception"></param>
        /// <param name="pathFormat"></param>
        /// <param name="path"></param>
        /// <param name="token"></param>
        /// <param name="tokenTypeHint"></param>
        partial void OnErrorTokenIntrospection(ref bool suppressDefaultLog, Exception exception, string pathFormat, string path, string token, Option<string> tokenTypeHint);

        /// <summary>
        /// Introspect Retrieve information about the provided token.
        /// </summary>
        /// <param name="token">The token to be introspected.</param>
        /// <param name="tokenTypeHint">A hint about the token type being queried in the request. (optional)</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns><see cref="Task"/>&lt;<see cref="ApiResponse{T}"/>&gt; where T : <see cref="TokenIntrospect"/></returns>
        public async Task<ApiResponse<TokenIntrospect>?> TokenIntrospectionOrDefaultAsync(string token, Option<string> tokenTypeHint = default, System.Threading.CancellationToken cancellationToken = default)
        {
            try
            {
                return await TokenIntrospectionAsync(token, tokenTypeHint, cancellationToken).ConfigureAwait(false);
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// Introspect Retrieve information about the provided token.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="token">The token to be introspected.</param>
        /// <param name="tokenTypeHint">A hint about the token type being queried in the request. (optional)</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns><see cref="Task"/>&lt;<see cref="ApiResponse{T}"/>&gt; where T : <see cref="TokenIntrospect"/></returns>
        public async Task<ApiResponse<TokenIntrospect>> TokenIntrospectionAsync(string token, Option<string> tokenTypeHint = default, System.Threading.CancellationToken cancellationToken = default)
        {
            DateTime requestedAtLocalVar = DateTime.UtcNow;

            try
            {
                ValidateTokenIntrospection(token, tokenTypeHint);
                FormatTokenIntrospection(ref token, ref tokenTypeHint);

                // Create a fresh Kiota client with current token
                var kiotaClient = await CreateKiotaClientWithTokenAsync(cancellationToken).ConfigureAwait(false);

                // Create request body
                var requestBody = new global::Kinde.Api.Kiota.Accounts.Oauth2.Introspect.IntrospectPostRequestBody
                {
                    Token = token
                };

                // Set token type hint if provided
                if (tokenTypeHint.IsSet && !string.IsNullOrEmpty(tokenTypeHint.Value))
                {
                    if (Enum.TryParse<global::Kinde.Api.Kiota.Accounts.Oauth2.Introspect.IntrospectPostRequestBody_token_type_hint>(
                        tokenTypeHint.Value.Replace("_", ""), true, out var hint))
                    {
                        requestBody.TokenTypeHint = hint;
                    }
                }

                // Call Kiota client
                var kiotaResponse = await kiotaClient.Oauth2.Introspect.PostAsync(
                    requestBody,
                    cancellationToken: cancellationToken).ConfigureAwait(false);

                // Map Kiota response to the expected model
                var mappedResponse = KiotaMapper.Map<TokenIntrospect>(kiotaResponse);

                // Create API response
                var apiResponseLocalVar = new ApiResponse<TokenIntrospect>(
                    "/oauth2/introspect",
                    mappedResponse,
                    HttpStatusCode.OK,
                    requestedAtLocalVar);

                AfterTokenIntrospectionDefaultImplementation(apiResponseLocalVar, token, tokenTypeHint);
                Events.ExecuteOnTokenIntrospection(apiResponseLocalVar);

                return apiResponseLocalVar;
            }
            catch (Exception e)
            {
                OnErrorTokenIntrospectionDefaultImplementation(e, "/oauth2/introspect", "/oauth2/introspect", token, tokenTypeHint);
                Events.ExecuteOnErrorTokenIntrospection(e);
                throw;
            }
        }

        partial void FormatTokenRevocation(ref string clientId, ref string token, ref Option<string> clientSecret, ref Option<string> tokenTypeHint);

        /// <summary>
        /// Validates the request parameters
        /// </summary>
        /// <param name="clientId"></param>
        /// <param name="token"></param>
        /// <param name="clientSecret"></param>
        /// <param name="tokenTypeHint"></param>
        /// <returns></returns>
        private void ValidateTokenRevocation(string clientId, string token, Option<string> clientSecret, Option<string> tokenTypeHint)
        {
            if (clientId == null)
                throw new ArgumentNullException(nameof(clientId));

            if (token == null)
                throw new ArgumentNullException(nameof(token));

            if (clientSecret.IsSet && clientSecret.Value == null)
                throw new ArgumentNullException(nameof(clientSecret));

            if (tokenTypeHint.IsSet && tokenTypeHint.Value == null)
                throw new ArgumentNullException(nameof(tokenTypeHint));
        }

        /// <summary>
        /// Processes the server response
        /// </summary>
        /// <param name="apiResponseLocalVar"></param>
        /// <param name="clientId"></param>
        /// <param name="token"></param>
        /// <param name="clientSecret"></param>
        /// <param name="tokenTypeHint"></param>
        private void AfterTokenRevocationDefaultImplementation(ApiResponse<object> apiResponseLocalVar, string clientId, string token, Option<string> clientSecret, Option<string> tokenTypeHint)
        {
            bool suppressDefaultLog = false;
            AfterTokenRevocation(ref suppressDefaultLog, apiResponseLocalVar, clientId, token, clientSecret, tokenTypeHint);
            if (!suppressDefaultLog)
                Logger.LogInformation("{0,-9} | {1} | {3}", (apiResponseLocalVar.DownloadedAt - apiResponseLocalVar.RequestedAt).TotalSeconds, apiResponseLocalVar.StatusCode, apiResponseLocalVar.Path);
        }

        /// <summary>
        /// Processes the server response
        /// </summary>
        /// <param name="suppressDefaultLog"></param>
        /// <param name="apiResponseLocalVar"></param>
        /// <param name="clientId"></param>
        /// <param name="token"></param>
        /// <param name="clientSecret"></param>
        /// <param name="tokenTypeHint"></param>
        partial void AfterTokenRevocation(ref bool suppressDefaultLog, ApiResponse<object> apiResponseLocalVar, string clientId, string token, Option<string> clientSecret, Option<string> tokenTypeHint);

        /// <summary>
        /// Logs exceptions that occur while retrieving the server response
        /// </summary>
        /// <param name="exception"></param>
        /// <param name="pathFormat"></param>
        /// <param name="path"></param>
        /// <param name="clientId"></param>
        /// <param name="token"></param>
        /// <param name="clientSecret"></param>
        /// <param name="tokenTypeHint"></param>
        private void OnErrorTokenRevocationDefaultImplementation(Exception exception, string pathFormat, string path, string clientId, string token, Option<string> clientSecret, Option<string> tokenTypeHint)
        {
            bool suppressDefaultLog = false;
            OnErrorTokenRevocation(ref suppressDefaultLog, exception, pathFormat, path, clientId, token, clientSecret, tokenTypeHint);
            if (!suppressDefaultLog)
                Logger.LogError(exception, "An error occurred while sending the request to the server.");
        }

        /// <summary>
        /// A partial method that gives developers a way to provide customized exception handling
        /// </summary>
        /// <param name="suppressDefaultLog"></param>
        /// <param name="exception"></param>
        /// <param name="pathFormat"></param>
        /// <param name="path"></param>
        /// <param name="clientId"></param>
        /// <param name="token"></param>
        /// <param name="clientSecret"></param>
        /// <param name="tokenTypeHint"></param>
        partial void OnErrorTokenRevocation(ref bool suppressDefaultLog, Exception exception, string pathFormat, string path, string clientId, string token, Option<string> clientSecret, Option<string> tokenTypeHint);

        /// <summary>
        /// Revoke token Use this endpoint to invalidate an access or refresh token. The token will no longer be valid for use.
        /// </summary>
        /// <param name="clientId">The &#x60;client_id&#x60; of your application.</param>
        /// <param name="token">The token to be revoked.</param>
        /// <param name="clientSecret">The &#x60;client_secret&#x60; of your application. Required for backend apps only. (optional)</param>
        /// <param name="tokenTypeHint">The type of token to be revoked. (optional)</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns><see cref="Task"/>&lt;<see cref="ApiResponse{T}"/>&gt; where T : <see cref="object"/></returns>
        public async Task<ApiResponse<object>?> TokenRevocationOrDefaultAsync(string clientId, string token, Option<string> clientSecret = default, Option<string> tokenTypeHint = default, System.Threading.CancellationToken cancellationToken = default)
        {
            try
            {
                return await TokenRevocationAsync(clientId, token, clientSecret, tokenTypeHint, cancellationToken).ConfigureAwait(false);
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// Revoke token Use this endpoint to invalidate an access or refresh token. The token will no longer be valid for use.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="clientId">The &#x60;client_id&#x60; of your application.</param>
        /// <param name="token">The token to be revoked.</param>
        /// <param name="clientSecret">The &#x60;client_secret&#x60; of your application. Required for backend apps only. (optional)</param>
        /// <param name="tokenTypeHint">The type of token to be revoked. (optional)</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns><see cref="Task"/>&lt;<see cref="ApiResponse{T}"/>&gt; where T : <see cref="object"/></returns>
        public async Task<ApiResponse<object>> TokenRevocationAsync(string clientId, string token, Option<string> clientSecret = default, Option<string> tokenTypeHint = default, System.Threading.CancellationToken cancellationToken = default)
        {
            DateTime requestedAtLocalVar = DateTime.UtcNow;

            try
            {
                ValidateTokenRevocation(clientId, token, clientSecret, tokenTypeHint);
                FormatTokenRevocation(ref clientId, ref token, ref clientSecret, ref tokenTypeHint);

                // Create a fresh Kiota client with current token
                var kiotaClient = await CreateKiotaClientWithTokenAsync(cancellationToken).ConfigureAwait(false);

                // Create request body
                var requestBody = new global::Kinde.Api.Kiota.Accounts.Oauth2.Revoke.RevokePostRequestBody
                {
                    ClientId = clientId,
                    Token = token
                };

                // Set optional fields
                if (clientSecret.IsSet && !string.IsNullOrEmpty(clientSecret.Value))
                {
                    requestBody.ClientSecret = clientSecret.Value;
                }

                if (tokenTypeHint.IsSet && !string.IsNullOrEmpty(tokenTypeHint.Value))
                {
                    if (Enum.TryParse<global::Kinde.Api.Kiota.Accounts.Oauth2.Revoke.RevokePostRequestBody_token_type_hint>(
                        tokenTypeHint.Value.Replace("_", ""), true, out var hint))
                    {
                        requestBody.TokenTypeHint = hint;
                    }
                }

                // Call Kiota client - returns void
                await kiotaClient.Oauth2.Revoke.PostAsync(
                    requestBody,
                    cancellationToken: cancellationToken).ConfigureAwait(false);

                // Create API response (successful revocation returns no content)
                var apiResponseLocalVar = new ApiResponse<object>(
                    "/oauth2/revoke",
                    new object(),
                    HttpStatusCode.OK,
                    requestedAtLocalVar);

                AfterTokenRevocationDefaultImplementation(apiResponseLocalVar, clientId, token, clientSecret, tokenTypeHint);
                Events.ExecuteOnTokenRevocation(apiResponseLocalVar);

                return apiResponseLocalVar;
            }
            catch (Exception e)
            {
                OnErrorTokenRevocationDefaultImplementation(e, "/oauth2/revoke", "/oauth2/revoke", clientId, token, clientSecret, tokenTypeHint);
                Events.ExecuteOnErrorTokenRevocation(e);
                throw;
            }
        }
    }
}
