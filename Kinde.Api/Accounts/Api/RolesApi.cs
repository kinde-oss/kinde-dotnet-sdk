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
using Kinde.Accounts.Model.Responses;

namespace Kinde.Accounts.Api
{
    /// <summary>
    /// Represents a collection of functions to interact with the API endpoints
    /// This class is registered as transient.
    /// </summary>
    public interface IRolesApi : IApi
    {
        /// <summary>
        /// The class containing the events
        /// </summary>
        RolesApiEvents Events { get; }

        /// <summary>
        /// Get roles
        /// </summary>
        /// <remarks>
        /// Returns all roles for the user 
        /// </remarks>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="pageSize">Number of results per page. Defaults to 10 if parameter not sent. (optional)</param>
        /// <param name="startingAfter">The ID of the role to start after. (optional)</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task&lt;ApiResponse&lt;GetUserRolesResponse&gt;&gt;</returns>
        Task<ApiResponse<GetUserRolesResponse>> GetUserRolesAsync(Option<int?> pageSize = default, Option<string?> startingAfter = default, System.Threading.CancellationToken cancellationToken = default);

        /// <summary>
        /// Get roles
        /// </summary>
        /// <remarks>
        /// Returns all roles for the user 
        /// </remarks>
        /// <param name="pageSize">Number of results per page. Defaults to 10 if parameter not sent. (optional)</param>
        /// <param name="startingAfter">The ID of the role to start after. (optional)</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task&lt;ApiResponse&gt;GetUserRolesResponse&gt;?&gt;</returns>
        Task<ApiResponse<GetUserRolesResponse>?> GetUserRolesOrDefaultAsync(Option<int?> pageSize = default, Option<string?> startingAfter = default, System.Threading.CancellationToken cancellationToken = default);
    }

    /// <summary>
    /// Represents a collection of functions to interact with the API endpoints
    /// This class is registered as transient.
    /// </summary>
    public class RolesApiEvents
    {
        /// <summary>
        /// The event raised after the server response
        /// </summary>
        public event EventHandler<ApiResponseEventArgs<GetUserRolesResponse>>? OnGetUserRoles;

        /// <summary>
        /// The event raised after an error querying the server
        /// </summary>
        public event EventHandler<ExceptionEventArgs>? OnErrorGetUserRoles;

        internal void ExecuteOnGetUserRoles(ApiResponse<GetUserRolesResponse> apiResponse)
        {
            OnGetUserRoles?.Invoke(this, new ApiResponseEventArgs<GetUserRolesResponse>(apiResponse));
        }

        internal void ExecuteOnErrorGetUserRoles(Exception exception)
        {
            OnErrorGetUserRoles?.Invoke(this, new ExceptionEventArgs(exception));
        }
    }

    /// <summary>
    /// Represents a collection of functions to interact with the API endpoints
    /// </summary>
    public sealed partial class RolesApi : KiotaAccountsBase, IRolesApi
    {
        private JsonSerializerOptions _jsonSerializerOptions;
        private readonly HttpClient _httpClient;
        private readonly TokenProvider<BearerToken> _bearerTokenProvider;

        /// <summary>
        /// The logger
        /// </summary>
        public ILogger<RolesApi> Logger { get; }

        /// <summary>
        /// The HttpClient
        /// </summary>
        public HttpClient HttpClient => _httpClient;

        /// <summary>
        /// The class containing the events
        /// </summary>
        public RolesApiEvents Events { get; }

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
        /// Initializes a new instance of the <see cref="RolesApi"/> class.
        /// </summary>
        /// <returns></returns>
        public RolesApi(ILogger<RolesApi> logger, HttpClient httpClient, JsonSerializerOptionsProvider jsonSerializerOptionsProvider, RolesApiEvents rolesApiEvents,
            TokenProvider<BearerToken> bearerTokenProvider)
        {
            _jsonSerializerOptions = jsonSerializerOptionsProvider.Options;
            Logger = logger;
            _httpClient = httpClient;
            Events = rolesApiEvents;
            _bearerTokenProvider = bearerTokenProvider;
        }

        partial void FormatGetUserRoles(ref Option<int?> pageSize, ref Option<string?> startingAfter);

        /// <summary>
        /// Processes the server response
        /// </summary>
        /// <param name="apiResponseLocalVar"></param>
        /// <param name="pageSize"></param>
        /// <param name="startingAfter"></param>
        private void AfterGetUserRolesDefaultImplementation(ApiResponse<GetUserRolesResponse> apiResponseLocalVar, Option<int?> pageSize, Option<string?> startingAfter)
        {
            bool suppressDefaultLog = false;
            AfterGetUserRoles(ref suppressDefaultLog, apiResponseLocalVar, pageSize, startingAfter);
            if (!suppressDefaultLog)
                Logger.LogInformation("{0,-9} | {1} | {3}", (apiResponseLocalVar.DownloadedAt - apiResponseLocalVar.RequestedAt).TotalSeconds, apiResponseLocalVar.StatusCode, apiResponseLocalVar.Path);
        }

