using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;
using Kinde.Api.Mapping;
using Kinde.Api.Mappers;
using Kinde.Api.Kiota.Management;
using Microsoft.Kiota.Abstractions.Authentication;
using Microsoft.Kiota.Abstractions.Store;
using Microsoft.Kiota.Http.HttpClientLibrary;
using Xunit;
using Xunit.Abstractions;
using KiotaModels = Kinde.Api.Kiota.Management.Models;

namespace Kinde.Api.Test.Integration.Mappers
{
    /// <summary>
    /// Maps real Kinde responses and fails on any field the API populated but the mapper dropped (null on both sides is only reported).
    /// Runs only with TestMode=Real in appsettings.json or KINDE_TEST_MODE=Real; skipped otherwise.
    /// </summary>
    public class RealTenantMappingCoverageTests : BaseIntegrationTest
    {
        private readonly ITestOutputHelper _output;
        private readonly IKindeMapper _mapper = KindeMapperConfiguration.Mapper;

        public RealTenantMappingCoverageTests(ITestOutputHelper output)
        {
            _output = output;
        }

        [Fact]
        [Trait("TestMode", "Real")]
        public async Task Users_RealResponse_MapsEveryPopulatedField()
        {
            await AssertMapsCleanly<KiotaModels.Users_response, global::Kinde.Api.Model.UsersResponse>(
                client => client.Api.V1.Users.GetAsync());
        }

        [Fact]
        [Trait("TestMode", "Real")]
        public async Task Organizations_RealResponse_MapsEveryPopulatedField()
        {
            await AssertMapsCleanly<KiotaModels.Get_organizations_response, global::Kinde.Api.Model.GetOrganizationsResponse>(
                client => client.Api.V1.Organizations.GetAsync());
        }

        [Fact]
        [Trait("TestMode", "Real")]
        public async Task Roles_RealResponse_MapsEveryPopulatedField()
        {
            await AssertMapsCleanly<KiotaModels.Get_roles_response, global::Kinde.Api.Model.GetRolesResponse>(
                client => client.Api.V1.Roles.GetAsync());
        }

        [Fact]
        [Trait("TestMode", "Real")]
        public async Task Permissions_RealResponse_MapsEveryPopulatedField()
        {
            await AssertMapsCleanly<KiotaModels.Get_permissions_response, global::Kinde.Api.Model.GetPermissionsResponse>(
                client => client.Api.V1.Permissions.GetAsync());
        }

        [Fact]
        [Trait("TestMode", "Real")]
        public async Task Applications_RealResponse_MapsEveryPopulatedField()
        {
            await AssertMapsCleanly<KiotaModels.Get_applications_response, global::Kinde.Api.Model.GetApplicationsResponse>(
                client => client.Api.V1.Applications.GetAsync());
        }

        [Fact]
        [Trait("TestMode", "Real")]
        public async Task Apis_RealResponse_MapsEveryPopulatedField()
        {
            await AssertMapsCleanly<KiotaModels.Get_apis_response, global::Kinde.Api.Model.GetApisResponse>(
                client => client.Api.V1.Apis.GetAsync());
        }

        [Fact]
        [Trait("TestMode", "Real")]
        public async Task Connections_RealResponse_MapsEveryPopulatedField()
        {
            await AssertMapsCleanly<KiotaModels.Get_connections_response, global::Kinde.Api.Model.GetConnectionsResponse>(
                client => client.Api.V1.Connections.GetAsync());
        }

        [Fact]
        [Trait("TestMode", "Real")]
        public async Task Subscribers_RealResponse_MapsEveryPopulatedField()
        {
            await AssertMapsCleanly<KiotaModels.Get_subscribers_response, global::Kinde.Api.Model.GetSubscribersResponse>(
                client => client.Api.V1.Subscribers.GetAsync());
        }

        [Fact]
        [Trait("TestMode", "Real")]
        public async Task Properties_RealResponse_MapsEveryPopulatedField()
        {
            await AssertMapsCleanly<KiotaModels.Get_properties_response, global::Kinde.Api.Model.GetPropertiesResponse>(
                client => client.Api.V1.Properties.GetAsync());
        }

        [Fact]
        [Trait("TestMode", "Real")]
        public async Task Timezones_RealResponse_MapsEveryPopulatedField()
        {
            await AssertMapsCleanly<KiotaModels.Get_timezones_response, global::Kinde.Api.Model.GetTimezonesResponse>(
                client => client.Api.V1.Timezones.GetAsync());
        }

        [Fact]
        [Trait("TestMode", "Real")]
        public async Task Industries_RealResponse_MapsEveryPopulatedField()
        {
            await AssertMapsCleanly<KiotaModels.Get_industries_response, global::Kinde.Api.Model.GetIndustriesResponse>(
                client => client.Api.V1.Industries.GetAsync());
        }

        [Fact]
        [Trait("TestMode", "Real")]
        public async Task Business_RealResponse_MapsEveryPopulatedField()
        {
            await AssertMapsCleanly<KiotaModels.Get_business_response, global::Kinde.Api.Model.GetBusinessResponse>(
                client => client.Api.V1.Business.GetAsync());
        }

