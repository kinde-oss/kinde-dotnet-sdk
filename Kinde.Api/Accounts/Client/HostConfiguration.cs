#nullable enable

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Net.Http;
using Microsoft.Extensions.DependencyInjection;
using Kinde.Accounts.Api;
using Kinde.Accounts.Model;
using Kinde.Accounts.Model.Entities;
using Kinde.Accounts.Model.Responses;

namespace Kinde.Accounts.Client
{
    /// <summary>
    /// Provides hosting configuration for Kinde.Accounts
    /// </summary>
    public class HostConfiguration
    {
        private readonly IServiceCollection _services;
        private readonly JsonSerializerOptions _jsonOptions = new JsonSerializerOptions();

        internal bool HttpClientsAdded { get; private set; }

        /// <summary>
        /// Instantiates the class 
        /// </summary>
        /// <param name="services"></param>
        public HostConfiguration(IServiceCollection services)
        {
            _services = services;
            _jsonOptions.Converters.Add(new JsonStringEnumConverter());
            _jsonOptions.Converters.Add(new DateTimeJsonConverter());
            _jsonOptions.Converters.Add(new DateTimeNullableJsonConverter());
            _jsonOptions.Converters.Add(new ErrorJsonConverter());
            _jsonOptions.Converters.Add(new ErrorResponseJsonConverter());
            _jsonOptions.Converters.Add(new GetEntitlementResponseJsonConverter());
            _jsonOptions.Converters.Add(new GetEntitlementResponseDataJsonConverter());
            _jsonOptions.Converters.Add(new EntitlementDetailJsonConverter());
            _jsonOptions.Converters.Add(new GetEntitlementsResponseJsonConverter());
            _jsonOptions.Converters.Add(new GetEntitlementsResponseDataJsonConverter());
            _jsonOptions.Converters.Add(new EntitlementJsonConverter());
            _jsonOptions.Converters.Add(new PlanJsonConverter());
            _jsonOptions.Converters.Add(new GetEntitlementsResponseMetadataJsonConverter());
            _jsonOptions.Converters.Add(new GetFeatureFlagsResponseJsonConverter());
            _jsonOptions.Converters.Add(new GetFeatureFlagsResponseDataJsonConverter());
            _jsonOptions.Converters.Add(new FeatureFlagJsonConverter());
            _jsonOptions.Converters.Add(new FeatureFlagValueJsonConverter());
            _jsonOptions.Converters.Add(new GetUserPermissionsResponseJsonConverter());
            _jsonOptions.Converters.Add(new GetUserPermissionsResponseDataJsonConverter());
            _jsonOptions.Converters.Add(new PermissionJsonConverter());
            _jsonOptions.Converters.Add(new GetUserPermissionsResponseMetadataJsonConverter());
            _jsonOptions.Converters.Add(new GetUserPropertiesResponseJsonConverter());
            _jsonOptions.Converters.Add(new GetUserPropertiesResponseDataJsonConverter());
            _jsonOptions.Converters.Add(new UserPropertyJsonConverter());
            _jsonOptions.Converters.Add(new UserPropertyValueJsonConverter());
            _jsonOptions.Converters.Add(new GetUserPropertiesResponseMetadataJsonConverter());
            _jsonOptions.Converters.Add(new GetUserRolesResponseJsonConverter());
            _jsonOptions.Converters.Add(new GetUserRolesResponseDataJsonConverter());
            _jsonOptions.Converters.Add(new RoleJsonConverter());
            _jsonOptions.Converters.Add(new GetUserRolesResponseMetadataJsonConverter());
            _jsonOptions.Converters.Add(new PortalLinkJsonConverter());
            _jsonOptions.Converters.Add(new TokenErrorResponseJsonConverter());
            _jsonOptions.Converters.Add(new TokenIntrospectJsonConverter());
            _jsonOptions.Converters.Add(new UserProfileV2JsonConverter());
            JsonSerializerOptionsProvider jsonSerializerOptionsProvider = new JsonSerializerOptionsProvider(_jsonOptions);
            _services.AddSingleton(jsonSerializerOptionsProvider);
            _services.AddSingleton<IApiFactory, ApiFactory>();
            _services.AddSingleton<BillingApiEvents>();
            _services.AddTransient<IBillingApi, BillingApi>();
            _services.AddSingleton<FeatureFlagsApiEvents>();
            _services.AddTransient<IFeatureFlagsApi, FeatureFlagsApi>();
            _services.AddSingleton<OAuthApiEvents>();
            _services.AddTransient<IOAuthApi, OAuthApi>();
            _services.AddSingleton<PermissionsApiEvents>();
            _services.AddTransient<IPermissionsApi, PermissionsApi>();
            _services.AddSingleton<PropertiesApiEvents>();
            _services.AddTransient<IPropertiesApi, PropertiesApi>();
            _services.AddSingleton<RolesApiEvents>();
            _services.AddTransient<IRolesApi, RolesApi>();
            _services.AddSingleton<SelfServePortalApiEvents>();
            _services.AddTransient<ISelfServePortalApi, SelfServePortalApi>();
        }

