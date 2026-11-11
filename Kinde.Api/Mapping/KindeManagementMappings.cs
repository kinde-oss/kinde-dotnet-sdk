using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Nodes;
using Kinde.Api.Model;
using Microsoft.Kiota.Abstractions.Serialization;
using KiotaModels = Kinde.Api.Kiota.Management.Models;
using PostConnections = Kinde.Api.Kiota.Management.Api.V1.Connections;
using PutConnection = Kinde.Api.Kiota.Management.Api.V1.Connections.Item;
using AppProperties = Kinde.Api.Kiota.Management.Api.V1.Applications.Item.Properties.Item;
using Directories = Kinde.Api.Kiota.Management.Api.V1.Directories;
using Mfa = Kinde.Api.Kiota.Management.Api.V1.Mfa;
using OrgMfa = Kinde.Api.Kiota.Management.Api.V1.Organizations.Item.Mfa;
using OrgProperties = Kinde.Api.Kiota.Management.Api.V1.Organizations.Item.Properties;
using UserProperties = Kinde.Api.Kiota.Management.Api.V1.Users.Item.Properties;

namespace Kinde.Api.Mapping
{
    /// <summary>
    /// Registers the Management API mappings that property-name matching cannot derive.
    /// </summary>
    internal static class KindeManagementMappings
    {
        internal static void Register(KindeMappingRegistry registry)
        {
            RegisterConnections(registry);
            RegisterConnectionOptions(registry);
            RegisterEnumBridges(registry);
            RegisterDynamicKeyedResponses(registry);
            RegisterDynamicKeyedRequests(registry);
            RegisterPropertyValueUnion(registry);
            RegisterOrganization(registry);
            RegisterIgnoredMembers(registry);
        }

        /// <summary>
        /// Kiota names the nested object <c>ConnectionProp</c>; the SDK model names it
        /// <c>VarConnection</c>. List items arrive flat, so their fields are read from <c>AdditionalData</c>.
        /// </summary>
        private static void RegisterConnections(KindeMappingRegistry registry)
        {
            registry.Convert<KiotaModels.Connection, Connection>((source, mapper) => new Connection(
                source.Code,
                source.Message,
                source.ConnectionProp is not null
                    ? mapper.Map<ConnectionConnection>(source.ConnectionProp)
                    : ReadFlatConnection(source.AdditionalData)));

            registry.Convert<Connection, KiotaModels.Connection>((source, mapper) =>
            {
                var destination = new KiotaModels.Connection();

                if (source.Code != null)
                {
                    destination.Code = source.Code;
                }

                if (source.Message != null)
                {
                    destination.Message = source.Message;
                }

                if (source.VarConnection != null)
                {
                    destination.ConnectionProp =
                        mapper.Map<KiotaModels.Connection_connection>(source.VarConnection);
                }

                return destination;
            });
        }

        private static ConnectionConnection ReadFlatConnection(IDictionary<string, object> additionalData)
        {
            var id = ReadString(additionalData, "id");
            var name = ReadString(additionalData, "name");
            var displayName = ReadString(additionalData, "display_name");
            var strategy = ReadString(additionalData, "strategy");

            return id is null && name is null && displayName is null && strategy is null
                ? null
                : new ConnectionConnection(id, name, displayName, strategy);
        }

