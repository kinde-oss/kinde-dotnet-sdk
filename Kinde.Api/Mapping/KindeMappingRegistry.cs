using System;
using System.Collections.Generic;
using System.Linq;

namespace Kinde.Api.Mapping
{
    /// <summary>
    /// Holds the mappings that cannot be derived from property names: explicit converters for
    /// pairs whose shapes genuinely differ, and the properties a convention mapping must leave
    /// alone.
    /// </summary>
    internal sealed class KindeMappingRegistry
    {
        private static readonly HashSet<string> NoIgnoredProperties =
            new HashSet<string>(StringComparer.OrdinalIgnoreCase);

        private readonly Dictionary<(Type Source, Type Destination), Func<object, KindeMapper, object>> _converters =
            new Dictionary<(Type, Type), Func<object, KindeMapper, object>>();

        private readonly Dictionary<(Type Source, Type Destination), HashSet<string>> _ignored =
            new Dictionary<(Type, Type), HashSet<string>>();

        /// <summary>
        /// Registers an explicit converter from <typeparamref name="TSource"/> to
        /// <typeparamref name="TDestination"/>, replacing convention mapping for that pair.
        /// </summary>
        internal KindeMappingRegistry Convert<TSource, TDestination>(
            Func<TSource, KindeMapper, TDestination> converter)
        {
            if (converter == null)
            {
                throw new ArgumentNullException(nameof(converter));
            }

            _converters[(typeof(TSource), typeof(TDestination))] =
                (source, mapper) => converter((TSource)source, mapper);

            return this;
        }

        /// <summary>
        /// Registers an explicit converter that does not need to map nested members.
        /// </summary>
        internal KindeMappingRegistry Convert<TSource, TDestination>(Func<TSource, TDestination> converter)
        {
            if (converter == null)
            {
                throw new ArgumentNullException(nameof(converter));
            }

            return Convert<TSource, TDestination>((source, _) => converter(source));
        }

        /// <summary>
        /// Maps <typeparamref name="TSource"/> to <typeparamref name="TDestination"/> by convention
        /// but leaves the named destination properties untouched.
        /// </summary>
        internal KindeMappingRegistry Ignore<TSource, TDestination>(params string[] propertyNames)
        {
            if (propertyNames == null || propertyNames.Length == 0)
            {
                throw new ArgumentException(
                    "Specify at least one property name to ignore.", nameof(propertyNames));
            }

            var key = (typeof(TSource), typeof(TDestination));
            if (!_ignored.TryGetValue(key, out var names))
            {
                names = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
                _ignored[key] = names;
            }

            foreach (var name in propertyNames)
            {
                names.Add(name);
            }

            return this;
        }

        /// <summary>
        /// Registers an explicit converter for a pair of enums whose members the two generators
        /// spell differently, matching them on their wire names in both directions.
        /// </summary>
        internal KindeMappingRegistry BridgeEnums<TFirst, TSecond>()
            where TFirst : struct, Enum
            where TSecond : struct, Enum
        {
            return Convert<TFirst, TSecond>(source => BridgeEnum<TSecond>(source))
                .Convert<TSecond, TFirst>(source => BridgeEnum<TFirst>(source));
        }

        /// <summary>
        /// Translates an enum member to the member of <typeparamref name="TDestination"/> carrying
        /// the same wire name, falling back to the member name.
        /// </summary>
        internal static TDestination BridgeEnum<TDestination>(Enum source)
            where TDestination : struct, Enum
        {
            var wire = KindeValueConverter.GetWireName(source) ?? source.ToString();

            foreach (var name in Enum.GetNames(typeof(TDestination)))
            {
                var candidate = (TDestination)Enum.Parse(typeof(TDestination), name);
                var candidateWire = KindeValueConverter.GetWireName(candidate) ?? name;

                if (string.Equals(candidateWire, wire, StringComparison.OrdinalIgnoreCase))
                {
                    return candidate;
                }
            }

            return default;
        }

        internal bool TryGetConverter(
            Type sourceType, Type destinationType, out Func<object, KindeMapper, object> converter)
        {
            if (_converters.TryGetValue((sourceType, destinationType), out converter))
            {
                return true;
            }

            // A converter registered against a base type still applies to a derived source, which
            // matters because call sites pass the source as object and Kiota uses inheritance for
            // some response shapes.
            for (var baseType = sourceType.BaseType; baseType != null; baseType = baseType.BaseType)
            {
                if (_converters.TryGetValue((baseType, destinationType), out converter))
                {
                    return true;
                }
            }

            converter = null;
            return false;
        }

        internal HashSet<string> GetIgnoredProperties(Type sourceType, Type destinationType)
        {
            return _ignored.TryGetValue((sourceType, destinationType), out var names)
                ? names
                : NoIgnoredProperties;
        }

        /// <summary>
        /// The source/destination pairs that have an explicit converter. Used by the tests to
        /// assert the registry still covers every pair that needs one.
        /// </summary>
        internal IReadOnlyCollection<(Type Source, Type Destination)> RegisteredConversions =>
            _converters.Keys.ToList();
    }
}
