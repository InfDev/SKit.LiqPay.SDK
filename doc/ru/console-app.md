# Console App & SKit.LiqPaySDK [⇑](index.md)

## Общие сведения

Применение SDK.
Пример консольного приложения без получения PDT-уведомления.

Функциональность:

- формирование кнопок оплаты
- оплата через персональную страницу LiqPay
- оплата карткой через API
- получение состояния платежа
- протоколирование обмена с LiqPay

Перед запуском демонстрационного приложения:

- создайте учетную запись в LiqPay, скопируйте свои ключи и пропишите их в appsettings.json.

## Вывод

Консоль

``` txt
Get form button ... Ok
Get hyperlink ... Ok
Server-Server checkout ... Ok
Get Payment State ... Ok
```

Выходные файлы операций формирования платежной кнопки и обмена с LiqPay помещаются в папку решения **assets/outputs/ConsoleApp**.

## Клиент LiqPay

В приложении не используется внедрение зависимостей, а клиент LiqPay оформлен в виде Singleton-класса. HttpClient задействуется только на время существования объекта сервиса LiqPayService и не более, т.к. сокеты это не безграничный ресурс.

``` csharp
public sealed class LiqPayClient
{
    private LiqPayGatewayOptions _gatewayOptions;

    private static readonly Lazy<LiqPayClient> LazyInstance = new Lazy<LiqPayClient>(() => new LiqPayClient());
    private LiqPayClient()
    {
        _gatewayOptions = AppConfig.Instance.
            Configuration.GetSection("LiqPayGateway").Get<LiqPayGatewayOptions>();
    }

    public static LiqPayClient Instance => LazyInstance.Value;

    public LiqPayGatewayOptions GatewayOptions => _gatewayOptions;

    public LiqPayCheckoutButtonService GetCheckoutButtonService() =>
        new LiqPayCheckoutButtonService(new LiqPayGatewayBase(GatewayOptions));

    public LiqPayService GetService() =>
        new LiqPayService(new LiqPayGateway(new HttpClient(), GatewayOptions));
}
```