        /// <summary>
        /// The connection options are oneOf unions: the SDK holds one ActualInstance, Kiota has a
        /// member property per variant.
        /// </summary>
        private static void RegisterConnectionOptions(KindeMappingRegistry registry)
        {
            registry.Convert<CreateConnectionRequestOptions, PostConnections.ConnectionsPostRequestBody.ConnectionsPostRequestBody_options>(
                (source, mapper) =>
                {
                    if (source?.ActualInstance is null)
                    {
                        return null;
                    }

                    var destination = new PostConnections.ConnectionsPostRequestBody.ConnectionsPostRequestBody_options();
                    switch (source.ActualInstance)
                    {
                        case CreateConnectionRequestOptionsOneOf variant:
                            destination.ConnectionsPostRequestBodyOptionsMember1 =
                                mapper.Map<PostConnections.ConnectionsPostRequestBody_optionsMember1>(variant);
                            break;
                        case CreateConnectionRequestOptionsOneOf1 variant:
                            destination.ConnectionsPostRequestBodyOptionsMember2 =
                                mapper.Map<PostConnections.ConnectionsPostRequestBody_optionsMember2>(variant);
                            break;
                        case CreateConnectionRequestOptionsOneOf2 variant:
                            destination.ConnectionsPostRequestBodyOptionsMember3 =
                                mapper.Map<PostConnections.ConnectionsPostRequestBody_optionsMember3>(variant);
                            break;
                        default:
                            throw new ArgumentException(
                                $"Unsupported CreateConnectionRequestOptions variant: {source.ActualInstance.GetType().FullName}. "
                                + "Expected CreateConnectionRequestOptionsOneOf, OneOf1, or OneOf2.",
                                nameof(source));
                    }

                    return destination;
                });

            registry.Convert<ReplaceConnectionRequestOptions, PutConnection.WithConnection_PutRequestBody.WithConnection_PutRequestBody_options>(
                (source, mapper) =>
                {
                    if (source?.ActualInstance is null)
                    {
                        return null;
                    }

                    var destination = new PutConnection.WithConnection_PutRequestBody.WithConnection_PutRequestBody_options();
                    switch (source.ActualInstance)
                    {
                        case ReplaceConnectionRequestOptionsOneOf variant:
                            destination.WithConnectionPutRequestBodyOptionsMember1 =
                                mapper.Map<PutConnection.WithConnection_PutRequestBody_optionsMember1>(variant);
                            break;
                        case ReplaceConnectionRequestOptionsOneOf1 variant:
                            destination.WithConnectionPutRequestBodyOptionsMember2 =
                                mapper.Map<PutConnection.WithConnection_PutRequestBody_optionsMember2>(variant);
                            break;
                        case ReplaceConnectionRequestOptionsOneOf2 variant:
                            destination.WithConnectionPutRequestBodyOptionsMember3 =
                                mapper.Map<PutConnection.WithConnection_PutRequestBody_optionsMember3>(variant);
                            break;
                        default:
                            throw new ArgumentException(
                                $"Unsupported ReplaceConnectionRequestOptions variant: {source.ActualInstance.GetType().FullName}. "
                                + "Expected ReplaceConnectionRequestOptionsOneOf, OneOf1, or OneOf2.",
                                nameof(source));
                    }

                    return destination;
                });

            registry.Convert<UpdateConnectionRequestOptions, PutConnection.WithConnection_PatchRequestBody.WithConnection_PatchRequestBody_options>(
                (source, mapper) =>
                {
                    if (source?.ActualInstance is null)
                    {
                        return null;
                    }

                    var destination = new PutConnection.WithConnection_PatchRequestBody.WithConnection_PatchRequestBody_options();
                    switch (source.ActualInstance)
                    {
                        case ReplaceConnectionRequestOptionsOneOf variant:
                            destination.WithConnectionPatchRequestBodyOptionsMember1 =
                                mapper.Map<PutConnection.WithConnection_PatchRequestBody_optionsMember1>(variant);
                            break;
                        case UpdateConnectionRequestOptionsOneOf variant:
                            destination.WithConnectionPatchRequestBodyOptionsMember2 =
                                mapper.Map<PutConnection.WithConnection_PatchRequestBody_optionsMember2>(variant);
                            break;
                        case UpdateConnectionRequestOptionsOneOf1 variant:
                            destination.WithConnectionPatchRequestBodyOptionsMember3 =
                                mapper.Map<PutConnection.WithConnection_PatchRequestBody_optionsMember3>(variant);
                            break;
                        default:
                            throw new ArgumentException(
                                $"Unsupported UpdateConnectionRequestOptions variant: {source.ActualInstance.GetType().FullName}. "
                                + "Expected ReplaceConnectionRequestOptionsOneOf, UpdateConnectionRequestOptionsOneOf, "
                                + "or UpdateConnectionRequestOptionsOneOf1.",
                                nameof(source));
                    }

                    return destination;
                });
        }

