
using System.Security.Cryptography.X509Certificates;

namespace SKit.LiqPay.SDK
{
    /// <summary>
    /// Payment base status for callback notification
    /// See <see href="https://www.liqpay.ua/en/documentation/api/callback">callback (ENG)</see>
    /// or <see href="https://www.liqpay.ua/documentation/api/callback">callback (UKR)</see> dоcumentation.
    /// </summary>
    public class LpCheckoutNotification : LpPaymentStateBase
    {
        ///// <summary>
        ///// Дата списання коштів
        ///// </summary>
        //public DateTime? CompletionDate { get; set; }
        ///// <summary>
        ///// Унікальний ідентифікатор користувача на сайті мерчанта.
        ///// Максимальна довжина 100 символів
        ///// </summary>
        //public string? Customer { get; set; }
        ///// <summary>
        ///// Код помилки
        ///// </summary>
        //public string? ErrCode { get; set; }
        ///// <summary>
        ///// Опис помилки
        ///// </summary>
        //public string? ErrDescription { get; set; }
        ///// <summary>
        ///// Код помилки
        ///// </summary>
        //public string? ErrErc { get; set; }
        ///// <summary>
        ///// Посилання на яке необхідно перенаправляти клієнта для проходження 3DS верифікації
        ///// </summary>
        //public string? RedirectTo { get; set; }
        ///// <summary>
        ///// Дата останнього повернення по платежу
        ///// </summary>
        //public DateTime? RefundDateLast { get; set; }
        ///// <summary>
        ///// Ім'я відправника
        ///// </summary>
        //public string? SenderFirstName { get; set; }
        ///// <summary>
        ///// Прізвище відправника
        ///// </summary>
        //public string? SenderLastName { get; set; }
        ///// <summary>
        ///// Токен платежу
        ///// </summary>
        //public string? Token { get; set; }
        ///// <summary>
        ///// Категорія товару
        ///// </summary>
        //public string? ProductCategory { get; set; }
        ///// <summary>
        ///// Опис товару
        ///// </summary>
        //public string? ProductDescription { get; set; }
        ///// <summary>
        ///// Назва товару
        ///// </summary>
        //public string? ProductName{ get; set; }
        ///// <summary>
        ///// Адреса сторінки з товаром
        ///// </summary>
        //public string? ProductUrl { get; set; }
        ///// <summary>
        ///// Сума повернення
        ///// </summary>
        //public double? RefundAmount { get; set; }
        ///// <summary>
        ///// Код верифікації
        ///// </summary>
        //public string? Verifycode { get; set; }
    }
}