        /// <summary>
        /// Calls the live endpoint, maps the response, and fails if any field the API populated did
        /// not survive.
        /// </summary>
        private async Task AssertMapsCleanly<TKiota, TSdk>(
            Func<KindeManagementClient, Task<TKiota>> call)
            where TKiota : class
        {
            if (!UseRealApi)
            {
                _output.WriteLine("Skipped: set TestMode to Real in appsettings.json to run this.");
                return;
            }

            var kiotaResponse = await call(CreateKiotaClient());
            if (kiotaResponse is null)
            {
                _output.WriteLine($"{typeof(TKiota).Name}: the API returned no body; nothing to compare.");
                return;
            }

            var mapped = _mapper.Map<TSdk>(kiotaResponse);
            Assert.NotNull(mapped);

            var report = new MappingReport();
            Compare(kiotaResponse, mapped, typeof(TKiota).Name, report, depth: 0);

            _output.WriteLine($"=== {typeof(TKiota).Name} -> {typeof(TSdk).Name} ===");
            _output.WriteLine($"populated on both sides : {report.Carried.Count}");
            _output.WriteLine($"null on both sides      : {report.EmptyBothSides.Count}");
            _output.WriteLine($"DROPPED by the mapper   : {report.Dropped.Count}");
            _output.WriteLine(string.Empty);

            foreach (var line in report.Carried)
            {
                _output.WriteLine($"  [ok]      {line}");
            }

            foreach (var line in report.EmptyBothSides)
            {
                _output.WriteLine($"  [empty]   {line}");
            }

            foreach (var line in report.Dropped)
            {
                _output.WriteLine($"  [DROPPED] {line}");
            }

            Assert.True(
                report.Dropped.Count == 0,
                $"The mapper dropped {report.Dropped.Count} field(s) the live API populated:"
                + Environment.NewLine
                + string.Join(Environment.NewLine, report.Dropped));
        }

        /// <summary>
        /// Walks both objects in step, recording for each source property whether its value reached
        /// the destination.
        /// </summary>
        private void Compare(object source, object destination, string path, MappingReport report, int depth)
        {
            // The response graphs are shallow; this only guards against a cycle through
            // AdditionalData holding a parent reference.
            if (depth > 6 || source is null)
            {
                return;
            }

            var destinationProperties = destination?.GetType()
                .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Where(property => property.GetIndexParameters().Length == 0)
                .ToDictionary(property => property.Name, StringComparer.OrdinalIgnoreCase)
                ?? new Dictionary<string, PropertyInfo>(StringComparer.OrdinalIgnoreCase);

            foreach (var sourceProperty in source.GetType()
                .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Where(property => property.GetIndexParameters().Length == 0)
                .Where(property => property.GetGetMethod() != null))
            {
                if (IsPlumbing(sourceProperty.Name))
                {
                    continue;
                }

                var sourceValue = SafeGet(sourceProperty, source);
                var memberPath = $"{path}.{sourceProperty.Name}";

                if (!destinationProperties.TryGetValue(sourceProperty.Name, out var destinationProperty))
                {
                    // No counterpart by name. Either it is deliberately bridged under a different
                    // name by an explicit converter, or it genuinely has no home on the SDK model.
                    // Only worth reporting when the API actually sent something.
                    if (!IsEmpty(sourceValue))
                    {
                        report.EmptyBothSides.Add($"{memberPath} (no same-named member on the SDK model)");
                    }

                    continue;
                }

                var destinationValue = SafeGet(destinationProperty, destination);

                if (IsEmpty(sourceValue))
                {
                    report.EmptyBothSides.Add($"{memberPath} (API returned nothing)");
                    continue;
                }

                if (IsEmpty(destinationValue))
                {
                    report.Dropped.Add(
                        $"{memberPath}: source has {Describe(sourceValue)} but the mapped model is {(destinationValue is null ? "null" : "empty")}");
                    continue;
                }

                report.Carried.Add($"{memberPath} = {Describe(destinationValue)}");

                // Recurse into nested models and the first element of each collection, which is
                // where a dropped nested field would otherwise hide.
                if (sourceValue is IEnumerable sourceItems && !(sourceValue is string))
                {
                    var firstSource = sourceItems.Cast<object>().FirstOrDefault();
                    var firstDestination = destinationValue is IEnumerable destinationItems
                        ? destinationItems.Cast<object>().FirstOrDefault()
                        : null;

                    if (firstSource is not null && firstDestination is not null && !IsScalar(firstSource))
                    {
                        Compare(firstSource, firstDestination, $"{memberPath}[0]", report, depth + 1);
                    }

                    continue;
                }

                if (!IsScalar(sourceValue))
                {
                    Compare(sourceValue, destinationValue, memberPath, report, depth + 1);
                }
            }
        }

        private static bool IsPlumbing(string name) =>
            name is "AdditionalData" or "BackingStore";

        private static object SafeGet(PropertyInfo property, object instance)
        {
            try
            {
                return property.GetValue(instance);
            }
            catch (Exception)
            {
                // Composed-type wrappers throw from some accessors when the discriminator is unset.
                return null;
            }
        }

        private static bool IsEmpty(object value) => value switch
        {
            null => true,
            string text => text.Length == 0,
            IEnumerable items => !items.Cast<object>().Any(),
            _ => false,
        };

        private static bool IsScalar(object value)
        {
            var type = value.GetType();
            return type.IsPrimitive
                || type.IsEnum
                || value is string
                || value is decimal
                || value is DateTime
                || value is DateTimeOffset
                || value is Guid
                || value is Uri;
        }

        private static string Describe(object value) => value switch
        {
            null => "null",
            string text => $"\"{Truncate(text)}\"",
            IEnumerable items => $"{items.Cast<object>().Count()} item(s)",
            _ => Truncate(value.ToString()),
        };

        private static string Truncate(string text) =>
            text.Length <= 60 ? text : text.Substring(0, 57) + "...";

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
                System.Threading.CancellationToken cancellationToken = default)
                => Task.FromResult(_token);
        }

        private sealed class MappingReport
        {
            internal List<string> Carried { get; } = new List<string>();

            internal List<string> EmptyBothSides { get; } = new List<string>();

            internal List<string> Dropped { get; } = new List<string>();
        }
    }
}