        /// <summary>
        /// Enum pairs the two generators spell differently, matched on their wire names.
        /// </summary>
        private static void RegisterEnumBridges(KindeMappingRegistry registry)
        {
            registry
                .BridgeEnums<CreateConnectionRequestOptionsOneOf2.NameIdFormatEnum,
                    PostConnections.ConnectionsPostRequestBody_optionsMember3_name_id_format>()
                .BridgeEnums<CreateConnectionRequestOptionsOneOf2.ProtocolBindingEnum,
                    PostConnections.ConnectionsPostRequestBody_optionsMember3_protocol_binding>()
                .BridgeEnums<CreateConnectionRequestOptionsOneOf2.SignRequestAlgorithmEnum,
                    PostConnections.ConnectionsPostRequestBody_optionsMember3_sign_request_algorithm>()

                .BridgeEnums<ReplaceConnectionRequestOptionsOneOf2.NameIdFormatEnum,
                    PutConnection.WithConnection_PutRequestBody_optionsMember3_name_id_format>()
                .BridgeEnums<ReplaceConnectionRequestOptionsOneOf2.ProtocolBindingEnum,
                    PutConnection.WithConnection_PutRequestBody_optionsMember3_protocol_binding>()
                .BridgeEnums<ReplaceConnectionRequestOptionsOneOf2.SignRequestAlgorithmEnum,
                    PutConnection.WithConnection_PutRequestBody_optionsMember3_sign_request_algorithm>()

                .BridgeEnums<UpdateConnectionRequestOptionsOneOf1.NameIdFormatEnum,
                    PutConnection.WithConnection_PatchRequestBody_optionsMember3_name_id_format>()
                .BridgeEnums<UpdateConnectionRequestOptionsOneOf1.ProtocolBindingEnum,
                    PutConnection.WithConnection_PatchRequestBody_optionsMember3_protocol_binding>()
                .BridgeEnums<UpdateConnectionRequestOptionsOneOf1.SignRequestAlgorithmEnum,
                    PutConnection.WithConnection_PatchRequestBody_optionsMember3_sign_request_algorithm>()

                .BridgeEnums<ReplaceMFARequest.EnabledFactorsEnum, Mfa.MfaPutRequestBody_enabled_factors>()
                .BridgeEnums<ReplaceOrganizationMFARequest.EnabledFactorsEnum, OrgMfa.MfaPutRequestBody_enabled_factors>()
                .BridgeEnums<CreateDirectoryRequest.ProviderCodeEnum, Directories.DirectoriesPostRequestBody_provider_code>();
        }

        /// <summary>
        /// Kiota models an additionalProperties schema as a dynamically keyed wrapper, so entries
        /// are read out of its AdditionalData. Response-only: the reverse is never rebuilt.
        /// </summary>
        private static void RegisterDynamicKeyedResponses(KindeMappingRegistry registry)
        {
            registry.Convert<KiotaModels.Get_environment_feature_flags_response, GetEnvironmentFeatureFlagsResponse>(
                (source, mapper) =>
                {
                    var destination = mapper.MapByConvention<GetEnvironmentFeatureFlagsResponse>(source);
                    destination.FeatureFlags = ReadFeatureFlags(source.FeatureFlags?.AdditionalData);
                    return destination;
                });

            registry.Convert<KiotaModels.Get_organization_feature_flags_response, GetOrganizationFeatureFlagsResponse>(
                (source, mapper) =>
                {
                    var destination = mapper.MapByConvention<GetOrganizationFeatureFlagsResponse>(source);
                    destination.FeatureFlags = ReadFeatureFlags(source.FeatureFlags?.AdditionalData);
                    return destination;
                });

            registry.Convert<KiotaModels.Search_users_response_results, SearchUsersResponseResultsInner>(
                (source, mapper) =>
                {
                    var destination = mapper.MapByConvention<SearchUsersResponseResultsInner>(source);
                    destination.Properties = ReadStringMap(source.Properties?.AdditionalData);
                    return destination;
                });

            registry
                .Ignore<KiotaModels.Get_environment_feature_flags_response, GetEnvironmentFeatureFlagsResponse>("FeatureFlags")
                .Ignore<KiotaModels.Get_organization_feature_flags_response, GetOrganizationFeatureFlagsResponse>("FeatureFlags")
                .Ignore<KiotaModels.Search_users_response_results, SearchUsersResponseResultsInner>("Properties")
                .Ignore<GetEnvironmentFeatureFlagsResponse, KiotaModels.Get_environment_feature_flags_response>("FeatureFlags")
                .Ignore<GetOrganizationFeatureFlagsResponse, KiotaModels.Get_organization_feature_flags_response>("FeatureFlags")
                .Ignore<SearchUsersResponseResultsInner, KiotaModels.Search_users_response_results>("Properties");
        }

