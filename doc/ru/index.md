# SKit.LiqPay.SDK [⇑](../../README.md)

## Общие сведения

SDK для клиентов платежного сервиса "[LiqPay](https://www.liqpay.ua/ru/documentation/api/aquiring)".

SDK предоставляет базовые функции для модулей интеграции приема платежей на сайте. Поддерживаемый фунционал:

- генерация HTML-формы или гиперссылки для перехода на [платежную страницу LiqPay](https://www.liqpay.ua/ru/documentation/api/aquiring/checkout).
  Поддерживаются также пользовательские шаблоны
- декодирование [PDT и IPN](pdt-ipn.md) уведомлений от LiqPay, проверка по сигнатуре и преобразование в типизированную форму
- [оплата Server-Server](https://www.liqpay.ua/ru/documentation/api/aquiring/pay) через API без перехода на платежную страницу LiqPay
- [получение состояния платежа](https://www.liqpay.ua/ru/documentation/api/information/status)
- [выставление счета](https://www.liqpay.ua/ru/documentation/api/aquiring/invoice/doc) на e-mail заказчика в режиме Server-Server
- [отмена счета](https://www.liqpay.ua/ru/documentation/api/aquiring/invoice_cancel/doc)
 

Общие ограничения: **API версии 3**, **платформа .NET 6.0+**

Преимущества:

- строгая типизация
- наличие тестов
- легкая расширяемость
- возможность протоколирования обмена на низком уровне
- код с комментариями к документации
- проект включает примеры использования SDK
  - **ConsoleApp** - формирование кнопок оплаты, оплата карткой через API и получение состояния платежа. [Подробнее...](console-app.md)
  - **BlazorApp** - веб-приложение на платформе Blazor Server с Minimal API архитектурой. [Подробнее...](blazor-app.md)

  Результаты операций приложений (html кнопок, запросы и ответы в json) помещаются в папку решения **assets/outputs**.
