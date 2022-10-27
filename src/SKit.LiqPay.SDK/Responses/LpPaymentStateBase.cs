
using SKit.LiqPay.SDK.Converters;

namespace SKit.LiqPay.SDK
{
    /// <summary>
    /// Payment base status for callback notification and getting payment status
    /// See <see href="https://www.liqpay.ua/en/documentation/api/information/status/doc">status (ENG)</see>
    /// or <see href="https://www.liqpay.ua/documentation/api/information/status/doc">status (UKR)</see> dоcumentation.
    /// See <see href="https://www.liqpay.ua/en/documentation/api/callback">callback (ENG)</see>
    /// or <see href="https://www.liqpay.ua/documentation/api/callback">callback (UKR)</see> dоcumentation.
    /// </summary>
    public class LpPaymentStateBase
    {
        /// <summary>
        /// ID еквайера.
        /// Еква́йр - банк або інша фінансова установа, що надає послуги еквайрингу,
        /// тобто, здійснює розрахунки з підприємствами, які приймають оплату
        /// від держателів платіжних карток за товари чи послуги або видають їм готівку
        /// </summary>
        public long AcqId { get; set; }
        /// <summary>
        /// Тип операції. Можливі значення:
        /// <list type="bullet">
        /// <item>pay - платіж</item>
        /// <item>hold - блокування коштів на рахунку відправника</item>
        /// <item>paysplit - розщеплення платежу</item>
        /// <item>subscribe - створення регулярного платежу</item>
        /// <item>paydonate - пожертвування</item>
        /// <item>auth - предавторізація картки</item>
        /// <item>regular - регулярний платіж</item>
        /// </list>
        /// </summary>
        public string Action { get; set; } = string.Empty;
        /// <summary>
        /// Комісія агента в валюті платежу
        /// </summary>
        public double AgentCommission { get; set; }
        /// <summary>
        /// Сума платежу
        /// </summary>
        public double Amount { get; set; }
        /// <summary>
        /// Бонус відправника у валюті платежу debit
        /// </summary>
        public double? AmountBonus { get; set; }
        /// <summary>
        /// Сума транзакції credit в валюті CurrencyCredit
        /// </summary>
        public double? AmountCredit { get; set; }
        /// <summary>
        /// Сума транзакції debit у валюті CurrencyDebit
        /// </summary>
        public double? AmountDebit { get; set; }
        /// <summary>
        /// Код авторизації по транзакції credit
        /// </summary>
        public string? AuthcodeCredit { get; set; }
        /// <summary>
        /// Код авторизації по транзакції debit
        /// </summary>
        public string? AuthcodeDebit { get; set; }
        /// <summary>
        /// Розмір знижки в процентах
        /// </summary>
        public double? BonusProcent { get; set; }
        /// <summary>
        /// Тип бонусу, можливі значення:
        /// <list type="bullet">
        /// <item>bonusplus</item>
        /// <item>discount_club</item>
        /// <item>personal</item>
        /// <item>promo</item>
        /// </list>
        /// </summary>
        public string? BonusType { get; set; }
        /// <summary>
        /// Token картки відправника
        /// </summary>
        public string? CardToken { get; set; }
        /// <summary>
        /// Комісія з одержувача у валюті CurrencyCredit
        /// </summary>
        public double? CommissionCredit { get; set; }
        /// <summary>
        /// Комісія з відправника у валюті CurrencyDebit
        /// </summary>
        public double? CommissionDebit { get; set; }
        /// <summary>
        /// Дата списання коштів
        /// </summary>
        public DateTime? CompletionDate { get; set; } // Format ?!
        /// <summary>
        /// Дата створення платежу
        /// </summary>
        [JsonConverter(typeof(JsonUnixEpochMsConverter))]
        public DateTime? CreateDate { get; set; } // Format ?!
        /// <summary>
        /// Валюта платежу
        /// </summary>
        public LpSupportedCurrency? Currency { get; set; }
        /// <summary>
        /// Валюта транзакції credit
        /// </summary>
        public LpSupportedCurrency? CurrencyCredit { get; set; }
        /// <summary>
        /// Валюта транзакції debit
        /// </summary>
        public LpSupportedCurrency? CurrencyDebit { get; set; }
        /// <summary>
        /// Унікальний ідентифікатор користувача на сайті мерчанта.
        /// Максимальна довжина 100 символів
        /// </summary>
        public string? Customer { get; set; }
        /// <summary>
        /// Коментар до платежу
        /// </summary>
        public string Description { get; set; } = string.Empty;
        /// <summary>
        /// Дата завершення/зміни платежу
        /// </summary>
        [JsonConverter(typeof(JsonUnixEpochMsConverter))]
        public DateTime? EndDate { get; set; } // Format?!
        /// <summary>
        /// Код помилки
        /// </summary>
        public string? ErrCode { get; set; }
        /// <summary>
        /// Опис помилки
        /// </summary>
        public string? ErrDescription { get; set; }
        /// <summary>
        /// Додаткова інформація про платіж
        /// </summary>
        public string? Info { get; set; }
        /// <summary>
        /// IP адреса відправника
        /// </summary>
        public string? Ip { get; set; }
        /// <summary>
        /// Транзакція з 3DS перевіркою
        /// </summary>
        public bool Is3ds { get; set; }
        /// <summary>
        /// Мова
        /// </summary>
        public string? Language { get; set; }
        /// <summary>
        /// OrderId платежу в системі LiqPay
        /// </summary>
        public string LiqpayOrderId { get; set; } = string.Empty;
        /// <summary>
        /// Ознака оплати частинами
        /// </summary>
        public bool MomentPart { get; set; }
        /// <summary>
        /// Використання 3D-Secure. Можливі значення:
        /// <list type="bullet">
        /// <item>5 - транзакція пройшла з 3DS (емітент і еквайєр підтримують технологію 3D-Secure)</item>
        /// <item>6 - емітент картки платника не підтримує технологію 3D-Secure</item>
        /// <item>7 - операція пройшла без 3D-Secure</item>
        /// </list>
        /// </summary>
        public int MpiEci { get; set; }
        /// <summary>
        /// Id замовлення платежу
        /// </summary>
        public string OrderId { get; set; } = string.Empty;
        /// <summary>
        /// Id платежу в системі LiqPay
        /// </summary>
        public long PaymentId { get; set; }
        /// <summary>
        /// Спосіб оплати:
        /// <list type="bullet">
        /// <item>card - оплата картою</item>
        /// <item>liqpay - через кабінет liqpay</item>
        /// <item>privat24 - через кабінет Приват24</item>
        /// <item>masterpass - через кабінет masterpass</item>
        /// <item>moment_part - розстрочка</item>
        /// <item>cash - готівкою</item>
        /// <item>invoice - рахунок на e-mail</item>
        /// <item>qr - сканування qr-код</item>
        /// </list>
        /// </summary>
        public LpPaymentMethod? Paytype { get; set; }
        /// <summary>
        /// Публічний ключ магазину
        /// </summary>
        public string PublicKey { get; set; } = string.Empty;
        /// <summary>
        /// Комісія з одержувача у валюті платежу
        /// </summary>
        public double? ReceiverCommission { get; set; }
        /// <summary>
        /// Посилання на яке необхідно перенаправляти клієнта
        /// для проходження 3DS верифікації
        /// </summary>
        public string? RedirectTo { get; set; }
        /// <summary>
        /// Дата останнього повернення по платежу
        /// </summary>
        public DateTime? RefundDateLast { get; set; } // Format?!
        /// <summary>
        /// Результат виконання запиту
        /// </summary>
        public LpResult Result { get; set; } = LpResult.Error;
        /// <summary>
        /// Унікальний номер транзакції в системі авторизації і розрахунків обслуговуючого банку Retrieval Reference number
        /// </summary>
        public string? RrnCredit { get; set; }
        /// <summary>
        /// Унікальний номер транзакції в системі авторизації і розрахунків обслуговуючого банку Retrieval Reference number
        /// </summary>
        public string? RrnDebit { get; set; }
        /// <summary>
        /// Бонус відправника у валюті платежу
        /// </summary>
        public double? SenderBonus { get; set; }
        /// <summary>
        /// Банк відправника
        /// </summary>
        public string? SenderCardBank { get; set; }
        /// <summary>
        /// Країна картки відправника. Цифровий ISO 3166-1 код
        /// </summary>
        public int? SenderCardCountry { get; set; }
        /// <summary>
        /// Карта відправника
        /// </summary>
        public string? SenderCardMask2 { get; set; }
        /// <summary>
        /// Тип картки відправника MC/Visa
        /// </summary>
        public string? SenderCardType { get; set; }
        /// <summary>
        /// Комісія з відправника у валюті платежу
        /// </summary>
        public double? SenderCommission { get; set; }
        /// <summary>
        /// Ім'я відправника
        /// </summary>
        public string? SenderFirstName { get; set; }
        /// <summary>
        /// Прізвище відправника
        /// </summary>
        public string? SenderLastName { get; set; }
        /// <summary>
        /// Телефон відправника
        /// </summary>
        public string? SenderPhone { get; set; }
        /// <summary>
        /// Статус платежу
        /// </summary>
        public LpPaymentStatus? Status { get; set; }
        /// <summary>
        /// Token платежу
        /// </summary>
        public string? Token { get; set; }
        /// <summary>
        /// Id транзакції в системі LiqPay
        /// </summary>
        public long TransactionId { get; set; }
        /// <summary>
        /// Тип платежу
        /// </summary>
        public string Type { get; set; } = string.Empty;
        /// <summary>
        /// Версія API
        /// </summary>
        public double Version { get; set; }

        /// <summary>
        /// Код помилки
        /// </summary>
        public string? ErrErc { get; set; }
        /// <summary>
        /// Категорія товару
        /// </summary>
        public string? ProductCategory { get; set; }
        /// <summary>
        /// Опис товару
        /// </summary>
        public string? ProductDescription { get; set; }
        /// <summary>
        /// Назва товару
        /// </summary>
        public string? ProductName { get; set; }
        /// <summary>
        /// Адреса сторінки з товаром
        /// </summary>
        public string? ProductUrl { get; set; }
        /// <summary>
        /// Сума повернення
        /// </summary>
        public double? RefundAmount { get; set; }
        /// <summary>
        /// Код верифікації
        /// </summary>
        public string? Verifycode { get; set; }
    }
}