        /// <summary>
        /// Free-form request objects. Kiota stores their entries in AdditionalData, which name
        /// matching never fills, so without these the object is sent empty or not at all.
        /// </summary>
        private static void RegisterDynamicKeyedRequests(KindeMappingRegistry registry)
        {
            registry.Convert<UpdateOrganizationPropertiesRequest, OrgProperties.PropertiesPatchRequestBody>(
                source =>
                {
                    var destination = new OrgProperties.PropertiesPatchRequestBody();
                    if (source.Properties is not null)
                    {
                        destination.Properties = new OrgProperties.PropertiesPatchRequestBody_properties
                        {
                            AdditionalData = ToAdditionalData(source.Properties),
                        };
                    }

                    return destination;
                });

            registry.Convert<UpdateOrganizationPropertiesRequest, UserProperties.PropertiesPatchRequestBody>(
                source =>
                {
                    var destination = new UserProperties.PropertiesPatchRequestBody();
                    if (source.Properties is not null)
                    {
                        destination.Properties = new UserProperties.PropertiesPatchRequestBody_properties
                        {
                            AdditionalData = ToAdditionalData(source.Properties),
                        };
                    }

                    return destination;
                });

            registry.Convert<Dictionary<string, CreateOrganizationRequest.InnerEnum>,
                Kiota.Management.Api.V1.Organization.OrganizationPostRequestBody_feature_flags>(
                source => new Kiota.Management.Api.V1.Organization.OrganizationPostRequestBody_feature_flags
                {
                    AdditionalData = ToAdditionalData(source),
                });

            RegisterUpstreamParams<PostConnections.ConnectionsPostRequestBody_optionsMember2_upstream_params>(registry);
            RegisterUpstreamParams<PostConnections.ConnectionsPostRequestBody_optionsMember3_upstream_params>(registry);
            RegisterUpstreamParams<PutConnection.WithConnection_PutRequestBody_optionsMember2_upstream_params>(registry);
            RegisterUpstreamParams<PutConnection.WithConnection_PutRequestBody_optionsMember3_upstream_params>(registry);
            RegisterUpstreamParams<PutConnection.WithConnection_PatchRequestBody_optionsMember2_upstream_params>(registry);
            RegisterUpstreamParams<PutConnection.WithConnection_PatchRequestBody_optionsMember3_upstream_params>(registry);
        }

        private static void RegisterUpstreamParams<TWrapper>(KindeMappingRegistry registry)
            where TWrapper : IAdditionalDataHolder, new()
        {
            registry.Convert<Dictionary<string, object>, TWrapper>(
                source => new TWrapper { AdditionalData = ToAdditionalData(source) });
        }

        /// <summary>
        /// Turns a free-form object (a dictionary, an anonymous object, a Json.NET or System.Text.Json
        /// value) into the entries of a Kiota additionalProperties wrapper. Nested values become
        /// untyped nodes, which the Kiota JSON writer serializes as written.
        /// </summary>
        private static IDictionary<string, object> ToAdditionalData(object source)
        {
            if (ToUntyped(source, 0) is not UntypedObject members)
            {
                throw new KindeMappingException(
                    $"Expected a set of key/value pairs but got '{source.GetType().FullName}'. "
                    + "Pass a dictionary, an anonymous object or a JSON object.");
            }

            return members.GetValue().ToDictionary(entry => entry.Key, entry => (object)entry.Value);
        }

