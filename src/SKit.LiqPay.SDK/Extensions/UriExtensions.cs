
namespace SKit.LiqPay.SDK
{
    /// <summary>
    /// Additional extensions for forming URI/URL
    /// </summary>
    /// <remarks>Додаткові розширення для формування URI/URL</remarks>
    public static class UriExtensions
    {
        /// <summary>
        /// Adding Segments to the URL
        /// </summary>
        /// <param name="baseURL"></param>
        /// <param name="segments"></param>
        /// <returns></returns>
        /// <remarks>Додавання сегментів до URL</remarks>
        public static string AppendToURL(this string baseURL, params string[] segments)
            => string.Join("/", new[] { baseURL.TrimEnd('/') }
                    .Concat(segments.Select(s => s.Trim('/'))));

        /// <summary>
        /// Adding Segments to the URI
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="segments"></param>
        /// <returns>URI</returns>
        /// <remarks>Додавання сегментів до URI</remarks>
        public static Uri Append(this Uri uri, params string[] segments)
            => new Uri(GetUrlWithSegments(uri, segments));

        /// <summary>
        /// Додавання сегментів до URI та повертає URL
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="segments"></param>
        /// <returns>URL</returns>
        /// <remarks></remarks>
        public static string GetUrlWithSegments(this Uri uri, string[] segments)
            => AppendToURL(uri.AbsoluteUri, segments);

        public static string Origin(this Uri uri) => $"{uri.Scheme}://{uri.Authority}";
    }
}