        /// <summary>
        /// Configures the HttpClients.
        /// </summary>
        /// <param name="client"></param>
        /// <param name="builder"></param>
        /// <returns></returns>
        public HostConfiguration AddApiHttpClients
        (
            Action<HttpClient>? client = null, Action<IHttpClientBuilder>? builder = null)
        {
            if (client == null)
                client = c => c.BaseAddress = new Uri(ClientUtils.BASE_ADDRESS);

            List<IHttpClientBuilder> builders = new List<IHttpClientBuilder>();

            builders.Add(_services.AddHttpClient<IBillingApi, BillingApi>(client));
            builders.Add(_services.AddHttpClient<IFeatureFlagsApi, FeatureFlagsApi>(client));
            builders.Add(_services.AddHttpClient<IOAuthApi, OAuthApi>(client));
            builders.Add(_services.AddHttpClient<IPermissionsApi, PermissionsApi>(client));
            builders.Add(_services.AddHttpClient<IPropertiesApi, PropertiesApi>(client));
            builders.Add(_services.AddHttpClient<IRolesApi, RolesApi>(client));
            builders.Add(_services.AddHttpClient<ISelfServePortalApi, SelfServePortalApi>(client));
            
            if (builder != null)
                foreach (IHttpClientBuilder instance in builders)
                    builder(instance);

            HttpClientsAdded = true;

            return this;
        }

        /// <summary>
        /// Configures the JsonSerializerSettings
        /// </summary>
        /// <param name="options"></param>
        /// <returns></returns>
        public HostConfiguration ConfigureJsonOptions(Action<JsonSerializerOptions> options)
        {
            options(_jsonOptions);

            return this;
        }

        /// <summary>
        /// Adds tokens to your IServiceCollection
        /// </summary>
        /// <typeparam name="TTokenBase"></typeparam>
        /// <param name="token"></param>
        /// <returns></returns>
        public HostConfiguration AddTokens<TTokenBase>(TTokenBase token) where TTokenBase : TokenBase
        {
            return AddTokens(new TTokenBase[]{ token });
        }

        /// <summary>
        /// Adds tokens to your IServiceCollection
        /// </summary>
        /// <typeparam name="TTokenBase"></typeparam>
        /// <param name="tokens"></param>
        /// <returns></returns>
        public HostConfiguration AddTokens<TTokenBase>(IEnumerable<TTokenBase> tokens) where TTokenBase : TokenBase
        {
            TokenContainer<TTokenBase> container = new TokenContainer<TTokenBase>(tokens);
            _services.AddSingleton(services => container);

            return this;
        }

        /// <summary>
        /// Adds a token provider to your IServiceCollection
        /// </summary>
        /// <typeparam name="TTokenProvider"></typeparam>
        /// <typeparam name="TTokenBase"></typeparam>
        /// <returns></returns>
        public HostConfiguration UseProvider<TTokenProvider, TTokenBase>() 
            where TTokenProvider : TokenProvider<TTokenBase>
            where TTokenBase : TokenBase
        {
            _services.AddSingleton<TTokenProvider>();
            _services.AddSingleton<TokenProvider<TTokenBase>>(services => services.GetRequiredService<TTokenProvider>());

            return this;
        }
    }
}
