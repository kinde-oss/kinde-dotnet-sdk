using System.Net.Http;
using AutoMapper;
using Microsoft.Kiota.Abstractions;
using Microsoft.Kiota.Abstractions.Authentication;
using Microsoft.Kiota.Http.HttpClientLibrary;
using Kinde.Api.Kiota.Management;
using Kinde.Api.Kiota.Accounts;
using Kinde.Api.Mappers;

namespace Kinde.Api.Facades
{
    /// <summary>
    /// Factory for creating Kiota clients with proper authentication and mapping configuration.
    /// This class provides centralized client creation for the facade layer.
    /// </summary>
    public class KiotaClientFactory
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of the KiotaClientFactory.
        /// </summary>
        /// <param name="httpClient">The HttpClient to use for requests.</param>
        /// <param name="baseUrl">The base URL for the API (e.g., https://your-subdomain.kinde.com).</param>
        public KiotaClientFactory(HttpClient httpClient, string baseUrl)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            _baseUrl = baseUrl ?? throw new ArgumentNullException(nameof(baseUrl));
            _mapper = KindeMapperConfiguration.Mapper;
        }

        /// <summary>
        /// Gets the AutoMapper instance for model translation.
        /// </summary>
        public IMapper Mapper => _mapper;

        /// <summary>
        /// Creates a Kiota Management API client.
        /// </summary>
        /// <param name="accessToken">The access token for authentication.</param>
        /// <returns>A configured KindeManagementClient instance.</returns>
        public KindeManagementClient CreateManagementClient(string accessToken)
        {
            var authProvider = new BaseBearerTokenAuthenticationProvider(new TokenProvider(accessToken));
            var adapter = new HttpClientRequestAdapter(authProvider, httpClient: _httpClient);
            adapter.BaseUrl = _baseUrl;
            return new KindeManagementClient(adapter);
        }

        /// <summary>
        /// Creates a Kiota Accounts API client.
        /// </summary>
        /// <param name="accessToken">The access token for authentication.</param>
        /// <returns>A configured KindeAccountsClient instance.</returns>
        public KindeAccountsClient CreateAccountsClient(string accessToken)
        {
            var authProvider = new BaseBearerTokenAuthenticationProvider(new TokenProvider(accessToken));
            var adapter = new HttpClientRequestAdapter(authProvider, httpClient: _httpClient);
            adapter.BaseUrl = _baseUrl;
            return new KindeAccountsClient(adapter);
        }

        /// <summary>
        /// Simple token provider implementation for bearer authentication.
        /// </summary>
        private class TokenProvider : IAccessTokenProvider
        {
            private readonly string _token;

            public TokenProvider(string token)
            {
                _token = token;
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

