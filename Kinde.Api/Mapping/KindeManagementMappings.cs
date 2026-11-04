using System;
using System.Collections.Generic;
using Kinde.Api.Model;
using Microsoft.Kiota.Abstractions.Serialization;
using KiotaModels = Kinde.Api.Kiota.Management.Models;
using PostConnections = Kinde.Api.Kiota.Management.Api.V1.Connections;
using PutConnection = Kinde.Api.Kiota.Management.Api.V1.Connections.Item;
using AppProperties = Kinde.Api.Kiota.Management.Api.V1.Applications.Item.Properties.Item;
using Directories = Kinde.Api.Kiota.Management.Api.V1.Directories;
using Mfa = Kinde.Api.Kiota.Management.Api.V1.Mfa;
using OrgMfa = Kinde.Api.Kiota.Management.Api.V1.Organizations.Item.Mfa;

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
            RegisterPropertyValueUnion(registry);
            RegisterOrganization(registry);
            RegisterIgnoredMembers(registry);
        }

        /// <summary>
        /// Kiota names the nested object <c>ConnectionProp</c>; the SDK model names it
        /// <c>VarConnection</c>.
        /// </summary>
        private static void RegisterConnections(KindeMappingRegistry registry)
        {
            registry.Convert<KiotaModels.Connection, Connection>((source, mapper) => new Connection(
                source.Code,
                source.Message,
                mapper.Map<ConnectionConnection>(source.ConnectionProp)));

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

            registry.Convert<KiotaModels.Connection_list_item, Connection>(source => new Connection(
                null,
                null,
                new ConnectionConnection(source.Id, source.Name, source.DisplayName, source.Strategy)));

            registry.Convert<Connection, KiotaModels.Connection_list_item>(source =>
            {
                var destination = new KiotaModels.Connection_list_item();
                var connection = source.VarConnection;
                if (connection == null)
                {
                    return destination;
                }

                if (connection.Id != null)
                {
                    destination.Id = connection.Id;
                }

                if (connection.Name != null)
                {
                    destination.Name = connection.Name;
                }

                if (connection.DisplayName != null)
                {
                    destination.DisplayName = connection.DisplayName;
                }

                if (connection.Strategy != null)
                {
                    destination.Strategy = connection.Strategy;
                }

                return destination;
            });
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
                        case CreateConnectionRequestOptionsOneOf variant:
                            destination.WithConnectionPutRequestBodyOptionsMember1 =
                                mapper.Map<PutConnection.WithConnection_PutRequestBody_optionsMember1>(variant);
                            break;
                        case ReplaceConnectionRequestOptionsOneOf variant:
                            destination.WithConnectionPutRequestBodyOptionsMember2 =
                                mapper.Map<PutConnection.WithConnection_PutRequestBody_optionsMember2>(variant);
                            break;
                        case ReplaceConnectionRequestOptionsOneOf1 variant:
                            destination.WithConnectionPutRequestBodyOptionsMember3 =
                                mapper.Map<PutConnection.WithConnection_PutRequestBody_optionsMember3>(variant);
                            break;
                        default:
                            throw new ArgumentException(
                                $"Unsupported ReplaceConnectionRequestOptions variant: {source.ActualInstance.GetType().FullName}. "
                                + "Expected CreateConnectionRequestOptionsOneOf, ReplaceConnectionRequestOptionsOneOf, "
                                + "or ReplaceConnectionRequestOptionsOneOf1.",
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
                        case CreateConnectionRequestOptionsOneOf variant:
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
                                + "Expected CreateConnectionRequestOptionsOneOf, UpdateConnectionRequestOptionsOneOf, "
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

                .BridgeEnums<ReplaceConnectionRequestOptionsOneOf1.NameIdFormatEnum,
                    PutConnection.WithConnection_PutRequestBody_optionsMember3_name_id_format>()
                .BridgeEnums<ReplaceConnectionRequestOptionsOneOf1.ProtocolBindingEnum,
                    PutConnection.WithConnection_PutRequestBody_optionsMember3_protocol_binding>()
                .BridgeEnums<ReplaceConnectionRequestOptionsOneOf1.SignRequestAlgorithmEnum,
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
                .Ignore<CreateOrganizationRequest, Kiota.Management.Api.V1.Organization.OrganizationPostRequestBody>("FeatureFlags")
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
                    value = ExtractUntypedValue(valueNode)?.ToString();
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
            var other => other.ToString(),
        };

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
                ? ExtractUntypedValue(node)?.ToString()
                : value.ToString();
        }
    }
}
