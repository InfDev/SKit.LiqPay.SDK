
namespace SKit.LiqPay.SDK.Converters;

/// <inheritdoc cref="JsonEnumerationConverterFactory" />
public class JsonEnumerationConverter<T> : JsonConverter<T> where T : Enumeration
{
    /// <inheritdoc/>
    public override T Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var input = reader.GetString()!;
        return Enumeration.FromName<T>(input);
    }
 
    /// <inheritdoc/>
    public override void Write(Utf8JsonWriter writer, T value, JsonSerializerOptions options)
        // If value is null, this method is not called.
        => writer.WriteStringValue(value.Name);
}