        /// <summary>
        /// Processes the server response
        /// </summary>
        /// <param name="suppressDefaultLog"></param>
        /// <param name="apiResponseLocalVar"></param>
        /// <param name="pageSize"></param>
        /// <param name="startingAfter"></param>
        partial void AfterGetUserRoles(ref bool suppressDefaultLog, ApiResponse<GetUserRolesResponse> apiResponseLocalVar, Option<int?> pageSize, Option<string?> startingAfter);

        /// <summary>
        /// Logs exceptions that occur while retrieving the server response
        /// </summary>
        /// <param name="exception"></param>
        /// <param name="pathFormat"></param>
        /// <param name="path"></param>
        /// <param name="pageSize"></param>
        /// <param name="startingAfter"></param>
        private void OnErrorGetUserRolesDefaultImplementation(Exception exception, string pathFormat, string path, Option<int?> pageSize, Option<string?> startingAfter)
        {
            bool suppressDefaultLog = false;
            OnErrorGetUserRoles(ref suppressDefaultLog, exception, pathFormat, path, pageSize, startingAfter);
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
        /// <param name="pageSize"></param>
        /// <param name="startingAfter"></param>
        partial void OnErrorGetUserRoles(ref bool suppressDefaultLog, Exception exception, string pathFormat, string path, Option<int?> pageSize, Option<string?> startingAfter);

        /// <summary>
        /// Get roles Returns all roles for the user 
        /// </summary>
        /// <param name="pageSize">Number of results per page. Defaults to 10 if parameter not sent. (optional)</param>
        /// <param name="startingAfter">The ID of the role to start after. (optional)</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns><see cref="Task"/>&lt;<see cref="ApiResponse{T}"/>&gt; where T : <see cref="GetUserRolesResponse"/></returns>
        public async Task<ApiResponse<GetUserRolesResponse>?> GetUserRolesOrDefaultAsync(Option<int?> pageSize = default, Option<string?> startingAfter = default, System.Threading.CancellationToken cancellationToken = default)
        {
            try
            {
                return await GetUserRolesAsync(pageSize, startingAfter, cancellationToken).ConfigureAwait(false);
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// Get roles Returns all roles for the user 
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="pageSize">Number of results per page. Defaults to 10 if parameter not sent. (optional)</param>
        /// <param name="startingAfter">The ID of the role to start after. (optional)</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns><see cref="Task"/>&lt;<see cref="ApiResponse{T}"/>&gt; where T : <see cref="GetUserRolesResponse"/></returns>
        public async Task<ApiResponse<GetUserRolesResponse>> GetUserRolesAsync(Option<int?> pageSize = default, Option<string?> startingAfter = default, System.Threading.CancellationToken cancellationToken = default)
        {
            DateTime requestedAtLocalVar = DateTime.UtcNow;

            try
            {
                FormatGetUserRoles(ref pageSize, ref startingAfter);

                // Create a fresh Kiota client with current token
                var kiotaClient = await CreateKiotaClientWithTokenAsync(cancellationToken).ConfigureAwait(false);

                // Call Kiota client
                var kiotaResponse = await kiotaClient.Account_api.V1.Roles.GetAsync(
                    config =>
                    {
                        if (pageSize.IsSet && pageSize.Value.HasValue)
                            config.QueryParameters.PageSize = pageSize.Value;
                        if (startingAfter.IsSet && !string.IsNullOrEmpty(startingAfter.Value))
                            config.QueryParameters.StartingAfter = startingAfter.Value;
                    },
                    cancellationToken).ConfigureAwait(false);

                // Map Kiota response to the expected model
                var mappedResponse = KiotaMapper.Map<GetUserRolesResponse>(kiotaResponse);

                // Create API response
                var apiResponseLocalVar = new ApiResponse<GetUserRolesResponse>(
                    "/account_api/v1/roles",
                    mappedResponse,
                    HttpStatusCode.OK,
                    requestedAtLocalVar);

                AfterGetUserRolesDefaultImplementation(apiResponseLocalVar, pageSize, startingAfter);
                Events.ExecuteOnGetUserRoles(apiResponseLocalVar);

                return apiResponseLocalVar;
            }
            catch (Exception e)
            {
                OnErrorGetUserRolesDefaultImplementation(e, "/account_api/v1/roles", "/account_api/v1/roles", pageSize, startingAfter);
                Events.ExecuteOnErrorGetUserRoles(e);
                throw;
            }
        }
    }
}