        /// <summary>How deep a free-form value may nest before it is taken to be a cycle.</summary>
        private const int MaxFreeFormDepth = 32;

        private static UntypedNode ToUntyped(object value, int depth)
        {
            if (depth > MaxFreeFormDepth)
            {
                throw new KindeMappingException(
                    $"A free-form value nests deeper than {MaxFreeFormDepth} levels. It most likely refers back to itself.");
            }

            switch (value)
            {
                case null:
                    return new UntypedNull();
                case UntypedNode node:
                    return node;
                case string text:
                    return new UntypedString(text);
                case bool flag:
                    return new UntypedBoolean(flag);
                case byte or sbyte or short or ushort or int:
                    return new UntypedInteger(System.Convert.ToInt32(value, CultureInfo.InvariantCulture));
                case uint or long:
                    return new UntypedLong(System.Convert.ToInt64(value, CultureInfo.InvariantCulture));
                case ulong large:
                    return large <= long.MaxValue
                        ? new UntypedLong((long)large)
                        : new UntypedDecimal(large);
                case float number:
                    return new UntypedFloat(number);
                case double number:
                    return new UntypedDouble(number);
                case decimal number:
                    return new UntypedDecimal(number);
                case Enum member:
                    return new UntypedString(KindeValueConverter.GetWireName(member) ?? member.ToString());
                case DateTime moment:
                    return new UntypedString(moment.ToString("o", CultureInfo.InvariantCulture));
                case DateTimeOffset moment:
                    return new UntypedString(moment.ToString("o", CultureInfo.InvariantCulture));
                case JsonElement element:
                    return FromJsonElement(element);
                case JsonNode jsonNode:
                    using (var document = JsonDocument.Parse(jsonNode.ToJsonString()))
                    {
                        return FromJsonElement(document.RootElement);
                    }
                case Newtonsoft.Json.Linq.JToken token:
                    using (var document = JsonDocument.Parse(token.ToString(Newtonsoft.Json.Formatting.None)))
                    {
                        return FromJsonElement(document.RootElement);
                    }
                case Guid or TimeSpan or Uri or char:
                    return new UntypedString(value.ToString());
            }

            if (KindeMapper.TryGetEntries(value, out var entries))
            {
                return new UntypedObject(entries.ToDictionary(
                    entry => System.Convert.ToString(entry.Key, CultureInfo.InvariantCulture),
                    entry => ToUntyped(entry.Value, depth + 1)));
            }

            if (value is IEnumerable items)
            {
                return new UntypedArray(items.Cast<object>().Select(item => ToUntyped(item, depth + 1)).ToList());
            }

            return new UntypedObject(value.GetType()
                .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Where(property => property.GetIndexParameters().Length == 0 && property.GetGetMethod() != null)
                .ToDictionary(property => property.Name, property => ToUntyped(property.GetValue(value), depth + 1)));
        }

        private static UntypedNode FromJsonElement(JsonElement element)
        {
            switch (element.ValueKind)
            {
                case JsonValueKind.Object:
                    return new UntypedObject(element.EnumerateObject()
                        .ToDictionary(member => member.Name, member => FromJsonElement(member.Value)));
                case JsonValueKind.Array:
                    return new UntypedArray(element.EnumerateArray().Select(FromJsonElement).ToList());
                case JsonValueKind.String:
                    return new UntypedString(element.GetString());
                case JsonValueKind.True:
                    return new UntypedBoolean(true);
                case JsonValueKind.False:
                    return new UntypedBoolean(false);
                case JsonValueKind.Number:
                    if (element.TryGetInt32(out var integer))
                    {
                        return new UntypedInteger(integer);
                    }

                    return element.TryGetInt64(out var wide)
                        ? new UntypedLong(wide)
                        : new UntypedDouble(element.GetDouble());
                default:
                    return new UntypedNull();
            }
        }

