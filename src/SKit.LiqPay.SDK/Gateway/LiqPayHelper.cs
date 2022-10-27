using System.Security.Cryptography;

namespace SKit.LiqPay.SDK
{
    /// <summary>
    /// Represents LiqPay helper
    /// </summary>
    public static class LiqPayHelper
    {
        /// <summary>
        /// Converts string to its equivalent string representation
        /// that is encoded with base-64 digits.
        /// </summary>
        /// <param name="text"></param>
        /// <returns>The string representation, in base 64, of the contents of .NET string</returns>
        /// <remarks>Перетворює .NET рядок в його еквівалентне представлення рядка,
        /// яке закодовано 64 символами ASCII.</remarks>
        public static string ToBase64String(this string text)
            => Encoding.UTF8.GetBytes(text).ToBase64String();

        /// <summary>
        /// Converts an array of 8-bit unsigned integers to its equivalent string representation
        /// that is encoded with base-64 digits.
        /// </summary>
        /// <param name="bytes">An array of 8-bit unsigned integers</param>
        /// <returns>The string representation, in base 64, of the contents of inArray.</returns>
        /// <remarks>Перетворює масив 8-розрядних цілих чисел без знака в його еквівалентне представлення рядка,
        /// яке закодовано 64 символами ASCII.</remarks>
        public static string ToBase64String(this byte[] bytes)
            => Convert.ToBase64String(bytes);

        /// <summary>
        /// Converts the specified string, which encodes binary data as base-64 digits,
        /// to an equivalent .NET string
        /// </summary>
        /// <param name="encodedText">The string to convert</param>
        /// <returns>.NET string that is equivalent to encodedText</returns>
        /// <remarks>Перетворює вказаний рядок, який кодує бінарні дані 64-ма символами ASCII, на еквівалентний рядок .NET</remarks>
        public static string DecodeBase64(this string encodedText)
            => Encoding.UTF8.GetString(Convert.FromBase64String(encodedText));

        /// <summary>
        /// Converts the specified string to UTF-8 format and compute the hash using the SHA1 algorithm
        /// </summary>
        /// <param name="value"></param>
        /// <returns>SHA1 hash</returns>
        /// <remarks>Перетворює вказаний рядок на формат у UTF-8 і обчислює хеш за допомогою алгоритму SHA1</remarks>
        public static byte[] SHA1Hash(this string value)
            => SHA1.Create().ComputeHash(Encoding.UTF8.GetBytes(value));

        /// <summary>
        /// Supported languages for the buyer
        /// </summary>
        /// <remarks>Підтримувані мови для покупця</remarks>
        public static readonly string[] SupportedLanguageForClient  = new string[] { "uk", "ru", "en" };

        /// <summary>
        /// Safely returns only supported languages for the buyer
        /// </summary>
        /// <param name="twoLetterISOLanguageName"></param>
        /// <returns>Безпечно повертає лише підтримувані мови для покупця</returns>
        public static string ToSupportedLanguageForClient(string? twoLetterISOLanguageName)
        {
            if (twoLetterISOLanguageName == null || !SupportedLanguageForClient.Contains(twoLetterISOLanguageName))
                return LiqPayConsts.DefaultLanguage;
            return twoLetterISOLanguageName;
        }

        /// <summary>
        /// Getting the data signature
        /// </summary>
        /// <param name="base64EncodedData">Data in Json converted to Base64 format</param>
        /// <param name="privateKey"></param>
        /// <returns></returns>
        public static string CreateSignature(string base64EncodedData, string privateKey) 
                => (privateKey + base64EncodedData + privateKey)
                .SHA1Hash()
                .ToBase64String();
    }
}