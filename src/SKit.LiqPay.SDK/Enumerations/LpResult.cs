
namespace SKit.LiqPay.SDK
{
    /// <summary>
    /// Query result
    /// </summary>
    /// <remarks>Результат виконання запиту</remarks>
    public class LpResult : Enumeration
    {
        public static readonly LpResult Error = new LpResult(1, "error");
        public static readonly LpResult Ok = new LpResult(2, "ok");

        protected LpResult(int id, string name) : base(id, name)
        { }
    }
}
