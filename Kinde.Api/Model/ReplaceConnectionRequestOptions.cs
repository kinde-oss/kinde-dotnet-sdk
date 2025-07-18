/*
 * Kinde Management API
 *
 *  Provides endpoints to manage your Kinde Businesses.  ## Intro  ## How to use  1. [Set up and authorize a machine-to-machine (M2M) application](https://docs.kinde.com/developer-tools/kinde-api/connect-to-kinde-api/).  2. [Generate a test access token](https://docs.kinde.com/developer-tools/kinde-api/access-token-for-api/)  3. Test request any endpoint using the test token 
 *
 * The version of the OpenAPI document: 1
 * Contact: support@kinde.com
 * Generated by: https://github.com/openapitools/openapi-generator.git
 */


using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.IO;
using System.Runtime.Serialization;
using System.Text;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using OpenAPIDateConverter = Kinde.Api.Client.OpenAPIDateConverter;
using System.Reflection;

namespace Kinde.Api.Model
{
    /// <summary>
    /// </summary>
    [JsonConverter(typeof(ReplaceConnectionRequestOptionsJsonConverter))]
    [DataContract(Name = "ReplaceConnection_request_options")]
    public partial class ReplaceConnectionRequestOptions : AbstractOpenAPISchema, IEquatable<ReplaceConnectionRequestOptions>
    {
        /// <summary>
        /// </summary>
        public ReplaceConnectionRequestOptions(CreateConnectionRequestOptionsOneOf actualInstance)
        {
            this.IsNullable = false;
            this.SchemaType= "oneOf";
            this.ActualInstance = actualInstance ?? throw new ArgumentException("Invalid instance found. Must not be null.");
        }

        /// <summary>
        /// </summary>
        public ReplaceConnectionRequestOptions(ReplaceConnectionRequestOptionsOneOf actualInstance)
        {
            this.IsNullable = false;
            this.SchemaType= "oneOf";
            this.ActualInstance = actualInstance ?? throw new ArgumentException("Invalid instance found. Must not be null.");
        }

        /// <summary>
        /// </summary>
        public ReplaceConnectionRequestOptions(ReplaceConnectionRequestOptionsOneOf1 actualInstance)
        {
            this.IsNullable = false;
            this.SchemaType= "oneOf";
            this.ActualInstance = actualInstance ?? throw new ArgumentException("Invalid instance found. Must not be null.");
        }


        private Object _actualInstance;

        /// <summary>
        /// </summary>
        public override Object ActualInstance
        {
            get
            {
                return _actualInstance;
            }
            set
            {
                if (value.GetType() == typeof(CreateConnectionRequestOptionsOneOf))
                {
                    this._actualInstance = value;
                }
                else if (value.GetType() == typeof(ReplaceConnectionRequestOptionsOneOf))
                {
                    this._actualInstance = value;
                }
                else if (value.GetType() == typeof(ReplaceConnectionRequestOptionsOneOf1))
                {
                    this._actualInstance = value;
                }
                else
                {
                    throw new ArgumentException("Invalid instance found. Must be the following types: CreateConnectionRequestOptionsOneOf, ReplaceConnectionRequestOptionsOneOf, ReplaceConnectionRequestOptionsOneOf1");
                }
            }
        }

        /// <summary>
        /// </summary>
        public CreateConnectionRequestOptionsOneOf GetCreateConnectionRequestOptionsOneOf()
        {
            return (CreateConnectionRequestOptionsOneOf)this.ActualInstance;
        }

        /// <summary>
        /// </summary>
        public ReplaceConnectionRequestOptionsOneOf GetReplaceConnectionRequestOptionsOneOf()
        {
            return (ReplaceConnectionRequestOptionsOneOf)this.ActualInstance;
        }

        /// <summary>
        /// </summary>
        public ReplaceConnectionRequestOptionsOneOf1 GetReplaceConnectionRequestOptionsOneOf1()
        {
            return (ReplaceConnectionRequestOptionsOneOf1)this.ActualInstance;
        }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class ReplaceConnectionRequestOptions {\n");
            sb.Append("  ActualInstance: ").Append(this.ActualInstance).Append("\n");
            sb.Append("}\n");
            return sb.ToString();
        }

        /// <summary>
        /// Returns the JSON string presentation of the object
        /// </summary>
        /// <returns>JSON string presentation of the object</returns>
        public override string ToJson()
        {
            return JsonConvert.SerializeObject(this.ActualInstance, ReplaceConnectionRequestOptions.SerializerSettings);
        }

