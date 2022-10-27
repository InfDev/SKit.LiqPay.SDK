# Console App & SKit.LiqPaySDK [⇑](index.md)

## Загальні відомості

Використання SDK.
Приклад консольної програми без отримання PDT-повідомлення.

Функціональність:

- формування кнопок оплати
- оплата через персональну сторінку LiqPay
- оплата карткою через API
- отримання стану платежу
- протоколювання обміну з LiqPa

Перед запуском демонстраційної програми:

- створіть обліковий запис LiqPay, скопіюйте свої ключі і пропишіть їх в appsettings.json.

## Виведення

Консоль

``` txt
Get form button ... Ok
Get hyperlink ... Ok
Server-Server checkout ... Ok
Get Payment State ... Ok
```

Вихідні файли операцій формування платіжної кнопки та обміну з LiqPay розміщуються в папці рішення **assets/outputs/ConsoleApp**.

## Клієнт LiqPay

У додатку не застосовується використання залежностей, а клієнт LiqPay оформлений у вигляді Singleton-класу. HttpClient використовується тільки на час існування об'єкта сервісу LiqPayService і не більше, т.к. сокети це не безмежний ресурс.

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
