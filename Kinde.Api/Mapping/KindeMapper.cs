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
    /// Maps between the SDK and Kiota models by matching property names, recursing into nested
    /// models and collections. Pairs that need bespoke handling come from the registry.
    /// Plans are built once per type pair and cached.
    /// </summary>
    internal sealed class KindeMapper : IKindeMapper
    {
        /// <summary>Kiota plumbing members, which have no counterpart on the SDK side.</summary>
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

        /// <summary>
        /// How deep the mapper will recurse. The generated models nest a handful of levels; this
        /// only bounds a graph that refers back to itself, which would otherwise overflow the
        /// stack and take the process with it.
        /// </summary>
        private const int MaxDepth = 64;

        [ThreadStatic]
        private static int _depth;

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

            if (_depth >= MaxDepth)
            {
                throw new KindeMappingException(
                    $"Mapping to '{destinationType.FullName}' exceeded {MaxDepth} levels. The "
                    + "source graph most likely refers back to itself.");
            }

            _depth++;
            try
            {
                return MapCore(source, destinationType);
            }
            finally
            {
                _depth--;
            }
        }

        private object MapCore(object source, Type destinationType)
        {
            var sourceType = source.GetType();

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
        /// Maps by property name, ignoring any converter registered for the pair, so a converter
        /// can map the conventional members and then adjust only what differs.
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

        /// <summary>The source property names this pair copies. Asserted on by the tests.</summary>
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
            if (destinationArguments == null || !TryGetEntries(source, out var entries))
            {
                return false;
            }

            var keyType = destinationArguments[0];
            var valueType = destinationArguments[1];
            var concrete = typeof(Dictionary<,>).MakeGenericType(keyType, valueType);
            var target = (IDictionary)Activator.CreateInstance(concrete);

            foreach (var entry in entries)
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

        /// <summary>Reads the entries of a dictionary source, generic or not.</summary>
        private static bool TryGetEntries(object source, out IEnumerable<KeyValuePair<object, object>> entries)
        {
            if (source is IDictionary dictionary)
            {
                entries = dictionary.Cast<DictionaryEntry>()
                    .Select(entry => new KeyValuePair<object, object>(entry.Key, entry.Value));
                return true;
            }

            var arguments = source.GetType().GetInterfaces()
                .Where(candidate => candidate.IsGenericType
                    && (candidate.GetGenericTypeDefinition() == typeof(IDictionary<,>)
                        || candidate.GetGenericTypeDefinition() == typeof(IReadOnlyDictionary<,>)))
                .Select(candidate => candidate.GetGenericArguments())
                .FirstOrDefault();

            if (arguments == null)
            {
                entries = null;
                return false;
            }

            var pairType = typeof(KeyValuePair<,>).MakeGenericType(arguments);
            var keyProperty = pairType.GetProperty(nameof(KeyValuePair<object, object>.Key));
            var valueProperty = pairType.GetProperty(nameof(KeyValuePair<object, object>.Value));

            entries = ((IEnumerable)source).Cast<object>()
                .Select(pair => new KeyValuePair<object, object>(
                    keyProperty.GetValue(pair),
                    valueProperty.GetValue(pair)));
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

        /// <summary>The cached per-pair plan: how to build the destination and what to copy.</summary>
        private sealed class MappingPlan
        {
            private readonly Type _destinationType;
            private readonly ConstructorInfo _constructor;
            private readonly object[] _constructorArguments;
            private readonly PropertyBinding[] _bindings;

            /// <summary>
            /// True for a Kiota destination, where assigning null erases a property that carries a
            /// schema default, dropping it from the payload instead of sending the default the
            /// caller relied on.
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

            /// <summary>The source property names this plan copies.</summary>
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
            /// Picks the shortest constructor and fills it with defaults; properties are assigned
            /// afterwards. Non-private ones are included because some oneOf wrappers only have
            /// internal constructors.
            /// </summary>
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
