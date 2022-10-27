
namespace SKit.LiqPay.SDK
{
    /// <summary>
    /// LpPaymentStatus extensions
    /// </summary>
    public static class LpPaymentStatusExtensions
    {
        /// <summary>
        /// Returns Payment Status Category
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static CategoryPaymentStatus Category(this LpPaymentStatus value)
            => value.Id < 10 ? CategoryPaymentStatus.Final :
                value.Id < 100 ? CategoryPaymentStatus.Final :
                (value.Id >= 200 ? CategoryPaymentStatus.Misc : 
                CategoryPaymentStatus.RequiresConfirmation);

    }
}
