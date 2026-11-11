// The Kiota/SDK type pairs the mapper carries, found by reflection so a regeneration cannot leave one untested.
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Kiota.Abstractions.Serialization;

namespace Kinde.Api.Test.Integration.Mappers
{
    internal static class ManagementMappingManifest
    {
        private const string KiotaNamespace = "Kinde.Api.Kiota.Management";

        /// <summary>
        /// Pairs whose names and shapes give nothing to match on. Everything else is discovered.
        /// </summary>
        private static readonly (Type Kiota, Type Sdk)[] ExplicitPairs =
        {
            (typeof(Kinde.Api.Kiota.Management.Api.V1.Applications.Item.Native_apps.Native_appsGetResponse), typeof(Kinde.Api.Model.GetApplicationNativeApps200Response)),
            (typeof(Kinde.Api.Kiota.Management.Api.V1.Applications.Item.Native_apps.Native_appsPostResponse), typeof(Kinde.Api.Model.CreateApplicationNativeApp201Response)),
            (typeof(Kinde.Api.Kiota.Management.Api.V1.Billing.Agreements.Item.WithAgreement_DeleteResponse), typeof(Kinde.Api.Model.DeleteBillingAgreement200Response)),
            (typeof(Kinde.Api.Kiota.Management.Api.V1.Organizations.Item.Mfa.MfaGetResponse), typeof(Kinde.Api.Model.GetOrganizationMFA200Response)),
        };

        /// <summary>
        /// Models the wrappers expose that have no Kiota counterpart, because those operations
        /// still go through the OpenAPI client.
        /// </summary>
        internal static readonly IReadOnlyCollection<Type> WithoutKiotaCounterpart = new[]
        {
            typeof(Kinde.Api.Model.GetPasskeyResponse),
            typeof(Kinde.Api.Model.UpdatePasskeyResponse),
            typeof(Kinde.Api.Model.GetOrganizationPasskeyResponse),
            typeof(Kinde.Api.Model.UpdateOrganizationPasskeyResponse),
        };

        private static readonly Lazy<IReadOnlyList<(Type Source, Type Destination)>> Discovered =
            new Lazy<IReadOnlyList<(Type, Type)>>(Discover);

        internal static IEnumerable<(Type Source, Type Destination)> Pairs => Discovered.Value;

        /// <summary>Every SDK model that appears in the signature of a wrapper operation.</summary>
        internal static IEnumerable<Type> ModelsOnTheApiSurface()
        {
            var assembly = typeof(Kinde.Api.Model.SuccessResponse).Assembly;

            return assembly.GetTypes()
                .Where(type => type.IsInterface && type.Namespace == "Kinde.Api.Api" && type.Name.EndsWith("ApiAsync"))
                .SelectMany(api => api.GetMethods())
                .Where(method => method.Name.EndsWith("Async") && !method.Name.Contains("WithHttpInfo"))
                .SelectMany(method => method.GetParameters().Select(parameter => parameter.ParameterType)
                    .Append(method.ReturnType.IsGenericType ? method.ReturnType.GetGenericArguments()[0] : method.ReturnType))
                .Select(ElementOf)
                .Where(type => IsSdkModel(type) && type.IsClass)
                .Distinct();
        }

