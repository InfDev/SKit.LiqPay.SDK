using Microsoft.AspNetCore.Http;

namespace SKit.LiqPay.SDK
{
    public static class HttpRequestExtensions
    {
        /// <summary>
        /// Peek at the Http request stream without consuming it
        /// </summary>
        /// <param name="request">Http Request object</param>
        /// <param name="encoding">user's desired encoding</param>
        /// <returns>string representation of the request body</returns>
        public static string PeekBody(this HttpRequest request, Encoding? encoding = null)
        {
            try
            {
                if (!request.ContentLength.HasValue || request.ContentLength == 0)
                    return String.Empty;
                if (encoding == null)
                    encoding = new UTF8Encoding();
                var buffer = new byte[Convert.ToInt32(request.ContentLength)];
                request.Body.Read(buffer, 0, buffer.Length);
                return encoding.GetString(buffer);
            }
            finally
            {
                request.Body.Position = 0;
            }
        }

        /// <summary>
        /// Asynchronous Peek at the Http request stream without consuming it
        /// </summary>
        /// <param name="request">Http Request object</param>
        /// <param name="encoding">user's desired encoding</param>
        /// <returns>string representation of the request body</returns>
        public static async Task<string> PeekBodyAsync(this HttpRequest request, Encoding? encoding = null)
        {
            try
            {
                if (!request.ContentLength.HasValue || request.ContentLength == 0)
                    return String.Empty;
                if (encoding == null)
                    encoding = new UTF8Encoding();
                var buffer = new byte[Convert.ToInt32(request.ContentLength)];
                await request.Body.ReadAsync(buffer, 0, buffer.Length);
                return encoding.GetString(buffer);
            }
            finally
            {
                request.Body.Position = 0;
            }
        }

        /// <summary>
        /// Peek at the Http request stream without consuming it
        /// </summary>
        /// <param name="request">Http Request object</param>
        /// <param name="encoding">user's desired encoding</param>
        /// <param name="jsonOptions"><see cref="JsonSerializerOptions"/></param>
        /// <returns>T type which provided at invocation</returns>
        public static T? PeekBody<T>(this HttpRequest request, Encoding? encoding = null,
        JsonSerializerOptions? jsonOptions = null) where T : class, new()
        {
            var content = request.PeekBody(encoding);
            return JsonSerializer.Deserialize<T>(content, jsonOptions);
        }

        /// <summary>
        /// Peek asynchronously at the Http request stream without consuming it
        /// </summary>
        /// <param name="request">Http Request object</param>
        /// <param name="encoding">user's desired encoding</param>
        /// <param name="jsonOptions"><see cref="JsonSerializerOptions"/></param>
        /// <returns>T type which provided at invocation</returns>
        public static async Task<T?> PeekBodyAsync<T>(this HttpRequest request, Encoding? encoding = null,
            JsonSerializerOptions? jsonOptions = null) where T : class, new()
        {
            var content = await request.PeekBodyAsync(encoding);
            return JsonSerializer.Deserialize<T>(content, jsonOptions);
        }
    }
}
