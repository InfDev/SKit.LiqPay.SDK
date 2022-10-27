// Source: https://github.com/nogic1008/JsonConverters/tree/main/src/Nogic.JsonConverters

namespace SKit.LiqPay.SDK.Converters;

/// <summary>Converter to convert enums to and from strings.</summary>
public class EnumStringConverterFactory : JsonConverterFactory
{
    /// <summary>
    /// <inheritdoc cref="EnumStringConverterFactory(bool, JsonNamingPolicy?)" path="/param[@name='allowIntegerValues']"/>
    /// </summary>
    private readonly bool _allowIntegerValues;

    /// <summary>
    /// <inheritdoc cref="EnumStringConverterFactory(bool, JsonNamingPolicy?)" path="/param[@name='namingPolicy']"/>
    /// </summary>
    private readonly JsonNamingPolicy? _namingPolicy;

    /// <summary>Initializes a new instance of <see cref="EnumStringConverterFactory"/>.</summary>
    /// <param name="allowIntegerValues">
    /// True to allow undefined <see langword="enum"/> values.
    /// When <see langword="true"/>, if an <see langword="enum"/> value isn't defined it will output as a number rather than a <see langword="string"/>.
    /// </param>
    /// <param name="namingPolicy">Naming policy of <see langword="enum"/> strings.</param>
    public EnumStringConverterFactory(bool allowIntegerValues = true, JsonNamingPolicy? namingPolicy = null)
        => (_allowIntegerValues, _namingPolicy) = (allowIntegerValues, namingPolicy);

    /// <inheritdoc/>
    public override bool CanConvert(Type typeToConvert) => typeToConvert.IsEnum;

    /// <inheritdoc/>
    public override JsonConverter? CreateConverter(Type typeToConvert, JsonSerializerOptions? options)
        => (JsonConverter?)Activator.CreateInstance(
            typeof(EnumStringConverter<>).MakeGenericType(typeToConvert),
            new object?[] { _allowIntegerValues, _namingPolicy, options });
}
