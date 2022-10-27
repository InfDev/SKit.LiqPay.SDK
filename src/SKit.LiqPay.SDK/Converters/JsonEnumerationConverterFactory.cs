
namespace SKit.LiqPay.SDK.Converters;

/// <summary>
/// Json converter for <see cref="Enumeration"/>.
/// </summary>
public class JsonEnumerationConverterFactory : JsonConverterFactory
{
    /// <inheritdoc/>
    public override bool CanConvert(Type typeToConvert)
        => (typeToConvert.BaseType == typeof(Enumeration) || typeToConvert.IsSubclassOf(typeof(Enumeration)));

    /// <inheritdoc/>
    public override JsonConverter? CreateConverter(Type typeToConvert, JsonSerializerOptions options)
        => (JsonConverter?)Activator.CreateInstance(
            typeof(JsonEnumerationConverter<>).MakeGenericType(typeToConvert));
}
