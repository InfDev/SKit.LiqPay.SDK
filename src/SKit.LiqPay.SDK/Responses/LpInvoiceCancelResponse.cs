
namespace SKit.LiqPay.SDK
{
    /// <summary>
    /// Invoice cancellation response
    /// </summary>
    /// <remarks>Відповідь на скасування інвойсу</remarks>
    public class LpInvoiceCancelResponse
    {
        /// <summary>
        /// The result of a request 'ok' or 'error'
        /// </summary>
        /// <remarks>Результат виконання запиту 'ok' або 'error'</remarks>
        public LpResult? Result { get; set; }
        /// <summary>
        /// Unique identifier of the invoice
        /// </summary>
        /// <remarks>Унікальний ідентифікатор інвойсу</remarks>
        public double? InvoiceId { get; set; }
    }
}
