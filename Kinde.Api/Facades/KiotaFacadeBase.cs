/*
 * Kinde API - Kiota Facade Base
 * 
 * Provides shared infrastructure for all API facades that wrap Kiota clients.
 */

using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Kinde.Api.Client;
using Kinde.Api.Mappers;
using Kinde.Api.Kiota.Management;
using Microsoft.Kiota.Abstractions.Authentication;
using Microsoft.Kiota.Http.HttpClientLibrary;

namespace Kinde.Api.Facades
{
    /// <summary>
    /// Base class providing shared Kiota infrastructure for API facades.
    /// </summary>
    public abstract class KiotaFacadeBase
    {
        private KindeManagementClient _kiotaClient;
        private HttpClient _httpClient;
        private IMapper _mapper;
        private readonly object _lock = new object();

        /// <summary>
        /// Gets the configuration for this API facade.
        /// </summary>
        protected abstract IReadableConfiguration Configuration { get; }

        /// <summary>
        /// Gets the AutoMapper instance for model translation.
        /// </summary>
        protected IMapper Mapper => _mapper ??= KindeMapperConfiguration.Mapper;

        /// <summary>
        /// Gets or creates the Kiota Management API client.
        /// </summary>
        protected KindeManagementClient KiotaClient
        {
            get
            {
                if (_kiotaClient == null)
                {
                    lock (_lock)
                    {
                        if (_kiotaClient == null)
                        {
                            _kiotaClient = CreateKiotaClient();
                        }
                    }
                }
                return _kiotaClient;
            }
        }

        /// <summary>
        /// Creates a new Kiota client with the current configuration.
        /// </summary>
        private KindeManagementClient CreateKiotaClient()
        {
            var tokenProvider = new StaticAccessTokenProvider(Configuration.AccessToken);
            var authProvider = new BaseBearerTokenAuthenticationProvider(tokenProvider);
            
            _httpClient ??= new HttpClient();
            
            var adapter = new HttpClientRequestAdapter(authProvider, httpClient: _httpClient);
            adapter.BaseUrl = Configuration.BasePath;
            
            return new KindeManagementClient(adapter);
        }

        /// <summary>
        /// Wraps a Kiota call and maps exceptions to the OpenAPI ApiException format.
        /// </summary>
        protected async Task<TResult> ExecuteKiotaAsync<TResult>(
            Func<KindeManagementClient, Task<TResult>> operation,
            string operationName,
            CancellationToken cancellationToken = default)
        {
            try
            {
                return await operation(KiotaClient).ConfigureAwait(false);
            }
            catch (Microsoft.Kiota.Abstractions.ApiException ex)
            {
                throw new ApiException((int)ex.ResponseStatusCode, $"Error in {operationName}: {ex.Message}", ex);
            }
            catch (Exception ex)
            {
                throw new ApiException(500, $"Error in {operationName}: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Wraps a Kiota call for void operations.
        /// </summary>
        protected async Task ExecuteKiotaAsync(
            Func<KindeManagementClient, Task> operation,
            string operationName,
            CancellationToken cancellationToken = default)
        {
            try
            {
                await operation(KiotaClient).ConfigureAwait(false);
            }
            catch (Microsoft.Kiota.Abstractions.ApiException ex)
            {
                throw new ApiException((int)ex.ResponseStatusCode, $"Error in {operationName}: {ex.Message}", ex);
            }
            catch (Exception ex)
            {
                throw new ApiException(500, $"Error in {operationName}: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Creates an ApiResponse wrapper for the result.
        /// </summary>
        protected ApiResponse<T> CreateApiResponse<T>(T data, System.Net.HttpStatusCode statusCode = System.Net.HttpStatusCode.OK)
        {
            return new ApiResponse<T>(statusCode, new Multimap<string, string>(), data);
        }

        /// <summary>
        /// Static access token provider for Kiota authentication.
        /// </summary>
        private class StaticAccessTokenProvider : IAccessTokenProvider
        {
            private readonly string _token;

            public StaticAccessTokenProvider(string token)
            {
                _token = token ?? string.Empty;
            }

            public AllowedHostsValidator AllowedHostsValidator => new AllowedHostsValidator();

            public Task<string> GetAuthorizationTokenAsync(
                Uri uri,
                Dictionary<string, object> additionalAuthenticationContext = null,
                CancellationToken cancellationToken = default)
            {
                return Task.FromResult(_token);
            }
        }
    }
}

