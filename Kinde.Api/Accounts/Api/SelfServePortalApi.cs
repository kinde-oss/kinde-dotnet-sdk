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
    public interface ISelfServePortalApi : IApi
    {
        /// <summary>
        /// The class containing the events
        /// </summary>
        SelfServePortalApiEvents Events { get; }

        /// <summary>
        /// Get self-serve portal link
        /// </summary>
        /// <remarks>
        /// Returns a link to the self-serve portal for the authenticated user. The user can use this link to manage their account, update their profile, and view their entitlements. 
        /// </remarks>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="subnav">The area of the portal you want the user to land on (optional)</param>
        /// <param name="returnUrl">The URL to redirect the user to after they have completed their actions in the portal. (optional)</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task&lt;ApiResponse&lt;PortalLink&gt;&gt;</returns>
        Task<ApiResponse<PortalLink>> GetPortalLinkAsync(Option<string?> subnav = default, Option<string?> returnUrl = default, System.Threading.CancellationToken cancellationToken = default);

        /// <summary>
        /// Get self-serve portal link
        /// </summary>
        /// <remarks>
        /// Returns a link to the self-serve portal for the authenticated user. The user can use this link to manage their account, update their profile, and view their entitlements. 
        /// </remarks>
        /// <param name="subnav">The area of the portal you want the user to land on (optional)</param>
        /// <param name="returnUrl">The URL to redirect the user to after they have completed their actions in the portal. (optional)</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task&lt;ApiResponse&gt;PortalLink&gt;?&gt;</returns>
        Task<ApiResponse<PortalLink>?> GetPortalLinkOrDefaultAsync(Option<string?> subnav = default, Option<string?> returnUrl = default, System.Threading.CancellationToken cancellationToken = default);
    }

    /// <summary>
    /// Represents a collection of functions to interact with the API endpoints
    /// This class is registered as transient.
    /// </summary>
    public class SelfServePortalApiEvents
    {
        /// <summary>
        /// The event raised after the server response
        /// </summary>
        public event EventHandler<ApiResponseEventArgs<PortalLink>>? OnGetPortalLink;

        /// <summary>
        /// The event raised after an error querying the server
        /// </summary>
        public event EventHandler<ExceptionEventArgs>? OnErrorGetPortalLink;

        internal void ExecuteOnGetPortalLink(ApiResponse<PortalLink> apiResponse)
        {
            OnGetPortalLink?.Invoke(this, new ApiResponseEventArgs<PortalLink>(apiResponse));
        }

        internal void ExecuteOnErrorGetPortalLink(Exception exception)
        {
            OnErrorGetPortalLink?.Invoke(this, new ExceptionEventArgs(exception));
        }
    }

    /// <summary>
    /// Represents a collection of functions to interact with the API endpoints
    /// </summary>
    public sealed partial class SelfServePortalApi : KiotaAccountsBase, ISelfServePortalApi
    {
        private JsonSerializerOptions _jsonSerializerOptions;
        private readonly HttpClient _httpClient;
        private readonly TokenProvider<BearerToken> _bearerTokenProvider;

        /// <summary>
        /// The logger
        /// </summary>
        public ILogger<SelfServePortalApi> Logger { get; }

        /// <summary>
        /// The HttpClient
        /// </summary>
        public HttpClient HttpClient => _httpClient;

        /// <summary>
        /// The class containing the events
        /// </summary>
        public SelfServePortalApiEvents Events { get; }

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
        /// Initializes a new instance of the <see cref="SelfServePortalApi"/> class.
        /// </summary>
        /// <returns></returns>
        public SelfServePortalApi(ILogger<SelfServePortalApi> logger, HttpClient httpClient, JsonSerializerOptionsProvider jsonSerializerOptionsProvider, SelfServePortalApiEvents selfServePortalApiEvents,
            TokenProvider<BearerToken> bearerTokenProvider)
        {
            _jsonSerializerOptions = jsonSerializerOptionsProvider.Options;
            Logger = logger;
            _httpClient = httpClient;
            Events = selfServePortalApiEvents;
            _bearerTokenProvider = bearerTokenProvider;
        }

        partial void FormatGetPortalLink(ref Option<string?> subnav, ref Option<string?> returnUrl);

        /// <summary>
        /// Processes the server response
        /// </summary>
        /// <param name="apiResponseLocalVar"></param>
        /// <param name="subnav"></param>
        /// <param name="returnUrl"></param>
        private void AfterGetPortalLinkDefaultImplementation(ApiResponse<PortalLink> apiResponseLocalVar, Option<string?> subnav, Option<string?> returnUrl)
        {
            bool suppressDefaultLog = false;
            AfterGetPortalLink(ref suppressDefaultLog, apiResponseLocalVar, subnav, returnUrl);
            if (!suppressDefaultLog)
                Logger.LogInformation("{0,-9} | {1} | {3}", (apiResponseLocalVar.DownloadedAt - apiResponseLocalVar.RequestedAt).TotalSeconds, apiResponseLocalVar.StatusCode, apiResponseLocalVar.Path);
        }

        /// <summary>
        /// Processes the server response
        /// </summary>
        /// <param name="suppressDefaultLog"></param>
        /// <param name="apiResponseLocalVar"></param>
        /// <param name="subnav"></param>
        /// <param name="returnUrl"></param>
        partial void AfterGetPortalLink(ref bool suppressDefaultLog, ApiResponse<PortalLink> apiResponseLocalVar, Option<string?> subnav, Option<string?> returnUrl);

        /// <summary>
        /// Logs exceptions that occur while retrieving the server response
        /// </summary>
        /// <param name="exception"></param>
        /// <param name="pathFormat"></param>
        /// <param name="path"></param>
        /// <param name="subnav"></param>
        /// <param name="returnUrl"></param>
        private void OnErrorGetPortalLinkDefaultImplementation(Exception exception, string pathFormat, string path, Option<string?> subnav, Option<string?> returnUrl)
        {
            bool suppressDefaultLog = false;
            OnErrorGetPortalLink(ref suppressDefaultLog, exception, pathFormat, path, subnav, returnUrl);
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
        /// <param name="subnav"></param>
        /// <param name="returnUrl"></param>
        partial void OnErrorGetPortalLink(ref bool suppressDefaultLog, Exception exception, string pathFormat, string path, Option<string?> subnav, Option<string?> returnUrl);

        /// <summary>
        /// Get self-serve portal link Returns a link to the self-serve portal for the authenticated user. The user can use this link to manage their account, update their profile, and view their entitlements. 
        /// </summary>
        /// <param name="subnav">The area of the portal you want the user to land on (optional)</param>
        /// <param name="returnUrl">The URL to redirect the user to after they have completed their actions in the portal. (optional)</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns><see cref="Task"/>&lt;<see cref="ApiResponse{T}"/>&gt; where T : <see cref="PortalLink"/></returns>
        public async Task<ApiResponse<PortalLink>?> GetPortalLinkOrDefaultAsync(Option<string?> subnav = default, Option<string?> returnUrl = default, System.Threading.CancellationToken cancellationToken = default)
        {
            try
            {
                return await GetPortalLinkAsync(subnav, returnUrl, cancellationToken).ConfigureAwait(false);
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// Get self-serve portal link Returns a link to the self-serve portal for the authenticated user. The user can use this link to manage their account, update their profile, and view their entitlements. 
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="subnav">The area of the portal you want the user to land on (optional)</param>
        /// <param name="returnUrl">The URL to redirect the user to after they have completed their actions in the portal. (optional)</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns><see cref="Task"/>&lt;<see cref="ApiResponse{T}"/>&gt; where T : <see cref="PortalLink"/></returns>
        public async Task<ApiResponse<PortalLink>> GetPortalLinkAsync(Option<string?> subnav = default, Option<string?> returnUrl = default, System.Threading.CancellationToken cancellationToken = default)
        {
            DateTime requestedAtLocalVar = DateTime.UtcNow;

            try
            {
                FormatGetPortalLink(ref subnav, ref returnUrl);

                // Create a fresh Kiota client with current token
                var kiotaClient = await CreateKiotaClientWithTokenAsync(cancellationToken).ConfigureAwait(false);

                // Call Kiota client
                var kiotaResponse = await kiotaClient.Account_api.V1.Portal_link.GetAsync(
                    config =>
                    {
                        if (subnav.IsSet && !string.IsNullOrEmpty(subnav.Value))
                        {
                            if (Enum.TryParse<global::Kinde.Api.Kiota.Accounts.Account_api.V1.Portal_link.GetSubnavQueryParameterType>(
                                subnav.Value.Replace("_", ""), true, out var subnavType))
                            {
                                config.QueryParameters.Subnav = subnavType;
                            }
                        }
                        if (returnUrl.IsSet && !string.IsNullOrEmpty(returnUrl.Value))
                            config.QueryParameters.ReturnUrl = returnUrl.Value;
                    },
                    cancellationToken).ConfigureAwait(false);

                // Map Kiota response to the expected model
                var mappedResponse = KiotaMapper.Map<PortalLink>(kiotaResponse);

                // Create API response
                var apiResponseLocalVar = new ApiResponse<PortalLink>(
                    "/account_api/v1/portal_link",
                    mappedResponse,
                    HttpStatusCode.OK,
                    requestedAtLocalVar);

                        AfterGetPortalLinkDefaultImplementation(apiResponseLocalVar, subnav, returnUrl);
                        Events.ExecuteOnGetPortalLink(apiResponseLocalVar);

                        return apiResponseLocalVar;
            }
            catch (Exception e)
            {
                OnErrorGetPortalLinkDefaultImplementation(e, "/account_api/v1/portal_link", "/account_api/v1/portal_link", subnav, returnUrl);
                Events.ExecuteOnErrorGetPortalLink(e);
                throw;
            }
        }
    }
}
