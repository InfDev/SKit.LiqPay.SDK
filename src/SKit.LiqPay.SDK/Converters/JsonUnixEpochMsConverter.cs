
namespace SKit.LiqPay.SDK.Converters
{
    /// <summary>
    /// Converter Unix Epoch time (milliseconds since 01.01.1970) ↔ DateTime (UTC).
    /// See <see href="https://www.epochconverter.com/">Dоcumentation</see>
    /// </summary>
    /// <remark>Конвертер Unix Epoch time (милисекунд з 01.01.1970) ↔ DateTime (UTC).</remark>
    public class JsonUnixEpochMsConverter : JsonConverter<DateTime>
    {
        private static readonly DateTime EpochBegin = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        /// <inheritdoc/>
        public override DateTime Read(ref Utf8JsonReader reader, 
            Type typeToConvert, 
            JsonSerializerOptions options)
                => EpochBegin.AddMilliseconds(reader.GetInt64());

        /// <inheritdoc/>
        public override void Write(Utf8JsonWriter writer,
            DateTime dateTimeValue,
            JsonSerializerOptions options) 
                => writer.WriteRawValue((dateTimeValue.ToUniversalTime() - EpochBegin).TotalMilliseconds.ToString());
    }
}