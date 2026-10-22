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
    public interface IPermissionsApi : IApi
    {
        /// <summary>
        /// The class containing the events
        /// </summary>
        PermissionsApiEvents Events { get; }

        /// <summary>
        /// Get permissions
        /// </summary>
        /// <remarks>
        /// Returns all the permissions the user has 
        /// </remarks>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="pageSize">Number of results per page. Defaults to 10 if parameter not sent. (optional)</param>
        /// <param name="startingAfter">The ID of the permission to start after. (optional)</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task&lt;ApiResponse&lt;GetUserPermissionsResponse&gt;&gt;</returns>
        Task<ApiResponse<GetUserPermissionsResponse>> GetUserPermissionsAsync(Option<int?> pageSize = default, Option<string?> startingAfter = default, System.Threading.CancellationToken cancellationToken = default);

        /// <summary>
        /// Get permissions
        /// </summary>
        /// <remarks>
        /// Returns all the permissions the user has 
        /// </remarks>
        /// <param name="pageSize">Number of results per page. Defaults to 10 if parameter not sent. (optional)</param>
        /// <param name="startingAfter">The ID of the permission to start after. (optional)</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task&lt;ApiResponse&gt;GetUserPermissionsResponse&gt;?&gt;</returns>
        Task<ApiResponse<GetUserPermissionsResponse>?> GetUserPermissionsOrDefaultAsync(Option<int?> pageSize = default, Option<string?> startingAfter = default, System.Threading.CancellationToken cancellationToken = default);
    }

    /// <summary>
    /// Represents a collection of functions to interact with the API endpoints
    /// This class is registered as transient.
    /// </summary>
    public class PermissionsApiEvents
    {
        /// <summary>
        /// The event raised after the server response
        /// </summary>
        public event EventHandler<ApiResponseEventArgs<GetUserPermissionsResponse>>? OnGetUserPermissions;

        /// <summary>
        /// The event raised after an error querying the server
        /// </summary>
        public event EventHandler<ExceptionEventArgs>? OnErrorGetUserPermissions;

        internal void ExecuteOnGetUserPermissions(ApiResponse<GetUserPermissionsResponse> apiResponse)
        {
            OnGetUserPermissions?.Invoke(this, new ApiResponseEventArgs<GetUserPermissionsResponse>(apiResponse));
        }

        internal void ExecuteOnErrorGetUserPermissions(Exception exception)
        {
            OnErrorGetUserPermissions?.Invoke(this, new ExceptionEventArgs(exception));
        }
    }

    /// <summary>
    /// Represents a collection of functions to interact with the API endpoints
    /// </summary>
    public sealed partial class PermissionsApi : KiotaAccountsBase, IPermissionsApi
    {
        private JsonSerializerOptions _jsonSerializerOptions;
        private readonly HttpClient _httpClient;
        private readonly TokenProvider<BearerToken> _bearerTokenProvider;

        /// <summary>
        /// The logger
        /// </summary>
        public ILogger<PermissionsApi> Logger { get; }

        /// <summary>
        /// The HttpClient
        /// </summary>
        public HttpClient HttpClient => _httpClient;

        /// <summary>
        /// The class containing the events
        /// </summary>
        public PermissionsApiEvents Events { get; }

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
        /// Initializes a new instance of the <see cref="PermissionsApi"/> class.
        /// </summary>
        /// <returns></returns>
        public PermissionsApi(ILogger<PermissionsApi> logger, HttpClient httpClient, JsonSerializerOptionsProvider jsonSerializerOptionsProvider, PermissionsApiEvents permissionsApiEvents,
            TokenProvider<BearerToken> bearerTokenProvider)
        {
            _jsonSerializerOptions = jsonSerializerOptionsProvider.Options;
            Logger = logger;
            _httpClient = httpClient;
            Events = permissionsApiEvents;
            _bearerTokenProvider = bearerTokenProvider;
        }

        partial void FormatGetUserPermissions(ref Option<int?> pageSize, ref Option<string?> startingAfter);

        /// <summary>
        /// Processes the server response
        /// </summary>
        /// <param name="apiResponseLocalVar"></param>
        /// <param name="pageSize"></param>
        /// <param name="startingAfter"></param>
        private void AfterGetUserPermissionsDefaultImplementation(ApiResponse<GetUserPermissionsResponse> apiResponseLocalVar, Option<int?> pageSize, Option<string?> startingAfter)
        {
            bool suppressDefaultLog = false;
            AfterGetUserPermissions(ref suppressDefaultLog, apiResponseLocalVar, pageSize, startingAfter);
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
        partial void AfterGetUserPermissions(ref bool suppressDefaultLog, ApiResponse<GetUserPermissionsResponse> apiResponseLocalVar, Option<int?> pageSize, Option<string?> startingAfter);

        /// <summary>
        /// Logs exceptions that occur while retrieving the server response
        /// </summary>
        /// <param name="exception"></param>
        /// <param name="pathFormat"></param>
        /// <param name="path"></param>
        /// <param name="pageSize"></param>
        /// <param name="startingAfter"></param>
        private void OnErrorGetUserPermissionsDefaultImplementation(Exception exception, string pathFormat, string path, Option<int?> pageSize, Option<string?> startingAfter)
        {
            bool suppressDefaultLog = false;
            OnErrorGetUserPermissions(ref suppressDefaultLog, exception, pathFormat, path, pageSize, startingAfter);
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
        partial void OnErrorGetUserPermissions(ref bool suppressDefaultLog, Exception exception, string pathFormat, string path, Option<int?> pageSize, Option<string?> startingAfter);

        /// <summary>
        /// Get permissions Returns all the permissions the user has 
        /// </summary>
        /// <param name="pageSize">Number of results per page. Defaults to 10 if parameter not sent. (optional)</param>
        /// <param name="startingAfter">The ID of the permission to start after. (optional)</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns><see cref="Task"/>&lt;<see cref="ApiResponse{T}"/>&gt; where T : <see cref="GetUserPermissionsResponse"/></returns>
        public async Task<ApiResponse<GetUserPermissionsResponse>?> GetUserPermissionsOrDefaultAsync(Option<int?> pageSize = default, Option<string?> startingAfter = default, System.Threading.CancellationToken cancellationToken = default)
        {
            try
            {
                return await GetUserPermissionsAsync(pageSize, startingAfter, cancellationToken).ConfigureAwait(false);
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// Get permissions Returns all the permissions the user has 
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="pageSize">Number of results per page. Defaults to 10 if parameter not sent. (optional)</param>
        /// <param name="startingAfter">The ID of the permission to start after. (optional)</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns><see cref="Task"/>&lt;<see cref="ApiResponse{T}"/>&gt; where T : <see cref="GetUserPermissionsResponse"/></returns>
        public async Task<ApiResponse<GetUserPermissionsResponse>> GetUserPermissionsAsync(Option<int?> pageSize = default, Option<string?> startingAfter = default, System.Threading.CancellationToken cancellationToken = default)
        {
            DateTime requestedAtLocalVar = DateTime.UtcNow;

            try
            {
                FormatGetUserPermissions(ref pageSize, ref startingAfter);

                // Create a fresh Kiota client with current token
                var kiotaClient = await CreateKiotaClientWithTokenAsync(cancellationToken).ConfigureAwait(false);

                // Call Kiota client
                var kiotaResponse = await kiotaClient.Account_api.V1.Permissions.GetAsync(
                    config =>
                    {
                        if (pageSize.IsSet && pageSize.Value.HasValue)
                            config.QueryParameters.PageSize = pageSize.Value;
                        if (startingAfter.IsSet && !string.IsNullOrEmpty(startingAfter.Value))
                            config.QueryParameters.StartingAfter = startingAfter.Value;
                    },
                    cancellationToken).ConfigureAwait(false);

                // Map Kiota response to the expected model
                var mappedResponse = KiotaMapper.Map<GetUserPermissionsResponse>(kiotaResponse);

                // Create API response
                var apiResponseLocalVar = new ApiResponse<GetUserPermissionsResponse>(
                    "/account_api/v1/permissions",
                    mappedResponse,
                    HttpStatusCode.OK,
                    requestedAtLocalVar);

                AfterGetUserPermissionsDefaultImplementation(apiResponseLocalVar, pageSize, startingAfter);
                Events.ExecuteOnGetUserPermissions(apiResponseLocalVar);

                return apiResponseLocalVar;
            }
            catch (Exception e)
            {
                OnErrorGetUserPermissionsDefaultImplementation(e, "/account_api/v1/permissions", "/account_api/v1/permissions", pageSize, startingAfter);
                Events.ExecuteOnErrorGetUserPermissions(e);
                throw;
            }
        }
    }
}
