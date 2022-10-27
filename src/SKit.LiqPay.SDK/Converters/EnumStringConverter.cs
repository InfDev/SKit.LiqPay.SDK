// Source: https://github.com/nogic1008/JsonConverters/tree/main/src/Nogic.JsonConverters

using System.Collections.Concurrent;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace SKit.LiqPay.SDK.Converters;

/// <inheritdoc cref="EnumStringConverterFactory"/>
/// <typeparam name="TEnum"><see langword="enum"/> type</typeparam>
public class EnumStringConverter<TEnum> : JsonConverter<TEnum> where TEnum : struct, Enum
{
    /// <summary>Cashe of <see cref="TypeCode"/></summary>
    private static readonly TypeCode _enumTypeCode = Type.GetTypeCode(typeof(TEnum));

    /// <summary>The string that denotes that the associated number is negative.</summary>
    private static readonly string? _negativeSign = ((int)_enumTypeCode % 2) == 0 ? null : NumberFormatInfo.CurrentInfo.NegativeSign;

    /// <summary>
    /// This is used to prevent flooding <see cref="_nameCache"/> due to exponential bitwise combinations of flags.
    /// Since multiple threads can add to <see cref="_nameCache"/>, a few more values might be added.
    /// </summary>
    private const int NameCacheSizeSoftLimit = 64;

    /// <summary>Mapping <typeparamref name="TEnum"/> to Encoded string.</summary>
    private readonly ConcurrentDictionary<long, JsonEncodedText> _nameCache = new();

    /// <summary>Mapping <see langword="string"/> to <typeparamref name="TEnum"/>.</summary>
    private readonly ConcurrentDictionary<string, TEnum> _valueCache = new();

    /// <summary>
    /// <inheritdoc cref="EnumStringConverterFactory(bool, JsonNamingPolicy?)" path="/param[@name='allowIntegerValues']"/>
    /// </summary>
    private readonly bool _allowIntegerValues;

    /// <summary>
    /// <inheritdoc cref="EnumStringConverterFactory(bool, JsonNamingPolicy?)" path="/param[@name='namingPolicy']"/>
    /// </summary>
    private readonly JsonNamingPolicy? _namingPolicy;

    /// <summary>Initializes a new instance of <see cref="EnumStringConverter{TEnum}"/>.</summary>
    /// <param name="allowIntegerValues">
    /// <inheritdoc cref="EnumStringConverterFactory(bool, JsonNamingPolicy?)" path="/param[@name='allowIntegerValues']"/>
    /// </param>
    /// <param name="namingPolicy">
    /// <inheritdoc cref="EnumStringConverterFactory(bool, JsonNamingPolicy?)" path="/param[@name='namingPolicy']"/>
    /// </param>
    /// <param name="serializerOptions">The serialization options to use.</param>
    public EnumStringConverter(bool allowIntegerValues = true, JsonNamingPolicy? namingPolicy = null, JsonSerializerOptions? serializerOptions = null)
    {
        _allowIntegerValues = allowIntegerValues;
        _namingPolicy = namingPolicy;

        foreach (var item in GetEnumValues())
        {
            var jsonProp = GetJsonPropertyNameAttribute(item);
            var attr = GetEnumMemberAttribute(item);
            string value = jsonProp?.Name ?? attr?.Value ?? ConvertName(item.ToString());
            if (!TryAddNameCache(ConvertToInt64(item), value, serializerOptions))
                break;
            if (jsonProp is not null || attr?.IsValueSetExplicitly == true)
                _valueCache.TryAdd(value, item);
        }

        static TEnum[] GetEnumValues() =>
#if NET5_0_OR_GREATER
            Enum.GetValues<TEnum>();
#else
            (TEnum[])Enum.GetValues(typeof(TEnum));
#endif

        static JsonPropertyNameAttribute? GetJsonPropertyNameAttribute(TEnum value)
            => typeof(TEnum).GetMember(value.ToString())[0]
                .GetCustomAttributes(typeof(JsonPropertyNameAttribute), false)
                .Cast<JsonPropertyNameAttribute>()
                .FirstOrDefault();

        static EnumMemberAttribute? GetEnumMemberAttribute(TEnum value)
            => typeof(TEnum).GetMember(value.ToString())[0]
                .GetCustomAttributes(typeof(EnumMemberAttribute), false)
                .Cast<EnumMemberAttribute>()
                .FirstOrDefault();
    }