        /// <summary>
        /// </summary>
        /// <param name="jsonString">JSON string</param>
        public static ReplaceConnectionRequestOptions FromJson(string jsonString)
        {
            ReplaceConnectionRequestOptions newReplaceConnectionRequestOptions = null;

            if (string.IsNullOrEmpty(jsonString))
            {
                return newReplaceConnectionRequestOptions;
            }
            int match = 0;
            List<string> matchedTypes = new List<string>();

            try
            {
                // if it does not contains "AdditionalProperties", use SerializerSettings to deserialize
                if (typeof(CreateConnectionRequestOptionsOneOf).GetProperty("AdditionalProperties") == null)
                {
                    newReplaceConnectionRequestOptions = new ReplaceConnectionRequestOptions(JsonConvert.DeserializeObject<CreateConnectionRequestOptionsOneOf>(jsonString, ReplaceConnectionRequestOptions.SerializerSettings));
                }
                else
                {
                    newReplaceConnectionRequestOptions = new ReplaceConnectionRequestOptions(JsonConvert.DeserializeObject<CreateConnectionRequestOptionsOneOf>(jsonString, ReplaceConnectionRequestOptions.AdditionalPropertiesSerializerSettings));
                }
                matchedTypes.Add("CreateConnectionRequestOptionsOneOf");
                match++;
            }
            catch (Exception exception)
            {
                // deserialization failed, try the next one
                System.Diagnostics.Debug.WriteLine(string.Format("Failed to deserialize `{0}` into CreateConnectionRequestOptionsOneOf: {1}", jsonString, exception.ToString()));
            }

            try
            {
                // if it does not contains "AdditionalProperties", use SerializerSettings to deserialize
                if (typeof(ReplaceConnectionRequestOptionsOneOf).GetProperty("AdditionalProperties") == null)
                {
                    newReplaceConnectionRequestOptions = new ReplaceConnectionRequestOptions(JsonConvert.DeserializeObject<ReplaceConnectionRequestOptionsOneOf>(jsonString, ReplaceConnectionRequestOptions.SerializerSettings));
                }
                else
                {
                    newReplaceConnectionRequestOptions = new ReplaceConnectionRequestOptions(JsonConvert.DeserializeObject<ReplaceConnectionRequestOptionsOneOf>(jsonString, ReplaceConnectionRequestOptions.AdditionalPropertiesSerializerSettings));
                }
                matchedTypes.Add("ReplaceConnectionRequestOptionsOneOf");
                match++;
            }
            catch (Exception exception)
            {
                // deserialization failed, try the next one
                System.Diagnostics.Debug.WriteLine(string.Format("Failed to deserialize `{0}` into ReplaceConnectionRequestOptionsOneOf: {1}", jsonString, exception.ToString()));
            }

            try
            {
                // if it does not contains "AdditionalProperties", use SerializerSettings to deserialize
                if (typeof(ReplaceConnectionRequestOptionsOneOf1).GetProperty("AdditionalProperties") == null)
                {
                    newReplaceConnectionRequestOptions = new ReplaceConnectionRequestOptions(JsonConvert.DeserializeObject<ReplaceConnectionRequestOptionsOneOf1>(jsonString, ReplaceConnectionRequestOptions.SerializerSettings));
                }
                else
                {
                    newReplaceConnectionRequestOptions = new ReplaceConnectionRequestOptions(JsonConvert.DeserializeObject<ReplaceConnectionRequestOptionsOneOf1>(jsonString, ReplaceConnectionRequestOptions.AdditionalPropertiesSerializerSettings));
                }
                matchedTypes.Add("ReplaceConnectionRequestOptionsOneOf1");
                match++;
            }
            catch (Exception exception)
            {
                // deserialization failed, try the next one
                System.Diagnostics.Debug.WriteLine(string.Format("Failed to deserialize `{0}` into ReplaceConnectionRequestOptionsOneOf1: {1}", jsonString, exception.ToString()));
            }

            if (match == 0)
            {
                throw new InvalidDataException("The JSON string `" + jsonString + "` cannot be deserialized into any schema defined.");
            }
            else if (match > 1)
            {
                throw new InvalidDataException("The JSON string `" + jsonString + "` incorrectly matches more than one schema (should be exactly one match): " + matchedTypes);
            }

            // deserialization is considered successful at this point if no exception has been thrown.
            return newReplaceConnectionRequestOptions;
        }

        /// <summary>
        /// Returns true if objects are equal
        /// </summary>
        /// <param name="input">Object to be compared</param>
        /// <returns>Boolean</returns>
        public override bool Equals(object input)
        {
            return this.Equals(input as ReplaceConnectionRequestOptions);
        }

        /// <summary>
        /// </summary>
        /// <returns>Boolean</returns>
        public bool Equals(ReplaceConnectionRequestOptions input)
        {
            if (input == null)
                return false;

            return this.ActualInstance.Equals(input.ActualInstance);
        }

        /// <summary>
        /// Gets the hash code
        /// </summary>
        /// <returns>Hash code</returns>
        public override int GetHashCode()
        {
            unchecked // Overflow is fine, just wrap
            {
                int hashCode = 41;
                if (this.ActualInstance != null)
                    hashCode = hashCode * 59 + this.ActualInstance.GetHashCode();
                return hashCode;
            }
        }
    }

    /// <summary>
    /// </summary>
    public class ReplaceConnectionRequestOptionsJsonConverter : JsonConverter
    {
        /// <summary>
        /// To write the JSON string
        /// </summary>
        /// <param name="writer">JSON writer</param>
        /// <param name="value">Object to be converted into a JSON string</param>
        /// <param name="serializer">JSON Serializer</param>
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            writer.WriteRawValue((string)(typeof(ReplaceConnectionRequestOptions).GetMethod("ToJson").Invoke(value, null)));
        }

        /// <summary>
        /// To convert a JSON string into an object
        /// </summary>
        /// <param name="reader">JSON reader</param>
        /// <param name="objectType">Object type</param>
        /// <param name="existingValue">Existing value</param>
        /// <param name="serializer">JSON Serializer</param>
        /// <returns>The object converted from the JSON string</returns>
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if(reader.TokenType != JsonToken.Null)
            {
                return ReplaceConnectionRequestOptions.FromJson(JObject.Load(reader).ToString(Formatting.None));
            }
            return null;
        }

        /// <summary>
        /// Check if the object can be converted
        /// </summary>
        /// <param name="objectType">Object type</param>
        /// <returns>True if the object can be converted</returns>
        public override bool CanConvert(Type objectType)
        {
            return false;
        }
    }

}
