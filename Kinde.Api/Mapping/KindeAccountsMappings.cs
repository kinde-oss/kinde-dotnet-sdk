using Kinde.Accounts.Model.Entities;
using KiotaModels = Kinde.Api.Kiota.Accounts.Models;

namespace Kinde.Api.Mapping
{
    /// <summary>
    /// Registers the Accounts API mappings that property-name matching cannot derive.
    /// </summary>
    internal static class KindeAccountsMappings
    {
        internal static void Register(KindeMappingRegistry registry)
        {
            RegisterUserPropertyValue(registry);
            RegisterFeatureFlagValue(registry);
        }

        /// <summary>
        /// <see cref="UserPropertyValue"/> is a hand-authored <c>oneOf</c> wrapper with read-only
        /// members and internal constructors, so it is built explicitly from the Kiota composed
        /// type rather than property by property.
        /// </summary>
        private static void RegisterUserPropertyValue(KindeMappingRegistry registry)
        {
            registry.Convert<KiotaModels.Get_user_properties_response_data_properties.Get_user_properties_response_data_properties_value, UserPropertyValue>(
                source =>
                {
                    if (source is null)
                    {
                        return null;
                    }

                    if (source.String is not null)
                    {
                        return new UserPropertyValue(source.String);
                    }

                    if (source.Boolean.HasValue)
                    {
                        return new UserPropertyValue(source.Boolean.Value);
                    }

                    return source.Integer.HasValue
                        ? new UserPropertyValue(source.Integer.Value)
                        : null;
                });

            registry.Convert<UserPropertyValue, KiotaModels.Get_user_properties_response_data_properties.Get_user_properties_response_data_properties_value>(
                source =>
                {
                    if (source is null)
                    {
                        return null;
                    }

                    var destination = new KiotaModels.Get_user_properties_response_data_properties.Get_user_properties_response_data_properties_value();
                    if (source.VarString is not null)
                    {
                        destination.String = source.VarString;
                    }
                    else if (source.VarBool.HasValue)
                    {
                        destination.Boolean = source.VarBool.Value;
                    }
                    else if (source.VarInt.HasValue)
                    {
                        destination.Integer = source.VarInt.Value;
                    }

                    return destination;
                });
        }

        /// <summary>
        /// The same read-only <c>oneOf</c> wrapper shape as
        /// <see cref="UserPropertyValue"/>, for feature flag values.
        /// </summary>
        /// <remarks>
        /// The AutoMapper profile this replaces carried no mapping for this pair, so the value was
        /// only ever populated by whichever internal constructor AutoMapper happened to select.
        /// Converting it explicitly makes <c>FeatureFlag.Value</c> carry the value the API
        /// actually returned.
        /// </remarks>
        private static void RegisterFeatureFlagValue(KindeMappingRegistry registry)
        {
            registry.Convert<KiotaModels.Get_feature_flags_response_data_feature_flags.Get_feature_flags_response_data_feature_flags_value, FeatureFlagValue>(
                source =>
                {
                    if (source is null)
                    {
                        return null;
                    }

                    if (source.String is not null)
                    {
                        return new FeatureFlagValue(source.String);
                    }

                    if (source.Boolean.HasValue)
                    {
                        return new FeatureFlagValue(source.Boolean.Value);
                    }

                    if (source.Integer.HasValue)
                    {
                        return new FeatureFlagValue(source.Integer.Value);
                    }

                    return source.GetFeatureFlagsResponseDataFeatureFlagsValueMember1 is { } member
                        ? new FeatureFlagValue((object)member)
                        : null;
                });

            registry.Convert<FeatureFlagValue, KiotaModels.Get_feature_flags_response_data_feature_flags.Get_feature_flags_response_data_feature_flags_value>(
                source =>
                {
                    if (source is null)
                    {
                        return null;
                    }

                    var destination = new KiotaModels.Get_feature_flags_response_data_feature_flags.Get_feature_flags_response_data_feature_flags_value();
                    if (source.VarString is not null)
                    {
                        destination.String = source.VarString;
                    }
                    else if (source.VarBool.HasValue)
                    {
                        destination.Boolean = source.VarBool.Value;
                    }
                    else if (source.VarInt.HasValue)
                    {
                        destination.Integer = source.VarInt.Value;
                    }

                    return destination;
                });
        }
    }
}
