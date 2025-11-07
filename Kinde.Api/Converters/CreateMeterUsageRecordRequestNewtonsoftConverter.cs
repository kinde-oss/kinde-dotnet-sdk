using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Kinde.Api.Model;
using Kinde.Api.Client;

namespace Kinde.Api.Converters
{
    /// <summary>
    /// Newtonsoft.Json converter for CreateMeterUsageRecordRequest that handles the Option<> structure
    /// </summary>
    public class CreateMeterUsageRecordRequestNewtonsoftConverter : Newtonsoft.Json.JsonConverter<CreateMeterUsageRecordRequest>
    {
        public override bool CanRead => true;
        public override bool CanWrite => true;

        public override CreateMeterUsageRecordRequest ReadJson(Newtonsoft.Json.JsonReader reader, Type objectType, CreateMeterUsageRecordRequest existingValue, bool hasExistingValue, Newtonsoft.Json.JsonSerializer serializer)
        {
            if (reader.TokenType != Newtonsoft.Json.JsonToken.StartObject)
            {
                throw new Newtonsoft.Json.JsonException($"Expected StartObject, got {reader.TokenType}");
            }

            string customerAgreementId = default(string);
            string billingFeatureCode = default(string);
            string meterValue = default(string);
            CreateMeterUsageRecordRequest.MeterTypeCodeEnum? meterTypeCode = null;
            DateTimeOffset? meterUsageTimestamp = null;

            var jsonObject = JObject.Load(reader);

            if (jsonObject["customer_agreement_id"] != null)
            {
                customerAgreementId = jsonObject["customer_agreement_id"].ToObject<string>();
            }

            if (jsonObject["billing_feature_code"] != null)
            {
                billingFeatureCode = jsonObject["billing_feature_code"].ToObject<string>();
            }

            if (jsonObject["meter_value"] != null)
            {
                meterValue = jsonObject["meter_value"].ToObject<string>();
            }

            if (jsonObject["meter_type_code"] != null)
            {
                var meterTypeCodeStr = jsonObject["meter_type_code"].ToObject<string>();
                if (!string.IsNullOrEmpty(meterTypeCodeStr))
                {
                    meterTypeCode = CreateMeterUsageRecordRequest.MeterTypeCodeEnumFromString(meterTypeCodeStr);
                }
            }

            if (jsonObject["meter_usage_timestamp"] != null)
            {
                meterUsageTimestamp = jsonObject["meter_usage_timestamp"].ToObject<DateTimeOffset?>();
            }

            return new CreateMeterUsageRecordRequest(
                customerAgreementId: customerAgreementId, billingFeatureCode: billingFeatureCode, meterValue: meterValue, meterTypeCode: meterTypeCode != null ? new Option<CreateMeterUsageRecordRequest.MeterTypeCodeEnum?>(meterTypeCode) : default, meterUsageTimestamp: meterUsageTimestamp != null ? new Option<DateTimeOffset?>(meterUsageTimestamp) : default
            );
        }

        public override void WriteJson(Newtonsoft.Json.JsonWriter writer, CreateMeterUsageRecordRequest value, Newtonsoft.Json.JsonSerializer serializer)
        {
            writer.WriteStartObject();

            if (value.MeterTypeCodeOption.IsSet && value.MeterTypeCode != null)
            {
                writer.WritePropertyName("meter_type_code");
                var metertypecodeStr = CreateMeterUsageRecordRequest.MeterTypeCodeEnumToJsonValue(value.MeterTypeCode.Value);
                writer.WriteValue(metertypecodeStr);
            }

            if (value.MeterUsageTimestampOption.IsSet && value.MeterUsageTimestamp != null)
            {
                writer.WritePropertyName("meter_usage_timestamp");
                writer.WriteValue(value.MeterUsageTimestamp.Value);
            }

            writer.WriteEndObject();
        }
    }
}
