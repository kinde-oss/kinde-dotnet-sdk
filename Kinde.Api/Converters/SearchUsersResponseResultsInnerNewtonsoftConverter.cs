using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Kinde.Api.Model;
using Kinde.Api.Client;

namespace Kinde.Api.Converters
{
    /// <summary>
    /// Newtonsoft.Json converter for SearchUsersResponseResultsInner that handles the Option<> structure
    /// </summary>
    public class SearchUsersResponseResultsInnerNewtonsoftConverter : Newtonsoft.Json.JsonConverter<SearchUsersResponseResultsInner>
    {
        public override bool CanRead => true;
        public override bool CanWrite => true;

        public override SearchUsersResponseResultsInner ReadJson(Newtonsoft.Json.JsonReader reader, Type objectType, SearchUsersResponseResultsInner existingValue, bool hasExistingValue, Newtonsoft.Json.JsonSerializer serializer)
        {
            if (reader.TokenType != Newtonsoft.Json.JsonToken.StartObject)
            {
                throw new Newtonsoft.Json.JsonException($"Expected StartObject, got {reader.TokenType}");
            }

            var jsonObject = JObject.Load(reader);

            string? id = default(string?);
            if (jsonObject["id"] != null)
            {
                id = jsonObject["id"].ToObject<string?>();
            }
            string? providedId = default(string?);
            if (jsonObject["provided_id"] != null)
            {
                providedId = jsonObject["provided_id"].ToObject<string?>();
            }
            string? email = default(string?);
            if (jsonObject["email"] != null)
            {
                email = jsonObject["email"].ToObject<string?>();
            }
            string? username = default(string?);
            if (jsonObject["username"] != null)
            {
                username = jsonObject["username"].ToObject<string?>();
            }
            string? lastName = default(string?);
            if (jsonObject["last_name"] != null)
            {
                lastName = jsonObject["last_name"].ToObject<string?>();
            }
            string? firstName = default(string?);
            if (jsonObject["first_name"] != null)
            {
                firstName = jsonObject["first_name"].ToObject<string?>();
            }
            bool? isSuspended = default(bool?);
            if (jsonObject["is_suspended"] != null)
            {
                isSuspended = jsonObject["is_suspended"].ToObject<bool?>(serializer);
            }
            string? picture = default(string?);
            if (jsonObject["picture"] != null)
            {
                picture = jsonObject["picture"].ToObject<string?>();
            }
            int? totalSignIns = default(int?);
            if (jsonObject["total_sign_ins"] != null)
            {
                totalSignIns = jsonObject["total_sign_ins"].ToObject<int?>(serializer);
            }
            int? failedSignIns = default(int?);
            if (jsonObject["failed_sign_ins"] != null)
            {
                failedSignIns = jsonObject["failed_sign_ins"].ToObject<int?>(serializer);
            }
            string? lastSignedIn = default(string?);
            if (jsonObject["last_signed_in"] != null)
            {
                lastSignedIn = jsonObject["last_signed_in"].ToObject<string?>();
            }
            string? createdOn = default(string?);
            if (jsonObject["created_on"] != null)
            {
                createdOn = jsonObject["created_on"].ToObject<string?>();
            }
            List<string> organizations = default(List<string>);
            if (jsonObject["organizations"] != null)
            {
                organizations = jsonObject["organizations"].ToObject<List<string>>(serializer);
            }
            List<UserIdentitiesInner> identities = default(List<UserIdentitiesInner>);
            if (jsonObject["identities"] != null)
            {
                identities = jsonObject["identities"].ToObject<List<UserIdentitiesInner>>(serializer);
            }
            Dictionary<string, string> properties = default(Dictionary<string, string>);
            if (jsonObject["properties"] != null)
            {
                properties = jsonObject["properties"].ToObject<Dictionary<string, string>>(serializer);
            }
            List<SearchUsersResponseResultsInnerApiScopesInner> apiScopes = default(List<SearchUsersResponseResultsInnerApiScopesInner>);
            if (jsonObject["api_scopes"] != null)
            {
                apiScopes = jsonObject["api_scopes"].ToObject<List<SearchUsersResponseResultsInnerApiScopesInner>>(serializer);
            }

            return new SearchUsersResponseResultsInner(
                id: id != null ? new Option<string?>(id) : default,                 providedId: providedId != null ? new Option<string?>(providedId) : default,                 email: email != null ? new Option<string?>(email) : default,                 username: username != null ? new Option<string?>(username) : default,                 lastName: lastName != null ? new Option<string?>(lastName) : default,                 firstName: firstName != null ? new Option<string?>(firstName) : default,                 isSuspended: isSuspended != null ? new Option<bool?>(isSuspended) : default,                 picture: picture != null ? new Option<string?>(picture) : default,                 totalSignIns: totalSignIns != null ? new Option<int?>(totalSignIns) : default,                 failedSignIns: failedSignIns != null ? new Option<int?>(failedSignIns) : default,                 lastSignedIn: lastSignedIn != null ? new Option<string?>(lastSignedIn) : default,                 createdOn: createdOn != null ? new Option<string?>(createdOn) : default,                 organizations: organizations != null ? new Option<List<string>?>(organizations) : default,                 identities: identities != null ? new Option<List<UserIdentitiesInner>?>(identities) : default,                 properties: properties != null ? new Option<Dictionary<string, string>>(properties) : default,                 apiScopes: apiScopes != null ? new Option<List<SearchUsersResponseResultsInnerApiScopesInner>?>(apiScopes) : default            );
        }

