using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Kinde.Api.Mapping;
using Kinde.Api.Mappers;
using Kinde.Api.Kiota.Management;
using Microsoft.Kiota.Abstractions.Authentication;
using Microsoft.Kiota.Http.HttpClientLibrary;
using Xunit;
using Xunit.Abstractions;
using KiotaModels = Kinde.Api.Kiota.Management.Models;
using SdkModels = Kinde.Api.Model;

namespace Kinde.Api.Test.Integration.Mappers
{
    /// <summary>
    /// Exercises, against live data, the mappings property-name matching cannot derive: dynamically keyed flags and
    /// user properties, undeclared organization fields, and the Connection wrapper. Runs only with TestMode=Real; read-only.
    /// </summary>
    public class RealTenantCustomMappingTests : BaseIntegrationTest
    {
        private readonly ITestOutputHelper _output;
        private readonly IKindeMapper _mapper = KindeMapperConfiguration.Mapper;

        public RealTenantCustomMappingTests(ITestOutputHelper output)
        {
            _output = output;
        }

        /// <summary>
        /// Kinde returns is_suspended and suspended_on although its OpenAPI description does not
        /// declare them, so Kiota leaves them in AdditionalData and an explicit converter lifts
        /// them onto the SDK model.
        /// </summary>
        [Fact]
        [Trait("TestMode", "Real")]
        public async Task GetOrganization_LiftsFieldsKindeReturnsOutsideItsSchema()
        {
            if (Skip())
            {
                return;
            }

            var client = CreateKiotaClient();
            var code = await FirstOrganizationCodeAsync(client);
            if (code is null)
            {
                _output.WriteLine("No organizations on this tenant; nothing to check.");
                return;
            }

            var kiota = await client.Api.V1.Organization.GetAsync(request =>
                request.QueryParameters.Code = code);
            Assert.NotNull(kiota);

            _output.WriteLine($"org code       : {code}");
            _output.WriteLine($"AdditionalData : {string.Join(", ", kiota.AdditionalData.Keys)}");

            var mapped = _mapper.Map<SdkModels.GetOrganizationResponse>(kiota);
            Assert.NotNull(mapped);

            _output.WriteLine($"Code           : {mapped.Code}");
            _output.WriteLine($"Name           : {mapped.Name}");
            _output.WriteLine($"IsSuspended    : {mapped.IsSuspended}");
            _output.WriteLine($"SuspendedOn    : {mapped.SuspendedOn ?? "(null)"}");

            Assert.Equal(code, mapped.Code);

            // Only assert the lift when Kinde actually sent the field; a tenant on a plan that
            // omits it would otherwise fail for a reason unrelated to the mapper.
            if (kiota.AdditionalData.TryGetValue("is_suspended", out var raw))
            {
                var expected = ReadBoolean(raw);
                if (expected.HasValue)
                {
                    Assert.Equal(expected.Value, mapped.IsSuspended);
                    _output.WriteLine($"is_suspended lifted from AdditionalData: {expected.Value}");
                }
            }
            else
            {
                _output.WriteLine("Kinde did not return is_suspended for this organization.");
            }
        }

        /// <summary>
        /// Kiota models an additionalProperties schema as a dynamically keyed wrapper rather than a
        /// dictionary, so the flag entries are read out of AdditionalData explicitly.
        /// </summary>
        [Fact]
        [Trait("TestMode", "Real")]
        public async Task GetOrganizationFeatureFlags_ReadsDynamicallyKeyedFlags()
        {
            if (Skip())
            {
                return;
            }

            var client = CreateKiotaClient();
            var code = await FirstOrganizationCodeAsync(client);
            if (code is null)
            {
                _output.WriteLine("No organizations on this tenant; nothing to check.");
                return;
            }

            var kiota = await client.Api.V1.Organizations[code].Feature_flags.GetAsync();
            Assert.NotNull(kiota);

            var rawKeys = kiota.FeatureFlags?.AdditionalData?.Keys?.ToList() ?? new List<string>();
            _output.WriteLine($"raw flag keys from Kiota : {rawKeys.Count}");
            foreach (var key in rawKeys)
            {
                _output.WriteLine($"  {key}");
            }

            var mapped = _mapper.Map<SdkModels.GetOrganizationFeatureFlagsResponse>(kiota);
            Assert.NotNull(mapped);
            Assert.NotNull(mapped.FeatureFlags);

            _output.WriteLine($"mapped flags             : {mapped.FeatureFlags.Count}");
            foreach (var flag in mapped.FeatureFlags)
            {
                _output.WriteLine($"  {flag.Key} = {flag.Value?.Value} ({flag.Value?.Type})");
            }

            // Every key Kinde returned must survive, with a value attached.
            Assert.Equal(rawKeys.Count, mapped.FeatureFlags.Count);
            foreach (var key in rawKeys)
            {
                Assert.True(
                    mapped.FeatureFlags.TryGetValue(key, out var flag),
                    $"Feature flag '{key}' was returned by Kinde but is missing after mapping.");
                Assert.NotNull(flag);
                Assert.False(
                    flag.Value is null,
                    $"Feature flag '{key}' lost its value during mapping.");
            }
        }

