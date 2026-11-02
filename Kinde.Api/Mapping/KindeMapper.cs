using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Kiota.Abstractions.Store;

namespace Kinde.Api.Mapping
{
    /// <summary>
    /// Maps between the OpenAPI-generated SDK models and the Kiota-generated models by matching
    /// property names, recursing into nested models and collections, and delegating the pairs
    /// that need bespoke handling to converters registered in a <see cref="KindeMappingRegistry"/>.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Both generators produce the same PascalCase property names from one OpenAPI description, so
    /// name matching covers the great majority of the mappings and keeps working when the Kiota
    /// models are regenerated. The pairs where the two shapes genuinely differ - composed
    /// <c>oneOf</c> wrappers, dynamically keyed <c>additionalProperties</c> objects, enums whose
    /// wire names the generators spell differently - are registered explicitly instead.
    /// </para>
    /// <para>
    /// Mapping plans are built once per source/destination pair and cached, so the reflection cost
    /// is paid on first use rather than per call.
    /// </para>
    /// </remarks>
    internal sealed class KindeMapper : IKindeMapper
    {
        /// <summary>
        /// Kiota plumbing members. They have no counterpart on the OpenAPI-generated side and must
        /// never be copied by convention.
        /// </summary>
        private static readonly HashSet<string> AlwaysIgnored =
            new HashSet<string>(StringComparer.OrdinalIgnoreCase) { "AdditionalData", "BackingStore" };

        private readonly KindeMappingRegistry _registry;

        private readonly ConcurrentDictionary<(Type Source, Type Destination), MappingPlan> _plans =
            new ConcurrentDictionary<(Type, Type), MappingPlan>();

        internal KindeMapper(KindeMappingRegistry registry)
        {
            _registry = registry ?? throw new ArgumentNullException(nameof(registry));
        }

        /// <inheritdoc />
        public TDestination Map<TDestination>(object source)
        {
            var mapped = Map(source, typeof(TDestination));
            return mapped == null ? default : (TDestination)mapped;
        }

        /// <inheritdoc />
        public object Map(object source, Type destinationType)
        {
            if (destinationType == null)
            {
                throw new ArgumentNullException(nameof(destinationType));
            }

            if (source == null)
            {
                return null;
            }

            var sourceType = source.GetType();

            // A destination typed as object takes the value through untouched. The SDK uses this
            // for payloads the OpenAPI description leaves unspecified.
            if (destinationType == typeof(object))
            {
                return source;
            }

            if (_registry.TryGetConverter(sourceType, destinationType, out var converter))
            {
                return converter(source, this);
            }

            if (KindeValueConverter.IsSimple(destinationType) || KindeValueConverter.IsSimple(sourceType))
            {
                if (KindeValueConverter.TryConvert(source, destinationType, out var converted))
                {
                    return converted;
                }

                // Nothing sensible to assign. Leaving it unset is safer than guessing.
                return null;
            }

            if (TryMapDictionary(source, destinationType, out var dictionary))
            {
                return dictionary;
            }

            if (TryMapEnumerable(source, destinationType, out var enumerable))
            {
                return enumerable;
            }

            return MapByConvention(source, destinationType);
        }

        /// <summary>
        /// Maps by matching property names, ignoring any explicit converter registered for the
        /// pair. Explicit converters use this to map the conventional members and then adjust only
        /// the few that need bespoke handling, instead of restating every property by hand.
        /// </summary>
        internal TDestination MapByConvention<TDestination>(object source)
        {
            var mapped = MapByConvention(source, typeof(TDestination));
            return mapped == null ? default : (TDestination)mapped;
        }

        internal object MapByConvention(object source, Type destinationType)
        {
            if (source == null)
            {
                return null;
            }

            var plan = _plans.GetOrAdd(
                (source.GetType(), destinationType),
                key => new MappingPlan(key.Source, key.Destination, _registry));

            return plan.Execute(source, this);
        }

