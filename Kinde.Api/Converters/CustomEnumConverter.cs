using System.Text.Json;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
using Kinde.Api.Model;
using System.Reflection;

namespace Kinde.Api.Converters
{
    /// <summary>
    /// Custom JSON converter for CreateUserRequestIdentitiesInner.TypeEnum that uses the SDK's TypeEnumToJsonValue method
    /// </summary>
    public class CustomEnumConverter : System.Text.Json.Serialization.JsonConverter<CreateUserRequestIdentitiesInner.TypeEnum>
    {
        public override CreateUserRequestIdentitiesInner.TypeEnum Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType != JsonTokenType.String)
            {
                throw new System.Text.Json.JsonException($"Unexpected token type: {reader.TokenType}");
            }

            string? stringValue = reader.GetString();
            if (stringValue == null)
            {
                throw new System.Text.Json.JsonException("String value is null");
            }

            return CreateUserRequestIdentitiesInner.TypeEnumFromString(stringValue);
        }

        public override void Write(Utf8JsonWriter writer, CreateUserRequestIdentitiesInner.TypeEnum value, JsonSerializerOptions options)
        {
            string jsonValue = CreateUserRequestIdentitiesInner.TypeEnumToJsonValue(value);
            writer.WriteStringValue(jsonValue);
        }
    }

    /// <summary>
    /// Generic JSON converter for enum types that have TypeEnumToJsonValue and TypeEnumFromString methods
    /// This converter automatically handles enum serialization/deserialization using the SDK's conversion methods
    /// </summary>
    public class GenericEnumConverter : System.Text.Json.Serialization.JsonConverterFactory
    {
        public override bool CanConvert(Type typeToConvert)
        {
            // Check if it's an enum type
            if (!typeToConvert.IsEnum)
                return false;

            // Check if the enum is nullable
            var underlyingType = Nullable.GetUnderlyingType(typeToConvert);
            var enumType = underlyingType ?? typeToConvert;

            // Get the declaring type (the class that contains this enum)
            var declaringType = enumType.DeclaringType;
            if (declaringType == null)
                return false;

            // Check if the declaring type has the required conversion methods
            var hasToJsonValueMethod = declaringType.GetMethod("TypeEnumToJsonValue", BindingFlags.Public | BindingFlags.Static) != null;
            var hasFromStringMethod = declaringType.GetMethod("TypeEnumFromString", BindingFlags.Public | BindingFlags.Static) != null;

            return hasToJsonValueMethod && hasFromStringMethod;
        }

        public override System.Text.Json.Serialization.JsonConverter CreateConverter(Type typeToConvert, JsonSerializerOptions options)
        {
            var underlyingType = Nullable.GetUnderlyingType(typeToConvert);
            var enumType = underlyingType ?? typeToConvert;
            var declaringType = enumType.DeclaringType!;

            // Create a generic converter for this specific enum type
            var converterType = typeof(GenericEnumConverter<>).MakeGenericType(enumType);
            return (System.Text.Json.Serialization.JsonConverter)Activator.CreateInstance(converterType, declaringType)!;
        }
    }

    /// <summary>
    /// Generic enum converter implementation
    /// </summary>
    public class GenericEnumConverter<T> : System.Text.Json.Serialization.JsonConverter<T> where T : struct, Enum
    {
        private readonly Type _declaringType;
        private readonly MethodInfo _toJsonValueMethod;
        private readonly MethodInfo _fromStringMethod;

        public GenericEnumConverter(Type declaringType)
        {
            _declaringType = declaringType;
            _toJsonValueMethod = declaringType.GetMethod("TypeEnumToJsonValue", BindingFlags.Public | BindingFlags.Static)!;
            _fromStringMethod = declaringType.GetMethod("TypeEnumFromString", BindingFlags.Public | BindingFlags.Static)!;
        }

        public override T Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType != JsonTokenType.String)
            {
                throw new System.Text.Json.JsonException($"Unexpected token type: {reader.TokenType}");
            }

            string? stringValue = reader.GetString();
            if (stringValue == null)
            {
                throw new System.Text.Json.JsonException("String value is null");
            }

            // Call the TypeEnumFromString method
            var result = _fromStringMethod.Invoke(null, new object[] { stringValue });
            return (T)result!;
        }

        public override void Write(Utf8JsonWriter writer, T value, JsonSerializerOptions options)
        {
            // Call the TypeEnumToJsonValue method
            var jsonValue = _toJsonValueMethod.Invoke(null, new object[] { value });
            writer.WriteStringValue((string)jsonValue!);
        }
    }

    /// <summary>
    /// Newtonsoft.Json converter for enums that uses the SDK's TypeEnumToJsonValue and TypeEnumFromString methods
    /// This is used by the OpenAPI client's CustomJsonCodec for actual HTTP requests
    /// </summary>
    public class NewtonsoftGenericEnumConverter : Newtonsoft.Json.JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            // Check if this is an enum type
            if (!objectType.IsEnum)
                return false;

            // Check if the containing class has the required methods
            var containingType = objectType.DeclaringType;
            if (containingType == null)
                return false;

            var toJsonMethod = containingType.GetMethod("TypeEnumToJsonValue", new[] { objectType });
            var fromStringMethod = containingType.GetMethod("TypeEnumFromString", new[] { typeof(string) });

            return toJsonMethod != null && fromStringMethod != null;
        }

        public override object ReadJson(Newtonsoft.Json.JsonReader reader, Type objectType, object existingValue, Newtonsoft.Json.JsonSerializer serializer)
        {
            if (reader.TokenType != Newtonsoft.Json.JsonToken.String)
            {
                throw new Newtonsoft.Json.JsonException($"Unexpected token type: {reader.TokenType}");
            }

            string? stringValue = reader.Value?.ToString();
            if (stringValue == null)
            {
                throw new Newtonsoft.Json.JsonException("String value cannot be null");
            }

            var containingType = objectType.DeclaringType;
            if (containingType == null)
            {
                throw new Newtonsoft.Json.JsonException($"Cannot find containing type for enum {objectType.Name}");
            }

            var fromStringMethod = containingType.GetMethod("TypeEnumFromString", new[] { typeof(string) });
            if (fromStringMethod == null)
            {
                throw new Newtonsoft.Json.JsonException($"TypeEnumFromString method not found on {containingType.Name}");
            }

            return fromStringMethod.Invoke(null, new object[] { stringValue })!;
        }

        public override void WriteJson(Newtonsoft.Json.JsonWriter writer, object value, Newtonsoft.Json.JsonSerializer serializer)
        {
            var objectType = value.GetType();
            var containingType = objectType.DeclaringType;
            if (containingType == null)
            {
                throw new Newtonsoft.Json.JsonException($"Cannot find containing type for enum {objectType.Name}");
            }

            var toJsonMethod = containingType.GetMethod("TypeEnumToJsonValue", new[] { objectType });
            if (toJsonMethod == null)
            {
                throw new Newtonsoft.Json.JsonException($"TypeEnumToJsonValue method not found on {containingType.Name}");
            }

            string jsonValue = (string)toJsonMethod.Invoke(null, new object[] { value })!;
            writer.WriteValue(jsonValue);
        }
    }
}