    /// <inheritdoc/>
    public override TEnum Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType == JsonTokenType.Number && _allowIntegerValues)
        {
            return _enumTypeCode switch
            {
                TypeCode.Int32 => AsTEnum(reader.TryGetInt32(out int int32), ref int32),
                TypeCode.UInt32 => AsTEnum(reader.TryGetUInt32(out uint uint32), ref uint32),
                TypeCode.UInt64 => AsTEnum(reader.TryGetUInt64(out ulong uint64), ref uint64),
                TypeCode.Int64 => AsTEnum(reader.TryGetInt64(out long int64), ref int64),
                TypeCode.SByte => AsTEnum(reader.TryGetSByte(out sbyte byte8), ref byte8),
                TypeCode.Byte => AsTEnum(reader.TryGetByte(out byte ubyte8), ref ubyte8),
                TypeCode.Int16 => AsTEnum(reader.TryGetInt16(out short int16), ref int16),
                TypeCode.UInt16 => AsTEnum(reader.TryGetUInt16(out ushort uint16), ref uint16),
                _ => default, // This is dead path because TEnum is only based on above type.
            };
        }

        string enumString = reader.GetString()!;
        return _valueCache.TryGetValue(enumString, out var value) || Enum.TryParse(enumString, true, out value)
            ? value
            : throw new JsonException();

        static TEnum AsTEnum<T>(bool success, ref T value)
            => success ? Unsafe.As<T, TEnum>(ref value) : throw new JsonException();
    }

    /// <inheritdoc/>
    public override void Write(Utf8JsonWriter writer, TEnum value, JsonSerializerOptions options)
    {
        long key = ConvertToInt64(value);

        if (_nameCache.TryGetValue(key, out var formatted))
        {
            writer.WriteStringValue(formatted);
            return;
        }

        string original = value.ToString();
        if (IsValidIdentifier(original))
        {
            string converted = ConvertName(original);
            TryAddNameCache(key, converted, options);
            writer.WriteStringValue(converted);
            return;
        }

        if (_allowIntegerValues)
        {
            if (_enumTypeCode != TypeCode.UInt64)
                writer.WriteNumberValue(ConvertToInt64(value));
            else
                writer.WriteNumberValue(Unsafe.As<TEnum, ulong>(ref value));
            return;
        }

        throw new JsonException();

        static bool IsValidIdentifier(string value) =>
            value[0] >= 'A' && (_negativeSign is null || !value.StartsWith(_negativeSign, StringComparison.Ordinal));
    }

    /// <summary>Convert <typeparamref name="TEnum"/> to <see langword="long"/>.</summary>
    /// <param name="value"><typeparamref name="TEnum"/> source</param>
    private static long ConvertToInt64(TEnum value)
        => _enumTypeCode switch
        {
            TypeCode.Int32 => Unsafe.As<TEnum, int>(ref value),
            TypeCode.UInt32 => Unsafe.As<TEnum, uint>(ref value),
            TypeCode.UInt64 => (long)Unsafe.As<TEnum, ulong>(ref value),
            TypeCode.Int64 => Unsafe.As<TEnum, long>(ref value),
            TypeCode.SByte => Unsafe.As<TEnum, sbyte>(ref value),
            TypeCode.Byte => Unsafe.As<TEnum, byte>(ref value),
            TypeCode.Int16 => Unsafe.As<TEnum, short>(ref value),
            TypeCode.UInt16 => Unsafe.As<TEnum, ushort>(ref value),
            _ => default // This is dead path because TEnum is only based on above type.
        };

    /// <summary>
    /// Try to add (<typeparamref name="TEnum"/>, string) pair to <see cref="_nameCache"/>.
    /// </summary>
    /// <param name="key"><typeparamref name="TEnum"/> value (cast)</param>
    /// <param name="name"><typeparamref name="TEnum"/> name</param>
    /// <param name="options">Encoding option</param>
    /// <returns>
    /// <see langword="true"/> if succeed add, <see langword="falue"/> if exceed to <see cref="NameCacheSizeSoftLimit"/>.
    /// </returns>
    private bool TryAddNameCache(long key, string name, JsonSerializerOptions? options)
    {
        if (_nameCache.Count >= NameCacheSizeSoftLimit)
            return false;
        var encoder = options?.Encoder;
        _nameCache.TryAdd(key, JsonEncodedText.Encode(name, encoder));
        return true;
    }

    /// <summary>
    /// Converts the specified name according to the <see cref="_namingPolicy"/>.
    /// </summary>
    /// <param name="name">The name to convert.</param>
    /// <returns>The converted name.</returns>
    private string ConvertName(string name)
    {
        if (_namingPolicy is null)
            return name;

        const string ValueSeparator = ", ";
        if (!name.Contains(ValueSeparator))
            return _namingPolicy.ConvertName(name);

        string[] enumValues = name.Split(new string[] { ValueSeparator }, StringSplitOptions.None);
        for (int i = 0; i < enumValues.Length; i++)
            enumValues[i] = _namingPolicy.ConvertName(enumValues[i]);
        return string.Join(ValueSeparator, enumValues);
    }
}
