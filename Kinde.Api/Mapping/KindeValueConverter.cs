using System;
using System.Collections.Concurrent;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;

namespace Kinde.Api.Mapping
{
    /// <summary>
    /// Converts scalars: strings, primitives, enums and dates. Collections and complex types
    /// are handled by <see cref="KindeMapper"/>.
    /// </summary>
    internal static class KindeValueConverter
    {
        private static readonly ConcurrentDictionary<Type, string[]> WireNames =
            new ConcurrentDictionary<Type, string[]>();

        /// <summary>True for types converted directly rather than walked property by property.</summary>
        internal static bool IsSimple(Type type)
        {
            var underlying = Nullable.GetUnderlyingType(type) ?? type;

            return underlying.IsPrimitive
                || underlying.IsEnum
                || underlying == typeof(string)
                || underlying == typeof(decimal)
                || underlying == typeof(DateTime)
                || underlying == typeof(DateTimeOffset)
                || underlying == typeof(DateOnly)
                || underlying == typeof(TimeOnly)
                || underlying == typeof(TimeSpan)
                || underlying == typeof(Guid)
                || underlying == typeof(Uri);
        }

        /// <summary>Converts a value, returning false when the pair needs the object mapper.</summary>
        internal static bool TryConvert(object source, Type destinationType, out object result)
        {
            result = null;
            if (source == null)
            {
                return true;
            }

            var sourceType = source.GetType();
            var target = Nullable.GetUnderlyingType(destinationType) ?? destinationType;

            if (!IsSimple(destinationType) && !IsSimple(sourceType))
            {
                return false;
            }

            if (target.IsInstanceOfType(source) && target == sourceType)
            {
                result = source;
                return true;
            }

            if (target.IsEnum)
            {
                return TryConvertToEnum(source, sourceType, target, out result);
            }

            if (sourceType.IsEnum)
            {
                return TryConvertFromEnum(source, target, out result);
            }

            if (target == typeof(string))
            {
                result = source is IFormattable formattable
                    ? formattable.ToString(null, CultureInfo.InvariantCulture)
                    : source.ToString();
                return true;
            }

            if (TryConvertTemporal(source, target, out result))
            {
                return true;
            }

            if (target == typeof(Guid))
            {
                if (source is string guidText && Guid.TryParse(guidText, out var guid))
                {
                    result = guid;
                    return true;
                }

                return false;
            }

            if (target == typeof(Uri))
            {
                if (source is string uriText
                    && Uri.TryCreate(uriText, UriKind.RelativeOrAbsolute, out var uri))
                {
                    result = uri;
                    return true;
                }

                return false;
            }

            if (source is IConvertible && (target.IsPrimitive || target == typeof(decimal)))
            {
                try
                {
                    result = Convert.ChangeType(source, target, CultureInfo.InvariantCulture);
                    return true;
                }
                catch (Exception exception) when (
                    exception is InvalidCastException
                    || exception is FormatException
                    || exception is OverflowException)
                {
                    return false;
                }
            }

            return false;
        }

        /// <summary>
        /// Matches enums by member name, then by <see cref="EnumMemberAttribute"/> wire value,
        /// because the generators spell the same constant differently.
        /// </summary>
        private static bool TryConvertToEnum(object source, Type sourceType, Type target, out object result)
        {
            result = null;

            string candidate = null;
            if (source is string text)
            {
                candidate = text;
            }
            else if (sourceType.IsEnum)
            {
                candidate = source.ToString();
            }

            if (candidate != null)
            {
                if (TryParseName(target, candidate, out result))
                {
                    return true;
                }

                if (TryParseWireName(target, candidate, out result))
                {
                    return true;
                }
            }

            if (sourceType.IsEnum)
            {
                var wire = GetWireName((Enum)source);
                if (wire != null && TryParseWireName(target, wire, out result))
                {
                    return true;
                }

                return false;
            }

            if (source is IConvertible)
            {
                try
                {
                    var numeric = Convert.ToInt64(source, CultureInfo.InvariantCulture);
                    var boxed = Enum.ToObject(target, numeric);
                    if (Enum.IsDefined(target, boxed))
                    {
                        result = boxed;
                        return true;
                    }
                }
                catch (Exception exception) when (
                    exception is InvalidCastException
                    || exception is FormatException
                    || exception is OverflowException)
                {
                    return false;
                }
            }

            return false;
        }

