
namespace SKit.LiqPay.SDK
{
    /// <summary>
    /// Товар для чека
    /// </summary>
    public class RroGood
    {
        /// <summary>
        /// Код товару. Максимальна довжина 256 символів.
        /// </summary>
        public string Code { get; set; } = string.Empty;
        /// <summary>
        /// Назва товару
        /// </summary>
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// Ціна в копійках за quantity = 1000 (наприклад, 1 грн за 1 шт. = 100).
        /// </summary>
        public double Price { get; set; }
        /// <summary>
        /// Значення штрихкоду. Опционально. Максимальна довжина 4096 символів.
        /// </summary>
        public string? Barcode { get; set; }
        /// <summary>
        /// Текст після товару. Опционально. Максимальна довжина 1024 символи.
        /// </summary>
        public string? Footer { get; set; }
        /// <summary>
        /// Текст перед назвою товару. Опционально. Максимальна довжина 1024 символи.
        /// </summary>
        public string? Header { get; set; }
        /// <summary>
        /// Масив ставок податків. Опционально. Передавати податок, вибраний на порталі UKey під час реєстрації каси.
        /// </summary>
        public double[]? Tax { get; set; }
        /// <summary>
        /// Код УКТ ЗЕД. Опционально. Максимальна довжина 64 символи.
        /// </summary>
        public string? Uktzed { get; set; }

    }
}
