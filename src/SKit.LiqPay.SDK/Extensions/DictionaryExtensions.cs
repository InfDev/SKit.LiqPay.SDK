
namespace SKit.LiqPay.SDK
{
    public static class DictionaryExtensions
    {
        public static TValue? GetValueOrDefault<TKey, TValue>(this Dictionary<TKey, TValue> dict, TKey key)
            where TKey : notnull
        {
            if (dict.TryGetValue(key, out var value))
            {
                return value;
            }
            return default(TValue);
        }

        public static TValue? GetValueOrElse<TKey, TValue>(this Dictionary<TKey, TValue> dict, TKey key, TValue? elseValue)
            where TKey : notnull
        {
            if (dict.TryGetValue(key, out var value))
            {
                return value;
            }
            return elseValue;
        }
    }
}
