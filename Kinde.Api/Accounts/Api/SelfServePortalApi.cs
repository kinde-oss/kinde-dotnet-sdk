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
    public sealed partial class SelfServePortalApi : ISelfServePortalApi
    {
        private JsonSerializerOptions _jsonSerializerOptions;

        /// <summary>
        /// The logger
        /// </summary>
        public ILogger<SelfServePortalApi> Logger { get; }

        /// <summary>
        /// The HttpClient
        /// </summary>
        public HttpClient HttpClient { get; }

        /// <summary>
        /// The class containing the events
        /// </summary>
        public SelfServePortalApiEvents Events { get; }

        /// <summary>
        /// A token provider of type <see cref="BearerToken"/>
        /// </summary>
        public TokenProvider<BearerToken> BearerTokenProvider { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="SelfServePortalApi"/> class.
        /// </summary>
        /// <returns></returns>
        public SelfServePortalApi(ILogger<SelfServePortalApi> logger, HttpClient httpClient, JsonSerializerOptionsProvider jsonSerializerOptionsProvider, SelfServePortalApiEvents selfServePortalApiEvents,
            TokenProvider<BearerToken> bearerTokenProvider)
        {
            _jsonSerializerOptions = jsonSerializerOptionsProvider.Options;
            Logger = logger;
            HttpClient = httpClient;
            Events = selfServePortalApiEvents;
            BearerTokenProvider = bearerTokenProvider;
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
            UriBuilder uriBuilderLocalVar = new UriBuilder();

            try
            {
                FormatGetPortalLink(ref subnav, ref returnUrl);

                using (HttpRequestMessage httpRequestMessageLocalVar = new HttpRequestMessage())
                {
                    uriBuilderLocalVar.Host = HttpClient.BaseAddress!.Host;
                    uriBuilderLocalVar.Port = HttpClient.BaseAddress.Port;
                    uriBuilderLocalVar.Scheme = HttpClient.BaseAddress.Scheme;
                    uriBuilderLocalVar.Path = ClientUtils.CONTEXT_PATH + "/account_api/v1/portal_link";

                    System.Collections.Specialized.NameValueCollection parseQueryStringLocalVar = System.Web.HttpUtility.ParseQueryString(string.Empty);

                    if (subnav.IsSet)
                        parseQueryStringLocalVar["subnav"] = subnav.Value?.ToString();

                    if (returnUrl.IsSet)
                        parseQueryStringLocalVar["return_url"] = returnUrl.Value?.ToString();

                    uriBuilderLocalVar.Query = parseQueryStringLocalVar.ToString();

                    List<TokenBase> tokenBaseLocalVars = new List<TokenBase>();

                    httpRequestMessageLocalVar.RequestUri = uriBuilderLocalVar.Uri;

                    BearerToken bearerTokenLocalVar = (BearerToken) await BearerTokenProvider.GetAsync(cancellationToken).ConfigureAwait(false);

                    tokenBaseLocalVars.Add(bearerTokenLocalVar);

                    bearerTokenLocalVar.UseInHeader(httpRequestMessageLocalVar, "");

                    string[] acceptLocalVars = new string[] {
                        "application/json"
                    };

                    string? acceptLocalVar = ClientUtils.SelectHeaderAccept(acceptLocalVars);

                    if (acceptLocalVar != null)
                        httpRequestMessageLocalVar.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue(acceptLocalVar));
                    httpRequestMessageLocalVar.Method = new HttpMethod("GET");

                    DateTime requestedAtLocalVar = DateTime.UtcNow;

                    using (HttpResponseMessage httpResponseMessageLocalVar = await HttpClient.SendAsync(httpRequestMessageLocalVar, cancellationToken).ConfigureAwait(false))
                    {
                        string responseContentLocalVar = await httpResponseMessageLocalVar.Content.ReadAsStringAsync().ConfigureAwait(false);

                        ApiResponse<PortalLink> apiResponseLocalVar = new ApiResponse<PortalLink>(httpRequestMessageLocalVar, httpResponseMessageLocalVar, responseContentLocalVar, "/account_api/v1/portal_link", requestedAtLocalVar, _jsonSerializerOptions);

                        AfterGetPortalLinkDefaultImplementation(apiResponseLocalVar, subnav, returnUrl);

                        Events.ExecuteOnGetPortalLink(apiResponseLocalVar);

                        if (apiResponseLocalVar.StatusCode == (HttpStatusCode) 429)
                            foreach(TokenBase tokenBaseLocalVar in tokenBaseLocalVars)
                                tokenBaseLocalVar.BeginRateLimit();

                        return apiResponseLocalVar;
                    }
                }
            }
            catch(Exception e)
            {
                OnErrorGetPortalLinkDefaultImplementation(e, "/account_api/v1/portal_link", uriBuilderLocalVar.Path, subnav, returnUrl);
                Events.ExecuteOnErrorGetPortalLink(e);
                throw;
            }
        }
    }
}