        public override void WriteJson(Newtonsoft.Json.JsonWriter writer, SearchUsersResponseResultsInner value, Newtonsoft.Json.JsonSerializer serializer)
        {
            writer.WriteStartObject();

            if (value.IdOption.IsSet && value.Id != null)
            {
                writer.WritePropertyName("id");
                serializer.Serialize(writer, value.Id);
            }
            if (value.ProvidedIdOption.IsSet && value.ProvidedId != null)
            {
                writer.WritePropertyName("provided_id");
                serializer.Serialize(writer, value.ProvidedId);
            }
            if (value.EmailOption.IsSet && value.Email != null)
            {
                writer.WritePropertyName("email");
                serializer.Serialize(writer, value.Email);
            }
            if (value.UsernameOption.IsSet && value.Username != null)
            {
                writer.WritePropertyName("username");
                serializer.Serialize(writer, value.Username);
            }
            if (value.LastNameOption.IsSet && value.LastName != null)
            {
                writer.WritePropertyName("last_name");
                serializer.Serialize(writer, value.LastName);
            }
            if (value.FirstNameOption.IsSet && value.FirstName != null)
            {
                writer.WritePropertyName("first_name");
                serializer.Serialize(writer, value.FirstName);
            }
            if (value.IsSuspendedOption.IsSet && value.IsSuspended != null)
            {
                writer.WritePropertyName("is_suspended");
                serializer.Serialize(writer, value.IsSuspended);
            }
            if (value.PictureOption.IsSet && value.Picture != null)
            {
                writer.WritePropertyName("picture");
                serializer.Serialize(writer, value.Picture);
            }
            if (value.TotalSignInsOption.IsSet && value.TotalSignIns != null)
            {
                writer.WritePropertyName("total_sign_ins");
                serializer.Serialize(writer, value.TotalSignIns);
            }
            if (value.FailedSignInsOption.IsSet && value.FailedSignIns != null)
            {
                writer.WritePropertyName("failed_sign_ins");
                serializer.Serialize(writer, value.FailedSignIns);
            }
            if (value.LastSignedInOption.IsSet && value.LastSignedIn != null)
            {
                writer.WritePropertyName("last_signed_in");
                serializer.Serialize(writer, value.LastSignedIn);
            }
            if (value.CreatedOnOption.IsSet && value.CreatedOn != null)
            {
                writer.WritePropertyName("created_on");
                serializer.Serialize(writer, value.CreatedOn);
            }
            if (value.OrganizationsOption.IsSet)
            {
                writer.WritePropertyName("organizations");
                serializer.Serialize(writer, value.Organizations);
            }
            if (value.IdentitiesOption.IsSet)
            {
                writer.WritePropertyName("identities");
                serializer.Serialize(writer, value.Identities);
            }
            if (value.PropertiesOption.IsSet)
            {
                writer.WritePropertyName("properties");
                serializer.Serialize(writer, value.Properties);
            }
            if (value.ApiScopesOption.IsSet)
            {
                writer.WritePropertyName("api_scopes");
                serializer.Serialize(writer, value.ApiScopes);
            }

            writer.WriteEndObject();
        }
    }
}