        /// <summary>
        /// The source property names a convention mapping between these two types actually copies.
        /// Used by the tests to tell a property that is deliberately not mapped apart from one that
        /// is being dropped by mistake.
        /// </summary>
        internal IReadOnlyCollection<string> DescribeMappedSourceMembers(Type sourceType, Type destinationType)
        {
            var plan = _plans.GetOrAdd(
                (sourceType, destinationType),
                key => new MappingPlan(key.Source, key.Destination, _registry));

            return plan.MappedSourceMemberNames;
        }

        private bool TryMapDictionary(object source, Type destinationType, out object result)
        {
            result = null;

            var destinationArguments = GetDictionaryArguments(destinationType);
            if (destinationArguments == null || !(source is IDictionary sourceDictionary))
            {
                return false;
            }

            var keyType = destinationArguments[0];
            var valueType = destinationArguments[1];
            var concrete = typeof(Dictionary<,>).MakeGenericType(keyType, valueType);
            var target = (IDictionary)Activator.CreateInstance(concrete);

            foreach (DictionaryEntry entry in sourceDictionary)
            {
                var key = Map(entry.Key, keyType);
                if (key == null)
                {
                    continue;
                }

                target[key] = Map(entry.Value, valueType);
            }

            result = target;
            return true;
        }

        private bool TryMapEnumerable(object source, Type destinationType, out object result)
        {
            result = null;

            if (!(source is IEnumerable sourceItems) || source is string)
            {
                return false;
            }

            var elementType = GetEnumerableElementType(destinationType);
            if (elementType == null)
            {
                return false;
            }

            var listType = typeof(List<>).MakeGenericType(elementType);
            var list = (IList)Activator.CreateInstance(listType);

            foreach (var item in sourceItems)
            {
                list.Add(Map(item, elementType));
            }

            if (destinationType.IsArray)
            {
                var array = Array.CreateInstance(elementType, list.Count);
                list.CopyTo(array, 0);
                result = array;
                return true;
            }

            if (!destinationType.IsAssignableFrom(listType))
            {
                // A collection shape the mapper cannot construct, for example a bespoke
                // collection with no list-compatible surface.
                return false;
            }

            result = list;
            return true;
        }

        private static Type[] GetDictionaryArguments(Type type)
        {
            if (type.IsGenericType)
            {
                var definition = type.GetGenericTypeDefinition();
                if (definition == typeof(Dictionary<,>)
                    || definition == typeof(IDictionary<,>)
                    || definition == typeof(IReadOnlyDictionary<,>))
                {
                    return type.GetGenericArguments();
                }
            }

            return type.GetInterfaces()
                .Where(candidate => candidate.IsGenericType
                    && candidate.GetGenericTypeDefinition() == typeof(IDictionary<,>))
                .Select(candidate => candidate.GetGenericArguments())
                .FirstOrDefault();
        }

        private static Type GetEnumerableElementType(Type type)
        {
            if (type == typeof(string) || GetDictionaryArguments(type) != null)
            {
                return null;
            }

            if (type.IsArray)
            {
                return type.GetElementType();
            }

            if (type.IsGenericType)
            {
                var definition = type.GetGenericTypeDefinition();
                if (definition == typeof(List<>)
                    || definition == typeof(IList<>)
                    || definition == typeof(ICollection<>)
                    || definition == typeof(IEnumerable<>)
                    || definition == typeof(IReadOnlyList<>)
                    || definition == typeof(IReadOnlyCollection<>))
                {
                    return type.GetGenericArguments()[0];
                }
            }

            return null;
        }

        /// <summary>
        /// The cached per-pair plan: how to build the destination and which properties to copy.
        /// </summary>
        private sealed class MappingPlan
        {
            private readonly Type _destinationType;
            private readonly ConstructorInfo _constructor;
            private readonly object[] _constructorArguments;
            private readonly PropertyBinding[] _bindings;

            /// <summary>
            /// True when the destination is a Kiota model. Assigning null to a Kiota property marks
            /// it dirty in the backing store, which serializes an explicit <c>"property": null</c> -
            /// and some Kinde endpoints reject those. Unset properties are simply left alone.
            /// </summary>
            private readonly bool _skipNulls;

