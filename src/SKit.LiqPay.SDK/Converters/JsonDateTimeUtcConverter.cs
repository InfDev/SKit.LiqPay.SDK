using System;
using System.Globalization;

namespace SKit.LiqPay.SDK.Converters
{
    /// <summary>
    /// 
    /// </summary>
    public class JsonDateTimeUtcConverter : JsonConverter<DateTime>
    {
        /// <inheritdoc/>
        public override DateTime Read(ref Utf8JsonReader reader,
            Type typeToConvert,
            JsonSerializerOptions options) => GetDateTime(reader.GetString()!);

        /// <inheritdoc/>
        public override void Write(Utf8JsonWriter writer,
            DateTime dateTimeValue,
            JsonSerializerOptions options) => writer.WriteStringValue(DateTimeToString(dateTimeValue));

        private static DateTime GetDateTime(string value)
        {
            if (DateTime.TryParseExact(value, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture,
                DateTimeStyles.AssumeUniversal | DateTimeStyles.AdjustToUniversal, out DateTime time))
            {
                if (time.Kind == DateTimeKind.Local)
                    time = time.ToUniversalTime();
                else if (time.Kind == DateTimeKind.Unspecified)
                    time = DateTime.SpecifyKind(time, DateTimeKind.Utc);
                return time;
            }
            time = DateTime.Parse(value, CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal);
            if (time.Kind == DateTimeKind.Local)
                time = time.ToUniversalTime();
            if (time.Kind == DateTimeKind.Unspecified)
                time = DateTime.SpecifyKind(time, DateTimeKind.Utc);
            return time;
        }

        public static string DateTimeToString(DateTime value)
        {
            if (value.Kind == DateTimeKind.Local)
                value = value.ToUniversalTime();
            else if (value.Kind == DateTimeKind.Unspecified)
                value = DateTime.SpecifyKind(value, DateTimeKind.Utc);
            return value.ToString("yyyy-MM-dd HH:mm:ss");
        }
    }
}
