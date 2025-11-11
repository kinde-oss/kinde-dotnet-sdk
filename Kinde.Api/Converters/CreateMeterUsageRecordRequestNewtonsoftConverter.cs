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

            var jsonObject = JObject.Load(reader);

            CreateMeterUsageRecordRequest.MeterTypeCodeEnum? meterTypeCode = default(CreateMeterUsageRecordRequest.MeterTypeCodeEnum?);
            if (jsonObject["meter_type_code"] != null)
            {
                var meterTypeCodeStr = jsonObject["meter_type_code"].ToObject<string>();
                if (!string.IsNullOrEmpty(meterTypeCodeStr))
                {
                    meterTypeCode = CreateMeterUsageRecordRequest.MeterTypeCodeEnumFromString(meterTypeCodeStr);
                }
            }
            DateTimeOffset? meterUsageTimestamp = default(DateTimeOffset?);
            if (jsonObject["meter_usage_timestamp"] != null)
            {
                meterUsageTimestamp = jsonObject["meter_usage_timestamp"].ToObject<DateTimeOffset?>(serializer);
            }
            string customerAgreementId = default(string);
            if (jsonObject["customer_agreement_id"] != null)
            {
                customerAgreementId = jsonObject["customer_agreement_id"].ToObject<string>();
            }
            string billingFeatureCode = default(string);
            if (jsonObject["billing_feature_code"] != null)
            {
                billingFeatureCode = jsonObject["billing_feature_code"].ToObject<string>();
            }
            string meterValue = default(string);
            if (jsonObject["meter_value"] != null)
            {
                meterValue = jsonObject["meter_value"].ToObject<string>();
            }

            return new CreateMeterUsageRecordRequest(
                meterTypeCode: meterTypeCode != null ? new Option<CreateMeterUsageRecordRequest.MeterTypeCodeEnum?>(meterTypeCode) : default,                 meterUsageTimestamp: meterUsageTimestamp != null ? new Option<DateTimeOffset?>(meterUsageTimestamp) : default,                 customerAgreementId: customerAgreementId,                 billingFeatureCode: billingFeatureCode,                 meterValue: meterValue            );
        }

        public override void WriteJson(Newtonsoft.Json.JsonWriter writer, CreateMeterUsageRecordRequest value, Newtonsoft.Json.JsonSerializer serializer)
        {
            writer.WriteStartObject();

            if (value.MeterTypeCodeOption.IsSet && value.MeterTypeCode != null)
            {
                writer.WritePropertyName("meter_type_code");
                var meterTypeCodeStr = CreateMeterUsageRecordRequest.MeterTypeCodeEnumToJsonValue(value.MeterTypeCode.Value);
                writer.WriteValue(meterTypeCodeStr);
            }
            if (value.MeterUsageTimestampOption.IsSet && value.MeterUsageTimestamp != null)
            {
                writer.WritePropertyName("meter_usage_timestamp");
                serializer.Serialize(writer, value.MeterUsageTimestamp);
            }

            writer.WriteEndObject();
        }
    }
}