        /// <summary>
        /// Kiota names the nested object ConnectionProp; the SDK model names it VarConnection. This
        /// is the mapping that caused the bug fixed in PR #125, so it is worth proving against the
        /// live single-GET shape rather than only the flat list shape.
        /// </summary>
        [Fact]
        [Trait("TestMode", "Real")]
        public async Task GetConnection_MapsConnectionPropOntoVarConnection()
        {
            if (Skip())
            {
                return;
            }

            var client = CreateKiotaClient();

            var list = await client.Api.V1.Connections.GetAsync();
            var first = list?.Connections?.FirstOrDefault();
            if (first?.Id is null)
            {
                _output.WriteLine("No connections on this tenant; nothing to check.");
                return;
            }

            _output.WriteLine($"connection id : {first.Id}");

            // The flat list shape first: these items carry the fields directly, and the mapper has
            // to fold them into VarConnection to keep the public List<Connection> contract.
            var mappedListItem = _mapper.Map<SdkModels.Connection>(first);
            Assert.NotNull(mappedListItem);
            Assert.NotNull(mappedListItem.VarConnection);
            Assert.Equal(first.Id, mappedListItem.VarConnection.Id);
            Assert.Equal(first.Name, mappedListItem.VarConnection.Name);
            _output.WriteLine(
                $"list item -> VarConnection: id={mappedListItem.VarConnection.Id}, "
                + $"name={mappedListItem.VarConnection.Name}, "
                + $"strategy={mappedListItem.VarConnection.Strategy}");

            // Then the wrapped single-GET shape.
            var kiota = await client.Api.V1.Connections[first.Id].GetAsync();
            Assert.NotNull(kiota);

            var mapped = _mapper.Map<SdkModels.Connection>(kiota);
            Assert.NotNull(mapped);

            _output.WriteLine($"single GET ConnectionProp present : {kiota.ConnectionProp != null}");
            _output.WriteLine($"single GET VarConnection present  : {mapped.VarConnection != null}");

            if (kiota.ConnectionProp is null)
            {
                _output.WriteLine("Kinde returned no nested connection object for this id.");
                return;
            }

            Assert.NotNull(mapped.VarConnection);
            Assert.Equal(kiota.ConnectionProp.Id, mapped.VarConnection.Id);
            Assert.Equal(kiota.ConnectionProp.Name, mapped.VarConnection.Name);
            Assert.Equal(kiota.ConnectionProp.Strategy, mapped.VarConnection.Strategy);
            _output.WriteLine(
                $"single GET -> VarConnection: id={mapped.VarConnection.Id}, "
                + $"name={mapped.VarConnection.Name}, strategy={mapped.VarConnection.Strategy}");
        }

        /// <summary>
        /// A single user carries the nested identity and billing shapes the list endpoint omits.
        /// </summary>
        [Fact]
        [Trait("TestMode", "Real")]
        public async Task GetUser_MapsNestedIdentitiesAndBilling()
        {
            if (Skip())
            {
                return;
            }

            var client = CreateKiotaClient();

            var list = await client.Api.V1.Users.GetAsync();
            var id = list?.Users?.FirstOrDefault()?.Id;
            if (id is null)
            {
                _output.WriteLine("No users on this tenant; nothing to check.");
                return;
            }

            var kiota = await client.Api.V1.User.GetAsync(request =>
                request.QueryParameters.Id = id);
            Assert.NotNull(kiota);

            var mapped = _mapper.Map<SdkModels.User>(kiota);
            Assert.NotNull(mapped);

            _output.WriteLine($"user id     : {mapped.Id}");
            _output.WriteLine($"email       : {mapped.PreferredEmail}");
            _output.WriteLine($"identities  : kiota={kiota.Identities?.Count ?? 0}, mapped={mapped.Identities?.Count ?? 0}");

            Assert.Equal(kiota.Id, mapped.Id);

            if (kiota.Identities is { Count: > 0 })
            {
                Assert.NotNull(mapped.Identities);
                Assert.Equal(kiota.Identities.Count, mapped.Identities.Count);

                for (var index = 0; index < kiota.Identities.Count; index++)
                {
                    _output.WriteLine(
                        $"  identity[{index}] type={mapped.Identities[index].Type} "
                        + $"identity={mapped.Identities[index].Identity}");
                    Assert.Equal(kiota.Identities[index].Identity, mapped.Identities[index].Identity);
                    Assert.Equal(kiota.Identities[index].Type, mapped.Identities[index].Type);
                }
            }
            else
            {
                _output.WriteLine("Kinde returned no identities for this user.");
            }

            if (kiota.Billing is not null)
            {
                Assert.NotNull(mapped.Billing);
                Assert.Equal(kiota.Billing.CustomerId, mapped.Billing.CustomerId);
                _output.WriteLine($"billing     : customerId={mapped.Billing.CustomerId}");
            }
            else
            {
                _output.WriteLine("Kinde returned no billing for this user.");
            }
        }

