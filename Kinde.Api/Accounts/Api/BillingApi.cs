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
    public interface IBillingApi : IApi
    {
        /// <summary>
        /// The class containing the events
        /// </summary>
        BillingApiEvents Events { get; }

        /// <summary>
        /// Get entitlement
        /// </summary>
        /// <remarks>
        /// Returns a single entitlement by the feature key 
        /// </remarks>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="key">The key of the feature</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task&lt;ApiResponse&lt;GetEntitlementResponse&gt;&gt;</returns>
        Task<ApiResponse<GetEntitlementResponse>> GetEntitlementAsync(string key, System.Threading.CancellationToken cancellationToken = default);

        /// <summary>
        /// Get entitlement
        /// </summary>
        /// <remarks>
        /// Returns a single entitlement by the feature key 
        /// </remarks>
        /// <param name="key">The key of the feature</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task&lt;ApiResponse&gt;GetEntitlementResponse&gt;?&gt;</returns>
        Task<ApiResponse<GetEntitlementResponse>?> GetEntitlementOrDefaultAsync(string key, System.Threading.CancellationToken cancellationToken = default);

        /// <summary>
        /// Get entitlements
        /// </summary>
        /// <remarks>
        /// Returns all the entitlements the user currently has access to 
        /// </remarks>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="pageSize">Number of results per page. Defaults to 10 if parameter not sent. (optional)</param>
        /// <param name="startingAfter">The ID of the entitlement to start after. (optional)</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task&lt;ApiResponse&lt;GetEntitlementsResponse&gt;&gt;</returns>
        Task<ApiResponse<GetEntitlementsResponse>> GetEntitlementsAsync(Option<int?> pageSize = default, Option<string?> startingAfter = default, System.Threading.CancellationToken cancellationToken = default);

        /// <summary>
        /// Get entitlements
        /// </summary>
        /// <remarks>
        /// Returns all the entitlements the user currently has access to 
        /// </remarks>
        /// <param name="pageSize">Number of results per page. Defaults to 10 if parameter not sent. (optional)</param>
        /// <param name="startingAfter">The ID of the entitlement to start after. (optional)</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task&lt;ApiResponse&gt;GetEntitlementsResponse&gt;?&gt;</returns>
        Task<ApiResponse<GetEntitlementsResponse>?> GetEntitlementsOrDefaultAsync(Option<int?> pageSize = default, Option<string?> startingAfter = default, System.Threading.CancellationToken cancellationToken = default);
    }

    /// <summary>
    /// Represents a collection of functions to interact with the API endpoints
    /// This class is registered as transient.
    /// </summary>
    public class BillingApiEvents
    {
        /// <summary>
        /// The event raised after the server response
        /// </summary>
        public event EventHandler<ApiResponseEventArgs<GetEntitlementResponse>>? OnGetEntitlement;

        /// <summary>
        /// The event raised after an error querying the server
        /// </summary>
        public event EventHandler<ExceptionEventArgs>? OnErrorGetEntitlement;

        internal void ExecuteOnGetEntitlement(ApiResponse<GetEntitlementResponse> apiResponse)
        {
            OnGetEntitlement?.Invoke(this, new ApiResponseEventArgs<GetEntitlementResponse>(apiResponse));
        }

        internal void ExecuteOnErrorGetEntitlement(Exception exception)
        {
            OnErrorGetEntitlement?.Invoke(this, new ExceptionEventArgs(exception));
        }

        /// <summary>
        /// The event raised after the server response
        /// </summary>
        public event EventHandler<ApiResponseEventArgs<GetEntitlementsResponse>>? OnGetEntitlements;

        /// <summary>
        /// The event raised after an error querying the server
        /// </summary>
        public event EventHandler<ExceptionEventArgs>? OnErrorGetEntitlements;

        internal void ExecuteOnGetEntitlements(ApiResponse<GetEntitlementsResponse> apiResponse)
        {
            OnGetEntitlements?.Invoke(this, new ApiResponseEventArgs<GetEntitlementsResponse>(apiResponse));
        }

        internal void ExecuteOnErrorGetEntitlements(Exception exception)
        {
            OnErrorGetEntitlements?.Invoke(this, new ExceptionEventArgs(exception));
        }
    }

    /// <summary>
    /// Represents a collection of functions to interact with the API endpoints
    /// </summary>
    public sealed partial class BillingApi : IBillingApi
    {
        private JsonSerializerOptions _jsonSerializerOptions;

        /// <summary>
        /// The logger
        /// </summary>
        public ILogger<BillingApi> Logger { get; }

        /// <summary>
        /// The HttpClient
        /// </summary>
        public HttpClient HttpClient { get; }

        /// <summary>
        /// The class containing the events
        /// </summary>
        public BillingApiEvents Events { get; }

        /// <summary>
        /// A token provider of type <see cref="BearerToken"/>
        /// </summary>
        public TokenProvider<BearerToken> BearerTokenProvider { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="BillingApi"/> class.
        /// </summary>
        /// <returns></returns>
        public BillingApi(ILogger<BillingApi> logger, HttpClient httpClient, JsonSerializerOptionsProvider jsonSerializerOptionsProvider, BillingApiEvents billingApiEvents,
            TokenProvider<BearerToken> bearerTokenProvider)
        {
            _jsonSerializerOptions = jsonSerializerOptionsProvider.Options;
            Logger = logger;
            HttpClient = httpClient;
            Events = billingApiEvents;
            BearerTokenProvider = bearerTokenProvider;
        }

        partial void FormatGetEntitlement(ref string key);

        /// <summary>
        /// Validates the request parameters
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        private void ValidateGetEntitlement(string key)
        {
            if (key == null)
                throw new ArgumentNullException(nameof(key));
        }

        /// <summary>
        /// Processes the server response
        /// </summary>
        /// <param name="apiResponseLocalVar"></param>
        /// <param name="key"></param>
        private void AfterGetEntitlementDefaultImplementation(ApiResponse<GetEntitlementResponse> apiResponseLocalVar, string key)
        {
            bool suppressDefaultLog = false;
            AfterGetEntitlement(ref suppressDefaultLog, apiResponseLocalVar, key);
            if (!suppressDefaultLog)
                Logger.LogInformation("{0,-9} | {1} | {3}", (apiResponseLocalVar.DownloadedAt - apiResponseLocalVar.RequestedAt).TotalSeconds, apiResponseLocalVar.StatusCode, apiResponseLocalVar.Path);
        }

        /// <summary>
        /// Processes the server response
        /// </summary>
        /// <param name="suppressDefaultLog"></param>
        /// <param name="apiResponseLocalVar"></param>
        /// <param name="key"></param>
        partial void AfterGetEntitlement(ref bool suppressDefaultLog, ApiResponse<GetEntitlementResponse> apiResponseLocalVar, string key);

        /// <summary>
        /// Logs exceptions that occur while retrieving the server response
        /// </summary>
        /// <param name="exception"></param>
        /// <param name="pathFormat"></param>
        /// <param name="path"></param>
        /// <param name="key"></param>
        private void OnErrorGetEntitlementDefaultImplementation(Exception exception, string pathFormat, string path, string key)
        {
            bool suppressDefaultLog = false;
            OnErrorGetEntitlement(ref suppressDefaultLog, exception, pathFormat, path, key);
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
        /// <param name="key"></param>
        partial void OnErrorGetEntitlement(ref bool suppressDefaultLog, Exception exception, string pathFormat, string path, string key);

        /// <summary>
        /// Get entitlement Returns a single entitlement by the feature key 
        /// </summary>
        /// <param name="key">The key of the feature</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns><see cref="Task"/>&lt;<see cref="ApiResponse{T}"/>&gt; where T : <see cref="GetEntitlementResponse"/></returns>
        public async Task<ApiResponse<GetEntitlementResponse>?> GetEntitlementOrDefaultAsync(string key, System.Threading.CancellationToken cancellationToken = default)
        {
            try
            {
                return await GetEntitlementAsync(key, cancellationToken).ConfigureAwait(false);
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// Get entitlement Returns a single entitlement by the feature key 
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="key">The key of the feature</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns><see cref="Task"/>&lt;<see cref="ApiResponse{T}"/>&gt; where T : <see cref="GetEntitlementResponse"/></returns>
        public async Task<ApiResponse<GetEntitlementResponse>> GetEntitlementAsync(string key, System.Threading.CancellationToken cancellationToken = default)
        {
            UriBuilder uriBuilderLocalVar = new UriBuilder();

            try
            {
                ValidateGetEntitlement(key);

                FormatGetEntitlement(ref key);

                using (HttpRequestMessage httpRequestMessageLocalVar = new HttpRequestMessage())
                {
                    uriBuilderLocalVar.Host = HttpClient.BaseAddress!.Host;
                    uriBuilderLocalVar.Port = HttpClient.BaseAddress.Port;
                    uriBuilderLocalVar.Scheme = HttpClient.BaseAddress.Scheme;
                    uriBuilderLocalVar.Path = ClientUtils.CONTEXT_PATH + "/account_api/v1/entitlement";
                    uriBuilderLocalVar.Path = uriBuilderLocalVar.Path.Replace("%7Bkey%7D", Uri.EscapeDataString(key.ToString()));

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

                        ApiResponse<GetEntitlementResponse> apiResponseLocalVar = new ApiResponse<GetEntitlementResponse>(httpRequestMessageLocalVar, httpResponseMessageLocalVar, responseContentLocalVar, "/account_api/v1/entitlement", requestedAtLocalVar, _jsonSerializerOptions);

                        AfterGetEntitlementDefaultImplementation(apiResponseLocalVar, key);

                        Events.ExecuteOnGetEntitlement(apiResponseLocalVar);

                        if (apiResponseLocalVar.StatusCode == (HttpStatusCode) 429)
                            foreach(TokenBase tokenBaseLocalVar in tokenBaseLocalVars)
                                tokenBaseLocalVar.BeginRateLimit();

                        return apiResponseLocalVar;
                    }
                }
            }
            catch(Exception e)
            {
                OnErrorGetEntitlementDefaultImplementation(e, "/account_api/v1/entitlement", uriBuilderLocalVar.Path, key);
                Events.ExecuteOnErrorGetEntitlement(e);
                throw;
            }
        }

        partial void FormatGetEntitlements(ref Option<int?> pageSize, ref Option<string?> startingAfter);

        /// <summary>
        /// Processes the server response
        /// </summary>
        /// <param name="apiResponseLocalVar"></param>
        /// <param name="pageSize"></param>
        /// <param name="startingAfter"></param>
        private void AfterGetEntitlementsDefaultImplementation(ApiResponse<GetEntitlementsResponse> apiResponseLocalVar, Option<int?> pageSize, Option<string?> startingAfter)
        {
            bool suppressDefaultLog = false;
            AfterGetEntitlements(ref suppressDefaultLog, apiResponseLocalVar, pageSize, startingAfter);
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
        partial void AfterGetEntitlements(ref bool suppressDefaultLog, ApiResponse<GetEntitlementsResponse> apiResponseLocalVar, Option<int?> pageSize, Option<string?> startingAfter);

        /// <summary>
        /// Logs exceptions that occur while retrieving the server response
        /// </summary>
        /// <param name="exception"></param>
        /// <param name="pathFormat"></param>
        /// <param name="path"></param>
        /// <param name="pageSize"></param>
        /// <param name="startingAfter"></param>
        private void OnErrorGetEntitlementsDefaultImplementation(Exception exception, string pathFormat, string path, Option<int?> pageSize, Option<string?> startingAfter)
        {
            bool suppressDefaultLog = false;
            OnErrorGetEntitlements(ref suppressDefaultLog, exception, pathFormat, path, pageSize, startingAfter);
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
        partial void OnErrorGetEntitlements(ref bool suppressDefaultLog, Exception exception, string pathFormat, string path, Option<int?> pageSize, Option<string?> startingAfter);

        /// <summary>
        /// Get entitlements Returns all the entitlements the user currently has access to 
        /// </summary>
        /// <param name="pageSize">Number of results per page. Defaults to 10 if parameter not sent. (optional)</param>
        /// <param name="startingAfter">The ID of the entitlement to start after. (optional)</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns><see cref="Task"/>&lt;<see cref="ApiResponse{T}"/>&gt; where T : <see cref="GetEntitlementsResponse"/></returns>
        public async Task<ApiResponse<GetEntitlementsResponse>?> GetEntitlementsOrDefaultAsync(Option<int?> pageSize = default, Option<string?> startingAfter = default, System.Threading.CancellationToken cancellationToken = default)
        {
            try
            {
                return await GetEntitlementsAsync(pageSize, startingAfter, cancellationToken).ConfigureAwait(false);
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// Get entitlements Returns all the entitlements the user currently has access to 
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="pageSize">Number of results per page. Defaults to 10 if parameter not sent. (optional)</param>
        /// <param name="startingAfter">The ID of the entitlement to start after. (optional)</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns><see cref="Task"/>&lt;<see cref="ApiResponse{T}"/>&gt; where T : <see cref="GetEntitlementsResponse"/></returns>
        public async Task<ApiResponse<GetEntitlementsResponse>> GetEntitlementsAsync(Option<int?> pageSize = default, Option<string?> startingAfter = default, System.Threading.CancellationToken cancellationToken = default)
        {
            UriBuilder uriBuilderLocalVar = new UriBuilder();

            try
            {
                FormatGetEntitlements(ref pageSize, ref startingAfter);

                using (HttpRequestMessage httpRequestMessageLocalVar = new HttpRequestMessage())
                {
                    uriBuilderLocalVar.Host = HttpClient.BaseAddress!.Host;
                    uriBuilderLocalVar.Port = HttpClient.BaseAddress.Port;
                    uriBuilderLocalVar.Scheme = HttpClient.BaseAddress.Scheme;
                    uriBuilderLocalVar.Path = ClientUtils.CONTEXT_PATH + "/account_api/v1/entitlements";

                    System.Collections.Specialized.NameValueCollection parseQueryStringLocalVar = System.Web.HttpUtility.ParseQueryString(string.Empty);

                    if (pageSize.IsSet)
                        parseQueryStringLocalVar["page_size"] = pageSize.Value?.ToString();

                    if (startingAfter.IsSet)
                        parseQueryStringLocalVar["starting_after"] = startingAfter.Value?.ToString();

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

                        ApiResponse<GetEntitlementsResponse> apiResponseLocalVar = new ApiResponse<GetEntitlementsResponse>(httpRequestMessageLocalVar, httpResponseMessageLocalVar, responseContentLocalVar, "/account_api/v1/entitlements", requestedAtLocalVar, _jsonSerializerOptions);

                        AfterGetEntitlementsDefaultImplementation(apiResponseLocalVar, pageSize, startingAfter);

                        Events.ExecuteOnGetEntitlements(apiResponseLocalVar);

                        if (apiResponseLocalVar.StatusCode == (HttpStatusCode) 429)
                            foreach(TokenBase tokenBaseLocalVar in tokenBaseLocalVars)
                                tokenBaseLocalVar.BeginRateLimit();

                        return apiResponseLocalVar;
                    }
                }
            }
            catch(Exception e)
            {
                OnErrorGetEntitlementsDefaultImplementation(e, "/account_api/v1/entitlements", uriBuilderLocalVar.Path, pageSize, startingAfter);
                Events.ExecuteOnErrorGetEntitlements(e);
                throw;
            }
        }
    }
}
