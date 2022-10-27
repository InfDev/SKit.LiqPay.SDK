
namespace SKit.LiqPay.SDK.Exceptions
{
    /// <summary>
    /// LiqPay request exception
    /// </summary>
    public class LiqPayRequestException : Exception
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="paramNames"></param>
        public LiqPayRequestException(string[] paramNames)
            : base($"Parameters not defined: {string.Join(',', paramNames)}")
        { }
    }
}
