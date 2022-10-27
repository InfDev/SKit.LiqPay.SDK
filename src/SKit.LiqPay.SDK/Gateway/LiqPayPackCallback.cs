
using Microsoft.AspNetCore.Http;
using System.Net.Mime;

namespace SKit.LiqPay.SDK
{
    /// <summary>
    /// Packet with packed data and signature transmitted over the network
    /// </summary>
    /// <remarks>Пакет із запакованими даними та підписом, що передається по мережі</remarks>
    public class LiqPayPackCallback : LiqPayPackRequest
    {
        /// <summary>
        /// Full control of model binding.
        /// <see href="https://learn.microsoft.com/en-us/aspnet/core/fundamentals/minimal-apis?view=aspnetcore-6.0#binding-precedence">Binding Precedence</see>
        /// </summary>
        /// <param name="httpContext"></param>
        /// <returns></returns>
        /// <remarks>FromForm attribute not supported in .NET 6 minimal API</remarks>
        public static ValueTask<LiqPayPackCallback?> BindAsync(HttpContext httpContext)
        {
            if (httpContext.Request.HasFormContentType) // application/x-www-form-urlencoded 
            {
                var dict = httpContext.Request.Form.ToDictionary(x => x.Key, x => x.Value.ToString());
                return ValueTask.FromResult<LiqPayPackCallback?>(
                    new LiqPayPackCallback
                    {
                        Data = dict.GetValueOrDefault("data") ?? string.Empty,
                        Signature = dict.GetValueOrDefault("signature") ?? string.Empty,
                    });
            }
            if (httpContext.Request.Method != HttpMethods.Post
                && httpContext.Request.ContentType == MediaTypeNames.Application.Json)
            {
                var lpPack = httpContext.Request.PeekBody<LiqPayPackCallback>(null, LiqPayGatewayBase.JsonSerializerOptions);
                return ValueTask.FromResult<LiqPayPackCallback?>(lpPack);
            }
            return ValueTask.FromResult<LiqPayPackCallback?>(null);
        }
    }
}
