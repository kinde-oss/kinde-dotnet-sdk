#nullable enable

using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Kiota.Abstractions;
using Microsoft.Kiota.Abstractions.Authentication;
using Microsoft.Kiota.Http.HttpClientLibrary;
using Kinde.Api.Mappers;
using Kinde.Accounts.Client;
using KiotaAccountsClient = Kinde.Api.Kiota.Accounts.KindeAccountsClient;

namespace Kinde.Accounts.Api
{
    /// <summary>
    /// Base class providing Kiota client infrastructure for Accounts API facades.
    /// This class manages the Kiota client creation and provides access to AutoMapper.
    /// </summary>
    public abstract class KiotaAccountsBase
    {
        private KiotaAccountsClient? _kiotaClient;
        private readonly object _lock = new object();

        /// <summary>
        /// Gets the AutoMapper instance for model translation.
        /// </summary>
        protected IMapper KiotaMapper => KindeMapperConfiguration.Mapper;

        /// <summary>
        /// Gets the HttpClient used for requests.
        /// </summary>
        protected abstract HttpClient KiotaHttpClient { get; }

        /// <summary>
        /// Gets the token provider for authentication.
        /// </summary>
        protected abstract TokenProvider<BearerToken> KiotaBearerTokenProvider { get; }

        /// <summary>
        /// Gets or creates the Kiota Accounts client.
        /// </summary>
        protected KiotaAccountsClient KiotaClient
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

        private KiotaAccountsClient CreateKiotaClient()
        {
            // Get token synchronously for client creation
            var bearerToken = KiotaBearerTokenProvider.GetAsync(CancellationToken.None).AsTask().GetAwaiter().GetResult();

            var authProvider = new BaseBearerTokenAuthenticationProvider(
                new StaticAccessTokenProvider(bearerToken.RawToken ?? string.Empty));
            
            var adapter = new HttpClientRequestAdapter(authProvider, httpClient: KiotaHttpClient);
            
            // Set base URL from HttpClient
            if (KiotaHttpClient.BaseAddress != null)
            {
                adapter.BaseUrl = KiotaHttpClient.BaseAddress.ToString().TrimEnd('/');
            }
            
            return new KiotaAccountsClient(adapter);
        }

        /// <summary>
        /// Creates a Kiota client with a fresh token (useful for methods that need token refresh).
        /// </summary>
        protected async Task<KiotaAccountsClient> CreateKiotaClientWithTokenAsync(CancellationToken cancellationToken)
        {
            var bearerToken = await KiotaBearerTokenProvider.GetAsync(cancellationToken).ConfigureAwait(false);

            var authProvider = new BaseBearerTokenAuthenticationProvider(
                new StaticAccessTokenProvider(bearerToken.RawToken ?? string.Empty));
            
            var adapter = new HttpClientRequestAdapter(authProvider, httpClient: KiotaHttpClient);
            
            if (KiotaHttpClient.BaseAddress != null)
            {
                adapter.BaseUrl = KiotaHttpClient.BaseAddress.ToString().TrimEnd('/');
            }
            
            return new KiotaAccountsClient(adapter);
        }

        /// <summary>
        /// Invalidates the cached Kiota client, forcing recreation on next use.
        /// </summary>
        protected void InvalidateKiotaClient()
        {
            lock (_lock)
            {
                _kiotaClient = null;
            }
        }

        /// <summary>
        /// Static token provider for Kiota authentication.
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
                Dictionary<string, object>? additionalAuthenticationContext = null,
                CancellationToken cancellationToken = default)
            {
                return Task.FromResult(_token);
            }
        }
    }
}

