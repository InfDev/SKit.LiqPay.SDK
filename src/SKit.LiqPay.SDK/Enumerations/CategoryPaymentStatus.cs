
using System.Runtime.ConstrainedExecution;

namespace SKit.LiqPay.SDK
{
    /// <summary>
    /// Payment Status Categories
    /// </summary>
    /// <remarks>Категорії статусів платежу</remarks>
    public enum CategoryPaymentStatus
    {
        /// <summary>
        /// Final payment statuses
        /// </summary>
        /// <remarks>Кінцеві статуси платежу</remarks>
        Final = 1,
        /// <summary>
        /// Requiring confirmation
        /// </summary>
        /// <remarks>Вимагають підтвердження</remarks>
        RequiresConfirmation = 2,
        /// <summary>
        /// Інші статуси платежу
        /// </summary>
        /// <remarks>Інші статуси платежу</remarks>
        Misc = 3
    }
}