        /// <summary>
        /// An application property value is a <c>oneOf</c> of boolean and string.
        /// </summary>
        private static void RegisterPropertyValueUnion(KindeMappingRegistry registry)
        {
            registry.Convert<UpdateApplicationsPropertyRequestValue, AppProperties.WithProperty_keyPutRequestBody.WithProperty_keyPutRequestBody_value>(
                source =>
                {
                    if (source?.ActualInstance is null)
                    {
                        return null;
                    }

                    var destination = new AppProperties.WithProperty_keyPutRequestBody.WithProperty_keyPutRequestBody_value();
                    switch (source.ActualInstance)
                    {
                        case bool value:
                            destination.Boolean = value;
                            break;
                        case string value:
                            destination.String = value;
                            break;
                        default:
                            throw new ArgumentException(
                                $"Unsupported UpdateApplicationsPropertyRequestValue variant: {source.ActualInstance.GetType().FullName}. "
                                + "Expected bool or string.",
                                nameof(source));
                    }

                    return destination;
                });

            registry.Convert<AppProperties.WithProperty_keyPutRequestBody.WithProperty_keyPutRequestBody_value, UpdateApplicationsPropertyRequestValue>(
                source =>
                {
                    if (source is null)
                    {
                        return null;
                    }

                    if (source.Boolean.HasValue)
                    {
                        return new UpdateApplicationsPropertyRequestValue(source.Boolean.Value);
                    }

                    return source.String is not null
                        ? new UpdateApplicationsPropertyRequestValue(source.String)
                        : null;
                });
        }

        /// <summary>
        /// The organization response carries two fields the OpenAPI description omits, so Kiota
        /// leaves them in AdditionalData.
        /// </summary>
        private static void RegisterOrganization(KindeMappingRegistry registry)
        {
            registry.Convert<KiotaModels.Get_organization_response, GetOrganizationResponse>(
                (source, mapper) =>
                {
                    var destination = mapper.MapByConvention<GetOrganizationResponse>(source);

                    if (ReadBool(source.AdditionalData, "is_suspended") is { } suspended)
                    {
                        destination.IsSuspended = suspended;
                    }

                    if (ReadString(source.AdditionalData, "suspended_on") is { } suspendedOn)
                    {
                        destination.SuspendedOn = suspendedOn;
                    }

                    return destination;
                });

            registry.Convert<GetOrganizationResponse, KiotaModels.Get_organization_response>(
                (source, mapper) =>
                {
                    var destination = mapper.MapByConvention<KiotaModels.Get_organization_response>(source);

                    var additionalData = destination.AdditionalData ?? new Dictionary<string, object>();
                    additionalData["is_suspended"] = source.IsSuspended;
                    if (source.SuspendedOn is { Length: > 0 } suspendedOn)
                    {
                        additionalData["suspended_on"] = suspendedOn;
                    }

                    destination.AdditionalData = additionalData;
                    return destination;
                });
        }

        /// <summary>Properties the convention pass must skip, because the shapes disagree.</summary>
        private static void RegisterIgnoredMembers(KindeMappingRegistry registry)
        {
            registry
                .Ignore<CreateCategoryRequest, Kiota.Management.Api.V1.Property_categories.Property_categoriesPostRequestBody>("Context")
                .Ignore<CreatePropertyRequest, Kiota.Management.Api.V1.Properties.PropertiesPostRequestBody>("Context", "Type")
                .Ignore<CreateFeatureFlagRequest, Kiota.Management.Api.V1.Feature_flags.Feature_flagsPostRequestBody>("Type", "AllowOverrideLevel")
                .Ignore<Kiota.Management.Api.V1.Organization.OrganizationPostRequestBody, CreateOrganizationRequest>("FeatureFlags");
        }

