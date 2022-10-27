
using Microsoft.AspNetCore.Http;
using System.Net.Mime;

namespace SKit.LiqPay.SDK
{
    /// <summary>
    /// Basic pack response of notification
    /// </summary>
    public class LiqPayPackResponse
    {
        /// <summary>
        /// Json string encoded by the base64 function
        /// </summary>
        public string? Data { get; set; }
        /// <summary>
        /// Signature of data with private key
        /// </summary>
        public string? Signature { get; set; }

        // If error

        /// <summary>
        /// Code
        /// </summary>
        public string? Code { get; set; }
        /// <summary>
        /// Error code 
        /// </summary>
        public string? ErrCode { get; set; }
        /// <summary>
        /// Error description
        /// </summary>
        public string? ErrDescription { get; set; }
        /// <summary>
        /// Key
        /// </summary>
        public string? Key { get; set; }
        /// <summary>
        /// Result
        /// </summary>
        public string? Result { get; set; }
        /// <summary>
        /// Status
        /// </summary>
        public string? Status { get; set; }

        ///// <summary>
        ///// Full control of model binding.
        ///// <see href="https://learn.microsoft.com/en-us/aspnet/core/fundamentals/minimal-apis?view=aspnetcore-6.0#binding-precedence">Binding Precedence</see>
        ///// </summary>
        ///// <param name="httpContext"></param>
        ///// <returns></returns>
        ///// <remarks>FromForm attribute not supported in .NET 6 minimal API</remarks>
        //public static ValueTask<LpPackResponse?> BindAsync(HttpContext httpContext)
        //{
        //    if (httpContext.Request.HasFormContentType) // application/x-www-form-urlencoded 
        //    {
        //        var dict = httpContext.Request.Form.ToDictionary(x => x.Key, x => x.Value.ToString());
        //        return ValueTask.FromResult<LpPackResponse?>(
        //            new LpPackResponse
        //            {
        //                Data = dict.GetValueOrDefault("data"),
        //                Signature = dict.GetValueOrDefault("signature"),
        //                Code = dict.GetValueOrDefault("code"),
        //                ErrCode = dict.GetValueOrDefault("err_code"),
        //                ErrDescription = dict.GetValueOrDefault("err_description"),
        //                Key = dict.GetValueOrDefault("key"),
        //                Result = dict.GetValueOrDefault("result"),
        //                Status = dict.GetValueOrDefault("status")
        //            });
        //    }
        //    if (httpContext.Request.Method != HttpMethods.Post
        //        && httpContext.Request.ContentType == MediaTypeNames.Application.Json)
        //    {
        //        var lpPack = httpContext.Request.PeekBody<LpPackResponse>(null, LiqPayGateway.JsonOptions);
        //        return ValueTask.FromResult<LpPackResponse?>(lpPack);
        //    }

        //    // Below just as an example of GET request data binding
        //    // Ниже только в качестве образца привязки данных запроса по GET

        //    //if (httpContext.Request.Method != HttpMethods.Get)
        //    //{
        //    //    var query = httpContext.Request.Query;
        //    //    return ValueTask.FromResult<LpPackResponse?>(
        //    //        new LpPackResponse
        //    //        {
        //    //            Data = query["data"],
        //    //            Signature = query["signature"],
        //    //            Code = query["code"],
        //    //            ErrCode = query["err_code"],
        //    //            ErrDescription = query["err_description"],
        //    //            Key = query["key"],
        //    //            Result = query["result"],
        //    //            Status = query["status"]
        //    //        });
        //    //}

        //    return ValueTask.FromResult<LpPackResponse?>(null);
        //}

    }
}
