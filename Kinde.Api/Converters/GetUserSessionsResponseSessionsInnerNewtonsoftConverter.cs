using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Kinde.Api.Model;
using Kinde.Api.Client;

namespace Kinde.Api.Converters
{
    /// <summary>
    /// Newtonsoft.Json converter for GetUserSessionsResponseSessionsInner that handles the Option<> structure
    /// </summary>
    public class GetUserSessionsResponseSessionsInnerNewtonsoftConverter : Newtonsoft.Json.JsonConverter<GetUserSessionsResponseSessionsInner>
    {
        public override bool CanRead => true;
        public override bool CanWrite => true;

        public override GetUserSessionsResponseSessionsInner ReadJson(Newtonsoft.Json.JsonReader reader, Type objectType, GetUserSessionsResponseSessionsInner existingValue, bool hasExistingValue, Newtonsoft.Json.JsonSerializer serializer)
        {
            if (reader.TokenType != Newtonsoft.Json.JsonToken.StartObject)
            {
                throw new Newtonsoft.Json.JsonException($"Expected StartObject, got {reader.TokenType}");
            }

            var jsonObject = JObject.Load(reader);

            string? userId = default(string?);
            if (jsonObject["user_id"] != null)
            {
                userId = jsonObject["user_id"].ToObject<string?>();
            }
            string? orgCode = default(string?);
            if (jsonObject["org_code"] != null)
            {
                orgCode = jsonObject["org_code"].ToObject<string?>();
            }
            string? clientId = default(string?);
            if (jsonObject["client_id"] != null)
            {
                clientId = jsonObject["client_id"].ToObject<string?>();
            }
            DateTimeOffset? expiresOn = default(DateTimeOffset?);
            if (jsonObject["expires_on"] != null)
            {
                expiresOn = jsonObject["expires_on"].ToObject<DateTimeOffset?>(serializer);
            }
            string? sessionId = default(string?);
            if (jsonObject["session_id"] != null)
            {
                sessionId = jsonObject["session_id"].ToObject<string?>();
            }
            DateTimeOffset? startedOn = default(DateTimeOffset?);
            if (jsonObject["started_on"] != null)
            {
                startedOn = jsonObject["started_on"].ToObject<DateTimeOffset?>(serializer);
            }
            DateTimeOffset? updatedOn = default(DateTimeOffset?);
            if (jsonObject["updated_on"] != null)
            {
                updatedOn = jsonObject["updated_on"].ToObject<DateTimeOffset?>(serializer);
            }
            string? connectionId = default(string?);
            if (jsonObject["connection_id"] != null)
            {
                connectionId = jsonObject["connection_id"].ToObject<string?>();
            }
            string? lastIpAddress = default(string?);
            if (jsonObject["last_ip_address"] != null)
            {
                lastIpAddress = jsonObject["last_ip_address"].ToObject<string?>();
            }
            string? lastUserAgent = default(string?);
            if (jsonObject["last_user_agent"] != null)
            {
                lastUserAgent = jsonObject["last_user_agent"].ToObject<string?>();
            }
            string? initialIpAddress = default(string?);
            if (jsonObject["initial_ip_address"] != null)
            {
                initialIpAddress = jsonObject["initial_ip_address"].ToObject<string?>();
            }
            string? initialUserAgent = default(string?);
            if (jsonObject["initial_user_agent"] != null)
            {
                initialUserAgent = jsonObject["initial_user_agent"].ToObject<string?>();
            }

            return new GetUserSessionsResponseSessionsInner(
                userId: userId != null ? new Option<string?>(userId) : default,                 orgCode: orgCode != null ? new Option<string?>(orgCode) : default,                 clientId: clientId != null ? new Option<string?>(clientId) : default,                 expiresOn: expiresOn != null ? new Option<DateTimeOffset?>(expiresOn) : default,                 sessionId: sessionId != null ? new Option<string?>(sessionId) : default,                 startedOn: startedOn != null ? new Option<DateTimeOffset?>(startedOn) : default,                 updatedOn: updatedOn != null ? new Option<DateTimeOffset?>(updatedOn) : default,                 connectionId: connectionId != null ? new Option<string?>(connectionId) : default,                 lastIpAddress: lastIpAddress != null ? new Option<string?>(lastIpAddress) : default,                 lastUserAgent: lastUserAgent != null ? new Option<string?>(lastUserAgent) : default,                 initialIpAddress: initialIpAddress != null ? new Option<string?>(initialIpAddress) : default,                 initialUserAgent: initialUserAgent != null ? new Option<string?>(initialUserAgent) : default            );
        }

        public override void WriteJson(Newtonsoft.Json.JsonWriter writer, GetUserSessionsResponseSessionsInner value, Newtonsoft.Json.JsonSerializer serializer)
        {
            writer.WriteStartObject();

            if (value.UserIdOption.IsSet && value.UserId != null)
            {
                writer.WritePropertyName("user_id");
                serializer.Serialize(writer, value.UserId);
            }
            if (value.OrgCodeOption.IsSet && value.OrgCode != null)
            {
                writer.WritePropertyName("org_code");
                serializer.Serialize(writer, value.OrgCode);
            }
            if (value.ClientIdOption.IsSet && value.ClientId != null)
            {
                writer.WritePropertyName("client_id");
                serializer.Serialize(writer, value.ClientId);
            }
            if (value.ExpiresOnOption.IsSet && value.ExpiresOn != null)
            {
                writer.WritePropertyName("expires_on");
                serializer.Serialize(writer, value.ExpiresOn);
            }
            if (value.SessionIdOption.IsSet && value.SessionId != null)
            {
                writer.WritePropertyName("session_id");
                serializer.Serialize(writer, value.SessionId);
            }
            if (value.StartedOnOption.IsSet && value.StartedOn != null)
            {
                writer.WritePropertyName("started_on");
                serializer.Serialize(writer, value.StartedOn);
            }
            if (value.UpdatedOnOption.IsSet && value.UpdatedOn != null)
            {
                writer.WritePropertyName("updated_on");
                serializer.Serialize(writer, value.UpdatedOn);
            }
            if (value.ConnectionIdOption.IsSet && value.ConnectionId != null)
            {
                writer.WritePropertyName("connection_id");
                serializer.Serialize(writer, value.ConnectionId);
            }
            if (value.LastIpAddressOption.IsSet && value.LastIpAddress != null)
            {
                writer.WritePropertyName("last_ip_address");
                serializer.Serialize(writer, value.LastIpAddress);
            }
            if (value.LastUserAgentOption.IsSet && value.LastUserAgent != null)
            {
                writer.WritePropertyName("last_user_agent");
                serializer.Serialize(writer, value.LastUserAgent);
            }
            if (value.InitialIpAddressOption.IsSet && value.InitialIpAddress != null)
            {
                writer.WritePropertyName("initial_ip_address");
                serializer.Serialize(writer, value.InitialIpAddress);
            }
            if (value.InitialUserAgentOption.IsSet && value.InitialUserAgent != null)
            {
                writer.WritePropertyName("initial_user_agent");
                serializer.Serialize(writer, value.InitialUserAgent);
            }

            writer.WriteEndObject();
        }
    }
}