        private static IReadOnlyList<(Type, Type)> Discover()
        {
            var assembly = typeof(Kinde.Api.Model.SuccessResponse).Assembly;
            var types = assembly.GetTypes().Where(type => type.IsClass).ToList();
            var kiotaModels = types.Where(type => IsKiotaModel(type)).ToList();
            var sdkModels = types.Where(IsSdkModel).ToList();

            var found = new HashSet<(Type Kiota, Type Sdk)>();

            foreach (var kiota in kiotaModels.Where(type => !type.IsNested))
            {
                foreach (var sdk in sdkModels.Where(candidate => Normalize(candidate.Name) == Normalize(kiota.Name)))
                {
                    found.Add((kiota, sdk));
                }
            }

            found.UnionWith(ExplicitPairs);

            // A request body has no name in common with its SDK request, but it is built from the
            // same schema, so the two carry the same set of members.
            var sdkMembers = sdkModels
                .Where(type => type.Name.Contains("Request"))
                .ToDictionary(type => type, MemberNames);

            foreach (var kiota in kiotaModels.Where(type => type.Name.Contains("RequestBody")))
            {
                var members = MemberNames(kiota);
                if (members.Length == 0)
                {
                    continue;
                }

                foreach (var sdk in sdkMembers.Where(entry => entry.Value.SequenceEqual(members)))
                {
                    if (SameKinds(kiota, sdk.Key))
                    {
                        found.Add((kiota, sdk.Key));
                    }
                }
            }

            // The mapper follows members by name, so each pair brings its nested pairs with it.
            var pending = new Queue<(Type Kiota, Type Sdk)>(found);
            while (pending.Count > 0)
            {
                var (kiota, sdk) = pending.Dequeue();
                var sdkProperties = sdk.GetProperties(BindingFlags.Public | BindingFlags.Instance)
                    .GroupBy(property => property.Name, StringComparer.OrdinalIgnoreCase)
                    .ToDictionary(group => group.Key, group => group.First(), StringComparer.OrdinalIgnoreCase);

                foreach (var property in kiota.GetProperties(BindingFlags.Public | BindingFlags.Instance))
                {
                    if (!sdkProperties.TryGetValue(property.Name, out var counterpart))
                    {
                        continue;
                    }

                    var nestedKiota = ElementOf(property.PropertyType);
                    var nestedSdk = ElementOf(counterpart.PropertyType);
                    if (IsKiotaModel(nestedKiota) && IsSdkModel(nestedSdk) && found.Add((nestedKiota, nestedSdk)))
                    {
                        pending.Enqueue((nestedKiota, nestedSdk));
                    }
                }
            }

            // A request converter is written for one direction only: nothing maps a request back.
            var registered = Kinde.Api.Mappers.KindeMapperConfiguration.CreateRegistry()
                .RegisteredConversions.ToHashSet();

            return found
                .OrderBy(pair => pair.Kiota.FullName, StringComparer.Ordinal)
                .ThenBy(pair => pair.Sdk.FullName, StringComparer.Ordinal)
                .SelectMany(pair => registered.Contains((pair.Sdk, pair.Kiota)) && !registered.Contains((pair.Kiota, pair.Sdk))
                    ? new[] { (pair.Sdk, pair.Kiota) }
                    : new[] { (pair.Kiota, pair.Sdk), (pair.Sdk, pair.Kiota) })
                .ToList();
        }

        private enum Kind { Simple, Collection, Complex }

        /// <summary>
        /// Two types built from one schema agree on the kind of every shared member, which keeps a
        /// single-member body such as <c>{ value }</c> from pairing with an unrelated one.
        /// </summary>
        private static bool SameKinds(Type kiota, Type sdk)
        {
            var sdkProperties = sdk.GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Where(property => property.GetIndexParameters().Length == 0)
                .ToDictionary(property => Normalize(property.Name), property => property.PropertyType);

            return kiota.GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Where(property => property.Name != "AdditionalData" && property.Name != "BackingStore")
                .All(property => KindOf(property.PropertyType) == KindOf(sdkProperties[Normalize(property.Name)]));
        }

        private static Kind KindOf(Type type)
        {
            type = Nullable.GetUnderlyingType(type) ?? type;

            if (type.IsPrimitive || type.IsEnum || type == typeof(string) || type == typeof(decimal)
                || type == typeof(DateTime) || type == typeof(DateTimeOffset) || type == typeof(Guid))
            {
                return Kind.Simple;
            }

            return typeof(IEnumerable).IsAssignableFrom(type) && !type.GetInterfaces().Any(
                       candidate => candidate.IsGenericType && candidate.GetGenericTypeDefinition() == typeof(IDictionary<,>))
                ? Kind.Collection
                : Kind.Complex;
        }

        private static bool IsKiotaModel(Type type) =>
            type.IsClass && type.Namespace != null && type.Namespace.StartsWith(KiotaNamespace, StringComparison.Ordinal)
            && typeof(IParsable).IsAssignableFrom(type);

        private static bool IsSdkModel(Type type) =>
            type.IsClass && type.Namespace != null
            && (type.Namespace == "Kinde.Api.Model" || type.Namespace.StartsWith("Kinde.Api.Models", StringComparison.Ordinal));

        private static string Normalize(string name) => name.Replace("_", string.Empty).ToLowerInvariant();

        private static string[] MemberNames(Type type) =>
            type.GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Where(property => property.GetIndexParameters().Length == 0)
                .Select(property => property.Name)
                .Where(name => name != "AdditionalData" && name != "BackingStore")
                .Select(Normalize)
                .OrderBy(name => name, StringComparer.Ordinal)
                .ToArray();

        /// <summary>The model inside a nullable, list or dictionary type.</summary>
        private static Type ElementOf(Type type)
        {
            type = Nullable.GetUnderlyingType(type) ?? type;

            return type.IsGenericType && type != typeof(string) && typeof(IEnumerable).IsAssignableFrom(type)
                ? type.GetGenericArguments().Last()
                : type;
        }
    }
}
