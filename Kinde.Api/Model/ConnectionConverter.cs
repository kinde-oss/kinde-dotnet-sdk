/*
 * Custom JsonConverter for Connection to fix deserialization when the API returns
 * a flat payload (ConnectionConnection shape) per list item instead of the wrapped
 * shape (code, message, connection). See: GetConnections() returning null properties
 * on .NET 10 when the payload matches ConnectionConnection.
 */

using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Kinde.Api.Model
{
    /// <summary>
    /// Deserializes Connection from either: (1) wrapped shape { code, message, connection },
    /// or (2) flat shape { id, name, display_name, strategy } (ConnectionConnection).
    /// </summary>
    public class ConnectionConverter : JsonConverter
    {
        /// <inheritdoc />
        public override bool CanConvert(Type objectType) => objectType == typeof(Connection);

        /// <inheritdoc />
        public override bool CanWrite => true;

        /// <inheritdoc />
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null)
                return null;

            var obj = JObject.Load(reader);

            // Wrapped shape: has "connection" or "varConnection" property
            var connectionToken = obj["connection"] ?? obj["varConnection"];
            if (connectionToken != null && connectionToken.Type == JTokenType.Object)
            {
                var code = obj["code"]?.ToString();
                var message = obj["message"]?.ToString();
                var varConnection = connectionToken.ToObject<ConnectionConnection>(serializer);
                return new Connection(code, message, varConnection);
            }

            // Flat shape: payload is ConnectionConnection (id, name, display_name, strategy at top level)
            if (obj["id"] != null || obj["name"] != null || obj["display_name"] != null || obj["strategy"] != null)
            {
                var varConnection = obj.ToObject<ConnectionConnection>(serializer);
                return new Connection(null, null, varConnection);
            }

            return new Connection(null, null, null);
        }

        /// <inheritdoc />
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var connection = (Connection)value;
            writer.WriteStartObject();
            if (connection.Code != null) { writer.WritePropertyName("code"); writer.WriteValue(connection.Code); }
            if (connection.Message != null) { writer.WritePropertyName("message"); writer.WriteValue(connection.Message); }
            if (connection.VarConnection != null)
            {
                writer.WritePropertyName("connection");
                serializer.Serialize(writer, connection.VarConnection);
            }
            writer.WriteEndObject();
        }
    }
}