            internal MappingPlan(Type sourceType, Type destinationType, KindeMappingRegistry registry)
            {
                _destinationType = destinationType;
                _skipNulls = typeof(IBackedModel).IsAssignableFrom(destinationType);

                _constructor = SelectConstructor(destinationType);
                _constructorArguments = _constructor?.GetParameters()
                    .Select(parameter => parameter.HasDefaultValue
                        ? parameter.DefaultValue
                        : DefaultOf(parameter.ParameterType))
                    .ToArray();

                var ignored = registry.GetIgnoredProperties(sourceType, destinationType);

                var sourceProperties = sourceType
                    .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                    .Where(property => property.GetIndexParameters().Length == 0)
                    .Where(property => property.GetGetMethod() != null)
                    .ToDictionary(property => property.Name, StringComparer.OrdinalIgnoreCase);

                _bindings = destinationType
                    .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                    .Where(property => property.GetIndexParameters().Length == 0)
                    .Where(property => property.GetSetMethod() != null)
                    .Where(property => !AlwaysIgnored.Contains(property.Name))
                    .Where(property => !ignored.Contains(property.Name))
                    .Select(property => sourceProperties.TryGetValue(property.Name, out var match)
                        ? new PropertyBinding(match, property)
                        : null)
                    .Where(binding => binding != null)
                    .ToArray();

                MappedSourceMemberNames = _bindings
                    .Select(binding => binding.Source.Name)
                    .ToArray();
            }

            /// <summary>
            /// The source property names this plan copies.
            /// </summary>
            internal IReadOnlyCollection<string> MappedSourceMemberNames { get; }

            internal object Execute(object source, IKindeMapper mapper)
            {
                if (_constructor == null)
                {
                    throw new KindeMappingException(
                        $"Cannot map to '{_destinationType.FullName}': it exposes no public constructor the "
                        + "mapper can call. Register an explicit converter for this type in "
                        + nameof(KindeMappingRegistry) + ".");
                }

                object destination;
                try
                {
                    destination = _constructor.Invoke(_constructorArguments);
                }
                catch (TargetInvocationException exception)
                {
                    throw new KindeMappingException(
                        $"Cannot map to '{_destinationType.FullName}': its constructor rejected the default "
                        + "arguments the mapper supplied. Register an explicit converter for this type in "
                        + nameof(KindeMappingRegistry) + ".",
                        exception.InnerException ?? exception);
                }

                foreach (var binding in _bindings)
                {
                    var value = binding.Source.GetValue(source);
                    var mapped = value == null ? null : mapper.Map(value, binding.DestinationType);

                    if (mapped == null && _skipNulls)
                    {
                        continue;
                    }

                    binding.Destination.SetValue(destination, mapped);
                }

                return destination;
            }

            /// <summary>
            /// Prefers a parameterless constructor, then the shortest one, filling its parameters
            /// with defaults. Every property the mapper cares about is then assigned directly, so
            /// the constructor only has to produce a usable instance.
            /// </summary>
            /// <remarks>
            /// Non-private constructors are included deliberately: several hand-authored
            /// <c>oneOf</c> wrappers in the SDK models expose only internal constructors, and
            /// AutoMapper used them too (its default constructor filter excludes only private
            /// ones). Narrowing this to public constructors would change those types quietly.
            /// </remarks>
            private static ConstructorInfo SelectConstructor(Type type)
            {
                return type
                    .GetConstructors(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
                    .Where(constructor => !constructor.IsPrivate)
                    .OrderBy(constructor => constructor.GetParameters().Length)
                    .FirstOrDefault();
            }

            private static object DefaultOf(Type type)
            {
                return type.IsValueType && Nullable.GetUnderlyingType(type) == null
                    ? Activator.CreateInstance(type)
                    : null;
            }
        }

        private sealed class PropertyBinding
        {
            internal PropertyBinding(PropertyInfo source, PropertyInfo destination)
            {
                Source = source;
                Destination = destination;
                DestinationType = destination.PropertyType;
            }

            internal PropertyInfo Source { get; }

            internal PropertyInfo Destination { get; }

            internal Type DestinationType { get; }
        }
    }
}