        /// <summary>
        /// Search results carry user properties as a dynamically keyed wrapper, converted to a
        /// plain string dictionary.
        /// </summary>
        [Fact]
        [Trait("TestMode", "Real")]
        public async Task SearchUsers_ConvertsDynamicallyKeyedProperties()
        {
            if (Skip())
            {
                return;
            }

            var client = CreateKiotaClient();

            var kiota = await client.Api.V1.Search.Users.GetAsync(request =>
                request.QueryParameters.Query = "a");
            if (kiota?.Results is null or { Count: 0 })
            {
                _output.WriteLine("Search returned no results; nothing to check.");
                return;
            }

            var mapped = _mapper.Map<SdkModels.SearchUsersResponse>(kiota);
            Assert.NotNull(mapped);
            Assert.NotNull(mapped.Results);
            Assert.Equal(kiota.Results.Count, mapped.Results.Count);

            _output.WriteLine($"results: {mapped.Results.Count}");

            for (var index = 0; index < kiota.Results.Count; index++)
            {
                var rawKeys = kiota.Results[index].Properties?.AdditionalData?.Keys?.ToList()
                    ?? new List<string>();
                var mappedProperties = mapped.Results[index].Properties;

                _output.WriteLine(
                    $"  [{index}] id={mapped.Results[index].Id} "
                    + $"rawProps={rawKeys.Count} mappedProps={mappedProperties?.Count ?? 0}");

                Assert.NotNull(mappedProperties);
                foreach (var key in rawKeys)
                {
                    Assert.True(
                        mappedProperties.ContainsKey(key),
                        $"Property '{key}' was returned by Kinde but is missing after mapping.");
                    _output.WriteLine($"      {key} = {mappedProperties[key]}");
                }
            }
        }

        private bool Skip()
        {
            if (UseRealApi)
            {
                return false;
            }

            _output.WriteLine("Skipped: set TestMode to Real in appsettings.json to run this.");
            return true;
        }

        private static async Task<string> FirstOrganizationCodeAsync(KindeManagementClient client)
        {
            var organizations = await client.Api.V1.Organizations.GetAsync();
            return organizations?.Organizations?.FirstOrDefault()?.Code;
        }

        private static bool? ReadBoolean(object value) => value switch
        {
            bool flag => flag,
            string text when bool.TryParse(text, out var parsed) => parsed,
            Microsoft.Kiota.Abstractions.Serialization.UntypedBoolean node => node.GetValue(),
            _ => null,
        };

        private KindeManagementClient CreateKiotaClient()
        {
            var provider = new BaseBearerTokenAuthenticationProvider(
                new StaticTokenProvider(ApiConfiguration.AccessToken));

            var adapter = new HttpClientRequestAdapter(provider, httpClient: new HttpClient())
            {
                BaseUrl = Config.ManagementApi.Domain,
            };

            return new KindeManagementClient(adapter);
        }

        private sealed class StaticTokenProvider : IAccessTokenProvider
        {
            private readonly string _token;

            internal StaticTokenProvider(string token) => _token = token ?? string.Empty;

            public AllowedHostsValidator AllowedHostsValidator { get; } = new AllowedHostsValidator();

            public Task<string> GetAuthorizationTokenAsync(
                Uri uri,
                Dictionary<string, object> additionalAuthenticationContext = null,
                CancellationToken cancellationToken = default)
                => Task.FromResult(_token);
        }
    }
}