        private static bool TryConvertFromEnum(object source, Type target, out object result)
        {
            result = null;

            if (target == typeof(string))
            {
                result = source.ToString();
                return true;
            }

            if (target.IsPrimitive)
            {
                try
                {
                    result = Convert.ChangeType(source, target, CultureInfo.InvariantCulture);
                    return true;
                }
                catch (Exception exception) when (
                    exception is InvalidCastException || exception is OverflowException)
                {
                    return false;
                }
            }

            return false;
        }

        private static bool TryConvertTemporal(object source, Type target, out object result)
        {
            result = null;

            switch (source)
            {
                case DateTimeOffset offset when target == typeof(DateTime):
                    result = offset.DateTime;
                    return true;
                case DateTime dateTime when target == typeof(DateTimeOffset):
                    result = dateTime.Kind == DateTimeKind.Unspecified
                        ? new DateTimeOffset(DateTime.SpecifyKind(dateTime, DateTimeKind.Utc))
                        : new DateTimeOffset(dateTime);
                    return true;
                case DateOnly dateOnly when target == typeof(DateTime):
                    result = dateOnly.ToDateTime(TimeOnly.MinValue);
                    return true;
                case DateTime fromDateTime when target == typeof(DateOnly):
                    result = DateOnly.FromDateTime(fromDateTime);
                    return true;
                case DateTimeOffset fromOffset when target == typeof(DateOnly):
                    result = DateOnly.FromDateTime(fromOffset.DateTime);
                    return true;
            }

            if (source is string temporalText)
            {
                if (target == typeof(DateTime)
                    && DateTime.TryParse(
                        temporalText, CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind, out var parsedDateTime))
                {
                    result = parsedDateTime;
                    return true;
                }

                if (target == typeof(DateTimeOffset)
                    && DateTimeOffset.TryParse(
                        temporalText, CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind, out var parsedOffset))
                {
                    result = parsedOffset;
                    return true;
                }

                if (target == typeof(DateOnly)
                    && DateOnly.TryParse(
                        temporalText, CultureInfo.InvariantCulture, DateTimeStyles.None, out var parsedDate))
                {
                    result = parsedDate;
                    return true;
                }
            }

            return false;
        }

        private static bool TryParseName(Type enumType, string name, out object result)
        {
            foreach (var candidate in Enum.GetNames(enumType))
            {
                if (string.Equals(candidate, name, StringComparison.OrdinalIgnoreCase))
                {
                    result = Enum.Parse(enumType, candidate);
                    return true;
                }
            }

            result = null;
            return false;
        }

        private static bool TryParseWireName(Type enumType, string wire, out object result)
        {
            var names = Enum.GetNames(enumType);
            var wireNames = WireNames.GetOrAdd(enumType, type => Enum.GetNames(type)
                .Select(name => type.GetMember(name)
                    .FirstOrDefault()
                    ?.GetCustomAttribute<EnumMemberAttribute>()
                    ?.Value)
                .ToArray());

            for (var index = 0; index < wireNames.Length; index++)
            {
                if (wireNames[index] != null
                    && string.Equals(wireNames[index], wire, StringComparison.OrdinalIgnoreCase))
                {
                    result = Enum.Parse(enumType, names[index]);
                    return true;
                }
            }

            result = null;
            return false;
        }

        /// <summary>The <see cref="EnumMemberAttribute"/> value for a member, or null.</summary>
        internal static string GetWireName(Enum value)
        {
            return value.GetType()
                .GetMember(value.ToString())
                .FirstOrDefault()
                ?.GetCustomAttribute<EnumMemberAttribute>()
                ?.Value;
        }
    }
}