        /// <summary>Converts a Kiota additionalProperties wrapper into typed feature flags.</summary>
        private static Dictionary<string, GetOrganizationFeatureFlagsResponseFeatureFlagsValue> ReadFeatureFlags(
            IDictionary<string, object> additionalData)
        {
            var result = new Dictionary<string, GetOrganizationFeatureFlagsResponseFeatureFlagsValue>();
            if (additionalData is null)
            {
                return result;
            }

            foreach (var entry in additionalData)
            {
                if (entry.Value is not UntypedObject flag)
                {
                    continue;
                }

                var fields = flag.GetValue();

                GetOrganizationFeatureFlagsResponseFeatureFlagsValue.TypeEnum? type = null;
                if (fields.TryGetValue("type", out var typeNode)
                    && ExtractUntypedValue(typeNode) is string typeName)
                {
                    type = typeName switch
                    {
                        "str" => GetOrganizationFeatureFlagsResponseFeatureFlagsValue.TypeEnum.Str,
                        "int" => GetOrganizationFeatureFlagsResponseFeatureFlagsValue.TypeEnum.Int,
                        "bool" => GetOrganizationFeatureFlagsResponseFeatureFlagsValue.TypeEnum.Bool,
                        _ => null,
                    };
                }

                string value = null;
                if (fields.TryGetValue("value", out var valueNode) && valueNode is not null)
                {
                    value = FormatInvariant(ExtractUntypedValue(valueNode));
                    if (type == GetOrganizationFeatureFlagsResponseFeatureFlagsValue.TypeEnum.Bool
                        && value is not null)
                    {
                        value = value.ToLowerInvariant();
                    }
                }

                result[entry.Key] = new GetOrganizationFeatureFlagsResponseFeatureFlagsValue(type, value);
            }

            return result;
        }

        /// <summary>Converts a Kiota additionalProperties wrapper into a string dictionary.</summary>
        internal static Dictionary<string, string> ReadStringMap(IDictionary<string, object> additionalData)
        {
            var result = new Dictionary<string, string>();
            if (additionalData is null)
            {
                return result;
            }

            foreach (var entry in additionalData)
            {
                result[entry.Key] = entry.Value switch
                {
                    UntypedNode node => FormatValue(ExtractUntypedValue(node)),
                    null => null,
                    var value => FormatValue(value),
                };
            }

            return result;
        }

        /// <summary>Formats a value, lowercasing booleans to match the JSON literals.</summary>
        private static string FormatValue(object value) => value switch
        {
            null => null,
            bool flag => flag ? "true" : "false",
            var other => FormatInvariant(other),
        };

        /// <summary>
        /// Formats a dynamic value with the invariant culture, so a number or date read out of
        /// AdditionalData does not pick up the host's decimal separator or date order.
        /// </summary>
        private static string FormatInvariant(object value) =>
            value is IFormattable formattable
                ? formattable.ToString(null, CultureInfo.InvariantCulture)
                : value?.ToString();

        /// <summary>
        /// Extracts the value from an <see cref="UntypedNode"/>. GetValue is non-virtual, so a
        /// base-typed call reaches a stub that throws; this dispatches on the concrete type.
        /// </summary>
        private static object ExtractUntypedValue(UntypedNode node) => node switch
        {
            UntypedString value => value.GetValue(),
            UntypedBoolean value => value.GetValue(),
            UntypedInteger value => value.GetValue(),
            UntypedLong value => value.GetValue(),
            UntypedDouble value => value.GetValue(),
            UntypedFloat value => value.GetValue(),
            UntypedDecimal value => value.GetValue(),
            UntypedObject value => value.GetValue(),
            UntypedArray value => value.GetValue(),
            _ => null,
        };

        private static bool? ReadBool(IDictionary<string, object> data, string key)
        {
            if (data is null || !data.TryGetValue(key, out var value) || value is null)
            {
                return null;
            }

            if (value is bool flag)
            {
                return flag;
            }

            if (value is string text && bool.TryParse(text, out var parsedText))
            {
                return parsedText;
            }

            if (value is UntypedNode node && ExtractUntypedValue(node) is { } extracted)
            {
                if (extracted is bool extractedFlag)
                {
                    return extractedFlag;
                }

                if (extracted is string extractedText && bool.TryParse(extractedText, out var parsedExtracted))
                {
                    return parsedExtracted;
                }
            }

            return bool.TryParse(value.ToString(), out var parsed) ? parsed : null;
        }

        private static string ReadString(IDictionary<string, object> data, string key)
        {
            if (data is null || !data.TryGetValue(key, out var value) || value is null)
            {
                return null;
            }

            if (value is string text)
            {
                return text;
            }

            return value is UntypedNode node
                ? FormatInvariant(ExtractUntypedValue(node))
                : FormatInvariant(value);
        }
    }
}
